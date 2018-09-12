﻿using UnityEngine;
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
}