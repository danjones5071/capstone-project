using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
    public GameObject HealthPickup;
    public GameObject EnergyPickup;
    public float healthPickupTimer = 10.0f;
    public float energyPickupTimer = 5.0f;
    public bool spawnHealthPickupActive = true;
    public bool spawnEnergyPickupActive = true;

	// Use this for initialization
	void Start()
	{
        // Start coroutine loop for spawning health pickups
        if (spawnHealthPickupActive)
        {
            StartCoroutine(SpawnHealthPickup());
        }

        // Start coroutine loop for spawning energy pickups
        if (spawnEnergyPickupActive)
        {
            StartCoroutine(SpawnEnergyPickup());
        }
    }

    IEnumerator SpawnHealthPickup()
    {
        while (spawnHealthPickupActive)
        {
            CreateHealthPickup();
            yield return new WaitForSeconds(healthPickupTimer);
        }
    }

    public void CreateHealthPickup()
    {
        // Randomize starting Y position of the pickup.
        //float posY = Random.Range(-4.9f, 4.9f);

        Instantiate( HealthPickup );
    }

    IEnumerator SpawnEnergyPickup()
    {
        while (spawnEnergyPickupActive)
        {
            CreateEnergyPickup();
            yield return new WaitForSeconds(energyPickupTimer);
        }
    }

    public void CreateEnergyPickup()
    {
        // Randomize starting Y position of the pickup.
        //float posY = Random.Range(-4.9f, 4.9f);

        Instantiate( EnergyPickup );
    }

}
