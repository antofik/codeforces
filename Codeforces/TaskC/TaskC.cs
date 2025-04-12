using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class TaskC
    {
        private void Solve()
        {
            int n = int.Parse(Console.ReadLine()!);
            long MOD = 1000_000_000 + 7;
            int[] factorials = Combinations.GetFactorials(n, MOD);
            int result =(int)((factorials[n] - Power(2, n - 1, MOD) + MOD) % MOD);
            Console.WriteLine(result);
        }

        private long Power(long a, int n, long MOD)
        {
            long result = 1L;
            for(int i=0;i<n;++i)
            {
                result = result * a % MOD;
            }
            return result;
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
