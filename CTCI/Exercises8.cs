using System.Collections.Generic;
using System.Globalization;

namespace CTCI;

public class Exercises8
{
    public static int Ex1_Fibonacci(int n) => n <= 1 ? 1 : Ex1_Fibonacci(n - 1) + Ex1_Fibonacci(n - 2);

    public static int Ex1_FibonacciIterative(int n)
    {
        if (n <= 1) return 1;

        var last = 1;
        var secondLast = 1;
        for (var i = 2; i <= n; i++)
        {
            (last, secondLast) = (last + secondLast, last);
        }

        return last;
    }

    public static int Ex2_Count(int n)
    {
        return Paths(n, n);

        static int Paths(int a, int b)
        {
            if (a == 1 && b == 1) return 1;
            return (b > 1 ? Paths(a, b - 1) : 0) + (a > 1 ? Paths(a - 1, b) : 0);
        }
    }

    public static int Ex2_CountWithOffLimits(int n, Func<int, int, bool> offLimits)
    {
        return Paths(n, n);

        int Paths(int a, int b)
        {
            if (a == 1 && b == 1)
                return !offLimits(n + 1 - a, n + 1 - b) ? 1 : 0;

            return
                (b > 1 && !offLimits(n + 1 - a, n + 1 - b + 1) ? Paths(a, b - 1) : 0) +
                (a > 1 && !offLimits(n + 1 - a + 1, n + 1 - b) ? Paths(a - 1, b) : 0);
        }
    }

    public static IEnumerable<IList<(int, int)>> Ex2_PathsWithOffLimits(int n, Func<int, int, bool> offLimits)
    {
        return Paths(n, n);

        IEnumerable<IList<(int, int)>> Paths(int a, int b)
        {
            var ia = n + 1 - a;
            var ib = n + 1 - b;
            if (a == 1 && b == 1)
            {
                if (!offLimits(ia, ib))
                    yield return new List<(int, int)> { (ia, ib) };
                else 
                    yield break;
            }

            var pathsGoingRight = b > 1 && !offLimits(ia, ib + 1) 
                ? Paths(a, b - 1).Select(p => p.Prepend((ia, ib)).ToList())
                : Enumerable.Empty<IList<(int, int)>>();

            var pathsGoingDown = a > 1 && !offLimits(ia + 1, ib)
                ? Paths(a - 1, b).Select(p => p.Prepend((ia, ib)).ToList())
                : Enumerable.Empty<IList<(int, int)>>();

            foreach (var path in pathsGoingRight.Concat(pathsGoingDown))
                yield return path;
        }
    }

    public static IEnumerable<ISet<T>> Ex3_Subsets<T>(ISet<T> set)
    {
        return Subsets(set.ToList(), 0);

        static IEnumerable<ISet<T>> Subsets(IList<T> list, int fromIndex)
        {
            if (fromIndex == list.Count)
            {
                yield return new HashSet<T> { };
                yield break;
            }

            foreach (var subset in Subsets(list, fromIndex + 1))
            {
                yield return subset;
                yield return subset.Append(list[fromIndex]).ToHashSet();
            }
        }
    }

    public static IEnumerable<ISet<T>> Ex3_SubsetsIterative<T>(ISet<T> set)
    {
        var list = set.ToList();

        for (var i = 0L; i < 1L << list.Count; i++)
        {
            var subset = new HashSet<T> { };

            var j = 0;
            var k = i;
            while (k != 0)
            {
                if ((k & 1) == 1)
                    subset.Add(list[j]);
                j++;
                k >>= 1;
            }

            yield return subset;
        }
    }

    public static IEnumerable<string> Ex4_AllPermutations(string s)
    {
        return AllPermutations(s, 0);
        
        static IEnumerable<string> AllPermutations(string s, int fromIndex)
        {
            if (fromIndex >= s.Length)
            {
                yield return string.Empty;
                yield break;
            }

            foreach (var permutation in AllPermutations(s, fromIndex + 1))
            {
                for (var i = 0; i < s.Length - fromIndex; i++)
                    yield return permutation[..i] + s[fromIndex] + permutation[i..];
            }
        }
    }

