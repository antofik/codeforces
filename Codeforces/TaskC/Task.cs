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
                var a = new int[n];
                int c = 0;
                for(int i = 0; i < n; i++)
                {
                    var j = (i + i) % n;
                    if (a[j] == 0)
                    {
                        a[j] = i + 1;
                        c++;
                    }
                }
                if (c == n)
                {
                    foreach(var x in a)
                    {
                        Console.Write(x);
                        Console.Write(' ');
                    }
                    Console.WriteLine();
                } else
                {
                    Console.WriteLine(-1);
                }
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
