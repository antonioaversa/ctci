using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Text;

namespace CTCI.Tests;

[TestClass]
public class Exercises1Tests
{
    //[TestMethod]
    public void Ex2_Reverse()
    {
        var s = "abcdefghijk\0".ToCharArray();
        Exercises1.Ex2_Reverse(s);
        Trace.WriteLine(new string(s));
    }

    [DataRow("", "")]
    [DataRow("a", "a")]
    [DataRow("aa", "a")]
    [DataRow("ab", "ab")]
    [DataRow("abaabcb", "abc")]
    [DataTestMethod]
    public void Ex3_RemoveDuplicates(string input, string expectedOutput)
    {
        var output = new string(Exercises1.Ex3_RemoveDuplicates(input.ToCharArray()).ToArray());
        Assert.AreEqual(expectedOutput, output);
    }

    [DataRow("", "")]
    [DataRow("a", "a")]
    [DataRow("aa", "a")]
    [DataRow("ab", "ab")]
    [DataRow("abaabcb", "abc")]
    [DataTestMethod]
    public void Ex3_RemoveDuplicatesInPlace(string input, string expectedOutput)
    {
        var inputArray = input.ToCharArray();
        Exercises1.Ex3_RemoveDuplicatesInPlace(inputArray);
        var output = new string(inputArray.TakeWhile(c => c != '\0').ToArray());
        Assert.AreEqual(expectedOutput, output);
    }

    [DataRow("", "", true)]
    [DataRow("a", "a", true)]
    [DataRow("aa", "aa", true)]
    [DataRow("ab", "ba", true)]
    [DataRow("abaabcb", "aaabbbc", true)]
    [DataRow("abaabcb", "aaabbbcd", false)]
    [DataRow("abaabcb", "aaabbbd", false)]
    [DataRow("abaadcb", "aaabbbc", false)]
    [DataTestMethod]
    public void Ex4_AreAnagrams(string s1, string s2, bool expectedOutput)
    {
        Assert.AreEqual(expectedOutput, Exercises1.Ex4_AreAnagrams(s1, s2));
    }

    [DataRow("", "")]
    [DataRow(" ", "%20")]
    [DataRow("a", "a")]
    [DataRow("a a", "a%20a")]
    [DataRow(" ab ", "%20ab%20")]
    [DataRow("  abaa bcb", "%20%20abaa%20bcb")]
    [DataTestMethod]
    public void Ex5_ReplaceSpaces(string s, string expectedOutput)
    {
        Assert.AreEqual(expectedOutput, new string(Exercises1.Ex5_ReplaceSpaces(s).ToArray()));
    }

    [DataRow("", "")]
    [DataRow(" ", "%20")]
    [DataRow("a", "a")]
    [DataRow("a a", "a%20a")]
    [DataRow(" ab ", "%20ab%20")]
    [DataRow("  abaa bcb", "%20%20abaa%20bcb")]
    [DataTestMethod]
    public void Ex5_ReplaceSpacesInPlace(string input, string expectedOutput)
    {
        var inputArray = (input + '\0').ToCharArray();
        Array.Resize(ref inputArray, inputArray.Length * 3);
        Exercises1.Ex5_ReplaceSpacesInPlace(inputArray);
        var output = new string(inputArray.TakeWhile(c => c != '\0').ToArray());
        Assert.AreEqual(expectedOutput, output);

        if (input.Contains(' '))
            Assert.ThrowsException<ArgumentException>(
                () => Exercises1.Ex5_ReplaceSpacesInPlace((input + '\0').ToCharArray()));
    }

    [DataRow(new int[] {}, 0, new int[] {})]
    [DataRow(new[] { 3 }, 1, new[] { 3 })]
    [DataRow(new[] { 1, 2, 3, 4 }, 2, new[] { 4, 1, 2, 3 })]
    [DataRow(new[] { 1, 2, 3, 8, 9, 4, 7, 6, 5 }, 3, new[] { 7, 8, 1, 6, 9, 2, 5, 4, 3 })]
    [DataRow(new[] { 1, 2, 3, 4, 12, 13, 14, 5, 11, 16, 15, 6, 10, 9, 8, 7 }, 4, 
        new[] { 10, 11, 12, 1, 9, 16, 13, 2, 8, 15, 14, 3, 7, 6, 5, 4 })]
    [DataTestMethod]
    public void Ex6_RotateMatrix(int[] input, int n, int[] expectedOutput)
    {
        var matrix = BuildMatrix(input, n);
        var expectedOutputMatrix = BuildMatrix(expectedOutput, n);
        Exercises1.Ex6_RotateMatrix(matrix);
        Assert.IsTrue(AreEqual(expectedOutputMatrix, matrix), 
            $"Expected:\n {MatrixToString(expectedOutputMatrix)}\nActual:\n{MatrixToString(matrix)}");
    }

