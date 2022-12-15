using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CTCI.Leetcode;

namespace CTCI.Tests;
internal static class Extensions
{
    public static bool SequenceEqual2d<T>(this IList<T[]> first, IList<T[]> second)
    {
        return first.Count == second.Count && Enumerable.Zip(first, second).All(c => c.First.SequenceEqual(c.Second));
    }

    public static int[] ToArray(this ListNode? list)
    {
        var result = new List<int> { };
        while (list != null)
        {
            result.Add(list.val);
            list = list.next;
        }

        return result.ToArray();
    }
}

[TestClass]
public class LeetcodeTests
{
    private static TreeNode? BuildTree(params int?[] numbers)
    {
        if (numbers.Length == 0 || numbers[0] == null)
            throw new ArgumentException($"{nameof(numbers)} is empty.");

        var nodes = Enumerable
            .Range(0, numbers.Length)
            .Select(i => numbers[i] != null ? new TreeNode(numbers[i].Value) : null)
            .ToArray();

        var nodesInCurrentLevel = 1;
        var firstIndexInCurrentLevel = 0;

        while (firstIndexInCurrentLevel < numbers.Length)
        {
            var nodesNonNullInCurrentLevel = 0;
            for (
                var currentIndex = firstIndexInCurrentLevel;
                currentIndex < firstIndexInCurrentLevel + nodesInCurrentLevel && currentIndex < numbers.Length;
                currentIndex++)
            {
                if (nodes[currentIndex] != null)
                {
                    var leftChildIndex = firstIndexInCurrentLevel + nodesInCurrentLevel + 2 * nodesNonNullInCurrentLevel;
                    if (leftChildIndex < nodes.Length)
                        nodes[currentIndex].left = nodes[leftChildIndex];

                    var rightChildIndex = firstIndexInCurrentLevel + nodesInCurrentLevel + 2 * nodesNonNullInCurrentLevel + 1;
                    if (rightChildIndex < nodes.Length)
                        nodes[currentIndex].right = nodes[rightChildIndex];

                    nodesNonNullInCurrentLevel++;
                }
            }

            firstIndexInCurrentLevel += nodesInCurrentLevel;
            nodesInCurrentLevel = nodesNonNullInCurrentLevel * 2;
        }

        return nodes[0];
    }

    private static ListNode? BuildListNode(params int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return null;

        var head = new ListNode(numbers[0]);
        var current = head;
        for (var i = 1; i < numbers.Length; i++)
        {
            current.next = new ListNode(numbers[i]);
            current = current.next;
        }

        return head;
    }

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
    public void Ex7_Reverse()
    {
        Assert.AreEqual(321, Leetcode.Ex7_Reverse(123));
        Assert.AreEqual(-321, Leetcode.Ex7_Reverse(-123));
        Assert.AreEqual(0, Leetcode.Ex7_Reverse(0));
        Assert.AreEqual(21, Leetcode.Ex7_Reverse(120));
        Assert.AreEqual(1, Leetcode.Ex7_Reverse(1000));
        Assert.AreEqual(-11, Leetcode.Ex7_Reverse(-1100));
        Assert.AreEqual(0, Leetcode.Ex7_Reverse(2147483647));
        Assert.AreEqual(0, Leetcode.Ex7_Reverse(-2147483648));
        Assert.AreEqual(-2143847412, Leetcode.Ex7_Reverse(-2147483412));
    }

    [TestMethod]
    public void Ex8_MyAtoi()
    {
        Assert.AreEqual(42, Leetcode.Ex8_MyAtoi("42"));
        Assert.AreEqual(int.MaxValue, Leetcode.Ex8_MyAtoi("9223372036854775808"));
        Assert.AreEqual(4193, Leetcode.Ex8_MyAtoi("4193 with words"));
        Assert.AreEqual(42, Leetcode.Ex8_MyAtoi(" +0042-"));
        Assert.AreEqual(0, Leetcode.Ex8_MyAtoi(" -"));
        Assert.AreEqual(int.MaxValue, Leetcode.Ex8_MyAtoi("2147483647"));
        Assert.AreEqual(int.MinValue + 1, Leetcode.Ex8_MyAtoi("-2147483647"));
        Assert.AreEqual(int.MinValue, Leetcode.Ex8_MyAtoi("-2147483650"));
        Assert.AreEqual(int.MinValue, Leetcode.Ex8_MyAtoi("-91283472332"));
        Assert.AreEqual(int.MaxValue, Leetcode.Ex8_MyAtoi("2147483648"));
    }

    [TestMethod]
    public void Ex8_MyAtoiOptimized()
    {
        Assert.AreEqual(42, Leetcode.Ex8_MyAtoiOptimized("42"));
        Assert.AreEqual(int.MaxValue, Leetcode.Ex8_MyAtoiOptimized("9223372036854775808"));
        Assert.AreEqual(4193, Leetcode.Ex8_MyAtoiOptimized("4193 with words"));
        Assert.AreEqual(42, Leetcode.Ex8_MyAtoiOptimized(" +0042-"));
        Assert.AreEqual(0, Leetcode.Ex8_MyAtoiOptimized(" -"));
        Assert.AreEqual(int.MaxValue, Leetcode.Ex8_MyAtoiOptimized("2147483647"));
        Assert.AreEqual(int.MinValue + 1, Leetcode.Ex8_MyAtoiOptimized("-2147483647"));
        Assert.AreEqual(int.MinValue, Leetcode.Ex8_MyAtoiOptimized("-2147483650"));
        Assert.AreEqual(int.MinValue, Leetcode.Ex8_MyAtoiOptimized("-91283472332"));
        Assert.AreEqual(int.MaxValue, Leetcode.Ex8_MyAtoiOptimized("2147483648"));
    }

