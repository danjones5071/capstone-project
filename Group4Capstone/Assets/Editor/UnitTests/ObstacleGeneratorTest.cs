using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ObstacleGeneratorTest
{
	GameObject obstacleGenerator = GameObject.Find( "Obstacle Generator" );
	ObstacleGenerator og;

	[OneTimeSetUp]
	public void TestSetup()
	{
		og = References.global.obstacleGenerator;
	}

    [Test]
    public void ObstacleGeneratorExists()
	{
		Assert.NotNull( obstacleGenerator );
    }
		
    [UnityTest]
    public IEnumerator AsteroidIsGenerated()
	{
		og.CreateAsteroid();
		GameObject asteroid = GameObject.Find( "Asteroid(Clone)" );
		Assert.NotNull( asteroid );
        yield return null;
    }
}
