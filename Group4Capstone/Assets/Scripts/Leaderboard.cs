using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
	// Visit the following URL for more information on dreamlo high scores: 
	private const string privateCode = "XybC6RGIukOjul8H5_vxogufrL0ynV5U2In6agZsUwgw";
	private const string publicCode = "5bdf467661f94409f8b4fd68";
	private const string url = "http://dreamlo.com/lb/";
	private bool submitted = false; // Has the user submitted their current score yet?

	public InputField nameField;
	public Text timeText;
	public GameObject submitScoreUI;
	public GameObject topScores;
	public GameObject scoreTextPrefab;

	private Score[] scoresList;

	void OnEnable()
	{
		if( !submitted )
		{
			submitScoreUI.SetActive( true );
			topScores.SetActive( false );
		}
		else
		{
			submitScoreUI.SetActive( false );
			topScores.SetActive( true );
			FetchScores();
		}
	}

	// When the leaderboard UI is disabled, make sure we destroy the current score textboxes for next time.
	void OnDisable()
	{
		Transform trans = topScores.transform;
		foreach( Transform child in trans )
		{
			if( child.name == "ScoreTextPrefab(Clone)" )
			{
				Destroy( child.gameObject );
			}
		}
	}

	public void SubmitScore()
	{
		string name = nameField.text;
		string fullScore = timeText.text;

		int score = MinSecToSeconds( fullScore );

		StartCoroutine( UploadScore( name, score ) );
	}

	public void DisplayScores()
	{
		int yStart = -100;
		int xOff = -300;
		int yOff = yStart;

		for( int i = 0; i < 10; i++ )
		{
			GameObject newTextObj = Instantiate( scoreTextPrefab, topScores.transform );
			Text newText = newTextObj.GetComponent<Text>();

			if( i < scoresList.Length )
			{
				Score currentScore = scoresList[i];
				newText.text = ( i + 1 ) + ". " + currentScore.name + ", " + SecondsToMinSec( currentScore.score );
			}
			else
			{
				newText.text = "" + ( i + 1 ) + '.';
			}

			Vector2 pos = newTextObj.transform.position;

			if( i == 5 )
			{
				yOff = yStart;
				xOff = 100;
			}
			
			newTextObj.transform.position = new Vector2( pos.x + xOff, pos.y - yOff );

			yOff += 50;
		}
	}

	IEnumerator UploadScore( string name, int score )
	{
		WWW src = new WWW( url + privateCode + "/add/" + WWW.EscapeURL( name ) + "/" + score );
		Debug.Log( "Attempting to submit with url: " + name + " - " + score );
		yield return src;

		if( string.IsNullOrEmpty( src.error ) )
		{
			Debug.Log( "Score submitted successfully!" );
			submitted = true;
		}
		else
		{
				Debug.Log( "Error uploading score: " + src.error );
		}

		FetchScores();
	}

	public void FetchScores()
	{
		StartCoroutine( DownloadScores() );
	}

	IEnumerator DownloadScores()
	{
		WWW src = new WWW( url + publicCode + "/pipe/" );
		yield return src;

		if( string.IsNullOrEmpty( src.error ) )
		{
			Debug.Log( "Scores fetched successfully!" );
			FormatScores( src.text );
			DisplayScores();
		}
		else
		{
			Debug.Log( "Error fetching leaderboard scores: " + src.error );
		}
	}

	private void FormatScores( string text )
	{
		string[] scores = text.Split( new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries );
		scoresList = new Score[scores.Length];

		for( int i = 0; i < scores.Length; i++ )
		{
			string[] info = scores[i].Split( new char[] {'|'});
			string name = info[0];
			int score = int.Parse( info[1] );
			scoresList[i] = new Score( name, score );
		}
	}

	public int MinSecToSeconds( string fullScore )
	{
		string[] temp = fullScore.Split( ':' );
		string scoreMin = temp[0];
		string scoreSec = temp[1];
		int min = int.Parse( scoreMin );
		int sec = int.Parse( scoreSec );
		int seconds = (60 * min) + sec;

		return seconds;
	}

	public string SecondsToMinSec( int seconds )
	{
		return TimeSpan.FromSeconds( seconds ).ToString();
	}
}

public struct Score
{
	public string name;
	public int score;

	public Score( string name, int score )
	{
		this.name = name;
		this.score = score;
	}
}