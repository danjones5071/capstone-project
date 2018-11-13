using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAI : MonoBehaviour
{
    public GameObject enemyLaser;
    private float enemyAllowedShootingDistance = 6F;
    private bool weaponDisable = false;

    // Private variables to cache necessary components.
    public GameObject laserOrigin;		// A child of the player game object to specify where the laser should shoot from.


    //Time to maintain stay in the waypoint.
    public float holdingPositionTime = 0.5F;

    //From the enemy point of view is the player
    public Transform playerLocation;

    //Location of the next waypoint for the enemy.
    private Transform target;
    //Default frequency to update
    public float updateRate = 2f;

    //Instance to access the path.
    private Seeker seeker;
    private Rigidbody2D rb;

    //Established path to get to the target
    public Path path;

    //Speed traversing the path
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //Pause the moving of the enemy
    private bool pauseMoving = false;

    //Distance of the next waypoint
    public float nextWaypointDistance = 3F;

    //Current waypoints
    private int currentWaypoint = 0;

    //Time variables to keep track of time holding in the waypoint
    private float startTimeEnemyMov;
    private float secondsElapsedEnemyMov;

    //Time variables to keep track of shooting cycle
    private float startTimeShooting;
    private float secondsElapsedLastShooting;

    private Vector3 initialPosition;

    //Reference game object that contains all the destination for the enemy.
    public GameObject enemyArea;

    // Use this for initialization
    void Start ()
    {
        if (playerLocation == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                playerLocation = GameObject.Find("Player").transform;
            }
            else
            {
                return;
            }
        }

        if (enemyArea == null)
            enemyArea = GameObject.Find("EnemyTypeBareas");

        target = playerLocation;
        target = enemyArea.transform.GetChild(0);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
           // Debug.LogError("No Player Found?");
            return;
        }

        //spawn outside of camera view up side or down side, random.
        transform.position = ((Random.Range(0, 1) == 1 ? (new Vector3(14F, -7F)) : (new Vector3(14F, 7F))));

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        initialPosition = transform.position;
        
        startTimeShooting = Time.time;

        StartCoroutine(UpdatePath());

       
    }
	
    /// <summary>
    /// Co-routine to perform the path finding functionality of A*.
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            yield return false;
        }

        AstarPath.active.Scan(); // This is refreshing the grid for new positions of obstacles.

        seeker.StartPath(transform.position, target.position, OnPathComplete); //Setting the path.
    
        yield return new WaitForSeconds(1F / updateRate); //Refresh cycle for this function

        StartCoroutine(UpdatePath()); //Calling itself for the next run.
    }

    /// <summary>
    /// In case we need to perform an action after a path is found. Maybe not? our environment is dynamic.
    /// </summary>
    /// <param name="p"></param>
    public void OnPathComplete(Path p)
    {
       // Debug.Log("We got a path!. Did it have an error? " + p.error);

        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        //If player is killed and a new instance is made bacause the player had more lifes.
        if (playerLocation == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                playerLocation = GameObject.Find("Player").transform;
            }
            else
            {
                return;
            }
        }

        //Rotating the enemy towards the player
        transform.up = playerLocation.position - transform.position;

        secondsElapsedLastShooting = Time.time - startTimeShooting;

        if (secondsElapsedLastShooting > 3f) 
        {    
            ShootLaser();

            if(secondsElapsedLastShooting > 4f)
            {
                startTimeShooting = Time.time;
                weaponDisable = false;
            }
        }

    }

    void ShootLaser()
    {
        if (!weaponDisable && ( enemyAllowedShootingDistance < Vector3.Distance(playerLocation.position, transform.position)))
        {
            GameObject laserRef = Instantiate(enemyLaser, laserOrigin.transform.position, Quaternion.identity);

            //Rotating the laser towards the player           
            laserRef.transform.up = (playerLocation.position - transform.position);
            laserRef.transform.rotation *= Quaternion.Euler(0, 0, 90);
            
			References.global.soundEffects.PlayEnemyLaserSound();

            weaponDisable = true;
        }
    }
    
    /// <summary>
    /// Use for physcis and complex calculations.
    /// </summary>
    void FixedUpdate()
    {
        if (target == null)
        {
            //search for player here in case players dies and spawn again.
            return;
        }
       
         if (path == null)
        {
            return;
        }

        secondsElapsedEnemyMov = Time.fixedTime - startTimeEnemyMov;
        
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            // Debug.Log("End of path Reached.");
            // pathIsEnded = true;

            target = enemyArea.transform.GetChild(Random.Range(0,3));
            pauseMoving = true;
            startTimeEnemyMov = Time.fixedTime;
            return;
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        
        if (!pauseMoving)
            rb.AddForce(dir, fMode); //Moving the enemy
        else
            if (secondsElapsedEnemyMov > holdingPositionTime)
                pauseMoving = false; //Enabling movement again.        

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWaypointDistance)
        { 
            currentWaypoint++;
            return;
        }     
    }

    void ExecuteBehavior()
    {
        transform.position = initialPosition;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If the Enemy collides with a laser...
        if (col.gameObject.tag == "Laser" || col.gameObject.tag == "Inferno" || col.gameObject.tag == "EnemyLaser")
        {
            RespawnSelf();            // Destroy the Enemy.
            Destroy(col.gameObject);  // And also destroy the laser blast.
        }

        // If the asteroid collides with a player...
        if (col.gameObject.tag == "Player")
        {
            // Cache player controller component.
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();

            // Play the crash sound effect.
            References.global.soundEffects.PlayCrashSound();

            // Player takes damage from impact.
            pc.TakeDamage(15);

            // Destroy asteroid on impact with player ship.
            RespawnSelf();
        }
    }

    private void RespawnSelf()
    {
        // Instantiate our explosion particle effects and destroy them after some time.
        //Destroy(Instantiate(explosion, asteroidTransform.position, Quaternion.identity), 4);

        // Destroy the asteroid.
        ExecuteBehavior();

        // Destroy the asteroid.
        // Temp fix to avoid slow down in higher phases.
        // Object pooling should be inserted to properly handle this
        //Destroy(gameObject);

        // Play the explosion sound effect.
        References.global.soundEffects.PlayExplosionSound();
    }

    private void OnDestroy()
    {
        //EnemyGenerator.enemyTypeAspotAvailable = true;
        References.global.enemyGenerator.EnemyTypeADestroyed();
    }
}
