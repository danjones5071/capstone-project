//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	SoundEffects.cs
//
//	A manager for playing sound effects at runtime.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

[ExecuteInEditMode]
public class SoundEffects : MonoBehaviour
{
	// Public variables which can be modified in the editor at runtime.
	public AudioClip laserBlast;
	public AudioClip infernoBlast;
	public AudioClip explosion;
	public AudioClip crash;

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
		infernoBlast.LoadAudioData();
		explosion.LoadAudioData();
		crash.LoadAudioData();
	}

	public void PlayLaserSound()
	{
		audioSource.PlayOneShot( laserBlast );
	}

	public void PlayInfernoSound()
    	{
    		audioSource.PlayOneShot( infernoBlast );
    	}

	public void PlayExplosionSound()
	{
		audioSource.PlayOneShot( explosion );
	}

	public void PlayCrashSound()
	{
		audioSource.PlayOneShot( crash );
	}

}
