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
            long n, x, y, dx, dy, t;
            Input.Next(out n, out x, out y, out dx, out dy, out t);

            while (dx < 0) dx += n;
            while (dy < 0) dy += n;
            var v = new long[] {x-1, y-1, dx, dy, 0, 1};

            // k = x + y + t + 2
            // dx' = dx + k = dx + x + y + t + 2
            // dy' = dy + k = dy + x + y + t + 2
            // x'= x + dx' = 2x + y + dx + t + 2
            // y'= y + dy' = x + 2y + dy + t + 2
            // t' = t + 1

            // x=0 y=1 dx=0 dy=1 t=0 k=3 dx'=3 dy'=4
            // x=3 y=0 dx=3 dy=4 t=1 k=6 dx'=4 dy'=0
            // x=2 y=0 dx=1 dy=2 t=2



            var m1 = Matrix.Create(new[,] { { 2, 1, 1, 1, 0, 0 }, 
                                            { 1, 2, 1, 1, 0, 0 },
                                            { 1, 0, 1, 0, 0, 0 },
                                            { 0, 1, 0, 1, 0, 0 },
                                            { 1, 1, 1, 1, 1, 0 },
                                            { 2, 2, 2, 2, 1, 1 }
                                          });
            m1.Modulo = n;
            var u = v*m1.BinPower(t);
/*
            var u1 = v*m1;
            var u2 = u1*m1;
            var u3 = u2 * m1;
            var u4 = u3 * m1;
            var u5 = u4 * m1;
  */          
            Console.WriteLine(++u[0] + " " + ++u[1]);
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
