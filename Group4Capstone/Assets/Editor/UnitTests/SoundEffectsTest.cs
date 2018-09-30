using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SoundEffectsTest
{
	SoundEffects soundEffects;

	[OneTimeSetUp]
	public void TestSetup()
	{
		soundEffects = References.global.soundEffects;
	}

    [UnityTest]
    public IEnumerator LaserSoundPlays()
	{
		soundEffects.PlayLaserSound();
		Assert.NotNull( soundEffects.laserBlast );

        yield return null;
    }

	[UnityTest]
	public IEnumerator ExplosionSoundPlays()
	{
		soundEffects.PlayExplosionSound();
		Assert.NotNull( soundEffects.explosion );

		yield return null;
	}

	[UnityTest]
	public IEnumerator CrashSoundPlays()
	{
		soundEffects.PlayCrashSound();
		Assert.NotNull( soundEffects.crash );

		yield return null;
	}

	[UnityTest]
    public IEnumerator InfernoSoundPlays()
    {
        soundEffects.PlayInfernoSound();
        Assert.NotNull( soundEffects.infernoBlast );

        yield return null;
    }
}
