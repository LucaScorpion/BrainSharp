using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class PrintInstruction : Instruction
    {
        public PrintInstruction()
        {

        }

        public override string GetCode()
        {
            return "Console.Write((char)stack[pointer]);";
        }
    }
}
