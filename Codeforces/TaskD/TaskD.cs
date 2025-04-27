using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Math;
using static Codeforces.Input;
using static Codeforces.Output;
using static Codeforces.Primes;
using static Codeforces.Combinations;


namespace Codeforces.Task
{
    public class TaskD
    {
        private readonly static long MOD = 1000_000_007;

        private long Solve(int test)
        {
            Next(out int n, out int m, out int k);
            int[] x = new int[k + 1];
            int[] y = new int[k + 1];
            for (int i = 0; i <= k; ++i)
            {
                Next(out x[i], out y[i]);
                x[i]--;
                y[i]--;
            }

            var graph = new Graph();

            void Add(int x1, int y1, int x2, int y2)
            {
                int a = x1 * m + y1;
                int b = x2 * m + y2;
                graph.AddEdge(a, b);
            }

            for (int i=1;i<=k;++i)
            {
                var dx = Abs(x[i] - x[i - 1]);
                var dy = Abs(y[i] - y[i - 1]);
                if (dx + dy != 2) return 0;

                if (dx > 0 && dy > 0) 
                {
                    Add(x[i], y[i - 1], x[i - 1], y[i]);
                } 
                else
                {
                    int xx = (x[i] + x[i - 1]) / 2;
                    int yy = (y[i] + y[i - 1]) / 2;
                    Add(xx, yy, xx, yy);
                }
            }

            long ans = 1;
            foreach(var subgraph in graph.Subgraphs())
            {

                if (subgraph.V == subgraph.E) // cycle
                {
                    if (subgraph.Loops == 0)
                        ans = ans * 2 % MOD;
                }
                else if (subgraph.V == subgraph.E + 1) // tree
                {
                    ans = ans * subgraph.V % MOD;
                }
                else
                {
                    return 0;
                }
            }

            return ans;
        }

        private void Solve()
        {
            int T = Int();
            for (int t = 1; t <= T; ++t)
            {
                Write(Solve(t));
            }
        }

        public static void Main()
        {
            var task = new TaskD();
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
