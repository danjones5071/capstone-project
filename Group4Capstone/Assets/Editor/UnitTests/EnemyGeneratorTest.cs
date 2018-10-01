using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyGeneratorTest
{
	EnemyGenerator eg;

	[OneTimeSetUp]
	public void TestSetup()
	{
		eg = References.global.enemyGenerator;
	}

	[UnityTest]
	public IEnumerator EnemyAIsGenerated()
	{
		eg.CreateTypeAenemy();
		GameObject enemy = GameObject.Find( "EnemyTypeA(Clone)" );
		Assert.NotNull( enemy );
		yield return null;
	}

	[UnityTest]
	public IEnumerator EnemyBIsGenerated()
	{
		eg.CreateTypeBenemy();
		GameObject enemy = GameObject.Find( "EnemyTypeB(Clone)" );
		Assert.NotNull( enemy );
		yield return null;
	}
}
