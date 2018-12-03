using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject enemyLaser;
	public Transform laserOrigin;

    public GameObject shipExplosion;

    // Protected values assigned in the Awake() method of subclasses.
    protected float speed;
	protected float shootingDistance;


	protected Transform playerLocation;
	protected bool weaponDisable = false;

	//Time variables to keep track of shooting cycle
	protected float startTimeShooting;
	protected float secondsElapsedLastShooting;
	protected float shootingCooldown;

	private ProjectilePool projPool;

	void Start()
	{
		projPool = References.global.projectilePool;
		playerLocation = References.global.playerTrans;
	}

	protected virtual void OnEnable()
	{

	}

	protected void Update()
    {
        //If player is killed and a new instance is made bacause the player had more lifes.
		if( playerLocation != null )
		{
			playerLocation = References.global.playerTrans;

			//Rotating the enemy towards the player
			transform.up = playerLocation.position - transform.position;
		

			secondsElapsedLastShooting = Time.time - startTimeShooting;

			if( secondsElapsedLastShooting > shootingCooldown )
			{    
				ShootLaser();

				startTimeShooting = Time.time;
				weaponDisable = false;
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

            //Effect of explosion
            // Instantiate our explosion particle effects and destroy them after some time.
            Destroy(Instantiate(shipExplosion, transform.position, Quaternion.identity), 3.5F);

            col.gameObject.SetActive( false );		// Deactivate the weapon.
			gameObject.SetActive( false );			// Deactivate this enemy.
		}
	}

	protected void ShootLaser()
	{
		if (!weaponDisable && ( shootingDistance < Vector3.Distance(playerLocation.position, transform.position)))
		{
			GameObject laserRef = projPool.enemyLaserPool.SpawnFromPool( laserOrigin.position, laserOrigin.up );
			laserRef.GetComponent<Rigidbody2D>().velocity = transform.up * 8;

			References.global.soundEffects.PlayEnemyLaserSound();

			weaponDisable = true;
		}
	}

}
