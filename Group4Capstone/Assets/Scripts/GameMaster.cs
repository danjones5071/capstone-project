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

	void Start () {
		// Start the infinite coroutine to add to our score for surviving a certain number of seconds.
		StartCoroutine( TimedScoreIncrease() );
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

	// Getter & setter methods for the score variable.
	public int Score
	{
		get{ return score; }
		set{ score = value; }
	}
}
