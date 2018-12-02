//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	PlayerController.cs
//
//	Controls user inputs for the player character to handle player movement and weapons.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour
{
    // Public variables which can be modified in the editor at runtime.
    public float speed = 5.0f;          // How quickly the player can move.
    public float yMax;                  // The highest point the player can move vertically.
    public float yMin;                  // The lowest point the player can move vertically.
    public float xMax;                  // The lowest point the player can move horizontally.
    public float xMin;                  // The lowest point the player can move horizontally.

    public float laserCooldown = 0.5f;  // How long the user must wait between laser bursts.
    public GameObject laserPrefab;      // The prefab used for a basic laser attack.
    public GameObject infernoPrefab;    // The prefab used for a basic inferno attack.
    public GameObject explosion;

    // Private variables to cache necessary components.
    private Transform laserOrigin;      // A child of the player game object to specify where the laser should shoot from.
    private Transform laserOriginL;     // A child of the player game object to specify where the laser should shoot from.
    private Transform laserOriginR;		// A child of the player game object to specify where the laser should shoot from.

    //public float batteryCapacity = 100;
	public float energy = 100;
	public float maxEnergy = 120;
    public int health = 100;			// The current amount of health the player has.
    public float rechargeInterval = 1;
    public float rechargeAmount = 2;
	public int currentWeapon = 0;

    // Private variables to track player-related data and statistics.
    private float laserTimer;			// A timer to track how long it has been since the last laser was fired.
    private int laserEnergyCost = 5;    // Ammount of energy to be deducted out of the batteries per laser shot.

    //Player Directions towards the mouse.
    private Vector3 playerDirection;

	// List of all weapons currently possessed by the player.
	public List<string> weapons;

    public Transform damageSparks;

	private ProjectilePool projPool;

    void Awake()
    {
		weapons = new List<string>(){ References.WNAME_LASER };



        laserOrigin = transform.Find("LaserOrigin");    // Cache a reference to the transform of the laser's origin point.
        laserOriginL = transform.Find("LaserOriginL");  // Cache a reference to the transform of the laser's origin point.
        laserOriginR = transform.Find("LaserOriginR");  // Cache a reference to the transform of the laser's origin point.
    }

    void Start()
    {
		damageSparks.gameObject.SetActive( false );
        StartCoroutine(Recharge());
		projPool = References.global.projectilePool;
    }

    // FixedUpdate is called once for every frame that is rendered.
    void FixedUpdate()
    {
        // The vertical and horizontal input axises handle inputs from the up/down/left/right arrow keys, 'W'/'S'/'A'/'D' keys, or joystick.
        float directionY = Input.GetAxis("Vertical");
        float directionX = Input.GetAxis("Horizontal");

        // Move the player based on the user's input to the vertical/horizontal axis and defined movement speed.
        References.global.playerRigid.velocity = Vector2.up * speed * directionY + Vector2.right * speed * directionX;

        // Make sure we do not let the player move away of the camera's view.
        References.global.playerRigid.position = new Vector2(Mathf.Clamp(References.global.playerRigid.position.x, xMin, xMax), Mathf.Clamp(References.global.playerRigid.position.y, yMin, yMax));

        playerDirection = FaceMouse();

        // If there is still some time to cool down after our last laser shot...
        if (laserTimer > 0)
        {
            // Decrease the laser timer by the amount of time passed.
            laserTimer -= Time.deltaTime;
        }

        // If the player hits the "space" key.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // If we don't need to wait more for our laser cooldown time.
            if (laserTimer <= 0)
            {
                FireWeapon();               // Call our method to shoot a laser.
                laserTimer = laserCooldown; // Set the laser timer to our cooldown time.
            }
        }
        // If the player hits the "E" key or scrolls up on the mouse wheel.
		if( Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0.0f )
		{
			CycleWeapon( 1 );
		}
		// If the player hits the "Q" key or scrolls down on the mouse wheel.
		else if( Input.GetKeyDown (KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0.0f )
		{
			CycleWeapon( -1 );
		}

		if (Input.GetKeyDown (KeyCode.Escape))
		{
			References.global.gameMaster.PauseGame();
		}

        // Deal damage to self for testing purposes.
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(50);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ReduceLives(1);
        Instantiate( explosion, transform.position, Quaternion.identity );
        References.global.uiManager.ShowPlayAgainUI();
        References.global.soundEffects.PlayExplosionSound();
		References.global.enemyGenerator.generate = false;
        gameObject.SetActive(false);
    }

    public void CycleWeapon( int i )
	{
		currentWeapon += i;
		if( currentWeapon > weapons.Count - 1 )
			currentWeapon = 0;
		else if( currentWeapon < 0 )
			currentWeapon = weapons.Count - 1;

		References.global.uiManager.SetWeaponText( weapons[currentWeapon].ToString() );

	}

	public void FireWeapon()
	{
		switch( weapons[currentWeapon].ToString() )
		{
			case References.WNAME_LASER:
				ShootLaser();
				break;
			case References.WNAME_INFERNO:
				ShootInferno();
				break;
			case References.WNAME_2LASER:
				ShootDoubleLaser();
				break;
		}
	}

    public void ShootLaser()
    {
        if (energy >= laserEnergyCost)
        {
            // Instantiate a laser blast at the laser origin point on our player.
			GameObject laserRef = projPool.laserPool.SpawnFromPool( laserOrigin.position, laserOrigin.up );
			laserRef.GetComponent<Rigidbody2D>().velocity = transform.up * 6;

			References.global.soundEffects.PlayLaserSound();

            energy -= laserEnergyCost;
        }
    }

    public void ShootDoubleLaser()
    {
        if (energy >= 2*laserEnergyCost)
        {
            // Instantiate a laser blast at the laser origin point on our player.

            GameObject laserRef_L = Instantiate(laserPrefab, laserOriginL.position, Quaternion.identity);
            GameObject laserRef_R = Instantiate(laserPrefab, laserOriginR.position, Quaternion.identity);

            //Rotating the laser towards the player 
            laserRef_L.transform.up = playerDirection;
            laserRef_L.transform.rotation *= Quaternion.Euler( 0, 0, 90 );


            //Rotating the laser towards the player 
            laserRef_R.transform.up = playerDirection;
            laserRef_R.transform.rotation *= Quaternion.Euler( 0, 0, 90 );

			References.global.soundEffects.PlayLaserSound();


            energy -= laserEnergyCost * 2; //Substracting energy value.

        }
    }

    public void ShootInferno()
    {
        if (energy >= laserEnergyCost)
        {
            // Instantiate a inferno blast at the laser origin point on our player.

            GameObject laserRef = Instantiate(infernoPrefab, laserOrigin.position, Quaternion.identity);

            //Rotating the laser towards the player 
            laserRef.transform.up = playerDirection;
            laserRef.transform.rotation *= Quaternion.Euler( 0, 0, 90 );

			References.global.soundEffects.PlayInfernoSound();

            energy -= laserEnergyCost; //Substracting energy value.

        }
    }

    IEnumerator Recharge()
    {
        while( true )
        {
			if( energy < maxEnergy )
            {
                AddEnergy( rechargeAmount );
            }
            yield return new WaitForSeconds( rechargeInterval );
        }
    }

    public void SetLaserOrigin( Transform origin )
    {
        laserOrigin = origin;
    }

    public void TakeDamage( int damage )
    {
        health -= damage;
    }

    public void Heal( int healing )
    {
        health = System.Math.Min( health + healing, 100 );
    }

    public void AddEnergy( float add )
    {
        // We don't want the energy to exceed 100.
        // So take the minimum between 100 and the sum of the current energy plus energy being added.
		energy = System.Math.Min( energy + add, maxEnergy );
    }

    private void ReduceLives( int lifeLost )
    {
        References.global.gameMaster.UpdateLivesCount( -lifeLost );
    }

    // Controls what happens when an asteroid collides with another object.
    void OnCollisionEnter2D( Collision2D col )
    {
        // If the asteroid collides with a laser or enemy...
        if( col.gameObject.tag == "EnemyLaser" || col.gameObject.tag == "Enemy" )
        {
            // Play the crash sound effect.
            References.global.soundEffects.PlayCrashSound();

            // Player takes damage from impact.
            TakeDamage( 15 );

			col.gameObject.SetActive( false ); // Enemy laser needs pooling!!!

			damageSparks.gameObject.SetActive( true );

            ContactPoint2D contact = col.contacts[0];  
            Vector3 pos = contact.point;
            damageSparks.position = pos;

            StartCoroutine( StopDamageSparks() );
        }

        if( col.gameObject.tag == "BlackHole" )
        {
            Die();
            References.global.soundEffects.PlayBlackHolePullSound();
        }
    }

    IEnumerator StopDamageSparks()
    {
        yield return new WaitForSeconds( 0.3f );
		damageSparks.gameObject.SetActive( false );
    }

	void OnTriggerEnter2D( Collider2D col )
	{
		switch( col.tag )
		{
			case "Currency":
				References.global.soundEffects.PlayCurrencyPickUpSound();
				References.global.gameMaster.AddToCurrency( 10 );
				col.gameObject.SetActive( false );
				break;
			case "EnergyPickup":
				References.global.soundEffects.PlayEnergyPickUpSound();
				AddEnergy( 15 );
				col.gameObject.SetActive( false );
				break;
			case "HealthPickup":
				References.global.soundEffects.PlayHealthPickUpSound();
				Heal( 15 );
				col.gameObject.SetActive( false );
				break;
		}
	}

    Vector3 FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y,0);

       transform.up = direction;

        return direction;
    }
}
