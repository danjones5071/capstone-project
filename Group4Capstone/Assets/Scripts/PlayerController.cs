//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	PlayerController.cs
//
//	Controls user inputs for the player character to handle player movement and weapons.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class PlayerController : MonoBehaviour
{	
	// Public variables which can be modified in the editor at runtime.
	public float speed = 5.0f;			// How quickly the player an move.
	public float yMax;					// The highest point the player can move vertically.
	public float yMin;					// The lowest point the player can move vertically.
	public GameObject laserPrefab;

	// Private variables to cache necessary components.
	private Rigidbody2D playerRigid;	// The player's rigidbody component.
	private Transform laserOrigin;		// A child of the player game object to specify where the laser should shoot from.

	void Awake()
	{
		playerRigid = GetComponent<Rigidbody2D>();		// Cache a reference to the player's rigidbody component.
		laserOrigin = transform.Find( "LaserOrigin" );	// Cache a reference to the transform of the laser's origin point.
	}

	// FixedUpdate is called once per frame.
	void FixedUpdate()
	{
		// The vertical input axis handles inputs from the up/down arrow keys, 'W'/'S' keys, or joystick.
		float direction = Input.GetAxis( "Vertical" );
		
		// Move the player based on the user's input to the vertical axis and defined movement speed.
		playerRigid.velocity = Vector2.up * speed * direction;
		
		// Make sure we do not let the player move above or below the camera's view.
		playerRigid.position = new Vector2( -8, Mathf.Clamp(playerRigid.position.y, yMin, yMax) );

		// If the player hits the "space" key.
		if( Input.GetKeyDown(KeyCode.Space) )
		{
			// Call our method to shoot a laser.
			shootLaser();
		}
	}

	public void shootLaser()
	{
		// Instantiate a laser blast at the laser origin point on our player.
		Instantiate( laserPrefab, laserOrigin.position, Quaternion.identity );
	}
}