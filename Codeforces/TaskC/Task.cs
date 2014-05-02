using System;
using System.Text;

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
            int n, m, z, y, x, p;
            Input.Next(out n, out m, out x, out y, out z, out p);

            var flipped = y%2 == 1;
            var horizontalFlip = x%2 == 0;
            var state = (4 + x%4 - z%4) % 4;
            var output = new StringBuilder();
            for (var t = 0; t < p; t++)
            {
                int X, Y;
                Input.Next(out Y, out X);
                if (flipped)
                {
                    if (horizontalFlip)
                    {
                        X = m - X + 1;
                    }
                    else
                    {
                        Y = n - Y + 1;
                    }
                }
                switch (state)
                {
                    case 1:
                        var c = X;
                        X = n - Y + 1;
                        Y = c;
                        break;
                    case 2:
                        X = m - X + 1;
                        Y = n - Y + 1;
                        break;
                    case 3:
                        var r = X;
                        X = Y;
                        Y = m - r + 1;
                        break;

                }
                output.AppendFormat("{0} {1}\r\n", Y, X);
            }
            Console.WriteLine(output);
        }
    }
}
