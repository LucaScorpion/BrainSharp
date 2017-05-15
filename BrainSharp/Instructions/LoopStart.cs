using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class LoopStart : Instruction
    {
        public override string ToString() => "Loop start";

        public override string GetCode() => "while (stack[pointer] != 0)\n{";

		public override int DeltaTabs => 1;
    }
}
