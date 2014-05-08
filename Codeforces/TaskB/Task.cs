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
            int n;
            Input.Next(out n);
            var map = new bool[n, n];
            for (var i = 0; i < n; i++)
            {
                string line;
                Input.Next(out line);
                for (var j = 0; j < n; j++)
                    if (line[j] == '#')
                        map[i, j] = true;
            }

            for (var i = 1; i < n - 1; i++)
                for (var j = 1; j < n - 1; j++)
                    if (map[i, j] && map[i + 1, j] && map[i, j + 1] && map[i - 1, j] && map[i, j - 1])
                        map[i, j] = map[i + 1, j] = map[i, j + 1] = map[i, j - 1] = map[i - 1, j] = false;

            var ok = true;
            for (var i = 0; i < n; i++)
                for (var j = 0; j < n; j++)
                    if (map[i, j])
                        ok = false;
            Console.Write(ok ? "YES" : "NO");
        }
    }
}
