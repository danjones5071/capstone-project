//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	PlayerController.cs
//
//	Controls user inputs for the player character to handle player movement and weapons.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour
{	
	// Public variables which can be modified in the editor at runtime.
	public float speed = 5.0f;			// How quickly the player an move.
	public float yMax;					// The highest point the player can move vertically.
	public float yMin;					// The lowest point the player can move vertically.
	public float laserCooldown = 0.5f;	// How long the user must wait between laser bursts.
	public GameObject laserPrefab;		// The prefab used for a basic laser attack.

	// Private variables to cache necessary components.
	private Rigidbody2D playerRigid;	// The player's rigidbody component.
	private Transform laserOrigin;		// A child of the player game object to specify where the laser should shoot from.
	private SoundEffects soundEffects;	// The sound effects manager.

    public static double batteryCapacity = 100;

	// Private variables to track player-related data and statistics.
	private float laserTimer;			// A timer to track how long it has been since the last laser was fired.
    private int laserEnergyCost = 5;    // Ammount of energy to be deducted out of the batteries per laser shot.

    void Awake()
	{
		playerRigid = GetComponent<Rigidbody2D>();		// Cache a reference to the player's rigidbody component.
		laserOrigin = transform.Find( "LaserOrigin" );	// Cache a reference to the transform of the laser's origin point.

		// Cache a reference to the sound effects manager script attached to the sfx manager game object.
		soundEffects = GameObject.Find( "Sound Effects Manager" ).GetComponent<SoundEffects>();
	}

	// FixedUpdate is called once for every frame that is rendered.
	void FixedUpdate()
	{
		// The vertical input axis handles inputs from the up/down arrow keys, 'W'/'S' keys, or joystick.
		float direction = Input.GetAxis( "Vertical" );
		
		// Move the player based on the user's input to the vertical axis and defined movement speed.
		playerRigid.velocity = Vector2.up * speed * direction;
		
		// Make sure we do not let the player move above or below the camera's view.
		playerRigid.position = new Vector2( -8, Mathf.Clamp(playerRigid.position.y, yMin, yMax) );

		// If there is still some time to cool down after our last laser shot...
		if( laserTimer > 0 )
		{
			// Decrease the laser timer by the amount of time passed.
			laserTimer -= Time.deltaTime;
		}

		// If the player hits the "space" key.
		if( Input.GetKeyDown(KeyCode.Space) )
		{
			// If we don't need to wait more for our laser cooldown time.
			if( laserTimer <= 0 )
			{
				ShootLaser();				// Call our method to shoot a laser.
				laserTimer = laserCooldown;	// Set the laser timer to our cooldown time.
			}
		}
	}

	public void ShootLaser()
	{
        if (batteryCapacity >= laserEnergyCost)
        {
            // Instantiate a laser blast at the laser origin point on our player.
            Instantiate(laserPrefab, laserOrigin.position, Quaternion.identity);

            batteryCapacity -= laserEnergyCost; //Substracting energy value.

            // Play the laser sound effect.
            soundEffects.PlayLaserSound();
        }
	}

	public void SetLaserOrigin( Transform origin )
	{
		laserOrigin = origin;
	}
}