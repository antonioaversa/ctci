using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI;

public static class DynamicProgramming
{
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
}
