using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PickupGeneratorTest {

    GameObject pickupGenerator = GameObject.Find("Pickup Generator");
    PickupGenerator pc;

    [OneTimeSetUp]
    public  void TestSetup()
    {
        pc = References.global.pickupGenerator;
    }

    [Test]
    public void PickupGeneratorExists()
    {
        Assert.NotNull(pickupGenerator);
    }

    [Test]
    public void PickupGeneratorReferenceExists()
    {
        Assert.NotNull(pc);
    }

    [UnityTest]
    public IEnumerator HealthPickupIsGenerated()
    {
        pc.CreateHealthPickup();
        GameObject pickup = GameObject.Find("HealthPickup(Clone)");
        Assert.NotNull(pickup);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnergyPickupIsGenerated()
    {
        pc.CreateEnergyPickup();
        GameObject pickup = GameObject.Find("EnergyPickup(Clone)");
        Assert.NotNull(pickup);
        yield return null;
    }

}
