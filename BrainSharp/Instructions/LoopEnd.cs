using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class LoopEnd : Instruction
    {
        public override string ToString() => "Loop end";

        public override string GetCode() => "}";

        public override int DeltaTabs => -1;
    }
}
