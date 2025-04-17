using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private readonly long MOD = 1000_000_007;

        private void Solve()
        {
            string s = Console.ReadLine()!;
            long countA = 0;
            long countB = 0;
            bool lastA = false;
            long ans = 1;
            for(int i=0;i<s.Length;++i)
            {
                if (s[i] == 'a')
                {
                    countA++;
                } 
                else if (s[i] == 'b')
                {
                    if (countA > 0)
                    {
                        ans = (ans * (countA + 1)) % MOD;
                    }
                    countA = 0;
                }
            }
            if (countA > 0)
            {
                ans = (ans * (countA + 1)) % MOD;
            }
            ans--;
            Console.WriteLine(ans);
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
