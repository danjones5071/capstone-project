//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPooledObject : MonoBehaviour
{
	private Queue<GameObject> poolQueue;
	bool initialized = false;

	// Ensure the pooled object beigins as inactive.
	// Otherwise, OnDisable() will add the same object to the pool queue twice.
	void Awake()
	{
		gameObject.SetActive( false );
		initialized = true;
	}

	public void SetPoolQueue( Queue<GameObject> queue )
	{
		poolQueue = queue;
	}

	void OnDisable()
	{
		if( initialized )
			poolQueue.Enqueue( gameObject );
	}
}
