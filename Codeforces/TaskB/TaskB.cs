using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskB
    {
        private readonly long MOD = 1000_000_007;

        private void Solve(int test)
        {
            int n = Input.Int();
            long[] A = Input.ArrayLong();

            var ok = true;
            Output.YesNo(ok);
        }

        private void Solve()
        {
            int T = Input.Int();
            for (int t = 1; t <= T; ++t)
            {
                Solve(t);
            }
        }

        public static void Main()
        {
            var task = new TaskB();
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
