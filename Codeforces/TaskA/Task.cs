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
            task.Solve();
        }

        void Solve()
        {
            var items = new bool[101];
            int n;
            Input.Next(out n);
            int l, r;
            Input.Next(out l, out r);
            for (var i = l; i < r; i++)
                items[i] = true;
            while (--n > 0)
            {
                int ll, rr;
                Input.Next(out ll, out rr);
                for (var i = Math.Max(l, ll); i < rr && i < r; i++)
                    items[i] = false;
            }
            var result = 0;
            for (var i = l; i < r; i++)
                if (items[i])
                    result ++;
            Console.WriteLine(result);
        }
    }
}
