using System;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            int n, t;
            Input.Next(out n, out t);
            // 3 <= n <= 30000
            // 2 <= t <= n
            var A = Input.ArrayInt().ToList();
            var x = 1;
            while (x < t)
            {
                x += A[x - 1];
            }
            Console.WriteLine(x == t ? "YES" : "NO");
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
