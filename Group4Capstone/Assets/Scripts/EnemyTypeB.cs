using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyTypeB : Enemy
{
    public float xTolerance = 1F;
    private Vector3 startingLocation;
    private Vector3 endLocation;
    
	void Awake()
	{
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
		References.global.enemyGenerator.EnemyTypeBDestroyed();
	}
}
