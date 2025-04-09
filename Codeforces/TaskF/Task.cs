using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskF
{
    public class Task
    {
        void Solve()
        {
            /*
            var heap = new BinaryHeap<int>(100);
            var rand = new Random();
            for (int i = 0; i < 100; ++i)
            {
                heap.Add(rand.Next(100) + 1);
            }
            int prev = -1;
            for (int i = 0; i < 100; ++i)
            {
                int v =  heap.Get();
                if (v < prev)
                {
                    Console.WriteLine("Bad");
                }
                prev = v;
                heap.Remove();
            }
            */
            int t = int.Parse(Console.ReadLine());
            while (t-- > 0)
            {
                int n = int.Parse(Console.ReadLine());
                int[] A = ReadArray();
                int[] C = ReadArray();

                var ok = Solve(A, C, n);

                if (ok)
                {
                    for(int i=1;i<=n;++i)
                    {
                        Console.Write(C[i]);
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(-1);
                }

            }
        }

        private int[] ReadArray()
        {
            var list = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            list.Insert(0, -1); // 1-based offset
            return list.ToArray();
        }

        private bool Solve(int[] a, int[] b, int n)
        {
            int[] posA = new int[n + 1];
            int[] posB = new int[n + 1];

            for (int i = 1; i <= n; ++i)
            {
                posA[a[i]] = i;
                if (b[i] != 0)
                {
                    posB[b[i]] = i;
                }
            }

            int[] copy = new int[n + 1];
            for(int i=0;i<b.Length;++i)
            {
                copy[i] = b[i];
            }

            // validate 
            if (!IsValid(n, a, posA, posB))
                return false;

            int[] l = GetLeft(a, n, posB);  // v => min pos
            int[] r = GetRight(a, n, posB); // v => max pos

            List<int> positions = new List<int>();
            for(int i=1;i<=n;++i)
            {
                if (b[i] == 0)
                {
                    positions.Add(i);
                }
            }

            List<Node> nodes = new List<Node>();
            for (int v=1;v<=n;v++)
            {
                if (posB[v] == 0)
                {
                    if (l[v] > r[v])
                    {
                        return false;
                    }
                    nodes.Add(new Node { V = v, L = l[v], R = r[v], Pos = posA[v] });
                }
            }
            nodes = nodes.OrderBy(c => c.L).ToList();

            int index = 0;
            BinaryHeap<Node> leftNodes = new BinaryHeap<Node>(n+1);
            foreach(var i in positions)
            {
                for(;index<nodes.Count;++index)
                {
                    if (nodes[index].L > i) break;
                    leftNodes.Add(nodes[index]);
                }
                if (leftNodes.Count == 0)
                {
                    return false;
                }
                var node = leftNodes.Get();
                leftNodes.Remove();
                if (node.R < i)
                {
                    return false;
                }
                b[i] = node.V;
            }

            //Validate(n, a, b, copy);

            return true;
        }

        class Node : IComparable
        {
            public int L { get; set; }
            public int R { get; set; }
            public int V { get; set; }
            public int Pos { get; set; }

            public int CompareTo(object other)
            {
                Node o = (Node)other;
                if (R == o.R)
                {
                    if (L == o.L)
                    {
                        return Pos - o.Pos;
                    }
                    return L - o.L;
                }
                return R - o.R;
            }

            /*
                //    if (x.R == y.R)
                //    {
                //        if (x.L == y.L)
                //        {
                //            return y.Pos - x.Pos;
                //        }
                //        return y.L - x.L;
                //    }
                //    return y.R - x.R;             
             */

            public override string ToString()
            {
                return $"L={L} R={R} V={V} Pos={Pos}";
            }
        }

        private static int[] GetRight(int[] a, int n, int[] posB)
        {
            var r = new int[n + 1];
            var right = new FenwickMinRight(n);
            for (int i = n; i > 0; --i)
            {
                var A = a[i];
                if (posB[A] == 0)
                {
                    r[A] = right.Min(A)-1;
                }
                else
                {
                    right.Update(A, posB[A]);
                }
            }

            return r;
        }

        private static int[] GetLeft(int[] a, int n, int[] posB)
        {
            var left = new FenwickMaxLeft(n);
            var l = new int[n + 1];
            for (int i = 1; i <= n; ++i)
            {
                var A = a[i];
                if (posB[A] == 0)
                {
                    l[A] = left.Max(A)+1;
                }
                else
                {
                    left.Update(A, posB[A]);
                }
            }
            return l;
        }

        private static bool IsValid(int n, int[] a, int[] posA, int[] posB)
        {
            FenwickMaxLeft tree = new FenwickMaxLeft(n);
            for (int v = 1; v <= n; ++v)
            {
                if (posB[v] != 0)
                {
                    if (tree.Max(posA[v]) > posB[v])
                    {
                        return false;
                    }
                    tree.Update(posA[v], posB[v]);
                }
            }
            return true;
        }


        private static void Validate(int n, int[] a, int[] b, int[] c)
        {
            int[] posA = new int[n + 1];
            int[] posB = new int[n + 1];
            for(int i=1;i<=n;i++)
            {
                posA[a[i]] = i;
                posB[b[i]] = i;
            }

            FenwickMaxLeft tree = new FenwickMaxLeft(n);
            for (int v = 1; v <= n; ++v)
            {
                if (posB[v] != 0)
                {
                    if (tree.Max(posA[v]) > posB[v])
                    {
                        var sa = string.Join(",", a);
                        var sb = string.Join(",", b);
                        var sc = string.Join(",", c);
                        Console.WriteLine("Bad:" + sa + ":" + sc + ":" + sb);
                    }
                    tree.Update(posA[v], posB[v]);
                }
            }
        }


        public class FenwickMinRight
        {
            private readonly int _maxValue;
            public readonly int N;
            private readonly int[] _items;

            public FenwickMinRight(int n, int? maxValue = null)
            {
                _maxValue = maxValue ?? n + 1;
                N = n;
                _items = new int[N + 1];
                for (int i = 0; i < _items.Length; ++i)
                {
                    _items[i] = _maxValue;
                }
            }

            public int Min(int index)
            {
                int result = _maxValue;
                for (int i = index; i <= N; i += i & (-i))
                {
                    result = Math.Min(result, _items[i]);
                }
                return result;
            }

            public void Update(int index, int value)
            {
                for (var i = index; i > 0; i -= i & (-i))
                {
                    _items[i] = Math.Min(_items[i], value);
                }
            }
        }

        public class FenwickMaxLeft
        {
            private readonly int _minValue;
            public readonly int N;
            private readonly int[] _items;

            public FenwickMaxLeft(int n, int minValue = 0)
            {
                _minValue = minValue;
                N = n;
                _items = new int[N + 1];
                if (_minValue != 0)
                {
                    for (int i = 0; i < _items.Length; ++i)
                    {
                        _items[i] = _minValue;
                    }
                }
            }

            public int Max(int index)
            {
                int result = _minValue;
                for (int i = index; i > 0; i -= i & (-i))
                {
                    result = Math.Max(result, _items[i]);
                }
                return result;
            }

            public void Update(int index, int value)
            {
                for (int i = index; i <= N; i += i & (-i))
                {
                    _items[i] = Math.Max(_items[i], value);
                }
            }
        }

        public class BinaryHeap<T> where T: IComparable
        {
            private T[] nodes;
            private int _count;

            public BinaryHeap(int n)
            {
                nodes = new T[n + 1];
            }

            public int Count => _count;

            public T Get()
            {
                return nodes[0];
            }

            public void Add(T value)
            {
                int i = _count++;
                nodes[i] = value;
                while(i > 0)
                {
                    int parent = (i - 1)>>1;
                    if (Bigger(parent, i))
                    {
                        Swap(i, parent);
                        i = parent;
                    } 
                    else
                    {
                        break;
                    }
                }
            }

            private void Swap(int i, int j)
            {
                var temp = nodes[j];
                nodes[j] = nodes[i];
                nodes[i] = temp;
            }

            public void Remove()
            {
                if (_count > 1)
                {
                    nodes[0] = nodes[_count - 1];
                }
                _count--;
                Heapify(0);
            }

            private void Heapify(int i)
            {
                if (i >= _count) return;
                int left = (i<<1) + 1;
                int right = left + 1;
                if (left >= _count && right >= _count) return;

                int j = i;

                if (left < _count && Bigger(j, left))
                {
                    j = left;
                }
                if (right < _count && Bigger(j, right))
                {
                    j = right;
                }

                
                if (i != j)
                {
                    Swap(i, j);
                    Heapify(j);
                }
            }

            private bool Bigger(int i, int j)
            {
                if (j >= _count) return true;
                if (i >= _count) return false;
                return nodes[i].CompareTo(nodes[j]) > 0;
            }

            class Node
            {
                public T value;
                public Node left;
                public Node right;
            }
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
