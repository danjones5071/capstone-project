using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
	[SerializeField]
	public GameObject foreground;
	public GameObject settingsPage;
	public Settings settings;

	void Start()
	{
		settings.InitVolume();

		settingsPage.SetActive( false );
		foreground.SetActive( true );
	}

	void Update()
	{
		if( Input.GetKeyDown(KeyCode.Space) & !settingsPage.activeInHierarchy )
		{
			SceneManager.LoadScene( "Gameplay" );
		}
	}
}
