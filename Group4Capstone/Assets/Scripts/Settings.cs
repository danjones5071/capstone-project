using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	public Slider musicSlider;
	public Slider sfxSlider;
	public AudioSource music;

	public void InitVolume()
	{
		if( PlayerPrefs.HasKey(References.KEY_MUSIC) )
		{
			float musicVol = PlayerPrefs.GetFloat( References.KEY_MUSIC );
			musicSlider.value = musicVol;
			music.volume = musicVol;
		}

		if( PlayerPrefs.HasKey(References.KEY_SFX) )
		{
			float sfxVol = PlayerPrefs.GetFloat( References.KEY_SFX );
			sfxSlider.value = sfxVol;
		}
	}

	public void SetMusicVolume()
	{
		float value = musicSlider.value;
		music.volume = value;
		PlayerPrefs.SetFloat( References.KEY_MUSIC, value );
	}

	public void SetSFXVolume()
	{
		float value = sfxSlider.value;
		PlayerPrefs.SetFloat( References.KEY_SFX, value );
	}

	public void DeleteSavedData()
	{
		PlayerPrefs.DeleteAll();
	}
}