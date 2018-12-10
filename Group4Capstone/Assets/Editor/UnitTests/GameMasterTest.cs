using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameMasterTest
{
	GameObject gameMaster;

	[OneTimeSetUp]
	public void TestSetup()
	{
		gameMaster = GameObject.Find( "Game Master" );
	}

	[Test]
	public void GameMasterExists()
	{
		Assert.NotNull( gameMaster );
	}

	[Test] public void GameMasterReferenceExists()
	{
		GameMaster gm = References.global.gameMaster;
		Assert.NotNull( gm );
	}

	[Test]
	public void CurrencyGetterSetterTest()
	{
		GameMaster gm = new GameMaster();
		gm.Currency = 100;
		Assert.AreNotEqual( gm.Currency, 0 );
		Assert.AreEqual( gm.Currency, 100 );

		gm.Currency = 22;
		Assert.AreEqual( gm.Currency, 22 );
	}

	[Test]
	public void LivesGetterSetterTest()
	{
		GameMaster gm = new GameMaster();

		// Should begin with 3 lives.
		Assert.AreEqual( gm.Lives, 3 );

		gm.Lives = 5;
		Assert.AreNotEqual( gm.Lives, 0 );
		Assert.AreEqual( gm.Lives, 8 );

		// The setter actually adds to the lives.
		gm.Lives = 2;
		Assert.AreEqual( gm.Lives, 10 );

		gm.Lives = -1;
		Assert.AreEqual( gm.Lives, 9 );
	}
}