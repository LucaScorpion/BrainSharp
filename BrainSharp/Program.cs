﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string open = null;
            string exeFile = null;
            string codeFile = null;
            string code = null;
            bool overwrite = false;
            bool run = false;

            // Parse all arguments
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    // File to open
                    case "-f":
                        if (++i < args.Length)
                            open = args[i];
                        else
                            Console.WriteLine("Found argument -f without filename following.");
                        break;
                    // Source to save
                    case "-s":
                        if (++i < args.Length)
                            codeFile = args[i];
                        else
                            Console.WriteLine("Found argument -s without filename following.");
                        break;
                    // Executable to save
                    case "-e":
                        if (++i < args.Length)
                            exeFile = args[i];
                        else
                            Console.WriteLine("Found argument -e without filename following.");
                        break;
                    // Code as argument
                    case "-t":
                        if (++i < args.Length)
                            code = args[i];
                        else
                            Console.WriteLine("Found argument -t without code following.");
                        break;
                    // Overwrite existing
                    case "-o":
                        overwrite = true;
                        break;
                    // Run
                    case "-r":
                        run = true;
                        break;
                    default:
                        Console.WriteLine("Unknown argument: " + args[i]);
                        break;
                }
            }

            // Check all sorts of things that could go wrong
            if (codeFile != null && File.Exists(codeFile) && !overwrite)
                Console.WriteLine("File to save code to already exists: " + codeFile + ". Use -o for overwrite.");
            // Check all sorts of things that could go wrong
            if (exeFile != null && File.Exists(exeFile) && !overwrite)
                Console.WriteLine("File to save executable to already exists: " + exeFile + ". Use -o for overwrite.");
            // No code or open file specified
            else if (open == null && code == null)
                Console.WriteLine("No file or code specified.");
            // File to open does not exist
            else if (open != null && !File.Exists(open))
                Console.WriteLine("File to open does not exist: " + open);
            // All is good
            else
            {
                // Read the code from the file
                if (open != null)
                    code = File.ReadAllText(open);

                // Parse
                Parser p = new Parser();
                p.Parse(code);

                // Save the code
                if (codeFile != null)
                {
                    try
                    {
                        File.WriteAllText(codeFile, p.ToString());
                        Console.WriteLine("Code saved to: " + codeFile);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("An exception occured while trying to save the code.\n" + e.Message);
                    }
                }

                if (exeFile != null || run)
                {
                    // Compile and save the exe
                    bool success = p.Compile(exeFile);
                    Console.WriteLine("Compilation " + (!success ? "not " : "") + "successful.");
                    if (exeFile != null)
                        Console.WriteLine("Executable saved to: " + exeFile);

                    // Run the program
                    if (success && run)
                    {
                        Console.WriteLine("Running compiled program:");
                        p.Run();
                    }
                }
            }
        }
    }
}