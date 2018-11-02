//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	GameMaster.cs
//
//	A singleton-like class responsible for tracking game-state information such as the player's score, in-game
//  currency, status effects, etc.
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
	public int currency = 0;

	void Start ()
	{
		// Start the infinite coroutine to add to our score for surviving a certain number of seconds.
		StartCoroutine( TimedScoreIncrease() );

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
}
