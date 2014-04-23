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
            var s = 1000000;
            var X = new HashSet<int>(Input.ArrayInt());
            var XX = new bool[s+1];
            foreach (var x in X)
                XX[x] = true;
            var Y = new List<int>();

            var j = 1;
            foreach(var x in X)
            {
                if (!XX[x]) continue;
                if (XX[s - x + 1])
                {
                    for(;j<=s/2;j++)
                        if (!XX[j] && !XX[s - j + 1])
                        {
                            Y.Add(j);
                            Y.Add(s - j + 1);
                            XX[s - x + 1] = false;
                            j++;
                            break;
                        }
                }
                else
                {
                    Y.Add(s - x + 1);
                }
            }
            Console.WriteLine(n);
            Console.WriteLine(string.Join(" ", Y));

        }
    }
}
