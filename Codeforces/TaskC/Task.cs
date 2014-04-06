using System;
using System.Collections.Generic;
using System.Linq;
/*Library*/

namespace Codeforces.TaskC
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                throw;
            }
        }

        double Self(long a, long v, long d)
        {
            var d1 = v*v/(2d*a);
            return d1 > d ? Math.Sqrt(2d*d/a) : 1d*v/a + 1d*(d - d1)/v;
        }

        void Solve()
        {
            long n, a, d;
            Input.Next(out n, out a, out d);
            var t = new long[n];
            var v = new long[n];
            for (var i = 0; i < n; i++) Input.Next(out t[i], out v[i]);

            var f = new double[n];
            f[0] = t[0] + Self(a, v[0], d);

            for (var i = 1; i < n; i++)
            {
                var self = t[i] + Self(a, v[i], d);
                f[i] = Math.Max(f[i - 1], self);
            }
            Console.WriteLine(string.Join(Environment.NewLine, f.Select(c=>c.ToString("F10").Replace(',','.'))));
        }
    }
}
