using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class InputInstruction : Instruction
    {
        public override string ToString()
        {
            return "Input instruction";
        }

        public override string GetCode()
        {
            return "stack[pointer] = ReadByte();";
        }
    }
}
