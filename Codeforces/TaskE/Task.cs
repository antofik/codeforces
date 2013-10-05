using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskE
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private const long Mod = 1000000007L;
        private const long Max = 102L;
        private long n, m, k;
        private long[,,,] Ways;
        private long[][] C;

        void Solve()
        {
            Input.Next(out n, out m, out k);

            C = new long[Max][];
            C[0] = new long[1];
            C[0][0] = 1;
            for (var i = 1; i < Max; i++)
            {
                C[i] = new long[i + 1];
                C[i][0] = 1; C[i][i] = 1;
                for (var j = 1; j < i; j++) 
                    C[i][j] = C[i - 1][j - 1] + C[i - 1][j] % Mod;
            }

            n /= 2;
            Ways = new long[n + 2, n + 2, n + 2, Max];
            Ways[0, 1, 0, 1] = 1;

            var result = 0L;
            for (var maxValue = 0; maxValue <= n && maxValue < m; maxValue++)
                for (var holes = 0; holes <= n; holes++)
                    for (var total = 0; total <= n; total++)
                        for (var ways = 0; ways <= k; ways++)
                        {
                            var prevWays = Ways[maxValue, holes, total, ways];
                            if (prevWays == 0) continue;
                            if (maxValue > 0)
                            {
                                result += (prevWays * (m - maxValue)) % Mod;
                                result %= Mod;
                            } 

                            for (var newCountOfMaxValues = 1; newCountOfMaxValues <= n - total; newCountOfMaxValues++)
                            {
                                var newWays = ways*C[holes + newCountOfMaxValues - 1][newCountOfMaxValues];
                                if (newWays <= k)
                                {
                                    Ways[maxValue + 1, newCountOfMaxValues, total + newCountOfMaxValues, newWays] += prevWays;
                                    Ways[maxValue + 1, newCountOfMaxValues, total + newCountOfMaxValues, newWays] %= Mod;
                                }
                            }
                        }
            Console.WriteLine(result);
        }
    }

    #region Input

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

        public static List<long> Numbers()
        {
            Next();
            return _line.Split(' ').Select(long.Parse).ToList();
        }
    }

    #endregion

}
