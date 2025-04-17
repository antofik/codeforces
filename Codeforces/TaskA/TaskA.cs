using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskA
    {
        private readonly long MOD = 1000_000_007;

        private void Solve()
        {
            int k = Input.Int();
            int[] C = new int[k + 1];
            int n = 0;
            for (int i = 1; i <= k; ++i)
            {
                C[i] = Input.Int();
                n += C[i];
            }

            var Cnk = Combinations.GetCombinations(1001, MOD);

            long ans = 1;
            long len = n;
            for(int i=k;i>0;i--)
            {
                ans = ans * Cnk[len-1, C[i]-1] % MOD;
                len -= C[i];
            }
            Console.WriteLine(ans);
        }

        private static long Count(int[] C, int ci, int length, long MOD)
        {
            long ans = 0;
            int c = C[ci];
            for (int j = 0; j < c; ++j)
            {
                ans += Count(C, ci - 1, length - 1 - j, MOD);
            }
            return ans;
        }

        public static void Main()
        {
            var task = new TaskA();
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
