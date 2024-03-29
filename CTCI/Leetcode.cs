﻿using System.Collections.Immutable;
using System.Data;
using System.Diagnostics.Metrics;
using System.Text;
using static CTCI.Exercises4;

namespace CTCI;

public static class Leetcode
{
    public static int Ex3_LengthOfLongestSubstring(string s)
    {
        if (s.Length <= 1) return s.Length;

        var i = 0; var j = 0; var max = 1;
        var charsInWindow = new HashSet<char> { s[0] };

        while (j < s.Length - 1)
        {
            if (!charsInWindow.Contains(s[j + 1]))
            {
                charsInWindow.Add(s[j + 1]);
                j++;
                max = Math.Max(max, charsInWindow.Count);
            }
            else if (i <= j)
            {
                charsInWindow.Remove(s[i]);
                i++;
            }
        }

        return max;
    }

    public static double Ex4_FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var n1 = nums1.Length;
        var n2 = nums2.Length;
        var nHalf = (n1 + n2 + 1) >> 1;

        if (n1 > n2)
            return Ex4_FindMedianSortedArrays(nums2, nums1);

        // n1 <= n2. Binary search on the shorter.
        int low = 0, high = n1;
        while (low <= high)
        {
            var m1 = low + ((high - low) >> 1);
            var m2 = nHalf - m1;

            var l1 = m1 == 0 ? int.MinValue : nums1[m1 - 1];
            var r1 = m1 == n1 ? int.MaxValue : nums1[m1];
            var l2 = m2 == 0 ? int.MinValue : nums2[m2 - 1];
            var r2 = m2 == n2 ? int.MaxValue : nums2[m2];

            Console.WriteLine($"low = {low}, high = {high}, m1 = {m1}, m2 = {m2}");
            Console.WriteLine($"l1 = {l1}, r1 = {r1}, l2 = {l2}, r2 = {r2}");

            if (l1 <= r2 && l2 <= r1)
                return (n1 + n2) % 2 == 0 ? (Math.Max(l1, l2) + Math.Min(r1, r2)) / 2.0 : Math.Max(l1, l2);
            if (l1 > r2)
                high = m1 - 1;
            else
                low = m1 + 1;
        }

