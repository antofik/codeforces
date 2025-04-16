using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private readonly long MOD = 998244353;

        private void Solve(int test)
        {
            int n = Input.Int();

        }

        private void Solve()
        {
            var C = Combinations.GetCombinations(60, MOD);
            var fact = Combinations.GetFactorials(60, MOD);

            var wins = new long[61];
            var losses = new long[61];
            var draws = new long[61];
            draws[0] = 1;
            wins[2] = 1;
            draws[2] = 1;

            for (int n=4;n<=60;n+=2)
            {
                /*
                 * n n-1  |        | win
                 * n      |   n-1  | win
                 *   n-1  | n      | reverse
                 *        | n n-1  | loss
                 * 
                 */

                wins[n] = (C[n - 1, n / 2 - 1] + losses[n-2]) % MOD;
                losses[n] = (C[n - 2, n / 2 - 2] + wins[n - 2]) % MOD;
                draws[n] = draws[n - 2];
            }

            int T = int.Parse(Console.ReadLine()!);
            for (int t = 1; t <= T; ++t)
            {
                int n = Input.Int();
                Console.WriteLine($"{wins[n]} {losses[n]} {draws[n]}");
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
