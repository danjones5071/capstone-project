//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Asteroid.cs
//
//	Controls all information related to an asteroid's transform and physics upon generation.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public GameObject explosion;

	// Declare variables to cache necessary components.
	private Rigidbody2D asteroidRigid;		// Asteroid's rigidbody component.
	private Transform asteroidTransform;	// Object's transform component.

	// Declare variables to store physics and position related values for our obstacle.
	private float createPosX;			// Horizontal position of the object.
	private float posY;				// Vertical position of the object.
	private float speedX;				// Speed at which the object moves.
    private float speedY;              // Speed at which the object moves.
    private float spin;				// Angular velocity of the object.

	void Awake()
	{
		asteroidRigid = GetComponent<Rigidbody2D>();	// Cache a reference to the object's rigidbody component.
		asteroidTransform = transform;					// Cache a reference to the object's transform component.
		createPosX = 15.0f;							// Initialize the horizontal position for object generation.
	}

	// Change to OnEnable() once object pooling is implemented.
	void Start()
	{
		// Randomize physical attributes of our new asteroid.
		posY = Random.Range( -4.9f, 4.9f  );			// Randomize the vertical position of the object.
		speedX = Random.Range( 3.0f, 10.5f );			// Randomize the speed of the object.
        speedY = Random.Range( -0.9f, 0.9f );         // Randomize the speed of the object.
        spin = Random.Range( -50.0f, 50.0f );			// Randomize the angular velocity of the object.

		// Apply move the object to the desired position.
		asteroidTransform.position = new Vector2( createPosX, posY );

        // Apply physical values to the object's rigidbody.
		asteroidRigid.velocity = new Vector2((Vector2.left * speedX).x, (Vector2.up * speedY).y);// Multiply the left vector by our speed to obtain velocity.
        asteroidRigid.angularVelocity = spin;			// Apply angular velocity to create a spin.
	}

    // Controls what happens when an asteroid collides with another object.
    void OnCollisionEnter2D(Collision2D col)
    {
        // If the asteroid collides with a laser...
        if (col.gameObject.tag == "Laser")
        {
            DestroySelf();            // Destroy the asteroid.
            Destroy(col.gameObject);  // And also destroy the laser blast.
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
        Destroy(Instantiate(explosion, asteroidTransform.position, Quaternion.identity), 4);

        // Destroy the asteroid.
        Destroy(gameObject);

        // Play the explosion sound effect.
        References.global.soundEffects.PlayExplosionSound();
    }
}
