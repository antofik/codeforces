using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        void Solve()
        {
            int t = int.Parse(Console.ReadLine());
            while (t-- > 0)
            {
                int[] a = Console.ReadLine().Split(' ').Select(x=> int.Parse(x)).ToArray();
                int n = a[0];
                int m = a[1];
                int k = a[2];

                int delta = n - m * k;

                int[] ans = new int[n];

                var period = Math.Max(k, n / (m + 1));

                for(int i=0;i<n;++i)
                {
                    ans[i] = i % period;
                }

                foreach(var x in ans) {
                    Console.Write(x);
                    Console.Write(" ");
                }
                Console.WriteLine();
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
