using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LeaderboardTest
{
	Leaderboard leaderboard = References.global.leaderboard;

    [Test]
    public void LeaderboardScriptExists() {
		Assert.NotNull( leaderboard );
    }

	[Test]
	public void ConvertMinSecToSeconds()
	{
		string minSec = "01:11";
		int seconds = leaderboard.MinSecToSeconds( minSec );
		Assert.AreEqual( 71, seconds );

		minSec = "5:00";
		seconds = leaderboard.MinSecToSeconds( minSec );
		Assert.AreEqual( 300, seconds );
	}

	[Test]
	public void ConvertSecondsToMinSec()
	{
		int seconds = 71;
		string minSec = leaderboard.SecondsToMinSec( seconds );
		Assert.AreEqual( "00:01:11", minSec );

		seconds = 300;
		minSec = leaderboard.SecondsToMinSec( seconds );
		Assert.AreEqual( "00:05:00", minSec );
	}

}
