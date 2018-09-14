﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
	public Image background;
	public GameObject prompt;
	public Color fadeColor;

	public float fadeSpeed = 2.0f;

	void Start ()
	{
		StartCoroutine( ShowPrompt() );
	}

	void Update () {
		background.color = Color.Lerp( background.color, fadeColor, fadeSpeed * Time.deltaTime );

		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			RestartGame();
		}
	}

	IEnumerator ShowPrompt()
	{
		yield return new WaitForSeconds( 1 );
		prompt.SetActive( true );
	}

	public void RestartGame()
	{
		SceneManager.LoadScene( "Gameplay" );
	}
}