    [TestMethod]
    public void Ex10_IsMatch()
    {
        Assert.IsTrue(Leetcode.Ex10_IsMatch("a", "a"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("a", "."));
        Assert.IsFalse(Leetcode.Ex10_IsMatch("a", "a."));

        Assert.IsTrue(Leetcode.Ex10_IsMatch("ab", "ab"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("ab", "a."));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("ab", ".."));

        Assert.IsTrue(Leetcode.Ex10_IsMatch("", "a*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("a", "a*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("aaaa", "a*"));
        Assert.IsFalse(Leetcode.Ex10_IsMatch("", "aa*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("a", "aa*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("aa", "aa*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("aaa", "aa*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("aa", "aa*a"));
        Assert.IsFalse(Leetcode.Ex10_IsMatch("a", "aa*a"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("", "a*a*a*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("aabbb", "a*b*"));
        Assert.IsTrue(Leetcode.Ex10_IsMatch("aabbb", "aa*abb*b"));
        Assert.IsFalse(Leetcode.Ex10_IsMatch("aabbb", "aa*aabb*b"));
    }

    [TestMethod]
    public void Ex11_MaxArea()
    {
        Assert.AreEqual(49, Leetcode.Ex11_MaxArea(new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }));
        Assert.AreEqual(1, Leetcode.Ex11_MaxArea(new[] { 1, 1 }));
        Assert.AreEqual(16, Leetcode.Ex11_MaxArea(new[] { 2, 1, 10, 5, 8 }));
    }

    [TestMethod]
    public void Ex12_IntToRoman_Recursive()
    {
        Assert.AreEqual("III", Leetcode.Ex12_IntToRoman_Recursive(3));
        Assert.AreEqual("LVIII", Leetcode.Ex12_IntToRoman_Recursive(58));
        Assert.AreEqual("MCMXCIV", Leetcode.Ex12_IntToRoman_Recursive(1994));
        Assert.AreEqual("DLXXXIX", Leetcode.Ex12_IntToRoman_Recursive(589));
        Assert.AreEqual("MMMCDXLIX", Leetcode.Ex12_IntToRoman_Recursive(3449));
    }

    [TestMethod]
    public void Ex12_IntToRoman_Iterative()
    {
        Assert.AreEqual("III", Leetcode.Ex12_IntToRoman_Iterative(3));
        Assert.AreEqual("LVIII", Leetcode.Ex12_IntToRoman_Iterative(58));
        Assert.AreEqual("MCMXCIV", Leetcode.Ex12_IntToRoman_Iterative(1994));
        Assert.AreEqual("DLXXXIX", Leetcode.Ex12_IntToRoman_Iterative(589));
        Assert.AreEqual("MMMCDXLIX", Leetcode.Ex12_IntToRoman_Iterative(3449));
    }

    [TestMethod]
    public void Ex13_RomanToInt()
    {
        Assert.AreEqual(1, Leetcode.Ex13_RomanToInt("I"));
        Assert.AreEqual(3, Leetcode.Ex13_RomanToInt("III"));
        Assert.AreEqual(44, Leetcode.Ex13_RomanToInt("XLIV"));
        Assert.AreEqual(3426, Leetcode.Ex13_RomanToInt("MMMCDXXVI"));
        Assert.AreEqual(3999, Leetcode.Ex13_RomanToInt("MMMCMXCIX"));
    }

    [TestMethod]
    public void Ex14_LongestCommonPrefix()
    {
        Assert.AreEqual("fl", Leetcode.Ex14_LongestCommonPrefix(new[] { "flower", "flow", "flight" }));
        Assert.AreEqual("", Leetcode.Ex14_LongestCommonPrefix(new[] { "dog", "racecar", "car" }));
        Assert.AreEqual("", Leetcode.Ex14_LongestCommonPrefix(new[] { "", "", "flight", "f" }));
        Assert.AreEqual("", Leetcode.Ex14_LongestCommonPrefix(new[] { "fl", "", "flight", "fli" }));
        Assert.AreEqual("a", Leetcode.Ex14_LongestCommonPrefix(new[] { "a", "aa", "aaa" }));
        Assert.AreEqual("aaa", Leetcode.Ex14_LongestCommonPrefix(new[] { "aaa", "aaa", "aaa" }));
    }

    [TestMethod]
    public void Ex17_LetterCombinations()
    {
        Assert.IsTrue(Array.Empty<string>().ToHashSet().SetEquals(
            Leetcode.Ex17_LetterCombinations("")));
        Assert.IsTrue(new[] { "w", "x", "y", "z" }.ToHashSet().SetEquals(
            Leetcode.Ex17_LetterCombinations("9")));
        Assert.IsTrue(new[] { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" }.ToHashSet().SetEquals(
            Leetcode.Ex17_LetterCombinations("23")));
        Assert.IsTrue(new[] { "aa", "ab", "ac", "ba", "bb", "bc", "ca", "cb", "cc" }.ToHashSet().SetEquals(
            Leetcode.Ex17_LetterCombinations("22")));
    }

    [TestMethod]
    public void Ex18_FourSum_DP()
    {
        Assert.IsTrue(new[] { (1, 0, -1, 0), (1, -1, -2, 2), (0, 0, -2, 2) }.ToHashSet().SetEquals(
            Leetcode.Ex18_FourSum_DP(new[] { 1, 0, -1, 0, -2, 2 }, 0).Select(l => (l[0], l[1], l[2], l[3]))));
        Assert.IsTrue(new[] { (2, 2, 2, 2) }.ToHashSet().SetEquals(
            Leetcode.Ex18_FourSum_DP(new[] { 2, 2, 2, 2, 2 }, 8).Select(l => (l[0], l[1], l[2], l[3]))));
    }

    [TestMethod]
    public void Ex19_RemoveNthFromEnd_TreePointers()
    {
        Assert.IsTrue(new[] { 1, 2, 3, 4 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TreePointers(BuildListNode(1, 2, 3, 4, 5 ), 1).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3, 5 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TreePointers(BuildListNode(1, 2, 3, 4, 5), 2).ToArray()));
        Assert.IsTrue(new[] { 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TreePointers(BuildListNode(1, 2, 3, 4, 5), 5).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TreePointers(BuildListNode(1, 2, 3, 4, 5), 6).ToArray()));
        Assert.IsTrue(new int[] { }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TreePointers(BuildListNode(1), 1).ToArray()));
    }

    [TestMethod]
    public void Ex19_RemoveNthFromEnd_TwoPointers()
    {
        Assert.IsTrue(new[] { 1, 2, 3, 4 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TwoPointers(BuildListNode(1, 2, 3, 4, 5), 1).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3, 5 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TwoPointers(BuildListNode(1, 2, 3, 4, 5), 2).ToArray()));
        Assert.IsTrue(new[] { 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TwoPointers(BuildListNode(1, 2, 3, 4, 5), 5).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TwoPointers(BuildListNode(1, 2, 3, 4, 5), 6).ToArray()));
        Assert.IsTrue(new int[] { }.SequenceEqual(
            Leetcode.Ex19_RemoveNthFromEnd_TwoPointers(BuildListNode(1), 1).ToArray()));
    }

    [TestMethod]
    public void Ex21_MergeTwoLists()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex21_MergeTwoLists(BuildListNode(1), BuildListNode()).ToArray()));
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex21_MergeTwoLists(BuildListNode(), BuildListNode(1)).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3 }.SequenceEqual(
            Leetcode.Ex21_MergeTwoLists(BuildListNode(1, 3), BuildListNode(2)).ToArray()));
        Assert.IsTrue(new[] { 0, 1, 2, 4, 5, 6  }.SequenceEqual(
            Leetcode.Ex21_MergeTwoLists(BuildListNode(1, 2, 5), BuildListNode(0, 4, 6)).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3 }.SequenceEqual(
            Leetcode.Ex21_MergeTwoLists(BuildListNode(), BuildListNode(1, 2, 3)).ToArray()));
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
    public void Ex23_MergeKLists()
    {
        var list1 = BuildListNode(0, 2, 3, 3, 6, 6, 6, 9, 10);
        var list2 = BuildListNode(1, 1, 2, 2, 4, 5, 7, 11, 11);
        var merged = Leetcode.Ex23_MergeKLists(new[] { list1, list2 });
        Assert.IsTrue(
            new[] { 0, 1, 1, 2, 2, 2, 3, 3, 4, 5, 6, 6, 6, 7, 9, 10, 11, 11 }.SequenceEqual(
                merged.ToArray()));
    }

    [TestMethod]
    public void Ex24_SwapPairs()
    {
        Assert.IsTrue(new int[] { }.SequenceEqual(
            Leetcode.Ex24_SwapPairs(BuildListNode()).ToArray()));
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex24_SwapPairs(BuildListNode(1)).ToArray()));
        Assert.IsTrue(new[] { 2, 1, 4, 3 }.SequenceEqual(
            Leetcode.Ex24_SwapPairs(BuildListNode(1, 2, 3, 4)).ToArray()));
        Assert.IsTrue(new[] { 2, 1, 4, 3, 5 }.SequenceEqual( 
            Leetcode.Ex24_SwapPairs(BuildListNode(1, 2, 3, 4, 5)).ToArray()));
    }

    [TestMethod]
    public void Ex25_ReverseKGroup_TwoPasses()
    {
        Assert.IsTrue(new[] { 1, 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex25_ReverseKGroup_TwoPasses(BuildListNode(1, 2, 3, 4, 5), 1).ToArray()));
        Assert.IsTrue(new[] { 2, 1, 4, 3, 5 }.SequenceEqual(
            Leetcode.Ex25_ReverseKGroup_TwoPasses(BuildListNode(1, 2, 3, 4, 5), 2).ToArray()));
        Assert.IsTrue(new[] { 3, 2, 1, 4, 5 }.SequenceEqual(
            Leetcode.Ex25_ReverseKGroup_TwoPasses(BuildListNode(1, 2, 3, 4, 5), 3).ToArray()));
        Assert.IsTrue(new[] { 4, 3, 2, 1, 5 }.SequenceEqual(
            Leetcode.Ex25_ReverseKGroup_TwoPasses(BuildListNode(1, 2, 3, 4, 5), 4).ToArray()));
        Assert.IsTrue(new[] { 5, 4, 3, 2, 1 }.SequenceEqual(
            Leetcode.Ex25_ReverseKGroup_TwoPasses(BuildListNode(1, 2, 3, 4, 5), 5).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex25_ReverseKGroup_TwoPasses(BuildListNode(1, 2, 3, 4, 5), 6).ToArray()));
    }

    [TestMethod]
    public void Ex26_RemoveDuplicates()
    {
        var l1 = new[] { 1 };
        Leetcode.Ex26_RemoveDuplicates(l1);
        Assert.IsTrue(new[] { 1 }.SequenceEqual(l1));

        l1 = new[] { 1, 1, 1 };
        Leetcode.Ex26_RemoveDuplicates(l1);
        Assert.IsTrue(new[] { 1 }.SequenceEqual(l1.Take(1)));

        l1 = new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };
        Leetcode.Ex26_RemoveDuplicates(l1);
        Assert.IsTrue(new[] { 0, 1, 2, 3, 4 }.SequenceEqual(l1.Take(5)));
    }

    [TestMethod]
    public void Ex32_LongestValidParentheses()
    {
        Assert.AreEqual(2, Leetcode.Ex32_LongestValidParentheses("(()"));
        Assert.AreEqual(4, Leetcode.Ex32_LongestValidParentheses(")()())"));
        Assert.AreEqual(14, Leetcode.Ex32_LongestValidParentheses("(()(()()(()))))()()((())(())"));
        Assert.AreEqual(2, Leetcode.Ex32_LongestValidParentheses("()(()"));
        Assert.AreEqual(6, Leetcode.Ex32_LongestValidParentheses("()(())"));
        Assert.AreEqual(8, Leetcode.Ex32_LongestValidParentheses("(()()(())(("));
    }

    [TestMethod]
    public void Ex32_LongestValidParentheses_WithArray()
    {
        Assert.AreEqual(2, Leetcode.Ex32_LongestValidParentheses_WithArray("(()"));
        Assert.AreEqual(4, Leetcode.Ex32_LongestValidParentheses_WithArray(")()())"));
        Assert.AreEqual(14, Leetcode.Ex32_LongestValidParentheses_WithArray("(()(()()(()))))()()((())(())"));
        Assert.AreEqual(2, Leetcode.Ex32_LongestValidParentheses_WithArray("()(()"));
        Assert.AreEqual(6, Leetcode.Ex32_LongestValidParentheses_WithArray("()(())"));
        Assert.AreEqual(8, Leetcode.Ex32_LongestValidParentheses_WithArray("(()()(())(("));
    }

    [TestMethod]
    public void Ex32_LongestValidParentheses_TwoCounters()
    {
        Assert.AreEqual(2, Leetcode.Ex32_LongestValidParentheses_TwoCounters("(()"));
        Assert.AreEqual(4, Leetcode.Ex32_LongestValidParentheses_TwoCounters(")()())"));
        Assert.AreEqual(14, Leetcode.Ex32_LongestValidParentheses_TwoCounters("(()(()()(()))))()()((())(())"));
        Assert.AreEqual(2, Leetcode.Ex32_LongestValidParentheses_TwoCounters("()(()"));
        Assert.AreEqual(6, Leetcode.Ex32_LongestValidParentheses_TwoCounters("()(())"));
        Assert.AreEqual(8, Leetcode.Ex32_LongestValidParentheses_TwoCounters("(()()(())(("));
    }

    [TestMethod]
    public void Ex35_SearchInsert()
    {
        Assert.AreEqual(0, Leetcode.Ex35_SearchInsert(new[] { 0, 1, 2, 3 }, 0));
        Assert.AreEqual(3, Leetcode.Ex35_SearchInsert(new[] { 0, 1, 2, 3 }, 3));
        Assert.AreEqual(2, Leetcode.Ex35_SearchInsert(new[] { 0, 1, 6, 7 }, 2));
        Assert.AreEqual(4, Leetcode.Ex35_SearchInsert(new[] { 0, 1, 2, 3 }, 7));
        Assert.AreEqual(0, Leetcode.Ex35_SearchInsert(new[] { 0, 1, 2, 3 }, -1));
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
    public void Ex38_CountAndSay()
    {
        Assert.AreEqual("1", Leetcode.Ex38_CountAndSay(1));
        Assert.AreEqual("11", Leetcode.Ex38_CountAndSay(2));
        Assert.AreEqual("21", Leetcode.Ex38_CountAndSay(3));
        Assert.AreEqual("1211", Leetcode.Ex38_CountAndSay(4));
        Assert.AreEqual("111221", Leetcode.Ex38_CountAndSay(5));
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
    public void Ex42_Trap_Quadratic()
    {
        Assert.AreEqual(6, Leetcode.Ex42_Trap_Quadratic(new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));
        Assert.AreEqual(9, Leetcode.Ex42_Trap_Quadratic(new[] { 4, 2, 0, 3, 2, 5 }));
        Assert.AreEqual(50, Leetcode.Ex42_Trap_Quadratic(new[] { 4,1,2,1,3,1,5,1,2,1,6,1,5,3,4,2,1,1,5,5,6,6 }));
        Assert.AreEqual(1, Leetcode.Ex42_Trap_Quadratic(new[] { 4,2,3 }));
        Assert.AreEqual(1, Leetcode.Ex42_Trap_Quadratic(new[] { 5,4,1,2 }));
    }

    [TestMethod]
    public void Ex42_TrapDP()
    {
        Assert.AreEqual(6, Leetcode.Ex42_TrapDP(new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));
        Assert.AreEqual(9, Leetcode.Ex42_TrapDP(new[] { 4, 2, 0, 3, 2, 5 }));
        Assert.AreEqual(50, Leetcode.Ex42_TrapDP(new[] { 4, 1, 2, 1, 3, 1, 5, 1, 2, 1, 6, 1, 5, 3, 4, 2, 1, 1, 5, 5, 6, 6 }));
        Assert.AreEqual(1, Leetcode.Ex42_TrapDP(new[] { 4, 2, 3 }));
        Assert.AreEqual(1, Leetcode.Ex42_TrapDP(new[] { 5, 4, 1, 2 }));
    }

    [TestMethod]
    public void Ex42_TrapDPBottomUp()
    {
        Assert.AreEqual(6, Leetcode.Ex42_TrapDPBottomUp(new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));
        Assert.AreEqual(9, Leetcode.Ex42_TrapDPBottomUp(new[] { 4, 2, 0, 3, 2, 5 }));
        Assert.AreEqual(50, Leetcode.Ex42_TrapDPBottomUp(new[] { 4, 1, 2, 1, 3, 1, 5, 1, 2, 1, 6, 1, 5, 3, 4, 2, 1, 1, 5, 5, 6, 6 }));
        Assert.AreEqual(1, Leetcode.Ex42_TrapDPBottomUp(new[] { 4, 2, 3 }));
        Assert.AreEqual(1, Leetcode.Ex42_TrapDPBottomUp(new[] { 5, 4, 1, 2 }));
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
    public void Ex51_SolveNQueens_Recursive()
    {
        Assert.AreEqual(
            "Q",
            Leetcode.Ex51_SolveNQueens_Recursive(1)
                .Aggregate((l1, l2) => l1.Concat(l2).OrderBy(s => s).ToList())
                .Aggregate((s1, s2) => string.Compare(s1, s2) <= 0 ? s1 + s2 : s2 + s1));
        Assert.AreEqual(
            "",
            Leetcode.Ex51_SolveNQueens_Recursive(2)
                .Aggregate(new List<string>(), (l1, l2) => l1.Concat(l2).ToList())
                .Aggregate("", (s1, s2) => s1 + s2));
        Assert.AreEqual(
            "...Q...Q..Q...Q..Q...Q..Q...Q...",
            Leetcode.Ex51_SolveNQueens_Recursive(4)
                .Aggregate((l1, l2) => l1.Concat(l2).OrderBy(s => s).ToList())
                .Aggregate((s1, s2) => string.Compare(s1, s2) <= 0 ? s1 + s2 : s2 + s1));
        Assert.AreEqual(
            "....Q....Q....Q....Q....Q....Q....Q....Q....Q....Q...Q....Q....Q....Q....Q....Q....Q....Q....Q....Q..." +
            "Q....Q....Q....Q....Q....Q....Q....Q....Q....Q...Q....Q....Q....Q....Q....Q....Q....Q....Q....Q...Q..." +
            ".Q....Q....Q....Q....Q....Q....Q....Q....Q....",
            Leetcode.Ex51_SolveNQueens_Recursive(5)
                .Aggregate((l1, l2) => l1.Concat(l2).OrderBy(s => s).ToList())
                .Aggregate((s1, s2) => string.Compare(s1, s2) <= 0 ? s1 + s2 : s2 + s1));
    }

    [TestMethod]
    public void Ex51_SolveNQueens_Iterative()
    {
        Assert.AreEqual(
            "Q",
            Leetcode.Ex51_SolveNQueens_Iterative(1)
                .Aggregate((l1, l2) => l1.Concat(l2).OrderBy(s => s).ToList())
                .Aggregate((s1, s2) => string.Compare(s1, s2) <= 0 ? s1 + s2 : s2 + s1));
        Assert.AreEqual(
            "",
            Leetcode.Ex51_SolveNQueens_Iterative(2)
                .Aggregate(new List<string>(), (l1, l2) => l1.Concat(l2).ToList())
                .Aggregate("", (s1, s2) => s1 + s2));
        Assert.AreEqual(
            "...Q...Q..Q...Q..Q...Q..Q...Q...",
            Leetcode.Ex51_SolveNQueens_Iterative(4)
                .Aggregate((l1, l2) => l1.Concat(l2).OrderBy(s => s).ToList())
                .Aggregate((s1, s2) => string.Compare(s1, s2) <= 0 ? s1 + s2 : s2 + s1));
        Assert.AreEqual(
            "....Q....Q....Q....Q....Q....Q....Q....Q....Q....Q...Q....Q....Q....Q....Q....Q....Q....Q....Q....Q..." +
            "Q....Q....Q....Q....Q....Q....Q....Q....Q....Q...Q....Q....Q....Q....Q....Q....Q....Q....Q....Q...Q..." +
            ".Q....Q....Q....Q....Q....Q....Q....Q....Q....",
            Leetcode.Ex51_SolveNQueens_Iterative(5)
                .Aggregate((l1, l2) => l1.Concat(l2).OrderBy(s => s).ToList())
                .Aggregate((s1, s2) => string.Compare(s1, s2) <= 0 ? s1 + s2 : s2 + s1));
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
    public void Ex56_Merge()
    {
        Assert.IsTrue(new[] { new[] { 1, 6 }, new[] { 8, 10 }, new[] { 15, 18 } }
            .Zip(
                Leetcode.Ex56_Merge(new[] { new[] { 1, 3 }, new[] { 2, 6 }, new[] { 8, 10 }, new[] { 15, 18 } }),
                (first, second) => first.SequenceEqual(second))
            .All(b => b));
        Assert.IsTrue(new[] { new[] { 1, 5 }, new[] { 6, 11 }, new[] { 12, 12 }, new[] { 13, 16 } }
            .Zip(
                Leetcode.Ex56_Merge(new[] { new[] { 1, 4 }, new[] { 4, 5 }, new[] { 7, 10 }, new[] { 6, 8 }, new[] { 8, 9 }, new[] { 9, 11 }, new[] { 12, 12 }, new[] { 13, 16 }, new[] { 13, 14 }, new[] { 13, 15 } }),
                (first, second) => first.SequenceEqual(second))
            .All(b => b));
    }

    [TestMethod]
    public void Ex56_Merge_InputSort()
    {
        Assert.IsTrue(new[] { new[] { 1, 6 }, new[] { 8, 10 }, new[] { 15, 18 } }
            .Zip(
                Leetcode.Ex56_Merge_InputSort(new[] { new[] { 1, 3 }, new[] { 2, 6 }, new[] { 8, 10 }, new[] { 15, 18 } }),
                (first, second) => first.SequenceEqual(second))
            .All(b => b));
        Assert.IsTrue(new[] { new[] { 1, 5 }, new[] { 6, 11 }, new[] { 12, 12 }, new[] { 13, 16 } }
            .Zip(
                Leetcode.Ex56_Merge_InputSort(new[] { new[] { 1, 4 }, new[] { 4, 5 }, new[] { 7, 10 }, new[] { 6, 8 }, new[] { 8, 9 }, new[] { 9, 11 }, new[] { 12, 12 }, new[] { 13, 16 }, new[] { 13, 14 }, new[] { 13, 15 } }),
                (first, second) => first.SequenceEqual(second))
            .All(b => b));
    }

    [TestMethod]
    public void Ex68_FullJustify()
    {
        Assert.IsTrue(
            new[] { "This    is    an", "example  of text", "justification.  " }.SequenceEqual(
                Leetcode.Ex68_FullJustify(
                    new[] { "This", "is", "an", "example", "of", "text", "justification." }, 16)));
        Assert.IsTrue(
            new[] { "What   must   be", "acknowledgment  ", "shall be        " }.SequenceEqual(
                Leetcode.Ex68_FullJustify(
                    new[] { "What", "must", "be", "acknowledgment", "shall be" }, 16)));

        Assert.IsTrue(
            new[] { "Science  is  what we", "understand      well", "enough to explain to", "a  computer.  Art is", 
                "everything  else  we", "do                  " }.SequenceEqual(
                Leetcode.Ex68_FullJustify(
                    new[] { "Science", "is", "what", "we", "understand", "well", "enough", "to", "explain", "to", "a", 
                        "computer.", "Art", "is", "everything", "else", "we", "do" }, 20)));
    }

    [TestMethod]
    public void Ex69_MySqrt()
    {
        Assert.AreEqual(46339, Leetcode.Ex69_MySqrt(2147395599));
    }

    [TestMethod]
    public void Ex72_MinDistance_DP()
    {
        Assert.AreEqual(3, Leetcode.Ex72_MinDistance_DP("horse", "ros"));
        Assert.AreEqual(5, Leetcode.Ex72_MinDistance_DP("intention", "execution"));
    }

    [TestMethod]
    public void Ex72_MinDistance_DPBottomUp()
    {
        Assert.AreEqual(3, Leetcode.Ex72_MinDistance_DPBottomUp("horse", "ros"));
        Assert.AreEqual(5, Leetcode.Ex72_MinDistance_DPBottomUp("intention", "execution"));
    }

    [TestMethod]
    public void Ex74_SearchMatrix()
    {
        Assert.IsTrue(Leetcode.Ex74_SearchMatrix(
            new[] { new[] { 1, 3, 5, 7 }, new[] { 10, 11, 16, 20 }, new[] { 23, 30, 34, 60 } }, 1));
        Assert.IsTrue(Leetcode.Ex74_SearchMatrix(
            new[] { new[] { 1, 3, 5, 7 }, new[] { 10, 11, 16, 20 }, new[] { 23, 30, 34, 60 } }, 11));
        Assert.IsTrue(Leetcode.Ex74_SearchMatrix(
            new[] { new[] { 1, 3, 5, 7 }, new[] { 10, 11, 16, 20 }, new[] { 23, 30, 34, 60 } }, 60));
        Assert.IsFalse(Leetcode.Ex74_SearchMatrix(
            new[] { new[] { 1, 3, 5, 7 }, new[] { 10, 11, 16, 20 }, new[] { 23, 30, 34, 60 } }, 2));
    }

    [TestMethod]
    public void Ex75_SortColors_Counting()
    {
        var a1 = new[] { 0, 1, 1, 2, 2, 0, 1, 0, 2, 2, 2, 1 };
        Leetcode.Ex75_SortColors_Counting(a1);
        Assert.IsTrue(new[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2 }.SequenceEqual(a1));
    }

    [TestMethod]
    public void Ex75_SortColors_Lomuto()
    {
        var a1 = new[] { 0, 1, 1, 2, 2, 0, 1, 0, 2, 2, 2, 1 };
        Leetcode.Ex75_SortColors_Lomuto(a1);
        Assert.IsTrue(new[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2 }.SequenceEqual(a1));
    }

    [TestMethod]
    public void Ex75_SortColors_DutchFlag()
    {
        var a1 = new[] { 0, 1, 1, 2, 2, 0, 1, 0, 2, 2, 2, 1 };
        Leetcode.Ex75_SortColors_DutchFlag(a1);
        Assert.IsTrue(new[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2 }.SequenceEqual(a1));
    }

    [TestMethod]
    public void Ex84_LargestRectangleArea()
    {
        Assert.AreEqual(10, Leetcode.Ex84_LargestRectangleArea(new[] { 2, 1, 5, 6, 2, 3 }));
        Assert.AreEqual(4, Leetcode.Ex84_LargestRectangleArea(new[] { 2, 4 }));
        Assert.AreEqual(18, Leetcode.Ex84_LargestRectangleArea(new[] { 1, 1, 2, 4, 4, 3, 3, 5, 5, 1, 1, 4 }));
    }

    [TestMethod]
    public void Ex85_MaximalRectangle_Quadratic()
    {
        Assert.AreEqual(6, Leetcode.Ex85_MaximalRectangle_Quadratic(new[] { 
            new[] { '1', '0', '1', '0', '0' }, 
            new[] { '1', '0', '1', '1', '1' }, 
            new[] { '1', '1', '1', '1', '1' }, 
            new[] { '1', '0', '0', '1', '0' } }));
        Assert.AreEqual(0, Leetcode.Ex85_MaximalRectangle_Quadratic(new[] { new[] { '0' } }));
        Assert.AreEqual(1, Leetcode.Ex85_MaximalRectangle_Quadratic(new[] { new[] { '1' } }));
        Assert.AreEqual(5, Leetcode.Ex85_MaximalRectangle_Quadratic(new[] {
            new[] { '1', '0', '1', '0', '0' },
            new[] { '1', '0', '1', '0', '1' },
            new[] { '1', '1', '1', '1', '1' },
            new[] { '1', '0', '0', '1', '0' } }));
        Assert.AreEqual(4, Leetcode.Ex85_MaximalRectangle_Quadratic(new[] {
            new[] { '1', '0', '1', '0', '0' },
            new[] { '1', '0', '1', '0', '1' },
            new[] { '1', '1', '0', '1', '1' },
            new[] { '1', '0', '0', '1', '0' } }));
        Assert.AreEqual(2, Leetcode.Ex85_MaximalRectangle_Quadratic(new[] {
            new[] { '1', '0', '1', '0', '0' },
            new[] { '1', '0', '1', '0', '1' },
            new[] { '0', '1', '0', '1', '1' },
            new[] { '1', '0', '0', '1', '0' } }));
    }

    [TestMethod]
    public void Ex115_NumDistinct_DPTopDown()
    {
        Assert.AreEqual(3, Leetcode.Ex115_NumDistinct_DPTopDown("rabbbit", "rabbit"));
        Assert.AreEqual(5, Leetcode.Ex115_NumDistinct_DPTopDown("babgbag", "bag"));
        Assert.AreEqual(32, Leetcode.Ex115_NumDistinct_DPTopDown("aaabbcccdabbc", "abc"));
    }

    [TestMethod]
    public void Ex115_NumDistinct_DPBottomUp()
    {
        Assert.AreEqual(3, Leetcode.Ex115_NumDistinct_DPBottomUp("rabbbit", "rabbit"));
        Assert.AreEqual(5, Leetcode.Ex115_NumDistinct_DPBottomUp("babgbag", "bag"));
        Assert.AreEqual(32, Leetcode.Ex115_NumDistinct_DPBottomUp("aaabbcccdabbc", "abc"));
    }

    [TestMethod]
    public void Ex115_NumDistinct_DPBottomUpSpaceOptimized()
    {
        Assert.AreEqual(3, Leetcode.Ex115_NumDistinct_DPBottomUpSpaceOptimized("rabbbit", "rabbit"));
        Assert.AreEqual(5, Leetcode.Ex115_NumDistinct_DPBottomUpSpaceOptimized("babgbag", "bag"));
        Assert.AreEqual(32, Leetcode.Ex115_NumDistinct_DPBottomUpSpaceOptimized("aaabbcccdabbc", "abc"));
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
    public void Ex143_ReorderList()
    {
        var list = BuildListNode(1); Leetcode.Ex143_ReorderList(list);
        Assert.IsTrue(new[] { 1 }.SequenceEqual(list.ToArray()));

        list = BuildListNode(1, 2); Leetcode.Ex143_ReorderList(list);
        Assert.IsTrue(new[] { 1, 2 }.SequenceEqual(list.ToArray()));

        list = BuildListNode(1, 2, 3); Leetcode.Ex143_ReorderList(list);
        Assert.IsTrue(new[] { 1, 3, 2 }.SequenceEqual(list.ToArray()));

        list = BuildListNode(1, 2, 3, 4); Leetcode.Ex143_ReorderList(list);
        Assert.IsTrue(new[] { 1, 4, 2, 3 }.SequenceEqual(list.ToArray()));

        list = BuildListNode(1, 2, 3, 4, 5); Leetcode.Ex143_ReorderList(list);
        Assert.IsTrue(new[] { 1, 5, 2, 4, 3 }.SequenceEqual(list.ToArray()));
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
    public void Ex155_MinStack_Monotonic()
    {
        var stack = new Ex155_MinStack_Monotonic();
        stack.Push(-2);
        stack.Push(0);
        stack.Push(-3);
        Assert.AreEqual(-3, stack.GetMin());
        stack.Pop();
        Assert.AreEqual(0, stack.Top());
        Assert.AreEqual(-2, stack.GetMin());
        stack.Push(4);
        Assert.AreEqual(-2, stack.GetMin());
        stack.Push(3);
        Assert.AreEqual(3, stack.Top());
        Assert.AreEqual(-2, stack.GetMin());
    }

    [TestMethod]
    public void Ex155_MinStack_StrictlyMonotonic()
    {
        var stack = new Ex155_MinStack_StrictlyMonotonic();
        stack.Push(-2);
        stack.Push(0);
        stack.Push(-3);
        Assert.AreEqual(-3, stack.GetMin());
        stack.Pop();
        Assert.AreEqual(0, stack.Top());
        Assert.AreEqual(-2, stack.GetMin());
        stack.Push(4);
        Assert.AreEqual(-2, stack.GetMin());
        stack.Push(3);
        Assert.AreEqual(3, stack.Top());
        Assert.AreEqual(-2, stack.GetMin());
    }

    [TestMethod]
    public void Ex159_LengthOfLongestSubstringTwoDistinct()
    {
        Assert.AreEqual(1, Leetcode.Ex159_LengthOfLongestSubstringTwoDistinct("a"));
        Assert.AreEqual(4, Leetcode.Ex159_LengthOfLongestSubstringTwoDistinct("aaaa"));
        Assert.AreEqual(3, Leetcode.Ex159_LengthOfLongestSubstringTwoDistinct("eceba"));
        Assert.AreEqual(14, Leetcode.Ex159_LengthOfLongestSubstringTwoDistinct("aaaababaabaabbcbababc"));
        Assert.AreEqual(8, Leetcode.Ex159_LengthOfLongestSubstringTwoDistinct("abbaacbabababadbdbdb"));
    }

    [TestMethod]
    public void Ex187_FindRepeatedDnaSequences_2Bits()
    {
        Assert.IsTrue(Array.Empty<string>().SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_2Bits("A")));
        Assert.IsTrue(Array.Empty<string>().SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_2Bits("ATCG")));
        Assert.IsTrue(new string[] { "AAAAACCCCC", "CCCCCAAAAA" }.SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_2Bits("AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT")));
        Assert.IsTrue(new string[] { "AATTCCAACC", "CCTTTTTCCC", "CTTTTTCCCC", "TTTTTCCCCC", "TTTTCCCCCT", "TTTCCCCCTT", "TTCCCCCTTT", "TCCCCCTTTT", "CCCCCTTTTT", "TTTTTTTTTT" }.SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_2Bits("AATTCCAACCAATTCCAACCTTTTTCCCCCTTTTTCCCCCTTTTTTTTTTTTTTTTTT")));
    }

    [TestMethod]
    public void Ex187_FindRepeatedDnaSequences_3Bits()
    {
        Assert.IsTrue(Array.Empty<string>().SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_3Bits("A")));
        Assert.IsTrue(Array.Empty<string>().SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_3Bits("ATCG")));
        Assert.IsTrue(new string[] { "AAAAACCCCC", "CCCCCAAAAA" }.SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_3Bits("AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT")));
        Assert.IsTrue(new string[] { "AATTCCAACC", "CCTTTTTCCC", "CTTTTTCCCC", "TTTTTCCCCC", "TTTTCCCCCT", "TTTCCCCCTT", "TTCCCCCTTT", "TCCCCCTTTT", "CCCCCTTTTT", "TTTTTTTTTT" }.SequenceEqual(
            Leetcode.Ex187_FindRepeatedDnaSequences_3Bits("AATTCCAACCAATTCCAACCTTTTTCCCCCTTTTTCCCCCTTTTTTTTTTTTTTTTTT")));
    }

    [TestMethod]
    public void Ex191_HammingWeight_ViaString()
    {
        Assert.AreEqual(0, Leetcode.Ex191_HammingWeight_ViaString(0b00000000_00000000_00000000_00000000));
        Assert.AreEqual(1, Leetcode.Ex191_HammingWeight_ViaString(0b00000000_00000000_00000000_00000001));
        Assert.AreEqual(2, Leetcode.Ex191_HammingWeight_ViaString(0b10000000_00000000_00000000_00000001));
        Assert.AreEqual(6, Leetcode.Ex191_HammingWeight_ViaString(0b11000001_00000001_00000001_00000001));
    }

    [TestMethod]
    public void Ex191_HammingWeight_Bit()
    {
        Assert.AreEqual(0, Leetcode.Ex191_HammingWeight_Bit(0b00000000_00000000_00000000_00000000));
        Assert.AreEqual(1, Leetcode.Ex191_HammingWeight_Bit(0b00000000_00000000_00000000_00000001));
        Assert.AreEqual(2, Leetcode.Ex191_HammingWeight_Bit(0b10000000_00000000_00000000_00000001));
        Assert.AreEqual(6, Leetcode.Ex191_HammingWeight_Bit(0b11000001_00000001_00000001_00000001));
    }

    [TestMethod]
    public void Ex191_HammingWeight_BitOptimized()
    {
        Assert.AreEqual(0, Leetcode.Ex191_HammingWeight_BitOptimized(0b00000000_00000000_00000000_00000000));
        Assert.AreEqual(1, Leetcode.Ex191_HammingWeight_BitOptimized(0b00000000_00000000_00000000_00000001));
        Assert.AreEqual(2, Leetcode.Ex191_HammingWeight_BitOptimized(0b10000000_00000000_00000000_00000001));
        Assert.AreEqual(6, Leetcode.Ex191_HammingWeight_BitOptimized(0b11000001_00000001_00000001_00000001));
    }

    [TestMethod]
    public void Ex200_NumIslands_WithAdjs()
    {
        Assert.AreEqual(1, Leetcode.Ex200_NumIslands_WithAdjs(
            new[]
            {
                new[] {'1', '1', '1', '1', '0'},
                new[] {'1', '1', '0', '1', '0'},
                new[] {'1', '1', '0', '0', '0'},
                new[] {'0', '0', '0', '0', '0'}
            }));

        Assert.AreEqual(3, Leetcode.Ex200_NumIslands_WithAdjs(
            new[] 
            { 
                new[] { '1', '1', '0', '0', '0' }, 
                new[] { '1', '1', '0', '0', '0' }, 
                new[] { '0', '0', '1', '0', '0' }, 
                new[] { '0', '0', '0', '1', '1' } 
            }));

        Assert.AreEqual(3, Leetcode.Ex200_NumIslands_WithAdjs(
            new[] 
            {
                new[] {'1','1','1','1','0'},
                new[] {'1','1','0','0','0'},
                new[] {'0','1','1','0','0'},
                new[] {'1','0','0','1','1'}
            }));
    }

    [TestMethod]
    public void Ex200_NumIslands_WithoutAdjs()
    {
        Assert.AreEqual(1, Leetcode.Ex200_NumIslands_WithoutAdjs(
            new[]
            {
                new[] {'1', '1', '1', '1', '0'},
                new[] {'1', '1', '0', '1', '0'},
                new[] {'1', '1', '0', '0', '0'},
                new[] {'0', '0', '0', '0', '0'}
            }));

        Assert.AreEqual(3, Leetcode.Ex200_NumIslands_WithoutAdjs(
            new[]
            {
                new[] { '1', '1', '0', '0', '0' },
                new[] { '1', '1', '0', '0', '0' },
                new[] { '0', '0', '1', '0', '0' },
                new[] { '0', '0', '0', '1', '1' }
            }));

        Assert.AreEqual(3, Leetcode.Ex200_NumIslands_WithoutAdjs(
            new[]
            {
                new[] {'1','1','1','1','0'},
                new[] {'1','1','0','0','0'},
                new[] {'0','1','1','0','0'},
                new[] {'1','0','0','1','1'}
            }));
    }

    [TestMethod]
    public void Ex200_NumIslands_TurnToZero()
    {
        Assert.AreEqual(1, Leetcode.Ex200_NumIslands_WithAdjs(
            new[]
            {
                new[] {'1', '1', '1', '0', '0'},
                new[] {'0', '0', '0', '0', '0'},
                new[] {'0', '0', '0', '0', '0'},
                new[] {'0', '0', '0', '0', '0'}
            }));


        int NumIslands(char[][] grid)
        {
            // If an "island" is found in the given grid, increment the counter and turn all connecting adjacent lands to "0".
            int islands = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        // turn all connecting adjacent lands to "0"
                        turnToZero(grid, i, j);
                        islands++;
                    }
                }
            }
            return islands;
        }

        void turnToZero(char[][] grid, int i, int j)
        {
            if (i < 0) return;
            if (j < 0) return;
            if (i >= grid.Length) return;
            if (j >= grid[i].Length) return;
            if (grid[i][j] == '0') return;

            grid[i][j] = '0';
            turnToZero(grid, i, j + 1);
            turnToZero(grid, i, j - 1);
            turnToZero(grid, i + 1, j);
            turnToZero(grid, i - 1, j);
        }

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
    public void Ex209_MinSubArrayLen_DP()
    {
        Assert.AreEqual(2, Leetcode.Ex209_MinSubArrayLen_DP(7, new[] { 2, 3, 1, 2, 4, 3 }));
        Assert.AreEqual(1, Leetcode.Ex209_MinSubArrayLen_DP(4,  new[] { 1, 4, 4 }));
        Assert.AreEqual(0, Leetcode.Ex209_MinSubArrayLen_DP(11, new[] { 1, 1, 1, 1, 1, 1, 1, 1 }));
        Assert.AreEqual(5, Leetcode.Ex209_MinSubArrayLen_DP(5, new[] { 1, 1, 1, 1, 1, 1, 1, 1 }));
        Assert.AreEqual(4, Leetcode.Ex209_MinSubArrayLen_DP(5, new[] { 1, 2, 1, 1, 1, 1, 1, 1 }));
        Assert.AreEqual(3, Leetcode.Ex209_MinSubArrayLen_DP(5, new[] { 1, 2, 1, 1, 1, 3, 1, 1 }));
    }

    [TestMethod]
    public void Ex209_MinSubArrayLen_SlidingWindow()
    {
        Assert.AreEqual(2, Leetcode.Ex209_MinSubArrayLen_SlidingWindow(7, new[] { 2, 3, 1, 2, 4, 3 }));
        Assert.AreEqual(1, Leetcode.Ex209_MinSubArrayLen_SlidingWindow(4, new[] { 1, 4, 4 }));
        Assert.AreEqual(0, Leetcode.Ex209_MinSubArrayLen_SlidingWindow(11, new[] { 1, 1, 1, 1, 1, 1, 1, 1 }));
        Assert.AreEqual(5, Leetcode.Ex209_MinSubArrayLen_SlidingWindow(5, new[] { 1, 1, 1, 1, 1, 1, 1, 1 }));
        Assert.AreEqual(4, Leetcode.Ex209_MinSubArrayLen_SlidingWindow(5, new[] { 1, 2, 1, 1, 1, 1, 1, 1 }));
        Assert.AreEqual(3, Leetcode.Ex209_MinSubArrayLen_SlidingWindow(5, new[] { 1, 2, 1, 1, 1, 3, 1, 1 }));
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
    public void Ex224_Calculate()
    {
        Assert.AreEqual(1 + 1, Leetcode.Ex224_Calculate("1 + 1"));
        Assert.AreEqual(2 - 1 + 2, Leetcode.Ex224_Calculate(" 2-1 + 2 "));
        Assert.AreEqual((1+(4+5+2)-3)+(6+8), Leetcode.Ex224_Calculate("(1+(4+5+2)-3)+(6+8)"));
        Assert.AreEqual(-2 + 1, Leetcode.Ex224_Calculate("-2+ 1"));
        Assert.AreEqual(2, Leetcode.Ex224_Calculate("-(-1)-(-1)"));
        Assert.AreEqual(0, Leetcode.Ex224_Calculate("(-1)-(-1)"));
        Assert.AreEqual(0, Leetcode.Ex224_Calculate("-(-1)+(-1)"));
        Assert.AreEqual(-2, Leetcode.Ex224_Calculate("-(-( -1))- (-(-1))"));
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
    public void Ex238_ProductExceptSelf()
    {
        Assert.IsTrue(new[] { 24, 12, 8, 6 }.SequenceEqual(Leetcode.Ex238_ProductExceptSelf(new int[] { 1, 2, 3, 4})));
        Assert.IsTrue(new[] { 0, 0, 9, 0, 0 }.SequenceEqual(Leetcode.Ex238_ProductExceptSelf(new int[] { -1, 1, 0, -3, 3 })));
    }

    [TestMethod]
    public void Ex239_MaxSlidingWindow_MinHeap()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex239_MaxSlidingWindow_MinHeap(new[] { 1 }, 1)));
        Assert.IsTrue(new[] { 3, 3, 5, 5, 6, 7 }.SequenceEqual(
            Leetcode.Ex239_MaxSlidingWindow_MinHeap(new[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3)));
        Assert.IsTrue(new[] { 3, 3, 2, 5 }.SequenceEqual(
            Leetcode.Ex239_MaxSlidingWindow_MinHeap(new[] { 1, 3, 1, 2, 0, 5 }, 3)));
    }

    [TestMethod]
    public void Ex239_MaxSlidingWindow_Deque()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex239_MaxSlidingWindow_Deque(new[] { 1 }, 1)));
        Assert.IsTrue(new[] { 3, 3, 5, 5, 6, 7 }.SequenceEqual(
            Leetcode.Ex239_MaxSlidingWindow_Deque(new[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3)));
        Assert.IsTrue(new[] { 3, 3, 2, 5 }.SequenceEqual(
            Leetcode.Ex239_MaxSlidingWindow_Deque(new[] { 1, 3, 1, 2, 0, 5 }, 3)));
    }

    [TestMethod]
    public void Ex253_MinMeetingRooms_UsingLINQ()
    {
        Assert.AreEqual(2, Leetcode.Ex253_MinMeetingRooms_UsingLINQ(
            new[] { new[] { 0, 30 }, new[] { 5, 10 }, new[] { 15, 20 } }));
        Assert.AreEqual(1, Leetcode.Ex253_MinMeetingRooms_UsingLINQ(
            new[] { new[] { 7, 10 }, new[] { 2, 4 } }));
        Assert.AreEqual(1, Leetcode.Ex253_MinMeetingRooms_UsingLINQ(
            new[] { new[] { 13, 15 }, new[] { 1, 13 } }));
    }

    [TestMethod]
    public void Ex253_MinMeetingRooms_UsingMinHeap()
    {
        Assert.AreEqual(2, Leetcode.Ex253_MinMeetingRooms_UsingMinHeap(
            new[] { new[] { 0, 30 }, new[] { 5, 10 }, new[] { 15, 20 } }));
        Assert.AreEqual(1, Leetcode.Ex253_MinMeetingRooms_UsingMinHeap(
            new[] { new[] { 7, 10 }, new[] { 2, 4 } }));
        Assert.AreEqual(1, Leetcode.Ex253_MinMeetingRooms_UsingMinHeap(
            new[] { new[] { 13, 15 }, new[] { 1, 13 } }));
    }    

    [TestMethod]
    public void Ex264_NthUglyNumber_TripleQueue()
    {
        Assert.AreEqual(1, Leetcode.Ex264_NthUglyNumber_TripleQueue(1));
        Assert.AreEqual(2, Leetcode.Ex264_NthUglyNumber_TripleQueue(2));
        Assert.AreEqual(3, Leetcode.Ex264_NthUglyNumber_TripleQueue(3));
        Assert.AreEqual(4, Leetcode.Ex264_NthUglyNumber_TripleQueue(4));
        Assert.AreEqual(5, Leetcode.Ex264_NthUglyNumber_TripleQueue(5));
        Assert.AreEqual(6, Leetcode.Ex264_NthUglyNumber_TripleQueue(6));
        Assert.AreEqual(12, Leetcode.Ex264_NthUglyNumber_TripleQueue(10));
        Assert.AreEqual(15, Leetcode.Ex264_NthUglyNumber_TripleQueue(11));
        Assert.AreEqual(16, Leetcode.Ex264_NthUglyNumber_TripleQueue(12));
        Assert.AreEqual(18, Leetcode.Ex264_NthUglyNumber_TripleQueue(13));
    }

    [TestMethod]
    public void Ex278_FirstBadVersion()
    {
        Assert.AreEqual(5, Leetcode.Ex278_FirstBadVersion(10, n => n >= 5));
        Assert.AreEqual(4, Leetcode.Ex278_FirstBadVersion(10, n => n > 3));
        Assert.AreEqual(10, Leetcode.Ex278_FirstBadVersion(10, n => n >= 10));
        Assert.AreEqual(1, Leetcode.Ex278_FirstBadVersion(10, n => n >= -1));
        Assert.AreEqual(1, Leetcode.Ex278_FirstBadVersion(10, n => true));
        Assert.AreEqual(11, Leetcode.Ex278_FirstBadVersion(10, n => false));
    }

    [TestMethod]
    public void Ex295_MedianFinder()
    {
        var finder = new Ex295_MedianFinder();
        finder.AddNum(1);
        finder.AddNum(2);
        Assert.AreEqual(1.5, finder.FindMedian());
        finder.AddNum(3);
        Assert.AreEqual(2.0, finder.FindMedian());
        finder.AddNum(0);
        Assert.AreEqual(1.5, finder.FindMedian());
        finder.AddNum(2);
        Assert.AreEqual(2.0, finder.FindMedian());
        finder.AddNum(4);
        Assert.AreEqual(2.0, finder.FindMedian());
        finder.AddNum(4);
        Assert.AreEqual(2.0, finder.FindMedian());
        finder.AddNum(3);
        Assert.AreEqual(2.5, finder.FindMedian());
        finder.AddNum(3);
        Assert.AreEqual(3.0, finder.FindMedian());
        finder.AddNum(1);
        Assert.AreEqual(2.5, finder.FindMedian());
        finder.AddNum(2);
        Assert.AreEqual(2.0, finder.FindMedian());
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
    public void Ex340_LengthOfLongestSubstringKDistinct()
    {
        Assert.AreEqual(3, Leetcode.Ex340_LengthOfLongestSubstringKDistinct("eceba", 2));
        Assert.AreEqual(4, Leetcode.Ex340_LengthOfLongestSubstringKDistinct("abaaaab", 1));
        Assert.AreEqual(5, Leetcode.Ex340_LengthOfLongestSubstringKDistinct("abbaacad", 2));
        Assert.AreEqual(7, Leetcode.Ex340_LengthOfLongestSubstringKDistinct("abbaacad", 3));
        Assert.AreEqual(0, Leetcode.Ex340_LengthOfLongestSubstringKDistinct("abbaacad", 0));
        Assert.AreEqual(0, Leetcode.Ex340_LengthOfLongestSubstringKDistinct("a", 0));
        Assert.AreEqual(9, Leetcode.Ex340_LengthOfLongestSubstringKDistinct("aaaaaaaaa", 1));
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
    public void Ex352_SummaryRanges_SortedSet()
    {
        var ds = new Ex352_SummaryRanges_SortedSet();
        ds.AddNum(1);
        Assert.IsTrue(new[] { new[] { 1, 1 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(3);
        Assert.IsTrue(new[] { new[] { 1, 1 }, new[] { 3, 3 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(7);
        Assert.IsTrue(new[] { new[] { 1, 1 }, new[] { 3, 3 }, new[] { 7, 7 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(2);
        Assert.IsTrue(new[] { new[] { 1, 3 }, new[] { 7, 7 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(6);
        Assert.IsTrue(new[] { new[] { 1, 3 }, new[] { 6, 7 } }.SequenceEqual2d(ds.GetIntervals()));
    }

    [TestMethod]
    public void Ex352_SummaryRanges_JumpListAndHeap()
    {
        var ds = new Ex352_SummaryRanges_JumpListAndHeap();
        ds.AddNum(1);
        Assert.IsTrue(new[] { new[] { 1, 1 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(3);
        Assert.IsTrue(new[] { new[] { 1, 1 }, new[] { 3, 3 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(7);
        Assert.IsTrue(new[] { new[] { 1, 1 }, new[] { 3, 3 }, new[] { 7, 7 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(2);
        Assert.IsTrue(new[] { new[] { 1, 3 }, new[] { 7, 7 } }.SequenceEqual2d(ds.GetIntervals()));
        ds.AddNum(6);
        Assert.IsTrue(new[] { new[] { 1, 3 }, new[] { 6, 7 } }.SequenceEqual2d(ds.GetIntervals()));
    }

    [TestMethod]
    public void Ex366_FindLeaves()
    {
        Assert.IsTrue(
            new[] 
            { 
                new[] { 12, 13, 14, 15, 16, 17 }, 
                new[] { 7, 9, 10, 11 }, 
                new[] { 4, 5, 6 }, 
                new[] { 2, 3 }, 
                new[] { 1 } 
            }
            .Zip(
                Leetcode.Ex366_FindLeaves(BuildTree(1, 2, 3, 4, 5, 6, null, 7, null, null, 9, 10, 11, null, 12, 13, 14, 15, null, 16, 17)),
                (first, second) => first.SequenceEqual(second))
            .All(b => b));
    }

    [TestMethod]
    public void Ex374_GuessNumber()
    {
        Assert.AreEqual(1, Leetcode.Ex374_GuessNumber(10, n => 1 - n));
        Assert.AreEqual(1, Leetcode.Ex374_GuessNumber(10, n => -n));
        Assert.AreEqual(5, Leetcode.Ex374_GuessNumber(10, n => 5 - n));
        Assert.AreEqual(10, Leetcode.Ex374_GuessNumber(10, n => 10 - n));
        Assert.AreEqual(11, Leetcode.Ex374_GuessNumber(10, n => 11 - n));
    }

    [TestMethod]
    public void Ex395_LongestSubstring_Quadratic()
    {
        Assert.AreEqual(1, Leetcode.Ex395_LongestSubstring_Quadratic("a", 1));
        Assert.AreEqual(3, Leetcode.Ex395_LongestSubstring_Quadratic("bbaaacbd", 3));
        Assert.AreEqual(3, Leetcode.Ex395_LongestSubstring_Quadratic("aaabb", 3));
        Assert.AreEqual(5, Leetcode.Ex395_LongestSubstring_Quadratic("ababbc", 2));
        Assert.AreEqual(0, Leetcode.Ex395_LongestSubstring_Quadratic("weitong", 2));
        Assert.AreEqual(9, Leetcode.Ex395_LongestSubstring_Quadratic("aabbcaccbdaaadaaaaaaaaaebbbbbbbbb", 3));
        Assert.AreEqual(10, Leetcode.Ex395_LongestSubstring_Quadratic("aabbcaccbdaaadaaaaaaaaaebbbbbbbbbb", 3));
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
    public void Ex399_CalcEquation_DisjointSets()
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
    public void Ex591_IsValid()
    {
        // Tag incomplete
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A></A"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A>/A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("A></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A</A>"));

        // Tag name
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><</A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A></<"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<></>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<AAAAAAAAA></AAAAAAAAA>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<AAAAAAAAAA></AAAAAAAAAA>"));

        // Tag name matching
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A></B>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<AB></BA>"));

        // String Content
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>abc</A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>123</A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>.$#</A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>>>></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A> \t\n</A>"));

        // Invalid Content
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><</A>"));

        // Root tag
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A></A><A></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A></A><B></B>"));

        // Nesting
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><A></A><B></B></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<C><A></A><B></B></C>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><B></B></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><A></A></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><B><A></A></B></A>"));

        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<C><B><A></A></B></C>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<C><B><A></C></B></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<C><B><A></A></B></D>"));

        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A></A></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><A></A>"));

        // String Content and Nesting
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>12<B>34</B>56</A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>12<B><C>3</C>4</B>56<D></D></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A>125<6<D></D></A>"));

        // Cdata
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[]]></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[]></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><![CDTA[]]></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>![CDTA[]]></A>"));

        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[a]]></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[<]]></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[>]]></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[ ]]></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[a]]><![CDATA[b]]></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[<![CDATA[b]]></A>"));

        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA]]></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><![]]></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><!></A>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[<![CDATA[b]]>]]></A>"));
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[<![CDATA[b]]><]]></A>"));

        // Cdata and tags
        Assert.IsFalse(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[</A>]]>"));
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A><![CDATA[</A>]]></A>"));

        // Cdata, tags and content
        Assert.IsTrue(Leetcode.Ex591_IsValid.IsValid("<A>A<A></A><![CDATA[</A>]]>A</A>"));
    }

    [TestMethod]
    public void Ex641_MyCircularDeque()
    {
        var deque = new Ex641_MyCircularDeque(3);
        Assert.AreEqual(true, deque.InsertLast(1));
        Assert.AreEqual(true, deque.InsertLast(2));
        Assert.AreEqual(true, deque.InsertFront(3));
        Assert.AreEqual(false, deque.InsertFront(4));
        Assert.AreEqual(2, deque.GetRear());
        Assert.AreEqual(true, deque.IsFull());
        Assert.AreEqual(true, deque.DeleteLast());
        Assert.AreEqual(true, deque.InsertFront(4));
        Assert.AreEqual(4, deque.GetFront());
        Assert.AreEqual(false, deque.InsertFront(5));
        Assert.AreEqual(false, deque.InsertFront(6));
        Assert.AreEqual(false, deque.InsertLast(7));
        Assert.AreEqual(4, deque.GetFront());
        Assert.AreEqual(1, deque.GetRear());
        Assert.AreEqual(true, deque.DeleteLast());
        Assert.AreEqual(3, deque.GetRear());
        Assert.AreEqual(4, deque.GetFront());
        Assert.AreEqual(true, deque.DeleteLast());
        Assert.AreEqual(true, deque.DeleteFront());
    }

    [TestMethod]
    public void Ex715_RangeModule()
    {
        var range = new Ex715_RangeModule();
        range.AddRange(10, 20);
        range.RemoveRange(14, 16);
        Assert.IsTrue(range.QueryRange(10, 14));
        Assert.IsFalse(range.QueryRange(13, 15));
        Assert.IsTrue(range.QueryRange(16, 17));
        range.AddRange(1, 3);
        range.AddRange(2, 4);
        Assert.IsTrue(range.QueryRange(2, 3));
        range.RemoveRange(2, 3);
        Assert.IsFalse(range.QueryRange(2, 3));
        range.AddRange(6, 10);
        Assert.IsFalse(range.QueryRange(1, 11));
    }

    [TestMethod]
    public void Ex718_FindLength_DP()
    {
        Assert.AreEqual(0, Leetcode.Ex718_FindLength_DP(new[] { 1, 2, 3, 2, 1 }, Array.Empty<int>()));
        Assert.AreEqual(1, Leetcode.Ex718_FindLength_DP(new[] { 1, 2, 3, 2, 1 }, new[] { 1 }));
        Assert.AreEqual(5, Leetcode.Ex718_FindLength_DP(new[] { 1, 2, 3, 2, 1 }, new[] { 1, 2, 3, 2, 1 }));
        Assert.AreEqual(3, Leetcode.Ex718_FindLength_DP(new[] { 1, 2, 3, 2, 1 }, new[] { 3, 2, 1, 4, 7 }));
        Assert.AreEqual(2, Leetcode.Ex718_FindLength_DP(new[] { 1, 2, 3, 2, 1 }, new[] { 3, 2, 4, 4, 7 }));
        Assert.AreEqual(2, Leetcode.Ex718_FindLength_DP(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 2, 1, 1, 2 }));
        Assert.AreEqual(4, Leetcode.Ex718_FindLength_DP(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 2, 1, 1, 2, 1, 1, 1, 3, 1, 1, 1, 1, 4 }));
    }

    [TestMethod]
    public void Ex718_FindLength_DPOptimized()
    {
        Assert.AreEqual(0, Leetcode.Ex718_FindLength_DPOptimized(new[] { 1, 2, 3, 2, 1 }, Array.Empty<int>()));
        Assert.AreEqual(1, Leetcode.Ex718_FindLength_DPOptimized(new[] { 1, 2, 3, 2, 1 }, new[] { 1 }));
        Assert.AreEqual(5, Leetcode.Ex718_FindLength_DPOptimized(new[] { 1, 2, 3, 2, 1 }, new[] { 1, 2, 3, 2, 1 }));
        Assert.AreEqual(3, Leetcode.Ex718_FindLength_DPOptimized(new[] { 1, 2, 3, 2, 1 }, new[] { 3, 2, 1, 4, 7 }));
        Assert.AreEqual(2, Leetcode.Ex718_FindLength_DPOptimized(new[] { 1, 2, 3, 2, 1 }, new[] { 3, 2, 4, 4, 7 }));
        Assert.AreEqual(2, Leetcode.Ex718_FindLength_DPOptimized(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 2, 1, 1, 2 }));
        Assert.AreEqual(4, Leetcode.Ex718_FindLength_DPOptimized(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 2, 1, 1, 2, 1, 1, 1, 3, 1, 1, 1, 1, 4 }));
    }

    [TestMethod]
    public void Ex718_FindLength_DPBottomUp()
    {
        Assert.AreEqual(0, Leetcode.Ex718_FindLength_DPBottomUp(new[] { 1, 2, 3, 2, 1 }, Array.Empty<int>()));
        Assert.AreEqual(1, Leetcode.Ex718_FindLength_DPBottomUp(new[] { 1, 2, 3, 2, 1 }, new[] { 1 }));
        Assert.AreEqual(5, Leetcode.Ex718_FindLength_DPBottomUp(new[] { 1, 2, 3, 2, 1 }, new[] { 1, 2, 3, 2, 1 }));
        Assert.AreEqual(3, Leetcode.Ex718_FindLength_DPBottomUp(new[] { 1, 2, 3, 2, 1 }, new[] { 3, 2, 1, 4, 7 }));
        Assert.AreEqual(2, Leetcode.Ex718_FindLength_DPBottomUp(new[] { 1, 2, 3, 2, 1 }, new[] { 3, 2, 4, 4, 7 }));
        Assert.AreEqual(2, Leetcode.Ex718_FindLength_DPBottomUp(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 2, 1, 1, 2 }));
        Assert.AreEqual(4, Leetcode.Ex718_FindLength_DPBottomUp(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 2, 1, 1, 2, 1, 1, 1, 3, 1, 1, 1, 1, 4 }));
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

    [TestMethod]
    public void Ex729_MyCalendar()
    {
        var calendar = new Ex729_MyCalendar();
        Assert.IsTrue(calendar.Book(1, 3));
        Assert.IsTrue(calendar.Book(5, 7));
        Assert.IsTrue(calendar.Book(-2, 0));
        Assert.IsTrue(calendar.Book(0, 1));
        Assert.IsFalse(calendar.Book(2, 6));
        Assert.IsFalse(calendar.Book(2, 5));
        Assert.IsFalse(calendar.Book(3, 6));
        Assert.IsTrue(calendar.Book(3, 5));
        Assert.IsFalse(calendar.Book(3, 5));
    }

    [TestMethod]
    public void Ex737_AreSentencesSimilarTwo()
    {
        Assert.IsTrue(
            Leetcode.Ex737_AreSentencesSimilarTwo(
                new[] { "great", "acting", "skills" }, 
                new[] { "fine", "drama", "talent" },
                new[] 
                {
                    new[] { "great", "good" }, new[] { "fine", "good" }, new[] { "drama", "acting" },
                    new[] { "skills", "talent" } 
                }));

        Assert.IsTrue(
            Leetcode.Ex737_AreSentencesSimilarTwo(
                new[] { "I", "love", "leetcode" }, 
                new[] { "I", "love", "onepiece" }, 
                new[] 
                { 
                    new[] { "manga", "onepiece" }, new[] { "platform", "anime" }, new[] { "leetcode", "platform" }, 
                    new[] { "anime", "manga" } 
                }));

        Assert.IsFalse(
            Leetcode.Ex737_AreSentencesSimilarTwo(
                new[] { "I", "love", "leetcode" }, 
                new[] { "I", "love", "onepiece" }, 
                new[] 
                { 
                    new[] { "manga", "hunterXhunter" }, new[] { "platform", "anime" }, new[] { "leetcode", "platform" }, 
                    new[] { "anime", "manga" } 
                }));
    }

    [TestMethod]
    public void Ex738_MonotoneIncreasingDigits()
    {
        Assert.AreEqual(9, Leetcode.Ex738_MonotoneIncreasingDigits(10));
        Assert.AreEqual(1234, Leetcode.Ex738_MonotoneIncreasingDigits(1234));
        Assert.AreEqual(299, Leetcode.Ex738_MonotoneIncreasingDigits(332));
        Assert.AreEqual(234999999, Leetcode.Ex738_MonotoneIncreasingDigits(235554586));
        Assert.AreEqual(11, Leetcode.Ex738_MonotoneIncreasingDigits(11));
    }

    [TestMethod]
    public void Ex755_PourWater_Quadratic()
    {
        Assert.IsTrue(new[] { 2, 2, 2, 3, 2, 2, 2 }.SequenceEqual(Leetcode.Ex755_PourWater_Quadratic(new[] { 2, 1, 1, 2, 1, 2, 2 }, 4, 3)));
        Assert.IsTrue(new[] { 2, 3, 3, 4 }.SequenceEqual(Leetcode.Ex755_PourWater_Quadratic(new[] { 1, 2, 3, 4 }, 2, 2)));
        Assert.IsTrue(new[] { 4, 4, 4 }.SequenceEqual(Leetcode.Ex755_PourWater_Quadratic(new[] { 3, 1, 3 }, 5, 1)));
        Assert.IsTrue(new[] { 6, 5, 5, 5, 6, 5, 8, 1, 3, 6 }.SequenceEqual(Leetcode.Ex755_PourWater_Quadratic(new[] { 6, 2, 3, 5, 2, 4, 8, 1, 3, 6 }, 10, 4)));
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
    public void Ex876_MiddleNode()
    {
        Assert.IsTrue(new[] { 1 }.SequenceEqual(Leetcode.Ex876_MiddleNode(BuildListNode(1)).ToArray()));
        Assert.IsTrue(new[] { 3, 4, 5 }.SequenceEqual(Leetcode.Ex876_MiddleNode(BuildListNode(1, 2, 3, 4, 5)).ToArray()));
        Assert.IsTrue(new[] { 4, 5, 6 }.SequenceEqual(Leetcode.Ex876_MiddleNode(BuildListNode(1, 2, 3, 4, 5, 6)).ToArray()));
        Assert.IsTrue(new[] { 4, 5, 6, 7 }.SequenceEqual(Leetcode.Ex876_MiddleNode(BuildListNode(1, 2, 3, 4, 5, 6, 7)).ToArray()));
    }

    [TestMethod]
    public void Ex907_SumSubarrayMins()
    {
        Assert.AreEqual(17, Leetcode.Ex907_SumSubarrayMins(new[] {3,1,2,4}));
        Assert.AreEqual(444, Leetcode.Ex907_SumSubarrayMins(new[] {11,81,94,43,3}));
        Assert.AreEqual(33, Leetcode.Ex907_SumSubarrayMins(new[] { 3, 5, 3, 4 }));
    }

    [TestMethod]
    public void Ex940_DistinctSubseqII()
    {
        Assert.AreEqual(7, Leetcode.Ex940_DistinctSubseqII("abc"));
        Assert.AreEqual(6, Leetcode.Ex940_DistinctSubseqII("aba"));
        Assert.AreEqual(3, Leetcode.Ex940_DistinctSubseqII("aaa"));
        Assert.AreEqual(1057, Leetcode.Ex940_DistinctSubseqII("abcadabccba"));
        Assert.AreEqual(1000000006, Leetcode.Ex940_DistinctSubseqII(
            "yezruvnatuipjeohsymapyxgfeczkevoxipckunlqjauvllfpwezhlzpbkfqazhexabomnlxkmoufneninbxxguuktvupmpfspwxiou" +
            "wlfalexmluwcsbeqrzkivrphtpcoxqsueuxsalopbsgkzaibkpfmsztkwommkvgjjdvvggnvtlwrllcafhfocprnrzfoyehqhrvhpbb" +
            "pxpsvomdpmksojckgkgkycoynbldkbnrlujegxotgmeyknpmpgajbgwmfftuphfzrywarqkpkfnwtzgdkdcyvwkqawwyjuskpvqomfc" +
            "hnlojmeltlwvqomucipcwxkgsktjxpwhujaexhejeflpctmjpuguslmzvpykbldcbxqnwgycpfccgeychkxfopixijeypzyryglutxw" +
            "effyrqtkfrqlhtjweodttchnugybsmacpgperznunffrdavyqgilqlplebbkdopyyxcoamfxhpmdyrtutfxsejkwiyvdwggyhgsdpfx" +
            "pznrccwdupfzlubkhppmasdbqfzttbhfismeamenyukzqoupbzxashwuvfkmkosgevcjnlpfgxgzumktsexvwhylhiupwfwyxotwnxo" +
            "dttsrifgzkkedurayjgxlhxjzlxikcgerptpufocymfrkyayvklsalgmtifpiczwnozmgowzchjiop"));
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
    public void Ex1101_EarliestAcq()
    {
        Assert.AreEqual(20190301, Leetcode.Ex1101_EarliestAcq(
            new[] 
            {
                new[] {20190101,0,1},new[] {20190104,3,4},new[] {20190107,2,3},new[] {20190211,1,5},
                new[] {20190224,2,4},new[] {20190301,0,3},new[] {20190312,1,2},new[] {20190322,4,5}
            }, 6));
        Assert.AreEqual(3, Leetcode.Ex1101_EarliestAcq(
            new[] 
            {
                new[] {0, 2, 0},new[] {1, 0, 1},new[] {3, 0, 3},new[] {4, 1, 2},new[] {7, 3, 1}
            }, 4));
    }

    [TestMethod]
    public void Ex1138_AlphabetBoardPath()
    {
        Assert.AreEqual("DDR!UURRR!!DDD!", Leetcode.Ex1138_AlphabetBoardPath("leet"));
        Assert.AreEqual("RR!DDRR!UUL!R!", Leetcode.Ex1138_AlphabetBoardPath("code"));
        Assert.AreEqual("DDDDD!UURRRR!UUU!DDDDLLLLD!UURRR!R!DLLLLD!", Leetcode.Ex1138_AlphabetBoardPath("ztezstz"));
    }

    [TestMethod]
    public void Ex1152_MostVisitedPattern()
    {
        Assert.IsTrue(new[] { "home", "about", "career" }.SequenceEqual(
            Leetcode.Ex1152_MostVisitedPattern(
                new[] { "joe", "joe", "joe", "james", "james", "james", "james", "mary", "mary", "mary" },
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new[] { "home", "about", "career", "home", "cart", "maps", "home", "home", "about", "career" }
            )));

        Assert.IsTrue(new[] { "a", "b", "a" }.SequenceEqual(
            Leetcode.Ex1152_MostVisitedPattern(
                new[] { "ua", "ua", "ua", "ub", "ub", "ub" },
                new[] { 1, 2, 3, 4, 5, 6 },
                new[] { "a", "b", "a", "a", "b", "c" }
            )));

        Assert.IsTrue(new[] { "a", "b", "b" }.SequenceEqual(
            Leetcode.Ex1152_MostVisitedPattern(
                new[] { "ua", "ua", "ua", "ub", "ub", "ub", "ua", "uc", "uc", "uc", "ub", "ub", "ub", "ua" },
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new[] { "a", "b", "a", "a", "b", "c", "a", "a", "b", "b", "a", "b", "b", "b" }
            )));

        Assert.IsTrue(new[] { "a", "a", "b" }.SequenceEqual(
            Leetcode.Ex1152_MostVisitedPattern(
                new[] { "a", "b", "c", "a", "c", "d", "c", "d", "a", "c", "c" },
                new[] { 9, 5, 8, 2, 10, 3, 1, 7, 11, 4, 6 },
                new[] { "a", "a", "a", "a", "a", "a", "a", "a", "b", "a", "b" }
            )));
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
    public void Ex1293_ShortestPath()
    {
        var maze1 = new[]
        {
            new[] { 0, 0, 0 },
            new[] { 1, 1, 0 },
            new[] { 0, 0, 0 },
            new[] { 0, 1, 1 },
            new[] { 0, 0, 0 }
        };

        Assert.AreEqual(6, Leetcode.Ex1293_ShortestPath(maze1, 1));
        Assert.AreEqual(10, Leetcode.Ex1293_ShortestPath(maze1, 0));
        Assert.AreEqual(6, Leetcode.Ex1293_ShortestPath(maze1, 2));

        var maze2 = new[]
        {
            new[] { 0, 0, 0 },
            new[] { 1, 1, 1 },
            new[] { 0, 0, 0 },
            new[] { 0, 1, 1 },
            new[] { 0, 1, 0 }
        };

        Assert.AreEqual(-1, Leetcode.Ex1293_ShortestPath(maze2, 1));
        Assert.AreEqual(-1, Leetcode.Ex1293_ShortestPath(maze2, 0));
        Assert.AreEqual(6, Leetcode.Ex1293_ShortestPath(maze2, 2));
    }

    [TestMethod]
    public void Ex1423_MaxScore_DP()
    {
        Assert.AreEqual(12, Leetcode.Ex1423_MaxScore_DP(new[] { 1, 2, 3, 4, 5, 6, 1 }, 3));
        Assert.AreEqual(4, Leetcode.Ex1423_MaxScore_DP(new[] { 2, 2, 2 }, 2));
        Assert.AreEqual(55, Leetcode.Ex1423_MaxScore_DP(new[] { 9, 7, 7, 9, 7, 7, 9 }, 7));
    }

    [TestMethod]
    public void Ex1243_TransformArray()
    {
        Assert.IsTrue(new[] { 6, 3, 3, 4 }.SequenceEqual(
            Leetcode.Ex1243_TransformArray(new[] { 6, 2, 3, 4 })));
        Assert.IsTrue(new[] { 1, 4, 4, 4, 4, 5 }.SequenceEqual(
            Leetcode.Ex1243_TransformArray(new[] { 1, 6, 3, 4, 3, 5 })));
        Assert.IsTrue(new[] { 2, 2, 1, 1, 1, 2, 2, 1 }.SequenceEqual(
            Leetcode.Ex1243_TransformArray(new[] { 2, 1, 2, 1, 1, 2, 2, 1 })));
    }

    [TestMethod]
    public void Ex1423_MaxScore_Window()
    {
        Assert.AreEqual(12, Leetcode.Ex1423_MaxScore_Window(new[] { 1, 2, 3, 4, 5, 6, 1 }, 3));
        Assert.AreEqual(4, Leetcode.Ex1423_MaxScore_Window(new[] { 2, 2, 2 }, 2));
        Assert.AreEqual(55, Leetcode.Ex1423_MaxScore_Window(new[] { 9, 7, 7, 9, 7, 7, 9 }, 7));
    }

    [TestMethod]
    public void Ex1443_MinTime()
    {
        Assert.AreEqual(8, Leetcode.Ex1443_MinTime(
            7,
            new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 4 }, new[] { 1, 5 }, new[] { 2, 3 }, new[] { 2, 6 } },
            new[] { false, false, true, false, true, true, false }));
        Assert.AreEqual(6, Leetcode.Ex1443_MinTime(
            7,
            new[] {new[] {0, 1},new[] {0,2},new[] {1,4},new[] {1,5},new[] {2,3},new[] {2,6}},
            new[] {false,false,true,false,false,true,false}));
        Assert.AreEqual(0, Leetcode.Ex1443_MinTime(
            7,
            new[] {new[] {0, 1},new[] {0,2},new[] {1,4},new[] {1,5},new[] {2,3},new[] {2,6}},
            new[] {false,false,false,false,false,false,false}));
        Assert.AreEqual(4, Leetcode.Ex1443_MinTime(
            4,
            new[] { new[] { 0, 2 }, new[] { 0, 3 }, new[] { 1, 2 } },
            new[] { false, true, false, false }));
    }

    [TestMethod]
    public void Ex1443_MinTimeOptimized()
    {
        Assert.AreEqual(8, Leetcode.Ex1443_MinTimeOptimized(
            7,
            new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 4 }, new[] { 1, 5 }, new[] { 2, 3 }, new[] { 2, 6 } },
            new[] { false, false, true, false, true, true, false }));
        Assert.AreEqual(6, Leetcode.Ex1443_MinTimeOptimized(
            7,
            new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 4 }, new[] { 1, 5 }, new[] { 2, 3 }, new[] { 2, 6 } },
            new[] { false, false, true, false, false, true, false }));
        Assert.AreEqual(0, Leetcode.Ex1443_MinTimeOptimized(
            7,
            new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 4 }, new[] { 1, 5 }, new[] { 2, 3 }, new[] { 2, 6 } },
            new[] { false, false, false, false, false, false, false }));
        Assert.AreEqual(4, Leetcode.Ex1443_MinTimeOptimized(
            4,
            new[] { new[] { 0, 2 }, new[] { 0, 3 }, new[] { 1, 2 } },
            new[] { false, true, false, false }));
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
    public void Ex1695_MaximumUniqueSubarray()
    {
        Assert.AreEqual(17, Leetcode.Ex1695_MaximumUniqueSubarray(new[] { 4, 2, 4, 5, 6 }));
        Assert.AreEqual(8, Leetcode.Ex1695_MaximumUniqueSubarray(new[] { 5, 2, 1, 2, 5, 2, 1, 2, 5 }));
        Assert.AreEqual(4, Leetcode.Ex1695_MaximumUniqueSubarray(new[] { 4 }));
        Assert.AreEqual(22, Leetcode.Ex1695_MaximumUniqueSubarray(new[] { 4, 2, 4, 5, 6, 4, 7, 4, 3, 2, 1, 7 }));
        Assert.AreEqual(17, Leetcode.Ex1695_MaximumUniqueSubarray(new[] { 4, 2, 4, 5, 0, 4, 7, 4, 3, 2, 1, 7 }));
    }

    [TestMethod]
    public void Ex1696_MaxResult_DP()
    {
        Assert.AreEqual(7, Leetcode.Ex1696_MaxResult_DP(new[] { 1, -1, -2, 4, -7, 3 }, 2));
        Assert.AreEqual(17, Leetcode.Ex1696_MaxResult_DP(new[] { 10, -5, -2, 4, 0, 3 }, 3));
        Assert.AreEqual(0, Leetcode.Ex1696_MaxResult_DP(new[] { 1, -5, -20, 4, -1, 3, -6, -3 }, 2));
        Assert.AreEqual(198, Leetcode.Ex1696_MaxResult_DP(new[] { 100, -1, -100, -1, 100 }, 2));
    }

    [TestMethod]
    public void Ex1696_MaxResult_DP_BottomUp()
    {
        Assert.AreEqual(7, Leetcode.Ex1696_MaxResult_DP_BottomUp(new[] { 1, -1, -2, 4, -7, 3 }, 2));
        Assert.AreEqual(17, Leetcode.Ex1696_MaxResult_DP_BottomUp(new[] { 10, -5, -2, 4, 0, 3 }, 3));
        Assert.AreEqual(0, Leetcode.Ex1696_MaxResult_DP_BottomUp(new[] { 1, -5, -20, 4, -1, 3, -6, -3 }, 2));
        Assert.AreEqual(198, Leetcode.Ex1696_MaxResult_DP_BottomUp(new[] { 100, -1, -100, -1, 100 }, 2));
    }

    [TestMethod]
    public void Ex1696_MaxResult_WithHeap()
    {
        Assert.AreEqual(7, Leetcode.Ex1696_MaxResult_WithHeap(new[] { 1, -1, -2, 4, -7, 3 }, 2));
        Assert.AreEqual(17, Leetcode.Ex1696_MaxResult_WithHeap(new[] { 10, -5, -2, 4, 0, 3 }, 3));
        Assert.AreEqual(0, Leetcode.Ex1696_MaxResult_WithHeap(new[] { 1, -5, -20, 4, -1, 3, -6, -3 }, 2));
        Assert.AreEqual(198, Leetcode.Ex1696_MaxResult_WithHeap(new[] { 100, -1, -100, -1, 100 }, 2));
    }

    [TestMethod]
    public void Ex1721_SwapNodes()
    {
        Assert.IsTrue(new[] { 1, 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex1721_SwapNodes(BuildListNode(1, 2, 3, 4, 5), 0).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex1721_SwapNodes(BuildListNode(1, 2, 3, 4, 5), 10).ToArray()));

        Assert.IsTrue(new[] { 5, 2, 3, 4, 1 }.SequenceEqual(
            Leetcode.Ex1721_SwapNodes(BuildListNode(1, 2, 3, 4, 5), 1).ToArray()));
        Assert.IsTrue(new[] { 1, 4, 3, 2, 5 }.SequenceEqual(
            Leetcode.Ex1721_SwapNodes(BuildListNode(1, 2, 3, 4, 5), 2).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 3, 4, 5 }.SequenceEqual(
            Leetcode.Ex1721_SwapNodes(BuildListNode(1, 2, 3, 4, 5), 3).ToArray()));
        Assert.IsTrue(new[] { 1, 4, 3, 2, 5 }.SequenceEqual(
            Leetcode.Ex1721_SwapNodes(BuildListNode(1, 2, 3, 4, 5), 4).ToArray()));
        Assert.IsTrue(new[] { 5, 2, 3, 4, 1 }.SequenceEqual(
            Leetcode.Ex1721_SwapNodes(BuildListNode(1, 2, 3, 4, 5), 5).ToArray()));
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
    public void Ex1937_MaxPoints_DPTopDown()
    {
        Assert.AreEqual(9, Leetcode.Ex1937_MaxPoints_DPTopDown(
            new[] { new[] { 1, 2, 3 }, new[] { 1, 5, 1 }, new[] { 3, 1, 1 } }));
        Assert.AreEqual(11, Leetcode.Ex1937_MaxPoints_DPTopDown(
            new[] { new[] { 1, 5 }, new[] { 2, 3 }, new[] { 4, 2 } }));
    }

    [TestMethod]
    public void Ex1987_NumberOfUniqueGoodSubsequences()
    {
        Assert.AreEqual(2, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("001"));
        Assert.AreEqual(2, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("11"));
        Assert.AreEqual(5, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("101"));
        Assert.AreEqual(227, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("101001101110"));
        Assert.AreEqual(227, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("000101001101110"));
        Assert.AreEqual(1, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("0"));
        Assert.AreEqual(1, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("0000"));
        Assert.AreEqual(846803618, Leetcode.Ex1987_NumberOfUniqueGoodSubsequences("1100100010101010100100000111110001111001000010000010010111011"));
    }

    [TestMethod]
    public void Ex2034_StockPrice()
    {
        var stockPrice = new Ex2034_StockPrice();
        stockPrice.Update(1, 10);
        stockPrice.Update(2, 5);
        Assert.AreEqual(5, stockPrice.Current());
        Assert.AreEqual(10, stockPrice.Maximum());
        stockPrice.Update(1, 3);
        Assert.AreEqual(5, stockPrice.Maximum());
        stockPrice.Update(4, 2);
        Assert.AreEqual(2, stockPrice.Minimum());
        stockPrice.Update(2, 8);
        Assert.AreEqual(2, stockPrice.Current());
        Assert.AreEqual(8, stockPrice.Maximum());
        Assert.AreEqual(2, stockPrice.Minimum());
        stockPrice.Update(2, 1);
        Assert.AreEqual(2, stockPrice.Current());
        Assert.AreEqual(3, stockPrice.Maximum());
        Assert.AreEqual(1, stockPrice.Minimum());
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
    public void Ex2096_GetDirections_TwoDfs()
    {
        var tree = BuildTree(5, 1, 2, 3, null, 6, 4, 7, 8, null, 9, 10, null, 11, 12, 13, null, null, null, 14, null, null, null, 15);
        Assert.AreEqual("U", Leetcode.Ex2096_GetDirections_TwoDfs(tree, 7, 3));
        Assert.AreEqual("UUURL", Leetcode.Ex2096_GetDirections_TwoDfs(tree, 7, 6));
        Assert.AreEqual("", Leetcode.Ex2096_GetDirections_TwoDfs(tree, 7, 7));
        Assert.AreEqual("UUURRLL", Leetcode.Ex2096_GetDirections_TwoDfs(tree, 7, 14));
        Assert.AreEqual("UUURLR", Leetcode.Ex2096_GetDirections_TwoDfs(tree, 7, 9));
    }

    [TestMethod]
    public void Ex2095_DeleteMiddle()
    {
        Assert.IsTrue(new[] { 1, 2, 4, 5 }.SequenceEqual(
            Leetcode.Ex2095_DeleteMiddle(BuildListNode(1, 2, 3, 4, 5 )).ToArray()));
        Assert.IsTrue(new[] { 1, 2, 4 }.SequenceEqual(
            Leetcode.Ex2095_DeleteMiddle(BuildListNode(1, 2, 3, 4)).ToArray()));
        Assert.IsTrue(new[] { 1 }.SequenceEqual(
            Leetcode.Ex2095_DeleteMiddle(BuildListNode(1, 2)).ToArray()));
        Assert.IsTrue(new int[] { }.SequenceEqual(
            Leetcode.Ex2095_DeleteMiddle(BuildListNode(1)).ToArray()));
    }

    [TestMethod]
    public void Ex2096_GetDirections_SingleDfs()
    {
        var tree = BuildTree(5, 1, 2, 3, null, 6, 4, 7, 8, null, 9, 10, null, 11, 12, 13, null, null, null, 14, null, null, null, 15);
        Assert.AreEqual("U", Leetcode.Ex2096_GetDirections_SingleDfs(tree, 7, 3));
        Assert.AreEqual("UUURL", Leetcode.Ex2096_GetDirections_SingleDfs(tree, 7, 6));
        Assert.AreEqual("", Leetcode.Ex2096_GetDirections_SingleDfs(tree, 7, 7));
        Assert.AreEqual("UUURRLL", Leetcode.Ex2096_GetDirections_SingleDfs(tree, 7, 14));
        Assert.AreEqual("UUURLR", Leetcode.Ex2096_GetDirections_SingleDfs(tree, 7, 9));
    }

    [TestMethod]
    public void Ex2096_GetDirections_WithStringBuffer()
    {
        var tree = BuildTree(5, 1, 2, 3, null, 6, 4, 7, 8, null, 9, 10, null, 11, 12, 13, null, null, null, 14, null, null, null, 15);
        Assert.AreEqual("U", Leetcode.Ex2096_GetDirections_WithStringBuffer(tree, 7, 3));
        Assert.AreEqual("UUURL", Leetcode.Ex2096_GetDirections_WithStringBuffer(tree, 7, 6));
        Assert.AreEqual("", Leetcode.Ex2096_GetDirections_WithStringBuffer(tree, 7, 7));
        Assert.AreEqual("UUURRLL", Leetcode.Ex2096_GetDirections_WithStringBuffer(tree, 7, 14));
        Assert.AreEqual("UUURLR", Leetcode.Ex2096_GetDirections_WithStringBuffer(tree, 7, 9));
    }

    [TestMethod]
    public void Ex2115_FindAllRecipes()
    {
        Assert.IsTrue(new[] { "bread" }.SequenceEqual(Leetcode.Ex2115_FindAllRecipes(
            new[] { "bread" }, new[] { new[] { "yeast", "flour" } }, new[] { "yeast", "flour", "corn" })));
        Assert.IsTrue(new[] { "bread", "sandwich" }.SequenceEqual(Leetcode.Ex2115_FindAllRecipes(
            new[] { "bread", "sandwich" }, 
            new[] { new[] { "yeast", "flour" }, new[] { "bread", "meat" } }, 
            new[] { "yeast", "flour", "meat" })));
        Assert.IsTrue(new[] { "bread", "sandwich", "burger" }.SequenceEqual(Leetcode.Ex2115_FindAllRecipes(
            new[] { "bread", "sandwich", "burger" }, 
            new[] { new[] { "yeast", "flour" }, new[] { "bread", "meat" }, new[] { "sandwich", "meat", "bread" } }, 
            new[] { "yeast", "flour", "meat" })));
    }

    [TestMethod]
    public void Ex2128_RemoveOnes()
    {
        Assert.IsTrue(Leetcode.Ex2128_RemoveOnes(new[] { new[] { 0, 1, 0 }, new[] { 1, 0, 1 }, new[] { 0, 1, 0 } }));
        Assert.IsFalse(Leetcode.Ex2128_RemoveOnes(new[] { new[] { 1, 1, 0 }, new[] { 0, 0, 0 }, new[] { 0, 0, 0 } }));
        Assert.IsTrue(Leetcode.Ex2128_RemoveOnes(new[] { new[] { 1 } }));
        Assert.IsFalse(Leetcode.Ex2128_RemoveOnes(new[] { new[] { 1, 0, 0 }, new[] { 0, 0, 0 }, new[] { 0, 0, 0 } }));
    }

    [TestMethod]
    public void Ex2158_AmountPainted_SortedSet()
    {
        Assert.IsTrue(new[] { 3, 3, 1 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_SortedSet(new[] { new[] { 1, 4 }, new[] { 4, 7 }, new[] { 5, 8 } })));
        Assert.IsTrue(new[] { 3, 3, 1 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_SortedSet(new[] { new[] { 1, 4 }, new[] { 5, 8 }, new[] { 4, 7 } })));
        Assert.IsTrue(new[] { 4, 0 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_SortedSet(new[] { new[] { 1, 5 }, new[] { 2, 4 } })));
        Assert.IsTrue(new[] { 1, 3, 1, 1, 1, 0, 2, 3, 1 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_SortedSet(new[] { new[] { 3, 4 }, new[] { 4, 7 }, new[] { 5, 8 }, new[] { 1, 2 }, 
                new[] { 1, 4 }, new[] { 1, 5 }, new[] { 1, 10 }, new[] { 12, 15 }, new[] { 15, 16 } })));
    }

    [TestMethod]
    public void Ex2158_AmountPainted_JumpArray()
    {
        Assert.IsTrue(new[] { 3, 3, 1 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_JumpArray(new[] { new[] { 1, 4 }, new[] { 4, 7 }, new[] { 5, 8 } })));
        Assert.IsTrue(new[] { 3, 3, 1 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_JumpArray(new[] { new[] { 1, 4 }, new[] { 5, 8 }, new[] { 4, 7 } })));
        Assert.IsTrue(new[] { 4, 0 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_JumpArray(new[] { new[] { 1, 5 }, new[] { 2, 4 } })));
        Assert.IsTrue(new[] { 1, 3, 1, 1, 1, 0, 2, 3, 1 }.SequenceEqual(
            Leetcode.Ex2158_AmountPainted_JumpArray(new[] { new[] { 3, 4 }, new[] { 4, 7 }, new[] { 5, 8 }, new[] { 1, 2 },
                new[] { 1, 4 }, new[] { 1, 5 }, new[] { 1, 10 }, new[] { 12, 15 }, new[] { 15, 16 } })));
    }

    [TestMethod]
    public void Ex2171_MinimumRemoval()
    {
        Assert.AreEqual(4, Leetcode.Ex2171_MinimumRemoval(new[] { 4, 1, 6, 5 }));
        Assert.AreEqual(7, Leetcode.Ex2171_MinimumRemoval(new[] { 2, 10, 3, 2 }));
        Assert.AreEqual(13, Leetcode.Ex2171_MinimumRemoval(new[] { 4, 1, 6, 5, 2, 1, 1, 8, 5, 4 }));
    }

    [TestMethod]
    public void Ex2171_MinimumRemoval_Optimized()
    {
        Assert.AreEqual(4, Leetcode.Ex2171_MinimumRemoval_Optimized(new[] { 4, 1, 6, 5 }));
        Assert.AreEqual(7, Leetcode.Ex2171_MinimumRemoval_Optimized(new[] { 2, 10, 3, 2 }));
        Assert.AreEqual(13, Leetcode.Ex2171_MinimumRemoval_Optimized(new[] { 4, 1, 6, 5, 2, 1, 1, 8, 5, 4 }));
    }

    [TestMethod]
    public void Ex2172_MaximumANDSum_DP()
    {
        Assert.AreEqual(9, Leetcode.Ex2172_MaximumANDSum_DP(new[] { 1, 2, 3, 4, 5, 6 }, 3));
        Assert.AreEqual(24, Leetcode.Ex2172_MaximumANDSum_DP(new[] { 1, 3, 10, 4, 7, 1 }, 9));
        Assert.AreEqual(60, Leetcode.Ex2172_MaximumANDSum_DP(new[] { 8, 13, 3, 15, 3, 15, 2, 15, 5, 7, 6 }, 8));
    }

    [TestMethod]
    public void Ex2172_MaximumANDSum_DPSpaceOptimized()
    {
        Assert.AreEqual(9, Leetcode.Ex2172_MaximumANDSum_DPSpaceOptimized(new[] { 1, 2, 3, 4, 5, 6 }, 3));
        Assert.AreEqual(24, Leetcode.Ex2172_MaximumANDSum_DPSpaceOptimized(new[] { 1, 3, 10, 4, 7, 1 }, 9));
        Assert.AreEqual(60, Leetcode.Ex2172_MaximumANDSum_DPSpaceOptimized(new[] { 8, 13, 3, 15, 3, 15, 2, 15, 5, 7, 6 }, 8));
    }

    [TestMethod]
    public void Ex2281_TotalStrength()
    {
        Assert.AreEqual(121473332, Leetcode.Ex2281_TotalStrength(
            File.ReadAllText("Resources/Ex2281_Input.txt").Split(",").Select(s => int.Parse(s)).ToArray()));
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

    [TestMethod]
    public void Ex2387_MatrixMedian()
    {
        Assert.AreEqual(2, Leetcode.Ex2387_MatrixMedian(new[] { new[] { 1, 1, 2 }, new[] { 2, 3, 3 }, new[] { 1, 3, 4 } }));
        Assert.AreEqual(1, Leetcode.Ex2387_MatrixMedian(new[] { new[] { 1 } }));
        Assert.AreEqual(3, Leetcode.Ex2387_MatrixMedian(new[] { new[] { 1, 1, 3, 3, 4 } }));
        Assert.AreEqual(3, Leetcode.Ex2387_MatrixMedian(new[] { new[] { 1 }, new[] { 3 }, new[] { 4 } }));
        Assert.AreEqual(4, Leetcode.Ex2387_MatrixMedian(new[] { new[] { 1, 1, 3, 3, 4 }, new[] {0, 5, 5, 5, 6 }, new[] { 2, 2, 4, 4, 4 } }));
    }
}
