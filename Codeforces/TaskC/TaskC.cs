using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private void Solve(int test)
        {
            int n = Input.Int();
            long ans = 0;
            long[,] dp = new long[4, 20];

            dp[0b00, 0] = 1;
            dp[0b01, 0] = 0;
            dp[0b10, 0] = 0;
            dp[0b11, 0] = 0;

            int i = 0;
            while(n > 0)
            {
                i++;
                int d = n % 10;
                n = n / 10;
                dp[0b00, i] = dp[0b00, i - 1] * (d + 1) + dp[0b01, i - 1] * d;
                dp[0b01, i] = dp[0b10, i - 1] * (d + 1) + dp[0b11, i - 1] * d; //0-0 1-1 2-2 9-9
                dp[0b10, i] = dp[0b00, i - 1] * (9 - d) + dp[0b01, i - 1] * (10 - d);
                dp[0b11, i] = dp[0b10, i - 1] * (9 - d) + dp[0b11, i - 1] * (10 - d); //0-10 1-9 9-1
            }

            /*
            d=5
            0: 5+0, 4+1, ... 0+5 => d+1
            1: 9+6, 8+7, 7+8, 6+9 => 9-d

            10=9
            11=8
            18=1
            19=0

             */

            Output.Write(dp[0b00, i] - 2);
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
