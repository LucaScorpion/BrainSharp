using BrainSharp.Arguments;
using System;
using System.IO;

namespace BrainSharp
{
    public class Program
    {
		private static ArgumentParser parser;

		private static void Main(string[] args)
		{
			parser = new ArgumentParser(args,
				new FlagArgument("help", "h", "help", "Display the help."),
				new ValueArgument("file", "f", "file", "The file to read brainfuck code from. Cannot be used in combination with -c.", "path"),
				new ValueArgument("code", "c", "code", "The brainfuck code to use. Cannot be used in combination with -f.", "brainfuck"),
				new FlagArgument("run", "r", "run", "Run the brainfuck code."),
				new ValueArgument("input", "i", "input", "The input to use when running the program.", "input"),
				new ValueArgument("save", "s", "save", "The file to save the generated C# code to.", "path"),
				new ValueArgument("exe", "x", "executable", "The file to save the generated executable to.", "path"),
				new FlagArgument("overwrite", "o", "overwrite", "Overwrite any existing files.")
			);

			bool success = parser.Success;

			// Check for conflicts between the given arguments.
			if (parser.IsEnabled("file") && parser.IsEnabled("code"))
			{
				Util.WriteLine("Arguments -f and -c cannot be used together.", ConsoleColor.Red);
				success = false;
			}

			// Check if no code is given.
			if (!parser.IsEnabled("file") && !parser.IsEnabled("code"))
			{
				Util.WriteLine("No file or code specified.", ConsoleColor.Red);
				success = false;
			}

			// Check if the input file exists.
			if (parser.IsEnabled("file") && !File.Exists(parser.GetValue("file")))
			{
				Util.WriteLine("File to read brainfuck code from does not exist: " + parser.GetValue("file"), ConsoleColor.Red);
				success = false;
			}

			// All is good.
			if (success)
				Process();

#if DEBUG
			Console.ReadLine();
#endif
		}

        private static void Process()
        {
			// Read the code.
			string code = parser.IsEnabled("file") ? File.ReadAllText(parser.GetValue("file")) : parser.GetValue("code");

			// Parse.
			Console.WriteLine("Parsing brainfuck code...");
            Builder builder = new Builder();
            builder.Parse(code);
			Console.WriteLine("Done.");

			// Save the code.
			if (parser.IsEnabled("save"))
            {
				string codeFile = parser.GetValue("save");

				if (File.Exists(codeFile) && !parser.IsEnabled("overwrite"))
					Util.WriteLine($"File {codeFile} already exists. Use the -o argument to allow overwriting.", ConsoleColor.Yellow);
				else
				{
					// Save the generated C# code to the file.
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
            }

            // Check if we have to compile the code.
            if (parser.IsEnabled("exe") || parser.IsEnabled("run"))
            {
				string exeFile = parser.IsEnabled("exe") ? parser.GetValue("exe") : null;

				// Compile.
				bool success = builder.Compile(exeFile);
                Util.WriteLine("Compilation " + (!success ? "not " : "") + "successful.", success ? ConsoleColor.Green : ConsoleColor.Red);

                // Save the exe file.
                if (exeFile != null)
                    Console.WriteLine("Executable saved to: " + exeFile);

                // Run the program.
                if (success && parser.IsEnabled("run"))
                {
					string input = parser.GetValue("input");
                    Console.WriteLine("Running compiled program" + (String.IsNullOrEmpty(input) ? "" : " with input \"" + input + "\"") + ":");
                    builder.Run(input);
                }
            }
        }
    }
}
