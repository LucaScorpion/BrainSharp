using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public interface IMergeable<T> where T : Instruction
    {
		T Merge(T other);
    }
}
