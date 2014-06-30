using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        bool isPrime(int n)
        {
            if (n%2 == 0) return false;
            for (var i=3;i*i<=n;i+=2)
                if (n%i == 0) return false;
            return true;
        }

        void Solve()
        {
            int n;
            Input.Next(out n);
            var X = Input.ArrayInt().ToArray();

            var counts = new int[n];
            foreach (var x in X)
            {
                if (isPrime(x))
                {
                    
                }
            }


            int m;
            Input.Next(out m);
            while (m-->0)
            {
                int l, r;
                Input.Next(out l, out r);
                var result = 0L;
                Console.Write(result);
            }

            Console.Write(0);
        }

        public static void Main()
        {
            var task = new Task();
            #if DEBUG
            task.Solve();
            #else
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
            #endif
        }
    }
}
