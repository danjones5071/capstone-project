using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
	public GameObject prompt;
	public GameObject timeSurvived;
	public GameObject store;
	public GameObject leaderboard;
	public GameObject storeButton;
	public GameObject mainMenuButton;
	public GameObject leaderboardButton;

	void OnEnable()
	{
		prompt.SetActive( false );
		timeSurvived.SetActive( false );
		storeButton.SetActive( false );
		mainMenuButton.SetActive( false );
		leaderboardButton.SetActive( false );
		StartCoroutine( ShowPrompt() );
	}

	void Update ()
	{
		if( prompt.activeInHierarchy && Input.GetKeyDown( KeyCode.Space ) )
		{
			if( !store.activeInHierarchy && !leaderboard.activeInHierarchy )
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
