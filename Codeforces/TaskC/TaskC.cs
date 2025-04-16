using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private void Solve(int test)
        {
            Input.Next(out long n, out long m);
            long[] A = Input.ArrayLong();
            var counts = new long[n + m + 1];
            var started = new long[n + m + 1];

            for (int i=0;i<started.Length;++i)
            {
                started[i] = -1;
            }

            for (int i=1;i<=n;++i)
            {
                started[A[i]] = 0;
            }

            long ans = 0;
            long prev = 0;
            for(int j=1;j<=m;++j)
            {
                Input.Next(out long p, out long v);

                long u = A[p];
                A[p] = v;

                if (started[u] != -1)
                    counts[u] += j - started[u];
                started[u] = -1;

                if (started[v] != -1)
                    counts[v] += j - started[v];
                started[v] = j;

                ans += j * n;
                long countOld = counts[u];
                long countNew = counts[v];

                long bonus = prev - (j - countOld) + (j - countNew);
                ans += bonus;
                prev = bonus;
            }
            Console.WriteLine(ans);
        }

        private void Solve()
        {
            int T = int.Parse(Console.ReadLine()!);
            for (int t = 1; t <= T; ++t)
            {
                Solve(t);
            }
        }

        public static void Main()
        {
            var task = new TaskC();
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
