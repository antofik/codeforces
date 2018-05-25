using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            int i;
            Input.Next(out int n);
            var from = new int[n];
            var to = new int[n];
            for (i = 0; i < n; i++)
            {
                Input.Next(out from[i], out to[i]);
            }

            var floor = 1;
            var state = new Dictionary<P, int>();
            i = 0;
            var time = from[i] - floor /*from 1st to Nth*/;
            state[new P(to[i])] = time; // we have 1 passanger, waiting for "to[i]" floor
            floor = from[i]; // current floor

            for (i=1; i < n; i++) {
                var newState = new Dictionary<P, int>();

                foreach (var p in state.Keys)
                {
                    if (p.size == 0) // cabin is empty, we are ready to go to i+1 passanger
                    {
                        time = from[i] - floor;
                        var newP = p.Add(to[i]);
                        newState[newP] = state[p] + time;
                    }
                }
            }

            // result + 2*n;
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


    struct P
    {
        public P(int p1) : this(p1, 0, 0)
        {
            size = 1;
        }

        public P(int p1, int p2) : this(p1, p2, 0)
        {
            size = 2;
        }

        public P(int p1, int p2, int p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            size = 3;
        }

        internal int size;
        internal int p1;
        internal int p2;
        internal int p3;

        public P Add(int p)
        {
            var x = (P) MemberwiseClone();
            x.size++;
            if (x.size == 1) p1 = p;
            else if (x.size == 2) p2 = p;
            else if (x.size == 3) p3 = p;
            else if (x.size == 4) p4 = p;
            return x;
        }
    }

}
