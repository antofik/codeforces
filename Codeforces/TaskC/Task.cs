using System;
using System.Collections.Generic;
using System.Linq;
/*Library*/
using System.Text;

namespace Codeforces.TaskC
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
            var w = 2 + n/p; //edges from each vertex
            var last = k - w*(n - 1); //edges from last vertex
            for (var i = 0; i < n - 1; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    g[i, i + j + 1] = g[i + j + 1, i] = true;
                }
            }
        }
    }
}
