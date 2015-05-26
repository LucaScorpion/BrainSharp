using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainSharp.Instructions;

namespace BrainSharp
{
    public class InstructionList
    {
        private List<Instruction> instructions;

        public InstructionList()
        {
            instructions = new List<Instruction>();
        }

        public void AddInstruction(Instruction i)
        {
            instructions.Add(i);
        }

        public string GetCode()
        {
            // Clean up the instruction list
            MergeInstructions();

            int tabs = BaseTabs;

            // Build the code
            StringBuilder code = new StringBuilder(PreCode);
            foreach (Instruction i in instructions)
            {
                string c = i.GetCode();
                if (c != null)
                {
                    // Change tabbing
                    if (i.DeltaTabs < 0)
                        tabs += i.DeltaTabs;

                    // Add all lines from the instruction
                    string[] lines = c.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    foreach (string line in lines)
                    {
                        for (int t = 0; t < tabs; t++)
                            code.Append('\t');
                        code.AppendLine(line);
                    }

                    // Change tabbing
                    if (i.DeltaTabs > 0)
                        tabs += i.DeltaTabs;
                }
            }
            code.Append(PostCode);

            return code.ToString();
        }

        private void MergeInstructions()
        {
            for (int i = 0; i < instructions.Count - 1; i++)
            {
                // Get the instructions
                Instruction current = instructions[i];
                Instruction next = instructions[i + 1];

                // Merge the current and next instruction
                if (current.CanMerge && current.GetType() == next.GetType())
                {
                    Instruction merged = current.Merge(next);

                    // Insert the new instruction, remove the old ones
                    if (merged != null)
                        instructions.Insert(i++, merged);
                    instructions.RemoveAt(i);
                    instructions.RemoveAt(i);

                    if (merged != null)
                        i -= 2;
                }
            }
        }

        #region Code snippets
        private const int BaseTabs = 3;

        private const string PreCode =
@"using System;

namespace brainfuck
{
    public class Program
    {
        private static byte[] stack = new byte[256];
        private static byte pointer = 0;
        private static string input = String.Empty;
        private static int inputPointer = 0;
        
        private static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                input += args[i];
                if (i < args.Length - 1)
                    input += "" "";
            }
            
";

        // Code goes here

        private const string PostCode =
@"            
            Console.Read();
        }
    }
}";
        #endregion
    }
}
