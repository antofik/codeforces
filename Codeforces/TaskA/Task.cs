using System;
using System.Collections.Generic;
using System.Linq;
/*Library*/

namespace Codeforces.TaskA
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        void Solve()
        {
            long n, k;
            Input.Next(out n, out k);
            var teams = new Team[n];
            for (var i = 0; i < n; i++)
            {
                teams[i].Index = i;
                Input.Next(out teams[i].Problems, out teams[i].Time);
            }

            var kTeam = teams.OrderByDescending(c => c).Skip((int) (k - 1)).First();
            Console.WriteLine(teams.Count(t=>t==kTeam));
        }

        struct Team : IComparable
        {
            public long Index;
            public long Problems;
            public long Time;

            public bool Equals(Team other)
            {
                return other.Time == Time && other.Problems == Problems;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public int CompareTo(object obj)
            {
                var other = (Team) obj;
                if (other == this) return 0;
                return this > other ? 1 : -1;
            }

            public override bool Equals(object obj)
            {
                return Equals((Team)obj);
            }

            public static bool operator ==(Team t1, Team t2)
            {
                return t1.Equals(t2);
            }

            public static bool operator !=(Team t1, Team t2)
            {
                return !(t1 == t2);
            }

            public static bool operator <=(Team t1, Team t2)
            {
                return t1 < t2 || t1 == t2;
            }

            public static bool operator >=(Team t1, Team t2)
            {
                return t1 > t2 || t1 == t2;
            }

            public static bool operator <(Team t2, Team t1)
            {
                if (t1.Problems > t2.Problems) return true;
                return t1.Problems == t2.Problems && t1.Time < t2.Time;
            }

            public static bool operator >(Team t1, Team t2)
            {
                if (t1.Problems > t2.Problems) return true;
                return t1.Problems == t2.Problems && t1.Time < t2.Time;
            }
        }
    }
}
