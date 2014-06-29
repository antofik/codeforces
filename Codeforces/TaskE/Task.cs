using System;
using System.Linq;

namespace Codeforces.TaskE
{
    public class Task
    {
        void Solve()
        {
            int n, k;
            Input.Next(out n, out k);
            var P = Input.ArrayInt().ToArray();
            var pos = new int[n];
            for (var i = 0; i < n; i++)
                pos[P[i]-1] = i;
            var bs = new bool[n];
            foreach (var b in Input.ArrayInt())
                bs[b-1] = true;
            var ft = new int[n];

            var w = 0L;
            var edges = new RedBlackTreeSimple<int>(n);
            edges.Add(-1);
            edges.Add(n);
            
            for (var i = 0; i < n; i++)
            {
                var pi = pos[i];
                if (bs[i])
                {
                    edges.Add(pi);
                }
                else
                {
                    var bounds = edges.Bounds(pi);
                    var l = bounds.Item1;
                    var r = bounds.Item2;
                    w += r - l - 1;
                    for (var f = r - 1; f >= 0; f = (f & (f + 1)) - 1)
                        w -= ft[f];
                    for (var f = l; f >= 0; f = (f & (f + 1)) - 1)
                        w += ft[f];
                    for (var f = pi; f < n; f = (f | (f + 1)))
                        ft[f]++;
                }
            }

            Console.Write(w);
        }

        public static void Main()
        {
            var task = new Task();
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
    }

}
