using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private readonly long MOD = 1000_000_007;

        private void Solve(int test)
        {
            Input.Next(out int n, out int k);
            for(int i=1;i<=k;++i)
            {
                Input.Next(out int r, out int c);
                n--;
                if (r != c) n--;
            }

            long[] dp = new long[n+2];
            dp[0] = 1;
            dp[1] = 1;

            for(int i=2;i<=n;++i)
            {
                dp[i] = (dp[i-1] + 2 * (i-1) * dp[i - 2]) % MOD;
            }

            Output.Write(dp[n]);
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
