using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawn : MonoBehaviour {

    public GameObject spawnedObject;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
	}

    void SpawnObject ()
    {
        Instantiate(spawnedObject, transform.position, transform.rotation);
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
