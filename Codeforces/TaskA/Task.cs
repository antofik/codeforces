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
            int n, x;
            Input.Next(out n, out x);
            Console.WriteLine(Math.Ceiling(1.0d * Math.Abs(Input.ArrayInt().Sum()) / x));

        }
    }
}
