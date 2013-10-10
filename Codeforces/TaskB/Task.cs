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
            string s;
            Input.Next(out s);
            var k = s.ToCharArray().GroupBy(c => c).Count(c => c.Count()%2 == 1);
            Console.WriteLine((k%2 == 1 || k==0) ? "First" : "Second");
        }
    }
}
