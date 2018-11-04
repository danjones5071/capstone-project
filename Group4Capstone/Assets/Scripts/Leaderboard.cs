using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
	private const string privateCode = "XybC6RGIukOjul8H5_vxogufrL0ynV5U2In6agZsUwgw";
	private const string publicCode = "5bdf467661f94409f8b4fd68";
	private const string url = "http://dreamlo.com/lb/";

	public InputField nameField;
	public Text timeText;
	public Transform topScores;

	private Score[] scoresList;

	public void SubmitScore()
	{
		string name = nameField.text;

		string fullScore = timeText.text;
		string[] temp = fullScore.Split( ':' );
		string scoreMin = temp[0];
		string scoreSec = temp[1];
		int score = int.Parse( scoreMin + scoreSec );
		StartCoroutine( UploadScore( name, score ) );
	}

	public void DisplayScores()
	{
		FetchScores();
		for( int i = 0; i < scoresList.Length; i++ )
		{
			Transform newText = createScoreTextComponent( scoresList[i] );
			Vector2 pos = newText.position;
			newText.position = new Vector2( pos.x, pos.y - 10 * i );
		}
	}

	public Transform createScoreTextComponent( Score score )
	{
		GameObject newText = new GameObject( "ScoreText" );
		newText.transform.SetParent( topScores );
		Text text = newText.AddComponent<Text>();
		text.text = score.name + ", " + score.score;
		return newText.transform;
	}

	IEnumerator UploadScore( string name, int score )
	{
		WWW src = new WWW( url + privateCode + "/add/" + WWW.EscapeURL( name ) + "/" + score );
		Debug.Log( "Attempting to submit with url: " + name + " - " + score );
		yield return src;

		if( string.IsNullOrEmpty( src.error ) )
		{
			Debug.Log( "Score submitted successfully!" );
		}
		else
		{
			Debug.Log( "Error uploading score: " + src.error );
		}
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
		}
		else
		{
			Debug.Log( "Error fetching leaderboard scores: " + src.error );
		}
	}

	void FormatScores( string text )
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