    public static IEnumerable<string> Ex5_AllCombinationsOfParenthesesWithTwoStacks(int n)
    {
        return AllCombinationsOfParentheses(n, Enumerable.Empty<char>(), Enumerable.Empty<char>()).Distinct();

        static IEnumerable<string> AllCombinationsOfParentheses(int n, IEnumerable<char> s1, IEnumerable<char> s2)
        {
            if (n == 0)
            {
                yield return new string(s1.Concat(s2.Reverse()).ToArray());
                yield break;
            }

            var combinations = AllCombinationsOfParentheses(n - 1, s1.Append('('), s2.Append(')'));

            combinations = combinations.Concat(AllCombinationsOfParentheses(n - 1, s1.Append('(').Append(')'), s2));
            combinations = combinations.Concat(AllCombinationsOfParentheses(n - 1, s1, s2.Append(')').Append('(')));

            foreach (var combination in combinations)
                yield return combination;
        }
    }

    public static IEnumerable<string> Ex5_AllCombinationsOfParenthesesConstructive(int n)
    {
        return AllCombinationsOfParentheses(n).Distinct();

        static IEnumerable<string> AllCombinationsOfParentheses(int n)
        {
            if (n == 0)
            {
                yield return string.Empty;
                yield break;
            }

            foreach (var combination in AllCombinationsOfParentheses(n - 1))
            {
                yield return combination + "()";
                yield return "(" + combination + ")";
                yield return "()" + combination;
            }
        }
    }

    public static IEnumerable<string> Ex5_AllCombinationsOfParenthesesChoices(int n)
    {
        return AllCombinationsOfParenthesesChoices(n, 0, 0);

        static IEnumerable<string> AllCombinationsOfParenthesesChoices(int n, int opened, int closed)
        {
            if (n == closed)
            {
                yield return string.Empty;
                yield break;
            }
            
            if (opened < n)
                foreach (var s in AllCombinationsOfParenthesesChoices(n, opened + 1, closed))
                    yield return "(" + s;

            if (closed < opened)
                foreach (var s in AllCombinationsOfParenthesesChoices(n, opened, closed + 1))
                    yield return ")" + s;
        }
    }

    public static void Ex6_Paint(char[,] img, int i, int j, char newColor)
    {
        var X = img.GetLength(0);
        var Y = img.GetLength(1);
        var oldColor = img[i, j];

        Paint(i, j);

        void Paint(int i, int j)
        {
            if (i >= X || j >= Y || i < 0 || j < 0)
                return;

            if (img[i, j] != oldColor)
                return;

            img[i, j] = newColor;
            Paint(i + 1, j);
            Paint(i - 1, j);
            Paint(i, j + 1);
            Paint(i, j - 1);
        }


        // Paint(i, j, 0, X - 1, 0, Y - 1);

        //void Paint(int i, int j, int min_i, int max_i, int min_j, int max_j)
        //{
        //    if (i > max_i || j >= max_j || i < min_i || j < min_j )
        //        return;
        //    if (img[i, j] != oldColor)
        //        return;

        //    img[i, j] = newColor;
        //    Paint(i + 1, j, i + 1, max_i, min_j, max_j);
        //    Paint(i - 1, j, min_i, i - 1, min_j, max_j);
        //    Paint(i, j + 1, min_i, max_i, j + 1, max_j);
        //    Paint(i, j - 1, min_i, max_j, min_j, j + 1);
        //}
    }

    public static int Ex7_NCents(int n)
    {
        var tokens = new int[] { 25, 10, 5, 1 };
        return NCents(n, 0);

        int NCents(int n, int maxTokenIndex)
        {
            if (n == 0)
                return 1;
            if (maxTokenIndex >= tokens.Length)
                return -1;

            var result = 0;
            for (var i = 0; i <= n / tokens[maxTokenIndex]; i++)
            {
                var ways = NCents(n - i * tokens[maxTokenIndex], maxTokenIndex + 1);
                if (ways < 0)
                    continue;
                result += ways;
            }
            return result;
        }
    }

    public static IEnumerable<IList<(int, int)>> Ex8_NQueens(int n)
    {
        return NQueens(n, new List<(int, int)> { });

        static IEnumerable<IList<(int, int)>> NQueens(int n, IList<(int, int)> placedQueens)
        {
            if (placedQueens.Count == n)
            {
                yield return placedQueens;
                yield break;
            }

            var i = placedQueens.Count;
            for (var j = 0; j < n; j++)
            {
                if (AreCompatible(placedQueens, i, j))
                {
                    var placedQueensWithOneMore = placedQueens.Append((i, j)).ToList();
                    foreach (var way in NQueens(n, placedQueensWithOneMore))
                        yield return way;
                }
            }
        }

        static bool AreCompatible(IList<(int, int)> placedQueens, int i, int j)
        {
            return placedQueens.All(
                c => c.Item1 != i && c.Item2 != j && c.Item1 - c.Item2 != i - j && c.Item1 + c.Item2 != i + j);
        }
    }
}
