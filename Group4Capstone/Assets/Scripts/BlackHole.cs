//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	BlackHole.cs
//
//	Controls all information related to an Black Hole's transform and physics upon generation.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BlackHole : ScrollingObject
{
	protected override void OnEnable()
	{
		// Perform the tasks of the Start() method in the base class.
		base.OnEnable();

        // Apply rotation to the black hole.
		rigid.angularVelocity = 50.0f;
	}

    void OnCollisionEnter2D( Collision2D col )
    {
        if( col.gameObject != null )
        {
			GameObject obj = col.gameObject;

			// If the colliding object is the player.
			if( obj.tag == "Player" )
			{
				// Call the die method.
				References.global.playerController.Die();
				References.global.soundEffects.PlayBlackHolePullSound();
			}
			// If the colliding object is part of an object pool.
            else if( obj.GetComponents<IPooledObject>().Length > 0 )
            {
                // Add the object back to the pool.
				obj.SetActive( false );
				References.global.soundEffects.PlayBlackHolePullSound();
            }
            else
            {
				// Simply destroy the object.
                Destroy( obj );
                References.global.soundEffects.PlayBlackHolePullSound();
            }
        }
    }
}