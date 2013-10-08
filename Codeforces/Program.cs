using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Codeforces
{
    class Program
    {
        static void Main(string[] args)
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
#endif

            var tests = new int []{
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
#else
            1,2,3,4,5
#endif
            };
            var tournament = MethodBase.GetCurrentMethod().DeclaringType.Namespace ?? "";

            var type = Type.GetType(string.Format("Codeforces.Task{0}.Task", problem));
            if (type == null)
            {
                Console.WriteLine("Task not found: {0}", problem);
            }
            else
            {
                var results = new List<bool>();
                foreach (var test in tests)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("*******************************************************************************");
                    Console.WriteLine("*                                                                             *");
                    //Console.WriteLine("*                            TOURNAMENT {0}{1}*", tournament, string.Join("", Enumerable.Repeat(" ", 38 - tournament.Length)));
                    Console.WriteLine("*                            TASK {0}  TEST {1}                                   *", problem, test);
                    Console.WriteLine("*                                                                             *");
                    Console.WriteLine("*******************************************************************************");
                    Console.WriteLine("\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    var stream = new MemoryStream();
                    var ok = false;
                    var message = string.Format("Test {0} failed", test);
                    try
                    {
                        Console.SetIn(new StreamReader(string.Format("Task{0}/Tests/test{1}.txt", problem, test)));
                        var defaultOut = Console.Out;
                        var writer = new StreamWriter(stream);
                        Console.SetOut(writer);
                        type.InvokeMember("Main", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);
                        writer.Flush();
                        writer.Close();
                        stream.Flush();
                        var output = Encoding.UTF8.GetString(stream.ToArray()).Replace("\r","");
                        var correctOutput = File.ReadAllText(string.Format("Task{0}/Results/test{1}.txt", problem, test));
                        if (output.EndsWith("\n")) output = output.Substring(0, output.Length - 1);
                        ok = output == correctOutput;
                        Console.SetOut(defaultOut);
                        if (!ok)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(output);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(correctOutput);
                        }
                        else
                        {
                            message = string.Format("Test {0} passed", test);
                            Console.ForegroundColor = ConsoleColor.Gray;
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
                    Console.WriteLine("*******************************************************************************");
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

            Console.WriteLine("\n");
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();

        }
    }
}
