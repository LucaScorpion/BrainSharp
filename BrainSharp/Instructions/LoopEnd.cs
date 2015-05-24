using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class LoopEnd : Instruction
    {
        public LoopEnd()
        {

        }

        public override string GetCode()
        {
            return "}";
        }

        public override int DeltaTabs
        {
            get { return -1; }
        }
    }
}
