using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        void Solve()
        {
            Input.Next(out string S);
            Input.Next(out string T);

            var dp = new int[S.Length + 1, T.Length + 1]; // best embedding S[0..si] => T[0..ti]
            for (var ti = 0; ti <= T.Length; ti++)
            {
                dp[0, ti] = T.Length;
            }

            for (var si = 1; si <= S.Length; si++)
            {
                var sc = S[si - 1];
                dp[si, 0] = T.Length + si;
                for (var ti = 1; ti <= T.Length; ti++)
                {
                    var tc = T[ti - 1];
                    var cost = tc == sc ? 0 : 1;
                    var left = dp[si - 1, ti - 1] - 1;                    
                    dp[si, ti] = Math.Min(left + cost, dp[si, ti-1]);                    
                }
            }
            var result = new List<string>();
            for (var si = S.Length; si >= 1; si--)
            {

            }

            Console.WriteLine(dp[S.Length, T.Length]);
        }

        public static void Main()
        {
            var task = new Task();
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
