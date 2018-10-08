using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CurrencyGeneratorTest
{
	GameObject currencyGenerator = GameObject.Find( "Obstacle Generator" );
	CurrencyGenerator cg;

	[OneTimeSetUp]
	public void TestSetup()
	{
		cg = References.global.currencyGenerator;
	}

	[Test]
	public void CurrencyGeneratorExists()
	{
		Assert.NotNull( currencyGenerator );
	}

	[UnityTest]
	public IEnumerator CurrencyIsGenerated()
	{
		cg.CreateCoin();
		GameObject coin = GameObject.Find( "Coin(Clone)" );
		Assert.NotNull( coin );
		yield return null;
	}
}
