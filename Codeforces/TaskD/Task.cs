using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;

/*Library*/

namespace Codeforces.TaskD
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
            long n, M;
            Input.Next(out n, out M);
            var str = n.ToString(CultureInfo.InvariantCulture);
            var a = str.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
            var N = str.Count();
            var setsCount = 1 << N;
            var counts = new int[10];
            
            var factor = 1L;
            foreach (var ch in a)
                factor *= ++counts[ch];

            var dp = new long[setsCount, M];

            for(var i=0;i<N;i++)
                if (a[i] != 0)
                    dp[1 << i, a[i] % M] = 1;

            for (var i = 1; i < setsCount; i++)
                for (var m = 0; m < M; m++)
                    if (dp[i,m] != 0)
                        for (var j = 0; j < N; j++)
                            if ((i & (1 << j)) == 0)
                                dp[i | (1 << j), (m*10 + a[j])%M] += dp[i, m];

            var result = dp[setsCount-1, 0]/factor;
            Console.WriteLine(result);
        }
    }
}
