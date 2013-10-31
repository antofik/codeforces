using System;
using System.Collections.Generic;
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

    public static class ListExtensions
    {
        /// <summary>
        /// Assumes items are sorted (asc)
        /// Returns index of the last item, which is less or equal to given
        /// </summary>
        public static int FindLeftItem(this IList<long> items, long item, int l = 0)
        {
            var r = items.Count - 1;
            if (r < 0) return -1;
            if (items[l] > item) return -1;
            while (r > l)
            {
                var m = (l + r + 1)/2;
                if (items[m] <= item) l = m;
                else r = m - 1;
            }
            return l;
        }
        /// <summary>
        /// Assumes items are sorted (asc)
        /// Returns index of the first item, which is greater or equal to given
        /// </summary>
        public static int FindRightItem(this IList<long> items, long item)
        {
            var l = 0;
            var r = items.Count - 1;
            if (r < 0) return -1;
            if (items[r] < item) return -1;
            while (r > l)
            {
                var m = (l + r)/2;
                if (items[m] >= item) r = m;
                else l = m + 1;
            }
            return r;
        }
    }
}