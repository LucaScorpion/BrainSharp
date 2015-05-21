using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace BrainSharp
{
    public class Parser
    {
        private StringBuilder code = new StringBuilder();
        private string comment = "";
        private int tabs = 0;
        private CompilerResults results;

        public Parser()
        {

        }

        public override string ToString()
        {
            return PreCode + code.ToString() + PostCode;
        }

        public void Parse(string bf)
        {
            code.Clear();

            /*
             * At this point the code already contains the following:
             * byte[255] stack
             * byte pointer
             * string input
             * int inputPointer
             * */

            // Stack and pointer changes
            int sChange = 0;
            int pChange = 0;

            // Parse all characters
            for (int i = 0; i < bf.Length; i++)
            {
                // Stack changes
                if (bf[i] == '+')
                    sChange++;
                else if (bf[i] == '-')
                    sChange--;
                else if (sChange != 0)
                {
                    // Add the instruction
                    Instruction("stack[pointer] " + (sChange > 0 ? "+" : "-") + "= " + Math.Abs(sChange) + ";");
                    sChange = 0;
                }

                // Pointer changes
                if (bf[i] == '>')
                    pChange++;
                else if (bf[i] == '<')
                    pChange--;
                else if (pChange != 0)
                {
                    // Add the instruction
                    Instruction("pointer " + (pChange > 0 ? "+" : "-") + "= " + Math.Abs(pChange) + ";");
                    pChange = 0;
                }

                // Other characters
                switch(bf[i])
                {
                    // Stack and pointer changes are already handled (see above)
                    case '+':
                    case '-':
                    case '>':
                    case '<':
                        break;
                    // Start while
                    case '[':
                        Instruction("while (stack[pointer] != 0)");
                        Instruction("{");
                        tabs++;
                        break;
                    // End while
                    case ']':
                        tabs--;
                        Instruction("}");
                        break;
                    // Set stack at pointer to input
                    case ',':
                        Instruction("stack[pointer] = (byte)(inputPointer < input.Length ? input[inputPointer++] : 0);");
                        break;
                    // Output
                    case '.':
                        Instruction("Console.Write((char)stack[pointer]);");
                        break;
                    // Comment
                    default:
                        comment += bf[i];
                        break;
                }
            }
        }

        /// <summary>
        /// Compile the program.
        /// </summary>
        /// <param name="filename">The filename to save to, or null to not save the executable.</param>
        /// <returns>A boolean indicating whether compilation was successful.</returns>
        public bool Compile(string filename)
        {
            // Compile the code
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters(new[] { "System.dll" }, filename);
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;
            results = provider.CompileAssemblyFromSource(parameters, ToString());

            // Check if there are any errors
            if (results.Errors.Count > 0)
            {
                foreach (var error in results.Errors)
                    Console.WriteLine(error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Run the compiled program.
        /// </summary>
        public void Run()
        {
            // Run the program
            if (results != null && results.Errors.Count == 0)
            {
                Assembly assembly = results.CompiledAssembly;
                Type program = assembly.GetType("brainfuck.Program");
                MethodInfo main = program.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic);
                main.Invoke(null, new object[] { new string[0] });
            }
        }

        private void Instruction(string i)
        {
            // Check if we need add a comment
            if (comment.Trim() != "")
            {
                comment = comment.Trim();
                AddTabbing();
                code.Append("/* ").Append(comment).AppendLine(" */");
                comment = "";
            }

            // Add the instruction followed by a newline
            AddTabbing();
            code.AppendLine(i);
        }

        private void AddTabbing()
        {
            // Add the set amount of tabs
            for (int t = 0; t < BaseTabs + tabs; t++)
                code.Append('\t');
        }

        #region Code snippets
        private const int BaseTabs = 3;

        private const string PreCode =
@"using System;

namespace brainfuck
{
    public class Program
    {
        private static byte[] stack = new byte[255];
        private static byte pointer = 0;
        private static string input = String.Empty;
        private static int inputPointer = 0;
        
        private static void Main(string[] args)
        {
";

        // Code goes here

        private const string PostCode = 
@"            Console.Read();
        }
    }
}";
        #endregion
    }
}
