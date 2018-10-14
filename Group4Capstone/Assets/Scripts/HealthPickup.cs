using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    // Public variables which can be modified in the editor at runtime.
    public float speed = 3;              // Speed of a pickup.

    // Private variables to cache necessary components.
    private Rigidbody2D pickupRigid;     // Pickup's rigidbody component.   

    void Awake()
    {
        pickupRigid = GetComponent<Rigidbody2D>();          // Cache a reference to the pickup's rigidbody component.
        pickupRigid.velocity = Vector2.left * speed;        // Set the velocity of the pickup.
    }

    // Controls what happens when a health pickup collides with another object.
    void OnTriggerEnter2D(Collider2D col)
    {

        // If the pickup collides with a player...
        if (col.gameObject.tag == "Player")
        {
            // Cache player controller component.
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();

            // Play the crash sound effect.
            References.global.soundEffects.PlayCrashSound();

            // Pickup heals player.
            pc.Heal(15);

            // Destroy asteroid on impact with player ship.
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        // Instantiate our explosion particle effects and destroy them after some time.
        // Destroy(Instantiate(explosion, asteroidTransform.position, Quaternion.identity), 4);

        // Destroy the pickup.
        Destroy(gameObject);

        // Play the health pickup sound effect.
        References.global.soundEffects.PlayInfernoSound();
    }

}
