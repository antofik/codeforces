using System;

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                throw;
            }
        }

        void Solve()
        {
            var line = Console.ReadLine() ?? "";
            var n = line.Length;
            const long mod = 1000000007L;
            var dp = new long[n + 1, 5];  //  00/10  *1  01/11  *2  1*/2*/**
            dp[0, 0] = 1;
            dp[0, 2] = 1;
            for (var i = 1; i <= n; i++)
            {
                var ch = line[i - 1];
                if (ch == '*')
                {
                    dp[i, 0] = 0;
                    dp[i, 1] = 0;
                    dp[i, 2] = 0;
                    dp[i, 3] = 0;
                    dp[i, 4] = (dp[i - 1, 2] + dp[i - 1, 3] + dp[i - 1, 4])%mod;
                }
                else if (ch == '2')
                {
                    dp[i, 0] = 0;
                    dp[i, 1] = 0;
                    dp[i, 2] = 0;
                    dp[i, 3] = dp[i - 1, 4];
                    dp[i, 4] = 0;
                }
                else if (ch == '1')
                {
                    dp[i, 0] = 0;
                    dp[i, 1] = dp[i - 1, 4];
                    dp[i, 2] = (dp[i - 1, 0] + dp[i - 1, 1])%mod;
                    dp[i, 3] = 0;
                    dp[i, 4] = 0;
                }
                else if (ch == '0')
                {
                    dp[i, 0] = (dp[i - 1, 0] + dp[i - 1, 1])%mod;
                    dp[i, 1] = 0;
                    dp[i, 2] = 0;
                    dp[i, 3] = 0;
                    dp[i, 4] = 0;
                }
                else if (ch == '?')
                {
                    dp[i, 0] = (dp[i - 1, 0] + dp[i - 1, 1])%mod;
                    dp[i, 1] = dp[i - 1, 4];
                    dp[i, 2] = (dp[i - 1, 0] + dp[i - 1, 1])%mod;
                    dp[i, 3] = dp[i - 1, 4];
                    dp[i, 4] = (dp[i - 1, 2] + dp[i - 1, 3] + dp[i - 1, 4])%mod;
                }
            }
            Console.WriteLine((dp[n, 0] + dp[n, 1] + dp[n, 4])%mod);
        }
    }
}
