using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
