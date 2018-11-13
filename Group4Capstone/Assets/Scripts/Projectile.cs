﻿//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Projectile.cs
//
//	Controls the movement of any projectile-style weapon generated by a player or AI enemy.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Projectile : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public float speed = 8;             // Speed of the projectile.

	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = transform.right * speed;
	}
}
