using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Arguments
{
	public class ArgumentParser
	{
		private List<FlagArgument> arguments;
		private bool success = true;

		public ArgumentParser(string[] input, params FlagArgument[] arguments)
		{
			this.arguments = arguments.ToList();
			Parse(input);
		}

		private void Parse(string[] input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				bool found = false;

				string inputArg = input[i];

				// Check if the given argument starts with - or --.
				bool longOption = inputArg.StartsWith("--");
				if (!longOption && !inputArg.StartsWith("-"))
				{
					InvalidArg(inputArg);
					continue;
				}

				// Check with each argument if the input matches.
				foreach (FlagArgument argument in arguments)
				{
					string compare = longOption ? argument.LongOption : argument.Option;
					found = compare != null && inputArg == compare;

					argument.Enabled |= found;
					if (found)
					{
						// Check if the argument requires a value.
						if (argument is ValueArgument)
						{
							ValueArgument valueArg = argument as ValueArgument;

							// Check if the value is present.
							if (i >= input.Length - 1)
							{
								Util.WriteLine("Missing value for argument: " + inputArg, ConsoleColor.Red);
								success = false;
								break;
							}

							valueArg.Value = input[++i];
						}

						break;
					}
				}

				// Check if the input was valid.
				if (!found)
					InvalidArg(inputArg);
			}
		}

		private void InvalidArg(string input)
		{
			Util.WriteLine("Invalid argument specified: " + input, ConsoleColor.Red);
			success = false;
		}

		public void PrintHelp()
		{
			// Options.
			List<string> options = new List<string>(arguments.Count);
			arguments.ForEach(arg => options.Add($"{arg.Option}, {arg.LongOption}"));
			PadStrings(options);

			// Argument values.
			List<string> values = new List<string>(arguments.Count);
			arguments.ForEach(arg => values.Add(arg is ValueArgument ? $"<{(arg as ValueArgument).ValueName}>" : String.Empty));
			PadStrings(values);

			// Descriptions.
			List<string> descriptions = new List<string>(arguments.Count);
			arguments.ForEach(arg => descriptions.Add(arg.Description));

			// Print everything.
			for (int i = 0; i < arguments.Count; i++)
				Console.WriteLine($"{options[i]} {values[i]} {descriptions[i]}");
		}

		private void PadStrings(List<string> strings)
		{
			int maxLength = strings.Max(s => s.Length);
			for (int i = 0; i < strings.Count; i++)
				strings[i] += new string(' ', maxLength - strings[i].Length);
		}

		public bool Success => success;

		public bool IsEnabled(string argName) => arguments.Find(arg => arg.Name == argName).Enabled;

		public string GetValue(string argName) => (arguments.Find(arg => arg.Name == argName) as ValueArgument)?.Value;
	}
}
