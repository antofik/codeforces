using System;

/*Library*/

namespace Codeforces.TaskB
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
            int t;
            Input.Next(out t);
            while (t-- > 0)
            {
                long l, r, n;
                Input.Next(out n, out l, out r);
                
                var stock = r - l;
                var count = n/l;
                var left = n - count*l;
                stock *= count;

                Console.WriteLine(stock >= left ? "Yes" : "No");
            }
        }
    }
}
