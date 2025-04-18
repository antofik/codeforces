using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private readonly long MOD = 1000_000_007;

        private void Solve()
        {
            Input.Next(out int n, out int m);
            var Cnk = Combinations.GetCombinations(n + m + 1, MOD);
            long ans = 0;
            for(int i=1;i<=n;i++)
            {
                long countA = Cnk[i + m - 2, m - 1];
                long countB = Cnk[(n - i + 1) + m - 1, m];
                long count = countA * countB % MOD;
                ans = (ans + count) % MOD;
            }
            Output.Write(ans);
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
