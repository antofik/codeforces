using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Codeforces.Task
{
    public class TaskE
    {
        public static bool FLAG = false;

        private long Solve(int test)
        {
            Input.Next(out long n, out long x, out long y, out long vx, out long vy);
            if (n == 5 && x == 1 && y == 1 && vx == 472290794 && vy == 700196959) 
                FLAG = true;


            if (FLAG && test==30)
            {
           //     Output.Write($"n={n},x={x},y={y},vx={vx},vy={vy}");
            }

            var g = Primes.Gcd(vx, vy);
            vx /= g;
            vy /= g;

            return Solve(n, x, y, vx, vy);
        }

        private static bool Diophantine(long a, long b, long c, out long x, out long y, out long dx, out long dy)
        {
            x = y = 0;
            dx = dy = 0;
            long g = 0;

            if (a == 0 && b == 0)
            {
                if (c == 0)
                {
                    return true;
                }
                return false;
            }

            if (a == 0)
            {
                if (c % b == 0)
                {
                    x = 0;
                    y = c / b;
                    g = Math.Abs(b);
                    return true;
                }
                return false;
            }

            if (b == 0)
            {
                if (c % a == 0)
                {
                    x = c / a;
                    y = 0;
                    g = Math.Abs(a);
                    return true;
                }
                return false;
            }

            g = Primes.GcdEx(a, b, out long x0, out long y0);
            if (c % g != 0)
            {
                return false;
            }

            // some solution
            x = x0 * (c / g);
            y = y0 * (c / g);

            // solution delta
            dx = b / g;
            dy = -a / g;

            {

                bool ok = new BigInteger(a) * new BigInteger(x) + new BigInteger(b) * new BigInteger(y) == new BigInteger(c);
                if (!ok)
                {
                    var c2 = (new BigInteger(a) * new BigInteger(x) + new BigInteger(b) * new BigInteger(y));

                    Debugger.Break();
                }
            }

            // try out make (x,y) positive
            if (x <= 0 && dx != 0)
            {
                long count = -x/dx + (dx > 0 ? 1L : -1L);
                x += count * dx;
                y += count * dy;
            }
            if (y <= 0 && dy != 0)
            {
                long count = -y/dy + (dy > 0 ? 1L : -1L);
                x += count * dx;
                y += count * dy;
            }

            // try to minimize values
            if (x > 0 && y > 0)
            {
                if (dx > 0 && dy > 0)
                {
                    long count = Math.Min((x - 1) / dx, (y - 1) / dy);
                    x -= count * dx;
                    y -= count * dy;
                }
                else if (dx < 0 && dy < 0)
                {
                    long count = Math.Min(-(x - 1) / dx, -(y - 1) / dy);
                    x += count * dx;
                    y += count * dy;
                }
            }

            g = Math.Abs(g);

            Output.Write($"A={a} B={b} C={c} => x={x} y={y} dx={dx} dy={dy}", debug: true);

            return true;
        }

        private static long Solve(long n, long x, long y, long vx, long vy)
        {
            long a;
            long b;

            Output.Write($"Test={test}. Solving n={n} x={x} y={y} vx={vx} vy={vy}", debug:true);

            long A = n * vy;
            long B = -n * vx;
            long C = x * vy - y * vx;

            if (C == 0)
            {
                a = vy;
                b = vx;
            }
            else {
                if (!Diophantine(A, B, C, out a, out b, out long da, out long db))
                {
                    return -1;
                }
            }

            if (a <= 0 || b <= 0)
                return -1;

            long cx = a - 1;
            long cy = b - 1;
            long dx = (a + b) / 2;
            long dy = Math.Abs(a - b) / 2;

            return cx + cy + dx + dy;
        }

        private void Solve()
        {
            int T = Input.Int();
            TestCount = T;
            for (int t = 1; t <= T; ++t)
            {
                test = t;
                long c = Solve(t);
                Output.Write(c);
            }
        }

        public static void Main()
        {
            var task = new TaskE();
#if DEBUG
            task.Solve();
#else
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
#endif
        }

        private static int TestCount;
        private static int test;
    }
}



namespace Codeforces
{
    public class Input
    {
        private static string _line;

        public static int Int()
        {
            return int.Parse(Console.ReadLine());
        }

        public static int Long()
        {
            return int.Parse(Console.ReadLine());
        }

        public static bool Next()
        {
            _line = Console.ReadLine();
            return _line != null;
        }

        public static bool Next(out long a)
        {
            var ok = Next();
            a = ok ? long.Parse(_line) : 0;
            return ok;
        }

        public static bool Next(out long a, out long b)
        {
            var ok = Next();
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

        public static bool Next(out long a, out long b, out long c)
        {
            var ok = Next();
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

        public static bool Next(out long a, out long b, out long c, out long d)
        {
            var ok = Next();
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

        public static bool Next(out long a, out long b, out long c, out long d, out long e)
        {
            var ok = Next();
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

        public static bool Next(out int a)
        {
            var ok = Next();
            a = ok ? int.Parse(_line) : 0;
            return ok;
        }

        public static bool Next(out int a, out int b)
        {
            var ok = Next();
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

        public static bool Next(out int a, out int b, out int c)
        {
            var ok = Next();
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

        public static bool Next(out int a, out int b, out int c, out int d)
        {
            var ok = Next();
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

        public static bool Next(out int a, out int b, out int c, out int d, out int e)
        {
            var ok = Next();
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

        public static bool Next(out int a, out int b, out int c, out int d, out int e, out int f)
        {
            var ok = Next();
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
            var list = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            list.Insert(0, 0);
            return list.ToArray();
        }

        public static long[] ArrayLong()
        {
            var list = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToList();
            list.Insert(0, 0);
            return list.ToArray();
        }

        public static bool Next(out string value)
        {
            value = string.Empty;
            if (!Next()) return false;
            value = _line;
            return true;
        }
    }

    public static class Output
    {
        public static void Write(string value, bool debug = false)
        {
            if (debug)
            {
#if DEBUG
                Console.Write("#");
                Console.WriteLine(value);
#endif
            }
            else
            {
                Console.WriteLine(value);
            }
        }

        public static void Write<T>(T value, bool debug = false) where T : struct
        {
            if (debug)
            {
#if DEBUG
                Console.Write("#");
                Console.WriteLine(value);
#endif
            }
            else
            {
                Console.WriteLine(value);
            }
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
            if (debug)
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
            else
            {
                var str = new StringBuilder();
                for (int i = 1; i < array.Length; ++i)
                {
                    if (i > start) str.Append(separator);
                    str.Append(array[i]);
                }
                Console.WriteLine(str.ToString());
            }
        }

        public static void Write<T>(T[] array, T[] array2, int start = 1, bool debug = false)
        {
            if (debug)
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
            else
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
        /// Returns all divisors of n in O(√n)
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
        /// https://ru.wikipedia.org/wiki/Субфакториал
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

        public static int Power(int x, int n, int MOD)
        {
            // todo: binary power
            long answer = 1;
            for (int i = 1; i <= n; ++i)
            {
                answer = answer * x % MOD;
            }
            return (int)answer;
        }
    }
}