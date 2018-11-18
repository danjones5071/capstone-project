using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : Generator
{
	// Public variables which can be modified in the editor at runtime.
    public GameObject healthPickup;
    public GameObject energyPickup;
	public GameObject coinPickup;
    public float healthPickupTimer = 10.0f;
    public float energyPickupTimer = 5.0f;
	public float coinPickupTimer = 10.0f;
    public bool spawnHealthPickup = true;
    public bool spawnEnergyPickup = true;
	public bool spawnCoinPickup = true;

	void Start()
	{
        // Start coroutine loop for spawning pickups.
        if( spawnHealthPickup )
        {
			StartCoroutine( GenerateObjects(healthPickup, healthPickupTimer) );
			StartCoroutine( GenerateObjects(energyPickup, energyPickupTimer) );
			StartCoroutine( GenerateObjects(coinPickup, coinPickupTimer) );
		}
    }
}