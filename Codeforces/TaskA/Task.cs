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
            int n, p, k;
            Input.Next(out n, out p, out k);

            var l = new List<int>();
            for(var i=p-k;i<p;i++)
                if (i>=1 && i<=n)
                    l.Add(i);
            var r = new List<int>();
            for(var i=p+1;i<=p+k;i++)
                if (i>=1 && i<=n)
                    r.Add(i);

            var s = string.Join(" ", l) + " (" + p + ") " + string.Join(" ", r);
            
            if (l.Count>0 && l[0] != 1) s = "<< " + s;
            if (r.Count>0 && r.Last() != n) s = s + " >>";
            Console.WriteLine(s);
        }
    }
}
