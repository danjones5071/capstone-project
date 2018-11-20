using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAI : Enemy
{
	public GameObject enemyArea;
    //Time to maintain stay in the waypoint.
    public float holdingPositionTime = 0.5F;

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

    private Vector3 initialPosition;

	void Awake()
	{
		speed = 2000;
		shootingDistance = 6;
		shootingCooldown = 5;
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
	}

    void OnEnable()
    {
        target = playerLocation;
        target = enemyArea.transform.GetChild(0);

        if(target == null)
        {
           // Debug.LogError("No Player Found?");
            return;
        }

        // Spawn outside of camera view up side or down side, random.
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

	void OnDisable()
	{
		References.global.enemyGenerator.EnemyTypeADestroyed();
	}
}
