//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	EnemyGenerator.cs
//
//	Generates AI enemies that target the player. Currently using the Instantiate and Destroy method,
//  which will ultimately be replaced by a more efficient object pool.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class EnemyGenerator : Generator
{
	// Enemy prefabs.
    public GameObject enemyA;
	public GameObject enemyB;

	// Number of active enemies in each category.
    private int activeEnemyTypeB = 0;
	private int activeEnemyTypeA = 0;

	// Enemy object pools.
	private ObjectPooler enemyPoolA;
	private ObjectPooler enemyPoolB;

	// Delegate methods.
	delegate int AddActive();
	delegate int GetActive();

	protected override void Awake()
	{
		base.Awake();

		// Cache references to enemy object pools.
		enemyPoolA = gameObject.AddComponent<ObjectPooler>();
		enemyPoolB = gameObject.AddComponent<ObjectPooler>();
	}

    void Start()
    {
		if( generate )
		{
			// Initialize delegates.
			AddActive addA = AddEnemyTypeA;
			AddActive addB = AddEnemyTypeB;
			GetActive getA = GetEnemyTypeA;
			GetActive getB = GetEnemyTypeB;

			// Initialize object pools.
			enemyPoolA.Initialize( enemyA, 5, trans );
			enemyPoolB.Initialize( enemyB, 5, trans );

			// Start generation coroutines.
			StartCoroutine( GenerateEnemy( enemyPoolA, addA, getA, (int) References.GamePhases.TypeAEnemyPhase ) );
			StartCoroutine( GenerateEnemy( enemyPoolB, addB, getB, (int) References.GamePhases.TypeBEnemyPhase ) );
		}
    }

	IEnumerator GenerateEnemy( ObjectPooler enemyPool, AddActive addActive, GetActive getActive, int phase )
	{
		int active = 0;

		while( true )
		{
			if( generate )
			{
				// Determine how many enemies are currently active.
				active = getActive();

				// Get the current phase to determine how many enemies should be generated.
				int max = References.global.phaseManager.phaseMultipliers[phase];

				// Generate the appropriate number of enemies.
				while( active < max )
				{
					CreateObject( enemyPool );
					active = addActive();

					// Wait a short time between each generated enemy.
					yield return new WaitForSeconds( 0.25f );
				}
			}
			// Wait 1 second before checking if more enemies should be generated.
			yield return new WaitForSeconds( 1 );
		}
	}

	public int AddEnemyTypeA()
	{
		++activeEnemyTypeA;
		return( activeEnemyTypeA );
	}

	public int AddEnemyTypeB()
	{
		++activeEnemyTypeB;
		return( activeEnemyTypeB );
	}

	public int GetEnemyTypeA()
	{
		return( activeEnemyTypeA );
	}

	public int GetEnemyTypeB()
	{
		return( activeEnemyTypeB );
	}

    public void EnemyTypeBDestroyed()
    {
		if( activeEnemyTypeB > 0 )
       		--activeEnemyTypeB;
    }

    public void EnemyTypeADestroyed()
    {
		if( activeEnemyTypeA > 0 )
        	--activeEnemyTypeA;
    }
}
