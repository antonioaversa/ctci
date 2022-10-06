namespace CTCI;

public static class Exercises1
{
    public static void Ex2_Reverse(char[] s)
    {
        RReverse(s, 0);
    }

    private static (int, char) RReverse(char[] s, int i)
    {
        if (s.Length <= i) throw new Exception("Malformed string");
        if (s[i] == '\0') return (i, '\0');
        var (N, v) = RReverse(s, i + 1);
        if (i < N / 2)
        {
            s[i] = v;
            return (N, '\0');
        }
        else
        {
            var t = s[i];
            s[i] = s[N - 1 - i];
            return (N, t);
        }
    }

    public static IEnumerable<char> Ex3_RemoveDuplicates(char[] s)
    {
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '\0')
                continue;

            yield return s[i];

            for (var j = i + 1; j < s.Length; j++)
                if (s[i] == s[j])
                    s[j] = '\0';
        }
    }

    public static void Ex3_RemoveDuplicatesInPlace(char[] s)
    {
        var next = 0;
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '\0')
                continue;

            s[next] = s[i];
            next++;

            for (var j = next; j < s.Length; j++)
                if (s[i] == s[j])
                    s[j] = '\0';
        }
    }

    public static bool Ex4_AreAnagrams(string s1, string s2)
    {
        if (s1.Length != s2.Length)
            return false;
        var s1Occurrences = GetOccurrences(s1);
        foreach (var c in s2)
        {
            if (!s1Occurrences.TryGetValue(c, out var i) || i == 0)
                return false;
            s1Occurrences[c] = i - 1;
        }
        return true;
    }

    private static IDictionary<char, int> GetOccurrences(string s)
    {
        var occurrences = new Dictionary<char, int> { };
        foreach (var c in s)
        {
            if (!occurrences.TryGetValue(c, out var i))
                occurrences[c] = 1;
            else occurrences[c] = i + 1;
        }

        return occurrences;
    }

    public static IEnumerable<char> Ex5_ReplaceSpaces(string s)
    {
        foreach (var c in s)
        {
            if (c == ' ')
            {
                yield return '%';
                yield return '2';
                yield return '0';
            }
            else
            {
                yield return c;
            }
        }
    }

    public static void Ex5_ReplaceSpacesInPlace(char[] s)
    {
        var length = 0;
        var numberOfSpaces = 0;
        foreach (var c in s) 
        {
            if (c == '\0') break;
            if (c == ' ') numberOfSpaces++;
            length++;
        }

        var j = length + numberOfSpaces * 2;
        if (s.Length <= j) throw new ArgumentException($"{nameof(s)} is not big enough");
        for (var i = length; i >= 0; i--)
        {
            if (s[i] == ' ')
            {
                s[j--] = '0';
                s[j--] = '2';
                s[j--] = '%';
            }
            else s[j--] = s[i];
        }
    }

    public static void Ex6_RotateMatrix(int[,] m)
    {
        var n = m.GetLength(0);
        for (var i = 0; i < n / 2; i++)
        {
            for (var j = i; j < n - 1 - i; j++)
            {
                var (v1, v2, v3, v4) = (m[i, j], m[j, n - 1 - i], m[n - 1 - i, n - 1 -i - j], m[n - 1 - i - j, i]);
                (m[i, j], m[j, n - 1 - i], m[n - 1 - i, n - 1 - i - j], m[n - 1 - i - j, i]) = (v2, v3, v4, v1);
            }
        }
    }

    public static void Ex7_ResetRowAndColumn(int[,] x)
    {
        var m = x.GetLength(0);
        var n = x.GetLength(1);
        var zeroInRow = new bool[n];
        var zeroInColumn = new bool[m];
        for (var i = 0; i < m; i++)
        {
            zeroInRow[i] = false;
            for (var j = 0; j < n; j++)
            {
                if (x[i, j] == 0)
                {
                    zeroInRow[i] = true;
                    break;
                }
            }
        }

        for (var j = 0; j < n; j++)
        {
            zeroInColumn[j] = false;
            for (var i = 0; i < m; i++)
            {
                if (x[i, j] == 0)
                {
                    zeroInColumn[i] = true;
                    break;
                }
            }
        }

        for (var i = 0; i < m; i++)
            for (var j = 0; j < n; j++)
                if (zeroInRow[i] || zeroInColumn[j])
                    x[i, j] = 0;
    }

    public static bool Ex8_IsRotation(string s1, string s2)
    {
        if (s1.Length != s2.Length) return false;
        return ((s1 + s1).Contains(s2));
    }
}