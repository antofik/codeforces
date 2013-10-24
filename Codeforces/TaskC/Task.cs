using System;
using System.Collections.Generic;
using System.Linq;
/*Library*/

namespace Codeforces.TaskC
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
            long n, x;
            Input.Next(out n, out x);

            var numbers = Input.Numbers().OrderBy(c=>c).ToList();

            var position = numbers.IndexOf(x);
            if (position == -1) position = BinarySearch(numbers, x);

            var answer = 0;
            while (numbers[(numbers.Count - 1) / 2] != x)
            {
                numbers.Insert(position, x);
                answer++;
            }
            Console.WriteLine(answer);
        }

        private int BinarySearch(IList<long> numbers, long x)
        {
            var l = 0;
            var r = numbers.Count;
            while (l < r)
            {
                var m = (l + r)/2;
                if (numbers[m] == x) return m;
                if (numbers[m] > x) r = m;
                else l = m + 1;
            }
            return l;
        }
    }
}
