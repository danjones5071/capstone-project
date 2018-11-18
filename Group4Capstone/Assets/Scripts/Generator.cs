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

	protected virtual IEnumerator GenerateObjects( GameObject obj, float timer )
	{
		while( generate )
		{
			CreateObject( obj );
			yield return new WaitForSeconds( timer );
		}
	}

	public virtual void CreateObject( GameObject obj )
	{
		Instantiate( obj ).transform.SetParent( trans );	
	}
}
