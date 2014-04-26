using System;
using System.Text;

/*Library*/

namespace Codeforces.TaskC
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
            int t;
            Input.Next(out t);
            var output = new StringBuilder();
            for(var i=0;i<t;i++)
            {
                int n, p;
                Input.Next(out n, out p);
                Make(n, p, output);
            }
            Console.WriteLine(output);
        }

        private void Make(int n, int p, StringBuilder output)
        {
            var g = new bool[n, n]; //graph
            var k = 2*n + p; //total edges
            var w = 2*k/(n - 1); //edges from each vertex
            var last = k - w*(n - 1)/2; //edges left


            for(var j=0;j<n;j++)
            {
                for (var i = 1; i < n - j; i++)
                {
                    g[i + j, i - 1] = true;
                    k--;
                    if (k == 0) break;
                }
                if (k == 0) break;
            }

            /*for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j <= w && j < n; j++)
                {
                    g[i, j] = g[j, i] = true;
                }
                if (last > 0)
                {
                    last--;
                    g[i, w + 1] = g[w + 1, i] = true;
                }
            }*/
            for (var i = 1; i < n; i++)
            {
                for (var j = 0; j < i; j++)
                {
                   // output.Append(g[i, j] ? "X" : ".");
                    if (g[i, j])
                      output.AppendFormat("{0} {1}\n", i + 1, j + 1);
                }
                //output.AppendLine();
            }
    
        }
    }
}
