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
    public AudioClip cybergun;
    public AudioClip blackHolePull;
    public AudioClip energyPickup;
    public AudioClip healthPickup;
	public AudioClip currencyPickup;

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

    public void PlayEnergyPickUpSound()
    {
        audioSource.PlayOneShot( energyPickup );
    }

    public void PlayHealthPickUpSound()
    {
        audioSource.PlayOneShot( healthPickup );
    }

	public void PlayCurrencyPickUpSound()
	{
		audioSource.PlayOneShot( currencyPickup );
	}

    public void PlayLaserSound()
	{
		audioSource.PlayOneShot( laserBlast );
	}

    public void PlayEnemyLaserSound()
    {
        audioSource.PlayOneShot(cybergun); //Need unique Audio for enemy laser. 
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

	public void PlayBlackHolePullSound()
    	{
    		audioSource.PlayOneShot( blackHolePull );
    	}

}
