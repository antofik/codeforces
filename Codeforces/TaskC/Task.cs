using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            int n;
            Input.Next(out n);
            var A = Input.ArrayInt().ToArray();
            n = A.Length;

            // <= 0 1 =>

            var result = 0L;
            var list = new List<int> {0};

            var bad = 0L;
            for (var i = 0; i < n; i++)
            {
                if (A[i] == 1)
                {
                    bad++;
                }
                else
                {
                    result += bad;
                }
            }
            Console.WriteLine(result);
        }

        public static void Main()
        {
            var task = new Task();
            #if DEBUG
            task.Solve();
            #else
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
            #endif
        }
    }
}
