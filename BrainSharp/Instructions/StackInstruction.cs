using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class StackInstruction : Instruction
    {
        private int change;

        public StackInstruction(int change)
        {
            this.change = change;
        }

        public override string GetCode()
        {
            return "stack[pointer] " + (change > 0 ? "+" : "-") + "= " + Math.Abs(change) + ";";
        }

        public override Instruction Merge(Instruction other)
        {
            if (!(other is StackInstruction))
                throw new InstructionMergeException(this, other);

            StackInstruction result = new StackInstruction(change + (other as StackInstruction).change);
            return result.change != 0 ? result : null;
        }
    }
}
