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
            var A = Input.ArrayInt().ToList();
            var m = A.Max();
            var r = new int[m]; 
            for (var i = 1; i <= m; i++)
                foreach (var x in A)
                    if (x >= i)
                        r[i-1]++;

            for(var i = 1; i <= n; i++)
            {
                A[i - 1] = 0;
                foreach(var x in r)
                    if (x > n-i)
                        A[i - 1]++;}
            
            Console.WriteLine(string.Join(" ", A));
        }
    }
}
