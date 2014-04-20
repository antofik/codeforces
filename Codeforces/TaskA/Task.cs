using System;
using System.Linq;

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
            int n;
            Input.Next(out n);
            Input.Next();
            var min = int.MaxValue;
            for (var i = 0; i < n; i++)
            {
                var m = Input.ArrayInt().ToArray();
                if (m.Any())
                {
                    var delay = m.Sum() + m.Count()*3;
                    min = Math.Min(min, delay);
                }
                else min = 0;
            }
            Console.WriteLine(min == int.MaxValue ? 0 : min * 5);
        }
    }
}
