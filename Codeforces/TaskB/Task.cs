using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            int t;
            Input.Next(out t);
            while(t-->0)
            {
                int n, x;
                Input.Next(out n, out x);
                var A = Input.ArrayInt().OrderByDescending(a=>a).ToList();
                var K = A.Select(a =>
                {
                    // x, x+1, x+2...
                    // x/2, ... x-1


                    // x=10
                    // 10, 11, 12, ..=> 1
                    // 5, 6, .. 9 => 2
                    // 4 => 3
                    // 3 => 4
                    // 2 => 5
                    // 1 => 10

                    return 1 + (x-1) / a;
                }).GroupBy(c=>c).OrderBy(c=>c.Key).ToList();

                int ans = 0;

                int leftover = 0;
                foreach(var k in K)
                {
                    var required = k.Key;
                    var count = k.Count() + leftover;
                    ans += count / required;
                    leftover = count % required;
                }

                Console.WriteLine(ans);
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
