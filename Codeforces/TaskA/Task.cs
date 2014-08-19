using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            int n;
            Input.Next(out n);
            Console.WriteLine(n % 2 == 0 ? n * n / 2 : (n * (n - 1) / 2 + (n + 1) / 2));

            if (n == 1)
            {
                Console.WriteLine("C");
                return;
            }

            var output = new StringBuilder();
            var chars = new string('C', n/2).ToCharArray();

            var line0 = string.Join(".", chars);
            var line1 = string.Join(".", chars);
            if (n%2 == 0)
            {
                line0 += ".";
                line1 = "." + line1;
            }
            else
            {
                line0 += ".C";
                line1 = "." + line1 + ".";
            }

            for (var i = 0; i < n; i++)
            {
                if (i%2 == 0)
                    output.Append(line0);
                else
                    output.Append(line1);
                output.Append("\r\n");
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
