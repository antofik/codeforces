using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskD
    {
        private void Solve()
        {
            Input.Next(out long n, out long k);

            long k1 = 1;
            long k2 = n * (n - 1) / 2;
            long k3 = n * (n - 1) * (n - 2) / 3;
            long k4 = n * (n - 1) * (n - 2) * (n - 3) / 4 / 3/ 2 * 9;

            long ans;
            if (k <= 1) ans = k1;
            else if (k == 2) ans = k2 + k1;
            else if (k == 3) ans = k3 + k2 + k1;
            else ans = k4 + k3 + k2 + k1;

            Output.Write(ans);
        }

        public static void Main()
        {
            var task = new TaskD();
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
