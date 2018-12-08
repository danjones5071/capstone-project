//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	GameMaster.cs
//
//	A singleton-like class responsible for tracking game-state information such as the player's score, in-game
//  currency, etc.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	// Declare variables to store score and game-state related values.
	private int score = 0;				// Player's current score for this round.
	private int scoreInterval = 1;		// How many seconds we should wait to add to the score.
	private int scorePerInterval = 5;	// How much is added to the score each interval.
	private const int START_LIVES = 3;
	private PolygonCollider2D playerCollider;

	public int currency;
	public int lives = START_LIVES;

	void Start ()
	{
		playerCollider = References.global.player.GetComponent<PolygonCollider2D>();

		// Start the infinite coroutine to add to our score for surviving a certain number of seconds.
		StartCoroutine( TimedScoreIncrease() );

		// Load player preference data related to music/sfx volume.
		LoadPreferences();
        LoadSavedData();
	}

	IEnumerator TimedScoreIncrease()
	{
		// Continue generating infinitely.
		while( true )
		{
			AddToScore( scorePerInterval );						// Add the desired amount to the score each interval.
			yield return new WaitForSeconds( scoreInterval );	// Wait a while before adding to the score again.
		}
	}

	public void AddToScore( int amount )
	{
		score += amount;
	}

	public void AddToCurrency( int amount )
	{
		currency += amount;
		References.global.uiManager.UpdateCurrencyCount( currency );
		PlayerPrefs.SetInt( "Currency", currency );
	}

	public void UpdateLivesCount( int lifeAmount )
    {
        lives += lifeAmount;
        References.global.uiManager.UpdateLivesCount( lives );
    }

	// Getter & setter methods for the score variable.
	public int Score
	{
		get{ return score; }
		set{ score = value; }
	}

	// Getter & setter methods for the score variable.
	public int Currency
	{
		get{ return score; }
		set{ score = value; }
	}

	// Getter & setter methods for the lives variable.
	public int Lives
	{
		get{ return lives; }
		set{ lives = value; }
    }

	public void PauseGame()
	{
		Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
		References.global.uiManager.TogglePauseUI();
	}

	public void LoadSavedData()
	{
		// Load Currency.
		currency = PlayerPrefs.GetInt( "Currency", 0 );
		References.global.uiManager.UpdateCurrencyCount( currency );

		// Load Weapon Purchases.
		if( PlayerPrefs.GetInt( References.WNAME_INFERNO, 0 ) != 0 )
		{
			References.global.playerController.weapons.Add( References.WNAME_INFERNO );
		}
		if( PlayerPrefs.GetInt( References.WNAME_2LASER, 0 ) != 0 )
		{
			References.global.playerController.weapons.Add( References.WNAME_2LASER );
		}
	}

	public void LoadPreferences()
	{
		if( PlayerPrefs.HasKey(References.KEY_MUSIC) )
		{
			float vol = PlayerPrefs.GetFloat( References.KEY_MUSIC );
			References.global.musicSource.volume = vol;
		}
		if( PlayerPrefs.HasKey(References.KEY_SFX) )
		{
			float vol = PlayerPrefs.GetFloat( References.KEY_SFX );
			References.global.soundEffectsSource.volume = vol;
		}
	}

	public void RespawnPlayer()
	{
		StartCoroutine( RespawnAfterTime() );
	}

	IEnumerator RespawnAfterTime()
	{
		Animator playerAnim = References.global.player.GetComponent<Animator>();

		// Wait a few seconds before respawning player
		yield return new WaitForSeconds( 3 );
		References.global.player.SetActive( true );
		playerAnim.enabled = true;
		References.global.enemyGenerator.generate = true;

		// While player collider is a trigger, they are immune to damage but can still pickup items.
		playerCollider.isTrigger = true;

		// Wait a few seconds before making player vulnerable again.
		yield return new WaitForSeconds( 3 );

		playerCollider.isTrigger = false;
	}

    public void ClearGameScreen()
    {
		References.global.obstacleGenerator.ClearGeneratedObjects();
		References.global.enemyGenerator.ClearGeneratedObjects();
		References.global.pickupGenerator.ClearGeneratedObjects();
		References.global.projectilePool.DestroyAllLasers();
    }
}
