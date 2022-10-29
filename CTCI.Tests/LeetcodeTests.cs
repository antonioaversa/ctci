using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CTCI.Tests;

using static CTCI.Leetcode;

[TestClass]
public class LeetcodeTests
{
    [TestMethod]
    public void Ex3_LengthOfLongestSubstring()
    {
        Assert.AreEqual(4, Leetcode.Ex3_LengthOfLongestSubstring("abcabcbbcdeecddef"));
        Assert.AreEqual(3, Leetcode.Ex3_LengthOfLongestSubstring("abcabcbb"));
        Assert.AreEqual(0, Leetcode.Ex3_LengthOfLongestSubstring(""));
        Assert.AreEqual(1, Leetcode.Ex3_LengthOfLongestSubstring("a"));
        Assert.AreEqual(1, Leetcode.Ex3_LengthOfLongestSubstring("aaaaaaaaa"));
    }

    [TestMethod]
    public void Ex5_LongestPalindrome()
    {
        Assert.AreEqual("bbb", Leetcode.Ex5_LongestPalindrome("bbb"));
        Assert.AreEqual("bbb", Leetcode.Ex5_LongestPalindrome("abbb"));
        Assert.AreEqual("bbb", Leetcode.Ex5_LongestPalindrome("bbba"));
        Assert.AreEqual("aaaa", Leetcode.Ex5_LongestPalindrome("bbbaaaacbb"));
        Assert.AreEqual("bbbaaaabbb", Leetcode.Ex5_LongestPalindrome("bbbaaaabbb"));
        Assert.AreEqual("bbbb", Leetcode.Ex5_LongestPalindrome("bbbb"));
        Assert.AreEqual("bab", Leetcode.Ex5_LongestPalindrome("babad"));
        Assert.AreEqual("bb", Leetcode.Ex5_LongestPalindrome("cbbd"));
    }

    [TestMethod]
    public void Ex22_GenerateParenthesis()
    {
        Assert.IsTrue(new HashSet<string> {
            "()()()", "()(())", "(())()", "(()())", "((()))" }
            .SetEquals(Leetcode.Ex22_GenerateParenthesis(3)));

        Assert.IsTrue(new HashSet<string> { 
            "()()()()", "()()(())", "()(())()", "()(()())", "()((()))", "(())()()", "(())(())", "(()())()", "(()()())", 
            "(()(()))", "((()))()", "((())())", "((()()))", "(((())))" }
            .SetEquals(Leetcode.Ex22_GenerateParenthesis(4)));
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

        Assert.IsTrue(Leetcode.Ex36_ValidSudoku(board));
    }

    [TestMethod]
    public void Ex41_FirstMissingPositive()
    {
        Assert.AreEqual(3, Leetcode.Ex41_FirstMissingPositive(new[] { 1, 2 }));
        Assert.AreEqual(3, Leetcode.Ex41_FirstMissingPositive(new[] { 1, 2, 4 }));
        Assert.AreEqual(3, Leetcode.Ex41_FirstMissingPositive(new[] { 1, 2, -1 }));
        Assert.AreEqual(4, Leetcode.Ex41_FirstMissingPositive(new[] { 1, 2, 0, -1, 3 }));
        Assert.AreEqual(4, Leetcode.Ex41_FirstMissingPositive(new[] { 1, 2, 1, 3, 3 }));
        Assert.AreEqual(3, Leetcode.Ex41_FirstMissingPositive(new[] { 1, 2, 4, 4, 5, 1, 0, -1 }));
        Assert.AreEqual(7, Leetcode.Ex41_FirstMissingPositive(new[] { 1, 2, 6, 3, 5, 4 }));
    }

    [TestMethod]
    public void Ex45_Jump()
    {
        Assert.AreEqual(2, Leetcode.Ex45_Jump(new int[] { 2, 3, 1, 9, 4 }));
        Assert.AreEqual(4, Leetcode.Ex45_Jump(new int[] { 2, 3, 1, 1, 1, 9, 4 }));
        Assert.AreEqual(2, Leetcode.Ex45_Jump(new int[] { 2, 3, 0, 1, 4 }));
    }

    [TestMethod]
    public void Ex45_JumpFaster()
    {
        Assert.AreEqual(2, Leetcode.Ex45_JumpFaster(new int[] { 2, 3, 1, 9, 4 }));
        Assert.AreEqual(4, Leetcode.Ex45_JumpFaster(new int[] { 2, 3, 1, 1, 1, 9, 4 }));
        Assert.AreEqual(2, Leetcode.Ex45_JumpFaster(new int[] { 2, 3, 0, 1, 4 }));
    }

    [TestMethod]
    public void Ex45_JumpFastest()
    { 
        Assert.AreEqual(2, Leetcode.Ex45_JumpFastest(new int[] { 2, 3, 1, 9, 4 }));
        Assert.AreEqual(4, Leetcode.Ex45_JumpFastest(new int[] { 2, 3, 1, 1, 1, 9, 4 }));
        Assert.AreEqual(2, Leetcode.Ex45_JumpFastest(new int[] { 2, 3, 0, 1, 4 }));
    }

