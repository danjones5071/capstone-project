//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	SoundEffects.cs
//
//	A manager for playing sound effects at runtime.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class SoundEffects : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public AudioClip laserBlast;
	public AudioClip explosion;

	// Private variables to cache necessary components.
	private AudioSource audioSource;

	void Awake()
	{
		// Cache a reference to the audio source component.
		audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		// Preload all the desired sound effects.
		laserBlast.LoadAudioData();
		explosion.LoadAudioData();
	}

	public void PlayLaserSound()
	{
		audioSource.PlayOneShot( laserBlast );
	}

	public void PlayExplosionSound()
	{
		audioSource.PlayOneShot( explosion );
	}

}
