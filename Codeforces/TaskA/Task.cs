using System;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            int n, c;
            Input.Next(out n, out c);
            var A = Input.ArrayInt().ToArray();
            Console.Write(Math.Max(0, Enumerable.Range(0, n - 1).Max(i => A[i] - A[i + 1]) - c));
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
