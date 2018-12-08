//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Generator.cs
//
//	Defines the base behavior for any generator subclass designed to continuously spawn game objects at runtime.
//  These generators include:
//  - Enemy Generator
//  - Obstacle Generator
//  - Pickup Generator.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public bool generate = true;  // Should the generator be spawning objects?
	protected Transform trans;    // Reference to the generator's tranform.

	protected virtual void Awake()
	{
		// Cache the generator's transoform component.
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

	// Disable all objects active in the pool.
	public virtual void ClearGeneratedObjects()
	{
		foreach( Transform child in trans )
		{
			child.gameObject.SetActive( false );
		}
	}
}