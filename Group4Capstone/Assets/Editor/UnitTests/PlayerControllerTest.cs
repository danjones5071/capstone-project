using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;

public class PlayerControllerTest
{
	GameObject player;
	Rigidbody2D playerRigid;
	PlayerController playerController;

	[OneTimeSetUp]
	public void TestSetup()
	{
		player = References.global.player;
		playerRigid = References.global.playerRigid;
		playerController = References.global.playerController;
	}

	[Test]
	public void PlayerExists()
	{
		Assert.NotNull( player );
	}

	[Test]
	public void PlayerHasRigidbody2D()
	{
		Assert.NotNull( playerRigid );
		Assert.AreEqual( playerRigid.isKinematic, true );
		Assert.AreEqual( playerRigid.simulated, true );
		Assert.AreEqual( playerRigid.useFullKinematicContacts, true );
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

	[Test]
	public void LaserPrefabExists()
	{
		GameObject laserPrefab = playerController.laserPrefab;

		Assert.NotNull( laserPrefab );
	}

	[Test]
	public void InfernoPrefabExists()
	{
		GameObject infernoPrefab = playerController.infernoPrefab;

		Assert.NotNull( infernoPrefab );
	}

	[UnityTest]
	public IEnumerator PlayerLivesPositive()
	{
		Assert.Greater( playerController.lives, 0 );
		yield return null;
	}

	[UnityTest]
	public IEnumerator PlayerHealthPositive()
	{
		Assert.Greater( playerController.health, 0 );
		yield return null;
	}

	[UnityTest]
	public IEnumerator PlayerSpeedPositive()
	{
		Assert.Greater( playerController.speed, 0 );

		yield return null;
	}

	[UnityTest]
	public IEnumerator TakeDamageTest()
	{
		Assert.AreEqual( playerController.health, 100 );
		playerController.TakeDamage( 5 );
		Assert.AreEqual( playerController.health, 95 );
		playerController.TakeDamage( -5 );
		Assert.AreEqual( playerController.health, 100 );

		yield return null;
	}

	[UnityTest]
	public IEnumerator AddEnergyTest()
	{
		Assert.AreEqual( playerController.batteryCapacity, 100 );
		playerController.AddEnergy( 100 );

		// Should not be able to exceed max energy.
		Assert.AreEqual( playerController.batteryCapacity, 100 );

		playerController.AddEnergy( -50 );
		Assert.AreEqual( playerController.batteryCapacity, 50 );

		// Should not be able to exceed max energy.
		playerController.AddEnergy( 500 );
		Assert.AreEqual( playerController.batteryCapacity, 100 );

		yield return null;
	}

	[UnityTest]
	public IEnumerator ShootLaserTest()
	{
		playerController.ShootLaser();
		GameObject laser = GameObject.Find( "Laser(Clone)" );

		Assert.NotNull( laser );

		yield return null;
	}

	[UnityTest]
    public IEnumerator ShootInfernoTest()
    {
    	playerController.ShootInferno();
    	GameObject inferno = GameObject.Find( "Inferno(Clone)" );
  
		Assert.NotNull( inferno );

    	yield return null;
    }

	[Test]
	public void LaserDualOriginExists()
	{
		Transform laserOriginL = player.transform.Find( "LaserOriginL" );
		Transform laserOriginR = player.transform.Find( "LaserOriginR" );

		Assert.NotNull( laserOriginL );
		Assert.NotNull( laserOriginL );
	}

	[UnityTest]
	public IEnumerator ShootDoubleLaserTest()
	{
		int laserCounter = 0;

		playerController.ShootDoubleLaser();
		GameObject[] go = GameObject.FindObjectsOfType( typeof( GameObject ) ) as GameObject[];

		foreach( GameObject g in go )
		{
			if( g.name == "Laser(Clone)" )
				laserCounter++;
		}

		Assert.GreaterOrEqual( laserCounter, 2 );

		yield return null;
	}

}
