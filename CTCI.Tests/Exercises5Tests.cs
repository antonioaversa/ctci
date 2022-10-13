using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI.Tests;

[TestClass]
public class Exercises5Tests
{
    [TestMethod]
    public void Ex1()
    {
        Assert.AreEqual(0b10001010100, Exercises5.Ex1(0b10000000000, 0b10101, 2, 6));
    }

    [TestMethod]
    public void Ex2()
    {
        Assert.AreEqual("1", Exercises5.Ex2("1"));
        Assert.AreEqual("10", Exercises5.Ex2("2"));
        Assert.AreEqual("11.1", Exercises5.Ex2("3.5"));
        Assert.AreEqual("1000000.01", Exercises5.Ex2("64.25"));
        Assert.AreEqual("10100.11", Exercises5.Ex2("20.75"));
        Assert.AreEqual("ERROR", Exercises5.Ex2("3.72"));
    }

    [TestMethod]
    public void Ex3()
    {
        Assert.AreEqual((int.MinValue, 0b10), Exercises5.Ex3(0b1));
        Assert.AreEqual((0b01, 0b100), Exercises5.Ex3(0b10));
        Assert.AreEqual((int.MinValue, 0b101), Exercises5.Ex3(0b11));
        Assert.AreEqual((0b011, 0b110), Exercises5.Ex3(0b101));
        Assert.AreEqual((0b01111, 0b11011), Exercises5.Ex3(0b10111));
    }

    [TestMethod]
    public void Ex4()
    {
        Assert.IsTrue(Exercises5.Ex4(0));
        Assert.IsTrue(Exercises5.Ex4(1));
        Assert.IsTrue(Exercises5.Ex4(2));
        Assert.IsFalse(Exercises5.Ex4(3));
        Assert.IsTrue(Exercises5.Ex4(4));
        Assert.IsTrue(Exercises5.Ex4(8));
        Assert.IsFalse(Exercises5.Ex4(15));
        Assert.IsTrue(Exercises5.Ex4(16));
        Assert.IsFalse(Exercises5.Ex4(17));
    }

    [TestMethod]
    public void Ex5()
    {
        Assert.AreEqual(2, Exercises5.Ex5(31, 14));
        Assert.AreEqual(1, Exercises5.Ex5(0b110, 0b111));
        Assert.AreEqual(3, Exercises5.Ex5(0b10010, 0b11001));
        Assert.AreEqual(5, Exercises5.Ex5(0b00000, 0b11111));
    }

    [TestMethod]
    public void Ex6()
    {
        Assert.AreEqual(0b11011000, Exercises5.Ex6(0b11100100));
    }

    [TestMethod]
    public void Ex7()
    {
        Assert.AreEqual(3, Exercises5.Ex7(3, (i, j) => j < 2 ? new byte[] { 0, 0, 0, 1, 1, 0 }[i * 2 + j] : (byte)0));
        Assert.AreEqual(2, Exercises5.Ex7(3, (i, j) => j < 2 ? new byte[] { 0, 0, 1, 1, 1, 0 }[i * 2 + j] : (byte)0));
        Assert.AreEqual(0, Exercises5.Ex7(3, (i, j) => j < 2 ? new byte[] { 0, 1, 1, 0, 1, 1 }[i * 2 + j] : (byte)0));
        Assert.AreEqual(1, Exercises5.Ex7(3, (i, j) => j < 2 ? new byte[] { 0, 0, 0, 1, 1, 1 }[i * 2 + j] : (byte)0));

        Assert.AreEqual(7, Exercises5.Ex7(7, (i, j) => j < 3 ? new byte[] { 
            0, 0, 0, 
            0, 0, 1, 
            0, 1, 0, 
            0, 1, 1, 
            1, 0, 0, 
            1, 0, 1, 
            1, 1, 0 }[i * 3 + j] : (byte)0));
        Assert.AreEqual(5, Exercises5.Ex7(7, (i, j) => j < 3 ? new byte[] { 
            0, 0, 0, 
            0, 0, 1, 
            0, 1, 0, 
            0, 1, 1, 
            1, 0, 0, 
            1, 1, 0, 
            1, 1, 1 }[i * 3 + j] : (byte)0));

        Assert.AreEqual(5, Exercises5.Ex7(6, (i, j) => j < 3 ? new byte[] {
            0, 0, 0,
            0, 0, 1,
            0, 1, 0,
            0, 1, 1,
            1, 0, 0,
            1, 1, 0 }[i * 3 + j] : (byte)0));

        Assert.AreEqual(5, Exercises5.Ex7(5, (i, j) => j < 3 ? new byte[] {
            0, 0, 0,
            1, 0, 0,
            0, 1, 0,
            1, 1, 0,
            0, 0, 1 }[i * 3 + j] : (byte)0));
    }
}
