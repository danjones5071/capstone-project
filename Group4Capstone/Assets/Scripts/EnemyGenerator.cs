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


    public static bool enemyTypeAspotAvailable = true;
    public static bool enemyTypeBspotAvailable = true;

    public static bool enemyTypeA_alive = false;


    void Update()
    {
        if (enemyTypeAspotAvailable)
            CreateTypeAenemy();
        if (enemyTypeBspotAvailable)
            CreateTypeBenemy();


    }

    public void CreateTypeBenemy()
	{
        enemyTypeBspotAvailable = false;

        Instantiate(enemyB);	
	}
    public void CreateTypeAenemy()
    {
        enemyTypeAspotAvailable = false;
        Instantiate(enemyA);   
    }
}
