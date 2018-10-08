using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class UIManagerTest
{
	UI_Manager uiManager;

	[OneTimeSetUp]
	public void TestSetup()
	{
		uiManager = References.global.uiManager;
	}

	[Test]
	public void UIManagerExists()
	{
		Assert.NotNull( uiManager );
	}

	[Test]
	public void HealthBarExists()
	{
		Assert.NotNull( uiManager.healthBar );
	}

	[Test]
	public void EnergyBarExists()
	{
		Assert.NotNull( uiManager.energyBar );
	}

	[UnityTest]
	public IEnumerator CurrencyCountTextUpdates()
	{
		GameMaster gameMaster = References.global.gameMaster;

		Assert.AreEqual( uiManager.currencyCount.text, "x 0" );

		gameMaster.AddToCurrency( 25 );

		Assert.AreEqual( uiManager.currencyCount.text, "x 25" );

		yield return null;
	}
}
