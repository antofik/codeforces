using System;
using System.Linq;

namespace CodeforcesRound181div2.TaskD
{
    public class Task
    {
        const long MaxDepth = 29;
        const long MaxK = 1000;
        const long Mod = 7340033;
        readonly long[,] Dp = new long[MaxDepth + 1, MaxK + 2];
        readonly long[] D = new long[MaxK * 2 + 2];

        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private void Solve()
        {
            Input.Next();

            Dp[0, 0] = 1;
            Dp[1, 0] = 1;
            Dp[1, 1] = 1;
            for (var d = 2; d <= MaxDepth; d++)
            {
                for (var k1 = 0; k1 <= MaxK; k1++)
                {
                    for (var k2 = 0; k2 <= MaxK; k2++)
                    {
                        D[k1 + k2] += Dp[d - 1, k1]*Dp[d - 1, k2];
                    }
                }
                for (var i = 0; i < D.Length; i++) D[i] %= Mod;
                for (var k1 = 0; k1 <= MaxK; k1++)
                {
                    for (var k2 = 0; k2 <= MaxK - k1; k2++)
                    {
                        Dp[d, k1 + k2 + 1] += D[k1] * D[k2];
                    }
                }
                for (var i = 0; i <= MaxK; i++) Dp[d, i] %= Mod;
                Dp[d, 0] = 1;

                Array.Clear(D, 0, D.Length);
            }

            long n, k;
            while (Input.Next(out n, out k))
            {
                var depth = 0;
                while (n > 1 && n % 2 == 1)
                {
                    n /= 2;
                    depth++;
                }
                var count = Dp[depth, k];
                Console.WriteLine(count);
            }
        }
    }

    public class Input
    {
        private static string _line;

        public static bool Next()
        {
            _line = Console.ReadLine();
            return !string.IsNullOrEmpty(_line);
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
    }
}
