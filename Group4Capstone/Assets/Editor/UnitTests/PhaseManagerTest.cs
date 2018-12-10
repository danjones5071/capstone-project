using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PhaseManagerTest {

    GameObject phaseManager = GameObject.Find("Enemy Generator");
    PhaseManger pm;

    [OneTimeSetUp]
    public void TestSetup()
    {
        pm = References.global.phaseManager;
    }

    [Test]
    public void PhaseManagerExists()
    {
        Assert.NotNull( phaseManager );
    }

    [Test]
    public void PhaseManagerReferenceExists()
    {
        Assert.NotNull( pm );
    }

    [UnityTest]
    public IEnumerator PhaseCountIncrements()
    {
        int startingPhaseCount = pm.getPhaseCount();
        pm.NextPhase();
        Assert.That( startingPhaseCount == (pm.getPhaseCount() - 1) );
        yield return null;
    }

    [UnityTest]
    public IEnumerator CurrentPhaseIncrements()
    {
        int startingCurrentPhase = pm.getCurrentPhase();
        pm.NextPhase();
        Assert.That( startingCurrentPhase == (pm.getCurrentPhase() - 1) );
        yield return null;
    }

    [UnityTest]
    public IEnumerator CurrentPhaseRollsOver()
    {
        // Get Current Phase to phase 4 - the last phase
        while( pm.getCurrentPhase() != 4 )
        {
            pm.NextPhase();
        }

        // Increment one more time and the phase should be set back to 0
        pm.NextPhase();
        Assert.Zero( pm.getCurrentPhase() );
        yield return null;
    }
}
