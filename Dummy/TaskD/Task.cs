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

        void Solve()
        {
            long n, d;
            var distances = new long[1000,1000];
            Input.Next(out n, out d);

            var stations = new List<Station>();
            var a = Input.Numbers();
            for (var i = 0; i < a.Count;i++)
            {
                long x, y;
                Input.Next(out x, out y);
                stations.Add(new Station { Index = i, A = a[i], X = x, Y = y });
            }

            for (var i=0;i<n;i++)
                for (var j = 0; j < n; j++)
                    if (i == j)
                    {
                        distances[i, j] = 0;
                    }
                    else
                    {
                        var from = stations[i];
                        var to = stations[j];
                        distances[i, j] = to.A - Math.Abs(to.X - from.X) - Math.Abs(to.Y - from.Y);
                    }

            long l = 0;
            long r = 1000000000;
            var costs = new int[n];
            while (l < r){

                var cost = (l + r)/2;
                var items = stations.ToList();
                
            }

            Console.WriteLine(l);
        }
        
        struct Station
        {
            public long Index;
            public long A;
            public long X;
            public long Y;
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
