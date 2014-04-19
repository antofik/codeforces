using System;
using System.Linq;

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

        class Visitor
        {
            public int i;
            public long c;
            public long p;
        }

        public class Table
        {
            public int i;
            public long r;
            public bool taken;
            public int visitor;
            public long cost;
        }

        void Solve()
        {
            long n;
            Input.Next(out n);

            var c = new long[n];
            var p = new long[n];
            for (var i = 0; i < n; i++)
                Input.Next(out p[i], out c[i]);

            var visitors = Enumerable.Range(0, (int)n).Select(i => new Visitor{ i=i, c = c[i], p = p[i] }).OrderByDescending(x => x.c).ThenBy(x => x.p).ToList();

            long k;
            Input.Next(out k);
            var r = Input.Numbers();

            var tables = Enumerable.Range(0, (int)k).Select(i => new Table { i=i, r = r[i] }).OrderBy(x => x.r).ToList();

            for (var ti = 0; ti < tables.Count; ti++)
            {
                var table = tables[ti];
                for (var vi = 0; vi < visitors.Count; vi++)
                {
                    var visitor = visitors[vi];
                    if (visitor.p <= table.r)
                    {
                        table.taken = true;
                        table.visitor = visitor.i;
                        table.cost = visitor.c;
                        visitors.Remove(visitor);
                        break;
                    }
                }
            }

            var count = tables.Count(x => x.taken);
            var sum = tables.Where(x => x.taken).Sum(x => x.cost);
            Console.WriteLine(count + " " + sum);
            foreach(var table in tables.Where(x=>x.taken))
                Console.WriteLine((table.visitor + 1) + " " + (table.i + 1));

        }
    }
}
