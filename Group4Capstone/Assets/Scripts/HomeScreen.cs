//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	HomeScreen.cs
//
//	Handles all functionality required for the game's main menu.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
	public GameObject foreground;    // The fade-in effect.
	public GameObject settingsPage;  // The settings page UI.
	public Settings settings;        // The settings cript.

	void Start()
	{
		// Initialize the game volume baesd on saved preferences.
		settings.InitVolume();

		settingsPage.SetActive( false );
		foreground.SetActive( true );
	}

	void Update()
	{
		// Load gameplay when the player hits the "space" key.
		if( Input.GetKeyDown(KeyCode.Space) & !settingsPage.activeInHierarchy )
		{
			SceneManager.LoadScene( References.SCENE_GAMEPLAY );
		}
	}
}