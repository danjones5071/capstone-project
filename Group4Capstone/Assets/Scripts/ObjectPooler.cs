using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private GameObject prefab;
	private Transform generator;
	private Queue<GameObject> poolQueue = new Queue<GameObject>();

	public void Initialize( GameObject prefab, int size, Transform generator )
	{
		this.prefab = prefab;
		this.generator = generator;

		for( int i = 0; i < size; ++i )
		{
			InstantiatePooledObject( false );
		}
	}

    public GameObject SpawnFromPool()
    {
        GameObject objectToSpawn = null;

		if( poolQueue.Count > 0 )
		{
			objectToSpawn = poolQueue.Dequeue();
			objectToSpawn.SetActive( true );
		}
		else
			objectToSpawn = InstantiatePooledObject( true );

        return objectToSpawn;
    }

	public GameObject SpawnFromPool( Vector2 position, Vector2 right )
	{
		GameObject objectToSpawn = SpawnFromPool();
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.right = right;

		return objectToSpawn;
	}

    public void ReturnToPool( GameObject obj )
    {
        obj.SetActive( false );
        poolQueue.Enqueue( obj );
    }

	private GameObject InstantiatePooledObject( bool active )
	{
		GameObject obj = Instantiate( prefab );
		obj.transform.SetParent( generator );
		obj.AddComponent<IPooledObject>().SetPoolQueue( poolQueue );
		poolQueue.Enqueue( obj );

		if( !active )
			obj.SetActive( false );

		return( obj );
	}

	public int getQueueCount()
	{
		return( poolQueue.Count );
	}

	public void printQueue()
	{
		foreach( GameObject obj in poolQueue )
		{
			Debug.Log( obj );
		}
	}

	public void DeactivatePooledObjects()
	{
		int count = poolQueue.Count;

		for( int i = 0; i < count; i++ )
		{
			GameObject obj = poolQueue.Dequeue();
			obj.SetActive( false );
		}
	}
}