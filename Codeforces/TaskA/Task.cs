using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            var endtime = DateTime.ParseExact(Console.ReadLine(), "HH:mm", CultureInfo.InvariantCulture);
            var time = DateTime.ParseExact(Console.ReadLine(), "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
            Console.WriteLine((endtime - time).ToString("HH:mm"));
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
