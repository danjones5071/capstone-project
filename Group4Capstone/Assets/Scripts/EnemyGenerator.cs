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

    //private float secondsElapsed;
    //private float startTime;

    public static bool enemyTypeA_alive = false;

    void Start()
    {

    }

    void Update()
    {
     //   if (!enemyA.activeInHierarchy)
       //     CreateTypeAenemy();

      //  if (!enemyB.activeInHierarchy)
           // CreateTypeBenemy();

    }

    public void CreateTypeBenemy()
	{
		Instantiate(enemyB);	
	}
    public void CreateTypeAenemy()
    {
        Instantiate(enemyA);   
    }
}
