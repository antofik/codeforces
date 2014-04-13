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
                unchecked
                {
                    task.Solve();
                }
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
            long n, k;
            Input.Next(out n, out k);
            var V = new List<long>[n];
            var parents = new Dictionary<long, long>();

            for (var i = 0; i < n; i++)
                V[i] = new List<long>();

            for (var i = 0; i < n - 1; i++)
            {
                long a, b;
                Input.Next(out a, out b);
                V[--a].Add(--b);
                V[b].Add(a);
            }

            var l = new List<long>((int)n+1);
            /* relax tree to vertex=0*/
            var stack = new Stack<long>((int) n);
            stack.Push(0);
            while (stack.Any())
            {
                var v = stack.Pop();
                l.Add(v);
                foreach (var u in V[v])
                {
                    V[u].Remove(v);
                    parents[u] = v;
                    stack.Push(u);
                }
            }

            var one = new long[n + 1, k + 1];
            for (var i = 0; i < n; i++)
                one[i, 0] = 1;

            stack.Clear();
            stack.Push(0);
            var result = 0L;
            unchecked
            {
                l.Reverse();
                foreach(var v in l)
                {
                    foreach (var u in V[v])
                        for (var i = 1; i <= k; i++)
                            one[v, i] += one[u, i - 1];

                    long s = 0;
                    foreach (var u in V[v])
                        for (var i = 1; i < k; i++)
                            s += one[u, i - 1]*(one[v, k - i] - one[u, k - i - 1]);
                    result += one[v, k] + s/2;
                }
            }

            Console.WriteLine(result);
        }
    }
}
