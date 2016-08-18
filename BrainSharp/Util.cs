using System;

namespace BrainSharp
{
    public static class Util
    {
        /// <summary>
        /// Write a line to the console in a certain color.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="color">The color to write in.</param>
        public static void WriteLine(object value, ConsoleColor color)
        {
            // Save the old color, set color, write, reset color.
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Get the value for an argument if it is present.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        /// <param name="index">The index of the argument value.</param>
        /// <param name="argument">The name of the argument.</param>
        /// <param name="what">What the value of the argument should contain.</param>
        /// <returns></returns>
        public static string GetArgumentValue(string[] args, int index, string argument, string what)
        {
            // Check if the value is present. Otherwise write an error and return null.
            if (index < args.Length)
                return args[index];
            else
                Util.WriteLine("Found argument " + argument + " without " + what + " following.", ConsoleColor.Yellow);
            return null;
        }
    }
}
