using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
/*Library*/

namespace Codeforces.TaskA
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
            var l = -2000000000L;
            var r = 2000000000L;

            long n;
            Input.Next(out n);

            for (var i = 0; i < n; i++)
            {
                string str;
                Input.Next(out str);
                var arr = str.Split();
                var op = arr[0];
                var y = long.Parse(arr[1]);
                var ok = arr[2] == "Y";
                if ((op == ">=" && ok) || (op == "<" && !ok))
                {
                    l = Math.Max(l, y);
                }
                else if ((op == ">" && ok) || (op == "<=" && !ok))
                {

                    l = Math.Max(l, y + 1);
                }
                else if ((op == "<=" && ok) || (op == ">" && !ok))
                {
                    r = Math.Min(r, y);
                }
                else if ((op == "<" && ok) || (op == ">=" && !ok))
                {
                    r = Math.Min(r, y - 1);
                }
                else Environment.Exit(-1);
            }

            Console.WriteLine(l <= r ? l.ToString(CultureInfo.InvariantCulture) : "Impossible");
        }
    }
}
