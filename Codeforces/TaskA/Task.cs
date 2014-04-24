using System;

/*Library*/

namespace Codeforces.TaskA
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
            int k, a, b, v;
            Input.Next(out k, out a, out b, out v);

            var count = 0;
            while (a > 0)
            {
                var d = Math.Min(b, k - 1);
                b -= d;
                var volume = (d + 1)*v;
                a -= volume;
                count++;
            }

            Console.WriteLine(count);
        }
    }
}