    [TestMethod]
    public void Ex53_MaximumSubarray()
    {
        Assert.AreEqual(10, Leetcode.Ex53_MaximumSubarray_Quadratic(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
        Assert.AreEqual(6, Leetcode.Ex53_MaximumSubarray_Quadratic(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
        Assert.AreEqual(1, Leetcode.Ex53_MaximumSubarray_Quadratic(new int[] { 1 }));
        Assert.AreEqual(23, Leetcode.Ex53_MaximumSubarray_Quadratic(new int[] { 5, 4, -1, 7, 8 }));
        Assert.AreEqual(10, Leetcode.Ex53_MaximumSubarray_Quadratic(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
    }

    [TestMethod]
    public void Ex53_MaximumSubarray_Linearithmic()
    {
        Assert.AreEqual(10, Leetcode.Ex53_MaximumSubarray_Kadane(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
        Assert.AreEqual(6, Leetcode.Ex53_MaximumSubarray_Kadane(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
        Assert.AreEqual(1, Leetcode.Ex53_MaximumSubarray_Kadane(new int[] { 1 }));
        Assert.AreEqual(23, Leetcode.Ex53_MaximumSubarray_Kadane(new int[] { 5, 4, -1, 7, 8 }));
        Assert.AreEqual(10, Leetcode.Ex53_MaximumSubarray_Kadane(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -2, 8, -6, 6 }));
        Assert.AreEqual(8, Leetcode.Ex53_MaximumSubarray_Kadane(
            new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4, -1, -10, -2, 8, -6, 6 }));
        Assert.AreEqual(-1, Leetcode.Ex53_MaximumSubarray_Kadane(new int[] { -2, -1 }));
        Assert.AreEqual(-1, Leetcode.Ex53_MaximumSubarray_Kadane(new int[] { -1, -2 }));
    }

    [TestMethod]
    public void Ex69_MySqrt()
    {
        Assert.AreEqual(46339, Leetcode.Ex69_MySqrt(2147395599));
    }

    [TestMethod]
    public void Ex131_PalindromePartition()
    {
        var partitions1 = Leetcode.Ex131_PalindromePartition("aab");
        Assert.IsTrue(partitions1.Count == 2);
        Assert.AreEqual(1, partitions1.Count(p => p.SequenceEqual(new[] { "a", "a", "b" })));
        Assert.AreEqual(1, partitions1.Count(p => p.SequenceEqual(new[] { "aa", "b" })));

        var partitions2 = Leetcode.Ex131_PalindromePartition("xxyxx");
        Assert.IsTrue(partitions2.Count == 6);
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "x", "x", "y", "x", "x" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "x", "x", "y", "xx" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "x", "xyx", "x" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "xx", "y", "x", "x" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "xx", "y", "xx" })));
        Assert.AreEqual(1, partitions2.Count(p => p.SequenceEqual(new[] { "xxyxx" })));
    }

    [TestMethod]
    public void Ex149_MaxPoints()
    {
        Assert.AreEqual(0, Leetcode.Ex149_MaxPoints(
            Array.Empty<int[]>()));
        Assert.AreEqual(1, Leetcode.Ex149_MaxPoints(
            new[] { new[] { 1, 1 }}));
        Assert.AreEqual(2, Leetcode.Ex149_MaxPoints(
            new[] { new[] { 1, 1 }, new[] { 1, 2 }, new[] { 2, 1 }, new[] { 2, 2 } }));
        Assert.AreEqual(3, Leetcode.Ex149_MaxPoints(
            new[] { new[] { 1, 1 }, new[] { 1, 2 }, new[] { 2, 1 }, new[] { 2, 2 }, new[] { 2, 3} }));
        Assert.AreEqual(3, Leetcode.Ex149_MaxPoints(
            new[] { new[] { 1, 1 }, new[] { 2, 2 }, new[] { 3, 3 } }));
        Assert.AreEqual(4, Leetcode.Ex149_MaxPoints(
            new[] { new[] { 1, 1 }, new[] { 3, 2 }, new[] { 5, 3 }, new[] { 4, 1 }, new[] { 2, 3 }, new[] { 1, 4 } }));
        Assert.AreEqual(2, Leetcode.Ex149_MaxPoints(
            new[] { new[] { 5151, 5150 },new[] { 0, 0 },new[] { 5152, 5151 } }));
        Assert.AreEqual(2, Leetcode.Ex149_MaxPoints(
           new[] { new[] { 1, 1 }, new[] { 2, 2 }, new[] { 2, 1 }, new[] { 3, 2 } }));
    }

    [TestMethod]
    public void Ex149_MaxPoints_Fixing1stPoint()
    {
        Assert.AreEqual(0, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            Array.Empty<int[]>()));
        Assert.AreEqual(1, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            new[] { new[] { 1, 1 } }));
        Assert.AreEqual(2, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            new[] { new[] { 1, 1 }, new[] { 1, 2 }, new[] { 2, 1 }, new[] { 2, 2 } }));
        Assert.AreEqual(3, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            new[] { new[] { 1, 1 }, new[] { 1, 2 }, new[] { 2, 1 }, new[] { 2, 2 }, new[] { 2, 3 } }));
        Assert.AreEqual(3, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            new[] { new[] { 1, 1 }, new[] { 2, 2 }, new[] { 3, 3 } }));
        Assert.AreEqual(4, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            new[] { new[] { 1, 1 }, new[] { 3, 2 }, new[] { 5, 3 }, new[] { 4, 1 }, new[] { 2, 3 }, new[] { 1, 4 } }));
        Assert.AreEqual(2, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            new[] { new[] { 5151, 5150 }, new[] { 0, 0 }, new[] { 5152, 5151 } }));
        Assert.AreEqual(2, Leetcode.Ex149_MaxPoints_Fixing1stPoint(
            new[] { new[] { 1, 1 }, new[] { 2, 2 }, new[] {  2, 1 }, new[] { 3, 2 } }));
    }

    [TestMethod]
    public void Ex207_CanFinish_EdgeList()
    {
        Assert.IsTrue(Leetcode.Ex207_CanFinish_EdgeList(2, new[] { new[] { 0, 1 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_EdgeList(2, new[] { new[] { 1, 0 } }));
        Assert.IsFalse(Leetcode.Ex207_CanFinish_EdgeList(2, new[] { new[] { 0, 1 }, new[] { 1, 0 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 2 } }));
        Assert.IsFalse(Leetcode.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 0 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_EdgeList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 0, 2 } }));
    }

    [TestMethod]
    public void Ex207_CanFinish_AdjList()
    {
        Assert.IsTrue(Leetcode.Ex207_CanFinish_AdjList(2, new[] { new[] { 0, 1 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_AdjList(2, new[] { new[] { 1, 0 } }));
        Assert.IsFalse(Leetcode.Ex207_CanFinish_AdjList(2, new[] { new[] { 0, 1 }, new[] { 1, 0 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 2 } }));
        Assert.IsFalse(Leetcode.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 0 } }));
        Assert.IsTrue(Leetcode.Ex207_CanFinish_AdjList(3, new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 0, 2 } }));

        Assert.IsTrue(Leetcode.Ex207_CanFinish_AdjList(100, new[] {
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
            Leetcode.Ex210_FindOrder(2, new[] { new[] { 1, 0 } })));
        Assert.IsTrue(new[] { 1, 0 }.SequenceEqual(
            Leetcode.Ex210_FindOrder(2, new[] { new[] { 0, 1 } })));
        Assert.IsTrue(new[] { 1, 0, 2 }.SequenceEqual(
            Leetcode.Ex210_FindOrder(3, new[] { new[] { 0, 1 }, new[] { 2, 1 }, new[] { 2, 0 } })));
        Assert.IsTrue(Array.Empty<int>().SequenceEqual(
            Leetcode.Ex210_FindOrder(3, new[] { new[] { 0, 1 }, new[] { 2, 1 }, new[] { 1, 2 } })));
    }

    [TestMethod]
    public void Ex226_InvertTree()
    {
        var input = ArrayToTree(new[] { 4, 2, 7, 1, 3, 6, 9 }, 0);
        Assert.IsTrue(new[] { 4, 7, 2, 9, 6, 3, 1 }.SequenceEqual(TreeToArray(Leetcode.Ex226_InvertTree(input))));

        TreeNode? ArrayToTree(int[] vals, int i)
        {
            if (i >= vals.Length) return null;
            var left = ArrayToTree(vals, 2 * i + 1);
            var right = ArrayToTree(vals, 2 * i + 2);
            return new TreeNode(vals[i], left, right);
        }

        IEnumerable<int> TreeToArray(TreeNode node)
        {
            if (node == null) yield break;

            var queue = new Queue<TreeNode>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                var n = queue.Dequeue();
                yield return n.val;
                if (n.left != null) queue.Enqueue(n.left);
                if (n.right != null) queue.Enqueue(n.right);
            }
        }
    }

    [TestMethod]
    public void Ex299_GetHint()
    {
        Assert.AreEqual("1A3B", Leetcode.Ex299_GetHint("1807", "7810"));
        Assert.AreEqual("1A1B", Leetcode.Ex299_GetHint("1123", "0111"));
        Assert.AreEqual("3A9B", Leetcode.Ex299_GetHint("18071236345345", "78101535234523"));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_BfsOnly()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_BfsOnly(4, 
            new[] { new[] { 1, 0 }, new[] { 1, 2}, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_BfsOnly(6, 
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_BfsOnlyOptimized()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_BfsOnlyOptimized(4,
            new[] { new[] { 1, 0 }, new[] { 1, 2 }, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_BfsOnlyOptimized(6,
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_DfsUndirectedTopoSort()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_DfsUndirectedTopoSort(4,
            new[] { new[] { 1, 0 }, new[] { 1, 2 }, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_DfsUndirectedTopoSort(6,
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
    }

    [TestMethod]
    public void Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue(4,
            new[] { new[] { 1, 0 }, new[] { 1, 2 }, new[] { 1, 3 } })));
        Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue(6,
            new[] { new[] { 3, 0 }, new[] { 3, 1 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 5, 4 } })));
        Assert.IsTrue(new[] { 0 }.SequenceEqual(
            Leetcode.Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue(1, Array.Empty<int[]>())));
    }

    [TestMethod]
    public void Ex329_LongestIncreasingPath()
    {
        Assert.AreEqual(5, Leetcode.Ex329_LongestIncreasingPath(
            new[] {new[] {9,9,4,3},new[] {6,6,8,2},new[] {2,1,1,1}}));
        Assert.AreEqual(4, Leetcode.Ex329_LongestIncreasingPath(
            new[] {new[] {9,9,4},new[] {6,6,8},new[] {2,1,1}}));
        Assert.AreEqual(4, Leetcode.Ex329_LongestIncreasingPath(
            new[] {new[] {9,9,4,2,1},new[] {6,6,8,2,1},new[] {2,1,1,1,1}}));
        Assert.AreEqual(1, Leetcode.Ex329_LongestIncreasingPath(
            new[] {new[] {0}}));
        Assert.AreEqual(1, Leetcode.Ex329_LongestIncreasingPath(
            new[] {new[] {0},new[] {0},new[] {0}}));
    }

    [TestMethod]
    public void Ex347_TopKFrequent()
    {
        Assert.IsTrue(Array.Empty<int>().SequenceEqual(Leetcode.Ex347_TopKFrequent(new[] { 1, 1, 1, 1, 2, 3, 4, 4, 4 }, 0)));
        Assert.IsTrue(new[] { 1 }.SequenceEqual(Leetcode.Ex347_TopKFrequent(new[] { 1, 1, 1, 1, 2, 3, 4, 4, 4 }, 1)));
        Assert.IsTrue(new[] { 1, 4 }.SequenceEqual(Leetcode.Ex347_TopKFrequent(new[] { 1, 1, 1, 2, 2, 3, 4, 4, 4 }, 2)));
    }

    [TestMethod]
    public void Ex347_TopKFrequent_PartialSort()
    {
        Assert.IsTrue(Array.Empty<int>().SequenceEqual(Leetcode.Ex347_TopKFrequent_PartialSort(new[] { 1, 1, 1, 1, 2, 3, 4, 4, 4 }, 0)));
        Assert.IsTrue(new[] { 1 }.SequenceEqual(Leetcode.Ex347_TopKFrequent_PartialSort(new[] { 1, 1, 1, 1, 2, 3, 4, 4, 4 }, 1)));
        Assert.IsTrue(new[] { 1, 4 }.SequenceEqual(Leetcode.Ex347_TopKFrequent_PartialSort(new[] { 1, 1, 1, 2, 2, 3, 4, 4, 4 }, 2)));
        Assert.IsTrue(new[] { 1, 2, 3 }.SequenceEqual(Leetcode.Ex347_TopKFrequent_PartialSort(new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3 }, 3)));
    }

    [TestMethod]
    public void Ex399_CalcEquation_Dfs()
    {
        new[] { 6.00000, 0.50000, 1.20000, 1.00000, -1.00000, 0.50000 }
            .Zip(Leetcode.Ex399_CalcEquation_Dfs(
                new[] { new[] { "a", "b" }, new[] { "b", "c" }, new[] { "c", "e" }, new[] { "d", "e" } },
                new[] { 2.0, 3.0, 0.2, 0.4 },
                new[] { 
                    new[] { "a", "c" }, new[] { "b", "a" }, new[] { "a", "e" }, new[] { "a", "a" }, 
                    new[] { "x", "x" }, new[] { "c", "d" } }))
            .ToList()
            .ForEach(c => Assert.AreEqual(c.First, c.Second, 0.00001));
    }

    [TestMethod]
    public void Ex399_CalcEquation_DisjointSet()
    {
        new[] { 6.00000, 0.50000, 1.20000, 1.00000, -1.00000, 0.50000 }
            .Zip(Leetcode.Ex399_CalcEquation_DisjointSets(
                new[] { new[] { "a", "b" }, new[] { "b", "c" }, new[] { "c", "e" }, new[] { "d", "e" } },
                new[] { 2.0, 3.0, 0.2, 0.4 },
                new[] {
                    new[] { "a", "c" }, new[] { "b", "a" }, new[] { "a", "e" }, new[] { "a", "a" },
                    new[] { "x", "x" }, new[] { "c", "d" } }))
            .ToList()
            .ForEach(c => Assert.AreEqual(c.First, c.Second, 0.00001));

        new[] { 6.00000, 0.50000, -1.00000, 1.00000, -1.00000 }
            .Zip(Leetcode.Ex399_CalcEquation_DisjointSets(
                new[] { new[] { "a", "b" }, new[] { "b", "c" } },
                new[] { 2.0, 3.0 },
                new[] {
                            new[] { "a", "c" }, new[] { "b", "a" }, new[] { "a", "e" }, new[] { "a", "a" },
                            new[] { "x", "x" }, new[] { "x", "x" } }))
            .ToList()
            .ForEach(c => Assert.AreEqual(c.First, c.Second, 0.00001));
    }

    [TestMethod]
    public void Ex451_FrequencySort()
    {
        Assert.AreEqual("eeerrt", Leetcode.Ex451_FrequencySort("trreee"));
        Assert.AreEqual("rrreet", Leetcode.Ex451_FrequencySort("treerr"));
        Assert.AreEqual("sssssssffffff44444aaaa55522", Leetcode.Ex451_FrequencySort("2a554442f544asfasssffffasss"));
    }

    [TestMethod]
    public void Ex451_FrequencySort_WithArray()
    {
        Assert.AreEqual("eeerrt", Leetcode.Ex451_FrequencySort_WithArray("trreee"));
        Assert.AreEqual("rrreet", Leetcode.Ex451_FrequencySort_WithArray("treerr"));
        Assert.AreEqual("sssssssffffff44444aaaa55522", Leetcode.Ex451_FrequencySort_WithArray("2a554442f544asfasssffffasss"));
    }

    [TestMethod]
    public void Ex494_FindTargetSumWays()
    {
        Assert.AreEqual(0, Leetcode.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1, 4, 0, 6 }, 8));
        Assert.AreEqual(0, Leetcode.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1, 4, 0 }, 8));
        Assert.AreEqual(5, Leetcode.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1, 5 }, 8));
        Assert.AreEqual(5, Leetcode.Ex494_FindTargetSumWays(new[] { 1, 1, 1, 1, 1 }, 3));
        Assert.AreEqual(1, Leetcode.Ex494_FindTargetSumWays(new[] { 1 }, 1));
        Assert.AreEqual(1, Leetcode.Ex494_FindTargetSumWays(new[] { 1 }, -1));
    }

    [TestMethod]
    public void Ex547_FindCircleNum()
    {
        Assert.AreEqual(2, Leetcode.Ex547_FindCircleNum(new[] {new[] {1, 1, 0},new[] {1, 1, 0},new[] {0, 0, 1}}));
        Assert.AreEqual(3, Leetcode.Ex547_FindCircleNum(new[] { new[] { 1, 0, 0 }, new[] { 0, 1, 0 }, new[] { 0, 0, 1 } }));
    }

    [TestMethod]
    public void Ex727_MinWindow()
    {
        Assert.AreEqual("xyz", Leetcode.Ex727_MinWindow("xxyxyz", "xyz"));
        Assert.AreEqual("xaybbz", Leetcode.Ex727_MinWindow("xaybbzxaaybz", "xyz"));
        Assert.AreEqual("xaaybz", Leetcode.Ex727_MinWindow("xaaybbzxaaybz", "xyz"));
        Assert.AreEqual("xyabcz", Leetcode.Ex727_MinWindow("xaayxaaaybxyabcz", "xyz"));
        Assert.AreEqual("", Leetcode.Ex727_MinWindow("yxz", "xyz"));
        Assert.AreEqual("", Leetcode.Ex727_MinWindow("yzx", "xyz"));
        Assert.AreEqual("xayyyyybz", Leetcode.Ex727_MinWindow("xxxxxxxayyyyybzzzzzz", "xyz"));
    }

    private record Ex843_Master(string Secret) : Ex843_IMaster
    {
        public int Count { get; private set; } = 0;
        public bool SecretFound { get; private set; }

        public int Guess(string word)
        {
            var result = Secret.Zip(word).Count(c => c.First == c.Second);
            Count++;
            SecretFound = result == word.Length;
            return result;
        }
    }

    [TestMethod]
    public void Ex843_FindSecretWord()
    {
        {
            var words = new string[]
            {
                "wichbx", "oahwep", "tpulot", "eqznzs", "vvmplb", "eywinm", "dqefpt", "kmjmxr", "ihkovg", "trbzyb",
                "xqulhc", "bcsbfw", "rwzslk", "abpjhw", "mpubps", "viyzbc", "kodlta", "ckfzjh", "phuepp", "rokoro",
                "nxcwmo", "awvqlr", "uooeon", "hhfuzz", "sajxgr", "oxgaix", "fnugyu", "lkxwru", "mhtrvb", "xxonmg",
                "tqxlbr", "euxtzg", "tjwvad", "uslult", "rtjosi", "hsygda", "vyuica", "mbnagm", "uinqur", "pikenp",
                "szgupv", "qpxmsw", "vunxdn", "jahhfn", "kmbeok", "biywow", "yvgwho", "hwzodo", "loffxk", "xavzqd",
                "vwzpfe", "uairjw", "itufkt", "kaklud", "jjinfa", "kqbttl", "zocgux", "ucwjig", "meesxb", "uysfyc",
                "kdfvtw", "vizxrv", "rpbdjh", "wynohw", "lhqxvx", "kaadty", "dxxwut", "vjtskm", "yrdswc", "byzjxm",
                "jeomdc", "saevda", "himevi", "ydltnu", "wrrpoc", "khuopg", "ooxarg", "vcvfry", "thaawc", "bssybb",
                "ccoyyo", "ajcwbj", "arwfnl", "nafmtm", "xoaumd", "vbejda", "kaefne", "swcrkh", "reeyhj", "vmcwaf",
                "chxitv", "qkwjna", "vklpkp", "xfnayl", "ktgmfn", "xrmzzm", "fgtuki", "zcffuv", "srxuus", "pydgmq"
            };
            var master = new Ex843_Master("ccoyyo");
            Leetcode.Ex843_FindSecretWord(words, master);
            Assert.IsTrue(master.SecretFound);
            Assert.IsTrue(master.Count <= 10);
        }
        {
            var words = new string[]
            {
                "gaxckt", "trlccr", "jxwhkz", "ycbfps", "peayuf", "yiejjw", "ldzccp", "nqsjoa", "qrjasy", "pcldos", 
                "acrtag", "buyeia", "ubmtpj", "drtclz", "zqderp", "snywek", "caoztp", "ibpghw", "evtkhl", "bhpfla", 
                "ymqhxk", "qkvipb", "tvmued", "rvbass", "axeasm", "qolsjg", "roswcb", "vdjgxx", "bugbyv", "zipjpc", 
                "tamszl", "osdifo", "dvxlxm", "iwmyfb", "wmnwhe", "hslnop", "nkrfwn", "puvgve", "rqsqpq", "jwoswl", 
                "tittgf", "evqsqe", "aishiv", "pmwovj", "sorbte", "hbaczn", "coifed", "hrctvp", "vkytbw", "dizcxz", 
                "arabol", "uywurk", "ppywdo", "resfls", "tmoliy", "etriev", "oanvlx", "wcsnzy", "loufkw", "onnwcy", 
                "novblw", "mtxgwe", "rgrdbt", "ckolob", "kxnflb", "phonmg", "egcdab", "cykndr", "lkzobv", "ifwmwp", 
                "jqmbib", "mypnvf", "lnrgnj", "clijwa", "kiioqr", "syzebr", "rqsmhg", "sczjmz", "hsdjfp", "mjcgvm", 
                "ajotcx", "olgnfv", "mjyjxj", "wzgbmg", "lpcnbj", "yjjlwn", "blrogv", "bdplzs", "oxblph", "twejel", 
                "rupapy", "euwrrz", "apiqzu", "ydcroj", "ldvzgq", "zailgu", "xgqpsr", "wxdyho", "alrplq", "brklfk"
            };
            var master = new Ex843_Master("hbaczn");
            Leetcode.Ex843_FindSecretWord(words, master);
            Assert.IsTrue(master.SecretFound);
            Assert.IsTrue(master.Count <= 10);
        }
    }

    [TestMethod]
    public void Ex992_SubarraysWithKDistinct_Quadratic()
    {
        Assert.AreEqual(7, Leetcode.Ex992_SubarraysWithKDistinct_Quadratic(new[] { 1, 2, 1, 2, 3 }, 2));
        Assert.AreEqual(3, Leetcode.Ex992_SubarraysWithKDistinct_Quadratic(new[] { 1, 2, 1, 3, 4 }, 3));
        Assert.AreEqual(10, Leetcode.Ex992_SubarraysWithKDistinct_Quadratic(new[] { 2, 1, 2, 1, 2 }, 2));
        Assert.AreEqual(23, Leetcode.Ex992_SubarraysWithKDistinct_Quadratic(new[] { 2, 2, 1, 2, 2, 2, 1, 1 }, 2));
    }

    [TestMethod]
    public void Ex992_SubarraysWithKDistinct_ForAndWhile()
    {
        Assert.AreEqual(7, Leetcode.Ex992_SubarraysWithKDistinct_ForAndWhile(new[] { 1, 2, 1, 2, 3 }, 2));
        Assert.AreEqual(3, Leetcode.Ex992_SubarraysWithKDistinct_ForAndWhile(new[] { 1, 2, 1, 3, 4 }, 3));
        Assert.AreEqual(10, Leetcode.Ex992_SubarraysWithKDistinct_ForAndWhile(new[] { 2, 1, 2, 1, 2 }, 2));
        Assert.AreEqual(23, Leetcode.Ex992_SubarraysWithKDistinct_ForAndWhile(new[] { 2, 2, 1, 2, 2, 2, 1, 1 }, 2));
    }
    [TestMethod]
    public void Ex1138_AlphabetBoardPath()
    {
        Assert.AreEqual("DDR!UURRR!!DDD!", Leetcode.Ex1138_AlphabetBoardPath("leet"));
        Assert.AreEqual("RR!DDRR!UUL!R!", Leetcode.Ex1138_AlphabetBoardPath("code"));
        Assert.AreEqual("DDDDD!UURRRR!UUU!DDDDLLLLD!UURRR!R!DLLLLD!", Leetcode.Ex1138_AlphabetBoardPath("ztezstz"));
    }

    [TestMethod]
    public void Ex1153_CanConvert()
    {
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("ab", "ba", 3));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("ab", "bb", 3));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("ab", "aa", 3));
        
        Assert.IsFalse(Leetcode.Ex1153_CanConvert("ab", "ba", 2));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("ab", "aa", 2));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("ab", "bb", 2));

        Assert.IsTrue(Leetcode.Ex1153_CanConvert("aba", "bab", 3));
        Assert.IsFalse(Leetcode.Ex1153_CanConvert("aba", "abc", 3));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("aba", "cac", 3));

        Assert.IsTrue(Leetcode.Ex1153_CanConvert("abcd", "aaaa", 4));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("abcd", "abbb", 4));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("abcd", "dcaa", 4));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("abcd", "abba", 4));
        Assert.IsTrue(Leetcode.Ex1153_CanConvert("abba", "baab", 3));

        Assert.IsTrue(Leetcode.Ex1153_CanConvert("aabcc", "ccdee", 26));
        Assert.IsFalse(Leetcode.Ex1153_CanConvert("leetcode", "codeleet", 26));
    }

    [TestMethod]
    public void Ex1423_MaxScore_DP()
    {
        Assert.AreEqual(12, Leetcode.Ex1423_MaxScore_DP(new[] { 1, 2, 3, 4, 5, 6, 1 }, 3));
        Assert.AreEqual(4, Leetcode.Ex1423_MaxScore_DP(new[] { 2, 2, 2 }, 2));
        Assert.AreEqual(55, Leetcode.Ex1423_MaxScore_DP(new[] { 9, 7, 7, 9, 7, 7, 9 }, 7));
    }

    [TestMethod]
    public void Ex1423_MaxScore_Window()
    {
        Assert.AreEqual(12, Leetcode.Ex1423_MaxScore_Window(new[] { 1, 2, 3, 4, 5, 6, 1 }, 3));
        Assert.AreEqual(4, Leetcode.Ex1423_MaxScore_Window(new[] { 2, 2, 2 }, 2));
        Assert.AreEqual(55, Leetcode.Ex1423_MaxScore_Window(new[] { 9, 7, 7, 9, 7, 7, 9 }, 7));
    }

    [TestMethod]
    public void Ex1499_FindMaxValueOfEquation()
    {
        Assert.AreEqual(4, Leetcode.Ex1499_FindMaxValueOfEquation(
            new[] { new[] { 1, 3 }, new[] { 2, 0 }, new[] { 5, 10 }, new[] { 6, -10 } }, 1));
        Assert.AreEqual(3, Leetcode.Ex1499_FindMaxValueOfEquation(
            new[] { new[] { 0, 0 }, new[] { 3, 0 }, new[] { 9, 2 } }, 3));
    }

    [TestMethod]
    public void Ex1499_FindMaxValueOfEquation_WithHeap()
    {
        Assert.AreEqual(4, Leetcode.Ex1499_FindMaxValueOfEquation_WithHeap(
            new[] { new[] { 1, 3 }, new[] { 2, 0 }, new[] { 5, 10 }, new[] { 6, -10 } }, 1));
        Assert.AreEqual(3, Leetcode.Ex1499_FindMaxValueOfEquation_WithHeap(
            new[] { new[] { 0, 0 }, new[] { 3, 0 }, new[] { 9, 2 } }, 3));
    }

    [TestMethod]
    public void Ex1548_MostSimilarPath()
    {
        var names = new[] { "A", "B", "C", "D", "E", "F" };
        var n = names.Length;
        var namesToIds = names.Select((n, i) => (n, i)).ToDictionary(c => c.n, c => c.i);
        IList<IList<int>> ToRoads(string roads) => roads
            .Split(';')
            .Select(road => road
                .Split(',')
                .Select(c => namesToIds[c])
                .ToList() as IList<int>)
            .ToList();
        string[] ToPath(string pathStr) => pathStr.Select(c => c.ToString()).ToArray();

        Assert.AreEqual("ABC",
            string.Join("", Leetcode.Ex1548_MostSimilarPath(n, ToRoads("A,B;B,C"), names, ToPath("ABC"))));
        Assert.AreEqual("ABA",
            string.Join("", Leetcode.Ex1548_MostSimilarPath(n, ToRoads("A,B"), names, ToPath("ABC"))));
        Assert.AreEqual("ACAC",
            string.Join("", Leetcode.Ex1548_MostSimilarPath(n, ToRoads("A,C;C,D"), names, ToPath("ABCC"))));
        Assert.AreEqual("ACDE",
            string.Join("", Leetcode.Ex1548_MostSimilarPath(n, ToRoads("A,C;C,D;D,E"), names, ToPath("ACDE"))));
        Assert.AreEqual("ACACA",
            string.Join("", Leetcode.Ex1548_MostSimilarPath(n, ToRoads("A,C;C,D;D,E"), names, ToPath("ABCDE"))));
    }

    [TestMethod]
    public void Ex1834_GetOrder()
    {
        Assert.IsTrue(new[] { 0, 2, 3, 1 }.SequenceEqual(Leetcode.Ex1834_GetOrder(
            new[] { new[] { 1, 2 }, new[] { 2, 4 }, new[] { 3, 2 }, new[] { 4, 1 } })));
        Assert.IsTrue(new[] { 4, 3, 2, 0, 1 }.SequenceEqual(Leetcode.Ex1834_GetOrder(
            new[] { new[] { 7, 10 }, new[] { 7, 12 }, new[] { 7, 5 }, new[] { 7, 4 }, new[] { 7, 2 } })));
    }

    [TestMethod]
    public void Ex1882_AssignTasks_SingleQueue()
    {
        Assert.IsTrue(new[] { 2, 2, 0, 2, 1, 2 }.SequenceEqual(Leetcode.Ex1882_AssignTasks_SingleQueue
            (new[] { 3, 3, 2 }, new[] { 1, 2, 3, 2, 1, 2 })));
    }

    [TestMethod]
    public void Ex1882_AssignTasks_TwoQueues()
    {
        Assert.IsTrue(new[] { 2, 2, 0, 2, 1, 2 }.SequenceEqual(Leetcode.Ex1882_AssignTasks_TwoQueues
            (new[] { 3, 3, 2 }, new[] { 1, 2, 3, 2, 1, 2 })));
    }

    [TestMethod]
    public void Ex2050_MinimumTime_ShortestPathViaTopoSort()
    {
        Assert.AreEqual(8, Leetcode.Ex2050_MinimumTime_ShortestPathViaTopoSort(
            3, new[] { new[] { 1, 3 }, new[] { 2, 3 } }, new[] { 3, 2, 5 }));
    }

    [TestMethod]
    public void Ex2050_MinimumTime_LongestPathViaTopoSort()
    {
        Assert.AreEqual(8, Leetcode.Ex2050_MinimumTime_AllPairsLongestPathViaTopoSort(
            3, new[] { new[] { 1, 3 }, new[] { 2, 3 } }, new[] { 3, 2, 5 }));
    }

    [TestMethod]
    public void Ex2050_MinimumTime_ShortestPathViaDfs()
    {
        Assert.AreEqual(8, Leetcode.Ex2050_MinimumTime_ShortestPathViaDfs(
            3, new[] { new[] { 1, 3 }, new[] { 2, 3 } }, new[] { 3, 2, 5 }));
    }

    [TestMethod]
    public void Ex2359_ClosestMeetingNode_TwoSimplifiedBfs()
    {
        Assert.AreEqual(2, Leetcode.Ex2359_ClosestMeetingNode_TwoSimplifiedBfs(new[] { 2, 2, 3, -1 }, 0, 1));
        Assert.AreEqual(1, Leetcode.Ex2359_ClosestMeetingNode_TwoSimplifiedBfs(new[] { 1, 2, -1 }, 0, 1));
        Assert.AreEqual(2, Leetcode.Ex2359_ClosestMeetingNode_TwoSimplifiedBfs(new[] { 1, 2, -1 }, 0, 2));
        Assert.AreEqual(1, Leetcode.Ex2359_ClosestMeetingNode_TwoSimplifiedBfs(new[] { 4, 4, 8, -1, 9, 8, 4, 4, 1, 1 }, 5, 6));
    }

    [TestMethod]
    public void Ex2359_ClosestMeetingNode_OptimizedWalking()
    {
        Assert.AreEqual(2, Leetcode.Ex2359_ClosestMeetingNode_OptimizedWalking(new[] { 2, 2, 3, -1 }, 0, 1));
        Assert.AreEqual(1, Leetcode.Ex2359_ClosestMeetingNode_OptimizedWalking(new[] { 1, 2, -1 }, 0, 1));
        Assert.AreEqual(2, Leetcode.Ex2359_ClosestMeetingNode_OptimizedWalking(new[] { 1, 2, -1 }, 0, 2));
        Assert.AreEqual(1, Leetcode.Ex2359_ClosestMeetingNode_OptimizedWalking(new[] { 4, 4, 8, -1, 9, 8, 4, 4, 1, 1 }, 5, 6));
    }

    [TestMethod]
    public void Ex2374_EdgeScore()
    {
        Assert.AreEqual(7, Leetcode.Ex2374_EdgeScore(new[] { 1, 0, 0, 0, 0, 7, 7, 5 }));
        Assert.AreEqual(0, Leetcode.Ex2374_EdgeScore(new[] { 2, 0, 0, 2 }));
        Assert.AreEqual(1, Leetcode.Ex2374_EdgeScore(new[] { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 }));
    }
}
