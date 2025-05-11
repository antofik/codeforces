public abstract class LinkCutTree<TValue>
{
    protected class Node
    {
        public Node Left, Right, Parent;
        public bool Reversed;
        public TValue Value, SubtreeValue;

        public Node(TValue value)
        {
            Value = value;
            SubtreeValue = value;
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

    protected Dictionary<int, Node> nodes = new Dictionary<int, Node>();
    protected virtual Node CreateNode(TValue value) => new Node(value);
    protected abstract TValue Aggregate(TValue left, TValue right);
    protected abstract TValue NeutralElement { get; }

    protected virtual void Update(Node x)
    {
        var agg = x.Value;
        if (x.Left != null)
            agg = Aggregate(agg, x.Left.SubtreeValue);
        if (x.Right != null)
            agg = Aggregate(agg, x.Right.SubtreeValue);
        x.SubtreeValue = agg;
    }

    protected virtual void Push(Node x)
    {
        if (x.Reversed)
        {
            var tmp = x.Left;
            x.Left = x.Right;
            x.Right = tmp;
            if (x.Left != null) x.Left.Reversed ^= true;
            if (x.Right != null) x.Right.Reversed ^= true;
            x.Reversed = false;
        }
    }

    private void Rotate(Node x)
    {
        var p = x.Parent;
        var g = p.Parent;
        if (!p.IsRoot())
        {
            if (g.Left == p) g.Left = x;
            else g.Right = x;
        }
        x.Parent = g;

        if (p.Left == x)
        {
            p.Left = x.Right;
            if (x.Right != null) x.Right.Parent = p;
            x.Right = p;
            p.Parent = x;
        }
        else
        {
            p.Right = x.Left;
            if (x.Left != null) x.Left.Parent = p;
            x.Left = p;
            p.Parent = x;
        }

        Update(p);
        Update(x);
    }

    private void Splay(Node x)
    {
        Push(x);
        while (!x.IsRoot())
        {
            var p = x.Parent;
            if (!p.IsRoot())
            {
                var g = p.Parent;
                Push(g);
            }
            Push(p);
            Push(x);

            if (!p.IsRoot())
            {
                if ((p.Left == x) == (p.Parent.Left == p)) Rotate(p);
                else Rotate(x);
            }
            Rotate(x);
        }
        Push(x);
        Update(x);
    }

    protected void Access(Node x)
    {
        Node last = null;
        for (Node y = x; y != null; y = y.Parent)
        {
            Splay(y);
            y.Right = last;
            Update(y);
            last = y;
        }
        Splay(x);
    }

    protected void MakeRoot(Node x)
    {
        Access(x);
        x.Reversed ^= true;
        Push(x);
    }

    protected Node FindRoot(Node x)
    {
        Access(x);
        while (x.Left != null)
        {
            Push(x);
            x = x.Left;
        }
        Splay(x);
        return x;
    }

    public void Add(int id, TValue value)
    {
        nodes[id] = CreateNode(value);
    }

    public void Remove(int id)
    {
        Node x = nodes[id];
        MakeRoot(x);
        if (x.Left != null) { x.Left.Parent = null; x.Left = null; }
        if (x.Right != null) { x.Right.Parent = null; x.Right = null; }
        nodes.Remove(id);
    }

    public void Set(int id, TValue value)
    {
        Node x = nodes[id];
        Access(x);
        x.Value = value;
        Update(x);
    }

    public void Link(int child, int parent)
    {
        Node x = nodes[child], y = nodes[parent];
        MakeRoot(x);
        if (FindRoot(y) == x)
            return;
        x.Parent = y;
    }

    public void Cut(int xId, int yId)
    {
        Node x = nodes[xId], y = nodes[yId];
        MakeRoot(x);
        Access(y);
        if (y.Left == x)
        {
            y.Left.Parent = null;
            y.Left = null;
            Update(y);
        }
    }

    public TValue Query(int uId, int vId)
    {
        Node u = nodes[uId], v = nodes[vId];
        MakeRoot(u);
        Access(v);
        return v.SubtreeValue;
    }

    public bool IsConnected(int uId, int vId)
    {
        Node u = nodes[uId], v = nodes[vId];
        return FindRoot(u) == FindRoot(v);
    }
}

public class SumLinkCutTree : LinkCutTree<int>
{
    protected override int Aggregate(int left, int right) => left + right;
    protected override int NeutralElement => 0;
}

public class MinLinkCutTree : LinkCutTree<int>
{
    protected override int Aggregate(int left, int right) => Math.Min(left, right);
    protected override int NeutralElement => int.MaxValue;
}

class Program 
{
    static void Main()
    {
        var lct = new SumLinkCutTree();
        for (int i = 0; i < 100; ++i)
        {
            lct.Add(i, i);
        }


        for (int i = 0; i < 99; ++i)
        {
            lct.Link(i, i+1);
        }

        Console.WriteLine(lct.IsConnected(10, 99)); // True

        Console.WriteLine(lct.Query(0, 99)); // 0+1+2+3+..+98+99 = 99 * 50 = 4950

        lct.Cut(50, 51);
        Console.WriteLine(lct.IsConnected(10, 99)); // False

        lct.Set(30, 30 + 50);
        lct.Link(49, 51); 
        Console.WriteLine(lct.Query(0, 99)); // 5000

    }
}
