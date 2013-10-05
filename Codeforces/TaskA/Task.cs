using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        void Solve()
        {
            long n, a, b, c;
            Input.Next(out n, out a, out b, out c);
            var vs = new [] {a, b, c}.Where(x=>x <= n).Distinct().ToArray();

            var dp = new long[n + 1];

            foreach (var v in vs)
            {
                dp[v] = 1;
            }

            for (var i = 0; i < n; i++)
            {
                var m = -1L;
                foreach (var v in vs)
                {
                    var d = i + 1 - v >= 0 ? dp[i + 1 - v] : -1;
                    if (d > m) m = d;
                }
                dp[i + 1] = m >= 0 ? m + 1 : -1;
            }

            Console.WriteLine(dp[n]);
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

        public static bool Next(out string value)
        {
            value = string.Empty;
            if (!Next()) return false;
            value = _line;
            return true;
        }
    }

    #endregion

}
