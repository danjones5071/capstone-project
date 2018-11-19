///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	PickupGenerator.cs
//
//	Generates pickup objects that the player can absorb to gain health, energy, and other effects.
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

	private ObjectPooler healthPool;
	private ObjectPooler energyPool;
	private ObjectPooler coinPool;

	protected override void Awake()
	{
		base.Awake();

		healthPool = gameObject.AddComponent<ObjectPooler>();
		energyPool = gameObject.AddComponent<ObjectPooler>();
		coinPool = gameObject.AddComponent<ObjectPooler>();
	}

	void Start()
	{
		healthPool.Initialize( healthPickup, 3, trans );
		energyPool.Initialize( energyPickup, 3, trans );
		coinPool.Initialize( coinPickup, 3, trans );

        // Start coroutine loop for spawning pickups.
		if( generate )
        {
			StartCoroutine( GenerateObjects(healthPool, healthPickupTimer) );
			StartCoroutine( GenerateObjects(energyPool, energyPickupTimer) );
			StartCoroutine( GenerateObjects(coinPool, coinPickupTimer) );
		}
    }
}