using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PickupGeneratorTest {

    GameObject pickupGenerator = GameObject.Find( "Pickup Generator" );
    PickupGenerator pg;

    [OneTimeSetUp]
    public  void TestSetup()
    {
        pg = References.global.pickupGenerator;
    }

    [Test]
    public void PickupGeneratorExists()
    {
        Assert.NotNull( pickupGenerator );
    }

    [Test]
    public void PickupGeneratorReferenceExists()
    {
        Assert.NotNull( pg );
    }

    [UnityTest]
    public IEnumerator HealthPickupIsGenerated()
    {
		pg.CreateObject( pg.healthPickup );
        GameObject pickup = GameObject.Find( "HealthPickup3D(Clone)" );
        Assert.NotNull( pickup );
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnergyPickupIsGenerated()
    {
		pg.CreateObject( pg.energyPickup );
        GameObject pickup = GameObject.Find( "EnergyPickup3D(Clone)" );
        Assert.NotNull( pickup );
        yield return null;
    }

	[UnityTest]
	public IEnumerator CurrencyIsGenerated()
	{
		pg.CreateObject( pg.coinPickup );
		GameObject pickup = GameObject.Find( "Coin3D(Clone)" );
		Assert.NotNull( pickup );
		yield return null;
	}
}
