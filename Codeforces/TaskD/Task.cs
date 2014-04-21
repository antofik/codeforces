using System;
using System.Linq;
using System.Numerics;

/*Library*/

namespace Codeforces.TaskD
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
            var P = Input.ArrayInt().ToList();
            P.Insert(0, 0);
            var C = new BigInteger[n + 1];
            const long mod = 1000000007L;
            var result = new BigInteger(0);
            var prev = new BigInteger(1);
            for (var i = n; i >= 1; i--)
            {
                var current = prev*2;
                result = (result + current) % mod;
                C[P[i]] -= current/2;
                prev = C[i] + current;
            }
            Console.WriteLine(result);
        }
    }
}
