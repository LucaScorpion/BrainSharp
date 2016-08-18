using System;
using System.IO;

namespace BrainSharp
{
    public class Program
    {
        private static string open = null;
        private static string exeFile = null;
        private static string codeFile = null;
        private static string code = null;
        private static string input = String.Empty;
        private static bool overwrite = false;
        private static bool run = false;

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Parse all arguments.
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    // File to open.
                    case "-f":
                        open = Util.GetArgumentValue(args, ++i, "-f", "filename");
                        break;
                    // Source to save.
                    case "-c":
                        codeFile = Util.GetArgumentValue(args, ++i, "-c", "filename");
                        break;
                    // Executable to save.
                    case "-x":
                        exeFile = Util.GetArgumentValue(args, ++i, "-x", "filename");
                        break;
                    // Code as argument.
                    case "-t":
                        code = Util.GetArgumentValue(args, ++i, "-t", "code");
                        break;
                    // Overwrite existing.
                    case "-o":
                        overwrite = true;
                        break;
                    // Run.
                    case "-r":
                        run = true;
                        break;
                    case "-i":
                        input = Util.GetArgumentValue(args, ++i, "-i", "input");
                        break;
                    default:
                        Console.WriteLine("Unknown argument: " + args[i]);
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            // Code or exe file already exists and overwrite is disabled.
            if (codeFile != null && File.Exists(codeFile) && !overwrite)
                Console.WriteLine("File to save code to already exists: " + codeFile + ". Use -o for overwrite.");
            if (exeFile != null && File.Exists(exeFile) && !overwrite)
                Console.WriteLine("File to save executable to already exists: " + exeFile + ". Use -o for overwrite.");
            // No code or open file specified.
            else if (open == null && code == null)
                Console.WriteLine("No file or code specified.");
            // File to open does not exist.
            else if (open != null && !File.Exists(open))
                Console.WriteLine("File to open does not exist: " + open);
            // All is good.
            else
            {
                // Do the actual code processing etc.
                Console.ResetColor();
                Process();
            }

            Console.ReadLine();
        }

        private static void Process()
        {
            // Read the code from the file.
            if (open != null)
                code = File.ReadAllText(open);

            // Parse.
            Builder builder = new Builder();
            builder.Parse(code);

            // Save the code.
            if (codeFile != null)
            {
                try
                {
                    File.WriteAllText(codeFile, builder.GetCode());
                    Console.WriteLine("Code saved to: " + codeFile);
                }
                catch (IOException e)
                {
                    Util.WriteLine("An exception occured while trying to save the code.\n" + e.Message, ConsoleColor.Red);
                }
            }

            // Check if we have to compile the code.
            if (exeFile != null || run)
            {
                // Compile.
                bool success = builder.Compile(exeFile);
                Util.WriteLine("Compilation " + (!success ? "not " : "") + "successful.", success ? ConsoleColor.Green : ConsoleColor.Red);

                // Save the exe file.
                if (exeFile != null)
                    Console.WriteLine("Executable saved to: " + exeFile);

                // Run the program.
                if (success && run)
                {
                    Console.WriteLine("Running compiled program" + (input == String.Empty ? "" : " with input \"" + input + "\"") + ":");
                    builder.Run(input);
                }
            }
        }
    }
}
