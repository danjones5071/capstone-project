//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	EnemyGenerator.cs
//
//	Generates AI enemies that target the player. Currently using the Instantiate and Destroy method,
//  which will ultimately be replaced by a more efficient object pool.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections;

public class EnemyGenerator : Generator
{
    public GameObject enemyA;
    public GameObject enemyB;

    public int activeEnemyTypeB = 0;
    public int activeEnemyTypeA = 0;

	private ObjectPooler enemyPoolA;
	private ObjectPooler enemyPoolB;

	// Delegate methods.
	delegate int AddActive();
	delegate int GetActive();

	protected override void Awake()
	{
		base.Awake();

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

			StartCoroutine( GenerateEnemy( enemyPoolA, addA, getA, (int) References.GamePhases.TypeAEnemyPhase ) );
			StartCoroutine( GenerateEnemy( enemyPoolB, addB, getB, (int) References.GamePhases.TypeBEnemyPhase ) );
		}
    }

	IEnumerator GenerateEnemy( ObjectPooler enemyPool, AddActive addActive, GetActive getActive, int phase )
	{
		int active = 0;

		while( generate )
		{
			active = getActive();

			// Get the current phase to determine how many enemies should be generated.
			int max = References.global.phaseManager.phaseMultipliers[phase];

			while( active < max )
			{
				CreateObject( enemyPool );
				active = addActive();
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
