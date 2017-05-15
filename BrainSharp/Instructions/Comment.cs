using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSharp.Instructions
{
    public class Comment : Instruction, IMergeable
    {
        private string comment;

        public Comment(string comment)
        {
            this.comment = comment;
        }

        public override string ToString() => GetCode() != null ? GetCode() : "[empty comment]";

        public override string GetCode() => !String.IsNullOrWhiteSpace(comment) ? "/* " + comment.Trim() + " */" : null;

        public Instruction Merge(Instruction other) => new Comment(comment + (other as Comment).comment);
    }
}
