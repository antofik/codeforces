using System;
using System.Linq;
using System.Text;

/*Library*/

namespace Codeforces.TaskB
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                throw;
            }
        }

        void Solve()
        {
            int N, K;
            Input.Next(out N, out K);
            var A = Input.ArrayInt().ToArray();

            var m = int.MaxValue;
            var j = 0;
            for (var k = 1000; k > 0; k--)
            {
                var counter = 0;
                for (var i = 0; i < N; i++)
                    if (A[i] != k + i*K)
                        counter++;
                if (counter < m)
                {
                    m = counter;
                    j = k;
                }
            }

            var output = new StringBuilder();
            output.AppendFormat("{0}\n", m);
            for (var i = 0; i < N; i++)
                if (A[i] > j + i*K)
                    output.AppendFormat("- {0} {1}\n", i + 1, A[i] - j - i*K);
                else if (A[i] < j + i*K)
                    output.AppendFormat("+ {0} {1}\n", i + 1, j + i * K - A[i]);
            Console.Write(output);
        }
    }
}
