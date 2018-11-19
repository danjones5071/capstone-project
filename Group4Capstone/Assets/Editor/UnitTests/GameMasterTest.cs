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
    public void ScoreStartsAtZero()
	{
		GameMaster gm = new GameMaster();
		Assert.AreEqual( gm.Score, 0 );
    }

	[Test]
	public void ScoreGetterSetterTest()
	{
		GameMaster gm = new GameMaster();
		gm.Score = 100;
		Assert.AreNotEqual( gm.Score, 0 );
		Assert.AreEqual( gm.Score, 100 );

		gm.Score = 22;
		Assert.AreEqual( gm.Score, 22 );
	}
		
	[UnityTest]
	public IEnumerator AddToScoreTest()
	{
		GameMaster gm = new GameMaster();
		Assert.AreEqual( gm.Score, 0 );

		gm.AddToScore( 5 );
		Assert.AreEqual( gm.Score, 5 );

		gm.AddToScore( 100 );
		Assert.AreEqual( gm.Score, 105 );

		gm.AddToScore( -5 );
		Assert.AreEqual( gm.Score, 100 );

		yield return null;
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
}
