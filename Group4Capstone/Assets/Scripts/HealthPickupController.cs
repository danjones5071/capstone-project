using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour {
    // Public variables which can be modified in the editor at runtime.
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    // Private variables to cache necessary components.
    public GameObject pickup;
    private float posY;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnHealthPickup", spawnTime, spawnDelay);
    }

    void SpawnHealthPickup()
    {
        // Randomize starting Y position of the pickup.
        posY = Random.Range(-4.9f, 4.9f);
        
        // Create HealthPickup instance.
        Instantiate(pickup, new Vector3(transform.position.x, posY, transform.position.z), transform.rotation);

        // End object spawning if necessary.
        if (stopSpawning)
        {
            CancelInvoke("SpawnHealthPickup");
        }
    }
}
