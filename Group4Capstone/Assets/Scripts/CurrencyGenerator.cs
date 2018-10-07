using UnityEngine;
using System.Collections;

public class CurrencyGenerator : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public GameObject coin;				 // The coin prefab.
	public float coinTimer = 10.0f;      // How long we wait before generating another coin.

	void Start()
	{
		// Start the infinite coroutine to generate coins.
		StartCoroutine( GenerateAsteroids() );
	}

	IEnumerator GenerateAsteroids()
	{
		// Continue generating infinitely.
		while( true )
		{
			CreateCoin();
			yield return new WaitForSeconds( coinTimer );	// Wait a bit to generate another coin.
		}
	}



	public void CreateCoin()
	{
		Instantiate( coin );	// Instantiate a new coin.
	}
}