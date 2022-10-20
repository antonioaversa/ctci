using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

    [TestMethod]
    public void Ex207_CanFinish_EdgeList()
    {
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_EdgeList(2, new[] { new[] { 0, 1 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_EdgeList(2, new[] { new[] { 1, 0 } }));
        Assert.IsFalse(DynamicProgramming.Ex207_CanFinish_EdgeList(2, new[] { new[] { 0, 1 }, new[] { 1, 0 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 2 } }));
        Assert.IsFalse(DynamicProgramming.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 0 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 0, 2 } }));
    }

    [TestMethod]
    public void Ex207_CanFinish_AdjList()
    {
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_AdjList(2, new[] { new[] { 0, 1 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_AdjList(2, new[] { new[] { 1, 0 } }));
        Assert.IsFalse(DynamicProgramming.Ex207_CanFinish_AdjList(2, new[] { new[] { 0, 1 }, new[] { 1, 0 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 2 } }));
        Assert.IsFalse(DynamicProgramming.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 0 } }));
        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 0, 2 } }));

        Assert.IsTrue(DynamicProgramming.Ex207_CanFinish_AdjList(100, new[] {
            new[] {1, 0},new[] {2,0},new[] {2,1},new[] {3,1},new[] {3,2},new[] {4,2},new[] {4,3},new[] {5,3},
            new[] {5,4},new[] {6,4},new[] {6,5},new[] {7,5},new[] {7,6},new[] {8,6},new[] {8,7},new[] {9,7},
            new[] {9,8},new[] {10,8},new[] {10,9},new[] {11,9},new[] {11,10},new[] {12,10},new[] {12,11},
            new[] {13,11},new[] {13,12},new[] {14,12},new[] {14,13},new[] {15,13},new[] {15,14},new[] {16,14},
            new[] {16,15},new[] {17,15},new[] {17,16},new[] {18,16},new[] {18,17},new[] {19,17},new[] {19,18},
            new[] {20,18},new[] {20,19},new[] {21,19},new[] {21,20},new[] {22,20},new[] {22,21},new[] {23,21},
            new[] {23,22},new[] {24,22},new[] {24,23},new[] {25,23},new[] {25,24},new[] {26,24},new[] {26,25},
            new[] {27,25},new[] {27,26},new[] {28,26},new[] {28,27},new[] {29,27},new[] {29,28},new[] {30,28},
            new[] {30,29},new[] {31,29},new[] {31,30},new[] {32,30},new[] {32,31},new[] {33,31},new[] {33,32},
            new[] {34,32},new[] {34,33},new[] {35,33},new[] {35,34},new[] {36,34},new[] {36,35},new[] {37,35},
            new[] {37,36},new[] {38,36},new[] {38,37},new[] {39,37},new[] {39,38},new[] {40,38},new[] {40,39},
            new[] {41,39},new[] {41,40},new[] {42,40},new[] {42,41},new[] {43,41},new[] {43,42},new[] {44,42},
            new[] {44,43},new[] {45,43},new[] {45,44},new[] {46,44},new[] {46,45},new[] {47,45},new[] {47,46},
            new[] {48,46},new[] {48,47},new[] {49,47},new[] {49,48},new[] {50,48},new[] {50,49},new[] {51,49},
            new[] {51,50},new[] {52,50},new[] {52,51},new[] {53,51},new[] {53,52},new[] {54,52},new[] {54,53},
            new[] {55,53},new[] {55,54},new[] {56,54},new[] {56,55},new[] {57,55},new[] {57,56},new[] {58,56},
            new[] {58,57},new[] {59,57},new[] {59,58},new[] {60,58},new[] {60,59},new[] {61,59},new[] {61,60},
            new[] {62,60},new[] {62,61},new[] {63,61},new[] {63,62},new[] {64,62},new[] {64,63},new[] {65,63},
            new[] {65,64},new[] {66,64},new[] {66,65},new[] {67,65},new[] {67,66},new[] {68,66},new[] {68,67},
            new[] {69,67},new[] {69,68},new[] {70,68},new[] {70,69},new[] {71,69},new[] {71,70},new[] {72,70},
            new[] {72,71},new[] {73,71},new[] {73,72},new[] {74,72},new[] {74,73},new[] {75,73},new[] {75,74},
            new[] {76,74},new[] {76,75},new[] {77,75},new[] {77,76},new[] {78,76},new[] {78,77},new[] {79,77},
            new[] {79,78},new[] {80,78},new[] {80,79},new[] {81,79},new[] {81,80},new[] {82,80},new[] {82,81},
            new[] {83,81},new[] {83,82},new[] {84,82},new[] {84,83},new[] {85,83},new[] {85,84},new[] {86,84},
            new[] {86,85},new[] {87,85},new[] {87,86},new[] {88,86},new[] {88,87},new[] {89,87},new[] {89,88},
            new[] {90,88},new[] {90,89},new[] {91,89},new[] {91,90},new[] {92,90},new[] {92,91},new[] {93,91},
            new[] {93,92},new[] {94,92},new[] {94,93},new[] {95,93},new[] {95,94},new[] {96,94},new[] {96,95},
            new[] {97,95},new[] {97,96},new[] {98,96},new[] {98,97},new[] {99,97}}));
    }

    [TestMethod]
    public void Ex210_FindOrder()
    {
        Assert.IsTrue(new[] { 0, 1}.SequenceEqual(
            DynamicProgramming.Ex210_FindOrder(2, new[] { new[] { 1, 0 } })));
        Assert.IsTrue(new[] { 1, 0 }.SequenceEqual(
            DynamicProgramming.Ex210_FindOrder(2, new[] { new[] { 0, 1 } })));
        Assert.IsTrue(new[] { 1, 0, 2 }.SequenceEqual(
            DynamicProgramming.Ex210_FindOrder(3, new[] { new[] { 0, 1 }, new[] { 2, 1 }, new[] { 2, 0 } })));
        Assert.IsTrue(Array.Empty<int>().SequenceEqual(
            DynamicProgramming.Ex210_FindOrder(3, new[] { new[] { 0, 1 }, new[] { 2, 1 }, new[] { 1, 2 } })));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_BfsOnly()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_BfsOnly(4, 
            new[] { new[] { 1, 0 }, new[] { 1, 2}, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_BfsOnly(6, 
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_BfsOnlyOptimized()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_BfsOnlyOptimized(4,
            new[] { new[] { 1, 0 }, new[] { 1, 2 }, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_BfsOnlyOptimized(6,
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_DfsUndirectedTopoSort()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_DfsUndirectedTopoSort(4,
            new[] { new[] { 1, 0 }, new[] { 1, 2 }, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_DfsUndirectedTopoSort(6,
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue(4,
            new[] { new[] { 1, 0 }, new[] { 1, 2 }, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue(6,
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
        Assert.IsTrue(new[] { 0 }.SequenceEqual(
            DynamicProgramming.Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue(1, Array.Empty<int[]>())));
    }

    [TestMethod]
    public void Ex494_FindTargetSumWays()
    {
        Assert.AreEqual(0, DynamicProgramming.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1, 4, 0, 6 }, 8));
        Assert.AreEqual(0, DynamicProgramming.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1, 4, 0 }, 8));
        Assert.AreEqual(5, DynamicProgramming.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1, 5 }, 8));
        Assert.AreEqual(5, DynamicProgramming.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1 }, 3));
        Assert.AreEqual(1, DynamicProgramming.Ex494_FindTargetSumWays(new[] { 1 }, 1));
        Assert.AreEqual(1, DynamicProgramming.Ex494_FindTargetSumWays(new[] { 1 }, -1));
    }
}
