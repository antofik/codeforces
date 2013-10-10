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
            task.Solve();
        }

        void Solve()
        {
            long n, k;
            Input.Next(out n, out k);
            var a = Input.Numbers();

            var limit = a.OrderByDescending(c => c).Skip((int) (k - 1)).First();
            var count = a.Count(c => c > 0 && c >= limit);
            Console.WriteLine(count);
        }
    }
}
