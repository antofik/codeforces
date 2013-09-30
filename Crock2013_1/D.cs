using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D
{
    public class Program
    {
        static void Main()
        {
            var problem = new Problem();
            problem.Solve();

#if DEBUG
            Console.ReadKey();
#endif
        }
    }

    public class Problem
    {
        public void Solve()
        {

        }
    }
}
