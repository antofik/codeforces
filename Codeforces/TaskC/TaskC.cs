using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private readonly long MOD = 998244353;

        private void Solve()
        {
            Input.Next(out long n, out long m, out long k);

            var Cnk = Combinations.GetCombinations((int)n + 1, MOD);
            var factorials = Combinations.GetFactorials((int)n + 1, MOD);
            long f = m;
            for (int i = 1; i <= k; ++i)
            {
                f = f * (m - 1) % MOD;
            }
            long ans = Cnk[n-1, k] * f % MOD;

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
