using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CTCI.Tests;

[TestClass]
public class Exercises9Tests
{
    [TestMethod]
    public void Ex1_Merge()
    {
        var a1 = new[] { 1, 4, 5, 7, -1, -1, -1, -1 };
        var b1 = new[] { 0, 2, 3, 9 };
        Exercises9.Ex1_Merge(a1, b1, 4);
        Assert.IsTrue(new[] { 0, 1, 2, 3, 4, 5, 7, 9 }.SequenceEqual(a1));

        var a2 = new[] { 1, 4, 5, 7, -1, -1, -1, -1, -1, -1 };
        var b2 = new[] { 0, 2, 3, 9 };
        Exercises9.Ex1_Merge(a2, b2, 4);
        Assert.IsTrue(new[] { 0, 1, 2, 3, 4, 5, 7, 9, -1, -1 }.SequenceEqual(a2));
    }

    [TestMethod]
    public void Ex2_SortAnagrams()
    {
        var a1 = new[] { "abc", "bac", "cab", "ccb", "cbc", "baa" };
        Exercises9.Ex2_SortAnagrams(a1);
        Assert.IsTrue(new[] { "baa", "abc", "bac", "cab", "ccb", "cbc" }.SequenceEqual(a1));
    }

    [TestMethod]
    public void Ex3_Find()
    {
        var a = new[] { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 };
        Assert.AreEqual(8, Exercises9.Ex3_Find(a, 5));
        Assert.AreEqual(0, Exercises9.Ex3_Find(a, 15));
        Assert.AreEqual(11, Exercises9.Ex3_Find(a, 14));
    }

    [TestMethod]
    public void Ex5_Find_WithInterspersed()
    {
        var a1 = new[] { "at", "", "", "", "ball", "", "", "car", "", "", "dad", "", "" };
        Assert.AreEqual(4, Exercises9.Ex5_Find_WithInterspersed(a1, "ball"));
        Assert.AreEqual(0, Exercises9.Ex5_Find_WithInterspersed(a1, "at"));
        Assert.AreEqual(10, Exercises9.Ex5_Find_WithInterspersed(a1, "dad"));

        var a2 = new[] { "at", "", "", "", "", "ball", "car", "", "", "dad", "", "" };
        Assert.AreEqual(-1, Exercises9.Ex5_Find_WithInterspersed(a2, "ballcar"));
    }

    [TestMethod]
    public void Ex5_Find_WithoutAuxArrays()
    {
        var a1 = new[] { "at", "", "", "", "ball", "", "", "car", "", "", "dad", "", "" };
        Assert.AreEqual(4, Exercises9.Ex5_Find_WithoutAuxArrays(a1, "ball"));
        Assert.AreEqual(0, Exercises9.Ex5_Find_WithoutAuxArrays(a1, "at"));
        Assert.AreEqual(10, Exercises9.Ex5_Find_WithoutAuxArrays(a1, "dad"));

        var a2 = new[] { "at", "", "", "", "", "ball", "car", "", "", "dad", "", "" };
        Assert.AreEqual(-1, Exercises9.Ex5_Find_WithoutAuxArrays(a2, "ballcar"));

        var a3 = new[] { "", "", "", "", "", "ball", "car", "", "", "dad", "", "" };
        Assert.AreEqual(5, Exercises9.Ex5_Find_WithoutAuxArrays(a3, "ball"));

        var a4 = new[] { "", "", "", "", "", "ball", "car", "", "", "dad", "", "egon" };
        Assert.AreEqual(11, Exercises9.Ex5_Find_WithoutAuxArrays(a4, "egon"));
    }

    [TestMethod]
    public void Ex6_Find_InSortedMatrix()
    {
        var m1 = new int[3, 6]
        {
            { 1, 6, 7, 9, 10, 10 },
            { 2, 7, 10, 11, 12, 13 },
            { 5, 9, 11, 20, 22, 22 }
        };

        Assert.IsTrue(
            new List<(int, int)>{ (0, 0) }.Contains(Exercises9.Ex6_Find_InSortedMatrix(m1, 1)));
        Assert.IsTrue(
            new List<(int, int)>{ (0, 2), (1, 1) }.Contains(Exercises9.Ex6_Find_InSortedMatrix(m1, 7)));
        Assert.IsTrue(
            new List<(int, int)>{ (1, 3), (2, 2) }.Contains(Exercises9.Ex6_Find_InSortedMatrix(m1, 11)));
        Assert.IsTrue(
            new List<(int, int)>{ (2, 3) }.Contains(Exercises9.Ex6_Find_InSortedMatrix(m1, 20)));
        Assert.IsTrue(
            new List<(int, int)> { (0, 4), (0, 5), (1, 2) }.Contains(Exercises9.Ex6_Find_InSortedMatrix(m1, 10)));

        Assert.AreEqual((-1, -1), Exercises9.Ex6_Find_InSortedMatrix(m1, 4));
    }

    [TestMethod]
    public void Ex6_Elimination()
    {
        var m1 = new int[3, 6]
        {
            { 1, 6, 7, 9, 10, 10 },
            { 2, 7, 10, 11, 12, 13 },
            { 5, 9, 11, 20, 22, 22 }
        };

        Assert.IsTrue(
            new List<(int, int)> { (0, 0) }.Contains(Exercises9.Ex6_Elimination(m1, 1)));
        Assert.IsTrue(
            new List<(int, int)> { (0, 2), (1, 1) }.Contains(Exercises9.Ex6_Elimination(m1, 7)));
        Assert.IsTrue(
            new List<(int, int)> { (1, 3), (2, 2) }.Contains(Exercises9.Ex6_Elimination(m1, 11)));
        Assert.IsTrue(
            new List<(int, int)> { (2, 3) }.Contains(Exercises9.Ex6_Elimination(m1, 20)));
        Assert.IsTrue(
            new List<(int, int)> { (0, 4), (0, 5), (1, 2) }.Contains(Exercises9.Ex6_Elimination(m1, 10)));

        Assert.AreEqual((-1, -1), Exercises9.Ex6_Elimination(m1, 4));
    }

    [TestMethod]
    public void Ex7_HighestTower()
    {
        Assert.AreEqual(6, Exercises9.Ex7_HighestTower(
            new List<(int, int)> { (65, 100), (70, 150), (56, 90), (75, 190), (60, 95), (68, 110) }));
    }


    [TestMethod]
    public void Ex7_HighestTowerOptimized()
    {
        Assert.AreEqual(6, Exercises9.Ex7_HighestTowerOptimized(
            new List<(int, int)> { (65, 100), (70, 150), (56, 90), (75, 190), (60, 95), (68, 110) }));
    }

    [TestMethod]
    public void Ex7_HighestTowerDynamicProgramming()
    {
        Assert.AreEqual(6, Exercises9.Ex7_HighestTowerDynamicProgramming(
            new List<(int, int)> { (65, 100), (70, 150), (56, 90), (75, 190), (60, 95), (68, 110) }));

        Assert.AreEqual(4, Exercises9.Ex7_HighestTowerDynamicProgramming(
            new List<(int, int)> { (1, 1), (1, 2), (2, 6), (2, 1), (3, 3), (3, 6), (4, 4), (4, 3), (4, 6), (5, 5) }));
    }
}