    private static int[,] BuildMatrix(int[] values, int n)
    {
        var matrix = new int[n, n];
        for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                matrix[i, j] = values[i * n + j];
        return matrix;
    }

    private static bool AreEqual(int[,] m1, int[,] m2)
    {
        if (m1.GetLength(0) != m2.GetLength(0))
            return false;
        if (m1.GetLength(1) != m2.GetLength(1))
            return false;

        for (var i = 0; i < m1.GetLength(0); i++)
            for (var j = 0; j < m1.GetLength(1); j++)
                if (m1[i, j] != m1[i, j])
                    return false;

        return true;
    }

    private static string MatrixToString(int[,] m)
    {
        var result = new StringBuilder();
        for (var i = 0; i < m.GetLength(0); i++)
        {
            for (var j = 0; j < m.GetLength(1); j++)
            {
                result.Append(m[i, j]);
                result.Append(' ');
            }
            result.AppendLine();
        }

        return result.ToString();
    }

    [DataRow(1, new int[] { }, 0, new int[] { })]
    [DataRow(2, new[] { 3 }, 1, new[] { 3 })]
    [DataRow(3, new[] { 1, 0, 3, 4 }, 2, new[] { 0, 0, 3, 0 })]
    [DataRow(4, new[] { 1, 2, 0, 4 }, 2, new[] { 0, 2, 0, 0 })]
    [DataRow(5, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 3, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [DataRow(6, new[] { 1, 2, 3, 4, 0, 6, 7, 8, 9 }, 3, new[] { 1, 0, 3, 0, 0, 0, 7, 0, 9 })]
    [DataRow(7, new[] { 0, 2, 3, 4, 5, 6, 7, 8, 9 }, 3, new[] { 0, 0, 0, 0, 5, 6, 0, 8, 9 })]
    [DataRow(8, new[] { 1, 0, 3, 4, 5, 6, 7, 8, 9 }, 3, new[] { 0, 0, 0, 4, 0, 6, 7, 0, 9 })]
    [DataRow(9, new[] { 0, 2, 3, 4, 5, 6, 7, 8, 0 }, 3, new[] { 0, 0, 0, 0, 5, 0, 0, 0, 0 })]
    [DataRow(10, new[] { 0, 2, 3, 0, 5, 6, 7, 8, 9 }, 3, new[] { 0, 0, 0, 0, 0, 0, 0, 8, 9 })]
    [DataRow(11,
        new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 4,
        new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
    [DataRow(12,
        new[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 4,
        new[] { 0, 0, 0, 0, 0, 6, 7, 8, 0, 10, 11, 12, 0, 14, 15, 16 })]
    [DataRow(13,
        new[] { 1, 2, 3, 4, 5, 0, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 4,
        new[] { 1, 0, 3, 4, 0, 0, 0, 0, 9, 0, 11, 12, 13, 0, 15, 16 })]
    [DataRow(14,
        new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 12, 13, 14, 15, 16 }, 4,
        new[] { 1, 0, 0, 4, 5, 0, 0, 8, 0, 0, 0, 0, 13, 0, 0, 16 })]
    [DataRow(15,
        new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 12, 13, 14, 15, 16 }, 4,
        new[] { 1, 2, 0, 4, 5, 6, 0, 8, 0, 0, 0, 0, 13, 14, 0, 16 })]
    [DataTestMethod]
    public void Ex7_ResetRowAndColumn(int id, int[] input, int n, int[] expectedOutput)
    {
        var matrix = BuildMatrix(input, n);
        var expectedOutputMatrix = BuildMatrix(expectedOutput, n);
        Exercises1.Ex7_ResetRowAndColumn(matrix);
        Assert.IsTrue(AreEqual(expectedOutputMatrix, matrix),
            $"Id: {id}\n, Expected:\n {MatrixToString(expectedOutputMatrix)}\nActual:\n{MatrixToString(matrix)}");
    }

    [DataRow("", "", true)]
    [DataRow("a", "a", true)]
    [DataRow("aa", "aa", true)]
    [DataRow("aab", "aba", true)]
    [DataRow("aab", "baa", true)]
    [DataRow("aab", "bba", false)]
    [DataRow("aabab", "baaba", true)]
    [DataRow("aabaa", "bbaaa", false)]
    [DataTestMethod]
    public void Ex8_IsRotation(string s1, string s2, bool expectedOutput)
    {
        Assert.AreEqual(expectedOutput, Exercises1.Ex8_IsRotation(s1, s2));
    }
}