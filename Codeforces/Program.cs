#define TASKC

#define TESTx

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Codeforces
{
    public class Program
    {
        public static void Main(string[] args)
        {            
#if TASKA
            const char problem = 'A';
#elif TASKB
            const char problem = 'B';
#elif TASKC
            const char problem = 'C';
#elif TASKD
            const char problem = 'D';
#elif TASKE
            const char problem = 'E';
#elif TASKF
            const char problem = 'F';
#elif TASKG
            const char problem = 'G';
#elif TASKH
            const char problem = 'H';
#elif TASKI
            const char problem = 'I';
#elif TASKJ
            const char problem = 'J';
#elif TASKK
            const char problem = 'K';
#elif TASKL
            const char problem = 'L';
#else
            const char problem = 'A';
#endif

            var tests = new[]{
#if TEST1
            1,
#elif TEST2
            2,
#elif TEST3
            3,
#elif TEST4
            4,
#elif TEST5
            5,
#elif TEST6
            6,
#elif TEST7
            7,
#elif TEST8
            8,
#elif TEST9
            9,
#elif TEST10
            10,
#else
                1,2,3,4,5,6,7,8,9,10,
#endif
            };

            var type = Type.GetType(string.Format("Codeforces.Task.Task{0}", problem));
            if (type == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Task not found: {0}", problem);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                var results = new List<bool>();
                var currentDir = Directory.GetCurrentDirectory();
                foreach (var test in tests.OrderBy(c => c).ToList())
                {
                    var path = string.Format("../../../Task{0}/Input/input{1}.txt", problem, test);

                    if (!File.Exists(path)) continue;
                    var text = File.ReadAllText(path);
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("*******************************************************************************");
                    Console.WriteLine("*                                                                             *");
                    Console.WriteLine("*                            TASK {0}  TEST {1}                                   *", problem, test);
                    Console.WriteLine("*                                                                             *");
                    Console.WriteLine("*******************************************************************************");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    var stream = new MemoryStream();
                    var ok = false;
                    var message = string.Format("Test {0} failed", test);
                    try
                    {
                        Console.SetIn(new StreamReader(path));
                        var defaultOut = Console.Out;
                        var writer = new StreamWriter(stream);
                        Console.SetOut(writer);
                        var watch = new Stopwatch();
                        watch.Start();
                        try
                        {
                            type.InvokeMember("Main", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);
                            Console.SetOut(defaultOut);
                        }
                        catch (Exception ex)
                        {
                            Console.SetOut(defaultOut);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n Error: {0}", ex);
                        }
                        watch.Stop();
                        writer.Flush();
                        writer.Close();
                        stream.Flush();
                        var output = Encoding.UTF8.GetString(stream.ToArray()).Replace("\r", "").Trim(new[] { ' ', '\t', '\n' });
                        var input = File.ReadAllText(string.Format("../../../Task{0}/Input/input{1}.txt", problem, test));
                        var correctOutput = File.ReadAllText(string.Format("../../../Task{0}/Output/output{1}.txt", problem, test)).Replace("\r", "").Trim(new[] { ' ', '\t', '\n' });
                        ok = output == correctOutput;
                        if (!ok)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine(" -- input --");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(input);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine(" -- your output --");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(output);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine(" -- correct output --");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(correctOutput);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            //                      Console.WriteLine("\nPress any key to continue...");
                            //                      Console.ReadKey();
                        }
                        else
                        {
                            message = string.Format("\nTest {0} passed in {1}ms", test, watch.ElapsedMilliseconds);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(input);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(output);
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n Error: test {0} for problem {1} was not found", test, problem);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n Error: test directory for problem {0} was not found", problem);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n Error: {0}", ex);
                    }
                    finally
                    {
                        stream.Dispose();
                    }
                    if (ok) Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    results.Add(ok);
                }
                if (tests.Count() > 1)
                {
                    Console.WriteLine("\n");
                    if (results.All(c => c))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("All tests passed");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed: {0}/{1}", results.Count(c => !c), results.Count);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            //     Console.WriteLine("Press any key to continue...");
            //     Console.ReadKey();

        }
    }
}
