using System;
using System.Globalization;
using System.Threading;

namespace Codeforces.TaskB
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

        void Solve()
        {
            var args = Console.ReadLine().Split();
            var a = double.Parse(args[0], CultureInfo.InvariantCulture);
            var d = double.Parse(args[1], CultureInfo.InvariantCulture);

            var n = int.Parse(Console.ReadLine());
            var x = 0d;
            var y = 0d;

            var run = 4 * a;
            if (d >= run)
            {
                d -= ((int)Math.Floor(d / run)) * run;
                if (d >= run) d -= run;
            }

            for (var i = 1; i <= n; i++)
            {
                var distance = i * d;
                distance -= ((int)Math.Floor(distance / run)) * run;
                while (distance >= run) distance -= run;
                var side = (int)Math.Floor(distance / a);
                distance -= side * a;
                switch (side)
                {
                    case 0: x = distance; y = 0; break;
                    case 1: x = a; y = distance; break;
                    case 2: x = a - distance; y = a; break;
                    case 3: x = 0; y = a - distance; break;
                }
                Console.WriteLine("{0} {1}", x.ToString("F10", CultureInfo.InvariantCulture), y.ToString("F10", CultureInfo.InvariantCulture));
            }
        }
    }
}