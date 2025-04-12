using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private void Solve()
        {
            Input.Next(out int n, out int l, out int r);

            int count = r - l + 1;

            int[] counts = new int[] {
               count > 0 ? Math.Max(0, count - 1) / 3 + 1 : 0,
               count > 1 ? Math.Max(0, count - 2) / 3 + 1 : 0,
               count > 2 ? Math.Max(0, count - 3) / 3 + 1 : 0
            };

            int offset = l % 3;

            long c0 = counts[(3 + 0 - offset) % 3];
            long c1 = counts[(3 + 1 - offset) % 3];
            long c2 = counts[(3 + 2 - offset) % 3];

            long z0 = c0;
            long z1 = c1;
            long z2 = c2;

            long MOD = 1000_000_000 + 7;

            for (int i=2;i<=n;++i)
            {
                long x0 = ((z0 * c0 % MOD) + (z1 * c2 % MOD) + (z2 * c1 % MOD)) % MOD;
                long x1 = ((z0 * c1 % MOD) + (z1 * c0 % MOD) + (z2 * c2 % MOD)) % MOD;
                long x2 = ((z0 * c2 % MOD) + (z1 * c1 % MOD) + (z2 * c0 % MOD)) % MOD;

                z0 = x0;
                z1 = x1;
                z2 = x2;
            }

            Console.WriteLine(z0);
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
