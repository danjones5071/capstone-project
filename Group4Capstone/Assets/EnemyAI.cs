using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAI : MonoBehaviour
{
    //From the enemy point of view is the player
    public Transform target;

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

    public float nextWaypointDistance = 3F;

    private int currentWaypoint = 0;

	// Use this for initialization
	void Start ()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            Debug.LogError("No Player Found?");
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }
	
    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            yield return false;
        }

        AstarPath.active.Scan();

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1F / updateRate);

        StartCoroutine(UpdatePath());
    }


    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path!. Did it have an error? " + p.error);

        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        transform.up = target.position - transform.position;        
    }


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

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            Debug.Log("End of path Reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Remove this line to intersect with the player position.
        //dir = new Vector3(0, dir.y, 0);

        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWaypointDistance)
        { 
            currentWaypoint++;
            return;
        }     
    }
   
}
