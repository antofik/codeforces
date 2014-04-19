using System;
using System.Collections.Generic;
using System.Text;

/*Library*/

namespace Codeforces.TaskC
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                throw;
            }
        }

        void Solve()
        {
            long n, k;
            Input.Next(out n, out k);
            var count = n/2;
            if (k < count || count == 0 && k > 0)
            {
                Console.WriteLine("-1");
            }
            else
            {
                var output = new StringBuilder();

                int[] lp;
                List<int> pr;
                Primes.ImprovedSieveOfEratosthenes(1000000, out lp, out pr);

                var d = k - count + 1;
                var i = 0;
                if (d > 1)
                {
                    i += 2;
                    output.Append(d);
                    output.Append(" ");
                    output.Append(d * 2);
                    output.Append(" ");
                }
                var prev = 1000000000;
                for (; i < n - 1; i+=2)
                {
                    output.Append(prev--);
                    output.Append(" ");
                    output.Append(prev--);
                    output.Append(" ");
                }
                if (i < n) output.Append(prev);
                Console.WriteLine(output);
            }
        }
    }
}
