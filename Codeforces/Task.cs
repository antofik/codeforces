using System;
using System.Collections.Generic;
using System.Linq;
/*Library*/

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

        }
    }
}
