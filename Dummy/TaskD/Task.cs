using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private List<long> a;
        private long[] x;
        private long[] y;
        private long[,] distances;
        private long d, n;
        private bool[] used;
        private long[] costs;

        bool Dijkstra(long start)
        {
            Array.Clear(used, 0, used.Length);
            for (var i = 0; i < n; i++) costs[i] = -1;
            costs[0] = start;
            for (var i = 0; i < n; i++)
            {
                var cost = -1L;
                var target = -1L;
                for (var j = 0; j < n; j++)
                    if (!used[j] && costs[j] > cost)
                    {
                        cost = costs[j];
                        target = j;
                    }
                if (target == -1) break;
                used[target] = true;
                for (var j = 0; j < n; j++)
                    if (distances[target, j] + costs[target] > costs[j])
                        costs[j] = distances[target, j] + costs[target];
                if (costs[n - 1] >= 0) break;

            }
            return costs[n - 1] >= 0;
        }

        void Solve()
        {
            Input.Next(out n, out d);

            a = Input.Numbers();
            a.Insert(0, 0);
            a.Add(0);
            x = new long[n];
            y = new long[n];
            for (var i = 0; i < a.Count; i++)
                Input.Next(out x[i], out y[i]);

            distances = new long[n, n];
            for (var i = 0; i < n; i++)
                for (var j = 0; j < n; j++)
                    distances[i, j] = i == j ? 0 : a[j] - (Math.Abs(x[j] - x[i]) + Math.Abs(y[j] - y[i])) * d;

            long l = 0;
            long r = 1000000000;
            costs = new long[n];
            used = new bool[n];
            while (l < r)
            {
                var cost = (l + r) / 2;

                if (Dijkstra(cost)) r = cost; else l = cost + 1;

            }

            Console.WriteLine(l);
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
            if (string.IsNullOrEmpty(_line)) return new List<long>();
            return _line.Split(' ').Select(long.Parse).ToList();
        }
    }

    #endregion
}
