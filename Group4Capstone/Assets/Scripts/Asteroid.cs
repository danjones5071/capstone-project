//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Asteroid.cs
//
//	Controls all information related to an asteroid's transform and physics upon generation.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Asteroid : ScrollingObject
{
	public GameObject explosion;

	// Private variables to store asteroid-specific physics and position related values.
    private float speedY;  // Speed at which the object moves vertically.
    private float spin;    // Angular velocity of the object.

	protected override void OnEnable()
	{
		// Perform the tasks of the OnEnable() method in the base class.
		base.OnEnable();

		// Randomize the vertical speed of the asteroid.
		if( posY > 0 )
			speedY = Random.Range( -0.7f, 0 );
		else
			speedY = Random.Range( 0, 0.7f );

		// Randomize the angular velocity of the object.
        spin = Random.Range( -50.0f, 50.0f );

        // Apply physical values to the asteroid's rigidbody.

		// Obtain our velocity from the randomly generated speeds.
		rigid.velocity = ( Vector2.left * speedX ) + ( Vector2.up * speedY );

		// Apply angular velocity to create a spin.
        rigid.angularVelocity = spin;
	}

    // Controls what happens when an asteroid collides with another object.
    void OnCollisionEnter2D( Collision2D col )
    {
		GameObject obj = col.gameObject;
		string tag = obj.tag;

        // If the asteroid collides with a weapon or enemy...
		if( tag == "Laser" || tag == "Inferno" || tag == "EnemyLaser" || tag == "Enemy" )
        {
			// Destroy the asteroid and colliding object.
            DestroySelf();
			obj.SetActive( false );
        }
        // If the asteroid collides with a player...
        else if( tag == "Player" )
        {
            // Play the crash sound effect.
			References.global.soundEffects.PlayCrashSound();

            // Player takes damage from impact.
			References.global.playerController.TakeDamage( 15 );

            // Destroy asteroid on impact with player ship.
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        // Instantiate our explosion particle effects and destroy them after some time.
        Destroy( Instantiate(explosion, trans.position, Quaternion.identity), 4 );

        // Play the explosion sound effect.
        References.global.soundEffects.PlayExplosionSound();

		// Disable the asteroid, adding it back to the object pool.
		gameObject.SetActive( false );
    }
}