using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        private List<Pair> Pairs;
        private Node Root;
        private long m;
        private long n;
        private List<long> p;

        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private void Solve()
        {
            Input.Next(out n, out m);
            p = Input.Numbers();
            Build();

            long l, r;
            while (Input.Next(out l, out r))
            {
                Console.WriteLine(Root.GetCount(l, r));
            }
        }

        private void Build()
        {
            Pairs = new List<Pair>();
            for (int i = 0; i < n; i++)
                for (int j = 0; j <= i; j++)
                    if (p[i]%p[j] == 0)
                        Pairs.Add(new Pair {L = j, R = i});
            Pairs = Pairs.OrderBy(c => c.L).ThenBy(c => c.R).ToList();
            Root = new Node(Pairs, 0, n - 1);
        }

        public class Node
        {
            public long L;
            public Node Left;
            public List<Pair> Pairs;
            public long R;
            public Node Right;

            public Node(IEnumerable<Pair> pairs, long l, long r)
            {
                Pairs = pairs.OrderBy(c => c.R).ToList();
                if (r <= l + 1 || !Pairs.Any()) return;

                var m = (r + l)/2;
                var middle = Pairs.FirstOrDefault(c => c.L >= m);
                if (middle == null)
                {
                    Left = new Node(Pairs, l, m - 1);
                }
                else
                {
                    var index = Pairs.IndexOf(middle);
                    if (index > 0) Left = new Node(Pairs.Take(index), l, m - 1);
                    Right = new Node(Pairs.Skip(index), m, r);
                }
            }

            public long GetCount(long l, long r)
            {
                if (l <= L && r >= R)
                {
                    return Pairs.Count(c => c.R < r);
                }
                else
                {
                    
                }

                return 0;
            }
        }

        public class Pair
        {
            public long L, R;
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
            bool ok = Next();
            a = ok ? long.Parse(_line) : 0;
            return ok;
        }

        public static bool Next(out long a, out long b)
        {
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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