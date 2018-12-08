using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	public Slider musicSlider;
	public Slider sfxSlider;
	public AudioSource music;

	private const string MUSIC_KEY = "MusicVolume";
	private const string SFX_KEY = "SFXVolume";

	public void InitVolume()
	{
		if( PlayerPrefs.HasKey(MUSIC_KEY) )
		{
			float musicVol = PlayerPrefs.GetFloat( MUSIC_KEY );
			musicSlider.value = musicVol;
			music.volume = musicVol;
		}

		if( PlayerPrefs.HasKey(SFX_KEY) )
		{
			float sfxVol = PlayerPrefs.GetFloat( SFX_KEY );
			sfxSlider.value = sfxVol;
		}
	}

	public void SetMusicVolume()
	{
		float value = musicSlider.value;
		music.volume = value;
		PlayerPrefs.SetFloat( MUSIC_KEY, value );
	}

	public void SetSFXVolume()
	{
		float value = sfxSlider.value;
		PlayerPrefs.SetFloat( SFX_KEY, value );
	}

	public void DeleteSavedData()
	{
		PlayerPrefs.DeleteAll();
	}
}