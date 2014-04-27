using System;
using System.Collections.Generic;
using System.Linq;

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
            var args = Console.ReadLine().Split();
            var x = int.Parse(args[0]);
            var k = int.Parse(args[1]);

            var list = new List<int>{x};
            for (var i = 0; i < k; i++)
            {
                var line = Console.ReadLine().Split();
                list.Add(int.Parse(line[1]));
                if (line.Count()>2) 
                    list.Add(int.Parse(line[2]));
            }

            list.Sort();

            var min = 0;
            var max = 0;
            var p = 0;
            foreach (var r in list)
            {
                var delta = r - p - 1;
                p = r;
                max += delta;
                min += (delta + 1)/2;
            }
            Console.WriteLine(min + " " + max);
        }
    }
}
