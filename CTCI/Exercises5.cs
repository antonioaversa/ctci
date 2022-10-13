using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace CTCI;

public static class Exercises5
{
    public static int Ex1(int n, int m, int i, int j)
    {
        var mask = (1 << (j - i + 1)) - 1;
        var r1 = n & ~(mask << i);
        var r2 = (m & mask) << i;
        return r1 | r2;
    }

    public static string Ex2(string decNumberString)
    {
        var decimalSeparator = '.';
        var decNumberStringParts = decNumberString.Split(decimalSeparator);

        if (decNumberStringParts.Length == 1)
            return new string(IntNumberStringToBinary(decNumberStringParts[0]).ToArray());

        if (decNumberStringParts.Length > 2)
            return "ERROR";

        var intPart = decNumberStringParts[0];
        var decimalPart = decNumberStringParts[1];

        try
        {
            var binaryIntPart = IntNumberStringToBinary(intPart);
            var binaryDecimalPart = DecNumberStringToBinary(decimalPart);
            var binaryString = binaryIntPart.Append(decimalSeparator).Concat(binaryDecimalPart);

            return new string(binaryString.ToArray());
        }
        catch (Exception)
        {
            return "ERROR";
        }

        IEnumerable<char> IntNumberStringToBinary(string intNumberString) 
        {
            var intValue = long.Parse(intNumberString, CultureInfo.InvariantCulture);
            var intDigits = new StringBuilder();
            for (var i = 0; true; i++)
            {
                intDigits.Append(intValue & 1);
                intValue >>= 1;
                if (intValue == 0)
                    break;
            }
            return intDigits.ToString().Reverse();
        }

        IEnumerable<char> DecNumberStringToBinary(string decNumberString)
        {
            var numberOfDigits = decNumberString.Length;
            var highestPower = (long)Math.Pow(10, numberOfDigits);
            var decValue = long.Parse(decNumberString, CultureInfo.InvariantCulture);
            var decDigits = new StringBuilder();
            int i;
            for (i = 1; i < 32; i++)
            {
                decValue *= 2;
                if (decValue >= highestPower)
                {
                    decDigits.Append('1');
                    decValue -= highestPower;
                }
                else
                {
                    decDigits.Append('0');
                }

                if (decValue == 0)
                    break;
            }

            if (i == 32)
                throw new Exception("Precision loss");

            return decDigits.ToString();
        }
    }

    public static (int, int) Ex3(int n)
    {
        if (n == 0) return (int.MinValue, int.MaxValue);

        // Next smaller: find lowest "10" and replace with "01"
        int i = 0, n1 = n;
        while (n1 != 0 && (n1 & 0b11) != 0b10)
        {
            n1 >>= 1;
            i++;
        }

        // "10" not found => all ones => return int.MinValue
        // "10" found with 0 at position i and 1 at position i + 1 => replace with "01"
        int nextSmaller = n1 == 0 ? int.MinValue : n & ~(1 << (i + 1)) | (1 << i);

        // Next bigger: find lowest "01" and replace with "10"
        int j = 0, n2 = n;
        while ((n2 & 0b11) != 0b01)
        {
            n2 >>= 1;
            j++;
        }

        // "01" found with 1 at position j and 0 at position j + 1 => replace with "10"
        int nextBigger = n & ~(1 << j) | (1 << (j + 1));

        return (nextSmaller, nextBigger);
    }

    public static bool Ex4(int n)
    {
        return (n & (n - 1)) == 0;
    }

    public static int Ex5(int a, int b)
    {
        var d = a ^ b;
        var i = 0;
        while (d != 0)
        {
            i += d & 1;
            d >>= 1;
        }
        return i;
    }

    public static int Ex6(int n)
    {
        int m = 0b01010101_01010101_01010101_01010101;
        return ((n & m) << 1) | ((n & ~m) >> 1);
    }

    public static int Ex7(int n, Func<int, int, byte> bit)
    {
        if (n == 0)
            throw new ArgumentException($"{nameof(n)} must be positive.");

        int highestBitSet = 0, n1 = n;
        while (n1 != 0)
        {
            highestBitSet++;
            n1 >>= 1;
        }

        int nextPowerOfTwo = 1 << highestBitSet;

        var result = 0;
        for (var i = n + 1; i < nextPowerOfTwo; i++)
        {
            result ^= i;
        }

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < 32; j++)
            {
                var currentBit = (result & (1 << j)) >> j;
                var newBit = currentBit ^ bit(i, j);
                result ^= (-newBit ^ result) & (1 << j);
            }
        }

        return result;
    }
}
