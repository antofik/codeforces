using System;
using System.Collections.Generic;
using System.Linq;

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
                int n;
                Input.Next(out n);
                var a = Console.ReadLine();
                var b = Console.ReadLine();
                int even = 0;
                int uneven = 0;
                for (int i = 0; i < n; i++)
                {
                    if (a[i] == '0')
                    {
                        if (i % 2 == 0)
                        {
                            uneven++;
                        }
                        else
                        {
                            even++;
                        }
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (b[i] == '0')
                    {
                        if (i % 2 == 0)
                        {
                            even++;
                        }
                        else
                        {
                            uneven++;
                        }
                    }
                }
                var requiredEven = n / 2;
                var requiredUneven = n / 2 + (n%2);
                var ok = even >= requiredEven && uneven >= requiredUneven;
                Console.WriteLine(ok ? "YES" : "NO");
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
