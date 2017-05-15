using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class StackInstruction : Instruction, IMergeable
    {
        private int change;

        public StackInstruction(int change)
        {
            this.change = change;
        }

        public override string ToString() => "Add " + change + " to current stack position";

        public override string GetCode() => "stack[pointer] " + (change > 0 ? "+" : "-") + "= " + Math.Abs(change) + ";";

        public Instruction Merge(Instruction other)
        {
            StackInstruction result = new StackInstruction(change + (other as StackInstruction).change);
            return result.change != 0 ? result : null;
        }
    }
}
