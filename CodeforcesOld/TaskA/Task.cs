using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            int T = int.Parse(Console.ReadLine());
            for (int t = 1; t <= T; ++t)
            {
            }
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
