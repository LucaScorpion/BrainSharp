using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Arguments
{
	public class ValueArgument : FlagArgument
	{
		private readonly string valueName;

		public ValueArgument(string name, string option, string longOption, string description, string valueName)
			: base(name, option, longOption, description)
		{
			this.valueName = valueName;
		}

		public override string ToString() => $"{base.ToString()} <{valueName}>";

		public string Value
		{
			get;
			set;
		}
	}
}
