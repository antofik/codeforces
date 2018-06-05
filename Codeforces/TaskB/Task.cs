using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            Input.Next(out long n, out long p, out long q, out long r);
            var a = Input.ArrayLong().ToList();
            a.Insert(0, 0);
            var d = new long[n+10, 10];
            for (var i = 0; i < 10; i++) d[0, i] = long.MinValue;
            for (var i = 1; i <= n; i++)
            {
                d[i, 0] = Math.Max(d[i - 1, 0], a[i] * p);
                d[i, 1] = Math.Max(d[i - 1, 1], a[i] * q + d[i, 0]);
                d[i, 2] = Math.Max(d[i - 1, 2], a[i] * r + d[i, 1]);
            }
            Console.WriteLine(d[n, 2]);
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
