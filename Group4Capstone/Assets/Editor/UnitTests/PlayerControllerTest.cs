using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;

public class PlayerControllerTest
{
	
	GameObject player = GameObject.FindGameObjectWithTag( "Player" );

	[Test]
	public void PlayerExists()
	{
		Assert.NotNull( player );
	}

	[Test]
	public void PlayerHasRigidbody2D()
	{
		Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

		Assert.NotNull( rigidbody );
		Assert.AreEqual( rigidbody.isKinematic, true );
		Assert.AreEqual( rigidbody.simulated, true );
		Assert.AreEqual( rigidbody.useFullKinematicContacts, true );
	}

	[Test]
	public void PlayerHasCollider2D()
	{
		Collider2D collider = player.GetComponent<Collider2D>();

		Assert.NotNull( collider );
	}

	[Test]
	public void LaserOriginExists()
	{
		Transform laserOrigin = player.transform.Find( "LaserOrigin" );

		Assert.NotNull( laserOrigin );
	}

	[UnityTest]
	public IEnumerator PlayerSpeedPositive()
	{
		PlayerController pc = player.GetComponent<PlayerController>();
		Assert.Greater( pc.speed, 0 );

		yield return null;
	}

}
