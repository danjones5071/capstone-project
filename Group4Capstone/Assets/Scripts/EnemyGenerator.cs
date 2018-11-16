//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	EnemyGenerator.cs
//
//	Generates AI enemies that target the player. Currently using the Instantiate and Destroy method,
//  which will ultimately be replaced by a more efficient object pool.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{
	public bool generate = true;
    public GameObject enemyA;
    public GameObject enemyB;

    public int activeEnemyTypeB = 0;
    public int activeEnemyTypeA = 0;

	private Transform trans;

	void Awake()
	{
		trans = transform;
	}

    void Start()
    {
		if( generate )
		{
			StartCoroutine( GenerateEnemy(enemyA, 2) );
			StartCoroutine( GenerateEnemy(enemyB, 1) );
		}
    }

	IEnumerator GenerateEnemy( GameObject enemy, int phase )
	{
		int active = 0;

		while( generate )
		{
			// Get the current phase to determine how many enemies should be generated.
			int max = References.global.phaseManager.phaseMultipliers[phase];

			// Looking for a more elegant solution. Can't pass by reference to IEnumerator.
			switch( phase )
			{
				case 1:
					active = activeEnemyTypeB;
					break;
				case 2:
					active = activeEnemyTypeA;
					break;
			}

			while( active < max )
			{
				CreateEnemy( enemy );

				// Looking for a more elegant solution. Can't pass by reference to IEnumerator.
				switch( phase )
				{
					case 1:
						++activeEnemyTypeB;
						break;
					case 2:
						++activeEnemyTypeA;
						break;
				}

				++active;
			}

			// Wait 1 second before checking if more enemies should be generated.
			yield return new WaitForSeconds( 1 );
		}
	}

	public void CreateEnemy( GameObject enemy )
	{
		Instantiate( enemy ).transform.SetParent( trans );	
	}

    public void EnemyTypeBDestroyed()
    {
        activeEnemyTypeB--;
    }

    public void EnemyTypeADestroyed()
    {
        activeEnemyTypeA--;
    }
}
