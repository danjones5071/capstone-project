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
	private const int LIVES = 3;
    private const int CURRENCY = 0;
	private PolygonCollider2D playerCollider;

	public int currency = GameMaster.CURRENCY;
	public int lives = GameMaster.LIVES;

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
        PlayerPrefs.SetInt( "Lives", lives );
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
			Debug.Log( "Inferno Weapon Loaded" );
		}
		if( PlayerPrefs.GetInt( References.WNAME_2LASER, 0 ) != 0 )
		{
			References.global.playerController.weapons.Add( References.WNAME_2LASER );
			Debug.Log( "Double Laser Weapon Loaded" );
		}
	}

/*	public void ResetData()
	{
		// Reset Currency.
		References.global.uiManager.UpdateCurrencyCount( GameMaster.CURRENCY );
        PlayerPrefs.SetInt( "Currency", GameMaster.CURRENCY );


		// Reset Lives.
		References.global.uiManager.UpdateLivesCount( GameMaster.LIVES );
		PlayerPrefs.SetInt( "Lives", GameMaster.LIVES );


		// Reset Weapon Purchases.
		if( PlayerPrefs.GetInt( References.WNAME_INFERNO, 0 ) != 0 )
		{
			References.global.playerController.weapons.Remove( References.WNAME_INFERNO );
			PlayerPrefs.SetInt( References.WNAME_INFERNO, 0 );
			Debug.Log( "Inferno Weapon Removed" );
		}
		if( PlayerPrefs.GetInt( References.WNAME_2LASER, 0 ) != 0 )
		{
			References.global.playerController.weapons.Remove( References.WNAME_2LASER );
			PlayerPrefs.SetInt( References.WNAME_2LASER, 0 );
            Debug.Log( "Double Laser Weapon Removed" );
		}
	}*/

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
		// Wait a few seconds before respawning player
		yield return new WaitForSeconds( 3 );
		References.global.player.SetActive( true );
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
