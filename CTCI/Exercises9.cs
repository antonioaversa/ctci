using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CTCI;

public static class Exercises9
{
    public static void Ex1_Merge(int[] a, int[] b, int aCount)
    {
        if (a.Length < aCount + b.Length) 
            throw new Exception($"Not enough buffer to merge {nameof(b)} into {nameof(a)}.");

        var k = aCount + b.Length - 1;
        var i = aCount - 1;
        var j = b.Length - 1;

        while (j >= 0)
        {
            if (i < 0 || a[i] < b[j])
            {
                a[k] = b[j];
                j--;
            }
            else
            {
                a[k] = a[i];
                i--;
            }

            k--;
        }
    }

    public static void Ex2_SortAnagrams(string[] a)
    {
        var b = new Dictionary<string, string> { };
        for (var i = 0; i < a.Length; i++)
        {
            var charArray = a[i].ToCharArray();
            MergeSort(charArray, c => c, new char[a[i].Length], 0, a[i].Length - 1);
            b[a[i]] = new string(charArray);
        }

        MergeSort(a, s => b[s], new string[a.Length], 0, a.Length - 1);
    }

    private static void MergeSort<T>(T[] a, Func<T, T> b, T[] t, int s, int e)
        where T : IComparable<T>
    {
        if (s >= e) return;

        var m = s + (e - s) / 2;
        int i = s, j = m + 1, k = s;

        MergeSort(a, b, t, s, m);
        MergeSort(a, b, t, m + 1, e);

        while (k <= e)
        {
            if (i > m) 
                t[k++] = a[j++];
            else if (j > e) 
                t[k++] = a[i++];
            else if (b(a[i]).CompareTo(b(a[j])) <= 0) 
                t[k++] = a[i++];
            else 
                t[k++] = a[j++];
        }

        for (i = s; i <= e; i++)
            a[i] = t[i];
    }

    public static int Ex3_Find(int[] a, int v)
    {
        return Find(0, a.Length - 1);

        int Find(int s, int e)
        {
            if (s > e)
                return -1;
            if (s == e)
                return a[s] == v ? s : -1;

            var m = s + (e - s) / 2;

            if (a[m] == v)
                return m;

            if (a[m] > v)
            {
                if (a[e] >= v)
                    return Find(s, m - 1);
                return Find(m + 1, e);
            }

            if (a[e] >= v)
                return Find(m + 1, e);
            return Find(s, m - 1);
        }
    }

    public static int Ex5_Find_WithInterspersed(string[] a, string v)
    {
        var p = new int[a.Length];
        var n = new int[a.Length];

        int i, j;

        j = -1;
        for (i = 0; i < a.Length; i++)
        {
            if (a[i] != "")
                j = i;
            p[i] = j;
        }
        j = -1;
        for (i = a.Length - 1; i >= 0 ; i--)
        {
            if (a[i] != "")
                j = i;
            n[i] = j;
        }

        i = n[0]; j = p[a.Length - 1];
        while (i >= 0 && j >= 0 && i <= j)
        {
            var m = p[i + (j - i) / 2];

            if (m < i)
                return -1;

            if (a[m] == v)
                return m;

            if (a[m].CompareTo(v) < 0)
                i = n[m + 1];
            else
                j = p[m - 1];
        }

        return -1;
    }

    public static int Ex5_Find_WithoutAuxArrays(string[] a, string v)
    {
        int i = 0, j = a.Length - 1;

        while (i <= j)
        {
            if (i == j) 
                return a[i] == v ? i : -1;

            var m = i + (j - i) / 2;
            while (m >= i && a[m] == "") m--;

            if (m < i)
            {
                i = i + (j - i) / 2 + 1;
            }
            else
            {
                var comparison = a[m].CompareTo(v);
                if (comparison == 0)
                    return m;
                if (comparison > 0)
                    j = m - 1;
                else
                    i = m + 1;
            }
        }

        return -1;
    }

