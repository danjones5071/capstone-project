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
	private const int START_LIVES = 3;         // The number of lives the player begins with.
	private PolygonCollider2D playerCollider;  // A reference to the player's collider component.
	public int currency;                       // How much currency the player has earned.
	private int lives = START_LIVES;           // how many lives the player currently has.
	private Store store;                       // Reference to the store script.

	void Start()
	{
		// Cache the necessary components.
		playerCollider = References.global.player.GetComponent<PolygonCollider2D>();
		store = References.global.storeScript;

		// Load player preference data related to music/sfx volume.
		LoadPreferences();

		// Load saved data like store purchases.
        LoadSavedData();
	}

	public void AddToCurrency( int amount )
	{
		currency += amount;
		References.global.uiManager.UpdateCurrencyCount( currency );
		PlayerPrefs.SetInt( References.KEY_CURRENCY, currency );
	}

	public void UpdateLivesCount( int lifeAmount )
    {
        lives += lifeAmount;
        References.global.uiManager.UpdateLivesCount( lives );
    }

	public void PauseGame()
	{
		Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
		References.global.uiManager.TogglePauseUI();
	}

	public void LoadSavedData()
	{
		// Load Currency.
		currency = PlayerPrefs.GetInt( References.KEY_CURRENCY, 0 );
		References.global.uiManager.UpdateCurrencyCount( currency );

		// Load Weapon Purchases.
		if( PlayerPrefs.HasKey(References.WNAME_INFERNO) )
		{
			References.global.playerController.weapons.Add( References.WNAME_INFERNO );
			store.AddToPurchasedList( References.WNAME_INFERNO );
		}
		if( PlayerPrefs.HasKey(References.WNAME_2LASER) )
		{
			References.global.playerController.weapons.Add( References.WNAME_2LASER );
			store.AddToPurchasedList( References.WNAME_2LASER );
		}
		if( PlayerPrefs.HasKey(References.UNAME_ARMOR) )
		{
			References.global.playerController.maxHealth = 150;
			store.AddToPurchasedList( References.UNAME_ARMOR );
		}
	}

	public void LoadPreferences()
	{
		// Load music volume preference.
		if( PlayerPrefs.HasKey(References.KEY_MUSIC) )
		{
			float vol = PlayerPrefs.GetFloat( References.KEY_MUSIC );
			References.global.musicSource.volume = vol;
		}

		// Loud sound effects volume preference.
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

		// Make the player vulnerable again.
		playerCollider.isTrigger = false;
	}

    public void ClearGameScreen()
    {
		// Clear all pooled objects on the screen.
		References.global.obstacleGenerator.ClearGeneratedObjects();
		References.global.enemyGenerator.ClearGeneratedObjects();
		References.global.pickupGenerator.ClearGeneratedObjects();
		References.global.projectilePool.DestroyAllLasers();
    }

	// Getter & setter methods for the score variable.
	public int Currency
	{
		get{ return currency; }
		set{ currency = value; }
	}

	// Getter & setter methods for the lives variable.
	public int Lives
	{
		get{ return lives; }
		set{ UpdateLivesCount( value ); }
	}
}