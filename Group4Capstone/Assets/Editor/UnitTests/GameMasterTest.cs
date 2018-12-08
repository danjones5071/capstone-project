using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameMasterTest
{
	GameObject gameMaster = GameObject.Find( "GameMaster" );

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
		gm.Lives = 5;
		Assert.AreNotEqual( gm.Lives, 0 );
		Assert.AreEqual( gm.Lives, 5 );

		gm.Lives = 3;
		Assert.AreEqual( gm.Lives, 3 );
	}
}