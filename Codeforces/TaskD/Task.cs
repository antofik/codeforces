using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        void Solve()
        {
            int n, m;
            Input.Next(out n, out m);
            var edges = new Tuple<int,int>[m];

            while (m-->0)
            {
                int from, to;
                Input.Next(out from, out to);
                edges[m] = new Tuple<int,int>(from-1, to-1);
            }

            var result = int.MaxValue;
            for (var i = 0; i < n; i++)
            {
                var g = new List<int>[n];
                for(var j=0;j<n;j++)
                    g[j] = new List<int>();
                var centerEdges = 0;
                var otherEdges = 0;
                foreach(var edge in edges)
                {
                    if (edge.Item1 == i || edge.Item2 == i)
                    {
                        centerEdges++;
                    }
                    else
                    {                        
                        g[edge.Item1].Add(edge.Item2);
                        otherEdges++;
                    }
                }

                var pairsCount = 0;
                var matching = GraphMatching.Kuhn(g, n, n);
                for (var j = 0; j < n; j++)
                    if (matching[j] != -1)
                        pairsCount++;

                var currentResult = 2 * (n - 1) + 1 - centerEdges + otherEdges - pairsCount + (n - 1) - pairsCount;

                if (currentResult < result)
                    result = currentResult;
            }

            Console.WriteLine(result);
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
                Console.WriteLine(ex);
            }
#endif
        }
    }
}
