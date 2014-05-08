using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            int n, m;
            Input.Next(out n, out m);
            var A = Input.ArrayInt().ToArray();
            var B = Input.ArrayInt().ToArray();
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
