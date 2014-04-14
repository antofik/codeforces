using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            long t;
            Input.Next(out t);
            var P = new long[t];
            for (var i = 0; i < t; i++)
                Input.Next(out P[i]);

            List<int> pr;
            int[] lp;
            Primes.ImprovedSieveOfEratosthenes(100000, out lp, out pr);

            var d2 = new int[10];
            var d32 = new int[10, 10];
            var d33 = new int[10, 10];
            var d42 = new int[10, 10, 10];
            var d43 = new int[10, 10, 10];
            var d44 = new int[10, 10, 10];
            var d52 = new int[10, 10, 10, 10];
            var d53 = new int[10, 10, 10, 10];
            var d54 = new int[10, 10, 10, 10];
            var d55 = new int[10, 10, 10, 10];

            foreach (var p in pr)
            {
                var n5 = p%10;
                var n4 = p%100/10;
                var n3 = p%1000/100;
                var n2 = p%10000/1000;
                var n1 = p/10000;
                if (p < 100)
                    d2[n4]++;
                if (p < 1000)
                {
                    d32[n3, n5]++;
                    d33[n3, n4]++;
                }
                if (p < 10000)
                {
                    d42[n2, n4, n5]++;
                    d43[n2, n3, n5]++;
                    d44[n2, n3, n4]++;
                }
                d52[n1, n3, n4, n5]++;
                d53[n1, n2, n4, n5]++;
                d54[n1, n2, n3, n5]++;
                d55[n1, n2, n3, n4]++;
            }
            
            /*
             1  2  3  4  5
             2  *  a  b  c
             3  a  *  d  e
             4  b  d  *  f
             5  c  e  f  *
             */
            unchecked
            {
                foreach (var p in P)
                {
                    var result = 0;
                    if (p < 100)
                    {
                        result += d2[p%10];
                    }
                    else if (p < 1000)
                    {
                        foreach (var a in Enumerable.Range(0, 10))
                            result += d32[p%100/10, a]*d33[p%10, a];
                    }
                    else if (p < 10000)
                    {
                        foreach (var a in Enumerable.Range(0, 10))
                            foreach (var b in Enumerable.Range(0, 10))
                                foreach (var d in Enumerable.Range(0, 10))
                                    result += d42[p%1000/100, a, b]*d43[p%100/10, a, d]*d44[p%10, b, d];
                    }
                    else
                    {
                        var n1 = p%10000/1000;
                        var n2 = p%1000/100;
                        var n3 = p%100/10;
                        var n4 = p%10;

                        for(var a=0;a<10;a++)
                            for(var b=0;b<10;b++)
                                for(var c=0;c<10;c++)
                                    for(var d=0;d<10;d++)
                                        for(var e=0;e<10;e++)
                                            for(var f=0;f<10;f++)
                                                result += d52[n1, a, b, c]*d53[n2, a, d, e]*d54[n3, b, d, f]*d55[n4, c, e, f];
                    }
                    Console.WriteLine(result);
                }
            }
        }
    }
}
