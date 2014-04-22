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
            var n = int.Parse(Console.ReadLine());
            var sum = 0;
            for (var i = 0; i < n; i++)
                if (Console.ReadLine()[i*2] == '1')
                    sum ^= 1;

            var output = new StringBuilder();
            var m = int.Parse(Console.ReadLine());
            for (var i = 0; i < m; i++)
                if (Console.ReadLine() == "3")
                    output.Append(sum);
                else
                    sum ^= 1;

            Console.WriteLine(output);
        }
    }
}
