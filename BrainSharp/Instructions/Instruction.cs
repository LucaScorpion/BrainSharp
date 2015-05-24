using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public abstract class Instruction
    {
        public abstract string GetCode();

        public virtual Instruction Merge(Instruction other)
        {
            throw new InvalidOperationException("This instruction (" + this.GetType() + ") cannot be merged with " + other.GetType());
        }

        public virtual int DeltaTabs
        {
            get { return 0; }
        }
        public virtual bool CanMerge
        {
            get { return false; }
        }
    }
}
