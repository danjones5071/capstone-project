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
		// Disable pooled objects.
        if (col.gameObject.GetComponents<IPooledObject>().Length > 0)
        {
			col.gameObject.SetActive( false );
        }
		// Destroy unpooled objects.
        else
        {
            Destroy( col.gameObject );
        }
	}
}
