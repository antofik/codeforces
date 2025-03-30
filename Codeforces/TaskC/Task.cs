using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            int t;
            Input.Next(out t);
            while(t-->0)
            {
                int n;
                Input.Next(out n);
                var a = Input.ArrayLong().ToList();
                var even = a.Where(c => c % 2 == 0).ToList();
                var uneven = a.Where(c => c % 2 == 1).ToList();
                if (even.Count==0 || uneven.Count==0)
                {
                    Console.WriteLine(a.Max());
                    continue;
                }

                var v = even.Sum();
                v += uneven.Sum() - uneven.Count + 1;
                Console.WriteLine(v);
            }
        }

        public static void Main()
        {
            var task = new Task();
            #if DEBUG
            task.Solve();
            #else
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
            #endif
        }
    }
}
