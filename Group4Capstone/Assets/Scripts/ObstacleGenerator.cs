﻿//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
	public GameObject asteroid;				// The asteroid prefab.
	public float asteroidTimer = 3.2f;      // How long we wait before generating another asteroid.
    public float increseDificultyTimer = 7;
    public float dificultyMultiplier = 0.3F;
    private float secondsElapsed;
    private float startTime;

    void Start()
	{
        startTime = Time.time;
        // Start the infinite coroutine to generate asteroids.
        StartCoroutine( GenerateObstacles() );
	}

	IEnumerator GenerateObstacles()
	{
		// Continue generating infinitely.
		while( true )
		{
			CreateAsteroid();

            secondsElapsed = Time.time - startTime;

            if(secondsElapsed > increseDificultyTimer)
            {
                if (asteroidTimer > dificultyMultiplier)
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

	public void CreateAsteroid()
	{
		Instantiate( asteroid );	// Instantiate a new astroid.
	}
}
