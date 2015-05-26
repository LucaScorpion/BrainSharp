using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class PointerInstruction : Instruction
    {
        private int change;

        public PointerInstruction(int change)
        {
            this.change = change;
        }

        public override string ToString()
        {
            return "Add " + change + " to the pointer";
        }

        public override string GetCode()
        {
            return "pointer " + (change > 0 ? "+" : "-") + "= " + Math.Abs(change) + ";";
        }

        public override Instruction Merge(Instruction other)
        {
            if (!(other is PointerInstruction))
                throw new InstructionMergeException(this, other);

            PointerInstruction result = new PointerInstruction(change + (other as PointerInstruction).change);
            return result.change != 0 ? result : null;
        }

        public override bool CanMerge
        {
            get { return true; }
        }
    }
}
