﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Codeforces
{
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
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

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
            for (var i = 0; i < m2.Width; i++)
                for (var j = 0; j < m1.Height; j++)
                {
                    for (var k = 0; k < m1.Width; k++)
                        m[j, i] += (m1[j, k] * m2[k, i]) % m1.Modulo;
                    m[j, i] %= m1.Modulo;
                }
            return m;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            var m = m1.Clone();
            for (var i = 0; i < m2.Width; i++)
                for (var j = 0; j < m1.Height; j++)
                    m[j, i] = (m[j, i] + m2[j, i]) % m1.Modulo;
            return m;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            var m = m1.Clone();
            for (var i = 0; i < m2.Width; i++)
                for (var j = 0; j < m1.Height; j++)
                    m[j, i] = (m[j, i] - m2[j, i]) % m1.Modulo;
            return m;
        }

        public static Matrix operator *(Matrix m1, long l)
        {
            var m = m1.Clone();
            for (var i = 0; i < m1.Width; i++)
                for (var j = 0; j < m1.Height; j++)
                    m[j, i] = (m[j, i] * l) % m.Modulo;
            return m;
        }

        public static Matrix operator *(long l, Matrix m1)
        {
            return m1 * l;
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
                    m = m * m;
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

    public static class BitFunctions
    {

        /// <summary>
        /// For Bit-matrix - transform it into diagonal form by Gauss algorithm
        /// </summary>
        public static void MatrixToDiagonalForm(List<BitArray> a)
        {
            if (a.Count == 0) return;
            int m = a[0].Count;

            int row = 0;
            for (int i = 0; i < m && row < a.Count; ++i)
            {
                if (!a[row][i]) // if diag[i] != 1, let's find next row with 1 and perform a swap
                {
                    for (int j = row + 1; j < a.Count; ++j)
                    {
                        if (a[j][i])
                        {
                            var tmp = a[row];
                            a[row] = a[j];
                            a[j] = tmp;
                            break;
                        }
                    }
                }
                if (a[row][i])
                {
                    for (int j = 0; j < a.Count; ++j)
                        if (row != j && a[j][i])
                            a[j].Xor(a[row]);
                    row++;
                }
            }
        }
    }

    public class RedBlackTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly List<Node> _items;
        private Node _root;
        private readonly IComparer<TKey> _comparer;

        public RedBlackTree(int capacity = 10)
        {
            _items = new List<Node>(capacity);
            _comparer = Comparer<TKey>.Default;
        }

        private Node _search(TKey key)
        {
            var node = _root;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return node;
                node = i > 0 ? node.Right : node.Left;
            }
            return null;
        }

        /// <summary>
        /// Returns lower or equal key
        /// </summary>
        public TKey Lower(TKey key, TKey notFound = default(TKey))
        {
            var node = _root;
            Node good = null;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return key;
                if (i > 0)
                {
                    good = node;
                    node = node.Right;
                }
                else
                    node = node.Left;
            }
            return good == null ? notFound : good.Key;
        }

        /// <summary>
        /// Returns higher or equal key
        /// </summary>
        public TKey Higher(TKey key, TKey notFound = default(TKey))
        {
            var node = _root;
            Node good = null;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return key;
                if (i < 0)
                {
                    good = node;
                    node = node.Left;
                }
                else
                    node = node.Right;
            }
            return good == null ? notFound : good.Key;
        }

        /// <summary>
        /// Returns minimum region, which encloses given key
        /// </summary>
        public Tuple<TKey, TKey> Bounds(TKey key, TKey notFound = default(TKey))
        {
            var node = _root;
            Node lower = null;
            Node higher = null;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return new Tuple<TKey, TKey>(key, key);
                if (i < 0)
                {
                    higher = node;
                    node = node.Left;
                }
                else
                {
                    lower = node;
                    node = node.Right;
                }
            }
            return new Tuple<TKey, TKey>(lower == null ? notFound : lower.Key, higher == null ? notFound : higher.Key);
        }

        public void Add(TKey key, TValue value)
        {
            var node = new Node { Key = key, Value = value, Red = true };
            _items.Add(node);
            if (_root == null)
            {
                _root = node;
                _root.Red = false;
                return;
            }
            var x = _root;
            Node y = null;
            while (x != null)
            {
                y = x;
                x = _comparer.Compare(key, x.Key) > 0 ? x.Right : x.Left;
            }
            node.Parent = y;
            if (y == null) _root = node;
            else if (_comparer.Compare(key, y.Key) > 0) y.Right = node;
            else y.Left = node;
            InsertFixup(node);
        }

        private void InsertFixup(Node node)
        {
            while (true)
            {
                var parent = node.Parent;
                if (parent == null || !parent.Red || parent.Parent == null) break;
                var grand = parent.Parent;

                if (parent == grand.Left)
                {
                    var uncle = grand.Right;
                    if (uncle != null && uncle.Red) /* parent and uncle both red: we could swap colors of them with grand */
                    {
                        parent.Red = false;
                        uncle.Red = false;
                        grand.Red = true;
                        node = grand; /* and continue fixing tree for grand */
                    }
                    else if (node == parent.Right) /* we rotate around the parent */
                    {
                        node = parent;
                        RotateLeft(node);
                    }
                    else /* rotate around grand and switch colors of grand and parent */
                    {
                        parent.Red = false;
                        grand.Red = true;
                        RotateRight(grand);
                        if (grand == _root) _root = parent;
                        break; /* and finish since tree is fixed */
                    }
                }
                else
                {
                    if (_root.Parent != null) Debugger.Break();

                    var uncle = grand.Left;
                    if (uncle != null && uncle.Red) /* parent and uncle both red: we could swap colors of them with grand */
                    {
                        parent.Red = false;
                        uncle.Red = false;
                        grand.Red = true;
                        node = grand; /* and continue fixing tree for grand */
                    }
                    else if (node == parent.Left) /* we rotate around the parent */
                    {
                        node = parent;
                        RotateRight(node);
                    }
                    else /* rotate around grand and switch colors of grand and parent */
                    {
                        parent.Red = false;
                        grand.Red = true;
                        RotateLeft(grand);
                        if (grand == _root) _root = parent;
                        break; /* and finish since tree is fixed */
                    }
                }
            }
            _root.Red = false;
        }

        private void RotateLeft(Node node)
        {
            if (node.Right == null)
                throw new NotSupportedException("Cannot rotate left: right node is missing");
            var parent = node.Parent;
            var right = node.Right;
            right.Parent = parent;
            if (parent != null)
            {
                if (parent.Left == node) parent.Left = right;
                else parent.Right = right;
            }
            node.Right = right.Left;
            if (node.Right != null)
                node.Right.Parent = node;
            right.Left = node;
            node.Parent = right;
        }

        private void RotateRight(Node node)
        {
            if (node.Left == null)
                throw new NotSupportedException("Cannot rotate right: left node is missing");
            var parent = node.Parent;
            var left = node.Left;
            left.Parent = parent;
            if (parent != null)
            {
                if (parent.Left == node) parent.Left = left;
                else parent.Right = left;
            }
            node.Left = left.Right;
            if (node.Left != null)
                node.Left.Parent = node;
            left.Right = node;
            node.Parent = left;
        }

        public void Remove(TKey key)
        {
            var node = _search(key);
            if (node == null) return;

            var deleted = node.Left == null || node.Right == null ? node : Next(node);
            var child = deleted.Left ?? deleted.Right;
            if (child != null)
                child.Parent = deleted.Parent;
            if (deleted.Parent == null) /* root */
                _root = child;
            else if (deleted == deleted.Parent.Left)
                deleted.Parent.Left = child;
            else
                deleted.Parent.Right = child;
            if (node != deleted)
            {
                node.Key = deleted.Key;
                node.Value = deleted.Value;
            }

            if (!deleted.Red)
            {
                DeleteFixup(child, deleted.Parent);
            }
        }

        private void DeleteFixup(Node node, Node parent)
        {
            while (parent != null && (node == null || !node.Red))
            {
                if (node == parent.Left)
                {
                    var brother = parent.Right;
                    if (brother.Red)
                    {
                        brother.Red = false;
                        parent.Red = true;
                        RotateLeft(parent);
                        brother = parent.Right;
                    }
                    if ((brother.Left == null || !brother.Left.Red) && (brother.Right == null || !brother.Right.Red))
                    {
                        node = parent.Red ? _root : parent;
                        brother.Red = true;
                        parent.Red = false;
                    }
                    else
                    {
                        if (brother.Right == null || !brother.Right.Red)
                        {
                            if (brother.Left != null)
                                brother.Left.Red = false;
                            brother.Red = true;
                            RotateRight(brother);
                            brother = parent.Right;
                        }
                        if (!brother.Right.Red) Debugger.Break();
                        brother.Red = parent.Red;
                        parent.Red = false;
                        if (brother.Right != null)
                            brother.Right.Red = false;
                        RotateLeft(parent);
                        node = _root;
                    }
                }
                else
                {
                    var brother = parent.Left;
                    if (brother.Red)
                    {
                        brother.Red = false;
                        parent.Red = true;
                        RotateRight(parent);
                        brother = parent.Left;
                    }
                    if ((brother.Right == null || !brother.Right.Red) && (brother.Left == null || !brother.Left.Red))
                    {
                        node = parent.Red ? _root : parent;
                        brother.Red = true;
                        parent.Red = false;
                    }
                    else
                    {
                        if (brother.Left == null || !brother.Left.Red)
                        {
                            if (brother.Right != null)
                                brother.Right.Red = false;
                            brother.Red = true;
                            RotateLeft(brother);
                            brother = parent.Left;
                        }
                        brother.Red = parent.Red;
                        parent.Red = false;
                        if (brother.Left != null)
                            brother.Left.Red = false;
                        RotateRight(parent);
                        node = _root;
                    }
                }
                parent = node.Parent;
            }

            node.Red = false;
        }

        public TValue this[TKey key]
        {
            get
            {
                var node = _search(key);
                return node == null ? default(TValue) : node.Value;
            }
            set
            {
                var node = _search(key);
                if (node == null) Add(key, value);
                else node.Value = value;
            }
        }

        public bool Check()
        {
            int count;
            return _root == null || _root.Check(out count);
        }

        private Node Minimum(Node node)
        {
            var result = node;
            while (result.Left != null) result = result.Left;
            return result;
        }

        private Node Maximum(Node node)
        {
            var result = node;
            while (result.Right != null) result = result.Right;
            return result;
        }

        private Node Next(Node node)
        {
            Node result;
            if (node.Right != null)
            {
                result = Minimum(node.Right);
            }
            else
            {
                while (node.Parent != null && node.Parent.Right == node)
                    node = node.Parent;
                result = node.Parent;
            }
            return result;
        }

        private Node Previous(Node node)
        {
            Node result;
            if (node.Left != null)
            {
                result = Maximum(node.Left);
            }
            else
            {
                while (node.Parent != null && node.Parent.Left == node)
                    node = node.Parent;
                result = node.Parent;
            }
            return result;
        }

        public void Print()
        {
            var node = Minimum(_root);
            while (node != null)
            {
                Console.Write(node.Red ? '*' : ' ');
                Console.Write(node.Key);
                Console.Write('\t');
                node = Next(node);
            }
        }

        #region Nested classes

        [DebuggerDisplay("{ToString()}")]
        private sealed class Node
        {
            public TKey Key;
            public TValue Value;
            public bool Red;

            public Node Parent;
            public Node Left;
            public Node Right;

            public bool Check(out int count)
            {
                count = 0;
                if (Red)
                {
                    if (Left != null && Left.Red) return false;
                    if (Right != null && Right.Red) return false;
                }
                var left = 0;
                var right = 0;
                if (Left != null && !Left.Check(out left)) return false;
                if (Right != null && !Right.Check(out right)) return false;
                count = left + (Red ? 0 : 1);
                if (left != right)
                {
                    ;
                }
                return left == right;
            }

            public override string ToString()
            {
                return ToShortString(2);
            }

            private string ToShortString(int depth = 0)
            {
                return depth == 0
                    ? string.Format("{0}{1}", Red ? "*" : "", Key)
                    : string.Format("({1}<{0}>{2})", ToShortString(), Left == null ? "null" : Left.ToShortString(depth - 1), Right == null ? "null" : Right.ToShortString(depth - 1));
            }
        }

        #endregion

        #region IEnumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public class RedBlackTreeSimple<TKey>
    {
        private readonly List<Node> _items;
        private Node _root;
        private readonly IComparer<TKey> _comparer;

        public RedBlackTreeSimple(int capacity = 10)
        {
            _items = new List<Node>(capacity);
            _comparer = Comparer<TKey>.Default;
        }

        private Node _search(TKey key)
        {
            var node = _root;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return node;
                node = i > 0 ? node.Right : node.Left;
            }
            return null;
        }

        /// <summary>
        /// Returns lower or equal key
        /// </summary>
        public TKey Lower(TKey key, TKey notFound = default(TKey))
        {
            var node = _root;
            Node good = null;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return key;
                if (i > 0)
                {
                    good = node;
                    node = node.Right;
                }
                else
                    node = node.Left;
            }
            return good == null ? notFound : good.Key;
        }

        /// <summary>
        /// Returns higher or equal key
        /// </summary>
        public TKey Higher(TKey key, TKey notFound = default(TKey))
        {
            var node = _root;
            Node good = null;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return key;
                if (i < 0)
                {
                    good = node;
                    node = node.Left;
                }
                else
                    node = node.Right;
            }
            return good == null ? notFound : good.Key;
        }

        /// <summary>
        /// Returns minimum region, which encloses given key
        /// </summary>
        public Tuple<TKey, TKey> Bounds(TKey key, TKey notFound = default(TKey))
        {
            var node = _root;
            Node lower = null;
            Node higher = null;
            while (node != null)
            {
                var i = _comparer.Compare(key, node.Key);
                if (i == 0) return new Tuple<TKey, TKey>(key, key);
                if (i < 0)
                {
                    higher = node;
                    node = node.Left;
                }
                else
                {
                    lower = node;
                    node = node.Right;
                }
            }
            return new Tuple<TKey, TKey>(lower == null ? notFound : lower.Key, higher == null ? notFound : higher.Key);
        }

        public void Add(TKey key)
        {
            var node = new Node(key);
            _items.Add(node);
            if (_root == null)
            {
                _root = node;
                _root.Red = false;
                return;
            }
            var x = _root;
            Node y = null;
            while (x != null)
            {
                y = x;
                x = _comparer.Compare(key, x.Key) > 0 ? x.Right : x.Left;
            }
            node.Parent = y;
            if (y == null) _root = node;
            else if (_comparer.Compare(key, y.Key) > 0) y.Right = node;
            else y.Left = node;
            InsertFixup(node);
        }

        private void InsertFixup(Node node)
        {
            while (true)
            {
                var parent = node.Parent;
                if (parent == null || !parent.Red || parent.Parent == null) break;
                var grand = parent.Parent;

                if (parent == grand.Left)
                {
                    var uncle = grand.Right;
                    if (uncle != null && uncle.Red) /* parent and uncle both red: we could swap colors of them with grand */
                    {
                        parent.Red = false;
                        uncle.Red = false;
                        grand.Red = true;
                        node = grand; /* and continue fixing tree for grand */
                    }
                    else if (node == parent.Right) /* we rotate around the parent */
                    {
                        node = parent;
                        RotateLeft(node);
                    }
                    else /* rotate around grand and switch colors of grand and parent */
                    {
                        parent.Red = false;
                        grand.Red = true;
                        RotateRight(grand);
                        if (grand == _root) _root = parent;
                        break; /* and finish since tree is fixed */
                    }
                }
                else
                {
                    if (_root.Parent != null) Debugger.Break();

                    var uncle = grand.Left;
                    if (uncle != null && uncle.Red) /* parent and uncle both red: we could swap colors of them with grand */
                    {
                        parent.Red = false;
                        uncle.Red = false;
                        grand.Red = true;
                        node = grand; /* and continue fixing tree for grand */
                    }
                    else if (node == parent.Left) /* we rotate around the parent */
                    {
                        node = parent;
                        RotateRight(node);
                    }
                    else /* rotate around grand and switch colors of grand and parent */
                    {
                        parent.Red = false;
                        grand.Red = true;
                        RotateLeft(grand);
                        if (grand == _root) _root = parent;
                        break; /* and finish since tree is fixed */
                    }
                }
            }
            _root.Red = false;
        }

        private void RotateLeft(Node node)
        {
            if (node.Right == null)
                throw new NotSupportedException("Cannot rotate left: right node is missing");
            var parent = node.Parent;
            var right = node.Right;
            right.Parent = parent;
            if (parent != null)
            {
                if (parent.Left == node) parent.Left = right;
                else parent.Right = right;
            }
            node.Right = right.Left;
            if (node.Right != null)
                node.Right.Parent = node;
            right.Left = node;
            node.Parent = right;
        }

        private void RotateRight(Node node)
        {
            if (node.Left == null)
                throw new NotSupportedException("Cannot rotate right: left node is missing");
            var parent = node.Parent;
            var left = node.Left;
            left.Parent = parent;
            if (parent != null)
            {
                if (parent.Left == node) parent.Left = left;
                else parent.Right = left;
            }
            node.Left = left.Right;
            if (node.Left != null)
                node.Left.Parent = node;
            left.Right = node;
            node.Parent = left;
        }

        public void Remove(TKey key)
        {
            var node = _search(key);
            if (node == null) return;

            var deleted = node.Left == null || node.Right == null ? node : Next(node);
            var child = deleted.Left ?? deleted.Right;
            if (child != null)
                child.Parent = deleted.Parent;
            if (deleted.Parent == null) /* root */
                _root = child;
            else if (deleted == deleted.Parent.Left)
                deleted.Parent.Left = child;
            else
                deleted.Parent.Right = child;
            if (node != deleted)
            {
                node.Key = deleted.Key;
            }

            if (!deleted.Red)
            {
                DeleteFixup(child, deleted.Parent);
            }
        }

        private void DeleteFixup(Node node, Node parent)
        {
            while (parent != null && (node == null || !node.Red))
            {
                if (node == parent.Left)
                {
                    var brother = parent.Right;
                    if (brother.Red)
                    {
                        brother.Red = false;
                        parent.Red = true;
                        RotateLeft(parent);
                        brother = parent.Right;
                    }
                    if ((brother.Left == null || !brother.Left.Red) && (brother.Right == null || !brother.Right.Red))
                    {
                        node = parent.Red ? _root : parent;
                        brother.Red = true;
                        parent.Red = false;
                    }
                    else
                    {
                        if (brother.Right == null || !brother.Right.Red)
                        {
                            if (brother.Left != null)
                                brother.Left.Red = false;
                            brother.Red = true;
                            RotateRight(brother);
                            brother = parent.Right;
                        }
                        if (!brother.Right.Red) Debugger.Break();
                        brother.Red = parent.Red;
                        parent.Red = false;
                        if (brother.Right != null)
                            brother.Right.Red = false;
                        RotateLeft(parent);
                        node = _root;
                    }
                }
                else
                {
                    var brother = parent.Left;
                    if (brother.Red)
                    {
                        brother.Red = false;
                        parent.Red = true;
                        RotateRight(parent);
                        brother = parent.Left;
                    }
                    if ((brother.Right == null || !brother.Right.Red) && (brother.Left == null || !brother.Left.Red))
                    {
                        node = parent.Red ? _root : parent;
                        brother.Red = true;
                        parent.Red = false;
                    }
                    else
                    {
                        if (brother.Left == null || !brother.Left.Red)
                        {
                            if (brother.Right != null)
                                brother.Right.Red = false;
                            brother.Red = true;
                            RotateLeft(brother);
                            brother = parent.Left;
                        }
                        brother.Red = parent.Red;
                        parent.Red = false;
                        if (brother.Left != null)
                            brother.Left.Red = false;
                        RotateRight(parent);
                        node = _root;
                    }
                }
                parent = node.Parent;
            }

            node.Red = false;
        }

        private Node Minimum(Node node)
        {
            var result = node;
            while (result.Left != null) result = result.Left;
            return result;
        }

        private Node Maximum(Node node)
        {
            var result = node;
            while (result.Right != null) result = result.Right;
            return result;
        }

        private Node Next(Node node)
        {
            Node result;
            if (node.Right != null)
            {
                result = Minimum(node.Right);
            }
            else
            {
                while (node.Parent != null && node.Parent.Right == node)
                    node = node.Parent;
                result = node.Parent;
            }
            return result;
        }

        private Node Previous(Node node)
        {
            Node result;
            if (node.Left != null)
            {
                result = Maximum(node.Left);
            }
            else
            {
                while (node.Parent != null && node.Parent.Left == node)
                    node = node.Parent;
                result = node.Parent;
            }
            return result;
        }

        public void Print()
        {
            var node = Minimum(_root);
            while (node != null)
            {
                Console.Write(node.Red ? '*' : ' ');
                Console.Write(node.Key);
                Console.Write('\t');
                node = Next(node);
            }
        }

        #region Nested classes

        private sealed class Node
        {
            public Node(TKey key)
            {
                Key = key;
            }

            public TKey Key;
            public bool Red = true;

            public Node Parent;
            public Node Left;
            public Node Right;
        }

        #endregion
    }

    public class Fenwick
    {
        /// <summary>
        /// Fenwick tree for summary on the array
        /// </summary>
        public class Sum
        {
            public readonly int Size;
            private readonly int[] _items;
            public Sum(int size)
            {
                Size = size;
                _items = new int[Size];
            }

            public Sum(IList<int> items)
                : this(items.Count)
            {
                for (var i = 0; i < Size; i++)
                    Increase(i, items[i]);
            }

            private int Query(int r)
            {
                if (r < 0) return 0;
                if (r >= Size) r = Size - 1;
                var result = 0;
                for (; r >= 0; r = (r & (r + 1)) - 1)
                    result += _items[r];
                return result;
            }

            private int Query(int l, int r)
            {
                return Query(r) - Query(l - 1);
            }

            public int this[int r]
            {
                get { return Query(r); }
            }

            public int this[int l, int r]
            {
                get { return Query(l, r); }
            }

            public void Increase(int i, int delta)
            {
                for (; i < Size; i = i | (i + 1))
                    _items[i] += delta;
            }
        }

        public class MinRight
        {
            private readonly int _maxValue;
            public readonly int N;
            private readonly int[] _items;

            public MinRight(int n, int? maxValue = null)
            {
                _maxValue = maxValue ?? n + 1;
                N = n;
                _items = new int[N + 1];
                for (int i = 0; i < _items.Length; ++i)
                {
                    _items[i] = _maxValue;
                }
            }

            public int Query(int index)
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

        public class MaxLeft
        {
            private readonly int _minValue;
            public readonly int N;
            private readonly int[] _items;

            public MaxLeft(int n, int minValue = 0)
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

            public int Query(int index)
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
    }

    /// <summary>
    /// Graph matching algorithms
    /// </summary>
    public class GraphMatching
    {
        #region Kuhn algorithm

        /// <summary>
        /// Kuhn algorithm for finding maximal matching
        /// in bipartite graph
        /// Vertexes are numerated from zero: 0, 1, 2, ... n-1
        /// Return array of matchings
        /// </summary>
        public static int[] Kuhn(List<int>[] g, int leftSize, int rightSize)
        {
            Debug.Assert(g.Count() == leftSize);

            var matching = new int[leftSize];
            for (var i = 0; i < rightSize; i++)
                matching[i] = -1;
            var used = new bool[leftSize];

            for (var v = 0; v < leftSize; v++)
            {
                Array.Clear(used, 0, leftSize);
                _kuhn_dfs(v, g, matching, used);
            }

            return matching;
        }

        private static bool _kuhn_dfs(int v, List<int>[] g, int[] matching, bool[] used)
        {
            if (used[v]) return false;
            used[v] = true;
            for (var i = 0; i < g[v].Count; i++)
            {
                var to = g[v][i];
                if (matching[to] == -1 || _kuhn_dfs(matching[to], g, matching, used))
                {
                    matching[to] = v;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }

    public class BinaryHeap<T> where T : IComparable
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
            while (i > 0)
            {
                int parent = (i - 1) >> 1;
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
            int left = (i << 1) + 1;
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

    public class Graph
    {
        public int E { get; set; }
        public int V { get; set; }
        public List<List<int>> Edges { get; }
        public List<int> Loops { get; set; }

        public Graph()
        {
            Edges = new();
            Loops = new();

            // zero pads
            Edges.Add(new());
            Loops.Add(0);
        }

        public void AddVertex(int v)
        {
            if (V < v)
            {
                for (int i = V + 1; i <= v; ++i)
                {
                    Edges.Add(new());
                    Loops.Add(0);
                }
                V = v;
            }
        }

        public void AddEdge(int v, int u)
        {
            AddVertex(v);
            AddVertex(u);

            if (v == u)
            {
                Edges[v].Add(v);
                Loops[v]++;
            }
            else
            {
                Edges[v].Add(u);
                Edges[u].Add(v);
            }
        }

        public IEnumerable<int> Dfs()
        {
            bool[] visited = new bool[V + 1];
            for (int v = 1; v <= V; ++v)
            {
                if (visited[v]) continue;
                visited[v] = true;

                var stack = new Stack<int>();
                stack.Push(v);

                while (stack.Count > 0)
                {
                    var w = stack.Pop();
                    yield return w;
                    foreach (var u in Edges[w])
                    {
                        if (!visited[u])
                        {
                            visited[u] = true;
                            stack.Push(u);
                        }
                    }
                }
            }
        }

        public IEnumerable<int> Bfs()
        {
            bool[] visited = new bool[V + 1];
            for (int v = 1; v <= V; ++v)
            {
                if (visited[v]) continue;
                visited[v] = true;

                var queue = new Queue<int>();
                queue.Enqueue(v);

                while (queue.Count > 0)
                {
                    var w = queue.Dequeue();
                    yield return w;
                    foreach (var u in Edges[w])
                    {
                        if (!visited[u])
                        {
                            visited[u] = true;
                            queue.Enqueue(u);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Enumerates all non-overlapping subgraphs
        /// </summary>
        public IEnumerable<Subgraph> Subgraphs()
        {
            bool[] visited = new bool[V + 1];
            for (int v = 1; v <= V; ++v)
            {
                if (visited[v]) continue;
                visited[v] = true;

                var queue = new List<int> { v };
                for (int i = 0; i < queue.Count; ++i)
                {
                    foreach (var u in Edges[queue[i]])
                    {
                        if (!visited[u])
                        {
                            visited[u] = true;
                            queue.Add(u);
                        }
                    }
                }

                int E = 0;
                int loopCount = 0;
                foreach (var u in queue)
                {
                    E += Edges[u].Count;
                    loopCount += Loops[u];
                }
                E = (E + loopCount) / 2;

                yield return new Subgraph { E = E, V = queue.Count, Vertexes = queue, Loops = loopCount };
            }
        }

        public class Subgraph
        {
            public int E { get; set; }
            public int V { get; set; }
            public int Loops { get; set; }
            public List<int> Vertexes { get; set; }
        }
    }

    public abstract class LinkCutTree<TValue>
    {
        protected class Node
        {
            public Node Left, Right, Parent;
            public bool Reversed;
            public TValue Value, Agg;

            public Node(TValue value)
            {
                Value = value;
                Agg = value;
            }

            public bool IsRoot()
            {
                return Parent == null || (Parent.Left != this && Parent.Right != this);
            }

            public override string? ToString()
            {
                return Parent == null ? $"Root[{Value}]"
                    : IsRoot() ? $"{Value} --> {Parent.Value}"
                    : $"{Value} => {Parent.Value}";
            }
        }

        Node[] nodes;
        protected virtual Node CreateNode(TValue value) => new Node(value);
        protected abstract TValue Aggregate(TValue left, TValue right);
        protected abstract TValue NeutralElement { get; }

        public LinkCutTree(int n)
        {
            nodes = new Node[n];
            var neutralValue = NeutralElement;
            for (int i = 0; i < n; ++i)
            {
                nodes[i] = CreateNode(neutralValue);
            }
        }

        private void Update(Node v)
        {
            var agg = v.Value;
            if (v.Left != null)
                agg = Aggregate(agg, v.Left.Agg);
            if (v.Right != null)
                agg = Aggregate(agg, v.Right.Agg);
            v.Agg = agg;
        }

        private void Push(Node v)
        {
            if (v.Reversed)
            {
                Node tmp = v.Left;
                v.Left = v.Right;
                v.Right = tmp;
                if (v.Left != null) v.Left.Reversed ^= true;
                if (v.Right != null) v.Right.Reversed ^= true;
                v.Reversed = false;
            }
        }

        private void Rotate(Node v)
        {
            var p = v.Parent;
            var g = p.Parent;
            if (!p.IsRoot())
            {
                if (g.Left == p) g.Left = v;
                else g.Right = v;
            }
            v.Parent = g;

            if (p.Left == v)
            {
                p.Left = v.Right;
                if (v.Right != null) v.Right.Parent = p;
                v.Right = p;
                p.Parent = v;
            }
            else
            {
                p.Right = v.Left;
                if (v.Left != null) v.Left.Parent = p;
                v.Left = p;
                p.Parent = v;
            }

            Update(p);
            Update(v);
        }

        private void Splay(Node v)
        {
            Push(v);
            while (!v.IsRoot())
            {
                var p = v.Parent;
                if (!p.IsRoot())
                {
                    var g = p.Parent;
                    Push(g);
                }
                Push(p);
                Push(v);

                if (!p.IsRoot())
                {
                    if ((p.Left == v) == (p.Parent.Left == p)) Rotate(p);
                    else Rotate(v);
                }
                Rotate(v);
            }
            Push(v);
            Update(v);
        }

        private void Expose(Node v)
        {
            Node last = null;
            for (Node y = v; y != null; y = y.Parent)
            {
                Splay(y);
                y.Right = last;
                Update(y);
                last = y;
            }
            Splay(v);
        }

        private void MakeRoot(Node v)
        {
            Expose(v);
            v.Reversed ^= true;
            Push(v);
        }

        private Node FindRoot(Node v)
        {
            Expose(v);
            while (v.Left != null)
            {
                Push(v);
                v = v.Left;
            }
            Splay(v);
            return v;
        }

        public void Set(int v, TValue value)
        {
            Node node = nodes[v];
            Expose(node);
            node.Value = value;
            Update(node);
        }

        public void Link(int u, int v)
        {
            Node n1 = nodes[u];
            Node n2 = nodes[v];
            MakeRoot(n1);
            if (FindRoot(n2) == n1)
                return;
            n1.Parent = n2;
        }

        public void Cut(int u, int v)
        {
            Node n1 = nodes[u];
            Node n2 = nodes[v];
            MakeRoot(n1);
            Expose(n2);
            if (n2.Left == n1)
            {
                n2.Left.Parent = null;
                n2.Left = null;
                Update(n2);
            }
        }

        public TValue Query(int u, int v)
        {
            Node n1 = nodes[u];
            Node n2 = nodes[v];
            MakeRoot(n1);
            Expose(n2);
            return n2.Agg;
        }

        public bool IsConnected(int u, int v)
        {
            Node n1 = nodes[u];
            Node n2 = nodes[v];
            return FindRoot(n1) == FindRoot(n2);
        }
    }

    public class SumLinkCutTree : LinkCutTree<long>
    {
        public SumLinkCutTree(int n) : base(n)
        {
        }

        protected override long Aggregate(long left, long right) => left + right;
        protected override long NeutralElement => 0;
    }
}