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
	public GameObject asteroid;				// The asteroid prefab.
	public float asteroidTimer = 3.2f;		// How long we wait before generating another asteroid.

	void Start()
	{
		// Start the infinite coroutine to generate asteroids.
		StartCoroutine( GenerateAsteroids() );
	}

	IEnumerator GenerateAsteroids()
	{
		// Continue generating infinitely.
		while( true )
		{
			Instantiate( asteroid );							// Instantiate a new astroid.
			yield return new WaitForSeconds( asteroidTimer );	// Wait a bit to generate another.
		}
	}
}
