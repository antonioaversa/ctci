using CTCI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI.Tests;

[TestClass]
public class DynamicProgrammingTests
{
    [TestMethod]
    public void Ex5_LongestPalindrome()
    {
        Assert.AreEqual("bbb", DynamicProgramming.Ex5_LongestPalindrome("bbb"));
        Assert.AreEqual("bbb", DynamicProgramming.Ex5_LongestPalindrome("abbb"));
        Assert.AreEqual("bbb", DynamicProgramming.Ex5_LongestPalindrome("bbba"));
        Assert.AreEqual("aaaa", DynamicProgramming.Ex5_LongestPalindrome("bbbaaaacbb"));
        Assert.AreEqual("bbbaaaabbb", DynamicProgramming.Ex5_LongestPalindrome("bbbaaaabbb"));
        Assert.AreEqual("bbbb", DynamicProgramming.Ex5_LongestPalindrome("bbbb"));
        Assert.AreEqual("bab", DynamicProgramming.Ex5_LongestPalindrome("babad"));
        Assert.AreEqual("bb", DynamicProgramming.Ex5_LongestPalindrome("cbbd"));
    }

    [TestMethod]
    public void Ex22_GenerateParenthesis()
    {
        Assert.IsTrue(new HashSet<string> {
            "()()()", "()(())", "(())()", "(()())", "((()))" }
            .SetEquals(DynamicProgramming.Ex22_GenerateParenthesis(3)));

        Assert.IsTrue(new HashSet<string> { 
            "()()()()", "()()(())", "()(())()", "()(()())", "()((()))", "(())()()", "(())(())", "(()())()", "(()()())", 
            "(()(()))", "((()))()", "((())())", "((()()))", "(((())))" }
            .SetEquals(DynamicProgramming.Ex22_GenerateParenthesis(4)));
    }

    [TestMethod]
    public void Ex36_ValidSudoku()
    {
        var board = new char[][]
        {
            new char[]{'5', '3', '.', '.', '7', '.', '.', '.', '.'},
            new char[]{'6', '.', '.', '1', '9', '5', '.', '.', '.'},
            new char[]{'.', '9', '8', '.', '.', '.', '.', '6', '.'},
            new char[]{'8', '.', '.', '.', '6', '.', '.', '.', '3'},
            new char[]{'4', '.', '.', '8', '.', '3', '.', '.', '1'},
            new char[]{'7', '.', '.', '.', '2', '.', '.', '.', '6'},
            new char[]{'.', '6', '.', '.', '.', '.', '2', '8', '.'},
            new char[]{'.', '.', '.', '4', '1', '9', '.', '.', '5'},
            new char[]{'.', '.', '.', '.', '8', '.', '.', '7', '9'},
        };

        Assert.IsTrue(DynamicProgramming.Ex36_ValidSudoku(board));
    }

    [TestMethod]
    public void Ex45_Jump()
    {
        Assert.AreEqual(2, DynamicProgramming.Ex45_Jump(new int[] { 2, 3, 1, 9, 4 }));
        Assert.AreEqual(4, DynamicProgramming.Ex45_Jump(new int[] { 2, 3, 1, 1, 1, 9, 4 }));
        Assert.AreEqual(2, DynamicProgramming.Ex45_Jump(new int[] { 2, 3, 0, 1, 4 }));
    }

    [TestMethod]
    public void Ex45_JumpFaster()
    {
        Assert.AreEqual(2, DynamicProgramming.Ex45_JumpFaster(new int[] { 2, 3, 1, 9, 4 }));
        Assert.AreEqual(4, DynamicProgramming.Ex45_JumpFaster(new int[] { 2, 3, 1, 1, 1, 9, 4 }));
        Assert.AreEqual(2, DynamicProgramming.Ex45_JumpFaster(new int[] { 2, 3, 0, 1, 4 }));
    }

    [TestMethod]
    public void Ex45_JumpFastest()
    { 
        Assert.AreEqual(2, DynamicProgramming.Ex45_JumpFastest(new int[] { 2, 3, 1, 9, 4 }));
        Assert.AreEqual(4, DynamicProgramming.Ex45_JumpFastest(new int[] { 2, 3, 1, 1, 1, 9, 4 }));
        Assert.AreEqual(2, DynamicProgramming.Ex45_JumpFastest(new int[] { 2, 3, 0, 1, 4 }));
    }

    [TestMethod]
    public void Ex53_MaximumSubarray()
    {
        Assert.AreEqual(10, DynamicProgramming.Ex53_MaximumSubarray_Quadratic(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
        Assert.AreEqual(6, DynamicProgramming.Ex53_MaximumSubarray_Quadratic(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
        Assert.AreEqual(1, DynamicProgramming.Ex53_MaximumSubarray_Quadratic(new int[] { 1 }));
        Assert.AreEqual(23, DynamicProgramming.Ex53_MaximumSubarray_Quadratic(new int[] { 5, 4, -1, 7, 8 }));
        Assert.AreEqual(10, DynamicProgramming.Ex53_MaximumSubarray_Quadratic(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
    }

    [TestMethod]
    public void Ex53_MaximumSubarray_Linearithmic()
    {
        Assert.AreEqual(10, DynamicProgramming.Ex53_MaximumSubarray_Kadane(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
        Assert.AreEqual(6, DynamicProgramming.Ex53_MaximumSubarray_Kadane(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
        Assert.AreEqual(1, DynamicProgramming.Ex53_MaximumSubarray_Kadane(new int[] { 1 }));
        Assert.AreEqual(23, DynamicProgramming.Ex53_MaximumSubarray_Kadane(new int[] { 5, 4, -1, 7, 8 }));
        Assert.AreEqual(10, DynamicProgramming.Ex53_MaximumSubarray_Kadane(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
        Assert.AreEqual(8, DynamicProgramming.Ex53_MaximumSubarray_Kadane(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -10, -2, 8, -6, 6 }));
        Assert.AreEqual(-1, DynamicProgramming.Ex53_MaximumSubarray_Kadane(new int[] { -2, -1 }));
        Assert.AreEqual(-1, DynamicProgramming.Ex53_MaximumSubarray_Kadane(new int[] { -1, -2 }));
    }

    [TestMethod]
    public void Ex131_PalindromePartition()
    {
        var partitions1 = DynamicProgramming.Ex131_PalindromePartition("aab");
        Assert.IsTrue(partitions1.Count == 2);
        Assert.AreEqual(1, partitions1.Count(p => p.SequenceEqual(new[] { "a", "a", "b" })));
        Assert.AreEqual(1, partitions1.Count(p => p.SequenceEqual(new[] { "aa", "b" })));

        var partitions2 = DynamicProgramming.Ex131_PalindromePartition("xxyxx");
        Assert.IsTrue(partitions2.Count == 6);
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "x", "x", "y", "x", "x" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "x", "x", "y", "xx" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "x", "xyx", "x" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "xx", "y", "x", "x" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "xx", "y", "xx" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "xxyxx" })));
    }
}
