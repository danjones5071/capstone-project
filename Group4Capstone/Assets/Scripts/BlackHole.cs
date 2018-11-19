//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	BlackHole.cs
//
//	Controls all information related to an Black Hole's transform and physics upon generation.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BlackHole : ScrollingObject
{
	protected override void OnEnable ()
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
            if (col.gameObject.GetComponents<IPooledObject>().Length > 0)
            {
                // Add the Asteroid back tot he Asteroid pool
				col.gameObject.SetActive( false );
            }
            else
            {
                Destroy( col.gameObject );
                References.global.soundEffects.PlayBlackHolePullSound();
            }
        }
    }

}
