using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            int i = 7 << 27;
            var min = 1;
            var max = 9;
            Input.Next(out int n);
            var from = new int[n];
            var to = new int[n];
            for (i = 0; i < n; i++)
            {
                Input.Next(out from[i], out to[i]);
            }

            var map = new Dictionary<int, int>();
            i = 0;

            // start always from getting first person 
            var currentFloor = 1;
            var nextFloor = from[0];
            map[to[i]] = from[i] - currentFloor; // from 1st to Nth
#if DEBUG
            Console.WriteLine($"We are on {currentFloor}. Next passenger on {nextFloor}. Time={from[i]-currentFloor}s");
#endif
            for (i = 1; i < n; i++)
            {
                currentFloor = nextFloor;
                nextFloor = from[i]; // next person
#if DEBUG
                Console.WriteLine($"We are on {currentFloor}. Next passenger on {nextFloor}");
#endif
                var newMap = new Dictionary<int, int>();
                foreach (var pair in map)
                {
                    var state = pair.Key;
                    var passengers = GetPassengers(state);
#if DEBUG
                    Console.WriteLine($"  State={string.Join(",", passengers)}. Time={pair.Value}s");
#endif
                    var time = pair.Value;

                    var bot = Math.Min(currentFloor, nextFloor);
                    var top = Math.Max(currentFloor, nextFloor);
                    var newPassengers = passengers.Where(c => c < bot || c > top).ToList();
                    newPassengers.Add(to[i]);
                    if (newPassengers.Count <= 4)
                    {
                        var directTime = time + top - bot;
                        var newState = GetState(newPassengers);
                        if (!newMap.ContainsKey(newState) || newMap[newState] > directTime)
                            newMap[newState] = directTime;
                    }

                    for (var l = 0; l < passengers.Count; l++) if (passengers[l] < bot || passengers[l] > top)
                    {
                            for (var r = l; r < passengers.Count; r++) if (passengers[r] < bot || passengers[r] > top)
                            {
                                    var pr = passengers[r];
                                    var pl = passengers[l];

                                    if (pl > pr) Debugger.Break();

                                    var thisTime = time + (pr - pl) + Math.Min(Math.Abs(currentFloor - pl) + Math.Abs(pr - nextFloor), Math.Abs(nextFloor - pl) + Math.Abs(pr - currentFloor));
                                    newPassengers = passengers.Where(c => c < Math.Min(bot, pl) || c > Math.Max(top, pr)).ToList();
#if DEBUG
                                    Console.Write($"  {pl}=>{pr}: new state={string.Join(",", newPassengers)} in {thisTime}s");
#endif
                                    if (newPassengers.Count > 3)
                                    {
#if DEBUG
                                        Console.WriteLine(" - overload");
#endif
                                        continue;
                                    }
                                    newPassengers.Add(to[i]);
                                    var newState = GetState(newPassengers);

                                    if (newMap.TryGetValue(newState, out int otherTime))
                                    {
                                        if (otherTime < thisTime)
                                        {
#if DEBUG
                                            Console.WriteLine(" - too slow");
#endif
                                            continue;
                                        }
                                    }
#if DEBUG
                                    Console.WriteLine("");
#endif
                                    newMap[newState] = thisTime;
                                }

                        }
                }

                map = newMap;
            }

            currentFloor = nextFloor;

            int minTime = int.MaxValue;
            foreach (var pair in map)
            {
                var state = pair.Key;
                var time = pair.Value;
                var passengers = GetPassengers(state);
                if (!passengers.Any()) continue;
                var l = passengers.First();
                var r = passengers.Last();
                if (l > r) Debugger.Break();
                time += Math.Min(Math.Abs(r - currentFloor), Math.Abs(currentFloor - l)) + r - l;
                if (time < minTime)
                    minTime = time;
            }

#if DEBUG
            Console.WriteLine($"We are on {currentFloor}. Time={minTime}s");
#endif

            var result = minTime + 2 * n;
            Console.WriteLine(result);
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

        public static int GetState(List<int> passengers)
        {
            if (passengers.Count == 0) return 0;
            passengers.Sort();
            var state = passengers[0];
            for (var i = 1; i < passengers.Count; i++)
            {
                state *= 10;
                state += passengers[i];
            }
            return state;
        }

        public static IList<int> GetPassengers(int state)
        {
            var passengers = new List<int>();
            while (state > 0)
            {
                var p = state % 10;
                if (p != 0)
                    passengers.Add(p);
                state /= 10;
            }
            passengers.Sort();
            return passengers;
        }
    }
}
