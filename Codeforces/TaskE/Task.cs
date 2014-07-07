using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codeforces.TaskE
{
    public class Task
    {
        void Solve()
        {
            int n, sx, sy, dx, dy, t;
            Input.Next(out n, out sx, out sy, out dx, out dy, out t);

            var v = new [] {sx, sy, dx, dy, 0, 1};

            var m1 = Matrix.Create(new[,] { { 2, 1, 1, 1, 0, 0 }, 
                                            { 1, 2, 1, 1, 0, 0 },
                                            { 1, 0, 1, 0, 0, 0 },
                                            { 0, 1, 0, 1, 0, 0 },
                                            { 0, 0, 1, 1, 1, 0 },
                                            { 0, 0, 0, 0, 1, 1 }
                                          });
            m1.Modulo = n;
            var m3 = m1.BinPower(2);
            var m2 = m1.BinPower(t-1);
            var u = v*m3;
            
            Console.WriteLine(u[0] + " " + u[1]);
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
