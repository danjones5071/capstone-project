using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ReferencesTest {

    [Test]
    public void GameMasterReferenced()
	{
		GameMaster gameMaster = References.global.gameMaster;

		Assert.NotNull( gameMaster );
    }
		
	[Test]
	public void PlayerReferenced()
	{
		GameObject player = References.global.player;

		Assert.NotNull( player );
	}

	[Test]
	public void ObstacleGeneratorReferenced()
	{
		ObstacleGenerator obstacleGenerator = References.global.obstacleGenerator;

		Assert.NotNull( obstacleGenerator );
	}

	[Test]
	public void CurrencyGeneratorReferenced()
	{
		CurrencyGenerator currencyGenerator = References.global.currencyGenerator;

		Assert.NotNull( currencyGenerator );
	}

	[Test]
	public void SoundEffectsManagerReferenced()
	{
		SoundEffects soundEffects = References.global.soundEffects;

		Assert.NotNull( soundEffects );
	}

	[Test]
	public void UIManagerReferenced()
	{
		UI_Manager uiManager = References.global.uiManager;

		Assert.NotNull( uiManager );
	}

	[Test]
	public void PlayAgainUIReferenced()
	{
		GameObject playAgainUI = References.global.playAgainUI;

		Assert.NotNull( playAgainUI );
	}

	[Test]
	public void StoreUIReferenced()
	{
		GameObject storeUI = References.global.storeUI;

		Assert.NotNull( storeUI );
	}

	[Test]
	public void PauseUIReferenced()
	{
		GameObject pauseUI = References.global.pauseUI;
		
		Assert.NotNull( pauseUI );
	}

	[Test]
	public void PlayerTransformReferenced()
	{
		Transform playerTrans = References.global.playerTrans;

		Assert.NotNull( playerTrans );
	}

	[Test]
	public void PlayerRigidbodyReferenced()
	{
		Rigidbody2D playerRigid = References.global.playerRigid;

		Assert.NotNull( playerRigid );
	}

	[Test]
	public void PlayerControllerReferenced()
	{
		PlayerController playerController = References.global.playerController;

		Assert.NotNull( playerController );
	}
}
