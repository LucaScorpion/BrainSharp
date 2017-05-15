using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class InputInstruction : Instruction
    {
        public override string ToString() => "Input instruction";

        public override string GetCode() => "stack[pointer] = ReadByte();";
    }
}
