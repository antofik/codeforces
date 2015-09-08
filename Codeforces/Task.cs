using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task/*#*/
{
    public class Task
    {
        private List<int>[] tree;
        private byte[] colors;
        private int[] back;
        const int WHITE = 0;
        const int GREY = 1;
        const int BLACK = 2;
        private int n;
        private int m;
        private int q;


        void Solve()
        {
            Input.Next(out n, out m, out q);
            var W = new int[n];
            tree = new List<int>[n];
            colors = new byte[n];
            back = new int[n];
            for (var i = 0; i < n; i++)
            {
                back[i] = i;
                Input.Next(out W[i]);
                tree[i] = new List<int>();
            }
            
            for (var i = 0; i < m; i++)
            {
                int a, b;
                Input.Next(out a, out b);
                a--; b--;
                tree[a].Add(b);
                tree[b].Add(a);
            }

            Dfs(0);
        }

        private int real(int number)
        {
            while (back[number] != number) number = back[number];
            return number;
        }
        
        private int Dfs(int current)
        {
            var vertexes = tree[current];
            var collapse = -1;
            foreach(var b in vertexes.ToList())
            {
                if (colors[b] == WHITE)
                {
                    collapse = Dfs(b);
                    if (collapse == -1) // do not need to collapse
                    {

                    }
                    else
                    {
                        shouldCollapseCurrentItemIntoParent = collapse == current;
                    }
                }
                else
                {
                    back[real(current)] = b;
                }
            }
            return collapse;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
            #endif
        }
    }
}
