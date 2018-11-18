//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	ObstacleGenerator.cs
//
//	Generates objects which the player must avoid or destroy. Currently using the Instantiate and Destroy method,
//  which will ultimately be replaced by a more efficient object pool.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ObstacleGenerator : Generator
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

    void Start()
	{
        objectPooler = ObjectPooler.Instance;
        startTime = Time.time;

        // Start the infinite coroutine to generate asteroids.
        StartCoroutine( GenerateAsteroids() );
        StartCoroutine( GenerateObjects(blackHole, blackHoleTimer) );
	}

	IEnumerator GenerateAsteroids()
	{
		// Continue generating infinitely.
		while( true )
		{
			CreateObject( asteroid );

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
}
