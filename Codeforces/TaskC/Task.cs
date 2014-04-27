using System;
using System.Text;

namespace Codeforces.TaskC
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
            var args = Console.ReadLine().Split();
            var zeros = long.Parse(args[0]);
            var ones = long.Parse(args[1]);

            if (ones < zeros - 1 || ones > (zeros + 1)*2)
            {
                Console.WriteLine(-1);
                return;
            }

            var output = new StringBuilder();

            var extra = Math.Max(0, ones - zeros*2);
            ones -= extra;
            if (extra > 0)
                output.Append(extra > 1 ? "11" : "1");

            var twos = ones - zeros;
            var i = 0;
            for (; i < twos; i++)
                output.Append("011");
            for (; i < zeros - 1; i++)
                output.Append("01");
            if (i==zeros-1)
                output.Append(ones > zeros - 1 ? "01" : "0");
            Console.WriteLine(output);
        }
    }
}
