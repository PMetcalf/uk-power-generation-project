﻿using GensetSimulator.Models;
using System;

namespace GensetSimulator
{
    class Program
    {
        /// <summary>
        /// This is the main executable class for the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = typeof(Program).Name;

            // Initialise webservice.

            Run();
        }

        /// <summary>
        /// Create genset, wait for user input then start.
        /// </summary>
        static void Run()
        {
            // Initialise genset.
            Genset genset = new Genset();

            while (true)
            {
                var consoleInput = ReadFromConsole();
                if (string.IsNullOrWhiteSpace(consoleInput)) continue;

                // Start genset.
                genset.StartGenset();
                Console.WriteLine("Press key to stop");

                try
                {
                    while (!Console.KeyAvailable)
                    {
                        // Run genset.
                        genset.RunGenset();
                    }                
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        const string _readPrompt = "Type to Start:";

        /// <summary>
        /// Read and return console input.
        /// </summary>
        /// <returns>ConsoleMessage</returns>
        public static string ReadFromConsole()
        {
            // Show a prompt, and get input:
            Console.Write(_readPrompt);
            return Console.ReadLine();
        }
    }
}