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
            int n;
            Input.Next(out n);
            string line;
            var x = ' ';
            var o = ' ';

            var ok = true;
            for (var i = 0; i < n; i++)
            {
                Input.Next(out line);
                if (i == 0)
                {
                    x = line[0];
                    o = line[1];
                    if (x == o)
                    {
                        ok = false;
                        break;
                    }
                }
                for (var j = 0; j < n; j++)
                {
                    var ch = line[j];
                    if (((j == i || j == n - i - 1) && ch != x) || ((j != i && j != n - i - 1) && ch != o))
                    {
                        ok = false;
                        break;
                    }
                }
                if (!ok) break;
            }
            Console.WriteLine(ok ? "YES" : "NO");
        }
    }
}
