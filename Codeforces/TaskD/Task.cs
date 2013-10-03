using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        private long m;
        private long n;
        private List<long> p;
        private long[] I;
        private long[] t;
        private List<long>[] divs;

        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        long Get(long x)
        {
            var r = 0L;
            for (; x>=0; x = (x&(x+1)) - 1)
            {
                r += t[x];
                if (x == 0) break;
            }
            return r;
        }

        private void Solve()
        {
            Input.Next(out n, out m);
            p = Input.Numbers();
            t = new long[n];
            divs = new List<long>[n+1];

            I = Enumerable.Repeat(-1L, (int) n + 1).ToArray();
            for (var i = 0; i < n; i++)
                I[p[i]] = i;

            for (var i = 0; i < n; i++)
                for (var j = p[i]; j <= n; j += p[i])
                {
                    if(divs[i] ==null) divs[i] = new List<long>();
                    if (I[j] != -1)
                        if (i < I[j])
                            divs[i].Add(I[j]);
                        else
                            divs[I[j]].Add(i);
                }

            var q = new Query[m];
            for (var i = 0; i < m; q[i].i = i++)
                Input.Next(out q[i].l, out q[i].r);
            
            q = q.OrderByDescending(c => c.l).ToArray();
            
            var answer = new long[m];
            var pi = n;
            foreach (var qq in q)
            {
                for (;pi >= qq.l;pi--)
                    foreach (var pj in divs[pi-1])
                        for (var x=pj; x < n; x |= (x + 1))
                            t[x]++;

                var r = 0L;
                for (var x = qq.r-1; x >= 0; x = (x & (x + 1)) - 1)
                {
                    r += t[x];
                    if (x == 0) break;
                }
                answer[qq.i] = r;
            }

            Console.WriteLine(string.Join("\n", answer));
        }
    }

    struct Query
    {
        public long l;
        public long r;
        public long i;

        public override string ToString()
        {
            return string.Format("{0}-{1} [{2}]", l, r, i);
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