using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI;

public static class Knapsack
{
    public static int Knapsack01(int[] ws, int[] vs, int W)
    {
        var solutions = new Dictionary<(int, int), int> { };
        return Knapsack01(0, W);

        int Knapsack01(int i, int rw)
        {
            if (i >= ws.Length) return 0;
            if (solutions.TryGetValue((i, rw), out var solution)) return solution;

            solution = 0;
            if (ws[i] <= rw)
                solution = Math.Max(solution, Knapsack01(i + 1, rw - ws[i]) + vs[i]);
            solution = Math.Max(solution, Knapsack01(i + 1, rw));

            solutions[(i, rw)] = solution;
            return solution;
        }
    }
}
