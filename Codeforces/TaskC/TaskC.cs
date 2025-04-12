using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class TaskC
    {
        private void Solve(int test)
        {
            int n = Input.Int();
            int[] A = Input.ArrayInt();

            long MOD = 998244353;

            long c1 = 0;
            long c2 = 0;
            long c3 = 0;
            for (int i=1;i<=n;++i)
            {
                if (A[i] == 1)
                {
                    c1++;
                    c1 %= MOD;
                }
                else if (A[i] == 2)
                {
                    c2 = c2 * 2 + c1;                    
                    c2 %= MOD;
                }
                else if (A[i] == 3)
                {
                    c3 = c3 + c2;
                    c3 %= MOD;
                }

            }
            Console.WriteLine(c3);
        }

        private void Solve()
        {
            int T = int.Parse(Console.ReadLine()!);
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
