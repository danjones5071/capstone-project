using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PickupGeneratorTest {

    GameObject pickupGenerator = GameObject.Find("Pickup Generator");
    PickupGenerator pg;

    [OneTimeSetUp]
    public  void TestSetup()
    {
        pg = References.global.pickupGenerator;
    }

    [Test]
    public void PickupGeneratorExists()
    {
        Assert.NotNull(pickupGenerator);
    }

    [Test]
    public void PickupGeneratorReferenceExists()
    {
        Assert.NotNull(pg);
    }

    [UnityTest]
    public IEnumerator HealthPickupIsGenerated()
    {
        pg.CreateHealthPickup();
        GameObject pickup = GameObject.Find("HealthPickup(Clone)");
        Assert.NotNull(pickup);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnergyPickupIsGenerated()
    {
        pg.CreateEnergyPickup();
        GameObject pickup = GameObject.Find("EnergyPickup(Clone)");
        Assert.NotNull(pickup);
        yield return null;
    }

}
