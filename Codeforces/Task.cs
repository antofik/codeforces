using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task/*#*/
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
            var line = Console.ReadLine() ?? "";
            var n = line.Length;
            var left = new List<int>();
            var right = new List<int>();
            var L = line[n - 1] == 'L' ? 'L' : 'R';
            var R = L == 'L' ? 'R' : 'L';

            if (n == 1)
            {
                Console.WriteLine(1);
                return;
            }

            var x = 0;
            var current = line[0];
            var list = current == R ? right : left;
            var direction = current == R ? 1 : -1;

            for (var i = 1; i < n; i++)
            {
                var ch = line[i];
                if (ch != current)
                {
                    list.Add(x);
                    current = ch;
                    list = current == R ? right : left;
                    direction = current == R ? 1 : -1;
                }
                x += direction;
            } 
            list.Add(x);

            var result = 0;
            Console.WriteLine(result);
        }
    }
}
