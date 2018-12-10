using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ProjectilePool : MonoBehaviour
{
	public GameObject laser;
	public GameObject enemyLaser;
	public ObjectPooler laserPool;
	public ObjectPooler enemyLaserPool;

	private Transform trans;

	void Awake()
	{
		trans = transform;
	}

	// Use this for initialization
	void Start ()
	{
		laserPool = gameObject.AddComponent<ObjectPooler>();
		enemyLaserPool = gameObject.AddComponent<ObjectPooler>();

		laserPool.Initialize( laser, 10, trans );
		enemyLaserPool.Initialize( enemyLaser, 15, trans );
	}

    public void DestroyAllLasers()
    {
        //TODO: rewrite this to be more generic

        // Destroy All Player Lasers
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Laser"))
        {
            obj.SetActive(false);
        }

        // Destroy All Enemy Lasers
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("EnemyLaser"))
        {
            obj.SetActive(false);
        }

    }
}
