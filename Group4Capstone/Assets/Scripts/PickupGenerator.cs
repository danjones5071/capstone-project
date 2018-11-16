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
    public bool spawnHealthPickup = true;
    public bool spawnEnergyPickup = true;
	public bool spawnCoinPickup = true;

	private Transform trans;

	void Awake()
	{
		trans = transform;
	}

	void Start()
	{
        // Start coroutine loop for spawning health pickups.
        if( spawnHealthPickup )
        {
			StartCoroutine( GeneratePickup(healthPickup, healthPickupTimer, spawnHealthPickup) );
        }

        // Start coroutine loop for spawning energy pickups.
        if( spawnEnergyPickup )
        {
			StartCoroutine( GeneratePickup(energyPickup, energyPickupTimer, spawnEnergyPickup) );
        }

		// Start coroutine loop for spawning coin pickups.
		if( spawnCoinPickup )
		{
			StartCoroutine( GeneratePickup(coinPickup, coinPickupTimer, spawnCoinPickup) );
		}
    }

	IEnumerator GeneratePickup( GameObject pickup, float timer, bool spawn )
    {
        while( spawn )
        {
			CreatePickup( pickup );
            yield return new WaitForSeconds( timer );
        }
    }

	public void CreatePickup( GameObject pickup )
	{
		Instantiate( pickup ).transform.SetParent( trans );
	}
}