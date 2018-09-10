using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SoundEffectsTest
{
	GameObject soundEffects = GameObject.Find( "Sound Effects Manager" );
	SoundEffects se;

	[OneTimeSetUp]
	public void TestSetup()
	{
		se = soundEffects.GetComponent<SoundEffects>();
	}

    [UnityTest]
    public IEnumerator LaserSoundPlays()
	{
		se.PlayLaserSound();

		Assert.NotNull( se.laserBlast );

        yield return null;
    }

	[UnityTest]
	public IEnumerator ExplosionSoundPlays()
	{
		se.PlayExplosionSound();

		Assert.NotNull( se.explosion );

		yield return null;
	}
}
