using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Library*/

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
            int n, k;
            Input.Next(out n, out k);
            var _D = Input.ArrayInt().ToArray();
            var D = new List<node>();
            for (var i = 0; i < n; i++)
            {
                D.Add(new node { length = _D[i], index = i + 1 });
            }
            D = D.OrderBy(c => c).ToList();
            //D.Sort();

            var v = new List<node>[n];
            for (var i = 0; i < n; i++)
                v[i] = new List<node>();

            if (D[0].length != 0 || (n > 1 && D[1].length == 0))
            {
                Console.WriteLine(-1);
                return;
            }

            var m = 0;
            var output = new StringBuilder((n-1) + Environment.NewLine);
            var currentDistance = 0;
            var currentItem = D[0];
            var stack = new Stack<node>();
            var newStack = new Stack<node>();
            newStack.Push(currentItem);
            var count = 0;
            foreach (var item in D.Skip(1))
            {
                if (item.length > currentDistance)
                {
                    if (item.length == currentDistance + 1)
                    {
                        currentDistance++;
                        stack = newStack;
                        if (stack.Count == 0)
                        {
                            Console.WriteLine(-1);
                            return;
                        }
                        currentItem = stack.Pop();
                        count = currentDistance == 1 ? -1 : 0;
                        newStack = new Stack<node>();
                    }
                    else
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                }

                if (item.length == currentDistance)
                {
                    newStack.Push(item);
                    if (count == k - 1)
                    {
                        if (stack.Count == 0)
                        {
                            Console.WriteLine(-1);
                            return;
                        }
                        currentItem = stack.Pop();
                        count = 0;
                    }
                    count++;
                    output.AppendFormat("{0} {1}\n", currentItem.index, item.index);
                }
            }
            Console.WriteLine(output);
        }

        class node : IComparable<node>
        {
            public int length;
            public int index;
            public int CompareTo(node other)
            {
                return length.CompareTo(other.length);
            }
        }
    }
}