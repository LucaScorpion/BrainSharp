using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Arguments
{
	public class FlagArgument
	{
		private readonly string name;
		private readonly string option;
		private readonly string longOption;
		private readonly string description;

		public FlagArgument(string name, string option, string longOption, string description)
		{
			// Check if at least one option is not null.
			if (option == null && longOption == null)
				throw new ArgumentException("At least one option must be specified for an argument.");

			this.name = name;
			this.option = '-' + option;
			this.longOption = "--" + longOption;
			this.description = description;
		}

		public override string ToString() => $"{name}: [{option}, {longOption}]";

		public string Name => name;

		public string Option => option;

		public string LongOption => longOption;

		public string Description => description;

		public bool Enabled
		{
			get;
			set;
		}
	}
}
