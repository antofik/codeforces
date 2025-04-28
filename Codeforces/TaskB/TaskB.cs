using System;
using System.Collections.Generic;
using System.Linq;
using static Codeforces.TaskB.Input;
using static Codeforces.TaskB.Output;
using static Codeforces.TaskB.Primes;
using static Codeforces.TaskB.Combinations;
using static Codeforces.TaskB.Utility;
using System.Numerics;
using System.Text;

namespace Codeforces.Task
{
    public class TaskB
    {
        public static int TestCount;
        public static int Test;

        private readonly long MOD = 1000_000_007;

        private long Solve()
        {
            Read(out int n);
            long[] A = ArrayLong();

            return -1;
        }

        private void SolveAll()
        {
            TestCount = Int();
            for (int Test = 1; Test <= TestCount; ++Test)
            {
                Write(Solve());
            }
        }

        public static void Main()
        {
            var task = new TaskB();
#if DEBUG
            task.SolveAll();
#else
            try
            {
                task.SolveAll();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
#endif
        }
    }
}

namespace Codeforces.TaskB
{
    public class Input
    {
        private static string _line = "";

        public static int Int()
        {
            return int.Parse(Console.ReadLine()!);
        }

        public static int Long()
        {
            return int.Parse(Console.ReadLine()!);
        }

        public static bool Read()
        {
            _line = Console.ReadLine()!;
            return _line != null;
        }

        public static bool Read(out long a)
        {
            var ok = Read();
            a = ok ? long.Parse(_line) : 0;
            return ok;
        }

        public static bool Read(out long a, out long b)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
            }
            else
            {
                a = b = 0;
            }

            return ok;
        }

