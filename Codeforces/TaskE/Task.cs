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
            int n;
            Input.Next(out n);
            var me = 0L;
            var he = 0L;
            var list = new List<int>();
            for (var i = 0; i < n; i++)
            {
                var cs = Input.ArrayInt().ToList();
                var count = cs[0];
                cs.RemoveAt(0);
                me += cs.Take(count / 2).Sum();
                he += cs.Skip(count - count / 2).Sum();
                if (count % 2 == 1)
                    list.Add(cs[count / 2]);
            }
            list.Sort();
            list.Reverse();

            var turn = true;
            for (var i = 0; i < list.Count; i++, turn ^= true)
            {
                if (turn)
                    me += list[i];
                else
                    he += list[i];
            }

            Console.WriteLine("{0} {1}", me, he);
        }
    }
}
