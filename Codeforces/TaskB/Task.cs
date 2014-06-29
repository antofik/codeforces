using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            string s;
            Input.Next(out s);
            var result = 0L;
            var delta = 0;
            while (true)
            {
                var i = s.IndexOf("bear", delta, StringComparison.Ordinal);
                if (i == -1) break;
                result += (i + 1 - delta) * (s.Length - i - 3);
                delta = i + 1;
            }
            Console.WriteLine(result);
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
