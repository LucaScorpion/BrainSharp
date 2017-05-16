using System;
using System.Collections.Generic;
using System.Text;
using BrainSharp.Instructions;
using BrainSharp.Properties;

namespace BrainSharp
{
    public class InstructionList
    {
		private const int BASE_TABS = 3;
		private const string CODE_TOKEN = "${code}";

		private List<Instruction> instructions = new List<Instruction>();

        public void AddInstruction(Instruction i) => instructions.Add(i);

		/// <summary>
		/// Generate the code from the instruction list.
		/// </summary>
		/// <returns>The complete program code.</returns>
		public string GenerateCode() => Resources.CodeTemplate.Replace(CODE_TOKEN, GetCodeFromInstructions());

		private string GetCodeFromInstructions()
		{
            int tabs = BASE_TABS;

            // Build the code.
            StringBuilder code = new StringBuilder();
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
                if (current is IMergeable && current.GetType() == next.GetType())
                {
                    Instruction merged = (current as IMergeable).Merge(next);

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
    }
}
