using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class LoopStart : Instruction
    {
        public override string ToString()
        {
            return "Loop start";
        }

        public override string GetCode()
        {
            return "while (stack[pointer] != 0)\n{";
        }

        public override int DeltaTabs
        {
            get { return 1; }
        }
    }
}
