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
    }
}
