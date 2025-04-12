using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private void Solve(int test)
        {
            Input.Next(out int n, out int m);
            long v = 0;
            for (int i=0;i<m;++i)
            {
                long l;
                long r;
                long x;
                Input.Next(out l, out r, out x);
                v |= x;
            }
            int MOD = 1000_000_007;
            long answer = Combinations.Power(2, n-1, MOD) * v % MOD;
            Console.WriteLine(answer);
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
