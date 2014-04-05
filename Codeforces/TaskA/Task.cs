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
            long n, x, y;
            Input.Next(out n, out x, out y);
            Console.WriteLine(Math.Max(0, Math.Ceiling(n*y/100.0) - x));
        }
    }
}
