using System.Linq;

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


