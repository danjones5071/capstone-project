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
		if( !References.global.playerController.weapons.Contains( References.WNAME_INFERNO ) )
		{
			int startCoins = References.global.gameMaster.currency;

			storeScript.Purchase( References.WNAME_INFERNO );

			int endCoins = References.global.gameMaster.currency;
			Item item = storeScript.weapons.Find( i => i.name == References.WNAME_INFERNO );
			Assert.AreEqual( startCoins - item.price, endCoins );
		}
		yield return null;
	}

	[UnityTest]
	public IEnumerator PurchaseDoubleLaserTest()
	{
		if( !References.global.playerController.weapons.Contains( References.WNAME_2LASER ) )
		{
			int startCoins = References.global.gameMaster.currency;

			storeScript.Purchase( References.WNAME_2LASER );

			int endCoins = References.global.gameMaster.currency;
			Item item = storeScript.weapons.Find( i => i.name == References.WNAME_2LASER );
			Assert.AreEqual( startCoins - item.price, endCoins );
		}
		yield return null;
	}
}
