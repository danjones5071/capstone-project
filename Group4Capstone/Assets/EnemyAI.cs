using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAI : MonoBehaviour
{
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

    //Reference game object that contains all the destination for the enemy.
    public GameObject enemyArea;

    // Use this for initialization
    void Start ()
    { 
        target = playerLocation;
        target = enemyArea.transform.GetChild(0);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
           // Debug.LogError("No Player Found?");
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

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
        //Rotating the enemy towards the player
        transform.up = playerLocation.position - transform.position;
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
}
