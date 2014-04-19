using System;

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
            new Task().Solve();
        }

        void Solve()
        {
            var arr = Console.ReadLine().Split();
            var N = int.Parse(arr[0]);
            var K = int.Parse(arr[1]);
            const int mod = 1000000007;

            var d = new int[N + 1][];

            for (var n = 1; n <= N; n++)
            {
                d[n] = new int[K + 1];
                d[n][1] = 1;
            }

            unchecked
            {
            for (var k = 2; k <= K; k++)
                for (var n = 1; n <= N; n++)
                    for (var i = n; i <= N; i += n)
                        d[i][k] = (d[i][k] + d[n][k - 1]) % mod;
            }

            var result = 0;
            for (var i = 1; i <= N; i++)
                result = (result + d[i][K]) % mod;
            Console.WriteLine(result);
        }
    }
}
