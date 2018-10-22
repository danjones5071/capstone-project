using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class StoreTest
{
	GameObject storeUI;
	Store storeScript;

	[OneTimeSetUp]
	public void TestSetup()
	{
		storeUI = References.global.storeUI;
		storeScript = storeUI.GetComponent<Store>();
	}

	[Test]
	public void StoreUIExists()
	{
		Assert.NotNull( storeUI );
	}

	[UnityTest]
	public IEnumerator PurchaseInfernoTest()
	{
		int startCoins = References.global.gameMaster.currency;

		storeScript.Purchase( "Inferno" );

		int endCoins = References.global.gameMaster.currency;
		Assert.AreEqual( startCoins - (int)Store.Weapons.Inferno, endCoins );

		yield return null;
	}

	[UnityTest]
	public IEnumerator PurchaseDoubleLaserTest()
	{
		int startCoins = References.global.gameMaster.currency;

		storeScript.Purchase( "DoubleLaser" );

		int endCoins = References.global.gameMaster.currency;
		Assert.AreEqual( startCoins - (int)Store.Weapons.DoubleLaser, endCoins );

		yield return null;
	}
}
