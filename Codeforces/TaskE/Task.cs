using System;
using System.Globalization;
using System.Text;

namespace Codeforces.TaskE
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        void Solve()
        {
            var n = int.Parse(Console.ReadLine());
            var output = new StringBuilder();
            if (n == 5)
            {
                output.AppendLine("1 2 3");
                output.AppendLine("1 3 3");
                output.AppendLine("2 4 2");
                output.AppendLine("4 5 1");
                output.AppendLine("3 4");
                output.AppendLine("3 5");
            }
            else
            {
                var n_ = n/2;
                for (var i = 1; i <= n_; i++)
                    output.AppendLine(string.Format("{0} {1} 1", i, i + n_));
                for (var i = n_+1; i < n; i++)
                    output.AppendLine(string.Format("{0} {1} {2}", i, i + 1, 2*(i - n_) - 1));
                output.AppendLine("1 3");
                for (var i = 1; i < n_; i++)
                    output.AppendLine(string.Format("{0} {1}", i, i + 1));
            }
            Console.Write(output);
        }
    }
}
