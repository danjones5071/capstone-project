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
        if (obj.GetComponents<IPooledObject>().Length > 0)
        {
            // This is very hacky need to find better way to check for IPooledObject at runtime
            // also the "obj.tag" will only work for objects where their tag matches the prefab name.
            // Need to find a way to access the prefab name programatically
            ObjectPooler.Instance.SpawnFromPool(obj.tag, trans.position, trans.rotation);
        }
        else
        {
		    Instantiate( obj ).transform.SetParent( trans );
        }
	}
}
