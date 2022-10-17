using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Markup;

namespace CTCI.Tests;

[TestClass]
public class Exercises8Tests
{
    [TestMethod]
    public void Ex1_Fibonacci()
    {
        Assert.AreEqual(1, Exercises8.Ex1_Fibonacci(1));
        Assert.AreEqual(2, Exercises8.Ex1_Fibonacci(2));
        Assert.AreEqual(3, Exercises8.Ex1_Fibonacci(3));
        Assert.AreEqual(5, Exercises8.Ex1_Fibonacci(4));
        Assert.AreEqual(8, Exercises8.Ex1_Fibonacci(5));
    }

    [TestMethod]
    public void Ex1_FibonacciIterative()
    {
        Assert.AreEqual(1, Exercises8.Ex1_FibonacciIterative(1));
        Assert.AreEqual(2, Exercises8.Ex1_FibonacciIterative(2));
        Assert.AreEqual(3, Exercises8.Ex1_FibonacciIterative(3));
        Assert.AreEqual(5, Exercises8.Ex1_FibonacciIterative(4));
        Assert.AreEqual(8, Exercises8.Ex1_FibonacciIterative(5));
    }

    [TestMethod]
    public void Ex2_Count()
    {
        Assert.AreEqual(1, Exercises8.Ex2_Count(1));
        Assert.AreEqual(2, Exercises8.Ex2_Count(2));
        Assert.AreEqual(6, Exercises8.Ex2_Count(3));
        Assert.AreEqual(20, Exercises8.Ex2_Count(4));
    }

    [TestMethod]
    public void Ex2_CountWithOffLimits()
    {
        Assert.AreEqual(1, Exercises8.Ex2_CountWithOffLimits(1, (i, j) => false));
        Assert.AreEqual(0, Exercises8.Ex2_CountWithOffLimits(1, (i, j) => true));
        Assert.AreEqual(2, Exercises8.Ex2_CountWithOffLimits(2, (i, j) => false));
        Assert.AreEqual(0, Exercises8.Ex2_CountWithOffLimits(2, (i, j) => true));
        Assert.AreEqual(1, Exercises8.Ex2_CountWithOffLimits(2, (i, j) => (i == 1 && j == 2)));
        Assert.AreEqual(1, Exercises8.Ex2_CountWithOffLimits(2, (i, j) => (i == 2 && j == 1)));
        Assert.AreEqual(6, Exercises8.Ex2_CountWithOffLimits(3, (i, j) => false));
        Assert.AreEqual(3, Exercises8.Ex2_CountWithOffLimits(3, (i, j) => (i == 1 && j == 2)));
        Assert.AreEqual(3, Exercises8.Ex2_CountWithOffLimits(3, (i, j) => (i == 2 && j == 1)));
        Assert.AreEqual(3, Exercises8.Ex2_CountWithOffLimits(3, (i, j) => (i == 1 && (j == 2 || j == 3))));
        Assert.AreEqual(0, Exercises8.Ex2_CountWithOffLimits(3, (i, j) => (i == 2 && (j == 1 || j == 2 || j == 3))));
        Assert.AreEqual(0, Exercises8.Ex2_CountWithOffLimits(3, (i, j) => (i == 3 && (j == 1 || j == 2 || j == 3))));
    }

    [TestMethod]
    public void Ex2_PathsWithOffLimits()
    {
        // _ x _
        // _ _ _
        // _ _ _
        var paths = Exercises8.Ex2_PathsWithOffLimits(3, (i, j) => (i == 1 && j == 2));
        Assert.IsTrue(paths.Count(p => p.SequenceEqual(new[] { (1, 1), (2, 1), (2, 2), (2, 3), (3, 3) })) == 1);
        Assert.IsTrue(paths.Count(p => p.SequenceEqual(new[] { (1, 1), (2, 1), (2, 2), (3, 2), (3, 3) })) == 1);
        Assert.IsTrue(paths.Count(p => p.SequenceEqual(new[] { (1, 1), (2, 1), (3, 1), (3, 2), (3, 3) })) == 1);
    }

    [TestMethod]
    public void Ex3_Subsets()
    {
        var set = new HashSet<int> { 1, 2, 3 };
        var subsets = Exercises8.Ex3_Subsets(set).ToList();
        Assert.AreEqual(8, subsets.Count);
        Assert.IsTrue(subsets.Count(s => s.SetEquals(new HashSet<int> { 1, 2, 3 })) == 1);
        Assert.IsTrue(subsets.Count(s => s.SetEquals(new HashSet<int> { })) == 1);
        Assert.IsTrue(subsets.Count(s => s.SetEquals(new HashSet<int> { 2, 3 })) == 1);
    }

    [TestMethod]
    public void Ex3_SubsetsIterative()
    {
        var set = new HashSet<int> { 1, 2, 3 };
        var subsets = Exercises8.Ex3_SubsetsIterative(set).ToList();
        Assert.AreEqual(8, subsets.Count);
        Assert.IsTrue(subsets.Count(s => s.SetEquals(new HashSet<int> { 1, 2, 3 })) == 1);
        Assert.IsTrue(subsets.Count(s => s.SetEquals(new HashSet<int> { })) == 1);
        Assert.IsTrue(subsets.Count(s => s.SetEquals(new HashSet<int> { 2, 3 })) == 1);
    }

