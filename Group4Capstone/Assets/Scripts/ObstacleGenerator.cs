//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	ObstacleGenerator.cs
//
//	Generates objects which the player must avoid or destroy. Currently using the Instantiate and Destroy method,
//  which will ultimately be replaced by a more efficient object pool.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public GameObject asteroid;               // The asteroid prefab.
	public GameObject blackHole;              // The blackHole prefab.
	public float asteroidTimer = 3.2f;        // How long we wait before generating another asteroid.
	public float blackHoleTimer = 8.2f;       // How long we wait before generating another asteroid.
    public float increseDificultyTimer = 50;
    public float dificultyMultiplier = 0.3F;
    private float secondsElapsed;
    private float startTime;
	private Transform trans;
    private ObjectPooler objectPooler;

	void Awake()
	{
		trans = transform;
	}

    void Start()
	{
        objectPooler = ObjectPooler.Instance;
        startTime = Time.time;

        // Start the infinite coroutine to generate asteroids.
        StartCoroutine( GenerateAsteroids() );
        StartCoroutine( GenerateBlackHole() );
	}

	IEnumerator GenerateAsteroids()
	{
		// Continue generating infinitely.
		while( true )
		{
			CreateObstacle( asteroid );

            secondsElapsed = Time.time - startTime;

            if( secondsElapsed > increseDificultyTimer )
            {
                if( asteroidTimer > dificultyMultiplier )
                {
                    asteroidTimer -= dificultyMultiplier;                    
                }
                else
                {
                    asteroidTimer = dificultyMultiplier;
                }

                startTime = Time.time;
            }

            yield return new WaitForSeconds( asteroidTimer );	// Wait a bit to generate another obstacle.
		}
	}

	IEnumerator GenerateBlackHole()
	{
	    while( true )
	    {
			CreateObstacle( blackHole );

            yield return new WaitForSeconds( blackHoleTimer );	// Wait a bit to generate another obstacle.
		}
	}

	public void CreateObstacle( GameObject obstacle )
	{
        //Instantiate( obstacle ).transform.SetParent( trans );
        objectPooler.SpawnFromPool("Asteroid", trans.position, trans.rotation);
	}
}
