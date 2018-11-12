//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	ObstacleGenerator.cs
//
//	Generates objects which the player must avoid or destroy. Currently using the Instantiate and Destroy method,
//  which will ultimately be replaced by a more efficient object pool.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{
    public float enemyTypeBtimer = 6.3F;

    public GameObject enemyA;
    public GameObject enemyB;

    public int ActiveEnemyTypeB = 0;
    public int MaxEnemyTypeB = 0;
    public int ActiveEnemyTypeA = 0;
    public int MaxEnemyTypeA = 0;

    void Update()
    {
        MaxEnemyTypeB = PhaseManger.PhaseMultipliers[1];
        MaxEnemyTypeA = PhaseManger.PhaseMultipliers[2];

        while (ActiveEnemyTypeB < MaxEnemyTypeB)
        {
            CreateTypeBenemy();
            ActiveEnemyTypeB++;
        }

        while (ActiveEnemyTypeA < MaxEnemyTypeA)
        {
            CreateTypeAenemy();
            ActiveEnemyTypeA++;
        }

    }

    public void CreateTypeBenemy()
	{
        // Randomize physical attributes of our new asteroid.
        float posY = Random.Range(-4.9f, 4.9f);			// Randomize the vertical position of the object.

        Instantiate(enemyB, new Vector3(transform.position.x, posY, transform.position.z), transform.rotation);	
	}
    public void CreateTypeAenemy()
    {
        Instantiate(enemyA);   
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
