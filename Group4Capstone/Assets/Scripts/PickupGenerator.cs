using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
    public GameObject healthPickup;
    public GameObject energyPickup;
	public GameObject coinPickup;
    public float healthPickupTimer = 10.0f;
    public float energyPickupTimer = 5.0f;
	public float coinPickupTimer = 10.0f;
    public bool spawnHealthPickupActive = true;
    public bool spawnEnergyPickupActive = true;
	public bool spawnCoinPickupActive = true;

	// Use this for initialization
	void Start()
	{
        // Start coroutine loop for spawning health pickups
        if( spawnHealthPickupActive )
        {
            StartCoroutine( SpawnHealthPickup() );
        }

        // Start coroutine loop for spawning energy pickups
        if( spawnEnergyPickupActive )
        {
            StartCoroutine( SpawnEnergyPickup() );
        }

		// Start coroutine loop for spawning coin pickups
		if( spawnCoinPickupActive )
		{
			StartCoroutine( SpawnCoinPickup() );
		}
    }

    IEnumerator SpawnHealthPickup()
    {
        while( spawnHealthPickupActive )
        {
			CreateHealthPickup();
            yield return new WaitForSeconds( healthPickupTimer );
        }
    }

    IEnumerator SpawnEnergyPickup()
    {
        while( spawnEnergyPickupActive )
        {
			CreateEnergyPickup();
            yield return new WaitForSeconds( energyPickupTimer );
        }
    }

	IEnumerator SpawnCoinPickup()
	{
		while( spawnCoinPickupActive )
		{
			CreateCoinPickup();
			yield return new WaitForSeconds( coinPickupTimer );
		}
	}

	public void CreateHealthPickup()
	{
		Instantiate( healthPickup );
	}

	public void CreateEnergyPickup()
	{
		Instantiate( energyPickup );
	}

	public void CreateCoinPickup()
	{
		Instantiate( coinPickup );
	}
}
