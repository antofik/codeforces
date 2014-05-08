using System;
using System.Collections.Generic;
using System.Linq;

/*Library*/

namespace Codeforces.TaskC
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
            int n;
            Input.Next(out n);
            var x = Input.ArrayInt().ToList();
            x.Sort();

            var result = 0;

            while (x.Count() > 0)
            {
                var weight = 0;
                foreach (var y in x.ToList())
                {
                    if (y < weight) continue;
                    x.Remove(y);
                    weight++;
                }
                result++;
            }

            Console.Write(result);
        }
    }
}
