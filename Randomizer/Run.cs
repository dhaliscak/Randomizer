using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Randomizer
{
    class Run
    {
        static void Main(string[] args)
        {
            const string path = "list.txt";
            var slowDown = 50;
            
            if (File.Exists(path))
            {
                // Read each line of the file into a string array 
                // Each element of the array is one line of the file
                var lines = File.ReadAllLines(path);
                var rand = new Random();
                var lenghtOfArray = lines.Length;
                var index = 0;

                Console.WriteLine("Generating..");
                Console.WriteLine();
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        index = rand.Next(0, lenghtOfArray);
                        Console.WriteLine(lines[index]);
                        Thread.Sleep(slowDown);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

                #region Slowdown Effect

                //Add slowdown at the end of executing
                
                for (var i = 0; i < 10; i++)
                {
                    slowDown += 10;
                    index = rand.Next(0, lenghtOfArray);
                    Console.WriteLine(lines[index]);
                    Thread.Sleep(slowDown);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                }

                #endregion

                //Write a name to output
                Console.Clear();
                Console.WriteLine("Done");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(lines[index]);
                
                // Keep the console window opened
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"File {path} was not found");
                
                // Keep the console window opened
                Console.ReadKey();
            }
        }

        //custom methods
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
