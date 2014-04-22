using System;
using System.Linq;

/*Library*/

namespace Codeforces.TaskE
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
            int n, m;
            Input.Next(out n, out m);

            const long mod = 1000000007L;
            var N = n + 100;
            var C = new long[N + 1, 110];
            for (var i = 0; i <= N; i++)
            {
                if (i < 110) C[i, i] = 1;
                C[i, 0] = 1;
                if (i == 0) continue;
                C[i, 1] = i;
                if (i < 110) C[i, i - 1] = i;
            }
            for (var i = 2; i < N; i++)
                for (var k = 1; k < Math.Min(i, 101); k++)
                    C[i, k] = (C[i - 1, k] + C[i - 1, k - 1]) % mod;

            var diff = new long[N + 1, 110];

            var A = Input.ArrayLong().ToArray();
            for (var i = 0; i < m; i++)
            {
                int l, r, k;
                Input.Next(out l, out r, out k);
                diff[l, k]++;
                for (var j = 0; j <= k; j++)
                    diff[r + 1, j] = (mod + diff[r + 1, j] - C[r - l + k - j, k - j])%mod;
            }

            var result = new long[n];

            for(var k=100;k>=0;k--)
                for (var i = 1; i <= n; i++)
                    diff[i, k] = ((diff[i, k] + diff[i - 1, k])%mod + diff[i, k + 1])%mod;

            for (var i = 1; i <= n; i++)
                result[i - 1] = (A[i - 1] + diff[i, 0])%mod;

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
