using System;
using System.Collections.Generic;
using System.Text;
using BrainSharp.Instructions;

namespace BrainSharp
{
    public class InstructionList
    {
        private List<Instruction> instructions = new List<Instruction>();

        public void AddInstruction(Instruction i)
        {
            instructions.Add(i);
        }

        /// <summary>
        /// Generate the code from the instruction list.
        /// </summary>
        /// <returns>The complete program code.</returns>
        public string GetCode()
        {
            int tabs = baseTabs;

            // Build the code.
            StringBuilder code = new StringBuilder(preCode);
            foreach (Instruction i in instructions)
            {
                string c = i.GetCode();
                if (c != null)
                {
                    // Change tabbing.
                    if (i.DeltaTabs < 0)
                        tabs += i.DeltaTabs;

                    // Add all lines from the instruction.
                    string[] lines = c.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    foreach (string line in lines)
                    {
                        for (int t = 0; t < tabs; t++)
                            code.Append('\t');
                        code.AppendLine(line);
                    }

                    // Change tabbing.
                    if (i.DeltaTabs > 0)
                        tabs += i.DeltaTabs;
                }
            }
            code.Append(postCode);

            return code.ToString();
        }

        /// <summary>
        /// Merge the instructions where possible.
        /// </summary>
        public void MergeInstructions()
        {
            for (int i = 0; i < instructions.Count - 1; i++)
            {
                // Get the instructions.
                Instruction current = instructions[i];
                Instruction next = instructions[i + 1];

                // Merge the current and next instruction.
                if (current.CanMerge && current.GetType() == next.GetType())
                {
                    Instruction merged = current.Merge(next);

                    // Insert the new instruction, remove the old ones.
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
        private const int baseTabs = 3;

        private const string preCode =
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
            
            /* Start of generated code. */
            
";

        // Code goes here.

        private const string postCode =
@"            
            /* End of generated code. */
            
            Console.Read();
        }
        
        private static byte ReadByte()
        {
            return (byte)(inputPointer < input.Length ? input[inputPointer++] : 0);
        }
        private static void PrintChar()
        {
            Console.Write((char)stack[pointer]);
        }
    }
}";
        #endregion
    }
}
