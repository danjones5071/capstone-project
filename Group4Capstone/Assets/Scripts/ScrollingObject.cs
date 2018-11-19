//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	ScrollingObject.cs
//
//	Defines the base behavior for game objects whose primary purpose is to "fly" across the screen from left to
//  right. Such game objects include the prefabs:
//  - Coin
//  - EnergyPickup
//  - HealthPickup
//  - Asteroid (inherits)
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
	// Declare variables to cache necessary components.
	protected Rigidbody2D rigid;  // Object's rigidbody component.
	protected Transform trans;    // Object's transform component.
	protected float speedX;       // Speed at which the object scrolls to the left.
	protected float posX;   // The x position at which the object will generate.
	protected float posY;         // The y position at which the object will generate.

	protected virtual void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();   // Cache a reference to the object's rigidbody component.
		trans = transform;                     // Cache a reference to the object's transform component.
		posX = 15.0f;                          // Initialize the horizontal position for object generation.
	}

	// Change to OnEnable() once object pooling is implemented.
	protected virtual void OnEnable()
	{
		// Randomize physical attributes of our object.
		posY = Random.Range( -4.9f, 4.9f  );   // Randomize the vertical position of the object.
		speedX = Random.Range( 1.2f, 2.5f );   // Randomize the horizontal speed of the object.

		// Move the object to the desired start position.
		trans.position = new Vector2( posX, posY );

		// Apply physical values to the object's rigidbody.
		rigid.velocity = Vector2.left * speedX; // Multiply the left vector by our speed to obtain velocity.
	}
}
