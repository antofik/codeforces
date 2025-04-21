using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskD
    {
        private readonly long MOD = 998244353;

        private void Solve()
        {
            long n = Input.Long();

            long[] pow2 = new long[n + 100];
            long[] pow2rev = new long[n + 100];
            pow2[0] = 1;
            pow2[1] = 2;
            for (int i = 1; i <= n; ++i)
            {
                pow2[i] = pow2[i - 1] * 2 % MOD;
                pow2rev[i] = Mathex.Reverse(pow2[i], MOD);
                long g = pow2[i] * pow2rev[i] % MOD;
            }

            long[] dp = new long[n + 100];
            dp[0] = 0;
            dp[1] = pow2rev[1];

            double[] p = new double[n + 100];
            p[1] = 0.5;

            Solve(n, pow2, pow2rev, dp, p);
            Output.Write(dp[n]);
        }

        void Solve(long n, long[] pow2, long[] pow2rev, long[] dp, double[] p)
        {
            long ans = 0;
            double prob = 0;
            for (int i = 1; i <= n; i += 2)
            {
                if (dp[n-i] == 0)
                    Solve(n - i, pow2, pow2rev, dp, p);

                ans = (ans + pow2rev[i] * dp[n-i]) % MOD;
                prob += p[i] / Math.Pow(2, i);
            }

            dp[n] = ans;
            p[n] = prob;
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
