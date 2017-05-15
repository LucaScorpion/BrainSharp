using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class PrintInstruction : Instruction
    {
        public override string ToString() => "Print instruction";

        public override string GetCode() => "PrintChar();";
    }
}
