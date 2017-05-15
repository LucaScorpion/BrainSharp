using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class PointerInstruction : Instruction, IMergeable
    {
        private int change;

        public PointerInstruction(int change)
        {
            this.change = change;
        }

        public override string ToString() => "Add " + change + " to the pointer";

        public override string GetCode() => "pointer " + (change > 0 ? "+" : "-") + "= " + Math.Abs(change) + ";";

        public Instruction Merge(Instruction other)
        {
            PointerInstruction result = new PointerInstruction(change + (other as PointerInstruction).change);
            return result.change != 0 ? result : null;
        }
    }
}
