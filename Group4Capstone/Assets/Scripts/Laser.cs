//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Laser.cs
//
//	Controls the behavior of a laser blast that is instantiated by teh PlayerController script.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Laser : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public float speed = 6;				// Speed of a laser blast.
	
	// Private variables to cache necessary components.
	private Rigidbody2D laserRigid;		// Blast's rigidbody component.	

	void Awake()
	{
		laserRigid = GetComponent<Rigidbody2D>();			// Cache a reference to the laser's rigidbody component.
		laserRigid.velocity = Vector2.right * speed;		// Set the velocity of the laser blast.
	}
}
