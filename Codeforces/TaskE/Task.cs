using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskE
{
    public class Task
    {
        void Solve()
        {

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
