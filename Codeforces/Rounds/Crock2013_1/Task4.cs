using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Rounds.Crock2013_1
{
    public class Problem4
    {
        public static void Main()
        {
            var instance = new Problem4();
            instance.Solve();
        }

        private int N, M, K;
        private readonly List<int> _left = new List<int>();
        private readonly List<int> _right = new List<int>();
        private readonly List<DisjointSet> _bottom = new List<DisjointSet>();
        private readonly List<DisjointSet> _top = new List<DisjointSet>();

        public void Solve()
        {
            unchecked
            {
                var items = Console.ReadLine().Split();
                N = int.Parse(items[0]);
                M = int.Parse(items[1]);

                for (var i = 1; i <= M; i++)
                {
                    items = Console.ReadLine().Split();
                    _left.Add(int.Parse(items[0]) - 1);
                    _right.Add(int.Parse(items[1]) - 1);
                }

                {
                    var bottom = new DisjointSet(N, N);
                    _bottom.Add(bottom);
                    var top = new DisjointSet(N, N);
                    _top.Add(top);

                    for (var i = 0; i < M; i++)
                    {
                        bottom = bottom.Clone();
                        bottom[_left[i]] = _right[i];
                        _bottom.Add(bottom);
                    }
                    for (var i = M - 1; i >= 0; i--)
                    {
                        top = top.Clone();
                        top[_left[i]] = _right[i];
                        _top.Add(top);
                    }
                }

                var result = new List<int>();
                K = int.Parse(Console.ReadLine());
                for (var i = M + 2; i < M + K + 2; i++)
                {
                    items = Console.ReadLine().Split();
                    var l = int.Parse(items[0]) - 1;
                    var r = M - int.Parse(items[1]);
                    var bottom = _bottom[l].Clone();
                    var top = _top[r];
                    for (var j = 0; j < N; j++)
                    {
                        bottom[j] = top[j];
                    }

                    result.Add(bottom.GetSetCount());
                }

                Console.Write(string.Join("\n", result));
            }
        }
    }

    public class DisjointSet
    {
        private readonly int[] _parent;
        private readonly int[] _rank;
        public int Count { get; private set; }

        public DisjointSet(int count, int initialize = 0)
        {
            Count = count;
            _parent = new int[Count];
            _rank = new int[Count];
            for (var i = 0; i < initialize; i++) _parent[i] = i;
        }

        private DisjointSet(int count, int[] parent, int[] rank)
        {
            Count = count;
            _parent = parent;
            _rank = rank;
        }

        public void Add(int v)
        {
            _parent[v] = v;
            _rank[v] = 0;
        }

        public int Find_Recursive(int v)
        {
            if (_parent[v] == v) return v;
            return _parent[v] = Find(_parent[v]);
        }

        public int Find(int v)
        {
            if (_parent[v] == v) return v;
            var last = v;
            while (_parent[last] != last) last = _parent[last];
            while (_parent[v] != v)
            {
                var t = _parent[v];
                _parent[v] = last;
                v = t;
            }
            return last;
        }

        public int this[int v]
        {
            get { return Find(v); }
            set { Union(v, value); }
        }
        
        public void Union(int a, int b)
        {
            a = Find(a);
            b = Find(b);
            if (a == b) return;
            if (_rank[a] < _rank[b])
            {
                var t = _rank[a];
                _rank[a] = _rank[b];
                _rank[b] = t;
            }
            _parent[b] = a;
            if (_rank[a] == _rank[b]) _rank[a]++;
        }

        public int GetSetCount()
        {
            var result = 0;
            for (var i = 0; i < Count; i++)
            {
                if (_parent[i] == i) result++;
            }
            return result;
        }

        public DisjointSet Clone()
        {
            var rank = new int[Count];
            _rank.CopyTo(rank, 0);
            var parent = new int[Count];
            _parent.CopyTo(parent, 0);
            return new DisjointSet(Count, parent, rank);
        }

        public override string ToString()
        {
            return string.Join(",", _parent.Take(50));
        }
    }
}
