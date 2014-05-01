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
            var I = int.Parse(Console.ReadLine());
            var all = new List<int> {1, 2, 3, 4, 6, 12};
            all.Reverse();
            for (var t = 0; t < I; t++)
            {
                var line = Console.ReadLine();
                var solutions = new List<int>();
                foreach (var b in all)
                {
                    for (var i = 0; i < b; i++) //row
                    {
                        var a = 12/b;
                        var ch = line[i];
                        if (ch == 'O') continue;
                        var ok = true;
                        for (var j = 0; j < a; j++)
                        {
                            if (line[i + j*b] != ch)
                            {
                                ok = false;
                                break;
                            }
                        }
                        if (ok)
                        {
                            solutions.Add(b);
                            break;
                        }
                    }
                }
                Console.WriteLine(solutions.Count + " " + string.Join(" ", solutions.Select(a=>string.Format("{0}x{1}", 12/a, a))));
            }
        }
    }
}
