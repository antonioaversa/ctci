using System;
using System.Collections.Generic;
using System.Globalization;
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
        if (n == 1) return new List<int>();

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
}
