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

    public int ActiveEnemyTypeB = 0;
    public int MaxEnemyTypeB = 0;
    public int ActiveEnemyTypeA = 0;
    public int MaxEnemyTypeA = 0;

    void Start()
    {
		if( generate )
		{
			StartCoroutine( GeneratorTypeA() );
			StartCoroutine( GeneratorTypeB() );
		}
    }

	IEnumerator GeneratorTypeA()
	{
		while( generate )
		{
			// Get the current phase to determine how many enemies should be generated.
			MaxEnemyTypeA = References.global.phaseManager.phaseMultipliers[2];

			while( ActiveEnemyTypeA < MaxEnemyTypeA )
			{
				CreateTypeAEnemy();
				ActiveEnemyTypeA++;
			}

			yield return new WaitForSeconds( 1 );
		}
	}

	IEnumerator GeneratorTypeB()
	{
		while( generate )
		{
			// Get the current phase to determine how many enemies should be generated.
			MaxEnemyTypeB = References.global.phaseManager.phaseMultipliers[1];

			while( ActiveEnemyTypeB < MaxEnemyTypeB )
			{
				CreateTypeBEnemy();
				ActiveEnemyTypeB++;
			}

			yield return new WaitForSeconds( 1 );
		}
	}

    public void CreateTypeBEnemy()
	{
        // Randomize physical attributes of our new asteroid.
        float posY = Random.Range( -4.9f, 4.9f );   // Randomize the vertical position of the object.

		Instantiate( enemyB, new Vector3(transform.position.x, posY, transform.position.z), Quaternion.identity );	
	}

    public void CreateTypeAEnemy()
    {
        Instantiate( enemyA );   
    }

    public void EnemyTypeBDestroyed()
    {
        ActiveEnemyTypeB--;
    }

    public void EnemyTypeADestroyed()
    {
        ActiveEnemyTypeA--;
    }
}
