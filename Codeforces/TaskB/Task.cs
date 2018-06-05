using System;
using System.Linq;
using System.Text;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            Input.Next(out int n, out int k);
            var a = Input.ArrayInt().ToArray();
            var b = new int[n];
            var d = new int[n+1, k+1];
            for (var i = 0; i <= k; i++)
                d[0, i] = i < a[0] ? a[0] : i;

            for (var i = 1; i < n; i++)
            {
                for (var j = k; j >= 0; j--)
                {
                    var thisDay = j < a[i] ? a[i] : j;
                    var previousDay = Math.Max(0, k - thisDay);
                    d[i, j] = d[i - 1, previousDay] + thisDay;
                    if (j < k)
                        d[i, j] = Math.Min(d[i, j], d[i, j + 1]);
                }
            }

            Console.WriteLine(d[n-1, 0] - a.Sum());
            var output = new StringBuilder();
            var prev = 0;
            for (var i = 0; i < n; i++)
            {
                output.Append(d[i, 0] - prev).Append(' ');
                prev = d[i, 0];
            }
            Console.WriteLine(output);
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
