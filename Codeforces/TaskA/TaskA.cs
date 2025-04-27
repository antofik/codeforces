using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskA
    {
        private readonly long MOD = 1000_000_007;

        private void Solve(int test)
        {
            string s = Console.ReadLine()!;
            int[] counts = new int[11];
            for (int i = 0; i < s.Length; ++i)
            {
                int n = s[i] - '0';
                counts[n]++;
            }
            string r = "";
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 9 - i; j <= 10; ++j)
                {
                    if (counts[j] > 0)
                    {
                        counts[j]--;
                        r += j.ToString();
                        break;
                    }
                }
            }

            Output.Write(r);
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
