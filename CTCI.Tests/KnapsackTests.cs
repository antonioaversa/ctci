using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CTCI.Tests;

[TestClass]
public class KnapsackTests
{
    [TestMethod]
    public void Knapsack01()
    {
        Assert.AreEqual(1, Knapsack.Knapsack01(new[] { 1 }, new[] { 1 }, 10));
        Assert.AreEqual(3, Knapsack.Knapsack01(new[] { 1, 1 }, new[] { 1, 2 }, 10));
        Assert.AreEqual(3, Knapsack.Knapsack01(new[] { 9, 1 }, new[] { 1, 2 }, 10));
        Assert.AreEqual(2, Knapsack.Knapsack01(new[] { 10, 1 }, new[] { 1, 2 }, 10));
        Assert.AreEqual(9, Knapsack.Knapsack01(new[] { 8, 3, 3, 3 }, new[] { 8, 3, 3, 3 }, 10));
        Assert.AreEqual(10, Knapsack.Knapsack01(new[] { 4, 4, 3, 3, 3 }, new[] { 4, 4, 3, 3, 3 }, 10));
        Assert.AreEqual(11, Knapsack.Knapsack01(new[] { 4, 4, 3, 3, 3 }, new[] { 4, 4, 3, 4, 3 }, 10));
        Assert.AreEqual(8, Knapsack.Knapsack01(new[] { 4, 4, 3, 3, 3 }, new[] { 4, 4, -2, -1, -3 }, 10));
    }
}
