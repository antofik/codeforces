using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskA
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
                var A = Input.ArrayInt().ToList();
                var n0 = 3;
                var n1 = 1;
                var n2 = 2;
                var n3 = 1;
                var n5 = 1;
                var ok = false;
                int i = 0;
                foreach(var a in A)
                {
                    i++;
                    if (a == 0) n0--;
                    if (a == 1) n1--;
                    if (a == 2) n2--;
                    if (a == 3) n3--;
                    if (a == 5) n5--;

                    if (n0 <= 0 && n1 <= 0 && n2 <= 0 && n3 <= 0 && n5 <= 0) {
                        Console.WriteLine(i);
                        ok = true;
                        break;
                    }
                }
                if (!ok)
                {
                    Console.WriteLine(0);
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
