using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
/*Library*/

namespace Codeforces.TaskD
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

        private void Solve()
        {
            long n, l, k;
            Input.Next(out n, out l, out k);
            var p = Input.Numbers().Select(c => c/100d).ToList();
            p.Insert(0, 0);
            var a = Input.Numbers();
            for (var i = 0; i < n; i++) a[i]++;
            a.Insert(0, 0);

            var d = new double[n + 1, n + 1, n + 1]; //tour prises bugs
            d[0, 0, 0] = 1;

            for (var tour = 1; tour <= n; tour++)
            {
                for (var prise = 0; prise < tour; prise++)
                {
                    for (var bug = 0; bug <= n; bug++)
                    {
                        //win
                        d[tour, prise + 1, Math.Min(n, bug + a[tour])] += p[tour]*d[tour - 1, prise, bug];

                        //lose
                        d[tour, prise, bug] += (1 - p[tour])*d[tour - 1, prise, bug];
                    }
                }
            }

            var r = 0d;
            for (var prise = l; prise <= n; prise++)
            {
                for (var bug = Math.Max(0, prise - k); bug <= n; bug++)
                {
                    r += d[n, prise, bug];
                }
            }
            Console.WriteLine(r.ToString("F12", CultureInfo.InvariantCulture));
        }
    }
}