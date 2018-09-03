//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Asteroid.cs
//
//	Controls all information related to an asteroid's transform and physics upon generation.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Asteroid : MonoBehaviour
{
	// Declare variables to cache necessary components.
	private Rigidbody2D objRigid;		// Object's rigidbody component.
	private Transform objTransform;		// Object's transform component.

	// Declare variables to store physics and position related values for our obstacle.
	private float _createPosX;			// Horizontal position of the object.
	private float _posY;				// Vertical position of the object.
	private float _speedX;				// Speed at which the object moves.
    private float _speedY;              // Speed at which the object moves.
    private float _spin;				// Angular velocity of the object.

	void Awake()
	{
		objRigid = GetComponent<Rigidbody2D>();		// Cache a reference to the object's rigidbody component.
		objTransform = transform;						// Cache a reference to the object's transform component.
		_createPosX = 15.0f;							// Initialize the horizontal position for object generation.
	}

	// Change to OnEnable() once object pooling is implemented.
	void Start()
	{
		// Randomize physical attributes of our new asteroid.
		_posY = Random.Range( -4.9f, 4.9f  );			// Randomize the vertical position of the object.
		_speedX = Random.Range( 3.0f, 5.5f );			// Randomize the speed of the object.
        _speedY = Random.Range( -0.6f, 0.6f );         // Randomize the speed of the object.
        _spin = Random.Range( -50.0f, 50.0f );			// Randomize the angular velocity of the object.

		// Apply move the object to the desired position.
		objTransform.position = new Vector2( _createPosX, _posY );

        // Apply physical values to the object's rigidbody.
		objRigid.velocity = new Vector2((Vector2.left * _speedX).x, (Vector2.up * _speedY).y);// Multiply the left vector by our speed to obtain velocity.
        objRigid.angularVelocity = _spin;			// Apply angular velocity to create a spin.
	}

	void OnCollisionEnter2D( Collision2D col )
	{
		// If the asteroid collides with a laser...
		if( col.gameObject.tag == "Laser" )
		{
			Destroy( gameObject );      // Destroy the asteroid.
			Destroy( col.gameObject );  // And also destroy the laser blast.
		}
	}
}
