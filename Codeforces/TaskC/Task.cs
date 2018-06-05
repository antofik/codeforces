using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        // AAAAaaAaaaaaAAaaaAAaaaa
        // AAAAAAAAAAAAAAaaaaaaaaa|A
        // AAAAAAAAAAAAAAAAAAAaaaaaa
        void Solve()
        {
            Input.Next(out string value);
            var d = new int[value.Length+10, 2];
            for (var i = 0; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    d[i + 1, 0] = d[i, 0]; // nothing changes
                    d[i + 1, 1] = Math.Min(d[i, 1], d[i, 0]) + 1; // lower last one
                }
                else
                {
                    d[i + 1, 0] = d[i, 0] + 1; // upper last one
                    d[i + 1, 1] = Math.Min(d[i, 1], d[i, 0]); // nothing changes
                }
            }
            Console.WriteLine(Math.Min(d[value.Length, 0], d[value.Length, 1]));
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
