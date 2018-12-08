using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
	public Image background;
	public GameObject prompt;
	public GameObject gameOverText;
	public GameObject timeSurvived;
	public GameObject store;
	public GameObject leaderboard;
	public GameObject storeButton;
	public GameObject mainMenuButton;
	public GameObject leaderboardButton;
	public Color fadeColor;

	public float fadeSpeed = 2.0f;

	void OnEnable()
	{
		prompt.SetActive( false );
		gameOverText.SetActive( false );
		timeSurvived.SetActive( false );
		storeButton.SetActive( false );
		mainMenuButton.SetActive( false );
		leaderboardButton.SetActive( false );
		StartCoroutine( ShowPrompt() );
	}

	void Update ()
	{
		background.color = Color.Lerp( background.color, fadeColor, fadeSpeed * Time.deltaTime );

		if(  prompt.activeInHierarchy && Input.GetKeyDown( KeyCode.Space ) )
		{
			if( !store.activeInHierarchy && !leaderboard.activeInHierarchy)
            	RestartGame();
		}
	}

	IEnumerator ShowPrompt()
	{
		yield return new WaitForSeconds( 1 );
		prompt.SetActive( true );
		timeSurvived.SetActive( true );
		storeButton.SetActive( true );
		mainMenuButton.SetActive( true );
		leaderboardButton.SetActive( true );
	}

	public void RestartGame()
	{
		SceneManager.LoadScene( References.SCENE_GAMEPLAY );
	}

	public void ReturnToMainMenu()
	{
	    //ensure game related values are reset before the player begins a new game
		SceneManager.LoadScene( References.SCENE_HOME );
	}
}
