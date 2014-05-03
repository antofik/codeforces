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
            Console.ReadLine();
            var line = Console.ReadLine();

            var n = 0L;
            for(var i=0;i<line.Length;i++)
                if (line[i] == 'R')
                    n += 1L << i;

            Console.WriteLine((1L << (line.Length)) - n - 1L);
        }
    }
}
