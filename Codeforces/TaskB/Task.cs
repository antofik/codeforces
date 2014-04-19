using System;
using System.Collections.Generic;
using System.Linq;

/*Library*/

namespace Codeforces.TaskB
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
            long n, a, b;
            Input.Next(out n, out a, out b);
            var X = Input.Numbers();
            var result = new long[n];

            var k = 1d*a/b;

            for (var i = 0; i < n; i++)
            {
                var x = X[i];
                var r = (long)Math.Floor(x*k);
                var r2 = (long)Math.Ceiling(r/k);
                result[i] = x - r2;
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
