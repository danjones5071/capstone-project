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
		// Destroy the game object.
		Destroy( col.gameObject );
	}
}
