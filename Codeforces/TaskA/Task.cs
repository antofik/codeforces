using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            Input.Next(out int c, out int d);
            Input.Next(out int n, out int m);
            Input.Next(out int k);

            // x*n + y >= m*n - k
            // min(x*c + y*d)

            if (m * n <= k) {
                Console.WriteLine(0);
                return;
            }
                        
            var dp = new int[n, m];

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
