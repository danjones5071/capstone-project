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

	// Declare variables to store asteroid-specific physics and position related values.
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
		rigid.velocity = (Vector2.left * speedX) + (Vector2.up * speedY); // Multiply the left vector by our speed to obtain velocity.
        rigid.angularVelocity = spin; // Apply angular velocity to create a spin.
	}

    // Controls what happens when an asteroid collides with another object.
    void OnCollisionEnter2D( Collision2D col )
    {
        // If the asteroid collides with a laser...
        if (col.gameObject.tag == "Laser" || col.gameObject.tag == "Inferno" || col.gameObject.tag == "EnemyLaser")
        {
            DestroySelf();            // Destroy the asteroid.
            Destroy(col.gameObject);  // And also destroy the laser blast.
        }

        // If the asteroid collides with an enemy...
        if (col.gameObject.tag == "Enemy")
        {
            DestroySelf();            // Destroy the asteroid.
            Destroy(col.gameObject);  // And also destroy the enemy.
        }

        // If the asteroid collides with a player...
        if (col.gameObject.tag == "Player")
        {
            // Cache player controller component.
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();

            // Play the crash sound effect.
			References.global.soundEffects.PlayCrashSound();

            // Player takes damage from impact.
            pc.TakeDamage(15);

            // Destroy asteroid on impact with player ship.
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        // Instantiate our explosion particle effects and destroy them after some time.
        Destroy( Instantiate(explosion, trans.position, Quaternion.identity), 4 );

        // Add the Asteroid back tot he Asteroid pool
        //ObjectPooler.Instance.ReturnToPool("Asteroid", gameObject);

        // Play the explosion sound effect.
        References.global.soundEffects.PlayExplosionSound();

		gameObject.SetActive( false );
    }
}
