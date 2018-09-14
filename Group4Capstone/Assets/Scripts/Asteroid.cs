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

	// Private variables to cache necessary components.
	private SoundEffects soundEffects;	// The sound effects manager.

	void Awake()
	{
		asteroidRigid = GetComponent<Rigidbody2D>();	// Cache a reference to the object's rigidbody component.
		asteroidTransform = transform;					// Cache a reference to the object's transform component.
		createPosX = 15.0f;							// Initialize the horizontal position for object generation.

		// Cache a reference to the sound effects manager script attached to the sfx manager game object.
		soundEffects = GameObject.Find( "Sound Effects Manager" ).GetComponent<SoundEffects>();
	}

	// Change to OnEnable() once object pooling is implemented.
	void Start()
	{
		// Randomize physical attributes of our new asteroid.
		posY = Random.Range( -4.9f, 4.9f  );			// Randomize the vertical position of the object.
		speedX = Random.Range( 3.0f, 5.5f );			// Randomize the speed of the object.
        speedY = Random.Range( -0.6f, 0.6f );         // Randomize the speed of the object.
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

        //Prototype idea for creating deflection out of colliding asteroids.
        //Consider adding multiplier and particle effects if collision occurs. 
        if (col.gameObject.tag == "Asteroid")
        {
            // Cache some additional components for efficiency and readability.
            Rigidbody2D colRigid = col.gameObject.GetComponent<Rigidbody2D>();
            Transform colTransform = col.gameObject.transform;

            //gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * (Vector2.Reflect(gameObject.GetComponent<Rigidbody2D>().velocity, col.gameObject.transform.position)).normalized;
            //col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity * (Vector2.Reflect(col.gameObject.GetComponent<Rigidbody2D>().velocity, gameObject.transform.position)).normalized;

            /* Notes:
			 * Very cool addition with allowing asteroids to "reflect" off each other!
			 * One thing I wanted to mention about your approach based on what I've learned over the years. GetComponent<>() calls can start to add up in terms of performance impact. Whenever I start thinking about the efficiency of a
			 * Unity project, this is one of the first calls I look for to see if they can be reduced. At the top of this script, you'll see that I already cached the Rigidbody2D and Transform components for this asteroid game object
			 * in the Awake() function. This allows us to access and modify the properties of the Rigidbody2D and Transform components whenever we want without any more expensive calls to GetComponent<>() or anything like that. Similarly,
			 * at the top of this if statement, I cached the rigidbody and transform components of the asteroid we're colliding with for the same reason. This makes the code a bit more efficient, and also a bit easier to read. You can see
			 * the difference between the two lines I commented out above, and the two lines I shortened them to below:
			 */

            // Reflect this asteroid.
            asteroidRigid.velocity *= (Vector2.Reflect(asteroidRigid.velocity, colTransform.position)).normalized;

            // Reflect the astroid that collided with us.
            colRigid.velocity *= (Vector2.Reflect(colRigid.velocity, asteroidTransform.position)).normalized;
        }

        // If the asteroid collides with a player...
        if (col.gameObject.tag == "Player")
        {
            // Cache player controller component.
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();

            // Play the crash sound effect.
			soundEffects.PlayCrashSound();

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
        soundEffects.PlayExplosionSound();
    }
}
