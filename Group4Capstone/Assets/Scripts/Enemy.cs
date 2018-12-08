//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Enemy.cs
//
//	Defines the base behavior for AI enemies in the game. Such enemies include the prefabs:
//  - Enemy Type A
//  - Enemy Type B
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Enemy : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public Transform laserOrigin;        // A child of the game object to specify where the laser should shoot from.
	public GameObject shipExplosion;     // The prefab used for an explosion effect upon death.

    // Private and protected values assigned in the Awake() method of subclasses.
	private Transform enemyTrans;         // The enemy's transform.
    protected float speed;                // The enemys speed.
	protected Transform playerTrans;      // Reference to player's transform.

	//Time variables to keep track of shooting cycle
	protected float startTimeShooting;           // When the enemy fired a shot.
	protected float secondsElapsedLastShooting;  // Time since the enemy fired a shot.
	protected float shootingCooldown;            // How long the enemy has to wait before firing again.
	protected float shootingDistance;            // Maximum distance at which an enemy can fire at the player.

	// Provide access to pooled enemy laser objects.
	private ProjectilePool projPool;

	void Start()
	{
		// Cache necessary components.
		enemyTrans = transform;
		projPool = References.global.projectilePool;
		playerTrans = References.global.playerTrans;
	}

	protected void Update()
    {
        // If player is still alive.
		if( playerTrans != null )
		{
			//Rotating the enemy towards the player
			enemyTrans.up = playerTrans.position - enemyTrans.position;

			// Calculate how much time it has been since the last shot was fired.
			secondsElapsedLastShooting = Time.time - startTimeShooting;

			// If more time has passed than the cooldown time.
			if( secondsElapsedLastShooting > shootingCooldown )
			{    
				ShootLaser();
				startTimeShooting = Time.time;
			}
		}
    }
	
	protected virtual void OnCollisionEnter2D( Collision2D col )
	{
		string tag = col.gameObject.tag;

		// If the Enemy collides with an attack
		if( tag == "Laser" || tag == "Inferno" || tag == "EnemyLaser" )
		{
			// Play the explosion sound effect.
			References.global.soundEffects.PlayExplosionSound();

            // Instantiate our explosion particle effects and destroy them after some time.
            Destroy( Instantiate(shipExplosion, enemyTrans.position, Quaternion.identity), 3.5F );

            col.gameObject.SetActive( false );	// Deactivate the weapon.
			gameObject.SetActive( false );	    // Deactivate this enemy.
		}
	}

	protected void ShootLaser()
	{
		// If the enemy is within range of hte player.
		if( shootingDistance < Vector3.Distance(playerTrans.position, enemyTrans.position) )
		{
			// Fire a laser.
			GameObject laserRef = projPool.enemyLaserPool.SpawnFromPool( laserOrigin.position, laserOrigin.up );
			laserRef.GetComponent<Rigidbody2D>().velocity = enemyTrans.up * 8;

			// Play the laser sound effect.
			References.global.soundEffects.PlayEnemyLaserSound();
		}
	}

}