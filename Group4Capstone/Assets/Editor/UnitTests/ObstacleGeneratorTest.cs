using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ObstacleGeneratorTest
{
	GameObject obstacleGenerator = GameObject.Find( "Obstacle Generator" );

    [Test]
    public void ObstacleGeneratorExists()
	{
		Assert.NotNull( obstacleGenerator );
    }
		
    [UnityTest]
    public IEnumerator AsteroidIsGenerated()
	{
		ObstacleGenerator og = obstacleGenerator.GetComponent<ObstacleGenerator>();
		og.CreateAsteroid();
		GameObject asteroid = GameObject.Find( "Asteroid(Clone)" );
		Assert.NotNull( asteroid );
        yield return null;
    }
}
