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
	public bool asteroidBelt = false;
	private float asteroidBeltTimer = 0.6f;
	private float timeMultiplier = 0.2f;

	private ObjectPooler asteroidPool;
	private ObjectPooler blackHolePool;

	protected override void Awake()
	{
		base.Awake();

		asteroidPool = gameObject.AddComponent<ObjectPooler>();
		blackHolePool = gameObject.AddComponent<ObjectPooler>();
	}

    void Start()
	{
		asteroidPool.Initialize( asteroid, 10, trans );
		blackHolePool.Initialize( blackHole, 5, trans );

		if( generate )
		{
			// Start the infinite coroutines to generate obstacles.
			StartCoroutine( GenerateAsteroids() );
			StartCoroutine( GenerateObjects( blackHolePool, blackHoleTimer ) );
		}
	}

	IEnumerator GenerateAsteroids()
	{
		float origTimer = asteroidTimer;
		Debug.Log( "origTimer: " + origTimer );
		int phaseMult;
		float newTime;

		// Continue generating infinitely.
		while( generate )
		{
			if( !asteroidBelt )
			{
				phaseMult = References.global.phaseManager.phaseMultipliers[(int)References.GamePhases.AsteroidPhase];
				newTime = origTimer - ( timeMultiplier * phaseMult );
				asteroidTimer = Mathf.Max( newTime, 1.0f );
			}
			else
				asteroidTimer = asteroidBeltTimer;

			CreateObject( asteroidPool );

            yield return new WaitForSeconds( asteroidTimer );	// Wait a bit to generate another obstacle.
		}
	}
}