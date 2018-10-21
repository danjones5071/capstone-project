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

	[Test]
	public void CurrencyCountTextExists()
	{
		Assert.NotNull( uiManager.currencyCount );
	}

	[Test]
	public void WeaponTextExists()
	{
		Assert.NotNull( uiManager.weaponText );
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

	[UnityTest]
	public IEnumerator WeaponTextUpdates()
	{
		uiManager.SetWeaponText( "Laser" );

		Assert.AreEqual( uiManager.weaponText.text, "Laser" );

		uiManager.SetWeaponText( "Testing..." );

		Assert.AreEqual( uiManager.weaponText.text, "Testing..." );
		
		yield return null;
	}

	[UnityTest]
	public IEnumerator ToggleStoreUITest()
	{
		bool enabled = References.global.storeUI.activeInHierarchy;

		uiManager.ToggleStoreUI();

		bool newState = References.global.storeUI.activeInHierarchy;

		Assert.AreEqual( newState, !enabled );

		yield return null;
	}

	[UnityTest]
	public IEnumerator TogglePauseUITest()
	{
		bool enabled = References.global.pauseUI.activeInHierarchy;
		
		uiManager.TogglePauseUI();
		
		bool newState = References.global.pauseUI.activeInHierarchy;
		
		Assert.AreEqual( newState, !enabled );
		
		yield return null;
	}
}
