using System;
using BrainSharp.Instructions;

namespace BrainSharp
{
    public class InstructionMergeException : Exception
    {
        private Instruction i1, i2;

        public InstructionMergeException(Instruction i1, Instruction i2)
        {
            this.i1 = i1;
            this.i2 = i2;
        }

        public override string Message
        {
            get
            {
                return "Cannot merge these instructions:\n" + i1.GetCode() + "\n" + i2.GetCode();
            }
        }
    }
}
