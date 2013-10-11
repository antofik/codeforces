using System;
using System.Collections.Generic;
using System.Linq;
/*Library*/

namespace Codeforces.TaskE
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
            long n;
            Input.Next(out n);
            var m = Matrix.Create(4, 4);
            m.Modulo = 1000000000 + 7;
            m.Fill(1);
            m -= 1;
            m = m.BinPower(n);
            Console.WriteLine(m[0, 0]);
        }
    }
}
