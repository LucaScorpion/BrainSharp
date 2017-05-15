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

		public virtual int DeltaTabs => 0;
    }
}
