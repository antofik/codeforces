using System;

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
            long m, n;
            Input.Next(out m, out n);
            var t = new long[m+1][];
            for (var i = 0; i < m; i++)
                t[i] = Input.Numbers().ToArray();
            var r = new long[n][];
            for(var i=0;i<n;i++) 
                r[i] = new long[m];

            r[0][0] = t[0][0];
            for (var j = 1; j < m; j++)
                r[0][j] = r[0][j - 1] + t[j][0];

            for (var i = 1; i < n; i++)
            {
                r[i][0] = r[i-1][0] + t[0][i];
                for (var j = 1; j < m; j++)
                    r[i][j] = Math.Max(r[i][j - 1], r[i-1][j]) + t[j][i];
            }
            Console.WriteLine(string.Join(" ", r[n-1]));
        }
    }
}