        return -1;
    }

    public static string Ex5_LongestPalindrome(string s)
    {
        var solutions = new Dictionary<(int, int), (int, int)> { };

        var (start, count) = LongestPalindrome(0, s.Length);
        return s[start..(start + count)];

        (int start, int count) LongestPalindrome(int start, int count)
        {
            if (count <= 1) return (start, count);
            if (count == 2) return s[start] == s[start + 1] ? (start, count) : (start, 1);
            if (solutions.TryGetValue((start, count), out var solution)) return solution;

            if (s[start] == s[start + count - 1])
            {
                var (start3, count3) = LongestPalindrome(start + 1, count - 2);
                if (count3 == count - 2)
                    return (start, count);
            }

            var (start1, count1) = LongestPalindrome(start, count - 1);
            if (count1 == count - 1)
                return (start1, count1);

            var (start2, count2) = LongestPalindrome(start + 1, count - 1);
            solution = count1 >= count2 ? (start1, count1) : (start2, count2);
            solutions[(start, count)] = solution;
            return solution;
        }
    }

    public static int Ex7_Reverse(int x)
    {
        // 10 = 0b1010 => 01 = 0b0001
        var xStr = x.ToString();
        var i = x < 0 ? 1 : 0;
        var result = xStr[i++] - '0';
        var power = 1;
        try
        {
            while (i < xStr.Length)
            {
                checked
                {
                    power *= 10;
                    result += (xStr[i] - '0') * power;
                }

                i++;
            }
        }
        catch (Exception)
        {
            return 0;
        }

        return x <= 0 ? -result : result;
    }

    public static int Ex8_MyAtoi(string s)
    {
        var n = s.Length;

        int i = 0;
        while (i < n && s[i] == ' ') i++;
        if (i == n) return 0;

        int sign = 1;
        if (s[i] == '-') { sign = -1; i++; }
        else if (s[i] == '+') i++;

        long result = 0;
        while (i < n && (s[i] - '0') is var digit && digit >= 0 && digit <= 9)
        {
            result *= 10;
            result += digit;
            if (result > int.MaxValue + 1L) return sign < 0 ? int.MinValue : int.MaxValue;
            i++;
        }

        result *= sign;
        if (result < int.MinValue) return int.MinValue;
        if (result > int.MaxValue) return int.MaxValue;
        return (int)result;
    }

    public static int Ex8_MyAtoiOptimized(string s)
    {
        var n = s.Length;

        int i = 0;
        while (i < n && s[i] == ' ') i++;
        if (i == n) return 0;

        int sign = 1;
        if (s[i] == '-') { sign = -1; i++; }
        else if (s[i] == '+') i++;

        uint result = 0;
        try
        {
            checked
            {
                while (i < n && (s[i] - '0') is var digit && digit >= 0 && digit <= 9)
                {

                    result *= 10;
                    result += (uint)digit;
                    if (result > int.MaxValue + (uint)1) return sign < 0 ? int.MinValue : int.MaxValue;
                    i++;
                }
            }
        }
        catch (Exception)
        {
            return sign < 0 ? int.MinValue : int.MaxValue;
        }

        if (result == int.MaxValue + (uint)1) return sign < 0 ? int.MinValue : int.MaxValue;
        return (int)(sign > 0 ? result : -result);
    }

    public static bool Ex10_IsMatch(string s, string p)
    {
        var n = s.Length;
        var m = p.Length;
        var solutions = new Dictionary<(int, int), bool> { };
        return IsMatch(0, 0);

        bool IsMatch(int i, int j)
        {
            if (i >= n && j >= m) return true;
            if (j >= m) return false;
            if (solutions.TryGetValue((i, j), out var solution)) return solution;

            if (j == m - 1 || p[j + 1] != '*')
            {
                if (i < n && (p[j] == '.' || p[j] == s[i]))
                    solution = IsMatch(i + 1, j + 1);
                else
                    solution = false;
            }
            else
            {
                if (i < n && (p[j] == '.' || p[j] == s[i]))
                    solution = IsMatch(i + 1, j);

                if (!solution)
                    solution = IsMatch(i, j + 2);
            }

            solutions[(i, j)] = solution;
            return solution;
        }
    }

    public static int Ex11_MaxArea(int[] height)
    {
        var n = height.Length;
        var i = 0;
        var j = n - 1;
        var max = (j - i) * Math.Min(height[i], height[j]);
        while (i < j)
        {
            if (height[j] <= height[i])
            {
                max = Math.Max(max, (j - i - 1) * Math.Min(height[i], height[j - 1]));
                j--;
            }
            else
            {
                max = Math.Max(max, (j - i - 1) * Math.Min(height[i + 1], height[j]));
                i++;
            }
        }
        return max;
    }

    public static string Ex12_IntToRoman_Recursive(int num)
    {
        if (num == 0) return string.Empty;
        if (num >= 1000) return "M" + Ex12_IntToRoman_Recursive(num - 1000);
        if (num >= 900) return "C" + Ex12_IntToRoman_Recursive(num + 100);
        if (num >= 500) return "D" + Ex12_IntToRoman_Recursive(num - 500);
        if (num >= 400) return "C" + Ex12_IntToRoman_Recursive(num + 100);
        if (num >= 100) return "C" + Ex12_IntToRoman_Recursive(num - 100);
        if (num >= 90) return "X" + Ex12_IntToRoman_Recursive(num + 10);
        if (num >= 50) return "L" + Ex12_IntToRoman_Recursive(num - 50);
        if (num >= 40) return "X" + Ex12_IntToRoman_Recursive(num + 10);
        if (num >= 10) return "X" + Ex12_IntToRoman_Recursive(num - 10);
        if (num >= 9) return "I" + Ex12_IntToRoman_Recursive(num + 1);
        if (num >= 5) return "V" + Ex12_IntToRoman_Recursive(num - 5);
        if (num >= 4) return "I" + Ex12_IntToRoman_Recursive(num + 1);
        return "I" + Ex12_IntToRoman_Recursive(num - 1);
    }

    public static string Ex12_IntToRoman_Iterative(int num)
    {
        var result = new StringBuilder();

        while (num > 0)
        {
            var (numeral, delta) = (num) switch
            {
                >= 1000 => ('M', -1000),
                >= 900 => ('C', +100),
                >= 500 => ('D', -500),
                >= 400 => ('C', +100),
                >= 100 => ('C', -100),
                >= 90 => ('X', +10),
                >= 50 => ('L', -50),
                >= 40 => ('X', +10),
                >= 10 => ('X', -10),
                >= 9 => ('I', +1),
                >= 5 => ('V', -5),
                >= 4 => ('I', +1),
                _ => ('I', -1),
            };
            result.Append(numeral);
            num += delta;
        }
        return result.ToString();
    }

    public static int Ex13_RomanToInt(string s)
    {
        var n = s.Length;
        return RomanToInt(0);

        int RomanToInt(int i)
        {
            if (i == n) return 0;
            if (s[i] == 'M') return 1000 + RomanToInt(i + 1);
            if (s[i] == 'D') return 500 + RomanToInt(i + 1);
            if (s[i] == 'L') return 50 + RomanToInt(i + 1);
            if (s[i] == 'V') return 5 + RomanToInt(i + 1);

            if (s[i] == 'C')
            {
                if (i < n - 1)
                {
                    if (s[i + 1] == 'M') return 900 + RomanToInt(i + 2);
                    if (s[i + 1] == 'D') return 400 + RomanToInt(i + 2);
                }

                return 100 + RomanToInt(i + 1);
            }

            if (s[i] == 'X')
            {
                if (i < n - 1)
                {
                    if (s[i + 1] == 'C') return 90 + RomanToInt(i + 2);
                    if (s[i + 1] == 'L') return 40 + RomanToInt(i + 2);
                }

                return 10 + RomanToInt(i + 1);
            }

            if (i < n - 1)
            {
                if (s[i + 1] == 'X') return 9 + RomanToInt(i + 2);
                if (s[i + 1] == 'V') return 4 + RomanToInt(i + 2);
            }

            return 1 + RomanToInt(i + 1);
        }
    }

    public static string Ex14_LongestCommonPrefix(string[] strs)
    {
        var n = strs.Length;

        var i = 0;
        while (strs[0].Length > i)
        {
            var c = strs[0][i];
            for (var j = 1; j < n; j++)
                if (strs[j].Length <= i || strs[j][i] != c)
                    return strs[0][0..i];

            i++;
        }
        return strs[0][0..i];
    }

    public static IList<string> Ex17_LetterCombinations(string digits)
    {
        if (digits.Length == 0) return Array.Empty<string>();

        var combinations = new List<string>() { "" };

        for (var i = 0; i < digits.Length; i++)
        {
            var possibleChars = Mappings(digits[i]);
            var newCombinations = new List<string>(combinations.Count * possibleChars.Length);
            foreach (var possibleChar in possibleChars)
                foreach (var combination in combinations)
                    newCombinations.Add(combination + possibleChar);
            combinations = newCombinations;
        }

        return combinations;

        static string Mappings(char c) => c switch
        {
            '2' => "abc",
            '3' => "def",
            '4' => "ghi",
            '5' => "jkl",
            '6' => "mno",
            '7' => "pqrs",
            '8' => "tuv",
            '9' => "wxyz",
        };
    }

    private class ListComparer : IEqualityComparer<IList<int>>
    {
        public bool Equals(IList<int> first, IList<int> second) =>
            first.ToHashSet().SetEquals(second.ToHashSet());
        public int GetHashCode(IList<int> list) =>
            list.Sum();
    }

    public static IList<IList<int>> Ex18_FourSum_DP(int[] nums, int target)
    {
        var n = nums.Length;
        var solutions = new Dictionary<(int, int, int), IList<IList<int>>>();
        return FourSum(0, 4, target).Distinct(new ListComparer()).ToList();

        IList<IList<int>> FourSum(int i, int numberOfAddends, int subTarget)
        {
            if (numberOfAddends == 0)
            {
                if (subTarget == 0)
                    return new List<IList<int>>() { Array.Empty<int>() };
                return Array.Empty<IList<int>>();
            }

            if (solutions.TryGetValue((i, numberOfAddends, subTarget), out var solution))
                return solution;

            var result = new List<IList<int>>();
            for (var j = i; j < n; j++)
            {
                result.AddRange(
                    FourSum(j + 1, numberOfAddends - 1, subTarget - nums[j])
                    .Select(l => l.Prepend(nums[j]).ToList()));
            }

            solutions[(i, numberOfAddends, subTarget)] = result;
            return result;
        }
    }

    public static ListNode Ex19_RemoveNthFromEnd_TreePointers(ListNode head, int n)
    {
        var current = head;
        int i;
        for (i = 0; i < n && current != null; i++)
            current = current.next;

        if (i < n) // Reached end of the list before n items => nothing to remove
            return head;

        ListNode previous = null;
        ListNode toDelete = head;
        while (current != null)
        {
            current = current.next;
            previous = toDelete;
            toDelete = toDelete.next;
        }

        if (previous == null) return head.next;
        previous.next = toDelete.next;
        toDelete.next = null;

        return head;
    }

    public static ListNode Ex19_RemoveNthFromEnd_TwoPointers(ListNode head, int n)
    {
        var current = head;
        int i;
        for (i = 0; i < n + 1 && current != null; i++)
            current = current.next;

        if (i < n) // Reached end of the list before n items => nothing to remove
            return head;
        else if (i == n)
            return head.next;

        ListNode previous = head;
        while (current != null)
        {
            current = current.next;
            previous = previous.next;
        }

        previous.next = previous.next.next;
        return head;
    }

    public static ListNode Ex21_MergeTwoLists(ListNode list1, ListNode list2)
    {
        if (list1 == null) return list2;
        if (list2 == null) return list1;

        ListNode head;
        if (list1.val <= list2.val)
        {
            head = list1;
            list1 = list1.next;
        }
        else
        {
            head = list2;
            list2 = list2.next;
        }

        ListNode current = head;
        while (list1 != null || list2 != null)
        {
            if (list1 == null)
            {
                current.next = list2;
                current = list2;
                list2 = list2.next;
            }
            else if (list2 == null || list1.val <= list2.val)
            {
                current.next = list1;
                current = list1;
                list1 = list1.next;
            }
            else
            {
                current.next = list2;
                current = list2;
                list2 = list2.next;
            }
        }

        return head;
    }

    public static IList<string> Ex22_GenerateParenthesis(int n)
    {
        return GenerateParenthesis(0, 0).ToList();

        IEnumerable<string> GenerateParenthesis(int opened, int closed)
        {
            if (closed == n) { yield return string.Empty; yield break; }
            if (opened == n) { yield return new string(')', n - closed); yield break; }
            if (closed < opened)
                foreach (var s in GenerateParenthesis(opened, closed + 1))
                    yield return ')' + s;
            foreach (var s in GenerateParenthesis(opened + 1, closed))
                yield return '(' + s;
        }
    }

    public static ListNode Ex25_ReverseKGroup_TwoPasses(ListNode head, int k)
    {
        if (k == 1) return head;

        var (last, lastKNode, afterLastKNode) = Reverse(head, k);

        if (lastKNode == null)
        {
            Reverse(last, 1);
            return head;
        }

        if (afterLastKNode != null)
        {
            afterLastKNode.next = null;
            Reverse(last, 1);
        }

        var currentKNode = lastKNode;
        var firstOfGroupBeforeCurrentKNode = MoveKNodes(lastKNode, k - 1);
        var previousKNode = firstOfGroupBeforeCurrentKNode.next;
        firstOfGroupBeforeCurrentKNode.next = afterLastKNode;

        while (currentKNode.next != null)
        {
            var firstPreviousGroup = MoveKNodes(previousKNode, k - 1);

            if (firstPreviousGroup == null)
                break;

            (firstPreviousGroup.next, currentKNode, previousKNode) =
                (currentKNode, previousKNode, firstPreviousGroup.next);
        }

        return currentKNode;

        static ListNode MoveKNodes(ListNode node, int k)
        {
            for (var i = 0; i < k && node != null; i++)
                node = node.next;
            return node;
        }

        static (ListNode last, ListNode lastKNode, ListNode afterLastKNode) Reverse(ListNode head, int k)
        {
            ListNode previous = null, current = head, next = head.next;
            ListNode lastKNode = null, afterLastKNode = null;
            int i = 0;
            while (current != null)
            {
                if ((i + 1) % k == 0)
                {
                    lastKNode = current;
                    afterLastKNode = next;
                }

                current.next = previous;
                previous = current;
                current = next;
                next = current?.next;
                i++;
            }

            return (previous, lastKNode, afterLastKNode);
        }
    }

    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }

        public override string ToString() => val.ToString();
    }

    public static ListNode Ex23_MergeKLists(ListNode[] lists)
    {
        // list index to priority of its first item
        var currentItems = new PriorityQueue<int, int>();
        for (var i = 0; i < lists.Length; i++)
            if (lists[i] != null)
                currentItems.Enqueue(i, lists[i].val);

        if (currentItems.Count == 0)
            return null;

        var result = new ListNode(0);
        var head = result;
        while (currentItems.Count > 0)
        {
            int listIndex = currentItems.Dequeue();
            result.next = new ListNode(lists[listIndex].val);
            result = result.next;

            lists[listIndex] = lists[listIndex].next;
            if (lists[listIndex] != null)
                currentItems.Enqueue(listIndex, lists[listIndex].val);
        }

        return head.next;
    }

    public static ListNode Ex24_SwapPairs(ListNode head)
    {
        if (head?.next == null) return head;

        ListNode current = head, previous = null;
        head = current.next;

        while (current?.next != null)
        {
            var next = current.next;
            var nextNext = current.next.next;

            if (previous != null)
                previous.next = next;
            next.next = current;
            current.next = nextNext;

            previous = current;
            current = nextNext;
        }

        return head;
    }

    public static int Ex26_RemoveDuplicates(int[] nums)
    {
        var j = 0;
        for (var i = 1; i < nums.Length; i++)
        {
            if (nums[j] != nums[i])
            {
                j++;
                nums[j] = nums[i];
            }
        }

        return j + 1;
    }

    public static int Ex27_RemoveElement(int[] nums, int val)
    {
        var n = nums.Length;
        var i = 0;
        for (var j = 0; j < n; j++)
        {
            if (nums[j] != val)
            {
                nums[i] = nums[j];
                i++;
            }
        }

        return i;
    }

    public static int Ex28_StrStr_RabinKarp(string haystack, string needle)
    {
        var n = haystack.Length;
        var m = needle.Length;
        var b = 31;

        if (n < m) return -1;
        int needleHash = 0, runningHash = 0, maxPower = 1;
        for (var i = 0; i < m; i++)
        {
            needleHash = needleHash * b + (needle[i] - 'a');
            runningHash = runningHash * b + (haystack[i] - 'a');
            maxPower = maxPower * b;
        }

        if (needleHash == runningHash && haystack[0..m] == needle)
            return 0;

        Console.WriteLine($"maxPower = {maxPower}");
        Console.WriteLine($"needleHash = {needleHash}");

        for (var i = m; i < n; i++)
        {
            var maxTerm = maxPower * (haystack[i - m] - 'a');
            var newTerm = haystack[i] - 'a';
            runningHash = runningHash * b - maxTerm + newTerm;

            Console.WriteLine($"runningHash = {runningHash}");

            if (needleHash == runningHash && haystack[(i - m + 1)..(i + 1)] == needle)
                return i - m + 1;
        }

        return -1;
    }

    public static int Ex28_StrStr_KMP(string haystack, string needle)
    {
        var n = haystack.Length;
        var m = needle.Length;

        if (n < m || m == 0) return -1;

        var lps = BuildLPS();
        Console.WriteLine($"lps = {string.Join(", ", lps)}");

        int i = 0, j = -1;
        while (i < n && j < m - 1)
        {
            if (haystack[i] == needle[j + 1])
            {
                i++;
                j++;
            }
            else if (j >= 0)
            {
                j = lps[j];
            }
            else
            {
                i++;
            }
        }

        return j == m - 1 ? i - m : -1;

        int[] BuildLPS()
        {
            var lps = new int[m];

            lps[0] = -1;

            int i = 1, j = -1;
            while (i < m)
            {
                if (needle[i] == needle[j + 1])
                {
                    lps[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j >= 0)
                {
                    j = lps[j];
                }
                else
                {
                    lps[i] = j;
                    i++;
                }
            }

            return lps;
        }
    }

    public static int Ex29_Divide_PowersOfTwo(int dividend, int divisor)
    {
        long dividendLong = dividend;
        long divisorLong = divisor;
        bool negative = false;

        if (dividendLong < 0)
        {
            negative = !negative;
            dividendLong = -dividendLong;
        }
        if (divisorLong < 0)
        {
            negative = !negative;
            divisorLong = -divisorLong;
        }

        var twoPowers = new List<long>(33) { 1 };
        var divisorMultipliers = new List<long>(33) { divisorLong };

        while (divisorMultipliers[^1] < dividendLong)
        {
            twoPowers.Add(twoPowers[^1] + twoPowers[^1]);
            divisorMultipliers.Add(divisorMultipliers[^1] + divisorMultipliers[^1]);
        }

        var result = 0L;
        for (var i = divisorMultipliers.Count - 1; i >= 0; i--)
        {
            if (dividendLong >= divisorMultipliers[i])
            {
                dividendLong -= divisorMultipliers[i];
                result += twoPowers[i];
            }
            if (dividendLong == 0)
                break;
        }

        if (negative)
            result = -result;

        return result > int.MaxValue ? int.MaxValue : (result < int.MinValue ? int.MinValue : (int)result);
    }

    public static int Ex29_Divide_BitShifting(int dividend, int divisor)
    {
        bool negative = false;
        uint dividendUnsigned, divisorUnsigned;

        if (dividend < 0)
        {
            negative = !negative;
            dividendUnsigned = (uint)(-(long)dividend);
        }
        else
        {
            dividendUnsigned = (uint)dividend;
        }

        if (divisor < 0)
        {
            negative = !negative;
            divisorUnsigned = (uint)(-(long)divisor);
        }
        else
        {
            divisorUnsigned = (uint)divisor;
        }

        uint divisorMultiplier = divisorUnsigned;
        uint twoPower = 1;

        while (divisorMultiplier < dividendUnsigned)
        {
            divisorMultiplier <<= 1;
            twoPower <<= 1;
        }

        uint result = 0;
        while (divisorMultiplier != 0)
        {
            if (dividendUnsigned >= divisorMultiplier)
            {
                dividendUnsigned -= divisorMultiplier;
                result += twoPower;
            }

            divisorMultiplier >>= 1;
            twoPower >>= 1;
        }

        if (negative)
            return result > int.MaxValue ? int.MinValue : -(int)result;
        return result > int.MaxValue ? int.MaxValue : (int)result;
    }

    public static IList<int> Ex30_FindSubstring_RollingHash(string s, string[] words)
    {
        var wordCount = words.Length;
        var wordLength = words[0].Length;
        var wordsLength = wordCount * wordLength;
        var wordsInString = s.Length / wordLength;

        Array.Sort(words);

        if (s.Length < wordsLength) return Array.Empty<int>();

        // Calculate hash of words
        var wordsHash = 0;
        for (var i = 0; i < wordCount; i++)
            for (var j = 0; j < wordLength; j++)
                wordsHash = wordsHash + (words[i][j] - '0');

        // Calculate first rolling hash
        var rollingHash = 0;
        for (var i = 0; i < wordsLength; i++)
            rollingHash = rollingHash + (s[i] - '0');

        var result = new List<int>();
        if (wordsHash == rollingHash && ActualCheck(0))
            result.Add(0);

        // Calculate following rolling hashes
        for (var i = wordsLength; i < s.Length; i++)
        {
            rollingHash = rollingHash - (s[i - wordsLength] - '0') + (s[i] - '0');

            //Console.WriteLine($"Hash of {s[(i - wordsLength + 1)..(i + 1)]} = {rollingHash}");

            if (wordsHash == rollingHash && ActualCheck(i - wordsLength + 1))
                result.Add(i - wordsLength + 1);
        }

        return result;

        bool ActualCheck(int i) => s[i..(i + wordsLength)]
            .Chunk(wordLength)
            .Select(x => new string(x))
            .OrderBy(x => x)
            .SequenceEqual(words);
    }

    public static void Ex31_NextPermutation_WithSorting(int[] nums)
    {
        var n = nums.Length;

        if (n <= 1) return;

        var maxIndex = -1;
        var i = n - 1;
        while (i >= 0)
        {
            if (maxIndex >= 0 && nums[i] < nums[maxIndex])
                break;
            maxIndex = i--;
        }

        if (i < 0)
        {
            Array.Reverse(nums);
            return;
        }

        var minIndex = i + 1;
        for (var k = i + 1; k < n; k++)
            if (nums[k] > nums[i] && nums[k] < nums[minIndex])
                minIndex = k;
        (nums[i], nums[minIndex]) = (nums[minIndex], nums[i]);

        Array.Sort(nums, i + 1, n - i - 1);
    }

    public static void Ex31_NextPermutation_ReverseOnly(int[] nums)
    {
        var n = nums.Length;

        if (n <= 1) return;

        var maxIndex = -1;
        var i = n - 1;
        while (i >= 0)
        {
            if (maxIndex >= 0 && nums[i] < nums[maxIndex])
                break;
            maxIndex = i--;
        }

        if (i >= 0)
        {
            var minIndex = i + 1;
            for (var k = i + 1; k < n; k++)
                if (nums[k] > nums[i] && nums[k] <= nums[minIndex])
                    minIndex = k;
            (nums[i], nums[minIndex]) = (nums[minIndex], nums[i]);
        }

        Array.Reverse(nums, i + 1, n - i - 1);
    }

    public static int Ex32_LongestValidParentheses(string s)
    {
        var stack = new Stack<int>();
        var maxLength = 0;
        var validIntervals = new Dictionary<int, int>();
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else
            {
                if (stack.Count > 0)
                {
                    var minIndex = stack.Pop();
                    var maxIndex = i;
                    var length = maxIndex - minIndex + 1;

                    if (validIntervals.TryGetValue(minIndex - 1, out var d))
                        length += d;
                    validIntervals[maxIndex] = length;
                    maxLength = Math.Max(maxLength, length);
                }
                else
                {
                    stack.Clear();
                    validIntervals.Clear();
                }
            }
        }

        return maxLength;
    }

    public static int Ex32_LongestValidParentheses_WithArray(string s)
    {
        var stack = new Stack<int>();
        var maxLength = 0;
        var validIntervals = new int[s.Length + 1];
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else
            {
                if (stack.Count > 0)
                {
                    var minIndex = stack.Pop();
                    var maxIndex = i;
                    var length = maxIndex - minIndex + 1 + validIntervals[minIndex];
                    validIntervals[maxIndex + 1] = length;
                    maxLength = Math.Max(maxLength, length);
                }
                else
                {
                    stack.Clear();
                }
            }
        }

        return maxLength;
    }

    public static int Ex32_LongestValidParentheses_TwoCounters(string s)
    {
        int n = s.Length;
        int maxLength = 0;
        int openF = 0, closedF = 0, openB = 0, closedB = 0;
        for (var i = 0; i < n; i++)
        {
            if (s[i] == '(') openF++; else closedF++;
            if (s[n - 1 - i] == '(') openB++; else closedB++;

            if (openF == closedF)
                maxLength = Math.Max(maxLength, openF * 2);
            else if (openF < closedF)
                openF = closedF = 0;

            if (openB == closedB)
                maxLength = Math.Max(maxLength, openB * 2);
            else if (openB > closedB)
                openB = closedB = 0;
        }

        return maxLength;
    }

    public static int Ex33_Search(int[] nums, int target)
    {
        int l = 0, r = nums.Length - 1;
        while (l <= r)
        {
            var m = l + (r - l) / 2;

            if (nums[m] == target) return m;

            if (nums[l] <= nums[r])
            {
                // l ..... m ..... r
                if (target > nums[m]) l = m + 1;
                else r = m - 1;
            }
            else if (nums[l] <= nums[m])
            {
                // l ..... m ..|.. r
                if (target > nums[m] || target <= nums[r]) l = m + 1;
                else r = m - 1;
            }
            else
            {
                // l ..|.. m ..... r
                if (target < nums[m] || target >= nums[l]) r = m - 1;
                else l = m + 1;
            }
        }

        return -1;
    }

    public static int[] Ex34_SearchRange(int[] nums, int target)
    {
        var n = nums.Length;

        if (n == 0) return new[] { -1, -1 };

        int l = 0, r = n - 1;
        while (l <= r)
        {
            var m = l + (r - l) / 2;
            if (target <= nums[m])
                r = m - 1;
            else
                l = m + 1;
        }

        if (l >= n || nums[l] != target) return new[] { -1, -1 };

        var first = l;
        l = first; r = n - 1;
        while (l <= r)
        {
            var m = l + (r - l) / 2;
            if (target >= nums[m])
                l = m + 1;
            else
                r = m - 1;
        }

        return new[] { first, r };
    }

    public static int Ex35_SearchInsert(int[] nums, int target)
    {
        var left = 0;
        var right = nums.Length - 1;
        while (left <= right)
        {
            var middle = left + (right - left) / 2;
            if (nums[middle] == target)
                return middle;
            if (nums[middle] < target)
                left = middle + 1;
            else
                right = middle - 1;
        }
        return left;
    }

    public static bool Ex36_ValidSudoku(char[][] board)
    {
        var n = board.Length;
        var m = (int)Math.Sqrt(n);
        for (var i = 0; i < n; i++)
        {
            var foundInRow = new bool[n];
            var foundInCol = new bool[n];
            var foundInSquare = new bool[n];

            for (var j = 0; j < n; j++)
            {
                if (board[i][j] != '.')
                {
                    var value = board[i][j] - '1';
                    if (value < 0 || value >= n || foundInRow[value])
                        return false;
                    foundInRow[value] = true;
                }
                if (board[j][i] != '.')
                {
                    var value = board[j][i] - '1';
                    if (value < 0 || value >= n || foundInCol[value])
                        return false;
                    foundInCol[value] = true;
                }

                var k = (i / m) * m + j / m;
                var l = (i % m) * m + j % m;
                if (board[k][l] != '.')
                {
                    var value = board[k][l] - '1';
                    if (value < 0 || value >= n || foundInSquare[value])
                        return false;
                    foundInSquare[value] = true;
                }
            }
        }
        return true;
    }

    public static string Ex38_CountAndSay(int n)
    {
        var result = new StringBuilder(1);
        result.Append('1');
        for (var j = 1; j < n; j++)
        {
            var next = new StringBuilder(result.Length * 2);
            var currentDigit = result[0];
            var currentCount = 1;
            for (var i = 1; i < result.Length; i++)
            {
                if (result[i] == currentDigit)
                    currentCount++;
                else
                {
                    next.Append(currentCount);
                    next.Append(currentDigit);
                    currentDigit = result[i];
                    currentCount = 1;
                }
            }
            next.Append(currentCount);
            next.Append(currentDigit);
            result = next;
        }
        return result.ToString();
    }

    public static IList<IList<int>> Ex39_CombinationSum(int[] candidates, int target)
    {
        var n = candidates.Length;
        return Combinations(0, target);

        IList<IList<int>> Combinations(int i, int target)
        {
            if (i == n)
            {
                if (target == 0)
                    return new List<IList<int>>() { new List<int>() };
                return new List<IList<int>>() { };
            }

            var result = new List<IList<int>>();
            var x = candidates[i];
            for (var j = 0; j <= target / x; j++)
            {
                foreach (var subResult in Combinations(i + 1, target - j * x))
                {
                    ((List<int>)subResult).AddRange(Enumerable.Repeat(x, j));
                    result.Add(subResult);
                }
            }

            return result;
        }
    }

    public static IList<IList<int>> Ex39_CombinationSum_Optimized(int[] candidates, int target)
    {
        var n = candidates.Length;
        return Combinations(0, target);

        IList<IList<int>> Combinations(int i, int target)
        {
            if (target == 0)
                return new List<IList<int>>() { new List<int>() };

            if (i == n)
                return new List<IList<int>>() { };

            var result = new List<IList<int>>();
            var x = candidates[i];
            for (var j = 0; j <= target / x; j++)
            {
                foreach (var subResult in Combinations(i + 1, target - j * x))
                {
                    (subResult as List<int>).AddRange(Enumerable.Repeat(x, j));
                    result.Add(subResult);
                }
            }

            return result;
        }
    }

    public static IList<IList<int>> Ex39_CombinationSum_Yield(int[] candidates, int target)
    {
        var n = candidates.Length;
        return Combinations(0, target).ToList();

        IEnumerable<IList<int>> Combinations(int i, int target)
        {
            if (target == 0)
            {
                yield return new List<int>();
                yield break;
            }

            if (i == n || target < 0)
                yield break;

            for (var j = i; j < n; j++)
            {
                foreach (var subResult in Combinations(j, target - candidates[j]))
                {
                    subResult.Add(candidates[j]);
                    yield return subResult;
                }
            }
        }
    }

    public static IList<IList<int>> Ex39_CombinationSum_MutatingCurrent(int[] candidates, int target)
    {
        var n = candidates.Length;
        var results = new List<IList<int>>();
        Combinations(0, target, new List<int>());
        return results;

        void Combinations(int i, int target, IList<int> current)
        {
            if (target == 0)
            {
                results.Add(current.ToList());
                return;
            }

            if (i == n || target < 0)
                return;

            for (var j = i; j < n; j++)
            {
                current.Add(candidates[j]);
                Combinations(j, target - candidates[j], current);
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    public static IList<IList<int>> Ex39_CombinationSum_StackBased(int[] candidates, int target)
    {
        var n = candidates.Length;

        var results = new List<IList<int>>();
        var stack = new Stack<(int, int, IList<int>)>();
        stack.Push((0, target, new List<int>()));

        while (stack.Count > 0)
        {
            var (i, t, current) = stack.Pop();

            if (i == -1)
            {
                current.RemoveAt(current.Count - 1);
                continue;
            }

            current.Add(candidates[i]);

            for (var j = i; j < n; j++)
            {
                if (t == candidates[j])
                {
                    results.Add(current.Append(candidates[j]).Skip(1).ToList());
                    continue;
                }
                else if (t < candidates[j])
                {
                    continue;
                }

                stack.Push((-1, -1, current));
                stack.Push((j, t - candidates[j], current));
            }
        }

        return results;
    }

    public static IList<IList<int>> Ex40_CombinationSum2(int[] candidates, int target)
    {
        var n = candidates.Length;

        Array.Sort(candidates);

        var results = new List<IList<int>>();
        Combinations(0, target, new List<int>());
        return results;

        void Combinations(int i, int target, IList<int> current)
        {
            if (target == 0)
            {
                results.Add(current.ToList());
                return;
            }

            if (i == n || target < 0)
                return;

            var j = i;
            while (j < n - 1 && candidates[j + 1] == candidates[i]) j++;

            Combinations(j + 1, target, current);

            for (var k = 0; k <= j - i; k++)
            {
                current.Add(candidates[i]);
                Combinations(j + 1, target - candidates[i] * (k + 1), current);
            }

            for (var k = 0; k <= j - i; k++)
            {
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    public static int Ex41_FirstMissingPositive(int[] nums)
    {
        for (var i = 0; i < nums.Length; i++)
        {
            var v = nums[i] - 1;

            while (v >= 0 && v < nums.Length && nums[v] != v + 1)
            {
                var t = nums[v] - 1;
                nums[v] = v + 1;
                v = t;
            }
        }

        for (var i = 0; i < nums.Length; i++)
            if (nums[i] != i + 1)
                return i + 1;

        return nums.Length + 1;
    }

    public static int Ex42_Trap_Quadratic(int[] height)
    {
        var n = height.Length;
        if (n < 3) return 0;

        // Find first max (or decreasing start edge)
        var i = 0;
        while (i < n - 1 && height[i] <= height[i + 1])
            i++;

        // Find next max bigger or equal than height[i]
        var j = i + 1;
        var totalWater = 0;
        while (i < n && j < n)
        {
            var currentWater = 0;
            var highestMaxIndex = int.MinValue;
            var highestMaxIndexWater = 0;
            while (j < n)
            {
                Console.WriteLine($"i = {i}, j = {j}, currentWater = {currentWater}, highestMaxIndex = {highestMaxIndex}, totalWater = {totalWater}");

                currentWater += Math.Max(0, height[i] - height[j]);

                var localMax = j < n - 1 && height[j - 1] <= height[j] && height[j] >= height[j + 1];
                var endEdgeIncreasing = j == n - 1 && height[j - 1] <= height[j];
                if ((localMax || endEdgeIncreasing) && height[i] <= height[j])
                {
                    // j points to a local max at least as high as i
                    i = j;
                    j = i + 1;
                    totalWater += currentWater;
                    highestMaxIndex = int.MinValue;
                    break;
                }

                if (highestMaxIndex == int.MinValue || height[highestMaxIndex] < height[j])
                {
                    // j points to a height lower than i, but higher than any other height lower than i
                    highestMaxIndex = j;
                    highestMaxIndexWater = currentWater;
                }

                j++;
            }

            if (highestMaxIndex != int.MinValue)
            {
                totalWater += highestMaxIndexWater - (highestMaxIndex - i) * (height[i] - height[highestMaxIndex]);
                i = highestMaxIndex;
                j = i + 1;
            }
        }

        return totalWater;
    }

    public static int Ex42_TrapDP(int[] height)
    {
        var leftMaxes = new Dictionary<int, int> { };
        var rightMaxes = new Dictionary<int, int> { };

        var totalWater = 0;
        for (var i = 0; i < height.Length; i++)
            totalWater += Trap(i);
        return totalWater;

        int LeftMax(int i)
        {
            if (i == 0) return height[i];
            if (leftMaxes.TryGetValue(i, out var solution)) return solution;
            solution = Math.Max(height[i], LeftMax(i - 1));
            return leftMaxes[i] = solution;
        }

        int RightMax(int i)
        {
            if (i == height.Length - 1) return height[i];
            if (rightMaxes.TryGetValue(i, out var solution)) return solution;
            solution = Math.Max(height[i], RightMax(i + 1));
            return rightMaxes[i] = solution;
        }

        int Trap(int i)
        {
            var leftMax = LeftMax(i);
            var rightMax = RightMax(i);
            var minMax = Math.Min(leftMax, rightMax);
            return Math.Max(0, minMax - height[i]);
        }
    }

    public static int Ex42_TrapDPBottomUp(int[] height)
    {
        var n = height.Length;
        var leftMaxes = new int[n];
        var rightMaxes = new int[n];

        leftMaxes[0] = height[0];
        rightMaxes[n - 1] = height[n - 1];
        for (var i = 1; i < n; i++)
        {
            leftMaxes[i] = Math.Max(leftMaxes[i - 1], height[i]);
            rightMaxes[n - 1 - i] = Math.Max(rightMaxes[n - i], height[n - 1 - i]);
        }

        var totalWater = 0;
        for (var i = 0; i < n; i++)
            totalWater += Math.Max(0, Math.Min(leftMaxes[i], rightMaxes[i]) - height[i]);
        return totalWater;
    }

    public static string Ex43_Multiply_SumNum2Times(string num1, string num2)
    {
        if (num1 == "0" || num2 == "0") return "0";
        if (num1 == "1") return num2;
        if (num2 == "1") return num1;

        var num1List = num1.Reverse().Select(c => (byte)(c - '0')).ToList();
        var num2List = num2.Reverse().Select(c => (byte)(c - '0')).ToList();
        var result = num1List.ToList();
        var count = new List<byte> { 1 };
        var one = new List<byte> { 1 };
        while (!count.SequenceEqual(num2List))
        {
            Sum(result, num1List);
            Sum(count, one);
        }

        result.Reverse();
        return string.Join("", result.Select(b => (char)(b + '0')));

        void Sum(IList<byte> num1, IList<byte> num2)
        {
            var n = num1.Count;
            var m = num2.Count;

            // Assuming num1 is longer or equal than num2
            var minLength = m;
            var maxLength = n;

            byte reminder = 0;
            int i = 0;
            while (i < maxLength)
            {
                var digitSum = num1[i] + (i < minLength ? num2[i] : 0) + reminder;
                reminder = (byte)(digitSum / 10);
                num1[i] = (byte)(digitSum % 10);
                i++;
            }

            if (reminder != 0)
                num1.Add(reminder);
        }
    }

    public static string Ex43_Multiply_LongMultiplication(string num1, string num2)
    {
        if (num1 == "0" || num2 == "0") return "0";
        if (num1 == "1") return num2;
        if (num2 == "1") return num1;

        var num1List = num1.Reverse().Select(c => (byte)(c - '0')).ToList();
        var num2List = num2.Reverse().Select(c => (byte)(c - '0')).ToList();
        var result = new List<byte> { 0 };

        var m = num2List.Count;
        for (var i = 0; i < m; i++)
        {
            var digitProduct = Enumerable.Repeat((byte)0, i).Concat(DigitMultiply(num1List, num2List[i])).ToList();
            result = Sum(result, digitProduct);
        }

        result.Reverse();
        return string.Join("", result.Select(b => (char)(b + '0')));

        List<byte> DigitMultiply(List<byte> num, byte digit)
        {
            var n = num.Count;

            var result = new List<byte>(num.Count + 1);
            byte reminder = 0;
            int i = 0;
            while (i < n)
            {
                var digitProduct = num[i] * digit + reminder;
                result.Add((byte)(digitProduct % 10));
                reminder = (byte)(digitProduct / 10);
                i++;
            }

            if (reminder != 0)
                result.Add(reminder);

            return result;
        }

        List<byte> Sum(List<byte> num1, List<byte> num2)
        {
            var n = num1.Count;
            var m = num2.Count;
            var maxLength = Math.Max(n, m);

            var result = new List<byte>(maxLength + 1);
            byte reminder = 0;
            int i = 0;
            while (i < maxLength)
            {
                var digitSum = (i < n ? num1[i] : 0) + (i < m ? num2[i] : 0) + reminder;
                reminder = (byte)(digitSum / 10);
                result.Add((byte)(digitSum % 10));
                i++;
            }

            if (reminder != 0)
                result.Add(reminder);

            return result;
        }
    }

    public static string Ex43_Multiply_Grid(string num1, string num2)
    {
        if (num1 == "0" || num2 == "0") return "0";
        if (num1 == "1") return num2;
        if (num2 == "1") return num1;

        var n = num1.Length;
        var m = num2.Length;
        var maxLength = Math.Max(n, m);

        var sumOfProducts = 0;
        var result = new char[n + m];
        for (var i = 0; i < n + m; i++)
        {
            for (var j = 0; j <= i; j++)
            {
                var firstDigit = j < n ? num1[n - 1 - j] - '0' : 0;
                var secondDigit = i - j < m ? num2[m - 1 - i + j] - '0' : 0;
                sumOfProducts += firstDigit * secondDigit;
            }
            result[n + m - 1 - i] = (char)((sumOfProducts % 10) + '0');
            sumOfProducts = sumOfProducts / 10;
        }

        var startIndex = 0;
        while (result[startIndex] == '0')
            startIndex++;

        return new string(result, startIndex, n + m - startIndex);
    }

    public static int Ex45_Jump(int[] nums)
    {
        var n = nums.Length;
        var solutions = new Dictionary<int, int> { };
        return Jump(0);

        int Jump(int p)
        {
            if (p == n - 1) return 0;
            if (solutions.TryGetValue(p, out var solution)) return solution;

            solution = int.MaxValue;

            for (var i = 1; i <= nums[p] && p + i < n; i++)
            {
                var jumps = Jump(p + i);
                if (jumps != int.MaxValue && jumps + 1 < solution)
                    solution = 1 + jumps;
            }

            solutions[p] = solution;
            return solution;
        }
    }

    public static int Ex45_JumpFaster(int[] nums)
    {
        var n = nums.Length;
        var solutions = new int[n - 1];
        return Jump(0);

        int Jump(int p)
        {
            if (p == n - 1) return 0;
            if (solutions[p] != 0) return solutions[p];

            var solution = int.MaxValue;

            for (var i = 1; i <= nums[p] && p + i < n; i++)
            {
                var jumps = Jump(p + i);
                if (jumps != int.MaxValue && jumps + 1 < solution)
                    solution = 1 + jumps;
            }

            solutions[p] = solution;
            return solution;
        }
    }

    public static int Ex45_JumpFastest(int[] nums)
    {
        var farthest = 0;
        var jumpMax = 0;
        var breath = 0;
        for (var i = 0; i < nums.Length - 1; i++)
        {
            farthest = Math.Max(farthest, i + nums[i]);

            if (i == jumpMax)
            {
                breath++;
                jumpMax = farthest; 
            }
        }

        return breath;
    }

    public static IList<IList<int>> Ex46_Permute_Intersperse(int[] nums)
    {
        var n = nums.Length;

        return Permute(0).ToList();

        IEnumerable<IList<int>> Permute(int i)
        {
            if (i == n) yield break;
            if (i == n - 1)
            {
                yield return new List<int> { nums[^1] };
                yield break;
            }

            var subPermutations = Permute(i + 1).ToList();

            for (var j = 0; j <= n - i - 1; j++)
                foreach (var subPermutation in subPermutations)
                    yield return subPermutation
                        .Take(j)
                        .Append(nums[i])
                        .Concat(subPermutation.Skip(j))
                        .ToList();
        }
    }

    public static IList<IList<int>> Ex46_Permute_Swapping(int[] nums)
    {
        var n = nums.Length;
        var permutations = new List<IList<int>>();
        Permute(0);
        return permutations;

        void Permute(int i)
        {
            if (i == n - 1)
            {
                permutations.Add(nums.ToList());
                return;
            }

            for (var j = i; j < n; j++)
            {
                (nums[i], nums[j]) = (nums[j], nums[i]);
                Permute(i + 1);
                (nums[i], nums[j]) = (nums[j], nums[i]);
            }
        }
    }

    public static IList<IList<int>> Ex47_PermuteUnique(int[] nums)
    {
        var n = nums.Length;
        var permutations = new List<IList<int>>();
        PermuteUnique(0);
        return permutations;

        void PermuteUnique(int i)
        {
            if (i == n - 1)
            {
                permutations.Add(nums.ToList());
                return;
            }

            var values = new HashSet<int>();

            for (var k = i; k < n; k++)
            {
                if (values.Contains(nums[k]))
                    continue;
                values.Add(nums[k]);

                (nums[i], nums[k]) = (nums[k], nums[i]);
                PermuteUnique(i + 1);
                (nums[i], nums[k]) = (nums[k], nums[i]);
            }
        }
    }

    public static IList<IList<int>> Ex47_PermuteUnique_Optimized(int[] nums)
    {
        var n = nums.Length;
        var permutations = new List<IList<int>>();
        PermuteUnique(0);
        return permutations;

        void PermuteUnique(int i)
        {
            if (i == n - 1)
            {
                permutations.Add(nums.ToList());
                return;
            }

            PermuteUnique(i + 1);

            var values = 1 << (nums[i] + 10);
            for (var k = i + 1; k < n; k++)
            {
                if (((values >> (nums[k] + 10)) & 1) != 0)
                    continue;
                values |= 1 << (nums[k] + 10);

                (nums[i], nums[k]) = (nums[k], nums[i]);
                PermuteUnique(i + 1);
                (nums[i], nums[k]) = (nums[k], nums[i]);
            }
        }
    }

    public static void Ex48_Rotate(int[][] matrix)
    {
        var m = matrix;
        var n = matrix.Length;
        for (var i = 0; i < n / 2 + n % 2; i++)
            for (var j = 0; j < n / 2; j++)
            {
                (m[i][j], m[j][n - 1 - i], m[n - 1 - i][n - 1 - j], m[n - 1 - j][i]) =
                (m[n - 1 - j][i], m[i][j], m[j][n - 1 - i], m[n - 1 - i][n - 1 - j]);
            }
    }

    public static IList<IList<string>> Ex49_GroupAnagrams(string[] strs)
    {
        var result = new Dictionary<string, IList<string>>();

        foreach (var str in strs)
        {
            var key = string.Join("", str.OrderBy(c => c));
            if (!result.TryGetValue(key, out var value))
                result[key] = value = new List<string>();
            value.Add(str);
        }

        return result.Select(kvp => kvp.Value).ToList();
    }

    public static double Ex50_MyPow(double x, int n)
    {
        if (n == 0) return 1.0;

        bool overflow = false;
        if (n == int.MinValue)
        {
            overflow = true;
            n++;
        }

        bool negative = false;
        if (n < 0)
        {
            negative = true;
            n = -n;
        }

        var powers = new double[(int)Math.Ceiling(Math.Log(n, 2)) + 1];
        powers[0] = x;

        for (int i = 1; i < powers.Length; i++)
            powers[i] = powers[i - 1] * powers[i - 1];

        int j = 0;
        double result = 1.0;
        while (n != 0)
        {
            if ((n & 1) != 0)
                result *= powers[j];
            n >>= 1;
            j++;
        }

        return (negative ? 1 / result : result) * (overflow ? 1.0 / x : 1.0);
    }

    public static IList<IList<string>> Ex51_SolveNQueens_Recursive(int n)
    {
        var solutions = SolveNQueens(n - 1);
        return Convert(solutions).ToList();

        IEnumerable<IList<string>> Convert(IList<IList<(int, int)>> configurations)
        {
            foreach (var configuration in configurations)
            {
                var strConfiguration = new List<string>();
                for (var i = 0; i < configuration.Count; i++)
                {
                    var qPosition = configuration[i].Item2;
                    strConfiguration.Add(
                        new string('.', qPosition) + 'Q' + new string('.', n - 1 - qPosition));
                }

                yield return strConfiguration;
            }
        }

        IList<IList<(int, int)>> SolveNQueens(int i)
        {
            if (i == 0) return Enumerable
                .Range(0, n)
                .Select(j => new List<(int, int)> { (i, j) } as IList<(int, int)>)
                .ToList();

            var configurations = SolveNQueens(i - 1);
            var result = new List<IList<(int, int)>> { };
            for (var q = 0; q < n; q++) // Queen in position (i, q)
            {
                foreach (var configuration in configurations)
                {
                    if (configuration.All(position =>
                        position.Item2 != q &&
                        position.Item1 + position.Item2 != i + q &&
                        position.Item1 - position.Item2 != i - q))
                    {
                        result.Add(configuration.Append((i, q)).ToList());
                    }
                }
            }

            return result;
        }
    }

    public static IList<IList<string>> Ex51_SolveNQueens_Iterative(int n)
    {
        var configurations = new List<IList<string>>();
        for (var q = 0; q < n; q++)
            configurations.Add(new List<string> { Line(q) });

        for (var i = 1; i < n; i++)
        {
            var newConfigurations = new List<IList<string>>();
            foreach (var configuration in configurations)
            {
                for (var q = 0; q < n; q++)
                {
                    if (Compatible(i, q, configuration))
                        newConfigurations.Add(configuration.Append(Line(q)).ToList());
                }
            }
            configurations = newConfigurations;
        }

        return configurations;

        bool Compatible(int row, int col, IList<string> configuration)
        {
            for (var lineIndex = 0; lineIndex < configuration.Count; lineIndex++)
            {
                var configurationLine = configuration[lineIndex];
                if (
                    configurationLine[col] == 'Q' ||
                    (col + row - lineIndex < n && configurationLine[col + row - lineIndex] == 'Q' ||
                    (col - row + lineIndex >= 0 && configurationLine[col - row + lineIndex] == 'Q')))
                    return false;
            }
            return true;
        }

        string Line(int q)
        {
            var stringBuilder = new StringBuilder(n);
            stringBuilder.Append('.', q);
            stringBuilder.Append('Q');
            stringBuilder.Append('.', n - 1 - q);
            return stringBuilder.ToString();
        }
    }

    public static int Ex53_MaximumSubarray_Quadratic(int[] nums)
    {
        var csums = new int[nums.Length + 1];
        var sum = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            csums[i] = sum;
            sum += nums[i];
        }
        csums[^1] = sum;

        var result = int.MinValue;
        for (var i = 0; i < nums.Length; i++)
        {
            for (var j = i; j < nums.Length; j++)
            {
                sum = csums[j + 1] - csums[i];
                result = Math.Max(result, sum);
            }
        }

        return result;
    }

    public static long Ex53_MaximumSubarray_Kadane(int[] nums)
    {
        long max = long.MinValue;
        long maxSum = 0;
        long currentSum = 0;

        for (var i = 0; i < nums.Length; i++)
        {
            currentSum = Math.Max(0, currentSum + nums[i]);
            maxSum = Math.Max(maxSum, currentSum);
            max = Math.Max(max, nums[i]);
        }

        return max > 0 ? maxSum : max;
    }

    public static IList<int> Ex54_SpiralOrder(int[][] matrix)
    {
        int n = matrix.Length, m = matrix[0].Length;

        var result = new List<int>(n * m);
        for (var r = 0; result.Count < n * m; r++)
        {
            for (var j = r; j < m - r; j++)
                result.Add(matrix[r][j]);
            if (result.Count == n * m) break;
            for (var i = r + 1; i < n - r; i++)
                result.Add(matrix[i][m - r - 1]);
            if (result.Count == n * m) break;
            for (var j = m - r - 2; j > r; j--)
                result.Add(matrix[n - r - 1][j]);
            if (result.Count == n * m) break;
            for (var i = n - r - 1; i > r; i--)
                result.Add(matrix[i][r]);
        }
        return result;
    }

    public static bool Ex55_CanJump_DPMemoized(int[] nums)
    {
        var n = nums.Length;
        var solutions = new Dictionary<int, bool>();
        return CanJump(0);

        bool CanJump(int i)
        {
            if (i == n - 1) return true;
            if (solutions.TryGetValue(i, out var solution)) return solution;

            solution = false;
            for (var j = 1; j <= nums[i]; j++)
            {
                if (CanJump(i + j))
                {
                    solution = true;
                    break;
                }
            }

            solutions[i] = solution;
            return solution;
        }
    }

    public static int[][] Ex56_Merge(int[][] intervals)
    {
        var events = intervals
            .SelectMany(i => new[] { (i[0], 1), (i[1], -1) })
            .OrderBy(i => (i.Item1, -i.Item2));

        var results = new List<int[]> { };
        var n = 0;
        var currentIntervalLeft = int.MinValue;
        foreach (var event1 in events)
        {
            n += event1.Item2;
            if (currentIntervalLeft == int.MinValue && n == 1)
            {
                currentIntervalLeft = event1.Item1;
            }
            else if (currentIntervalLeft != int.MinValue && n == 0)
            {
                results.Add(new[] { currentIntervalLeft, event1.Item1 });
                currentIntervalLeft = int.MinValue;
            }
        }

        return results.ToArray();
    }

    private class IntervalsComparer : IComparer<int[]>
    {
        public int Compare(int[] x, int[] y) => x[0] - y[0];
    }

    public static int[][] Ex56_Merge_InputSort(int[][] intervals)
    {
        Array.Sort(intervals, new IntervalsComparer());

        var results = new List<int[]> { };
        var currentMin = intervals[0][0];
        var currentMax = intervals[0][1];
        for (var i = 1; i < intervals.Length; i++)
        {
            if (currentMax >= intervals[i][0])
            {
                currentMax = Math.Max(currentMax, intervals[i][1]);
            }
            else
            {
                results.Add(new[] { currentMin, currentMax });
                currentMin = intervals[i][0];
                currentMax = intervals[i][1];
            }
        }

        results.Add(new[] { currentMin, currentMax });

        return results.ToArray();
    }

    public static int[][] Ex57_Insert_WithSortedSet(int[][] intervals, int[] newInterval)
    {
        var n = intervals.Length;

        var events = new SortedSet<(int, int)>();
        events.Add((newInterval[0], -2));
        events.Add((newInterval[1], +2));
        foreach (var interval in intervals)
        {
            events.Add((interval[0], -1));
            events.Add((interval[1], +1));
        }

        var result = new List<int[]>(n + 1);
        var numberOfIntervals = 0;
        var start = -1;
        foreach (var (value, type) in events)
        {
            numberOfIntervals -= type;
            if (numberOfIntervals == 0)
            {
                result.Add(new[] { start, value });
                start = -1;
            }
            else if (start < 0)
            {
                start = value;
            }
        }

        return result.ToArray();
    }

    public static int[][] Ex57_Insert_WithSortedSetAndBinarySearchCuts(int[][] intervals, int[] newInterval)
    {
        var n = intervals.Length;

        int a = FindInterval(newInterval[0]);
        int b = FindInterval(newInterval[1]);

        //Console.WriteLine($"a = {a}, b = {b}");

        var result = new List<int[]>(n + 1);

        for (var i = 0; i < a; i++)
            result.Add(intervals[i]);

        var events = new SortedSet<(int, int)>();
        events.Add((newInterval[0], -2));
        events.Add((newInterval[1], +2));
        for (var i = Math.Max(a, 0); i <= b; i++)
        {
            events.Add((intervals[i][0], -1));
            events.Add((intervals[i][1], +1));
        }

        var numberOfIntervals = 0;
        var start = -1;
        foreach (var (value, type) in events)
        {
            numberOfIntervals -= type;
            if (numberOfIntervals == 0)
            {
                result.Add(new[] { start, value });
                start = -1;
            }
            else if (start < 0)
            {
                start = value;
            }
        }

        for (var i = b + 1; i < n; i++)
            result.Add(intervals[i]);

        return result.ToArray();

        int FindInterval(int value)
        {
            int left = 0, right = intervals.Length - 1;
            while (left <= right)
            {
                var middle = left + (right - left) / 2;
                if (intervals[middle][0] <= value && intervals[middle][1] >= value)
                    return middle;

                if (intervals[middle][0] < value)
                    left = middle + 1;
                else
                    right = middle - 1;
            }

            return right;
        }
    }

    public static int Ex58_LengthOfLastWord(string s)
    {
        var i = s.Length - 1;
        while (i >= 0 && s[i] == ' ') i--;
        var j = i;
        while (i >= 0 && s[i] != ' ') i--;
        return j - i;
    }

    public static int[] Ex66_PlusOne(int[] digits)
    {
        int reminder = 1, digitSum = 0;
        for (var i = digits.Length - 1; i >= 0; i--)
        {
            digitSum = digits[i] + reminder;
            digits[i] = digitSum % 10;
            reminder = digitSum / 10;
        }

        if (reminder == 0) return digits;
        var result = new int[digits.Length + 1];
        result[0] = reminder;
        Array.Copy(digits, 0, result, 1, digits.Length);
        return result;
    }

    public static string Ex67_AddBinary(string a, string b)
    {
        var i = a.Length - 1;
        var j = b.Length - 1;

        var reminder = 0;
        var result = new char[Math.Max(i + 1, j + 1) + 1];
        var k = result.Length - 1;
        while (i >= 0 || j >= 0 || reminder > 0)
        {
            //Console.WriteLine($"i = {i}, j = {j}, k = {k}, reminder = {reminder}");
            var digitSum = (i >= 0 ? a[i--] - '0' : 0) + (j >= 0 ? b[j--] - '0' : 0) + reminder;
            result[k--] = (char)('0' + (digitSum & 1));
            reminder = digitSum >> 1;
        }

        return new string(result, k + 1, result.Length - k - 1);
    }

    public static IList<string> Ex68_FullJustify(string[] words, int maxWidth)
    {
        var result = new List<string>();

        int indexFirstWordOfLine = 0;
        int currentLineCharsWithoutSpaces = words[0].Length;
        int currentLineChars = words[0].Length;
        for (var i = 1; i < words.Length; i++)
        {
            if (currentLineChars + 1 + words[i].Length > maxWidth)
            {
                if (i - indexFirstWordOfLine == 1)
                {
                    result.Add(words[indexFirstWordOfLine] + new string(' ', maxWidth - words[indexFirstWordOfLine].Length));
                }
                else
                {
                    var equiSpaces = (maxWidth - currentLineCharsWithoutSpaces) / (i - indexFirstWordOfLine - 1);
                    var numberOfWordsWithAdditionalSpaces = maxWidth - currentLineCharsWithoutSpaces - equiSpaces * (i - indexFirstWordOfLine - 1);

                    for (var j = indexFirstWordOfLine; j < indexFirstWordOfLine + numberOfWordsWithAdditionalSpaces; j++)
                        words[j] += " ";
                    result.Add(string.Join(new string(' ', equiSpaces), words[indexFirstWordOfLine..i]));
                }
                indexFirstWordOfLine = i;
                currentLineChars = words[i].Length;
                currentLineCharsWithoutSpaces = words[i].Length;
            }
            else
            {
                currentLineChars = currentLineChars + 1 + words[i].Length; ;
                currentLineCharsWithoutSpaces = currentLineCharsWithoutSpaces + words[i].Length;
            }
        }

        var lastLine = string.Join(' ', words[indexFirstWordOfLine..]);
        result.Add(lastLine + new string(' ', maxWidth - lastLine.Length));
        return result;
    }

    public static int Ex69_MySqrt(int x)
    {
        if (x <= 1) return x;
        var low = 1;
        var hi = x / 2;
        while (low <= hi)
        {
            var middle = low + (hi - low) / 2;
            var middleSquare = (long)middle * middle;
            if (middleSquare == x) return middle;
            if (middleSquare < x)
                low = middle + 1;
            else
                hi = middle - 1;
        }

        return hi;
    }

    public static int Ex70_ClimbStairs(int n)
    {
        var solutions = new Dictionary<int, int>();
        return ClimbStairsMemoized(n);

        int ClimbStairsMemoized(int n)
        {
            if (n <= 1) return 1;
            if (solutions.TryGetValue(n, out var solution)) return solution;
            solution = ClimbStairsMemoized(n - 1) + ClimbStairsMemoized(n - 2);
            solutions[n] = solution;
            return solution;
        }
    }

    public static string Ex71_SimplifyPath(string path)
    {
        var n = path.Length;

        var result = new char[n];
        result[0] = '/';

        int j = 1, previousSlash = 0;
        for (int i = 1; i <= n; i++)
        {
            if (i < n && path[i] != '/')
                continue;

            if (i - previousSlash > 1)
            {
                bool skip;
                if (i - previousSlash == 2 && path[i - 1] == '.')
                {
                    skip = true;
                }
                else if (i - previousSlash == 3 && path[i - 1] == '.' && path[i - 2] == '.')
                {
                    skip = true;
                    if (j > 0)
                    {
                        do j--; while (result[j] != '/');
                    }
                }
                else
                {
                    skip = false;
                }

                if (!skip)
                {
                    if (j == 0 || result[j - 1] != '/')
                        result[j++] = '/';
                    for (var k = previousSlash + 1; k < i; k++)
                        result[j++] = path[k];
                }
            }

            previousSlash = i;
        }

        return new string(result, 0, j > 0 ? j : 1);
    }

    public static int Ex72_MinDistance_DP(string word1, string word2)
    {
        var solutions = new Dictionary<(int, int), int> { };
        var n = word1.Length;
        var m = word2.Length;
        return MinDistance(0, 0);

        int MinDistance(int i, int j)
        {
            if (i == n) return m - j;
            if (j == m) return n - i;
            if (solutions.TryGetValue((i, j), out var solution)) return solution;

            // Match or Replace
            solution = (word1[i] == word2[j] ? 0 : 1) + MinDistance(i + 1, j + 1);
            // Insert 
            solution = Math.Min(solution, 1 + MinDistance(i, j + 1));
            // Delete
            solution = Math.Min(solution, 1 + MinDistance(i + 1, j));

            solutions[(i, j)] = solution;
            return solution;
        }
    }

    public static int Ex72_MinDistance_DPBottomUp(string word1, string word2)
    {
        var n = word1.Length;
        var m = word2.Length;
        var solutions = new int[n + 1, m + 1];

        for (var j = 0; j <= m; j++)
            solutions[n, j] = m - j;
        for (var i = 0; i <= n; i++)
            solutions[i, m] = n - i;

        for (var i = n - 1; i >= 0; i--)
            for (var j = m - 1; j >= 0; j--)
                solutions[i, j] = Math.Min(
                    (word1[i] == word2[j] ? 0 : 1) + solutions[i + 1, j + 1],
                    1 + Math.Min(solutions[i, j + 1], solutions[i + 1, j])
                );
        return solutions[0, 0];
    }

    public static void Ex73_SetZeroes_LinearSpace(int[][] matrix)
    {
        var n = matrix.Length;
        var m = matrix[0].Length;
        var row = new bool[n];
        var col = new bool[m];
        for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
                if (matrix[i][j] == 0)
                {
                    row[i] = true;
                    col[j] = true;
                }

        for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
                if (row[i] || col[j])
                    matrix[i][j] = 0;

    }

    public static void Ex73_SetZeroes_ConstantSpace(int[][] matrix)
    {
        var n = matrix.Length;
        var m = matrix[0].Length;

        var firstRowNeedZeros = false;
        for (var j = 0; j < m; j++)
            if (matrix[0][j] == 0)
            {
                firstRowNeedZeros = true;
                break;
            }

        var firstColNeedZeros = false;
        for (var i = 0; i < n; i++)
            if (matrix[i][0] == 0)
            {
                firstColNeedZeros = true;
                break;
            }

        for (var i = 1; i < n; i++)
            for (var j = 1; j < m; j++)
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = 0;
                    matrix[0][j] = 0;
                }

        for (var i = 1; i < n; i++)
            for (var j = 1; j < m; j++)
                if (matrix[i][0] == 0 || matrix[0][j] == 0)
                    matrix[i][j] = 0;

        if (firstRowNeedZeros)
            for (var j = 0; j < m; j++)
                matrix[0][j] = 0;

        if (firstColNeedZeros)
            for (var i = 0; i < n; i++)
                matrix[i][0] = 0;
    }

    public static bool Ex74_SearchMatrix(int[][] matrix, int target)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        int start = 0, end = n * m - 1;
        while (start <= end)
        {
            var middle = start + (end - start) / 2;
            var middleEl = matrix[middle / n][middle % n];
            if (middleEl == target) return true;
            if (middleEl > target)
                end = middle - 1;
            else
                start = middle + 1;
        }
        return false;
    }

    public static void Ex75_SortColors_Counting(int[] nums)
    {
        var counts = new int[3];
        for (var i = 0; i < nums.Length; i++)
            counts[nums[i]]++;

        var k = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            while (counts[k] == 0) k++;
            nums[i] = k;
            counts[k]--;
        }
    }

    public static void Ex75_SortColors_Lomuto(int[] nums)
    {
        var lastSmallerPivot = -1; // last smaller than the pivot
        var lastEqualToPivot = -1;
        var pivot = 1;
        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] < pivot)
            {
                var v1 = nums[++lastSmallerPivot];
                var v2 = nums[++lastEqualToPivot];
                var v3 = nums[i];

                nums[i] = v2;
                nums[lastEqualToPivot] = v1;
                nums[lastSmallerPivot] = v3;
            }
            else if (nums[i] == pivot)
            {
                var v2 = nums[++lastEqualToPivot];
                var v3 = nums[i];

                nums[i] = v2;
                nums[lastEqualToPivot] = v3;
            }
        }
    }

    public static void Ex75_SortColors_DutchFlag(int[] nums)
    {
        var i = 0; // first item non < p
        var j = 0; // first item non == p
        var k = nums.Length - 1; // last item non > p

        while (j <= k)
        {
            if (nums[j] == 1)
            {
                j++;
            }
            else if (nums[j] == 2)
            {
                (nums[j], nums[k]) = (nums[k], nums[j]);
                k--;
            }
            else
            {
                (nums[i], nums[j]) = (nums[j], nums[i]);
                i++; j++;
            }
        }
    }

    public static ListNode Ex83_DeleteDuplicates(ListNode head)
    {
        var current = head;
        while (current != null)
        {
            while (current.next != null && current.val == current.next.val)
                current.next = current.next.next;
            current = current.next;
        }
        return head;
    }

    public static int Ex84_LargestRectangleArea(int[] heights)
    {
        var n = heights.Length;

        var leftQueue = new LinkedList<int>();
        var leftIndexes = new int[n];
        var rightQueue = new LinkedList<int>();
        var rightIndexes = new int[n];

        for (var i = 0; i < n; i++)
        {
            while (leftQueue.Count > 0 && heights[leftQueue.Last.Value] >= heights[i])
                leftQueue.RemoveLast();

            leftIndexes[i] = leftQueue.Count > 0 ? leftQueue.Last.Value + 1 : 0;

            leftQueue.AddLast(i);

            while (rightQueue.Count > 0 && heights[rightQueue.Last.Value] >= heights[n - 1 - i])
                rightQueue.RemoveLast();

            rightIndexes[n - 1 - i] = rightQueue.Count > 0 ? rightQueue.Last.Value - 1 : n - 1;

            rightQueue.AddLast(n - 1 - i);

            //Console.WriteLine("leftQueue: " + string.Join(" ", leftQueue));
            //Console.WriteLine("rightQueue: " + string.Join(" ", rightQueue));
            //Console.WriteLine("leftIndexes: " + string.Join(" ", leftIndexes));
            //Console.WriteLine("rightIndexes: " + string.Join(" ", rightIndexes));
            //Console.WriteLine();
        }

        var max = int.MinValue;
        for (var i = 0; i < n; i++)
            max = Math.Max(max, (rightIndexes[i] - leftIndexes[i] + 1) * heights[i]);
        return max;
    }

    public static int Ex85_MaximalRectangle_Quadratic(char[][] matrix)
    {
        var n = matrix.Length;
        var m = matrix[0].Length;

        var rowAccs = BuildRowAccs(matrix);
        var colAccs = BuildColAccs(matrix);
        var matAccs = BuildMatAccs(matrix, rowAccs, colAccs);

        PrintMatrix(rowAccs);
        PrintMatrix(colAccs);
        PrintMatrix(matAccs);

        var max = 0;

        for (var i1 = 0; i1 < n; i1++)
            for (var j1 = 0; j1 < m; j1++)
                for (var i2 = i1; i2 < n; i2++)
                    for (var j2 = j1; j2 < m; j2++)
                    {
                        var ones = matAccs[i2, j2]
                            - (j1 > 0 ? matAccs[i2, j1 - 1] : 0)
                            - (i1 > 0 ? matAccs[i1 - 1, j2] : 0)
                            + (i1 > 0 && j1 > 0 ? matAccs[i1 - 1, j1 - 1] : 0);
                        if (ones == (i2 - i1 + 1) * (j2 - j1 + 1))
                            max = Math.Max(max, ones);
                    }

        return max;

        int[,] BuildRowAccs(char[][] matrix)
        {
            var rowAccs = new int[n, m];
            for (var i = 0; i < n; i++)
            {
                rowAccs[i, 0] = matrix[i][0] - '0';
                for (var j = 1; j < m; j++)
                    rowAccs[i, j] = rowAccs[i, j - 1] + (matrix[i][j] - '0');
            }
            return rowAccs;
        }

        int[,] BuildColAccs(char[][] matrix)
        {
            var colAccs = new int[n, m];
            for (var j = 0; j < m; j++)
            {
                colAccs[0, j] = matrix[0][j] - '0';
                for (var i = 1; i < n; i++)
                    colAccs[i, j] = colAccs[i - 1, j] + (matrix[i][j] - '0');
            }
            return colAccs;
        }

        int[,] BuildMatAccs(char[][] matrix, int[,] rowAccs, int[,] colAccs)
        {
            var matAccs = new int[n, m];
            for (var i = 0; i < n; i++)
                matAccs[i, 0] = colAccs[i, 0];
            for (var j = 0; j < m; j++)
                matAccs[0, j] = rowAccs[0, j];

            for (var i = 0; i < n - 1; i++)
                for (var j = 0; j < m - 1; j++)
                    matAccs[i + 1, j + 1] = matAccs[i, j] + rowAccs[i + 1, j] + colAccs[i, j + 1] + (matrix[i + 1][j + 1] - '0');

            return matAccs;
        }

        void PrintMatrix<T>(T[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var sb = new StringBuilder();
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.Append(" " + matrix[i, j].ToString());
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }

    public static void Ex88_Merge(int[] nums1, int m, int[] nums2, int n)
    {
        int i = m - 1, j = n - 1, k = m + n - 1;
        while (i >= 0 || j >= 0)
        {
            if (i >= 0 && j >= 0)
            {
                if (nums1[i] >= nums2[j])
                    nums1[k--] = nums1[i--];
                else
                    nums1[k--] = nums2[j--];
            }
            else if (i >= 0) nums1[k--] = nums1[i--];
            else nums1[k--] = nums2[j--];
        }
    }

    public static IList<int> Ex94_InorderTraversal(TreeNode root)
    {
        var result = new List<int>();
        Inorder(root);
        return result;

        void Inorder(TreeNode root)
        {
            if (root == null) return;
            Inorder(root.left);
            result.Add(root.val);
            Inorder(root.right);
        }
    }

    public static bool Ex100_IsSameTree_Recursive(TreeNode p, TreeNode q) =>
        (p == null && q == null) ||
        (p != null && q != null && 
            p.val == q.val && 
            Ex100_IsSameTree_Recursive(p.left, q.left) && 
            Ex100_IsSameTree_Recursive(p.right, q.right));


    public static int Ex115_NumDistinct_DPTopDown(string s, string t)
    {
        var solutions = new Dictionary<(int, int), int>();
        var n = s.Length;
        var m = t.Length;
        return NumDistinct(0, 0);

        int NumDistinct(int i, int j)
        {
            if (j == m) return 1;
            if (i == n) return 0;
            if (n - i < m - j) return 0;
            if (solutions.TryGetValue((i, j), out var solution)) return solution;

            solution = 0;
            if (s[i] == t[j])
                solution += NumDistinct(i + 1, j + 1);
            solution += NumDistinct(i + 1, j);

            solutions[(i, j)] = solution;
            return solution;
        }
    }

    public static int Ex115_NumDistinct_DPBottomUp(string s, string t)
    {
        var n = s.Length;
        var m = t.Length;
        var solutions = new int[n + 1, m + 1];
        for (var i = 0; i <= n; i++)
            solutions[i, m] = 1;
        for (var j = 0; j < m; j++)
            solutions[n, j] = 0;

        for (var i = n - 1; i >= 0; i--)
            for (var j = m - 1; j >= 0; j--)
                solutions[i, j] = (s[i] == t[j] ? solutions[i + 1, j + 1] : 0) + solutions[i + 1, j];

        return solutions[0, 0];
    }

    public static int Ex115_NumDistinct_DPBottomUpSpaceOptimized(string s, string t)
    {
        var n = s.Length;
        var m = t.Length;

        var solutions = new int[m + 1];
        solutions[m] = 1;

        var minJ = m - 1;
        for (var i = n - 1; i >= 0; i--)
        {
            for (var j = minJ; j < m; j++)
                if (s[i] == t[j])
                    solutions[j] += solutions[j + 1];

            if (minJ > 0 && solutions[minJ] != 0)
                minJ--;

            //Console.WriteLine(string.Join(" ", solutions));
        }

        return solutions[0];
    }

    public static IList<IList<string>> Ex131_PalindromePartition(string s)
    {
        var solutions = new IList<IList<string>>[s.Length];
        return PalindromePartition(0).ToList();
        
        bool IsPalindrome(string s, int i, int j)
        {
            if (j <= i) return true;
            while (i < j)
                if (s[i++] != s[j--]) 
                    return false;
            return true;
        }

        IList<IList<string>> PalindromePartition(int i)
        {
            if (i == s.Length)
                return new List<IList<string>> { new List<string> { } };

            if (i == s.Length - 1)
                return new List<IList<string>> { new List<string> { s[^1].ToString() } };

            if (solutions[i] != null)
                return solutions[i];

            var solution = new List<IList<string>>();
            for (var j = i; j < s.Length; j++)
            {
                if (IsPalindrome(s, i, j))
                {
                    foreach (var partition in PalindromePartition(j + 1))
                    {
                        solution.Add(partition.Prepend(s[i..(j + 1)]).ToList());
                    }
                }
            }

            solutions[i] = solution;
            return solution;
        }
    }

    public static void Ex143_ReorderList(ListNode head)
    {
        if (head.next == null) return;

        var beforeMiddlePoint = BeforeMiddlePoint(head);
        var secondHalf = beforeMiddlePoint.next;
        beforeMiddlePoint.next = null;

        Merge(head, Reverse(secondHalf));

        void Merge(ListNode first, ListNode second)
        {
            while (first != null)
            {
                var firstNext = first.next;
                var secondNext = second?.next;
                first.next = second;
                if (second != null)
                    second.next = firstNext;

                first = firstNext;
                second = secondNext;
            }
        }

        ListNode BeforeMiddlePoint(ListNode head)
        {
            var slow = head;
            var fast = head.next;
            while (fast?.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            return slow;
        }

        ListNode Reverse(ListNode head)
        {
            ListNode previous = null, current = head, next = head.next;
            while (current != null)
            {
                current.next = previous;
                previous = current;
                current = next;
                next = current?.next;
            }
            return previous;
        }
    }

    public static IList<int> Ex144_PreorderTraversal(TreeNode root)
    {
        var result = new List<int>();
        Preorder(root);
        return result;

        void Preorder(TreeNode node)
        {
            if (node == null) return;
            result.Add(node.val);
            Preorder(node.left);
            Preorder(node.right);
        }
    }

    public static int Ex149_MaxPoints(int[][] points)
    {
        var X = 0;
        var Y = 1;

        var n = points.GetLength(0);
        if (n <= 1) return n;

        var lines = new Dictionary<(double slope, double yIntercept), ISet<(int, int)>> { };
        var max = int.MinValue;
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var line = LineThough(points[i], points[j]);

                if (lines.TryGetValue(line, out var pointsOnLine))
                {
                    pointsOnLine.Add((points[i][X], points[i][Y]));
                    pointsOnLine.Add((points[j][X], points[j][Y]));
                }
                else
                {
                    pointsOnLine = new HashSet<(int, int)>
                    {
                        (points[i][X], points[i][Y]),
                        (points[j][X], points[j][Y])
                    };
                    lines[(line)] = pointsOnLine;
                }

                Console.WriteLine($"Lines: {string.Join("\n", lines.Select(kvp => $"{kvp.Key} => {string.Join(" ", kvp.Value)}"))}");

                max = Math.Max(max, pointsOnLine.Count);
            }
        }

        return max;

        (double slope, double yIntercept) LineThough(int[] p1, int[] p2, int digitsPrecision = 8)
        {
            if (p1[X] == p2[X]) return (int.MaxValue, p1[X]);
            var slope = ((double)p1[Y] - p2[Y]) / (p1[X] - p2[X]);
            var yIntercept = p1[Y] - slope * p1[X];
            return (Math.Round(slope, digitsPrecision), Math.Round(yIntercept, digitsPrecision));
        }
    }

    public static int Ex149_MaxPoints_Fixing1stPoint(int[][] points)
    {
        var X = 0;
        var Y = 1;

        var n = points.GetLength(0);
        if (n <= 1) return n;

        var max = int.MinValue;
        for (var i = 0; i < n; i++)
        {
            var lines = new Dictionary<double, int> { };

            for (var j = i + 1; j < n; j++)
            {
                var slope = SlopeThrough(points[i], points[j]);

                if (lines.ContainsKey(slope))
                    lines[slope]++;
                else
                    lines[slope] = 2;

                Console.WriteLine($"Lines: {string.Join("\n", lines.Select(kvp => $"{kvp.Key} => {kvp.Value}"))}");

                max = Math.Max(max, lines[slope]);
            }
        }

        return max;

        double SlopeThrough(int[] p1, int[] p2, int digitsPrecision = 8)
        {
            if (p1[X] == p2[X]) return int.MaxValue;
            var slope = ((double)p1[Y] - p2[Y]) / (p1[X] - p2[X]);
            return Math.Round(slope, digitsPrecision);
        }
    }

    public class Ex155_MinStack_Monotonic
    {
        private readonly IList<int> values = new List<int>();
        private readonly IList<int> mins = new List<int>();

        public void Push(int val)
        {
            values.Add(val);
            var lastMin = mins.Count == 0 ? int.MaxValue : mins[^1];
            if (val <= lastMin)
                mins.Add(Math.Min(lastMin, val));
        }

        public void Pop()
        {
            if (values[^1] == mins[^1])
                mins.RemoveAt(mins.Count - 1);
            values.RemoveAt(values.Count - 1);
        }

        public int Top()
        {
            return values[^1];
        }

        public int GetMin()
        {
            return mins[^1];
        }
    }

    public class Ex155_MinStack_StrictlyMonotonic
    {
        private readonly IList<int> values = new List<int>();
        private readonly IList<int> mins = new List<int>();

        public void Push(int val)
        {
            if (mins.Count == 0 || values[mins[^1]] > val)
                mins.Add(values.Count);
            values.Add(val);
        }

        public void Pop()
        {
            if (mins[^1] == values.Count - 1)
                mins.RemoveAt(mins.Count - 1);
            values.RemoveAt(values.Count - 1);
        }

        public int Top()
        {
            return values[^1];
        }

        public int GetMin()
        {
            return values[mins[^1]];
        }
    }

    public static int Ex159_LengthOfLongestSubstringTwoDistinct(string s)
    {
        if (s.Length <= 2) return s.Length;

        // i = start of the window
        // j = end of the window inclusive
        // i and j moving from left to right

        var i = 0; var j = 0; var max = 1;
        var distinctChars = new Dictionary<char, int> { [s[0]] = 1 };
        while (j < s.Length - 1)
        {
            if (!distinctChars.TryGetValue(s[j + 1], out var countNextJ))
                countNextJ = 0;
            if (distinctChars.Count < 2 || countNextJ > 0)
            {
                j++;
                distinctChars[s[j]] = countNextJ + 1;
                max = Math.Max(max, j - i + 1);
            }
            else if (i <= j)
            {
                distinctChars[s[i]]--;
                if (distinctChars[s[i]] == 0)
                    distinctChars.Remove(s[i]);
                i++;
            }
        }

        return max;
    }

    public static int[] Ex167_TwoSum(int[] numbers, int target)
    {
        var n = numbers.Length;
        var i = 0;
        var j = n - 1;

        while (i < j)
        {
            var sum = numbers[i] + numbers[j];
            if (sum == target)
                return new[] { i + 1, j + 1 };
            if (sum < target)
                i++;
            else
                j--;
        }

        return Array.Empty<int>();
    }

    public static IList<string> Ex187_FindRepeatedDnaSequences_2Bits(string s)
    {
        if (s.Length < 10)
            return Array.Empty<string>();

        var result = new List<string> { };
        var values = new Dictionary<char, int>(4)
        {
            ['A'] = 0,
            ['C'] = 1,
            ['G'] = 2,
            ['T'] = 3,
        };

        var counts = new Dictionary<int, int> { };
        var hash = 0;
        for (var i = 0; i < 10; i++)
            hash = (hash << 2) + values[s[i]];
        counts[hash] = 1;

        for (var i = 10; i < s.Length; i++)
        {
            hash &= 0b00111111111111111111;
            hash = (hash << 2) + values[s[i]];
            if (counts.TryGetValue(hash, out var count))
            {
                if (count == 1)
                    result.Add(s[(i - 9)..(i + 1)]);
                counts[hash]++;
            }
            else
            {
                counts[hash] = 1;
            }
        }

        return result;
    }

    public static IList<string> Ex187_FindRepeatedDnaSequences_3Bits(string s)
    {
        if (s.Length < 10)
            return Array.Empty<string>();

        var result = new List<string> { };

        var counts = new Dictionary<int, int> { };
        var hash = 0;
        for (var i = 0; i < 10; i++)
            hash = (hash << 3) + (s[i] - 'A');
        counts[hash] = 1;

        for (var i = 10; i < s.Length; i++)
        {
            hash &= 0b00111111111111111111111111111;
            hash = (hash << 3) + (s[i] - 'A');
            if (counts.TryGetValue(hash, out var count))
            {
                if (count == 1)
                    result.Add(s[(i - 9)..(i + 1)]);
                counts[hash] = count + 1;
            }
            else
            {
                counts[hash] = 1;
            }
        }

        return result;
    }

    public static int Ex191_HammingWeight_ViaString(uint n) => Convert.ToString(n, 2).Count(c => c == '1');

    public static int Ex191_HammingWeight_Bit(uint n)
    {
        var result = 0;
        for (var i = 0; i < 32; i++, n >>= 1)
            if ((n & 1) == 1)
                result++;
        return result;
    }

    public static int Ex191_HammingWeight_BitOptimized(uint n)
    {
        var result = 0;
        while (n != 0)
        {
            result += (int)(n & 1);
            n >>= 1;
        }
        return result;
    }

    public static int Ex200_NumIslands_WithAdjs(char[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        var maxVertices = 301;

        var landAdjs = BuildAdjs();

        var numberOfIslands = 0;
        var visitedLands = new HashSet<int>();
        foreach (var landId in landAdjs.Keys)
        {
            if (visitedLands.Contains(landId))
                continue;
            numberOfIslands++;
            ExploreLands(landId, visitedLands);
        }

        return numberOfIslands;

        void ExploreLands(int landId, ISet<int> visitedLands)
        {
            if (visitedLands.Contains(landId)) return;
            visitedLands.Add(landId);

            foreach (var adjLandId in landAdjs[landId])
                ExploreLands(adjLandId, visitedLands);
        }

        IDictionary<int, ISet<int>> BuildAdjs()
        {
            var adjs = new Dictionary<int, ISet<int>>();
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (grid[i][j] != '1')
                        continue;

                    var adj = new HashSet<int> { };

                    if (j > 0 && grid[i][j - 1] == '1')
                        adj.Add(VertexId(i, j - 1));
                    if (j < n - 1 && grid[i][j + 1] == '1')
                        adj.Add(VertexId(i, j + 1));
                    if (i > 0 && grid[i - 1][j] == '1')
                        adj.Add(VertexId(i - 1, j));
                    if (i < m - 1 && grid[i + 1][j] == '1')
                        adj.Add(VertexId(i + 1, j));

                    adjs[VertexId(i, j)] = adj;
                }
            }
            return adjs;
        }

        int VertexId(int i, int j) => maxVertices * i + j;
    }

    public static int Ex200_NumIslands_WithoutAdjs(char[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        var maxVertices = 301;

        var numberOfIslands = 0;
        var visitedLands = new HashSet<int>();
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var landId = maxVertices * i + j;
                if (grid[i][j] != '1' || visitedLands.Contains(landId))
                    continue;
                numberOfIslands++;
                ExploreLands(i, j, visitedLands);
            }
        }

        return numberOfIslands;

        void ExploreLands(int i, int j, ISet<int> visitedLands)
        {
            var landId = maxVertices * i + j;
            if (visitedLands.Contains(landId)) return;
            visitedLands.Add(landId);

            if (i > 0 && grid[i - 1][j] == '1')
                ExploreLands(i - 1, j, visitedLands);
            if (i < m - 1 && grid[i + 1][j] == '1')
                ExploreLands(i + 1, j, visitedLands);
            if (j > 0 && grid[i][j - 1] == '1')
                ExploreLands(i, j - 1, visitedLands);
            if (j < n - 1 && grid[i][j + 1] == '1')
                ExploreLands(i, j + 1, visitedLands);
        }
    }

    public static bool Ex207_CanFinish_EdgeList(int numCourses, int[][] prerequisites)
    {
        var visited = new HashSet<int> { };
        for (var course = 0; course < numCourses; course++)
        {
            if (visited.Contains(course)) continue;
            if (Dfs(course, new HashSet<int> { })) return false;
        }

        return true;

        IEnumerable<int> Adj(int c)
        {
            foreach (var prerequisite in prerequisites)
                if (prerequisite[1] == c)
                    yield return prerequisite[0];
        }

        bool Dfs(int c, ISet<int> path)
        {
            path.Add(c);

            foreach (var n in Adj(c))
                if (path.Contains(n) || Dfs(n, path))
                    return true;

            path.Remove(c);

            return false;
        }
    }

    public static bool Ex207_CanFinish_AdjList(int numCourses, int[][] prerequisites)
    {
        var adjs = new ISet<int>[numCourses];
        foreach (var prerequisite in prerequisites)
        {
            if (adjs[prerequisite[1]] == null)
                adjs[prerequisite[1]] = new HashSet<int> { prerequisite[0] };
            else
                adjs[prerequisite[1]].Add(prerequisite[0]);
        }

        var visited = new HashSet<int> { };
        for (var course = 0; course < numCourses; course++)
        {
            if (visited.Contains(course)) continue;
            if (Dfs(course, new HashSet<int> { })) return false;
        }

        return true;

        bool Dfs(int c, ISet<int> path)
        {
            if (path.Contains(c))
                return true;

            if (visited.Contains(c))
                return false;

            visited.Add(c);
            path.Add(c);

            if (adjs[c] != null)
                foreach (var n in adjs[c])
                    if (Dfs(n, path))
                        return true;

            path.Remove(c);

            return false;
        }
    }

    public static int Ex209_MinSubArrayLen_DP(int target, int[] nums)
    {
        var solutions = new Dictionary<(int, int), int> { };
        var min = int.MaxValue;
        for (var i = 0; i < nums.Length; i++)
            min = Math.Min(min, MinSubArrayLen(i, target));
        return min == int.MaxValue ? 0 : min;

        int MinSubArrayLen(int i, int target)
        {
            if (i >= nums.Length)
                return int.MaxValue;

            if (nums[i] >= target)
                return 1;

            if (solutions.TryGetValue((i, target), out var solution)) return solution;

            var v = MinSubArrayLen(i + 1, target - nums[i]);
            solution = v == int.MaxValue ? int.MaxValue : 1 + v;
            solutions[(i, target)] = solution;
            return solution;
        }
    }

    public static int Ex209_MinSubArrayLen_SlidingWindow(int target, int[] nums)
    {
        var n = nums.Length;

        var min = int.MaxValue;
        var start = 0; var end = 0; var sum = nums[end];
        while (start < n && end < n)
        {
            if (sum >= target)
            {
                min = Math.Min(min, end - start + 1);
            }

            if (end < n - 1 && sum < target)
            {
                end++;
                sum += nums[end];
            }
            else if (start < end)
            {
                sum -= nums[start];
                start++;
            }
            else
            {
                break;
            }
        }

        return min == int.MaxValue ? 0 : min;
    }

    public static int[] Ex210_FindOrder(int numCourses, int[][] prerequisites)
    {
        // Result is order[topoOrder] = vertexId
        var currentOrder = numCourses - 1;
        var order = new int[numCourses];

        // Transform edge list representation into adj list representation
        var adjs = BuildAdjs();

        // DFS on the entire graph 
        var visited = new HashSet<int> { };
        for (var i = 0; i < numCourses; i++)
        {
            if (visited.Contains(i)) continue;
            if (Dfs(i, new HashSet<int> { })) return Array.Empty<int>();
        }
        return order;

        ISet<int>[] BuildAdjs()
        {
            var adjs = new ISet<int>[numCourses];
            foreach (var prerequisite in prerequisites)
            {
                if (adjs[prerequisite[1]] == null)
                    adjs[prerequisite[1]] = new HashSet<int> { prerequisite[0] };
                else
                    adjs[prerequisite[1]].Add(prerequisite[0]);
            }
            return adjs;
        }

        bool Dfs(int v, ISet<int> path)
        {
            if (path.Contains(v)) return true;
            if (visited.Contains(v)) return false;

            visited.Add(v);
            path.Add(v);

            if (adjs[v] != null)
            {
                foreach (var n in adjs[v])
                {
                    if (Dfs(n, path))
                        return true;
                }
            }

            path.Remove(v);

            order[currentOrder--] = v;

            return false;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
             this.val = val;
             this.left = left;
             this.right = right;
        }
    }

    public static IList<IList<int>> Ex216_CombinationSum3(int k, int n)
    {
        var results = new List<IList<int>>();
        Combinations(k, n, 9, new List<int>());
        return results;

        void Combinations(int k, int n, int m, IList<int> current)
        {
            if (n == 0)
            {
                if (k == 0)
                    results.Add(current.ToList());
                return;
            }

            if (n < 0 || k == 0)
                return;

            for (var i = m; i >= 0; i--)
            {
                current.Add(i);
                Combinations(k - 1, n - i, i - 1, current);
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    public static int Ex224_Calculate(string s)
    {
        s = s.Replace(" ", "");
        return Expression(0).Item2;

        Tuple<int, int> Expression(int i)
        {
            var result = SimpleExpression(i);
            if (result != null) return result;
            return Parentheses(i);
        }

        Tuple<int, int> Parentheses(int i)
        {
            if (i >= s.Length || s[i] != '(') return null;
            var result = Expression(i + 1);
            if (result == null) return null;
            if (result.Item1 >= s.Length || s[result.Item1] != ')') return null;
            return Tuple.Create(result.Item1 + 1, result.Item2);
        }

        Tuple<int, int> SimpleExpression(int i)
        {
            var result = Factor(i);
            if (result == null) return null;
            while (result.Item1 < s.Length && (s[result.Item1] == '+' || s[result.Item1] == '-'))
            {
                var nextFactor = Factor(result.Item1 + 1);
                var sign = s[result.Item1] == '+' ? 1 : -1;
                result = Tuple.Create(nextFactor.Item1, result.Item2 + nextFactor.Item2 * sign);
            }
            return result;
        }

        Tuple<int, int> Factor(int i)
        {
            var result = Parentheses(i);
            if (result != null) return result;
            result = SignedExpression(i);
            if (result != null) return result;
            result = Number(i);
            if (result != null) return result;
            return null;
        }

        Tuple<int, int> SignedExpression(int i)
        {
            if (i >= s.Length || s[i] != '-') return null;
            var result = Parentheses(i + 1);
            if (result != null) return Tuple.Create(result.Item1, -result.Item2);
            result = Number(i + 1);
            if (result != null) return Tuple.Create(result.Item1, -result.Item2);
            return null;
        }

        Tuple<int, int> Number(int i)
        {
            var value = 0;
            if (i < s.Length && s[i] >= '0' && s[i] <= '9')
                value = s[i++] - '0';
            while (i < s.Length && s[i] >= '0' && s[i] <= '9')
                value = value * 10 + (s[i++] - '0');
            return Tuple.Create(i, value);
        }
    }

    public static TreeNode Ex226_InvertTree(TreeNode root)
    {
        if (root == null) return null;

        var left = Ex226_InvertTree(root.left);
        var right = Ex226_InvertTree(root.right);
        return new TreeNode(root.val, right, left);
    }

    public static int[] Ex238_ProductExceptSelf(int[] nums)
    {
        var n = nums.Length;
        var leftProducts = new int[n];
        var rightProducts = new int[n];
        leftProducts[0] = 1;
        rightProducts[n - 1] = 1;
        for (var i = 1; i < n; i++)
        {
            leftProducts[i] = leftProducts[i - 1] * nums[i - 1];
            rightProducts[n - 1 - i] = rightProducts[n - i] * nums[n - i];
        }

        var result = new int[n];
        for (var i = 0; i < n; i++)
            result[i] = leftProducts[i] * rightProducts[i];
        return result;
    }

    public static int[] Ex239_MaxSlidingWindow_MinHeap(int[] nums, int k)
    {
        var n = nums.Length;
        var maxs = new PriorityQueue<int, int>();

        for (var i = 0; i < k - 1; i++)
            maxs.Enqueue(i, -nums[i]);

        var result = new int[n - k + 1];
        for (var i = k - 1; i < n; i++)
        {
            maxs.Enqueue(i, -nums[i]);

            while (maxs.TryPeek(out var maxIndex, out var maxValue) && maxIndex <= i - k)
                maxs.Dequeue();

            result[i - k + 1] = nums[maxs.Peek()];
        }

        return result;
    }

    public static int[] Ex239_MaxSlidingWindow_Deque(int[] nums, int k)
    {
        var n = nums.Length;
        var maxes = new LinkedList<int>();

        var result = new int[n - k + 1];
        for (var i = 0; i < n; i++)
        {
            // Clean out-of-window values from the front
            while (maxes.Count > 0 && maxes.First.Value <= i - k)
                maxes.RemoveFirst();

            // Remove smaller-than-new values from the rear
            while (maxes.Count > 0 && nums[maxes.Last.Value] <= nums[i])
                maxes.RemoveLast();

            maxes.AddLast(i);

            if (i >= k - 1)
                result[i - k + 1] = nums[maxes.First.Value];
        }

        return result;
    }

    public static int Ex253_MinMeetingRooms_UsingLINQ(int[][] intervals)
    {
        var events = intervals
            .SelectMany(i => new[] { (i[0], +1), (i[1], -1) })
            .OrderBy(c => c);
        var max = 0;
        var current = 0;
        foreach (var ev in events)
        {
            current += ev.Item2;
            max = Math.Max(max, current);
        }
        return max;
    }

    public static int Ex253_MinMeetingRooms_UsingMinHeap(int[][] intervals)
    {
        Array.Sort(intervals, (x, y) => x[0] - y[0]);

        var endIntervalsQueue = new PriorityQueue<int, int>();
        endIntervalsQueue.Enqueue(intervals[0][1], intervals[0][1]);
        for (var i = 1; i < intervals.Length; i++)
        {
            if (intervals[i][0] >= endIntervalsQueue.Peek())
                endIntervalsQueue.Dequeue();
            endIntervalsQueue.Enqueue(intervals[i][1], intervals[i][1]);
        }
        return endIntervalsQueue.Count;
    }

    public static int Ex264_NthUglyNumber_TripleQueue(int n)
    {
        if (n == 1) return 1;

        var priorityQueue = new PriorityQueue<long, long>[3]
        {
            new PriorityQueue<long, long>(),
            new PriorityQueue<long, long>(),
            new PriorityQueue<long, long>(),
        };

        TripleEnqueue(0, 1);

        long result = 0;
        for (var i = 1; i < n; i++)
        {
            var min0 = priorityQueue[0].Peek();
            var min1 = priorityQueue[1].Peek();
            var min2 = priorityQueue[2].Peek();

            if (min0 <= min1 && min0 <= min2)
            {
                result = priorityQueue[0].Dequeue();
                TripleEnqueue(0, min0);
            }
            else if (min1 <= min0 && min1 <= min2)
            {
                result = priorityQueue[1].Dequeue();
                TripleEnqueue(1, min1);
            }
            else
            {
                result = priorityQueue[2].Dequeue();
                TripleEnqueue(2, min2);
            }
        }

        return (int)result;

        void TripleEnqueue(int queueIndex, long k)
        {
            if (queueIndex <= 0)
                priorityQueue[0].Enqueue(k * 2, k * 2);

            if (queueIndex <= 1)
                priorityQueue[1].Enqueue(k * 3, k * 3);

            if (queueIndex <= 2)
                priorityQueue[2].Enqueue(k * 5, k * 5);
        }
    }

    public static int Ex278_FirstBadVersion(int n, Predicate<int> isBadVersion)
    {
        var left = 1;
        var right = n;

        while (left <= right)
        {
            var middle = left + (right - left) / 2;
            if (isBadVersion(middle))
                right = middle - 1;
            else
                left = middle + 1;
        }
        return left;
    }

    public class Ex295_MedianFinder
    {
        private PriorityQueue<int, int> SmallerHalf { get; } = new PriorityQueue<int, int>();
        private PriorityQueue<int, int> BiggerHalf { get; } = new PriorityQueue<int, int>();

        public Ex295_MedianFinder()
        {
        }

        public void AddNum(int num)
        {
            if (SmallerHalf.Count == 0)
            {
                SmallerHalf.Enqueue(num, -num);
                return;
            }

            var maxOfSmallerHalf = SmallerHalf.Peek();

            if (SmallerHalf.Count <= BiggerHalf.Count)
            {
                if (num <= maxOfSmallerHalf)
                {
                    SmallerHalf.Enqueue(num, -num);
                }
                else
                {
                    BiggerHalf.Enqueue(num, num);
                    var minOfBiggerHalf = BiggerHalf.Dequeue();
                    SmallerHalf.Enqueue(minOfBiggerHalf, -minOfBiggerHalf);
                }
            }
            else
            {
                if (num <= maxOfSmallerHalf)
                {
                    SmallerHalf.Enqueue(num, -num);
                    maxOfSmallerHalf = SmallerHalf.Dequeue();
                    BiggerHalf.Enqueue(maxOfSmallerHalf, maxOfSmallerHalf);
                }
                else
                {
                    BiggerHalf.Enqueue(num, num);
                }

            }

            //Console.WriteLine(nameof(SmallerHalf) + ": " + string.Join(", ", SmallerHalf.UnorderedItems));
            //Console.WriteLine(nameof(BiggerHalf) + ": " + string.Join(", ", BiggerHalf.UnorderedItems));
        }

        public double FindMedian()
        {
            if (SmallerHalf.Count < BiggerHalf.Count)
                return BiggerHalf.Peek();
            else if (SmallerHalf.Count > BiggerHalf.Count)
                return SmallerHalf.Peek();
            return (BiggerHalf.Peek() + SmallerHalf.Peek()) / 2.0;
        }
    }

    public static string Ex299_GetHint(string secret, string guess)
    {
        var n = secret.Length;
        var potentialCowsInSecret = new int[10];
        var potentialCowsInGuess = new int[10];

        var bulls = 0;
        for (var i = 0; i < n; i++)
        {
            if (secret[i] == guess[i])
            {
                bulls++;
                continue;
            }

            potentialCowsInSecret[secret[i] - '0']++;
            potentialCowsInGuess[guess[i] - '0']++;
        }

        var cows = 0;
        for (var i = 0; i < 10; i++)
        {
            cows += Math.Min(potentialCowsInSecret[i], potentialCowsInGuess[i]);
        }

        return $"{bulls}A{cows}B";
    }

    public static IList<int> Ex310_FindMinHeightTrees_BfsOnly(int n, int[][] edges)
    {
        var adjs = BuildAdjs();

        var heights = new int[n];
        for (var i = 0; i < n; i++)
            heights[i] = Bfs(i);

        var minHeight = heights.Min();
        return heights.Select((h, i) => (h, i)).Where(c => c.h == minHeight).Select(c => c.i).ToList();

        ISet<int>[] BuildAdjs()
        {
            var adjs = new ISet<int>[n];
            foreach (var edge in edges)
            {
                if (adjs[edge[0]] == null)
                    adjs[edge[0]] = new HashSet<int> { edge[1] };
                else
                    adjs[edge[0]].Add(edge[1]);

                if (adjs[edge[1]] == null)
                    adjs[edge[1]] = new HashSet<int> { edge[0] };
                else
                    adjs[edge[1]].Add(edge[0]);
            }
            return adjs;
        }

        int Bfs(int v)
        {
            var visited = new HashSet<int> { };
            var maxHeight = 0;

            var queue = new Queue<(int, int)>();
            queue.Enqueue((v, 0));

            while (queue.Count > 0)
            {
                var (j, hj) = queue.Dequeue();

                visited.Add(j);
                maxHeight = Math.Max(maxHeight, hj);
                
                if (adjs[j] != null)
                    foreach (var n in adjs[j])
                        if (!visited.Contains(n))
                            queue.Enqueue((n, hj + 1));
            }

            return maxHeight;
        }
    }

    public static IList<int> Ex310_FindMinHeightTrees_BfsOnlyOptimized(int n, int[][] edges)
    {
        var adjs = BuildAdjs();

        var heights = new int[n];
        var minHeight = int.MaxValue;
        var results = new List<int> { };
        for (var i = 0; i < n; i++)
        {
            heights[i] = Bfs(i, minHeight);

            if (heights[i] < minHeight)
            {
                minHeight = heights[i];
                results.Clear();
            }
            if (heights[i] <= minHeight)
            {
                results.Add(i);
            }
        }

        return results;

        ISet<int>[] BuildAdjs()
        {
            var adjs = new ISet<int>[n];
            foreach (var edge in edges)
            {
                if (adjs[edge[0]] == null)
                    adjs[edge[0]] = new HashSet<int> { edge[1] };
                else
                    adjs[edge[0]].Add(edge[1]);

                if (adjs[edge[1]] == null)
                    adjs[edge[1]] = new HashSet<int> { edge[0] };
                else
                    adjs[edge[1]].Add(edge[0]);
            }
            return adjs;
        }

        int Bfs(int v, int minHeight)
        {
            var visited = new HashSet<int> { };
            var maxHeight = 0;

            var queue = new Queue<(int, int)>();
            queue.Enqueue((v, 0));

            while (queue.Count > 0)
            {
                var (j, hj) = queue.Dequeue();
                if (hj > minHeight)
                    return minHeight + 1;

                visited.Add(j);
                maxHeight = Math.Max(maxHeight, hj);
                if (adjs[j] != null)
                    foreach (var n in adjs[j])
                        if (!visited.Contains(n))
                            queue.Enqueue((n, hj + 1));
            }

            return maxHeight;
        }
    }

    public static IList<int> Ex310_FindMinHeightTrees_DfsUndirectedTopoSort(int n, int[][] edges)
    {
        var adjs = BuildAdjs();
        var remaining = new HashSet<int>(Enumerable.Range(0, n));
        while (remaining.Count > 2)
        {
            var i = remaining.First();
            if (remaining.Contains(i))
                Dfs(i, new HashSet<int> { });
        }
        return remaining.ToList();

        ISet<int>[] BuildAdjs()
        {
            var adjs = new ISet<int>[n];
            foreach (var edge in edges)
            {
                if (adjs[edge[0]] == null)
                    adjs[edge[0]] = new HashSet<int> { edge[1] };
                else
                    adjs[edge[0]].Add(edge[1]);

                if (adjs[edge[1]] == null)
                    adjs[edge[1]] = new HashSet<int> { edge[0] };
                else
                    adjs[edge[1]].Add(edge[0]);
            }
            return adjs;
        }

        void Dfs(int v, ISet<int> visited)
        {
            if (visited.Contains(v))
                return;
            visited.Add(v);

            var leaf = adjs[v].Count == 1;

            foreach (var n in adjs[v])
                Dfs(n, visited);

            if (leaf)
            {
                adjs[adjs[v].First()].Remove(v);
                adjs[v].Clear();
                remaining.Remove(v);
            }
        }
    }

    public static IList<int> Ex310_FindMinHeightTrees_DfsUndirectedTopoSortWithQueue(int n, int[][] edges)
    {
        if (n == 1) return new List<int> { 0 };

        var adjs = BuildAdjs();

        var queue = new Queue<int>();

        var levels = new int[n];
        for (var v = 0; v < n; v++)
            if (adjs[v].Count == 1)
            {
                levels[v] = 0;
                queue.Enqueue(v);
            }

        var lastTwo = new Queue<int>();
        while (queue.Count > 0)
        {
            var v = queue.Dequeue();
            
            lastTwo.Enqueue(v);
            if (lastTwo.Count > 2)
                lastTwo.Dequeue();

            var w = adjs[v].SingleOrDefault(-1);
            if (w < 0)
                continue;

            adjs[v].Clear();
            adjs[w].Remove(v);

            if (adjs[w].Count == 1)
            {
                queue.Enqueue(w);
                levels[w] = levels[v] + 1;
            }
        }

        var result = lastTwo.ToList();
        if (result.Count < 2 || levels[result[0]] == levels[result[1]]) 
            return result;
        return new List<int> { result[1] };

        ISet<int>[] BuildAdjs()
        {
            var adjs = new ISet<int>[n];
            foreach (var edge in edges)
            {
                if (adjs[edge[0]] == null)
                    adjs[edge[0]] = new HashSet<int> { edge[1] };
                else
                    adjs[edge[0]].Add(edge[1]);

                if (adjs[edge[1]] == null)
                    adjs[edge[1]] = new HashSet<int> { edge[0] };
                else
                    adjs[edge[1]].Add(edge[0]);
            }
            return adjs;
        }
    }

    public static int Ex329_LongestIncreasingPath(int[][] matrix)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        var solutions = new int[m, n];

        var result = 0;
        for (var i = 0; i < m; i++)
            for (var j = 0; j < n; j++)
                result = Math.Max(LongestIncreasingPath(i, j), result);
        return result;

        int LongestIncreasingPath(int i, int j)
        {
            if (solutions[i, j] > 0) return solutions[i, j];

            var result = 0;
            if (j + 1 < n && matrix[i][j] < matrix[i][j + 1])
                result = Math.Max(result, LongestIncreasingPath(i, j + 1));
            if (j - 1 >= 0 && matrix[i][j] < matrix[i][j - 1])
                result = Math.Max(result, LongestIncreasingPath(i, j - 1));
            if (i + 1 < m && matrix[i][j] < matrix[i + 1][j])
                result = Math.Max(result, LongestIncreasingPath(i + 1, j));
            if (i - 1 >= 0 && matrix[i][j] < matrix[i - 1][j])
                result = Math.Max(result, LongestIncreasingPath(i - 1, j));
            return solutions[i, j] = 1 + result;
        }
    }

    public static int Ex340_LengthOfLongestSubstringKDistinct(string s, int k)
    {
        var n = s.Length;
        if (k == 0) return 0;
        if (n <= k) return n;

        var max = 1;

        // start = start of the window, inclusive
        // end = end of the window, inclusive
        var start = 0; var end = 0;
        var counts = new Dictionary<char, int> { [s[end]] = 1 };

        while (end < n - 1)
        {
            if (!counts.TryGetValue(s[end + 1], out var countOfNext))
                countOfNext = 0;

            if (counts.Count < k || countOfNext > 0)
            {
                end++;
                counts[s[end]] = countOfNext + 1;
                max = Math.Max(max, end - start + 1);
            }
            else if (start <= end)
            {
                counts[s[start]]--;
                if (counts[s[start]] == 0)
                    counts.Remove(s[start]);
                start++;
            }
        }

        return max;
    }

    public static int[] Ex347_TopKFrequent(int[] nums, int k)
    {
        var counts = new Dictionary<int, int> { };
        for (var i = 0; i < nums.Length; i++)
            if (counts.TryGetValue(nums[i], out var c))
                counts[nums[i]] = c + 1;
            else
                counts[nums[i]] = 1;

        var indexedCounts = counts.OrderBy(kvp => -kvp.Value).ToList();

        var results = new int[k];
        for (var i = 0; i < k; i++)
            results[i] = indexedCounts[i].Key;
        return results;
    }

    public static int[] Ex347_TopKFrequent_PartialSort(int[] nums, int k)
    {
        var counts = new Dictionary<int, int> { };
        for (var i = 0; i < nums.Length; i++)
            if (counts.TryGetValue(nums[i], out var c))
                counts[nums[i]] = c + 1;
            else
                counts[nums[i]] = 1;

        var indexedCounts = counts.ToArray();
        SortFirstK(indexedCounts, k, 0, indexedCounts.Length - 1);

        var results = new int[k];
        for (var i = 0; i < k; i++)
            results[i] = indexedCounts[i].Key;
        return results;

        void SortFirstK(KeyValuePair<int, int>[] values, int k, int s, int e)
        {
            if (e - s <= 0) return;

            var pivot = Partition(values, s, e);
            SortFirstK(values, k, s, pivot - 1);
            if (pivot < k - 1)
                SortFirstK(values, k, pivot + 1, e);
        }

        int Partition(KeyValuePair<int, int>[] values, int s, int e)
        {
            var pivot = values[e];
            var i = s - 1;
            for (var j = s; j < e; j++)
            {
                if (values[j].Value >= pivot.Value)
                {
                    i++;
                    (values[i], values[j]) = (values[j], values[i]);
                }
            }

            i++;
            (values[i], values[e]) = (values[e], values[i]);

            return i;
        }
    }

    public class Ex352_SummaryRanges_SortedSet
    {
        private SortedSet<(int value, int type)> events = new();
        // type: 1 = end of interval, -1 = start of interval

        public void AddNum(int value)
        {
            var nextEvent = events
                .GetViewBetween((value, -1), (int.MaxValue, +1))
                .FirstOrDefault((int.MaxValue, -1));
            var previousEvent = events
                .GetViewBetween((int.MinValue, -1), (value, +1))
                .LastOrDefault((int.MinValue, +1));

            if (nextEvent.Item1 == value || previousEvent.Item1 == value) return;

            if (previousEvent.Item2 < 0 || nextEvent.Item2 > 0) return;

            if (previousEvent.Item1 == value - 1)
            {
                if (nextEvent.Item1 == value + 1)
                {
                    events.Remove(previousEvent);
                    events.Remove(nextEvent);
                }
                else
                {
                    events.Remove(previousEvent);
                    events.Add((value, +1));
                }
            }
            else
            {
                if (nextEvent.Item1 == value + 1)
                {
                    events.Add((value, -1));
                    events.Remove(nextEvent);
                }
                else
                {
                    events.Add((value, -1));
                    events.Add((value, +1));
                }
            }
        }

        public int[][] GetIntervals()
        {
            var results = new int[events.Count / 2][];
            int[] current = null;
            int i = 0;
            foreach (var (eventValue, eventType) in events)
            {
                if (current == null)
                    current = new int[] { eventValue, 0 };
                else
                {
                    current[1] = eventValue;
                    results[i++] = current;
                    current = null;
                }
            }

            return results;
        }
    }

    public class Ex352_SummaryRanges_JumpListAndHeap
    {
        private int[] values = new int[10001];
        private PriorityQueue<int, int> sortedValues = new PriorityQueue<int, int>();

        public Ex352_SummaryRanges_JumpListAndHeap()
        {
            for (var i = 0; i < values.Length; i++)
                values[i] = -1;
        }

        public void AddNum(int value)
        {
            if (values[value] >= 0) return;

            if (value < values.Length - 1 && values[value + 1] >= 0)
                values[value] = values[value + 1];
            else
                values[value] = value;

            if (value > 0 && values[value - 1] >= 0)
            {
                values[value - 1] = values[value];
            }

            sortedValues.Enqueue(value, value);
            //Console.WriteLine(string.Join(" ", values.Take(20)));
        }

        public int[][] GetIntervals()
        {
            var newSortedValues = new PriorityQueue<int, int>();

            var result = new List<int[]>();
            var j = int.MinValue;
            while (sortedValues.Count > 0)
            {
                var s = sortedValues.Dequeue();
                if (s <= j) continue;

                j = s;
                while (values[j] >= 0 && values[j] != j)
                    j = values[j];
                result.Add(new[] { s, j });
                newSortedValues.Enqueue(s, s);
            }
            sortedValues = newSortedValues;
            return result.ToArray();
        }
    }

    public static IList<IList<int>> Ex366_FindLeaves(TreeNode root)
    {
        var result = new Dictionary<int, IList<int>> { };
        var maxLevel = Dfs(root, 0);
        return Enumerable.Range(1, maxLevel).Select(l => result[l]).ToList();

        int Dfs(TreeNode node, int level)
        {
            if (node == null) return level;

            int maxLevelLeft = Dfs(node.left, level + 1);
            int maxLevelRight = Dfs(node.right, level + 1);
            int maxLevelChildren = Math.Max(maxLevelLeft, maxLevelRight);

            if (!result.TryGetValue(maxLevelChildren - level, out var list))
                result[maxLevelChildren - level] = new List<int> { node.val };
            else
                list.Add(node.val);

            return Math.Max(level, maxLevelChildren);
        }
    }

    public static int Ex374_GuessNumber(int n, Func<int, int> guess)
    {
        var left = 1;
        var right = n;
        while (left <= right)
        {
            var middle = left + (right - left) / 2;
            var guessOutput = guess(middle);
            if (guessOutput == 0) return middle;
            if (guessOutput > 0)
                left = middle + 1;
            else
                right = middle - 1;
        }
        return left;
    }

    public static int Ex395_LongestSubstring_Quadratic(string s, int k)
    {
        var n = s.Length;

        var max = 0;
        for (var i = 0; i < n; i++)
        {
            var counts = new Dictionary<char, int> { };
            var lowerThanBar = 0;
            for (var j = i; j < n; j++)
            {
                if (counts.TryGetValue(s[j], out var count))
                {
                    counts[s[j]] = count + 1;
                    lowerThanBar += k != counts[s[j]] ? 0 : -1;
                }
                else
                {
                    counts[s[j]] = 1;
                    lowerThanBar += k != 1 ? 1 : 0;
                }

                //Console.WriteLine("Counts: " + string.Join(", ", counts.Select(kvp => $"{kvp.Key} -> {kvp.Value}")));
                //Console.WriteLine("Lower than bar: " + lowerThanBar);

                if (lowerThanBar == 0)
                    max = Math.Max(max, j - i + 1);
            }
        }
        return max;
    }

    public static double[] Ex399_CalcEquation_Dfs(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
    {
        var adjs = new Dictionary<string, ISet<string>> { };
        var ws = new Dictionary<(string, string), double> { };
        for (var i = 0; i < equations.Count; i++)
        {
            var equation = equations[i];
            var w = values[i];

            if (adjs.ContainsKey(equation[0]))
                adjs[equation[0]].Add(equation[1]);
            else
                adjs[equation[0]] = new HashSet<string> { equation[1] };

            if (adjs.ContainsKey(equation[1]))
                adjs[equation[1]].Add(equation[0]);
            else
                adjs[equation[1]] = new HashSet<string> { equation[0] };

            ws[(equation[0], equation[1])] = w;
            ws[(equation[1], equation[0])] = 1 / w;
        }

        var results = new double[queries.Count];
        for (var i = 0; i < queries.Count; i++)
        {
            var query = queries[i];
            results[i] = Dfs(query[0], query[1], new HashSet<string> { });
        }

        return results;

        double Dfs(string s, string e, ISet<string> visited)
        {
            if (visited.Contains(s)) return -1;
            visited.Add(s);

            if (adjs.TryGetValue(s, out var adj))
            {
                if (s == e) return 1;

                foreach (var n in adj)
                {
                    var v = Dfs(n, e, visited);
                    if (v != -1)
                        return ws[(s, n)] * v;
                }
            }

            return -1;
        }
    }

    public static double[] Ex399_CalcEquation_DisjointSets(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
    {
        var parents = new Dictionary<string, string> { };
        var ws = new Dictionary<(string, string), double> { };

        for (var i = 0; i < equations.Count; i++)
        {
            var equation = equations[i];
            var value = values[i];
            ws[(equation[0], equation[1])] = value;
            ws[(equation[1], equation[0])] = 1 / value;
        }

        for (var i = 0; i < equations.Count; i++)
            Connect(equations[i][0], equations[i][1], values[i]);

        (string, double) Root(string s, bool pathCompression = true)
        {
            var root = s;
            var value = 1.0;

            if (parents.TryGetValue(root, out var parent) && parent != root)
            {
                var (parentRoot, parentValue) = Root(parent, pathCompression);

                value = ws[(parent, s)] * parentValue;
                root = parentRoot;
            }

            if (pathCompression)
            {
                parents[s] = root;
                ws[(root, s)] = value;
            }

            return (root, value);
        }

        void Connect(string s1, string s2, double v)
        {
            var c = s1.CompareTo(s2);

            if (c == 0)
                return;

            if (c > 0)
            {
                Connect(s2, s1, 1 / v);
                return;
            }

            var (r1, v1) = Root(s1);
            var (r2, v2) = Root(s2);
            if (r1 == r2) return;

            // TODO: merge by rank?
            if (r1.CompareTo(r2) < 0)
            {
                parents[r2] = r1;
                ws[(r1, r2)] = v * v1 / v2;
            }
            else
            {
                parents[r1] = r2;
                ws[(r2, r1)] = v1 * v2 / v;
            }
        }

        var results = new double[queries.Count];
        for (var i = 0; i < queries.Count; i++)
        {
            var query = queries[i];

            if (!parents.ContainsKey(query[0]) && !parents.ContainsValue(query[0]))
            {
                results[i] = -1;
                continue;
            }

            var (r1, v1) = Root(query[0], false);
            var (r2, v2) = Root(query[1], false);
            results[i] = r1 == r2 ? v2 / v1 : -1;
        }

        return results;
    }

    public static string Ex451_FrequencySort(string s)
    {
        var frequences = new Dictionary<char, int> { };
        foreach (var c in s)
            if (frequences.TryGetValue(c, out var f))
                frequences[c] = f + 1;
            else
                frequences[c] = 1;

        var stringBuilder = new StringBuilder();
        foreach (var kvp in frequences.OrderByDescending(kvp => kvp.Value))
            stringBuilder.Append(kvp.Key, kvp.Value);

        return stringBuilder.ToString();
    }

    public static string Ex451_FrequencySort_WithArray(string s)
    {
        var frequencies = new int[75];
        foreach (var c in s)
            frequencies[c - '0']++;

        var stringBuilder = new StringBuilder();
        var frequenciesArray = Enumerable.Range(0, 75).ToArray();
        Array.Sort(frequenciesArray, (i, j) => frequencies[j] - frequencies[i]);
        foreach (var i in frequenciesArray)
            stringBuilder.Append((char)('0' + i), frequencies[i]);

        return stringBuilder.ToString();
    }

    public static int Ex494_FindTargetSumWays(int[] nums, int target)
    {
        var solutions = new Dictionary<(int, int), int> { };
        return FindTargetSumWays(target, 0);

        int FindTargetSumWays(int target, int i)
        {
            if (i == nums.Length) return target == 0 ? 1 : 0;
            if (solutions.TryGetValue((target, i), out var solution)) return solution;
            var c1 = FindTargetSumWays(target - nums[i], i + 1);
            var c2 = FindTargetSumWays(target + nums[i], i + 1);
            return solutions[(target, i)] = c1 + c2;
        }
    }

    public static int Ex547_FindCircleNum(int[][] isConnected)
    {
        var n = isConnected.Length;
        var parents = new int[n];
        var ranks = new int[n];

        for (var i = 0; i < n; i++)
            parents[i] = i;

        for (var i = 1; i < n; i++)
            for (var j = 0; j < i; j++)
                if (isConnected[i][j] == 1)
                    Connect(i, j);

        var roots = 0;
        for (var i = 0; i < n; i++)
            if (parents[i] == i)
                roots++;

        return roots;

        int Root(int i)
        {
            var r = i;
            if (parents[r] != r)
                r = Root(parents[r]);

            parents[i] = r;
            return r;
        }

        void Connect(int i, int j)
        {
            var ri = Root(i);
            var rj = Root(j);
            if (ri == rj) return;
            if (ranks[ri] >= ranks[rj])
            {
                parents[ri] = rj;
                ranks[ri] = Math.Max(rj, ri + 1);
            }
            else
            {
                parents[rj] = ri;
                ranks[rj] = Math.Max(rj + 1, ri);
            }
        }
    }

    public class Ex591_IsValid
    {
        private record State(string Code, int Position, ImmutableStack<string> Tags)
        {
            public State? Parse(Func<State, State?> rule) => rule(this);

            public State? ParseFirst(params Func<State, State?>[] rules)
            {
                return rules.Select(rule => rule.Invoke(this)).Where(state => state != null).FirstOrDefault();
            }

            public State? ParseZeroOrMore(Func<State, State?> rule)
            {
                var previousState = this;
                while (previousState.Parse(rule) is State state)
                    previousState = state;
                return previousState;
            }
        }

        // GRAMMAR
        // start = closedTag
        // closedTag = tagOpening tagContent tagClosing
        //
        // tagOpening = "<" tagName ">"
        // tagClosing = "</" tagName ">"
        // tagContent = (closedTag | cdataTag | tagString)*
        // tagName = [A-Z]{1, 9}
        // 
        // cdataTag = "<![CDATA[" cdataContent "]]>" (eager matching of "]]>")
        // cdataContent = .* (except "]]>")

        public static bool IsValid(string code) =>
            new State(code, 0, ImmutableStack<string>.Empty)
            .Parse(Start)
            is State { Position: var position, Tags: var tags }
                && position == code.Length
                && tags.IsEmpty;

        static State? Start(State state) => state
            .Parse(ClosedTag);

        static State? ClosedTag(State state) => state
            .Parse(TagOpening)
            ?.Parse(TagContent)
            ?.Parse(TagClosing);

        static State? TagOpening(State state)
        {
            var initialPosition = state.Position;
            var result = state
                .Parse(StringValue("<"))
                ?.Parse(TagName)
                ?.Parse(StringValue(">"));

            if (result == null)
                return result;

            var finalPosition = result.Position;
            var tagName = result.Code[(initialPosition + 1)..(finalPosition - 1)];
            return result with
            {
                Tags = result.Tags.Push(tagName)
            };
        }

        static State? TagClosing(State state)
        {
            var initialPosition = state.Position;
            var result = state
                .Parse(StringValue("</"))
                ?.Parse(TagName)
                ?.Parse(StringValue(">"));

            if (result == null)
                return result;

            var finalPosition = result.Position;
            var tagName = result.Code[(initialPosition + 2)..(finalPosition - 1)];
            if (result.Tags.Peek() != tagName)
                return null;

            return result with
            {
                Tags = result.Tags.Pop()
            };
        }

        static State? TagName(State state)
        {
            int i;
            for (i = 0; i < 9 && i < state.Code.Length - state.Position; i++)
            {
                var currentChar = state.Code[state.Position + i];
                if (!(currentChar >= 'A' && currentChar <= 'Z'))
                    break;
            }

            if (i == 0)
                return null; // Not even a single char has been parsed
            return state with { Position = state.Position + i };
        }

        static Func<State, State?> StringValue(string s) =>
            state =>
            {
                if (state.Position + s.Length > state.Code.Length)
                    return null;

                if (state.Code[state.Position..(state.Position + s.Length)] != s)
                    return null;

                return state with { Position = state.Position + s.Length };
            };

        static State? TagContent(State state) => state
            .ParseZeroOrMore(state1 => state1
                .ParseFirst(ClosedTag, CdataTag, TagString));

        static State? CdataTag(State state) => state
            .Parse(StringValue("<![CDATA["))
            ?.Parse(CdataContent)
            ?.Parse(StringValue("]]>"));

        static State? CdataContent(State state)
        {
            var i = 0;
            while (
                state.Position + i < state.Code.Length &&
                (
                    state.Position + i > state.Code.Length - 3 ||
                    state.Code[(state.Position + i)..(state.Position + i + 3)] != "]]>")
                )
                i++;

            return state with { Position = state.Position + i };
        }

        static State? TagString(State state)
        {
            var i = 0;
            while (
                state.Position + i < state.Code.Length &&
                state.Code[state.Position + i] != '<')
                i++;
            if (i == 0)
                return null;
            return state with { Position = state.Position + i };
        }
    }

    public class Ex641_MyCircularDeque
    {
        private int[] values;
        private int front;
        private int back;
        private int count;
        private readonly int k;

        public Ex641_MyCircularDeque(int k)
        {
            values = new int[k];
            front = -1;
            back = 0;
            count = 0;
            this.k = k;
        }

        public bool InsertFront(int value)
        {
            if (IsFull()) return false;
            front = (front + 1) % k;
            values[front] = value;
            ++count;
            return true;
        }

        public bool InsertLast(int value)
        {
            if (IsFull()) return false;
            back = (back - 1 + k) % k;
            values[back] = value;
            ++count;
            return true;
        }

        public bool DeleteFront()
        {
            if (IsEmpty()) return false;
            var value = values[front];
            front = (front - 1 + k) % k;
            --count;
            return true;
        }

        public bool DeleteLast()
        {
            if (IsEmpty()) return false;
            var value = values[back];
            back = (back + 1) % k;
            --count;
            return true;
        }

        public int GetFront()
        {
            if (IsEmpty()) return -1;
            return values[front];
        }

        public int GetRear()
        {
            if (IsEmpty()) return -1;
            return values[back];
        }

        public bool IsEmpty() => count == 0;

        public bool IsFull() => count == k;
    }

    public class Ex715_RangeModule
    {
        private readonly SortedSet<(int, int)> intervals = new();

        public void AddRange(int left, int right)
        {
            var overlaps = intervals.GetViewBetween((left, -1), (right, +1)).ToList();
            if (overlaps.Count == 0 &&
                intervals.GetViewBetween((right, +1), (int.MaxValue, +1)).FirstOrDefault((-1, +1)).Item2 < 0)
                return;

            var addStart = overlaps.Count == 0 || overlaps[0].Item2 > 0;
            var addEnd = overlaps.Count == 0 || overlaps[^1].Item2 < 0;

            foreach (var overlap in overlaps)
                intervals.Remove(overlap);

            if (addStart) intervals.Add((left, +1));
            if (addEnd) intervals.Add((right, -1));
        }

        public bool QueryRange(int left, int right)
        {
            var overlaps = intervals.GetViewBetween((left, +1), (right, -1)).ToList();
            if (overlaps.Count > 2)
                return false;
            if (overlaps.Count == 2)
                return overlaps[0].Item1 == left && overlaps[^1].Item1 == right;
            if (overlaps.Count == 1)
                return overlaps[0].Item1 == left || overlaps[^1].Item1 == right;

            return intervals.GetViewBetween((right, +1), (int.MaxValue, +1)).FirstOrDefault((-1, +1)).Item2 < 0;
        }

        public void RemoveRange(int left, int right)
        {
            var overlaps = intervals.GetViewBetween((left, +1), (right, -1)).ToList();
            if (overlaps.Count == 0 &&
                intervals.GetViewBetween((right, +1), (int.MaxValue, +1)).FirstOrDefault((-1, +1)).Item2 > 0)
                return;

            var addEnd = overlaps.Count == 0 || overlaps[0].Item2 < 0;
            var addStart = overlaps.Count == 0 || overlaps[^1].Item2 > 0;

            foreach (var overlap in overlaps)
                intervals.Remove(overlap);

            if (addEnd) intervals.Add((left, -1));
            if (addStart) intervals.Add((right, +1));
        }
    }

    public static int Ex718_FindLength_DP(int[] nums1, int[] nums2)
    {
        var solutions = new Dictionary<(int, int), int> { };

        var max = 0;
        for (var i = 0; i < nums1.Length; i++)
            for (var j = 0; j < nums2.Length; j++)
                max = Math.Max(max, FindLength(i, j));
        return max;

        int FindLength(int i, int j)
        {
            if (i >= nums1.Length || j >= nums2.Length || nums1[i] != nums2[j])
                return 0;
            if (solutions.TryGetValue((i, j), out var solution))
                return solution;

            solution = 1 + FindLength(i + 1, j + 1);
            solutions[(i, j)] = FindLength(i + 1, j + 1);
            return solution;
        }

    }

    public static int Ex718_FindLength_DPOptimized(int[] nums1, int[] nums2)
    {
        var solutions = new Dictionary<(int, int), int> { };

        var max = 0;
        for (var i = 0; i < nums1.Length; i++)
            for (var j = 0; j < nums2.Length; j++)
                max = Math.Max(max, FindLength(i, j, max));
        return max;

        int FindLength(int i, int j, int max)
        {
            if (nums1.Length - i <= max || nums2.Length - j <= max)
                return max;

            if (i >= nums1.Length || j >= nums2.Length || nums1[i] != nums2[j])
                return 0;
            if (solutions.TryGetValue((i, j), out var solution))
                return solution;

            solution = 1 + FindLength(i + 1, j + 1, max - 1);
            solutions[(i, j)] = solution;
            return solution;
        }
    }

    public static int Ex718_FindLength_DPBottomUp(int[] nums1, int[] nums2)
    {
        var n = nums1.Length;
        var m = nums2.Length;

        var solutions = new int[n + 1, m + 1];

        var max = 0;
        for (var d = 0; d < n + m - 1; ++d)
        {
            var i = d < m ? 0 : d - m + 1;
            var j = d - i;
            while (i < nums1.Length && j >= 0)
            {
                Console.WriteLine($"Filling in ({i}, {j})...");

                max = Math.Max(
                    max,
                    solutions[i, j] = (nums1[i] == nums2[j])
                        ? 1 + (i > 0 && j > 0 ? solutions[i - 1, j - 1] : 0)
                        : 0);
                i++; j--;
            }
        }

        return max;
    }

    public static string Ex727_MinWindow(string s, string t)
    {
        var n = s.Length;
        var m = t.Length;
        var solutions = new Dictionary<(int, int), int> { };

        var minLength = int.MaxValue;
        var minIndex = -1;
        for (var i = 0; i < n; i++)
        {
            var length = MinWindow(i, 0);
            if (length >= 0 && length < minLength)
            {
                minLength = length;
                minIndex = i;
            }
        }

        return minIndex >= 0 ? s[minIndex..(minIndex + minLength)] : string.Empty;

        int MinWindow(int i, int j)
        {
            if (j >= m) return 0;
            if (i >= n) return -1;
            if (solutions.TryGetValue((i, j), out var solution)) return solution;

            if (s[i] == t[j])
                solution = MinWindow(i + 1, j + 1);
            else
                solution = MinWindow(i + 1, j);

            solution = solution >= 0 ? solution + 1 : -1;
            solutions[(i, j)] = solution;
            return solution;
        }
    }

    public class Ex729_MyCalendar
    {
        private SortedSet<(int value, int type)> events = new();

        public bool Book(int start, int end)
        {
            var firstOverlap = events
                .GetViewBetween((start, +1), (end, -1))
                .FirstOrDefault((int.MinValue, -1));
            if (firstOverlap.Item1 != int.MinValue) return false;

            firstOverlap = events
                .GetViewBetween((end, +1), (int.MaxValue, +1))
                .FirstOrDefault((int.MaxValue, +1));
            if (firstOverlap.Item2 == -1) return false;

            events.Add((start, +1));
            events.Add((end, -1));
            return true;
        }
    }

    public static bool Ex737_AreSentencesSimilarTwo(
        string[] sentence1, string[] sentence2, IList<IList<string>> similarPairs)
    {
        if (sentence1.Length != sentence2.Length) return false;

        var simParents = new Dictionary<string, string>();

        foreach (var similarPair in similarPairs)
            AreSimilar(similarPair[0], similarPair[1]);

        for (var i = 0; i < sentence1.Length; i++)
            if (Root(sentence1[i]) != Root(sentence2[i]))
                return false;
        return true;

        void AreSimilar(string word1, string word2)
        {
            if (word1 == word2) return;

            var root1 = Root(word1);
            var root2 = Root(word2);
            if (root1 == root2) return;
            simParents[root1] = root2;
        }

        string Root(string word)
        {
            var root = word;
            while (simParents.TryGetValue(root, out var parent))
                root = parent;
            return root;
        }
    }

    public static int Ex738_MonotoneIncreasingDigits(int n)
    {
        var digits = n.ToString();
        var lastStrictlyIncreasing = 0;
        int i;
        for (i = 1; i < digits.Length; i++)
        {
            if (digits[i - 1] > digits[i])
                break;
            if (digits[i - 1] < digits[i])
                lastStrictlyIncreasing = i;
        }

        if (i == digits.Length)
            return n;
        return int.Parse(
            digits[..lastStrictlyIncreasing] +
            (char)(digits[lastStrictlyIncreasing] - 1) +
            new string('9', digits.Length - 1 - lastStrictlyIncreasing));
    }

    public static int[] Ex755_PourWater_Quadratic(int[] heights, int volume, int k)
    {
        var n = heights.Length;

        for (var i = 0; i < volume; i++)
        {
            int p1 = k, p2 = k;
            while (p1 > 0 && heights[p1 - 1] <= heights[p1])
            {
                if (heights[p1 - 1] < heights[p1])
                    p2 = p1 - 1;
                p1--;
            }

            if (heights[p2] < heights[k])
            {
                heights[p2]++;
                continue;
            }

            p1 = k; p2 = k;
            while (p1 < n - 1 && heights[p1 + 1] <= heights[p1])
            {
                if (heights[p1 + 1] < heights[p1])
                    p2 = p1 + 1;
                p1++;
            }

            heights[p2]++;
        }

        return heights;
    }

    public interface Ex843_IMaster
    {
        public abstract int Guess(string word);
    }

    public static void Ex843_FindSecretWord(string[] words, Ex843_IMaster master)
    {
        var W = words.Length; // number of words => indexed by w
        var P = words[0].Length; // number of char positions in each word (6) => indexed by p
        var C = ('z' - 'a') + 1; // number of possible chars values (26) => indexed by c

        // Build:
        // - frequencies matrix: char c and position p => frequency of c at position p across all words
        // - distinctChars array of sets: position p => distinct chars at position p across all words
        var frequencies = new float[C, P];
        var distinctChars = new ISet<char>[P];
        for (var p = 0; p < P; p++)
        {
            distinctChars[p] = new HashSet<char> { };

            for (var w = 0; w < W; w++)
            {
                var word = words[w];
                var c = word[p] - 'a';
                frequencies[c, p]++;
                distinctChars[p].Add(word[p]);
            }
        }

        Console.WriteLine("FREQUENCIES:");
        Console.WriteLine(string.Join("\n", Enumerable
            .Range(0, frequencies.GetLength(0))
            .Select(c => $"c = {(char)('a' + c)} => " + string.Join(" ", Enumerable
                .Range(0, frequencies.GetLength(1))
                .Select(p => frequencies[c, p])))));

        Console.WriteLine("DISTINCT CHARS:");
        Console.WriteLine(string.Join("\n", Enumerable
            .Range(0, distinctChars.Length)
            .Select((c, i) => $"p = {i} => " + string.Join(" ", distinctChars[c]))));

        // Transform frequencies into scores for deviation from the best case 
        var scores = new float[C, P];
        for (var p = 0; p < P; p++)
        {
            for (var c = 0; c < C; c++)
            {
                scores[c, p] = (float)Math.Pow(W / 2.0 - frequencies[c, p], 2);
            }
        }

        var ws = new HashSet<string>(words);

        while (true)
        {
            var word = ws.MinBy(w => w.Select((c, i) => scores[c - 'a', i]).Sum());
            var charsInCommon = master.Guess(word);
            if (charsInCommon == word.Length)
                return;
            var wordsToRemove = ws.Where(w => w.Zip(word).Count(c => c.First == c.Second) != charsInCommon);
            foreach (var wordToRemove in wordsToRemove)
                ws.Remove(wordToRemove);
            ws.Remove(word);

            Console.WriteLine($"Remaining {ws.Count} words");
        }
    }

    public static ListNode Ex876_MiddleNode(ListNode head)
    {
        var slow = head;
        var fast = head.next;

        while (fast != null)
        {
            slow = slow.next;
            fast = fast?.next?.next;
        }

        return slow;
    }

    public static int Ex907_SumSubarrayMins(int[] arr)
    {
        var n = arr.Length;

        var result = 0L;
        var (leftIndices, rightIndices) = CalculateSideIndices();
        for (var i = 0; i < n; i++)
        {
            var leftIndex = leftIndices[i];
            var rightIndex = rightIndices[i];

            var numberOfIntervals = (i - leftIndex + 1) * (rightIndex - i + 1);
            Console.WriteLine($"i = {i}, leftIndex = {leftIndex}, rightIndex = {rightIndex}, numberOfIntervals = {numberOfIntervals}");

            result = (result + (long)arr[i] * numberOfIntervals) % 1_000_000_007;
        }

        return (int)(result);

        (int[], int[]) CalculateSideIndices()
        {
            var leftStack = new List<int>();
            var leftIndices = new int[n];
            var rightStack = new List<int>();
            var rightIndices = new int[n];

            for (var i = 0; i < n; i++)
            {
                while (leftStack.Count > 0 && arr[leftStack[^1]] > arr[i])
                    leftStack.RemoveAt(leftStack.Count - 1);
                leftIndices[i] = leftStack.Count > 0 ? leftStack[^1] + 1 : 0;
                leftStack.Add(i);

                while (rightStack.Count > 0 && arr[rightStack[^1]] >= arr[n - 1 - i])
                    rightStack.RemoveAt(rightStack.Count - 1);
                rightIndices[n - 1 - i] = rightStack.Count > 0 ? rightStack[^1] - 1 : n - 1;
                rightStack.Add(n - 1 - i);
            }

            return (leftIndices, rightIndices);
        }
    }

    public static int Ex992_SubarraysWithKDistinct_Quadratic(int[] nums, int k)
    {
        var n = nums.Length;
        var result = 0;
        for (var windowLength = k; windowLength <= n; windowLength++)
        {
            var counts = new Dictionary<int, int>();
            for (var i = 0; i < windowLength; i++)
            {
                if (counts.TryGetValue(nums[i], out var count))
                    counts[nums[i]] = count + 1;
                else
                    counts[nums[i]] = 1;
            }

            for (var windowStart = 0; windowStart <= n - windowLength; windowStart++)
            {
                if (counts.Count == k)
                {
                    Console.WriteLine(string.Join(" ", nums[windowStart..(windowStart + windowLength)]));
                    result++;
                }

                counts[nums[windowStart]]--;
                if (counts[nums[windowStart]] == 0)
                    counts.Remove(nums[windowStart]);

                if (windowStart + windowLength < n)
                {
                    if (counts.TryGetValue(nums[windowStart + windowLength], out var count))
                        counts[nums[windowStart + windowLength]] = count + 1;
                    else
                        counts[nums[windowStart + windowLength]] = 1;
                }
            }
        }
        return result;
    }

    public static int Ex940_DistinctSubseqII(string s)
    {
        var n = s.Length;
        var mod = 1_000_000_007;

        var results = new long[n];
        results[0] = 2;
        var previous = new Dictionary<int, int> { [s[0]] = 0 };
        for (var i = 1; i < n; i++)
        {
            results[i] = 2 * results[i - 1];
            if (previous.TryGetValue(s[i], out var prevIndex))
            {
                var repetitions = prevIndex > 0 ? results[prevIndex - 1] : 1;
                results[i] = (results[i] + mod - repetitions) % mod;
            }

            previous[s[i]] = i;
        }

        return (int)(results[^1] + mod - 1) % mod;
    }

    public static int Ex992_SubarraysWithKDistinct_ForAndWhile(int[] nums, int k)
    {
        var n = nums.Length;
        var result = 0;

        if (nums.Length == 0) return 0;

        for (var i = 0; i <= n - k; i++)
        {
            var distinctValues = new HashSet<int> { };

            var j = i;
            while (j < n && distinctValues.Count <= k)
            {
                distinctValues.Add(nums[j]);
                if (distinctValues.Count == k)
                    result++;
                j++;
            }
        }

        return result;
    }

    private class TimestampComparer : IComparer<int[]>
    {
        public int Compare(int[] first, int[] second) => first[0] - second[0];
    }

    public static int Ex1101_EarliestAcq(int[][] logs, int n)
    {
        var parents = new int[n];
        var ranks = new int[n];
        var roots = n;
        for (var i = 0; i < n; i++)
            parents[i] = i;

        int Root(int i)
        {
            if (i == parents[i])
                return i;

            var root = Root(parents[i]);
            parents[i] = root;
            return root;
        }

        Array.Sort(logs, new TimestampComparer());
        for (var i = 0; i < logs.Length; i++)
        {
            Connect(logs[i][1], logs[i][2]);
            if (roots == 1)
                return logs[i][0];
        }

        return -1;

        void Connect(int i, int j)
        {
            var rootI = Root(i);
            var rootJ = Root(j);
            if (rootI == rootJ)
                return;

            if (ranks[rootI] >= ranks[rootJ])
            {
                parents[rootJ] = rootI;
                ranks[rootI] = Math.Max(ranks[rootJ] + 1, ranks[rootI]);
            }
            else
            {
                parents[rootI] = rootJ;
                ranks[rootJ] = Math.Max(ranks[rootI] + 1, ranks[rootJ]);
            }

            roots--;
        }
    }

    public static string Ex1138_AlphabetBoardPath(string target)
    {
        var moves = new StringBuilder();
        var currI = 0; var currJ = 0;
        foreach (var c in target)
        {
            var (targetI, targetJ) = TargetPosition(c);
            IssueMoves(moves, (currI, currJ), (targetI, targetJ));
            moves.Append('!');
            (currI, currJ) = (targetI, targetJ);
        }
        return moves.ToString();

        static (int, int) TargetPosition(char c) => ((c - 'a') / 5, (c - 'a') % 5);

        static void IssueMoves(StringBuilder moves, (int i, int j) initial, (int i, int j) final)
        {
            if (initial == final) return;
            if (initial == (5, 0))
            {
                moves.Append('U');
                IssueMoves(moves, (4, 0), final);
                return;
            }

            if (final == (5, 0))
            {
                IssueMoves(moves, initial, (4, 0));
                moves.Append('D');
                return;
            }

            var horintalMove = initial.i < final.i ? 'D' : 'U';
            for (var i = 0; i < Math.Abs(final.i - initial.i); i++)
                moves.Append(horintalMove);

            var verticalMove = initial.j < final.j ? 'R' : 'L';
            for (var i = 0; i < Math.Abs(final.j - initial.j); i++)
                moves.Append(verticalMove);

        }
    }

    public static IList<string> Ex1152_MostVisitedPattern(string[] username, int[] timestamp, string[] website)
    {
        // group by username => sort each group by timestamp
        // go through each group (in timestamp order)
        // consider each triple of indices (i, j, k) where i < j < k and increase occurrence of that pattern by 1
        // gather in a priority queue of patterns prioritized by their occurrence
        // get the pattern with max priority
        // if multiple have same prio => take the one with lowest lexicographic order
        var visitsByUser =
            from e in Enumerable.Zip(username, timestamp, website)
            group e by e.First into userVisits
            select new
            {
                username = userVisits.Key,
                visits = userVisits.OrderBy(e => e.Second).Select(e => e.Third).ToList(),
            };

        //foreach (var userVisits in visitsByUser)
        //    Console.WriteLine($"{userVisits.username} => {string.Join(" ", userVisits.visits)}");

        var scores = new Dictionary<(string, string, string), int>();
        (string, string, string) maxScorePattern = ("", "", "");
        int maxScore = 0;
        foreach (var userVisits in visitsByUser)
        {
            var distinctPatterns = new HashSet<(string, string, string)>();
            var visits = userVisits.visits;
            for (var i = 0; i < visits.Count; i++)
                for (var j = i + 1; j < visits.Count; j++)
                    for (var k = j + 1; k < visits.Count; k++)
                    {
                        var pattern = (visits[i], visits[j], visits[k]);
                        if (distinctPatterns.Contains(pattern)) continue;
                        distinctPatterns.Add(pattern);
                        if (!scores.TryGetValue(pattern, out var score))
                            score = 0;
                        score++;
                        scores[pattern] = score;
                        if (score > maxScore || (score == maxScore &&
                            Compare(pattern, maxScorePattern) < 0))
                        {
                            maxScore = score;
                            maxScorePattern = pattern;
                        }
                    }
        }

        //foreach (var (pattern, score) in scores)
        //    Console.WriteLine($"{pattern} => {score}");

        return new string[] { maxScorePattern.Item1, maxScorePattern.Item2, maxScorePattern.Item3 };

        static int Compare((string, string, string) x, (string, string, string) y) =>
            string.Compare($"{x.Item1} {x.Item2} {x.Item3}", $"{y.Item1} {y.Item2} {y.Item3}");
    }

    public static bool Ex1153_CanConvert(string s1, string s2, int alphabetSize = 26)
    {
        var n = s1.Length;
        if (n != s2.Length) return false;
        if (s1 == s2) return true;

        var distinctChars1 = new HashSet<char>(s1);
        var distinctChars2 = new HashSet<char>(s2);

        if (distinctChars1.Count >= alphabetSize && distinctChars2.Count >= alphabetSize)
            return false;
        if (distinctChars2.Count > distinctChars1.Count)
            return false;

        var mapping = new Dictionary<char, char> { };
        for (var i = 0; i < n; i++)
        {
            if (mapping.TryGetValue(s1[i], out var c) && c != s2[i])
                return false;
            mapping[s1[i]] = s2[i];
        }

        return true;
    }

    public static IList<int> Ex1243_TransformArray(int[] arr)
    {
        var n = arr.Length;
        var changes = 1;
        while (changes > 0)
        {
            changes = 0;
            var previous = arr[0];
            for (var i = 1; i < n - 1; i++)
            {
                var current = arr[i];
                if (previous > arr[i] && arr[i + 1] > arr[i])
                {
                    arr[i]++; changes++;
                }
                else if (previous < arr[i] && arr[i + 1] < arr[i])
                {
                    arr[i]--; changes++;
                }
                previous = current;
            }
        }

        return arr;
    }

    public static int Ex1293_ShortestPath(int[][] grid, int kMax)
    {
        var m = grid.Length;
        var n = grid[0].Length;

        var queue = new Queue<(int, int, int, int)> { };
        var visited = new HashSet<(int, int, int)> { };
        queue.Enqueue((0, 0, kMax, 0));
        while (queue.Count > 0)
        {
            var (i, j, k, d) = queue.Dequeue();
            if (i == m - 1 && j == n - 1) return d;

            var vertex = (i, j, k);
            if (visited.Contains(vertex)) continue;
            visited.Add(vertex);

            if (i > 0 && k - grid[i - 1][j] is var k1 and >= 0)
                queue.Enqueue((i - 1, j, k1, d + 1));
            if (i < m - 1 && k - grid[i + 1][j] is var k2 and >= 0)
                queue.Enqueue((i + 1, j, k2, d + 1));
            if (j > 0 && k - grid[i][j - 1] is var k3 and >= 0)
                queue.Enqueue((i, j - 1, k3, d + 1));
            if (j < n - 1 && k - grid[i][j + 1] is var k4 and >= 0)
                queue.Enqueue((i, j + 1, k4, d + 1));
        }

        return -1;
    }

    public static int Ex1423_MaxScore_DP(int[] cardPoints, int k)
    {
        var n = cardPoints.Length;
        if (n <= k)
            return cardPoints.Sum();

        var solutions = new Dictionary<(int, int, int), int> { };
        return MaxScore(0, n - 1, k);

        int MaxScore(int i, int j, int k)
        {
            if (i > j) return 0;
            if (k == 0) return 0;
            if (solutions.TryGetValue((i, j, k), out var solution)) return solution;

            var c1 = cardPoints[i] + MaxScore(i + 1, j, k - 1);
            var c2 = cardPoints[j] + MaxScore(i, j - 1, k - 1);
            solution = Math.Max(c1, c2);
            solutions[(i, j, k)] = solution;
            return solution;
        }
    }

    public static int Ex1423_MaxScore_Window(int[] cardPoints, int k)
    {
        var n = cardPoints.Length;
        var partial = 0;
        for (var i = 0; i < n - k; i++)
            partial += cardPoints[i];

        var min = partial;
        var total = partial;
        for (var i = 0; i < k; i++)
        {
            partial += cardPoints[n - k + i] - cardPoints[i];
            total += cardPoints[n - k + i];
            min = Math.Min(min, partial);
        }
        return total - min;
    }

    public static int Ex1443_MinTime(int n, int[][] edges, IList<bool> hasApple)
    {
        var adjs = new ISet<int>[n];
        for (var i = 0; i < edges.Length; i++)
        {
            var edge = edges[i];
            if (adjs[edge[0]] == null)
                adjs[edge[0]] = new HashSet<int> { edge[1] };
            else
                adjs[edge[0]].Add(edge[1]);

            if (adjs[edge[1]] == null)
                adjs[edge[1]] = new HashSet<int> { edge[0] };
            else
                adjs[edge[1]].Add(edge[0]);
        }

        return Dfs(0, new HashSet<int> { }).edges;

        (int edges, int apples) Dfs(int vertex, ISet<int> visited)
        {
            if (visited.Contains(vertex)) return (0, 0);
            visited.Add(vertex);

            var apples = hasApple[vertex] ? 1 : 0;
            var edges = 0;
            if (adjs[vertex] != null)
            {
                foreach (var neighbor in adjs[vertex])
                {
                    var (neighborEdges, neighborApples) = Dfs(neighbor, visited);
                    apples += neighborApples;
                    if (neighborApples > 0)
                        edges += 2 + neighborEdges;
                }
            }

            return (edges, apples);
        }
    }

    public static int Ex1443_MinTimeOptimized(int n, int[][] edges, IList<bool> hasApple)
    {
        var adjs = new ISet<int>[n];
        for (var i = 0; i < edges.Length; i++)
        {
            var edge = edges[i];
            if (adjs[edge[0]] == null)
                adjs[edge[0]] = new HashSet<int> { edge[1] };
            else
                adjs[edge[0]].Add(edge[1]);

            if (adjs[edge[1]] == null)
                adjs[edge[1]] = new HashSet<int> { edge[0] };
            else
                adjs[edge[1]].Add(edge[0]);
        }

        return Math.Max(0, Dfs(0, new bool[n]) - 2);

        int Dfs(int vertex, bool[] visited)
        {
            if (visited[vertex]) return 0;
            visited[vertex] = true;

            var edges = 0;
            if (adjs[vertex] != null)
            {
                foreach (var neighbor in adjs[vertex])
                {
                    var neighborEdges = Dfs(neighbor, visited);
                    edges += neighborEdges;
                }
            }

            return hasApple[vertex] || edges > 0 ? edges + 2 : 0;
        }
    }

    public static int Ex1499_FindMaxValueOfEquation(int[][] points, int k)
    {
        var n = points.Length;
        var max = int.MinValue;
        for (var i = 0; i < n - 1; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var d = points[j][0] - points[i][0];
                if (d > k) break;
                max = Math.Max(max, d + points[j][1] + points[i][1]);
            }
        }

        return max;
    }

    public static int Ex1499_FindMaxValueOfEquation_WithHeap(int[][] points, int k)
    {
        var X = 0;
        var Y = 1;
        var n = points.Length;
        var queue = new PriorityQueue<int, int>();

        var max = int.MinValue;
        for (var j = 1; j < n; j++)
        {
            queue.Enqueue(j - 1, points[j - 1][X] - points[j - 1][Y]);

            while (queue.Count > 0 && points[j][X] - points[queue.Peek()][X] > k)
                queue.Dequeue();

            if (queue.Count == 0) continue;

            var i = queue.Peek();
            max = Math.Max(max, points[j][X] + points[j][Y] + points[i][Y] - points[i][X]);
        }
        return max;
    }

    public static string[] Ex1548_MostSimilarPath(
        int n, IList<IList<int>> roads, string[] names, string[] targetPath)
    {
        if (n <= 0 || targetPath.Length == 0) return Array.Empty<string>();

        var namesToIds = names.Select((name, index) => (name, index)).ToDictionary(c => c.name, c => c.index);
        var targetPathIds = targetPath.Select(name => namesToIds[name]).ToArray();

        var adjs = new ISet<int>[n];
        for (var i = 0; i < roads.Count; i++)
        {
            var city1 = roads[i][0];
            var city2 = roads[i][1];
            if (adjs[city1] == null)
                adjs[city1] = new HashSet<int> { city2 };
            else
                adjs[city1].Add(city2);
            if (adjs[city2] == null)
                adjs[city2] = new HashSet<int> { city1 };
            else
                adjs[city2].Add(city1);
        }

        var solutions = new Dictionary<(int, int), (int, int[])> { };
        return MostSimilarPath(targetPathIds[0], 0).path!.Select(v => names[v]).ToArray();

        (int editDistance, int[] path) MostSimilarPath(int vertex, int j)
        {
            var nextTargetVertex = vertex != targetPathIds[j] ? 1 : 0;
            if (j == targetPath.Length - 1) 
                return (nextTargetVertex, new[] { vertex });

            if (solutions.TryGetValue((vertex, j), out var solution)) return solution;

            int minEditDistance = int.MaxValue;
            int[] minPath = Array.Empty<int>();
            foreach (var neighbor in adjs[vertex].OrderBy(i => i))
            {
                var (editDistance, path) = MostSimilarPath(neighbor, j + 1);

                if (minEditDistance > editDistance)
                {
                    minEditDistance = editDistance;
                    minPath = path;
                }
            }

            solution = minEditDistance == int.MaxValue
                ? (minEditDistance, minPath)
                : (minEditDistance + nextTargetVertex, minPath.Prepend(vertex).ToArray());

            solutions[(vertex, j)] = solution;
            return solution;
        }
    }

    public static int Ex1695_MaximumUniqueSubarray(int[] nums)
    {
        var n = nums.Length;

        // Window nums[i..(j + 1)] 
        var i = 0; var j = 0; var sum = nums[j]; var result = nums[0];
        var positions = new Dictionary<int, int> { [nums[0]] = 0 };
        while (j < n)
        {
            Console.WriteLine($"i = {i}, j = {j}, sum = {sum}, result = {result}");

            // Push j as far as possible, i.e. until next j is already in the window
            while (j < n - 1 && (!positions.TryGetValue(nums[j + 1], out var position) || position < i || position > j))
            {
                j++;
                positions[nums[j]] = j;
                sum += nums[j];
            }

            result = Math.Max(result, sum);

            if (j == n - 1)
                break;

            i = positions[nums[j + 1]] + 1;
            j = i;
            sum = nums[j];
            positions[nums[j]] = j;
        }

        return result;
    }

    public static int Ex1695_MaximumUniqueSubarrayOptimized(int[] nums)
    {
        var n = nums.Length;

        // Window nums[i..(j + 1)] 
        var i = 0; var j = 0; var sums = new int[n]; var result = nums[0];
        sums[0] = nums[j];
        var positions = new Dictionary<int, int> { [nums[0]] = 0 };
        while (j < n)
        {
            // Push j as far as possible, i.e. until next j is already in the window
            while (j < n - 1 && (!positions.TryGetValue(nums[j + 1], out var position) || position < i || position > j))
            {
                j++;
                positions[nums[j]] = j;
                sums[j] = (j > 0 ? sums[j - 1] : 0) + nums[j];
            }

            result = Math.Max(result, sums[j] - (i > 0 ? sums[i - 1] : 0));

            if (j == n - 1)
                break;

            j++;
            i = positions[nums[j]] + 1;

            positions[nums[j]] = j;
            sums[j] = sums[j - 1] + nums[j];
        }

        return result;
    }

    public static int Ex1696_MaxResult_DP(int[] nums, int k)
    {
        var solutions = new Dictionary<int, int> { };
        var n = nums.Length;
        return MaxResult(0);

        int MaxResult(int i) // Suffixes nums[i..]
        {
            if (i > n - 1) return int.MinValue;
            if (i == n - 1) return nums[i];
            if (solutions.TryGetValue(i, out var solution)) return solution;

            solution = int.MinValue;

            for (var j = i + 1; j <= Math.Min(n - 1, i + k); j++)
            {
                var remainingPathResult = MaxResult(j);
                if (remainingPathResult != int.MinValue)
                    solution = Math.Max(solution, nums[i] + remainingPathResult);
            }

            solutions[i] = solution;
            return solution;
        }
    }

    public static int Ex1696_MaxResult_DP_BottomUp(int[] nums, int k)
    {
        var n = nums.Length;
        var solutions = new int[n];

        solutions[n - 1] = nums[n - 1];
        for (var i = n - 2; i >= 0; i--)
        {
            var solution = int.MinValue;
            for (var j = i + 1; j <= Math.Min(n - 1, i + k); j++)
            {
                if (solutions[j] != int.MinValue)
                    solution = Math.Max(solution, nums[i] + solutions[j]);
            }
            solutions[i] = solution;
        }

        return solutions[0];
    }

    public static int Ex1696_MaxResult_WithHeap(int[] nums, int k)
    {
        var n = nums.Length;
        var solutions = new int[n];
        var values = new PriorityQueue<int, int> { };

        solutions[n - 1] = nums[n - 1];
        for (var i = n - 2; i >= 0; i--)
        {
            values.Enqueue(i + 1, -solutions[i + 1]);

            while (values.Peek() > i + k)
                values.Dequeue();

            solutions[i] = nums[i] + solutions[values.Peek()];
        }

        return solutions[0];
    }

    public static ListNode Ex1721_SwapNodes(ListNode head, int k)
    {
        var first = head;

        int i;
        for (i = 1; i < k && first != null; i++)
            first = first.next;

        if (i != k)
            return head;

        var current = first;
        var second = head;
        while (current.next != null)
        {
            current = current.next;
            second = second.next;
        }

        (first.val, second.val) = (second.val, first.val);
        return head;
    }

    public static int[] Ex1834_GetOrder(int[][] tasks)
    {
        var waitingTasks = new PriorityQueue<int, (int enqueueTime, int taskId)>();
        var readyTasks = new PriorityQueue<int, (int processingTime, int taskId)>();

        for (var i = 0; i < tasks.Length; ++i)
        {
            var task = tasks[i];
            waitingTasks.Enqueue(i, (task[0], i));
        }

        var schedule = new int[tasks.Length];
        var k = 0;
        var time = 0;
        while (k < schedule.Length)
        {
            while (waitingTasks.Count > 0 && tasks[waitingTasks.Peek()][0] <= time)
            {
                var waitingTaskId = waitingTasks.Dequeue();
                readyTasks.Enqueue((waitingTaskId), (tasks[waitingTaskId][1], waitingTaskId));
            }

            if (readyTasks.Count == 0)
            {
                time = tasks[waitingTasks.Peek()][0];
                continue;
            }

            var nextTaskId = readyTasks.Dequeue();
            schedule[k++] = nextTaskId;
            time += tasks[nextTaskId][1];
        }

        return schedule;
    }

    public static int[] Ex1882_AssignTasks_SingleQueue(int[] servers, int[] tasks)
    {
        var serversQueue = new PriorityQueue<(int serverId, int freeTime), (int freeTime, int weight, int serverId)>();

        for (var i = 0; i < servers.Length; ++i)
            serversQueue.Enqueue((i, 0), (0, servers[i], i));

        var ans = new int[tasks.Length];
        for (var taskId = 0; taskId < tasks.Length; ++taskId)
        {
            var (nextAvailableServerId, nextAvailableServerFreeTime) = serversQueue.Dequeue();
            while (nextAvailableServerFreeTime < taskId)
            {
                serversQueue.Enqueue((nextAvailableServerId, taskId), (taskId, servers[nextAvailableServerId], nextAvailableServerId));
                (nextAvailableServerId, nextAvailableServerFreeTime) = serversQueue.Dequeue();
            }

            var startTime = Math.Max(taskId, nextAvailableServerFreeTime);
            var endTime = startTime + tasks[taskId];

            ans[taskId] = nextAvailableServerId;

            serversQueue.Enqueue(
                (nextAvailableServerId, startTime + tasks[taskId]),
                (endTime, servers[nextAvailableServerId], nextAvailableServerId));
        }

        return ans;
    }

    public static int[] Ex1882_AssignTasks_TwoQueues(int[] servers, int[] tasks)
    {
        var freeServers = new PriorityQueue<int, (int weight, int serverId)>();
        var busyServers = new PriorityQueue<(int serverId, int freeTime), (int freeTime, int weight, int serverId)>();

        for (var i = 0; i < servers.Length; ++i)
            freeServers.Enqueue(i, (servers[i], i));

        var ans = new int[tasks.Length];
        for (var taskId = 0; taskId < tasks.Length; ++taskId)
        {
            while (busyServers.Count > 0 && busyServers.Peek().freeTime <= taskId)
            {
                var (busyServerId, busyServerFreeTime) = busyServers.Dequeue();
                freeServers.Enqueue(busyServerId, (servers[busyServerId], busyServerId));
            }

            var (nextAvailableServerId, nextAvailableServerFreeTime) =
                freeServers.Count > 0 ? (freeServers.Dequeue(), taskId) : busyServers.Dequeue();

            var startTime = Math.Max(taskId, nextAvailableServerFreeTime);
            var endTime = startTime + tasks[taskId];

            ans[taskId] = nextAvailableServerId;

            busyServers.Enqueue(
                (nextAvailableServerId, startTime + tasks[taskId]),
                (endTime, servers[nextAvailableServerId], nextAvailableServerId));
        }

        return ans;
    }

    public static long Ex1937_MaxPoints_DPTopDown(int[][] points)
    {
        var solutions = new Dictionary<(int, int), long>();
        var rows = points.Length;
        var cols = points[0].Length;

        var result = long.MinValue;
        for (var col = 0; col < cols; col++)
            result = Math.Max(result, MaxPoints(0, col));
        return result;

        long MaxPoints(int row, int col)
        {
            if (row == rows) return 0;
            if (solutions.TryGetValue((row, col), out var solution)) return solution;

            solution = long.MinValue;
            for (var nextCol = 0; nextCol < cols; nextCol++)
            {
                solution = Math.Max(
                    solution,
                    points[row][col] + MaxPoints(row + 1, nextCol) - Math.Abs(nextCol - col)
                );
            }

            solutions[(row, col)] = solution;
            return solution;
        }
    }

    public static int Ex1987_NumberOfUniqueGoodSubsequences(string binary)
    {
        var n = binary.Length;
        var mod = 1_000_000_007;

        var previous = new int[2] { -1, -1 };
        var results = new int[n];
        var anyZero = false;

        int i = 0;
        while (i < n && binary[i] == '0')
        {
            anyZero = true;
            i++;
        }

        if (i == n)
            return 1;

        results[i] = 1;
        previous[1] = i;
        i++;

        while (i < n)
        {
            var value = binary[i] - '0';
            if (value == 0) anyZero = true;

            results[i] = (results[i - 1] * 2) % mod;
            if (previous[value] >= 0)
            {
                var repetitions = previous[value] > 0 ? results[previous[value] - 1] : 0;
                results[i] = (results[i] + mod - repetitions) % mod;
            }

            previous[value] = i;
            i++;
        }

        return (results[^1] + (anyZero ? 1 : 0) + mod) % mod;
    }

    public class Ex2034_StockPrice
    {
        private readonly Dictionary<int, int> prices = new();
        private int maxTimestamp = int.MinValue;
        private readonly PriorityQueue<int, int> maxes = new();
        private readonly PriorityQueue<int, int> mins = new();

        public Ex2034_StockPrice()
        {
        }

        // Update price for timestamp
        public void Update(int timestamp, int price)
        {
            prices[timestamp] = price;
            maxTimestamp = Math.Max(maxTimestamp, timestamp);
            maxes.Enqueue(timestamp, -price);
            mins.Enqueue(timestamp, price);
        }

        // Price at max timestamp
        public int Current() => prices[maxTimestamp];

        // Max price
        public int Maximum()
        {
            int price;
            while (maxes.TryPeek(out var timestamp, out price) && prices[timestamp] != -price)
                maxes.Dequeue();
            return -price;
        }

        // Min price
        public int Minimum()
        {
            int price;
            while (mins.TryPeek(out var timestamp, out price) && prices[timestamp] != price)
                mins.Dequeue();
            return price;
        }
    }

    public static int Ex2050_MinimumTime_ShortestPathViaTopoSort(int n, int[][] relations, int[] time)
    {
        // Vertices: 0 = start, 1..n = begin of courses, n+1..2n = end of courses
        var v = 2 * n + 1;
        var adjs = new ISet<int>[v];
        var ws = new Dictionary<(int, int), int> { };

        SetupDependencies();
        SetupTimes();

        var currentTopoSortValue = v - 1;
        var topoSort = new int[v];
        TopoSort(0, new HashSet<int> { });

        var bestEstimates = ShortestPathOnDag();
        var min = int.MaxValue;
        for (var i = n + 1; i <= 2 * n; i++)
            min = Math.Min(min, bestEstimates[i].Item1);
        return -min;

        // Setup dependencies: start node to all start courses, end of previous course -> start of next course
        void SetupDependencies()
        {
            adjs[0] = new HashSet<int> { };
            for (var i = 1; i <= n; i++)
            {
                adjs[0].Add(i);
                ws[(0, i)] = 0;
            }

            for (var i = 0; i < relations.Length; i++)
            {
                var relation = relations[i];
                if (adjs[relation[0] + n] != null)
                    adjs[relation[0] + n].Add(relation[1]);
                else
                    adjs[relation[0] + n] = new HashSet<int> { relation[1] };
                ws[(relation[0] + n, relation[1])] = 0;
            }
        }

        // Setup times: start of course -> end of course
        void SetupTimes()
        {
            for (var i = 1; i <= time.Length; i++)
            {
                if (adjs[i] != null)
                    adjs[i].Add(i + n);
                else
                    adjs[i] = new HashSet<int> { i + n };
                ws[(i, i + n)] = -time[i - 1];
            }
        }

        // Find topological sort of the DAG
        void TopoSort(int i, ISet<int> visited)
        {
            if (visited.Contains(i)) return;
            visited.Add(i);

            if (adjs[i] != null)
                foreach (var j in adjs[i])
                    TopoSort(j, visited);

            topoSort[currentTopoSortValue--] = i;
        }

        // Edge relaxation in toposort to find shortest distance
        IDictionary<int, (int, int)> ShortestPathOnDag()
        {
            var bestEstimates = new Dictionary<int, (int, int)>
            {
                [0] = (0, -1),
            };
            for (var i = 0; i < topoSort.Length; i++)
            {
                if (bestEstimates.TryGetValue(topoSort[i], out var d1) && adjs[topoSort[i]] != null)
                {
                    foreach (var j in adjs[topoSort[i]])
                    {
                        var edgeWeigth = ws[(topoSort[i], j)];
                        if (!bestEstimates.TryGetValue(j, out var d2) || d1.Item1 + edgeWeigth < d2.Item1)
                            bestEstimates[j] = (d1.Item1 + edgeWeigth, topoSort[i]);
                    }
                }
            }

            return bestEstimates;
        }
    }

    public static int Ex2050_MinimumTime_ShortestPathViaDfs(int n, int[][] relations, int[] time)
    {
        // Vertices: 0 = start, 1..n = begin of courses, n+1..2n = end of courses
        var v = 2 * n + 1;
        var adjs = new ISet<int>[v];
        var ws = new Dictionary<(int, int), int> { };

        SetupDependencies();
        SetupTimes();

        var longestPaths = new int[v];
        for (var i = 0; i < v; ++i)
            longestPaths[i] = -1;
        return Dfs(0, longestPaths);

        // Setup dependencies: end of previous course -> start of next course
        void SetupDependencies()
        {
            adjs[0] = new HashSet<int> { };
            for (var i = 1; i <= n; ++i)
            {
                adjs[0].Add(i);
                ws[(0, i)] = 0;
            }

            for (var i = 0; i < relations.Length; ++i)
            {
                var relation = relations[i];
                if (adjs[relation[0] + n] != null)
                    adjs[relation[0] + n].Add(relation[1]);
                else
                    adjs[relation[0] + n] = new HashSet<int> { relation[1] };
                ws[(relation[0] + n, relation[1])] = 0;
            }
        }

        // Setup times: start of course -> end of course
        void SetupTimes()
        {
            for (var i = 1; i <= time.Length; ++i)
            {
                if (adjs[i] != null)
                    adjs[i].Add(i + n);
                else
                    adjs[i] = new HashSet<int> { i + n };
                ws[(i, i + n)] = time[i - 1];
            }
        }

        int Dfs(int i, int[] longestPaths)
        {
            if (longestPaths[i] >= 0) return longestPaths[i];

            var max = 0;

            if (adjs[i] != null)
            {
                foreach (var j in adjs[i])
                    max = Math.Max(max, ws[(i, j)] + Dfs(j, longestPaths));
            }

            longestPaths[i] = max;
            return max;
        }
    }

    public static int Ex2050_MinimumTime_AllPairsLongestPathViaTopoSort(int n, int[][] relations, int[] time)
    {
        // Vertices: 0 = start, 1..n = begin of courses, n+1..2n = end of courses
        var v = 2 * n + 1;
        var adjs = new ISet<int>[v];
        var ws = new Dictionary<(int, int), int> { };

        SetupDependencies();
        SetupTimes();

        var currentTopoSortValue = v - 1;
        var topoSort = new int[v];
        TopoSort(0, new HashSet<int> { });

        var longestPaths = LongestPathOnDag();
        var max = int.MinValue;
        for (var i = n + 1; i <= 2 * n; i++)
            max = Math.Max(max, longestPaths[0, i]);
        return max;

        // Setup dependencies: start node to all start courses, end of previous course -> start of next course
        void SetupDependencies()
        {
            adjs[0] = new HashSet<int> { };
            for (var i = 1; i <= n; i++)
            {
                adjs[0].Add(i);
                ws[(0, i)] = 0;
            }

            for (var i = 0; i < relations.Length; i++)
            {
                var relation = relations[i];
                if (adjs[relation[0] + n] != null)
                    adjs[relation[0] + n].Add(relation[1]);
                else
                    adjs[relation[0] + n] = new HashSet<int> { relation[1] };
                ws[(relation[0] + n, relation[1])] = 0;
            }
        }

        // Setup times: start of course -> end of course
        void SetupTimes()
        {
            for (var i = 1; i <= time.Length; i++)
            {
                if (adjs[i] != null)
                    adjs[i].Add(i + n);
                else
                    adjs[i] = new HashSet<int> { i + n };
                ws[(i, i + n)] = time[i - 1];
            }
        }

        // Find topological sort of the DAG
        void TopoSort(int i, ISet<int> visited)
        {
            if (visited.Contains(i)) return;
            visited.Add(i);

            if (adjs[i] != null)
                foreach (var j in adjs[i])
                    TopoSort(j, visited);

            topoSort[currentTopoSortValue--] = i;
        }

        // Edge maximization in reverse toposort to find longest distance
        int[,] LongestPathOnDag()
        {
            var longestPaths = new int[v, v];
            for (var i = 0; i < v; i++)
                for (var j = 0; j < v; j++)
                    longestPaths[i, j] = i != j ? int.MaxValue : 0; // In a DAG every vertex reaches itself with 0 max distance

            for (var vertexSortId = v - 1; vertexSortId >= 0; vertexSortId--)
            {
                var vertex = topoSort[vertexSortId];

                if (adjs[vertex] == null) continue;

                foreach (var neighbor in adjs[vertex])
                {
                    for (var otherVertexSort = vertexSortId + 1; otherVertexSort < v; otherVertexSort++)
                    {
                        var otherVertex = topoSort[otherVertexSort];
                        if (longestPaths[neighbor, otherVertex] != int.MaxValue)
                            longestPaths[vertex, otherVertex] = Math.Max(
                                longestPaths[vertex, otherVertex] == int.MaxValue ? int.MinValue : longestPaths[vertex, otherVertex], 
                                ws[(vertex, neighbor)] + longestPaths[neighbor, otherVertex]);
                    }
                }
            }

            return longestPaths;
        }
    }

    public static string Ex2096_GetDirections_TwoDfs(TreeNode root, int startValue, int destValue)
    {
        var pathToStart = Dfs(root, startValue).ToArray();
        var pathToDest = Dfs(root, destValue).ToArray();

        // Calculate last node in common
        var i = 0;
        while (i < pathToStart.Length && i < pathToDest.Length && pathToStart[i] == pathToDest[i])
            i++;

        var path = new StringBuilder();
        path.Append(new string('U', pathToStart.Length - i));
        for (var j = i - 1; j < pathToDest.Length - 1; j++)
            if (pathToDest[j].left?.val == pathToDest[j + 1].val)
                path.Append('L');
            else
                path.Append('R');

        return path.ToString();

        IEnumerable<TreeNode> Dfs(TreeNode node, int targetValue)
        {
            if (node == null)
                yield break;

            if (node.val == targetValue)
            {
                yield return node;
                yield break;
            }

            var leftPath = Dfs(node.left, targetValue).ToList();
            if (leftPath.Any())
            {
                yield return node;
                foreach (var leftPathNode in leftPath)
                    yield return leftPathNode;
                yield break;
            }

            var rightPath = Dfs(node.right, targetValue).ToList();
            if (rightPath.Any())
            {
                yield return node;
                foreach (var rightPathNode in rightPath)
                    yield return rightPathNode;
            }
        }
    }

    public static ListNode Ex2095_DeleteMiddle(ListNode head)
    {
        if (head?.next == null) return null;

        var slow = head;
        var fast = head.next?.next;
        while (fast?.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        slow.next = slow.next.next;
        return head;
    }

    public static string Ex2096_GetDirections_SingleDfs(TreeNode root, int startValue, int destValue)
    {
        List<TreeNode> pathToStart = new List<TreeNode>(), pathToDest = new List<TreeNode>();
        Dfs(root, new List<TreeNode>());

        // Calculate last node in common
        var i = 0;
        while (i < pathToStart.Count && i < pathToDest.Count && pathToStart[i] == pathToDest[i])
            i++;

        var path = new StringBuilder();
        path.Append(new string('U', pathToStart.Count - i));
        for (var j = i - 1; j < pathToDest.Count - 1; j++)
            if (pathToDest[j].left?.val == pathToDest[j + 1].val)
                path.Append('L');
            else
                path.Append('R');

        return path.ToString();

        void Dfs(TreeNode node, List<TreeNode> currentPath)
        {
            if (node == null)
                return;

            currentPath.Add(node);
            if (node.val == startValue)
                pathToStart = currentPath.ToList();

            if (node.val == destValue)
                pathToDest = currentPath.ToList();

            Dfs(node.left, currentPath);
            Dfs(node.right, currentPath);
            currentPath.RemoveAt(currentPath.Count - 1);
        }
    }

    public static string Ex2096_GetDirections_WithStringBuffer(TreeNode root, int startValue, int destValue)
    {
        var pathToStart = Dfs(root, startValue, new StringBuilder());
        var pathToDest = Dfs(root, destValue, new StringBuilder());

        //Console.WriteLine($"{nameof(pathToStart)} = {pathToStart}, {nameof(pathToDest)} = {pathToDest}");

        // Calculate last node in common
        var i = 0;
        while (i < pathToStart.Length && i < pathToDest.Length && pathToStart[i] == pathToDest[i])
            i++;

        return new string('U', pathToStart.Length - i) + pathToDest[i..];

        string Dfs(TreeNode node, int targetValue, StringBuilder currentPath)
        {
            if (node == null)
                return null;

            if (node.val == targetValue)
                return currentPath.ToString();

            currentPath.Append("L");
            var leftPath = Dfs(node.left, targetValue, currentPath);
            if (leftPath != null) return leftPath;

            currentPath.Remove(currentPath.Length - 1, 1);

            currentPath.Append("R");
            var rightPath = Dfs(node.right, targetValue, currentPath);
            if (rightPath != null) return rightPath;

            currentPath.Remove(currentPath.Length - 1, 1);

            return null;
        }
    }

    public static IList<string> Ex2115_FindAllRecipes(
        string[] recipes, IList<IList<string>> ingredients, string[] supplies)
    {
        var (adjs, reversedAdjs) = BuildAdjs();
        var topoSort = TopoSort();

        var available = supplies.ToHashSet();
        var result = new List<string>();
        for (var i = 0; i < topoSort.Length; i++)
        {
            var recipe = topoSort[i];
            if (reversedAdjs.ContainsKey(recipe) && reversedAdjs[recipe].Count > 0 && reversedAdjs[recipe].IsSubsetOf(available))
            {
                available.Add(recipe);
                result.Add(recipe);
            }
        }

        return result;

        (IDictionary<string, ISet<string>>, IDictionary<string, ISet<string>>) BuildAdjs()
        {
            var adjs = new Dictionary<string, ISet<string>>();
            var reversedAdjs = new Dictionary<string, ISet<string>>();

            for (var i = 0; i < supplies.Length; i++)
            {
                adjs[supplies[i]] = new HashSet<string>();
                reversedAdjs[supplies[i]] = new HashSet<string>();
            }

            for (var i = 0; i < recipes.Length; i++)
            {
                var recipe = recipes[i];
                if (!adjs.TryGetValue(recipe, out var adj1))
                {
                    adj1 = new HashSet<string>();
                    adjs[recipe] = adj1;
                }

                if (!reversedAdjs.TryGetValue(recipe, out var reversedAdj1))
                {
                    reversedAdj1 = new HashSet<string>();
                    reversedAdjs[recipe] = reversedAdj1;
                }

                foreach (var ingredient in ingredients[i])
                {
                    if (!adjs.TryGetValue(ingredient, out var adj))
                    {
                        adj = new HashSet<string>();
                        adjs[ingredient] = adj;
                    }
                    adj.Add(recipe);

                    if (!reversedAdjs.TryGetValue(recipe, out var reversedAdj))
                    {
                        reversedAdj = new HashSet<string>();
                        reversedAdjs[recipe] = reversedAdj;
                    }
                    reversedAdj.Add(ingredient);
                }
            }
            return (adjs, reversedAdjs);
        }

        string[] TopoSort()
        {
            var topoSort = new string[adjs.Count];
            var currentTopoSort = adjs.Count - 1;
            var visited = new HashSet<string>();

            foreach (var vertex in adjs.Keys)
                if (!visited.Contains(vertex))
                    Dfs(vertex);

            return topoSort;

            void Dfs(string vertex)
            {
                if (visited.Contains(vertex)) return;

                visited.Add(vertex);
                if (adjs.TryGetValue(vertex, out var adj))
                    foreach (var neighbor in adj)
                        Dfs(neighbor);

                topoSort[currentTopoSort--] = vertex;
            }
        }
    }

    public static bool Ex2128_RemoveOnes(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;

        var swappedRows = new bool?[m];
        var swappedCols = new bool?[n];

        for (var i = 0; i < m; i++)
        {
            var rowToSwap = grid[i][0] != 0;
            if (swappedRows[i] == !rowToSwap) return false;
            swappedRows[i] = rowToSwap;

            for (var j = 0; j < n; j++)
            {
                var colToSwap = rowToSwap ? (grid[i][j] == 0) : (grid[i][j] != 0);
                if (swappedCols[j] == !colToSwap) return false;
                swappedCols[j] = colToSwap;
            }
        }

        return true;
    }

    public static int[] Ex2158_AmountPainted_SortedSet(int[][] paint)
    {
        var results = new int[paint.Length];
        var events = new SortedSet<(int, int)>();

        for (var i = 0; i < paint.Length; i++)
        {
            var overlappingEvents = events.GetViewBetween((paint[i][0], -1), (paint[i][1], +1)).ToList();
            //Console.WriteLine($"{i}: Events: {string.Join(" ", overlappingEvents)}");

            if (overlappingEvents.Count == 0 &&
                events.GetViewBetween((paint[i][1], +1), (int.MaxValue, +1)).FirstOrDefault((-1, +1)).Item2 < 0)
                continue;

            var addStart = overlappingEvents.Count == 0 || overlappingEvents[0].Item2 > 0;
            var addEnd = overlappingEvents.Count == 0 || overlappingEvents[^1].Item2 < 0;

            results[i] = paint[i][1] - paint[i][0];
            var current = !addStart ? paint[i][0] : int.MinValue;
            foreach (var overlappingEvent in overlappingEvents)
            {
                if (overlappingEvent.Item2 < 0)
                {
                    results[i] -= overlappingEvent.Item1 - current;
                    current = int.MinValue;
                }
                else
                {
                    current = overlappingEvent.Item1;
                }

                events.Remove(overlappingEvent);
            }

            if (current != int.MinValue)
                results[i] -= paint[i][1] - current;

            if (addStart) events.Add((paint[i][0], +1));
            if (addEnd) events.Add((paint[i][1], -1));
        }

        return results;
    }

    public static int[] Ex2158_AmountPainted_JumpArray(int[][] paint)
    {
        int n = paint.Length, m = 100001;
        var wall = new int[m];
        var result = new int[n];
        for (var i = 0; i < paint.Length; i++)
        {
            var j = paint[i][0];
            var end = paint[i][1];
            var painted = 0;
            while (j < end)
            {
                if (wall[j] > 0)
                {
                    (j, wall[j]) = (wall[j], end);
                }
                else
                {
                    painted++;
                    (j, wall[j]) = (j + 1, end);
                }
            }
            result[i] = painted;
        }

        return result;
    }

    public static long Ex2171_MinimumRemoval(int[] beans)
    {
        var n = beans.Length;
        Array.Sort(beans);

        var prefixSum = CalculatePrefixSum();
        var minBeans = long.MaxValue;

        var i = 0;
        while (i < n)
        {
            var valuesStrictlyLess = prefixSum[i];
            var valuesBiggerOrEqual = (prefixSum[n] - prefixSum[i] - (long)(n - i) * beans[i]);

            minBeans = Math.Min(minBeans, valuesStrictlyLess + valuesBiggerOrEqual);

            i++;
            while (i < n && beans[i] == beans[i - 1])
                i++;
        }

        return minBeans;

        long[] CalculatePrefixSum()
        {
            var prefixSum = new long[n + 1];
            prefixSum[0] = 0;
            for (var i = 0; i < n; i++)
            {
                prefixSum[i + 1] = prefixSum[i] + beans[i];
            }
            return prefixSum;
        }
    }

    public static long Ex2171_MinimumRemoval_Optimized(int[] beans)
    {
        var n = beans.Length;
        Array.Sort(beans);

        var sum = 0L;
        for (var j = 0; j < n; j++) sum += beans[j];

        var minBeans = long.MaxValue;

        var i = 0;
        while (i < n)
        {
            minBeans = Math.Min(minBeans, sum - (long)(n - i) * beans[i]);

            i++;
            while (i < n && beans[i] == beans[i - 1])
                i++;
        }

        return minBeans;
    }

    public static int Ex2172_MaximumANDSum_DP(int[] nums, int numSlots)
    {
        var n = nums.Length;

        var solutions = new Dictionary<(int, long), int>();
        return Choice(0, 0L);

        int Choice(int j, long slots)
        {
            if (j == n) return 0;
            if (solutions.TryGetValue((j, slots), out var solution)) return solution;

            solution = int.MinValue;
            for (var i = 0; i < numSlots; i++)
            {
                var shiftCount = i * 2;
                var slotCount = (slots >> shiftCount) & 0b11; // 00, 01, 10
                if (slotCount == 2) continue;

                var higherPart = (slots >> (shiftCount + 2)) << (shiftCount + 2);
                var middlePart = (slotCount + 1) << shiftCount;
                var lowerPart = slots & ((1 << shiftCount) - 1);
                var newSlots = higherPart | middlePart | lowerPart;

                //Console.WriteLine($"j = {j}, slots = {Convert.ToString(slots, 2)}, i = {i}, newSlots = {Convert.ToString(newSlots, 2)}");

                solution = Math.Max(solution, (nums[j] & (i + 1)) + Choice(j + 1, newSlots));
            }

            //Console.WriteLine($"j = {j}, slots = {Convert.ToString(slots, 2)}, maxValue = {maxValue}");
            solutions[(j, slots)] = solution;
            return solution;
        }
    }

    public static int Ex2172_MaximumANDSum_DPSpaceOptimized(int[] nums, int numSlots)
    {
        var n = nums.Length;

        var solutions = new Dictionary<long, int>();
        return Choice(0, 0L);

        int Choice(int j, long slots)
        {
            if (j == n) return 0;
            if (solutions.TryGetValue((j << 20) + slots, out var solution)) return solution;

            solution = int.MinValue;
            for (var i = 0; i < numSlots; i++)
            {
                var shiftCount = i * 2;
                var slotCount = (slots >> shiftCount) & 0b11; // 00, 01, 10
                if (slotCount == 2) continue;

                var higherPart = (slots >> (shiftCount + 2)) << (shiftCount + 2);
                var middlePart = (slotCount + 1) << shiftCount;
                var lowerPart = slots & ((1 << shiftCount) - 1);
                var newSlots = higherPart | middlePart | lowerPart;

                //Console.WriteLine($"j = {j}, slots = {Convert.ToString(slots, 2)}, i = {i}, newSlots = {Convert.ToString(newSlots, 2)}");
                solution = Math.Max(solution, (nums[j] & (i + 1)) + Choice(j + 1, newSlots));
            }

            //Console.WriteLine($"j = {j}, slots = {Convert.ToString(slots, 2)}, maxValue = {maxValue}");
            solutions[(j << 20) + slots] = solution;
            return solution;
        }
    }

    public static int Ex2281_TotalStrength(int[] strength)
    {
        var n = strength.Length;
        var mod = 1_000_000_007;

        var result = 0L;
        var (leftIndices, rightIndices) = CalculateSideIndices();
        var (sumLeftPrefixes, sumRightPrefixes) = CalculateSumPrefixes();
        var (multLeftPrefixes, multRightPrefixes) = CalculateMultPrefixes();

        for (var i = 0; i < n; i++)
        {
            // All sub-arrays of [leftIndices[i]..rightIndices[i]] have min = strength[i].
            // So I can factor out min and focus on sum of all sub-arrays in the range containing item at index i.
            var leftIndex = leftIndices[i];
            var leftSize = i - leftIndex + 1;
            var rightIndex = rightIndices[i];
            var rightSize = rightIndex - i + 1;

            var sumOfLeftSubArrays = CalculateSumOfSubArraysLeft(leftIndex, i) % mod;
            var sumofRightSubArrays = CalculateSumOfSubArraysRight(i, rightIndex) % mod;
            var sumOfSubArrays = (sumOfLeftSubArrays * rightSize + sumofRightSubArrays * leftSize - (long)leftSize * rightSize * strength[i]) % mod;

            //Console.WriteLine($"i = {i}, leftIndex = {leftIndex}, leftSize = {leftSize}, rightIndex = {rightIndex}, rightSize = {rightSize}");
            //Console.WriteLine($"sumOfLeftSubArrays = {sumOfLeftSubArrays}, sumofRightSubArrays = {sumofRightSubArrays}, sumOfSubArrays = {sumOfSubArrays}");

            result = (result + strength[i] * sumOfSubArrays) % mod;
        }

        return (int)result;

        long CalculateSumOfSubArraysLeft(int s, int e)
        {
            var result = multLeftPrefixes[e + 1] - multLeftPrefixes[s]
                - s * (sumLeftPrefixes[e + 1] - sumLeftPrefixes[s]);

            //Console.WriteLine($"CalculateSumOfSubArraysLeft {s} {e} = {result}");
            return result;
        }

        long CalculateSumOfSubArraysRight(int s, int e)
        {
            var result = multRightPrefixes[s] - multRightPrefixes[e + 1]
                - (n - 1 - e) * (sumRightPrefixes[s] - sumRightPrefixes[e + 1]);

            //Console.WriteLine($"CalculateSumOfSubArraysRight {s} {e} = {result}");
            return result;
        }

        (int[], int[]) CalculateSideIndices()
        {
            var leftIndices = new int[n];
            var leftStack = new List<int>();
            var rightIndices = new int[n];
            var rightStack = new List<int>();

            for (var i = 0; i < n; i++)
            {
                while (leftStack.Count > 0 && strength[leftStack[^1]] > strength[i])
                    leftStack.RemoveAt(leftStack.Count - 1);
                leftIndices[i] = leftStack.Count > 0 ? leftStack[^1] + 1 : 0;
                leftStack.Add(i);

                while (rightStack.Count > 0 && strength[rightStack[^1]] >= strength[n - 1 - i])
                    rightStack.RemoveAt(rightStack.Count - 1);
                rightIndices[n - 1 - i] = rightStack.Count > 0 ? rightStack[^1] - 1 : n - 1;
                rightStack.Add(n - 1 - i);
            }
            //Console.WriteLine($"leftIndices = [{string.Join(" ", leftIndices)}]");
            //Console.WriteLine($"rightIndices = [{string.Join(" ", rightIndices)}]");

            return (leftIndices, rightIndices);
        }

        (long[], long[]) CalculateSumPrefixes()
        {
            var leftSumPrefixes = new long[n + 1];
            var rightSumPrefixes = new long[n + 1];
            leftSumPrefixes[0] = 0;
            rightSumPrefixes[^1] = 0;
            for (var i = 1; i <= n; i++)
            {
                leftSumPrefixes[i] = leftSumPrefixes[i - 1] + strength[i - 1];
                rightSumPrefixes[n - i] = rightSumPrefixes[n - i + 1] + strength[n - i];
            }
            //Console.WriteLine($"leftSumPrefixes = [{string.Join(" ", leftSumPrefixes)}]");
            //Console.WriteLine($"rightSumPrefixes = [{string.Join(" ", rightSumPrefixes)}]");

            return (leftSumPrefixes, rightSumPrefixes);
        }

        (long[], long[]) CalculateMultPrefixes()
        {
            var leftMultPrefixes = new long[n + 1];
            var rightMultPrefixes = new long[n + 1];
            leftMultPrefixes[0] = 0;
            rightMultPrefixes[^1] = 0;
            for (var i = 1; i <= n; i++)
            {
                leftMultPrefixes[i] = leftMultPrefixes[i - 1] + (long)i * strength[i - 1];
                rightMultPrefixes[n - i] = rightMultPrefixes[n - i + 1] + (long)i * strength[n - i];
            }
            //Console.WriteLine($"leftMultPrefixes = [{string.Join(" ", leftMultPrefixes)}]");
            //Console.WriteLine($"rightMultPrefixes = [{string.Join(" ", rightMultPrefixes)}]");

            return (leftMultPrefixes, rightMultPrefixes);
        }
    }

    public static int Ex2359_ClosestMeetingNode_TwoSimplifiedBfs(int[] edges, int node1, int node2)
    {
        var node1Distances = Bfs(node1);
        var node2Distances = Bfs(node2);
        var reachableNodes = new HashSet<int>(node1Distances.Keys).Intersect(node2Distances.Keys);

        var meetingNode = -1;
        var meetingNodeMaxDistance = int.MaxValue;

        foreach (var n in reachableNodes)
        {
            var d = Math.Max(node1Distances[n], node2Distances[n]);
            if (d < meetingNodeMaxDistance || (d == meetingNodeMaxDistance && n < meetingNode))
            {
                meetingNode = n;
                meetingNodeMaxDistance = d;
            }
        }
        return meetingNode;

        IDictionary<int, int> Bfs(int v)
        {
            var visited = new HashSet<int> { v };
            var distances = new Dictionary<int, int> { [v] = 0 };
            var u = edges[v];
            var d = 1;
            while (u >= 0 && !visited.Contains(u))
            {
                visited.Add(u);
                distances[u] = d++;
                u = edges[u];
            }

            return distances;
        }
    }

    public static int Ex2359_ClosestMeetingNode_OptimizedWalking(int[] edges, int node1, int node2)
    {
        var distances = new int[edges.Length];
        for (var i = 0; i < distances.Length; i++)
            distances[i] = int.MaxValue;

        var visited = new HashSet<int> { };
        var u = node1;
        var d = 0;
        while (u >= 0 && !visited.Contains(u))
        {
            visited.Add(u);
            distances[u] = d++;
            u = edges[u];
        }

        Console.WriteLine(string.Join(" ", distances.Select(d => d.ToString())));

        var meetingNode = -1;
        var meetingNodeMaxDistance = int.MaxValue;

        visited.Clear();
        u = node2;
        d = 0;
        while (u >= 0 && !visited.Contains(u))
        {
            visited.Add(u);
            distances[u] = Math.Max(distances[u], d++);

            if (distances[u] < meetingNodeMaxDistance || (distances[u] == meetingNodeMaxDistance && u < meetingNode))
            {
                meetingNode = u;
                meetingNodeMaxDistance = distances[u];
            }

            u = edges[u];
        }

        return meetingNode;
    }

    public static int Ex2374_EdgeScore(int[] edges)
    {
        var scores = new long[edges.Length];
        for (var i = 0; i < edges.Length; i++)
            scores[edges[i]] += i;

        long maxScore = long.MinValue;
        var maxScoreNode = -1;
        for (var i = 0; i < scores.Length; i++)
            if (scores[i] > maxScore)
            {
                maxScore = scores[i];
                maxScoreNode = i;
            }

        return maxScoreNode;
    }

    public static int Ex2387_MatrixMedian(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        var mid = m * n / 2 + 1;

        var min = int.MaxValue;
        var max = int.MinValue;
        for (var i = 0; i < m; i++)
        {
            min = Math.Min(min, grid[i][0]);
            max = Math.Max(max, grid[i][n - 1]);
        }

        int minResult = min;
        int maxResult = max;

        while (minResult <= maxResult)
        {
            var midResult = minResult + (maxResult - minResult) / 2;
            var midCount = CountInMatrix(midResult);

            if (midCount < mid)
                minResult = midResult + 1;
            else
                maxResult = midResult - 1;
        }

        return minResult;

        // # values in matrix <= v
        int CountInMatrix(int v)
        {
            var count = 0;
            for (var i = 0; i < m; i++)
                count += CountInRow(grid[i], v);
            return count;
        }

        // # values in row <= v
        int CountInRow(int[] row, int v)
        {
            var left = 0;
            var right = row.Length - 1;
            while (left <= right)
            {
                var middle = left + (right - left) / 2;
                if (row[middle] <= v)
                    left = middle + 1;
                else
                    right = middle - 1;
            }

            return left;
        }
    }
}
