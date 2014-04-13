using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
/*Library*/

namespace Codeforces.TaskB
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
            long n, k;
            Input.Next(out n, out k);
            var items = new List<Item>();
            for (var i = 0; i < n; i++)
            {
                long c, t;
                Input.Next(out c, out t);
                items.Add(new Item{Index = i, Cost = c, Type = t});
            }

            items = items.OrderBy(c => c.Type).ThenByDescending(c => c.Cost).ToList();

            var cost = 0d;
            var builder = new StringBuilder(Environment.NewLine);
            for (var i = 0; i < k - 1; i++)
            {
                builder.Append("1 ");
                builder.Append(items[i].Index + 1);
                builder.Append(Environment.NewLine);
                cost += items[i].Cost/(items[i].Type==1 ? 2.0 : 1.0);
            }
            builder.Append(n - k + 1);
            var minCost = double.PositiveInfinity;
            for (var i = (int)k - 1; i < n; i++)
            {
                builder.Append(" ");
                builder.Append(items[i].Index + 1);
                cost += items[i].Cost;
                if (items[i].Cost < minCost) minCost = items[i].Cost;
            }
            if (items[(int)k - 1].Type == 1) cost -= minCost / 2.0;
            builder.Insert(0, cost.ToString("F1", CultureInfo.InvariantCulture));
            Console.Write(builder);
        }

        class Item
        {
            public long Cost;
            public long Type;
            public int Index;
        }
    }
}
