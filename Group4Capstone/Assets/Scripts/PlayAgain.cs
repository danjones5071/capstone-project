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

	void Update () {
		background.color = Color.Lerp( background.color, fadeColor, fadeSpeed * Time.deltaTime );

		if( Input.GetKeyDown( KeyCode.Space ) && prompt.activeInHierarchy )
		{
			RestartGame();
		}
	}

	IEnumerator ShowPrompt()
	{
		yield return new WaitForSeconds( 1 );
	    DecidePromptOrGameOver();
		timeSurvived.SetActive( true );
		mainMenuButton.SetActive( true );
		leaderboardButton.SetActive( true );
	}

	public void RestartGame()
	{
		SceneManager.LoadScene( "Gameplay" );
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene( "Home" );
	}

	private void DecidePromptOrGameOver()
	{
	    if(PlayerPrefs.GetInt( "Lives", 0 ) > 0)
	    {
	        Debug.Log("Showing prompt and store since player still has lives remaining");
	        prompt.SetActive( true );
	        storeButton.SetActive( true );
            References.global.gameMaster.GameDataResetFlag( false);
	    }
	    else
	    {
	        Debug.Log("Not showing prompt and store since player still has no lives remaining");
	        gameOverText.SetActive( true );
	        storeButton.SetActive( false );
            References.global.gameMaster.GameDataResetFlag( true);
	    }
	}
}