    public static (int, int) Ex6_Find_InSortedMatrix(int[,] x, int v)
    {
        int r = x.GetLength(0), c = x.GetLength(1);
        if (r == 0 || c == 0) return (-1, -1);

        return BinarySearch2D(x, 0, r - 1, 0, c - 1, v);

        static (int, int) BinarySearch2D(int[,] x, int i_s, int i_e, int j_s, int j_e, int v)
        {
            if (!(i_s <= i_e && j_s <= j_e)) return (-1, -1);

            int j_m = BinarySearchInRow(x, i_s, j_s, j_e, v);
            if (x[i_s, j_m] == v)
                return (i_s, j_m);

            int i_m = BinarySearchInColumn(x, j_m, i_s, i_e, v);
            if (x[i_m, j_m] == v)
                return (i_m, j_m);

            var result = BinarySearch2D(x, i_s, i_m - 1, j_m + 1, j_e, v);
            if (result != (-1, -1))
                return result;

            return BinarySearch2D(x, i_m + 1, i_e, j_s, j_m - 1, v);
        }

        static int BinarySearchInRow(int[,] x, int r, int j_s, int j_e, int v)
        {
            while (j_s <= j_e)
            {
                var jm = j_s + (j_e - j_s) / 2;
                if (x[r, jm] == v) 
                    return jm;
                if (x[r, jm] > v) 
                    j_e = jm - 1;
                else 
                    j_s = jm + 1;
            }

            return j_e;
        }

        static int BinarySearchInColumn(int[,] x, int c, int i_s, int i_e, int v)
        {
            while (i_s <= i_e)
            {
                var i_m = i_s + (i_e - i_s) / 2;
                if (x[i_m, c] == v) return i_m;
                if (x[i_m, c] > v)
                    i_e = i_m - 1;
                else
                    i_s = i_m + 1;
            }

            return i_e;
        }
    }

    public static (int, int) Ex6_Elimination(int[,] x, int v)
    {
        int i = 0, j = x.GetLength(1) - 1;

        while (i <= x.GetLength(0) - 1 && j >= 0)
        {
            if (x[i, j] == v) 
                return (i, j);
            if (x[i, j] > v)
                j--;
            else
                i++;
        }

        return (-1, -1);
    }

    public static int Ex7_HighestTower(IList<(int height, int weight)> people)
    {
        return HighestTower(people, int.MaxValue, int.MaxValue);

        static int HighestTower(IList<(int height, int weight)> people, int maxHeight, int maxWeight)
        {
            var result = 0;
            foreach (var (height, weight) in people)
                if (height < maxHeight && weight < maxWeight)
                    result = Math.Max(result, 1 + HighestTower(people, height, weight));
            return result;
        }
    }

    public static int Ex7_HighestTowerOptimized(IList<(int height, int weight)> people)
    {
        return HighestTower(people, 0);

        static int HighestTower(IList<(int height, int weight)> people, int currentBest)
        {
            foreach (var (height, weight) in people)
            {
                var compatiblePeople = people.Where(p => p.height < height && p.weight < weight).ToList();
                if (currentBest < compatiblePeople.Count + 1)
                    currentBest = 1 + HighestTower(compatiblePeople, currentBest);
            }

            return currentBest;
        }
    }

    public static int Ex7_HighestTowerDynamicProgramming(IList<(int height, int weight)> people) 
    {
        people = people.OrderBy(p => p.height * 1000 + p.weight).ToList();
        var solutions = new Dictionary<(int, int, int), (int, int, int)> { };

        return HighestTower(people.Count, int.MinValue, int.MinValue).heightTower;

        (int heightTower, int minHeightInTower, int minWeigthInTower) HighestTower(int k, int minHeight, int minWeight)
        {
            if (k == 0) return (0, int.MaxValue, int.MaxValue);

            if (solutions.TryGetValue((k, minHeight, minWeight), out var result))
            {
                return result;
            }

            var c1 = HighestTower(k - 1, minHeight, minWeight);
            var (height, weight) = people[people.Count - k];
            var c2 = HighestTower(k - 1, height, weight);
            if (c2.minWeigthInTower > height && c2.minWeigthInTower > weight)
                result = c1.heightTower >= 1 + c2.heightTower 
                    ? c1 
                    : (c2.heightTower + 1, height, weight);
            else
                result = c1;

            solutions[(k, minHeight, minWeight)] = result;
            return result;
        }
    }
}