    [TestMethod]
    public void Ex4_AllPermutations()
    {
        Assert.IsTrue(Exercises8.Ex4_AllPermutations("abc").ToHashSet().SetEquals(
            new HashSet<string> { "abc", "acb", "bac", "bca", "cab", "cba" }));
    }

    [TestMethod]
    public void Ex5_AllCombinationsOfParenthesesWithTwoStacks()
    {
        var combinations1 = Exercises8.Ex5_AllCombinationsOfParenthesesWithTwoStacks(3).ToList();
        Assert.IsTrue(combinations1.ToHashSet().SetEquals(
            new HashSet<string> { "()()()", "(())()", "(()())", "()(())", "((()))" }));
    }

    [TestMethod]
    public void Ex5_AllCombinationsOfParenthesesConstructive()
    {
        var combinations1 = Exercises8.Ex5_AllCombinationsOfParenthesesConstructive(3).ToList();
        Assert.IsTrue(combinations1.ToHashSet().SetEquals(
            new HashSet<string> { "()()()", "(())()", "(()())", "()(())", "((()))" }));
    }

    [TestMethod]
    public void Ex5_AllCombinationsOfParenthesesChoices()
    {
        var combinations1 = Exercises8.Ex5_AllCombinationsOfParenthesesChoices(3).ToList();
        Assert.IsTrue(combinations1.ToHashSet().SetEquals(
            new HashSet<string> { "()()()", "(())()", "(()())", "()(())", "((()))" }));
    }

    [TestMethod]
    public void Ex6_Paint()
    {
        var img1 = StringToImg(@"
            11111
            11001
            00111
            00000
            11000");
        Exercises8.Ex6_Paint(img1, 0, 0, '2');
        Assert.AreEqual(Normalize(@"
            22222
            22002
            00222
            00000
            11000"), Normalize(ImgToString(img1)));
        Exercises8.Ex6_Paint(img1, 1, 2, '3');
        Assert.AreEqual(Normalize(@"
            22222
            22332
            00222
            00000
            11000"), Normalize(ImgToString(img1)));
        Exercises8.Ex6_Paint(img1, 4, 4, '4');
        Assert.AreEqual(Normalize(@"
            22222
            22332
            44222
            44444
            11444"), Normalize(ImgToString(img1)));

        var img2 = StringToImg(@"
            1111101
            1000101
            0011111
            0000001
            1110001
            1010000
            1110000");
        Exercises8.Ex6_Paint(img2, 1, 4, '2');
        Assert.AreEqual(Normalize(@"
            2222202
            2000202
            0022222
            0000002
            1110002
            1010000
            1110000"), Normalize(ImgToString(img2)));
    }

    private static char[,] StringToImg(string s)
    {
        s = Normalize(s);
        var n = (int)Math.Sqrt(s.Length);
        var result = new char[n, n];
        for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                result[i, j] = s[i * n + j];
        return result;
    }

    private static string Normalize(string s) =>
        s.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(" ", "");

    private static string ImgToString(char[,] img)
    {
        var result = new StringBuilder();
        for (var i = 0; i < img.GetLength(0); i++)
        {
            for (var j = 0; j < img.GetLength(1); j++)
                result.Append(img[i, j]);
            result.AppendLine();
        }

        return result.ToString();
    }

    [TestMethod]
    public void Ex7_NCents()
    {
        Assert.AreEqual(1, Exercises8.Ex7_NCents(1));
        Assert.AreEqual(1, Exercises8.Ex7_NCents(2));
        Assert.AreEqual(1, Exercises8.Ex7_NCents(4));
        Assert.AreEqual(2, Exercises8.Ex7_NCents(5));
        Assert.AreEqual(2, Exercises8.Ex7_NCents(7));
        Assert.AreEqual(4, Exercises8.Ex7_NCents(10));
        Assert.AreEqual(4, Exercises8.Ex7_NCents(14));
        Assert.AreEqual(6, Exercises8.Ex7_NCents(15));
        Assert.AreEqual(9, Exercises8.Ex7_NCents(20));
    }

    [TestMethod]
    public void Ex8_NQueens()
    {
        var n = 8;
        var permutations = Exercises8.Ex8_NQueens(n);
        Assert.IsTrue(permutations.All(p => p.Count == n));
        Assert.IsTrue(permutations.All(p => 
            p.Select(c => c.Item1).Distinct().Count() == n &&
            p.Select(c => c.Item2).Distinct().Count() == n &&
            p.All(c1 => p.All(c2 => c1 == c2 || c1.Item1 - c1.Item2 != c2.Item1 - c2.Item2) &&
            p.All(c1 => p.All(c2 => c1 == c2 || c2.Item1 + c2.Item2 != c1.Item1 + c1.Item2)))));
    }
}
