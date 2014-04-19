using System;
using System.Collections.Generic;
using System.Linq;

/*Library*/

namespace Codeforces.TaskA
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
            long n, m;
            Input.Next(out n, out m);

            var B = Input.Numbers();

            var result = new long[n];

            var d = n + 1;
            foreach (var b in B)
            {
                if (b >= d) continue;
                for (var i = b; i < d; i++)
                    result[i - 1] = b;
                d = b;
            }
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
