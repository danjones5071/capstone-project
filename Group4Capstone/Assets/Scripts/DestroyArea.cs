//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	DestroyArea.cs
//
//	Creates a region around the visible screen. When an object with a collider exits the region, it is destroyed.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class DestroyArea : MonoBehaviour
{
	// When a game object with a collider component leaves this region.
	void OnTriggerExit2D( Collider2D col )
	{
		GameObject obj = col.gameObject;

		// Disable pooled objects.
        if( obj.GetComponents<IPooledObject>().Length > 0 )
        {
			obj.SetActive( false );
        }
		// Destroy unpooled objects.
        else
        {
            Destroy( obj );
        }
	}
}
