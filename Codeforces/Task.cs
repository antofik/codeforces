using System;
/*Library*/
using System.Collections.Generic;

namespace Codeforces.Task
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
            var tree = new RedBlackTree<int, Guid>();
            var rand = new Random();

            var l = new List<int>();
            for (var i = 0; i < 10000; i++)
            {
                var key = rand.Next(0, 10000);
                tree[key] = Guid.NewGuid();
                if (rand.Next(4)==1) l.Add(key);
            }

            foreach (var key in l)
            {
                tree.Remove(key);
            }

           // tree.Print();
        }
    }
}
