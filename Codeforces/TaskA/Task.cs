using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            int t = int.Parse(Console.ReadLine());
            while(t-->0)
            {
                int n = int.Parse(Console.ReadLine());
                int[] nums = new int[n];
                bool ok = false;

                /*
                 * 100=
                          1   2 3 4     97 98 99 100
                          100 1 2 3 ... 96 97 98  99
                    3=
                    3 2 1
                      1 2
                    3 1 2+

                    
                 
                 */

                if (n % 2 == 1)
                {
                    ok = true;
                    for(int i=0;i<n;i++)
                    {
                        nums[i] = i;
                    }
                    nums[0] = n;
                }

                if (ok)
                {
                    foreach (var x in nums)
                    {
                        Console.Write(x);
                        Console.Write(" ");
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
