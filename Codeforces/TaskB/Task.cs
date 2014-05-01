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
            task.Solve();
        }

        void Solve()
        {
            var n = int.Parse(Console.ReadLine().Split()[0]);
            var set = new HashSet<int>();
            for (var t = 0; t < n; t++)
            {
                var line = Console.ReadLine();
                var gnome = line.IndexOf('G');
                var candy = line.IndexOf('S');
                if (gnome > candy)
                {
                    Console.WriteLine(-1);
                    return;
                }
                if (gnome < candy)
                {
                    set.Add(candy - gnome);
                }
            }
            Console.WriteLine(set.Count);
        }
    }
}
