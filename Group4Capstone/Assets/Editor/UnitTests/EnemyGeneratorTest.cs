using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyGeneratorTest
{
	GameObject enemyGenerator = GameObject.Find( "EnemyGenerator" );
	EnemyGenerator eg;

	[OneTimeSetUp]
	public void TestSetup()
	{
		eg = References.global.enemyGenerator;
	}

	[Test]
	public void EnemyGeneratorExists()
	{
		Assert.NotNull( enemyGenerator );
	}

	[Test]
	public void EnemyGeneratorReferenceExists()
	{
		Assert.NotNull( eg );
	}

	[UnityTest]
	public IEnumerator EnemyAIsGenerated()
	{
		eg.CreateObject( eg.enemyA );
		GameObject enemy = GameObject.Find( "EnemyTypeA(Clone)" );
		Assert.NotNull( enemy );
		yield return null;
	}

	[UnityTest]
	public IEnumerator EnemyBIsGenerated()
	{
		eg.CreateObject( eg.enemyB );
		GameObject enemy = GameObject.Find( "EnemyTypeB(Clone)" );
		Assert.NotNull( enemy );
		yield return null;
	}
}
