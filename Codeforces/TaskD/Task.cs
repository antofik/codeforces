using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Library*/

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
#if DEBUG
            new Task().Solve();
#else
            try
            {
                var task = new Task();
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.StackTrace);
            }
#endif
        }

        private HashSet<int>[] z;
        private int[] colors;
        private HashSet<int> found; 

        private const int black = 0;
        private const int gray = 1;
        private const int white = 2;

        void dfs(int v)
        {
            colors[v] = gray;
            foreach (var u in z[v])
                if (colors[u] == black) 
                    dfs(u);
            
            colors[v] = white;
            found.Add(v);
        }

        void Solve()
        {
            int N, M, K;
            Input.Next(out N, out M, out K);
            var C = Input.ArrayInt().ToArray();

            var G = new Dictionary<int, int>[N];
            z = new HashSet<int>[N];
            for (var i = 0; i < N; i++)
            {
                G[i] = new Dictionary<int, int>();
                z[i] = new HashSet<int>();
            }
            colors = new int[N];

            const int MAX = 10000000;

            //read tree
            for (var i = 0; i < M; i++)
            {
                int u, v, x;
                Input.Next(out u, out v, out x);
                v--;
                u--;
                G[u][v] = G[u].ContainsKey(v) ? Math.Min(G[u][v], x) : x;
                G[v][u] = G[u][v];
                if (G[u][v] == 0)
                {
                    z[u].Add(v);
                    z[v].Add(u);
                }
            }

            //map indexes of Cs
            var indexes = new int[K];
            var map = new int[N];
            for (var t = 1; t < K; t++)
            {
                indexes[t] = indexes[t - 1] + C[t - 1];
                for (var i = 0; i < C[t]; i++)
                    map[indexes[t] + i] = t;
            }

            //clusterize with dfs
            found = new HashSet<int>();
            for (var k = 0; k < K; k++)
            {
                var start = indexes[k];
                if (!found.Contains(start))
                {
                    found.Clear();
                    Array.Clear(colors, 0, colors.Length);
                    dfs(start);
                }
                for(var j=0;j<C[k];j++)
                    if (!found.Contains(j + start))
                    {
                        Console.WriteLine("No");
                        return;
                    }
            }

            var g = new Dictionary<int, int>[K];
            for(var k=0;k<K;k++)
                g[k] = new Dictionary<int, int>();

            //create KxK tree
            for (var i = 0; i < N; i++)
            {
                var v = map[i];
                foreach (var p in G[i])
                {
                    var x = p.Value;
                    var u = map[p.Key];
                    g[v][u] = g[u][v] = g[u].ContainsKey(v) ? Math.Min(g[u][v], x) : x;
                }
            }

            //create pathes array
            var d = new int[K, K];
            for (var i = 0; i < K; i++)
                for (var j = i + 1; j < K; j++)
                    d[i, j] = d[j, i] = g[i].ContainsKey(j) ? g[i][j] : MAX;

            //find all minimal pathes
            for (var k = 0; k < K; k++)
                for(var i=0;i<K;i++)
                    for (var j = 0; j < K; j++)
                        if (d[i, j] > d[i, k] + d[k, j])
                            d[i, j] = d[i, k] + d[k, j];

            //output
            var output = new StringBuilder("Yes\r\n");
            for(var i=0;i<K;i++)
            {
                var costs = new List<int>();
                for (var j = 0; j < K; j++)
                    costs.Add(d[i, j] == MAX ? -1 : d[i, j]);
                output.AppendLine(string.Join(" ", costs));
            }

            Console.WriteLine(output);            

        }
    }
}
