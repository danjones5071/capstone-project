//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	ScrollingObject.cs
//
//	Defines the base behavior for game objects whose primary purpose is to "fly" across the screen from left to
//  right. Such game objects include the prefabs:
//  - Coin Pickup
//  - Energy Pickup
//  - Health Pickup
//  - Asteroid (inherits)
//  - Black Hole (inherits)
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
	// Declare variables to cache necessary components.
	protected Rigidbody2D rigid;  // Object's rigidbody component.
	protected Transform trans;    // Object's transform component.
	protected float speedX;       // Speed at which the object scrolls to the left.
	protected float posX;         // The x position at which the object will generate.
	protected float posY;         // The y position at which the object will generate.

	protected virtual void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();   // Cache a reference to the object's rigidbody component.
		trans = transform;                     // Cache a reference to the object's transform component.
		posX = 15.0f;                          // Initialize the horizontal position for object generation.
	}

	protected virtual void OnEnable()
	{
		// Randomize physical attributes of our object.
		posY = Random.Range( -4.9f, 4.9f  );   // Randomize the vertical starting position of the object.
		speedX = Random.Range( 1.2f, 2.5f );   // Randomize the horizontal speed of the object.

		// Move the object to the desired start position.
		trans.position = new Vector2( posX, posY );

		// Multiply the left vector by random speed to set velocity.
		rigid.velocity = Vector2.left * speedX; 
	}
}