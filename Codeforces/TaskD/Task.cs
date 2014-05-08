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
            int k;
            Input.Next(out k);
            var max = 1001;
            var map = new bool[max][];
            for (var i = 0; i < max; i++)
                map[i] = new bool[max];

/*
 *   987,654,321
 * 
 *              1 - (3,4...10,11,12) - 
 * 
 */

            var start = 1;
            var end = 2;
            var v = 3;
            var countOfOnes = k.ToString().Length-1;
            var countOfTens = 0;
            while (k > 0)
            {
                var d = k % 10;
                k /= 10;
                if (d > 0)
                {
                    var current = start;
                    for (var i = 0; i < countOfTens; i++)
                    {
                        var next = v++;
                        for (var j = 0; j < 10; j++)
                        {
                            var middle = v++;
                            map[current][middle] = map[middle][current] = true;
                            map[next][middle] = map[middle][next] = true;
                        }
                        current = next;
                    }
                    for (var i = 0; i < countOfOnes; i++)
                    {
                        var next = v++;
                        var middle = v++;
                        map[current][middle] = map[middle][current] = true;
                        map[middle][next] = map[next][middle] = true;
                        current = next;
                    }
                    for (var i = 0; i < d; i++)
                    {
                        var middle = v++;
                        map[current][middle] = map[middle][current] = true;
                        map[middle][end] = map[end][middle] = true;
                    }
                }
                countOfOnes--;
                countOfTens++;
            }

            v--;
            Console.WriteLine(v);
            for (var i = 1; i <= v; i++)
                Console.WriteLine(string.Join("", map[i].Skip(1).Take(v).Select(c => c ? "Y" : "N")));
        }
    }
}
