namespace CTCI.Tests;

[TestClass]
public class Exercises6Tests
{
    [TestMethod]
    public void Ex6_SuperEggDrop_DP()
    {
        Assert.AreEqual(3, Exercises6.Ex6_SuperEggDrop_DP(2, 6));
        // Assert.AreEqual(23, Exercises6.Ex6_SuperEggDrop_DP(4, 10000));  // Too slow
        // Assert.AreEqual(13, Exercises6.Ex6_SuperEggDrop_DP(7, 5000)); // Too slow
    }

    [TestMethod]
    public void Ex6_SuperEggDrop_Binomial()
    {
        Assert.AreEqual(3, Exercises6.Ex6_SuperEggDrop_Binomial(2, 6));
        Assert.AreEqual(23, Exercises6.Ex6_SuperEggDrop_Binomial(4, 10000));
        Assert.AreEqual(13, Exercises6.Ex6_SuperEggDrop_Binomial(7, 5000));
    }
}
