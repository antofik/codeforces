using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Codeforces.Task
{
    public class Task
    {
        private static readonly Random Rand = new Random();

        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        void Solve()
        {

            var list = new List<int>(1000000);
            for(var i=0;i<1000000;i++) list.Add(Rand.Next(10000000));
            var a = list.ToArray();
            //var c = Sorting.MergeSort(0, list.Count, list);
            Array.Sort(a);
            Debugger.Break();
        }
    }
}
