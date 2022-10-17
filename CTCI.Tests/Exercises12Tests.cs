using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace CTCI.Tests;

[TestClass]
public class Exercises12Tests
{
    [TestMethod]
    public void Ex1_LastKLines()
    {
        Action<string> action = 
            (tempFilePath) => 
            {
                var lines = Exercises12.Ex1_LastKLines(tempFilePath, 8)
                    .Select(l => int.Parse(l, CultureInfo.InvariantCulture))
                    .Reverse()
                    .ToList();
                Assert.IsTrue(lines.Count <= 8);
                Assert.IsTrue(Enumerable.Range(0, 8).Zip(lines).All(c => c.First == c.Second));
            };
        GenerateRandomFileAndRun(action, 0);
        GenerateRandomFileAndRun(action, 3);
        GenerateRandomFileAndRun(action, 8);
        GenerateRandomFileAndRun(action, 40);
    }

    private static void GenerateRandomFileAndRun(Action<string> action, int numberOfLines)
    {
        var tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName().Replace(".", ""));
        try
        {
            File.WriteAllLines(tempFilePath, Enumerable.Range(0, numberOfLines).Reverse().Select(i => i.ToString()));
            action(tempFilePath);
        }
        finally
        {
            File.Delete(tempFilePath);
        }
    }
}
