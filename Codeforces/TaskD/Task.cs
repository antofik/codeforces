using System;
using System.Collections.Generic;
using System.Linq;

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
            long n;
            Input.Next(out n);
            var shoes = new Shoes[n];
            for (var i = 0; i < n; i++)
            {
                shoes[i].Index = i;
                Input.Next(out shoes[i].Cost, out shoes[i].Size);
            }
            shoes = shoes.OrderBy(c => c.Size).ToArray();
            var maxSize = shoes[n - 1].Size;

            long m;
            Input.Next(out m);
            var customers = new Customer[m];
            for (var i = 0; i < m; i++)
            {
                customers[i].Index = i;
                Input.Next(out customers[i].Money, out customers[i].Size);
            }
            var groups = customers.OrderBy(c=>c.Money).GroupBy(c=>c.Size).OrderBy(c => c.Key).ToList();

            var dp = new long[n + 1];
            var dp1 = new long[n + 1];
            bool b = true, b2 = true;  //shoes-before were bought
            bool a = false, a2 = false; //allowed to buy shoews-before
            var boughtBefore = -1;

            var g = 0;
            for (var i = 0; i < n; i++)
            {
                while (g < groups.Count && groups[g].Key < shoes[i].Size) g++;
                var bought = false;
                if (groups[g].Key == shoes[i].Size)
                {
                    if (groups[g].Any(c => c.Money >= shoes[i].Cost))
                    {
                        foreach (var customer in groups[g])
                        {
                            if (customer.Money < shoes[i].Cost) continue;

                            if (customer.Index != boughtBefore)
                            {
                                dp[i] = dp1[i - 1] + shoes[i].Cost;
                                bought = true;
                                break;
                            }
                            dp[i] = Math.Max(dp1[i - 1], dp[i - 1] + shoes[i].Cost);
                        }
                    }
                    else
                    {
                        dp[i] = dp1[i];
                    }
                }
                else
                {
                    dp[i] = dp1[i];
                }

                if (g == groups.Count - 1) break;
                g++;
                if (groups[g].Key == shoes[i].Size - 1)
                {
                    var money = 0;
                    foreach (var customer in groups[g])
                    {
                        if (customer.Money < shoes[i].Cost) break;

                        if (customer.Index != boughtBefore)
                        {
                            dp[i] = Math.Max(dp[i], dp1[i - 1] + shoes[i].Cost);
                        }
                        else
                        {
                            dp[i] = dp[i - 1] + shoes[i].Cost;
                        }
                        break;
                    }
                }
                
            }
        }
    }

    struct Customer
    {
        public long Size;
        public long Money;
        public int Index;
    }

    struct Shoes
    {
        public long Size;
        public long Cost;
        public int Index;
    }
}
