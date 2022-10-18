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
}
