using System;
using System.Text;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            Input.Next(out int n);
            var builder = new StringBuilder();
            for (var i = 0; i < n; i++)
            {
                Input.Next(out long p, out long q, out long b);
                if (p == 0) {
                    builder.AppendLine("Finite");
                    continue;
                }
                var gcd = Primes.Gcd(p, q);
                p /= gcd; q /= gcd;

                var ok = true;
                while (q > 1)
                {
                    gcd = Primes.Gcd(q, b);
                    if (gcd == 1) {
                        ok = false;
                        break;
                    }
                    q /= gcd;
                    b = gcd;
                }

                builder.AppendLine(ok ? "Finite" : "Infinite");
            }
            Console.WriteLine(builder.ToString());
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
