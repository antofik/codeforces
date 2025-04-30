using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;
using CompLib.Mathematics;

public class Program
{
    private const int Max = 400000;
    private List<int> P;
    private long[] CusumP;

    public void Solve()
    {
        P = new List<int>();
        MathEx.Sieve(10000000, P);

        // Console.WriteLine(P.Count);
        CusumP = new long[Max + 1];
        for (int i = 0; i < Max; i++)
        {
            CusumP[i + 1] = CusumP[i] + P[i];
        }

        var sc = new Scanner();
#if !DEBUG
        System.Console.SetOut(new System.IO.StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Query(sc);
        }

        Console.Out.Flush();
    }

    void Query(Scanner sc)
    {
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        Array.Sort(a);
        Array.Reverse(a);
        var cusumA = new long[n + 1];
        for (int i = 0; i < n; i++)
        {
            cusumA[i + 1] = cusumA[i] + a[i];
            Console.Write($"{cusumA[i + 1]}\t");
        }
        Console.WriteLine();

        for(int i=0;i<n;i++)
        {
            Console.Write($"{CusumP[i + 1]}\t");
        }
        Console.WriteLine();

        // 2 3 5 7

        int l = 0;
        int r = n + 1;
        while (r - l > 1)
        {
            int mid = (l + r) / 2;
            if (CusumP[mid] <= cusumA[mid]) l = mid;
            else r = mid;
        }

        Console.WriteLine(n - l);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Mathematics
{
    using System;
    using System.Collections.Generic;

    #region GCD LCM

    /// <summary>
    /// 様々な数学的関数の静的メソッドを提供します．
    /// </summary>
    public static partial class MathEx
    {
        /// <summary>
        /// 2 つの整数の最大公約数を求めます．
        /// </summary>
        /// <param name="n">最初の値</param>
        /// <param name="m">2 番目の値</param>
        /// <returns>2 つの整数の最大公約数</returns>
        /// <remarks>ユークリッドの互除法に基づき最悪計算量 O(log N) で実行されます．</remarks>
        public static int GCD(int n, int m)
        {
            return (int)GCD((long)n, m);
        }


        /// <summary>
        /// 2 つの整数の最大公約数を求めます．
        /// </summary>
        /// <param name="n">最初の値</param>
        /// <param name="m">2 番目の値</param>
        /// <returns>2 つの整数の最大公約数</returns>
        /// <remarks>ユークリッドの互除法に基づき最悪計算量 O(log N) で実行されます．</remarks>
        public static long GCD(long n, long m)
        {
            n = Math.Abs(n);
            m = Math.Abs(m);
            while (n != 0)
            {
                m %= n;
                if (m == 0) return n;
                n %= m;
            }

            return m;
        }


        /// <summary>
        /// 2 つの整数の最小公倍数を求めます．
        /// </summary>
        /// <param name="n">最初の値</param>
        /// <param name="m">2 番目の値</param>
        /// <returns>2 つの整数の最小公倍数</returns>
        /// <remarks>最悪計算量 O(log N) で実行されます．</remarks>
        public static long LCM(long n, long m)
        {
            return (n / GCD(n, m)) * m;
        }
    }

    #endregion

    #region PrimeSieve

    public static partial class MathEx
    {
        /// <summary>
        /// ある値までに素数表を構築します．
        /// </summary>
        /// <param name="max">最大の値</param>
        /// <param name="primes">素数のみを入れた数列が返される</param>
        /// <returns>0 から max までの素数表</returns>
        /// <remarks>エラトステネスの篩に基づき，最悪計算量 O(N loglog N) で実行されます．</remarks>
        public static bool[] Sieve(int max, List<int> primes = null)
        {
            var isPrime = new bool[max + 1];
            for (int i = 2; i < isPrime.Length; i++) isPrime[i] = true;
            for (int i = 2; i * i <= max; i++)
                if (!isPrime[i]) continue;
                else
                    for (int j = i * i; j <= max; j += i)
                        isPrime[j] = false;
            if (primes != null)
                for (int i = 0; i <= max; i++)
                    if (isPrime[i])
                        primes.Add(i);

            return isPrime;
        }
    }

    #endregion
}

namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public ulong NextULong() => ulong.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().AsParallel().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().AsParallel().Select(long.Parse).ToArray();
        public ulong[] ULongArray() => Array().AsParallel().Select(ulong.Parse).ToArray();
        public double[] DoubleArray() => Array().AsParallel().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().AsParallel().Select(decimal.Parse).ToArray();
    }
}