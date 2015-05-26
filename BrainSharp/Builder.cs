using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BrainSharp.Instructions;
using Microsoft.CSharp;

namespace BrainSharp
{
    public class Builder
    {
        private InstructionList instructions;
        private string comment = String.Empty;
        private CompilerResults results;

        public Builder()
        {
            instructions = new InstructionList();
        }

        public void Parse(string bf)
        {
            // Parse all characters
            foreach (char c in bf)
            {
                switch (c)
                {
                    case '+':
                        instructions.AddInstruction(new StackInstruction(1));
                        break;
                    case '-':
                        instructions.AddInstruction(new StackInstruction(-1));
                        break;
                    case '>':
                        instructions.AddInstruction(new PointerInstruction(1));
                        break;
                    case '<':
                        instructions.AddInstruction(new PointerInstruction(-1));
                        break;
                    case '[':
                        instructions.AddInstruction(new LoopStart());
                        break;
                    case ']':
                        instructions.AddInstruction(new LoopEnd());
                        break;
                    case ',':
                        instructions.AddInstruction(new InputInstruction());
                        break;
                    case '.':
                        instructions.AddInstruction(new PrintInstruction());
                        break;
                    default:
                        instructions.AddInstruction(new Comment(c.ToString()));
                        break;
                }
            }
        }

        public string GetCode()
        {
            return instructions.GetCode();
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
            results = provider.CompileAssemblyFromSource(parameters, GetCode());

            // Check if there are any errors
            if (results.Errors.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(results.Errors.Count + " error(s) occurred during compilation:");
                Console.ResetColor();

                // Print each error
                foreach (CompilerError error in results.Errors)
                    Console.WriteLine((error.IsWarning ? "Warning " : "Error ") + error.ErrorNumber + " at (" + error.Line + "," + error.Column + "): " + error.ErrorText);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Run the compiled program.
        /// </summary>
        public void Run(string input)
        {
            // Run the program
            if (results != null && results.Errors.Count == 0)
            {
                Assembly assembly = results.CompiledAssembly;
                Type program = assembly.GetType("brainfuck.Program");
                MethodInfo main = program.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic);
                main.Invoke(null, new object[] { new[] { input } });
            }
        }
    }
}
