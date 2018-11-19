using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public bool generate = true;
	protected Transform trans;

	protected virtual void Awake()
	{
		trans = transform;
	}

	// GenerateObjects method without object pooling.
	protected virtual IEnumerator GenerateObjects( GameObject obj, float timer )
	{
		while( generate )
		{
			CreateObject( obj );
			yield return new WaitForSeconds( timer );
		}
	}

	// GenerateObjects method with object pooling.
	protected virtual IEnumerator GenerateObjects( ObjectPooler pool, float timer )
	{
		while( generate )
		{
			CreateObject( pool );
			yield return new WaitForSeconds( timer );
		}
	}

	// CreateObject method without object pooling.
	public virtual GameObject CreateObject( GameObject obj )
	{
		GameObject created = Instantiate( obj );
		created.transform.SetParent( trans );
		return created;
	}
		
	// CreateObject method with object pooling.
	public virtual GameObject CreateObject( ObjectPooler pool )
	{
		return pool.SpawnFromPool();
	}
}
