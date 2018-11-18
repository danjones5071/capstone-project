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

public class EnemyGenerator : MonoBehaviour
{
	public bool generate = true;
    public GameObject enemyA;
    public GameObject enemyB;

    public int activeEnemyTypeB = 0;
    public int activeEnemyTypeA = 0;

	private Transform trans;

	// Delegate methods.
	delegate int AddActive();
	delegate int GetActive();

	void Awake()
	{
		trans = transform;
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

			StartCoroutine( GenerateEnemy( enemyB, addB, getB, 1 ) );
			StartCoroutine( GenerateEnemy( enemyA, addA, getA, 2 ) );
		}
    }

	IEnumerator GenerateEnemy( GameObject enemy, AddActive addActive, GetActive getActive, int phase )
	{
		int active = 0;

		while( generate )
		{
			active = getActive();

			// Get the current phase to determine how many enemies should be generated.
			int max = References.global.phaseManager.phaseMultipliers[phase];

			while( active < max )
			{
				CreateEnemy( enemy );
				active = addActive();
			}

			// Wait 1 second before checking if more enemies should be generated.
			yield return new WaitForSeconds( 1 );
		}
	}

	public void CreateEnemy( GameObject enemy )
	{
		Instantiate( enemy ).transform.SetParent( trans );	
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
        --activeEnemyTypeB;
    }

    public void EnemyTypeADestroyed()
    {
        --activeEnemyTypeA;
    }
}
