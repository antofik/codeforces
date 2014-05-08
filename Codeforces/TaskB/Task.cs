using System;
using System.Linq;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            int n, m;
            Input.Next(out n, out m);
            var A = Input.ArrayInt().ToList();
            var B = Input.ArrayInt().ToList();
            A.Sort();
            B.Sort();
            A.Reverse();
            var result = 0;
            foreach (var a in A)
            {
                var ok = false;
                foreach (var b in B)
                {
                    if (b >= a)
                    {
                        ok = true;
                        B.Remove(b);
                        break;
                    }
                }
                if (!ok) result++;
            }
            Console.Write(result);
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
