using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyTypeB : MonoBehaviour
{
    public float speed = 200;
    public float xTolerance = 1F;

    public Transform playerLocation;
    public GameObject enemyArea;

    public GameObject enemyLaser;
    private float enemyAllowedShootingDistance = 5F;
    private bool weaponDisable = false;

    // Private variables to cache necessary components.
    public GameObject laserOrigin;      // A child of the player game object to specify where the laser should shoot from.

    //Time variables to keep track of shooting cycle
    private float startTimeShooting;
    private float secondsElapsedLastShooting;

    private Vector3 startingLocation;
    private Vector3 endLocation;

    private Vector3 initialPosition;


    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        if (playerLocation == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                playerLocation = GameObject.Find("Player").transform;
            }
            else
            {
                return;
            }
        }

        if (enemyArea == null)
            enemyArea = GameObject.Find("EnemyTypeBareas");

        rb = GetComponent<Rigidbody2D>();


    ExecuteBehavior();
    }

    // Update is called once per frame
    void Update()
    {
        //If player is killed and a new instance is made bacause the player had more lifes.
        if (playerLocation == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                playerLocation = GameObject.Find("Player").transform;
            }
            else
            {
                return;
            }
        }

        //Rotating the enemy towards the player
        transform.up = playerLocation.position - transform.position;

        secondsElapsedLastShooting = Time.time - startTimeShooting;

        if (secondsElapsedLastShooting > 2f && transform.position.x > -9F)
        {
            ShootLaser();

            if (secondsElapsedLastShooting > 2.5f)
            {
                startTimeShooting = Time.time;
                weaponDisable = false;
            }
        }
    }

    void ExecuteBehavior()
    {
        endLocation = enemyArea.transform.GetChild(Random.Range(0, 4)).position;
        startingLocation = new Vector3(14F, endLocation.y); //Spawn outside of camera view 14 units to the right.
        transform.position = startingLocation;

        Vector3 dir = (endLocation - startingLocation).normalized;
        dir *= speed;
        rb.AddForce(dir, ForceMode2D.Force); //Moving the enemy
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If the enemy collides with a laser...
        if (col.gameObject.tag == "Laser" || col.gameObject.tag == "Inferno" || col.gameObject.tag == "EnemyLaser" || col.gameObject.tag == "Enemy")
        {
            // Play the explosion sound effect.
            References.global.soundEffects.PlayExplosionSound();

            // Destroy the enemy.
            // Destroy(gameObject);
            // Temp fix to avoid slow down in higher phases.
            // Object pooling should be inserted to properly handle this
            ExecuteBehavior();

            Destroy(col.gameObject);  // And also destroy the laser blast.
        }

        // If the asteroid collides with a player...
        if (col.gameObject.tag == "Player")
        {
            // Cache player controller component.
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();

            // Play the crash sound effect.
            References.global.soundEffects.PlayCrashSound();

            // Player takes damage from impact.
            pc.TakeDamage(15);
        }
    }

    void ShootLaser()
    {
        if (!weaponDisable && (enemyAllowedShootingDistance < Vector3.Distance(playerLocation.position, transform.position)))
        {
            GameObject laserRef = Instantiate(enemyLaser, laserOrigin.transform.position, Quaternion.identity);

            //Rotating the laser towards the player           
            laserRef.transform.up = (playerLocation.position - transform.position);
            laserRef.transform.rotation *= Quaternion.Euler(0, 0, 90);

            weaponDisable = true;
        }
    }

    private void OnDestroy()
    {
        References.global.enemyGenerator.EnemyTypeBDestroyed();
    }
}
