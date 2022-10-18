using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI;

public static class Exercises6
{
    public static int Ex6_SuperEggDrop_DP(int k, int n)
    {
        var solutions = new Dictionary<(int, int), int> { };
        return SuperEggDrop(k, n, solutions);

        static int SuperEggDrop(int k, int n, IDictionary<(int, int), int> solutions)
        {
            if (n <= 0) return 0;
            if (k == 0) return int.MaxValue;
            if (k == 1) return n;
            if (solutions.TryGetValue((k, n), out var solution)) return solution;

            var attempts = int.MaxValue;
            for (var i = 1; i <= n; i++)
            {
                var c1 = SuperEggDrop(k - 1, i - 1, solutions); // The egg breaks
                if (c1 + 1 >= attempts)
                    continue;

                var c2 = SuperEggDrop(k, n - i, solutions); // The egg doesn't break
                if (c1 > c2)
                    c2 = c1;

                if (c2 != int.MaxValue)
                    attempts = Math.Min(attempts, c2 + 1);
            }

            solutions[(k, n)] = attempts;
            return attempts;
        }
    }

    public static int Ex6_SuperEggDrop_Binomial(int eggs, int floors)
    {
        if (floors < 0 || eggs < 0) throw new ArgumentException($"Invalid {nameof(eggs)} and/or {nameof(floors)}");

        if (floors == 0) return 0;
        if (eggs == 0) return 0;

        // find smallest d such that #floors(k eggs, d drops) >= n
        // #floors(k eggs, d drops) = sum(k = 1 to k, d over k)

        static long BinomialCoefficient(long d, long k)
        {
            long result = 1;
            for (var i = 1; i <= k; i++)
            {
                result *= d--;
                result /= i;
            }
            return result;
        }

        static long Floors(long drops, long eggs)
        {
            long result = 0;
            for (var i = 1; i <= eggs; i++)
                result += BinomialCoefficient(drops, i);
            return result;
        }

        for (var drops = 1; true; drops++)
            if (Floors(drops, eggs) >= floors)
                return drops;

        /*
        // TODO: fix OVERFLOW ISSUES with the following
        int s = 1, e = floors;
        while (s <= e)
        {
            var m = s + (e - s) / 2;
            if (Floors(m, eggs) >= floors)
                e = m - 1;
            else
                s = m + 1;
        }

        return Floors(s, eggs) >= floors ? s : s + 1;
        */
    }
}
