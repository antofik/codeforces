using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces
{
    public class Input
    {
        private static string _line;

        public static bool Next()
        {
            _line = Console.ReadLine();
            return !string.IsNullOrEmpty(_line);
        }

        public static bool Next(out long a)
        {
            var ok = Next();
            a = ok ? long.Parse(_line) : 0;
            return ok;
        }

        public static bool Next(out long a, out long b)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
            }
            else
            {
                a = b = 0;
            }

            return ok;
        }

        public static bool Next(out long a, out long b, out long c)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
            }
            else
            {
                a = b = c = 0;
            }
            return ok;
        }

        public static bool Next(out long a, out long b, out long c, out long d)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
            }
            else
            {
                a = b = c = d = 0;
            }
            return ok;
        }

        public static bool Next(out long a, out long b, out long c, out long d, out long e)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
                e = array[4];
            }
            else
            {
                a = b = c = d = e = 0;
            }
            return ok;
        }

        public static List<long> Numbers()
        {
            Next();
            return _line.Split(' ').Select(long.Parse).ToList();
        }

        public static bool Next(out string value)
        {
            value = string.Empty;
            if (!Next()) return false;
            value = _line;
            return true;
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