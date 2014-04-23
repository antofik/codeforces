using System;
using System.Collections.Generic;

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

        private List<int>[] adjucent;
        private HashSet<int>[] blocked;

        int dfs(int v)
        {
            var adj = new List<int>();
            foreach(var u in adjucent[v])
                if (!blocked[v].Contains(u))
                {
                    adj.Add(u);
                    blocked[v].Add(u);
                    blocked[u].Add(v);
                }

            var unpaired = -1;
            foreach (var u in adj)
            {
                var p = dfs(u);
                if (p >= 0)
                {
                    Console.WriteLine("{0} {1} {2}", v + 1, u + 1, p + 1);
                }
                else
                {
                    if (unpaired == -1)
                        unpaired = u;
                    else
                    {
                        Console.WriteLine("{0} {1} {2}", u + 1, v + 1, unpaired + 1);
                        unpaired = -1;
                    }
                }
            }

            return unpaired;
        }

        void Solve()
        {
            int n, m;
            Input.Next(out n, out m);
            adjucent = new List<int>[n];
            blocked = new HashSet<int>[n];
            for (var i = 0; i < n; i++)
            {
                adjucent[i] = new List<int>();
                blocked[i] = new HashSet<int>();
            }

            for (var i = 0; i < m; i++)
            {
                int a, b;
                Input.Next(out a, out b);
                a--;
                b--;
                adjucent[a].Add(b);
                adjucent[b].Add(a);
            }

            if (m%2==0)
            {
                dfs(0);
            }
            else Console.WriteLine("No solution");
        }
    }
}
