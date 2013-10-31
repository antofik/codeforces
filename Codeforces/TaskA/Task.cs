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

            var s = new long[n];
            var knights = new SortedSet<int>(Enumerable.Range(1, (int)n));

            for (var i = 0; i < m; i++)
            {
                long l, r, x;
                Input.Next(out l, out r, out x);

                var fighters = knights.GetViewBetween((int)l, (int)r).ToArray();

                foreach(var fighter in fighters)
                {
                    if (fighter == x) continue;
                    s[fighter - 1] = x;
                    knights.Remove(fighter);
                }
            }
            Console.WriteLine(string.Join(" ", Enumerable.Range(0, (int)n).Select(c=>s[c])));
        }
    }
}
