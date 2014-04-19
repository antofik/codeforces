using System;

/*Library*/

namespace Codeforces.TaskD
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
            long n;
            Input.Next(out n);
            var a = Input.Numbers();

            var result = 1;
            var prev = 0L;
            var known = false;
            var deltaKnown = false;
            var delta = 0L;
            var zerosMiddle = 0;
            var zerosBefore = 0;

            foreach (var x in a)
            {
                if (x == -1)
                {
                    if (known)
                    {
                        if (deltaKnown)
                        {
                            prev += delta;
                            if (prev < 1)
                            {
                                result++;
                                known = false;
                                deltaKnown = false;
                                zerosBefore = 1;
                            }
                            else zerosMiddle = 1;
                        } 
                        else  zerosMiddle++;
                    }
                    else zerosBefore++;
                }
                else         // -1 -1 -1 324177995 -1 -1 85363006 527062853 -1 -1 502518581 -1 976180765 25042894 280800949 -1 -1 -1 4489293
                {
                    if (known)
                    {
                        if (deltaKnown)
                        {
                            if (delta * zerosMiddle != x - prev)
                            {
                                result++;
                                deltaKnown = false;
                            }
                        }
                        else
                        {
                            if ((x - prev)%zerosMiddle == 0)
                            {
                                deltaKnown = true;
                                delta = (x - prev) / zerosMiddle;

                                if (zerosBefore > 0 && delta > 0 && prev - zerosBefore*delta < 1)
                                {
                                    result++;
                                    deltaKnown = false;
                                }
                            }
                            else result++;
                            zerosBefore = 0;
                        }
                    }
                    else known = true;
                    
                    prev = x;
                    zerosMiddle = 1;
                }
            }
            Console.WriteLine(result);
        }
    }
}
