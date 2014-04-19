using System;
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

        private const int MAX = 1000000000;

        void Solve()
        {
            long n, m;
            Input.Next(out n, out m);
            var distance = new uint[n, n];
            var roads = new uint[n, n];

            unchecked
            {
                for (var i = 0; i < n; i++)
                {
                    for (var j = 0; j < n; j++)
                        roads[i, j] = distance[i, j] = MAX;
                    roads[i, i] = distance[i, i] = 0;
                }

                for (var i = 0; i < m; i++)
                {
                    long A, B, l;
                    Input.Next(out A, out B, out l);
                    uint a = (uint)A - 1;
                    uint b = (uint)B - 1;
                    roads[b, a] = roads[a, b] = distance[b, a] = distance[a, b] = (uint)l;
                }

                for (var k = 0; k < n; k++)
                    for (var j = 0; j < n; j++)
                        for (var i = 0; i < n; i++)
                            distance[j, i] = Math.Min(distance[j, i], distance[j, k] + distance[k, i]);

                var result = new long[n, n];
                var counts = new long[n];

                for (var i = 0; i < n; i++)
                {
                    Array.Clear(counts, 0, counts.Length);

                    for (var j = 0; j < n; j++)
                        for (var k = 0; k < n; k++)
                            if (j != k && distance[i, j] + roads[j, k] == distance[i, k] && distance[i, k] < MAX)
                                counts[k]++;

                    for (var j = 0; j < n; j++)
                        for (var k = 0; k < n; k++)
                            if (distance[i, j] + distance[j, k] == distance[i, k])
                                result[i, k] += counts[j];
                }

                var output = new StringBuilder();
                for (var i = 0; i < n; i++)
                    for (var j = i + 1; j < n; j++)
                    {
                        output.Append(result[i, j].ToString("D") + " ");
                    }
                Console.WriteLine(output);
            }
        }
    }
}
