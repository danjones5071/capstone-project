//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	BlackHole.cs
//
//	Controls all information related to an Black Hole's transform and physics upon generation.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BlackHole : MonoBehaviour {

    // Declare variables to cache necessary components.
    	private Rigidbody2D blackHoleRigid;		// Asteroid's rigidbody component.
    	private Transform blackHoleTransform;	// Object's transform component.

    	// Declare variables to store physics and position related values for our obstacle.
    	private float createPosX;			// Horizontal position of the object.
    	private float posY;				// Vertical position of the object.
        private float revSpeed = 50.0f;
        private float speedX = 2f;			// Randomize the horizontal speed of the object.


    void Awake()
        {
            blackHoleRigid = GetComponent<Rigidbody2D>();	// Cache a reference to the object's rigidbody component.
            blackHoleTransform = transform;					// Cache a reference to the object's transform component.
            createPosX = 15.0f;							// Initialize the horizontal position for object generation.
        }
	// Use this for initialization
	void Start () {
		// Randomize physical attributes of our new asteroid.
   	    posY = Random.Range( -4.9f, 4.9f  );			// Randomize the vertical position of the object.

        // Apply move the object to the desired position.
        blackHoleTransform.position = new Vector2( createPosX, posY );

        // Multiply the left vector by our speed to obtain velocity.
        blackHoleRigid.velocity = new Vector2((Vector2.left * speedX).x, Vector2.zero.y);

	}

	void FixedUpdate()
    {
        blackHoleRigid.MoveRotation(blackHoleRigid.rotation + revSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject != null)
        {
            Destroy(col.gameObject);
            References.global.soundEffects.PlayBlackHolePullSound();
        }
    }

}
