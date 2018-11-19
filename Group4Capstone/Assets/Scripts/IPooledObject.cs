//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPooledObject : MonoBehaviour
{
	private Queue<GameObject> poolQueue;

	public void SetPoolQueue( Queue<GameObject> queue )
	{
		poolQueue = queue;
	}

	void OnDisable()
	{
		poolQueue.Enqueue( gameObject );
	}
}
