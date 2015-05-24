using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class Comment : Instruction
    {
        private string comment;

        public Comment(string comment)
        {
            this.comment = comment;
        }

        public override string GetCode()
        {
            return !String.IsNullOrWhiteSpace(comment) ? "/* " + comment.Trim() + " */" : null;
        }

        public override Instruction Merge(Instruction other)
        {
            if (!(other is Comment))
                throw new InstructionMergeException(this, other);

            return new Comment(comment + (other as Comment).comment);
        }

        public override bool CanMerge
        {
            get { return true; }
        }
    }
}
