using System.Data;
using System.Text;

namespace CTCI;

public static class Leetcode
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

    public static TreeNode Ex226_InvertTree(TreeNode root)
    {
        if (root == null) return null;

        var left = Ex226_InvertTree(root.left);
        var right = Ex226_InvertTree(root.right);
        return new TreeNode(root.val, right, left);
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
}
