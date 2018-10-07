using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
	// Declare variables to cache necessary components.
	private Rigidbody2D coinRigid;		// Coin's rigidbody component.
	private Transform coinTransform;	// Coin's transform component.
	private float speedX;				// Speed at which the object moves.
	private float createPosX;

	// Use this for initialization
	void Awake()
	{
		coinRigid = GetComponent<Rigidbody2D>();	// Cache a reference to the object's rigidbody component.
		coinTransform = transform;
		createPosX = 15.0f;							// Initialize the horizontal position for object generation.
	}
	void Start ()
	{
		
		// Randomize physical attributes of our new asteroid.
		float posY = Random.Range( -4.9f, 4.9f  );		// Randomize the vertical position of the object.
		speedX = Random.Range( 1.0f, 2.5f );			// Randomize the horizontal speed of the object.

		// Apply move the object to the desired position.
		coinTransform.position = new Vector2( createPosX, posY );

		// Apply physical values to the object's rigidbody.
		coinRigid.velocity = Vector2.left * speedX; // Multiply the left vector by our speed to obtain velocity.
	}

}
