//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Laser.cs
//
//	Controls the behavior of a laser blast that is instantiated by teh PlayerController script.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public float speed = 6;              // Speed of a laser blast.

    void Start()
    {
        // Play the laser sound effect.
        References.global.soundEffects.PlayEnemyLaserSound();
    }

    void Update()
	{
        transform.Translate(speed * Time.deltaTime,0,0);
    }
}
