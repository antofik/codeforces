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

        bool IsEqual(double d1, double d2)
        {
            return Math.Abs(d1 - d2) <= 0.00000001;
        }

        bool IsInteger(double d)
        {
            return IsEqual(Math.Round(d), d);
        }

        bool IsZero(double d)
        {
            return IsEqual(Math.Round(d), 0);
        }

        void Solve()
        {
            int a, b;
            Input.Next(out a, out b);

            var coef = 1d*b/a;

            var ok = false;
            for (var xa = -a; xa <= a; xa += 1)
            {
                var ya = Math.Sqrt(a*a - xa*xa);
                if (!IsInteger(ya)) continue;
                var xb = ya*coef;
                if (!IsInteger(xb)) continue;
                var yb = -xa*coef;
                if (!IsInteger(yb)) continue;
                if (IsZero(ya) || IsZero(xa) || IsZero(yb) || IsZero(xb)) continue;
                if (IsEqual(xa, xb) || IsEqual(ya, yb)) continue;
                Console.WriteLine("YES");
                Console.WriteLine("0 0");
                Console.WriteLine("{0} {1}", xa, ya);
                Console.WriteLine("{0} {1}", xb, yb);
                ok = true;
                break;
            }
            if (!ok) 
                Console.WriteLine("NO");
        }
    }
}