        public static bool Read(out long a, out long b, out long c)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
            }
            else
            {
                a = b = c = 0;
            }
            return ok;
        }

        public static bool Read(out long a, out long b, out long c, out long d)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
            }
            else
            {
                a = b = c = d = 0;
            }
            return ok;
        }

        public static bool Read(out long a, out long b, out long c, out long d, out long e)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
                e = array[4];
            }
            else
            {
                a = b = c = d = e = 0;
            }
            return ok;
        }

        public static bool Read(out int a)
        {
            var ok = Read();
            a = ok ? int.Parse(_line) : 0;
            return ok;
        }

        public static bool Read(out int a, out int b)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(int.Parse).ToArray();
                a = array[0];
                b = array[1];
            }
            else
            {
                a = b = 0;
            }

            return ok;
        }

        public static bool Read(out int a, out int b, out int c)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(int.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
            }
            else
            {
                a = b = c = 0;
            }
            return ok;
        }

        public static bool Read(out int a, out int b, out int c, out int d)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(int.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
            }
            else
            {
                a = b = c = d = 0;
            }
            return ok;
        }

        public static bool Read(out int a, out int b, out int c, out int d, out int e)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(int.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
                e = array[4];
            }
            else
            {
                a = b = c = d = e = 0;
            }
            return ok;
        }

        public static bool Read(out int a, out int b, out int c, out int d, out int e, out int f)
        {
            var ok = Read();
            if (ok)
            {
                var array = _line.Split(' ').Select(int.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
                e = array[4];
                f = array[5];
            }
            else
            {
                a = b = c = d = e = f = 0;
            }
            return ok;
        }

        public static int[] ArrayInt()
        {
            var list = Console.ReadLine()!.Split(' ').Select(x => int.Parse(x)).ToList();
            list.Insert(0, 0);
            return list.ToArray();
        }

        public static long[] ArrayLong()
        {
            var list = Console.ReadLine()!.Split(' ').Select(x => long.Parse(x)).ToList();
            list.Insert(0, 0);
            return list.ToArray();
        }

        public static bool Read(out string value)
        {
            value = string.Empty;
            if (!Read()) return false;
            value = _line;
            return true;
        }
    }

    public static class Output
    {
        public static void Write(string value)
        {
            Console.WriteLine(value);         
        }

        public static void DEBUG(string value)
        {
#if DEBUG
            Console.Write("#");
            Console.WriteLine(value);
#endif
        }

        public static void Write<T>(T value, bool debug = false) where T : struct
        {
            Console.WriteLine(value);
        }

        public static void DEBUG<T>(T value, bool debug = false) where T : struct
        {
#if DEBUG
            Console.Write("#");
            Console.WriteLine(value);
#endif
        }

        public static void Write(bool condition)
        {
            if (condition)
            {
                YES();
            }
            else
            {
                NO();
            }
        }

        public static void YES()
        {
            Write("YES");
        }

        public static void NO()
        {
            Write("NO");
        }

        public static void Write<T>(T[] array, int start = 1, char separator = ' ', bool debug = false)
        {
            var str = new StringBuilder();
            for (int i = 1; i < array.Length; ++i)
            {
                if (i > start) str.Append(separator);
                str.Append(array[i]);
            }
            Console.WriteLine(str.ToString());
        }

        public static void DEBUG<T>(T[] array, int start = 1, char separator = ' ', bool debug = false)
        {
#if DEBUG
            var str = new StringBuilder();
            str.Append("#");
            for (int i = 1; i < array.Length; ++i)
            {
                if (i > start)
                {
                    str.Append(separator);
                    if (separator == '\n')
                    {
                        str.Append("#");
                    }
                }
                str.Append(array[i]);
            }
            Console.WriteLine(str.ToString());
#endif
        }

        public static void Write<T>(T[] array, T[] array2, int start = 1, bool debug = false)
        {
            var str = new StringBuilder();
            for (int i = 1; i < array.Length; ++i)
            {
                if (i > start) str.Append('\n');
                str.Append(array[i]);
                str.Append(' ');
                str.Append(array2[i]);
            }
            Console.WriteLine(str.ToString());
        }

        public static void DEBUG<T>(T[] array, T[] array2, int start = 1, bool debug = false)
        {
#if DEBUG
            var str = new StringBuilder();
            for (int i = 1; i < array.Length; ++i)
            {
                if (i > start) str.Append('\n');
                str.Append("#\t");
                str.Append(array[i]);
                str.Append(' ');
                str.Append(array2[i]);
            }
            Console.WriteLine(str.ToString());
#endif
        }
    }

    /// <summary>
    /// Prime numbers
    /// </summary>
    public class Primes
    {
        /// <summary>
        /// Returns prime numbers in O(n)
        /// Returns lowet divisors as well
        /// Memory O(n)
        /// </summary>
        public static void ImprovedSieveOfEratosthenes(int n, out int[] lp, out List<int> pr)
        {
            lp = new int[n];
            pr = new List<int>();
            for (var i = 2; i < n; i++)
            {
                if (lp[i] == 0)
                {
                    lp[i] = i;
                    pr.Add(i);
                }
                foreach (var prJ in pr)
                {
                    var prIj = i * prJ;
                    if (prJ <= lp[i] && prIj <= n - 1)
                    {
                        lp[prIj] = prJ;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns prime numbers in O(n*n)
        /// </summary>
        public static void SieveOfEratosthenes(int n, out List<int> pr)
        {
            var m = 50000;//(int)(3l*n/(long)Math.Log(n)/2);
            pr = new List<int>();
            var f = new bool[m];
            for (var i = 2; i * i <= n; i++)
                if (!f[i])
                    for (var j = (long)i * i; j < m && j * j < n; j += i)
                        f[j] = true;
            pr.Add(2);
            for (var i = 3; i * i <= n; i += 2)
                if (!f[i])
                    pr.Add(i);
        }

        /// <summary>
        /// Greatest common divisor 
        /// </summary>
        public static int Gcd(int x, int y)
        {
            while (y != 0)
            {
                var c = y;
                y = x % y;
                x = c;
            }
            return x;
        }

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        public static long Gcd(long x, long y)
        {
            while (y != 0)
            {
                var c = y;
                y = x % y;
                x = c;
            }
            return x;
        }

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        public static long GcdEx(long x, long y, out long a, out long b)
        {
            if (x == 0)
            {
                a = 0;
                b = 1;
                return y;
            }
            long g = GcdEx(y % x, x, out long a1, out long b1);
            a = b1 - (y / x) * a1;
            b = a1;
            return g;
        }

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        public static long Reverse(long x, long MOD)
        {
            long g = GcdEx(x, MOD, out long a, out long b);
            while (a < 0) a += MOD;
            return g == 1 ? a : 0;
        }

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        public static BigInteger Gcd(BigInteger x, BigInteger y)
        {
            while (y != 0)
            {
                var c = y;
                y = x % y;
                x = c;
            }
            return x;
        }

        /// <summary>
        /// Returns all divisors of n in O(?n)
        /// </summary>
        public static IEnumerable<long> GetDivisors(long n)
        {
            long r;
            while (true)
            {
                var x = Math.DivRem(n, 2, out r);
                if (r != 0) break;
                n = x;
                yield return 2;
            }
            var i = 3;
            while (i <= Math.Sqrt(n))
            {
                var x = Math.DivRem(n, i, out r);
                if (r == 0)
                {
                    n = x;
                    yield return i;
                }
                else i += 2;
            }
            if (n != 1) yield return n;
        }
    }

    public class Combinations
    {
        public static long[] GetFactorials(int n, long MOD)
        {
            long[] factorials = new long[n + 1];
            factorials[0] = 1;
            long value = 1;
            for (int i = 1; i <= n; ++i)
            {
                value = value * i % MOD;
                factorials[i] = value;
            }
            return factorials;
        }

        /// <summary>
        /// https://ru.wikipedia.org/wiki/????????????
        /// </summary>
        public static long[] GetSubfactorials(int n, long MOD)
        {
            long[] subfactorials = new long[n + 1];
            subfactorials[0] = 1;

            long prev = 1;
            for (int i = 1; i <= n; ++i)
            {
                prev = (prev * i + (i % 2 == 0 ? 1 : -1)) % MOD;
                subfactorials[i] = prev;
            }

            return subfactorials;
        }

        public static int[,] GetCombinations(int n, long MOD)
        {
            int[,] C = new int[n + 1, n + 1];
            C[0, 0] = 1;
            for (int i = 1; i <= n; ++i)
            {
                C[i, 0] = 1;
                C[i, i] = 1;
                for (int j = 1; j < i; j++)
                {
                    long c = ((long)C[i - 1, j] + C[i - 1, j - 1]) % MOD;
                    C[i, j] = (int)c;
                }
            }
            return C;
        }

        public static long Power(long x, long l, long MOD)
        {
            long result = 1;
            long n = 1;
            long m = x;
            while (l != 0)
            {
                long i = l & ~(l - 1);
                l -= i;
                while (n < i)
                {
                    m = m * m % MOD;
                    n <<= 1;
                }
                result = result * m % MOD;
            }
            return result;
        }
    }

    public static class Utility
    {
        public static void Swap<T>(List<T> list, int i, int j)
        {
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }

    public class Pair<K, V>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Pair() {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Pair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public K Key { get; set; }
        public V Value { get; set; }
    }

    public class Map<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new() where TKey : notnull
    {
        public bool Create { get; set; } = true;

        public new TValue this[TKey key]
        {
            set
            {
                base[key] = value;
            }
            get
            {
                if (TryGetValue(key, out TValue v))
                {
                    return v;
                }
                if (Create)
                {
                    v = new();
                    this[key] = v;
                    return v;
                }
                else
                {
                    return default;
                }
            }
        }
    }

}
