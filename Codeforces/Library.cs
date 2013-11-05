﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            return !Next() ? new List<long>() : _line.Split(' ').Select(long.Parse).ToList();
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

    public class Matrix
    {
        public static Matrix Create(int i, int j)
        {
            var m = new Matrix(i, j);
            return m;
        }

        public static Matrix Create(int i)
        {
            var m = new Matrix(i, i);
            return m;
        }

        public long Modulo { get; set; }

        private readonly int _width;
        private readonly int _height;
        private readonly long[,] _data;
        public int Width{get { return _width; }}
        public int Height{get { return _height; }}

        private Matrix(int i, int j)
        {
            Modulo = 1000000000000000000L;
            _width = j;
            _height = i;
            _data = new long[_height, _width];
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Width != m2.Height) throw new InvalidDataException("m1.Width != m2.Height");
            var m = Create(m2.Width, m1.Height);
            m.Modulo = m1.Modulo;
            for (var i=0;i<m2.Width;i++)
                for (var j = 0; j < m1.Height; j++)
                {
                    for (var k = 0; k < m1.Width; k++)
                        m[j, i] += (m1[j, k]*m2[k, i]) % m1.Modulo;
                    m[j, i] %= m1.Modulo;
                }
            return m;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            var m = m1.Clone();
            for (var i=0;i<m2.Width;i++)
                for (var j = 0; j < m1.Height; j++)
                    m[j, i] = (m[j, i] + m2[j, i]) % m1.Modulo;
            return m;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            var m = m1.Clone();
            for (var i=0;i<m2.Width;i++)
                for (var j = 0; j < m1.Height; j++)
                    m[j, i] = (m[j, i] - m2[j, i]) % m1.Modulo;
            return m;
        }

        public static Matrix operator *(Matrix m1, long l)
        {
            var m = m1.Clone();
            for (var i=0;i<m1.Width;i++)
                for (var j=0;j<m1.Height;j++)
                        m[j, i] = (m[j, i] * l) % m.Modulo;
            return m;
        }

        public static Matrix operator *(long l, Matrix m1)
        {
            return m1*l;
        }

        public static Matrix operator +(Matrix m1, long l)
        {
            var m = m1.Clone();
            for (var i = 0; i < m1.Width; i++)
                    m[i, i] = (m[i, i] + l) % m.Modulo;
            return m;
        }

        public static Matrix operator +(long l, Matrix m1)
        {
            return m1 + l;
        }

        public static Matrix operator -(Matrix m1, long l)
        {
            return m1 + (-l);
        }

        public static Matrix operator -(long l, Matrix m1)
        {
            var m = m1.Clone() * -1;
            return m + l;
        }

        public Matrix BinPower(long l)
        {
            var n = 1;
            var m = Clone();
            var result = new Matrix(m.Height, m.Width) + 1;
            result.Modulo = m.Modulo;
            while (l != 0)
            {
                var i = l & ~(l - 1);
                l -= i;
                while (n < i)
                {
                    m = m*m;
                    n <<= 1;
                }
                result *= m;
            }
            return result;
        }

        public void Fill(long l)
        {
            l %= Modulo;
            for (var i = 0; i < _height; i++)
                for (var j = 0; j < _width; j++)
                    _data[i, j] = l;
        }

        public Matrix Clone()
        {
            var m = new Matrix(_width, _height);
            Array.Copy(_data, m._data, _data.Length);
            m.Modulo = Modulo;
            return m;
        }

        public long this[int i, int j]
        {
            get { return _data[i, j]; }
            set { _data[i, j] = value % Modulo; }
        }
    }

    public sealed class Graph<V, E>
    {
        /// <summary>
        /// Size of graph
        /// </summary>
        public readonly long N;

        private readonly SortedSet<long>[] _edges;
        private readonly Dictionary<Tuple<long, long>, E> _edgeInfo = new Dictionary<Tuple<long, long>, E>();
        private readonly Dictionary<long, V> _vertexInfo = new Dictionary<long, V>();

        public Graph(long n)
        {
            N = n;
            _edges = new SortedSet<long>[N];
            for (var i = 0; i < N; i++)
            {
                _edges[i] = new SortedSet<long>();
            }
        }

        /// <summary>
        /// Add edge between vertixes
        /// a,b - [0..N)
        /// </summary>
        public void Add(long a, long b)
        {
            _edges[a].Add(b);
            _edges[b].Add(a);
        }

        /// <summary>
        /// TRUE is exists edge from a to b
        /// </summary>
        public bool AreConnected(long a, long b)
        {
            return _edges[a].Contains(b);
        }

        /// <summary>
        /// Removes edge between a and b
        /// </summary>
        public void Remove(long a, long b)
        {
            _edges[a].Remove(b);
            _edges[b].Remove(a);
        }

        /// <summary>
        /// Removes all edges from vertex a
        /// </summary>
        public void ClearVertex(long a)
        {
            foreach (var b in _edges[a])
            {
                _edges[b].Remove(a);
            }
            _edges[a].Clear();
        }

        /// <summary>
        /// Returns count of edges for given vertex
        /// </summary>
        public long GetEdgeCount(long a)
        {
            return _edges[a].Count;
        }

        /// <summary>
        /// Returns vertexes, linked to given
        /// </summary>
        public SortedSet<long> GetLinkedVertexes(long a)
        {
            return _edges[a];
        }

        /// <summary>
        /// Returns vertex information 
        /// </summary>
        public V this[long v]
        {
            get
            {
                V info;
                if (!_vertexInfo.TryGetValue(v, out info))
                {
                    info = default(V);
                    _vertexInfo[v] = info;
                }
                return info;
            }
            set
            {
                _vertexInfo[v] = value;
            }
        }

        /// <summary>
        /// Returns edge information 
        /// </summary>
        public E this[long a, long b]
        {
            get
            {
                var key = a > b
                    ? new Tuple<long, long>(b, a)
                    : new Tuple<long, long>(a, b);

                E info;
                if (!_edgeInfo.TryGetValue(key, out info))
                {
                    info = default(E);
                    _edgeInfo[key] = info;
                }
                return info;
            }
            set
            {
                var key = a > b
                    ? new Tuple<long, long>(b, a)
                    : new Tuple<long, long>(a, b);
                _edgeInfo[key] = value;
            }
        }
    }
}