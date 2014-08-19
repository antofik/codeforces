using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            int n, m, k;
            Input.Next(out n, out m, out k);

            Console.WriteLine(m * (m-1) / 2);
            var output = new StringBuilder();
            for (var i = 1; i < m; i++)
            {
                for (var j = m; j > i; j--)
                {
                    if (k == 0) // up
                        output.Append(string.Format("{0} {1}\r\n", j-1, j));
                    else // down
                        output.Append(string.Format("{0} {1}\r\n", j, j-1));
                }
            }
            Console.Write(output);
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
