using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Library*/

namespace Codeforces.TaskA
{
    public class Task
    {
        private StringBuilder output = new StringBuilder();

        public static void Main()
        {
            var task = new Task();
            task.Solve();
            Console.Write(task.output);
        }

        void Solve()
        {
            int n;
            Input.Next(out n);
            var a = Input.ArrayInt().ToArray();
            var gcd = a[0];
            for (var i = 1; i < a.Length; i++)
            {
                gcd = Primes.Gcd(gcd, a[i]);
            }
            output.Append(gcd * n);
        }
    }
}
