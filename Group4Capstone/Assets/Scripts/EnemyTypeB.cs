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

    private Vector3 startingLocation;
    private Vector3 endLocation;

    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();

        endLocation = enemyArea.transform.GetChild(Random.Range(0,4)).position;
        startingLocation = new Vector3(8F, endLocation.y);
        transform.position = startingLocation;

        Vector3 dir = (endLocation - startingLocation).normalized;
        dir *= speed;
        rb.AddForce(dir, ForceMode2D.Force); //Moving the enemy

    }

    // Update is called once per frame
    void Update()
    {
        //Rotating the enemy towards the player
        transform.up = playerLocation.position - transform.position;

        if (transform.position.x - xTolerance > playerLocation.position.x)
        {
            //Shoot();
        }
    }


}
