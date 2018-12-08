//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	EnemyTypeB.cs
//
//	Defines the base behavior for Type B enemies which are capable of aiming and firing at the player,
//  but are unable to move toward the player, drifting straight across the screen instead.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyTypeB : Enemy
{
    
	void Awake()
	{
		// Set Type B-specific attributes.
		speed = 100;
		shootingDistance = 5;
		shootingCooldown = 3;
	}

    protected override void OnCollisionEnter2D( Collision2D col )
    {
		// Apply base behavior inherited from Enemy.
		base.OnCollisionEnter2D( col );
		
		// Also allow Type B enemies to collide with each other.
		if( col.gameObject.tag == "Enemy" )
		{
			// Play the explosion sound effect.
			References.global.soundEffects.PlayExplosionSound();

			// Deactivate this enemy. No need to worry about the colliding enemy since
			// they have the same collision behavior.
			gameObject.SetActive( false );
		}
    }

	void OnDisable()
	{
		// Indicate that this enemy has been destroyed.
		References.global.enemyGenerator.EnemyTypeBDestroyed();
	}
}