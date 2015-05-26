# BrainSharp
A brainfuck to C# converter and compiler.

BrainSharp is a simple command-line tool that allows you to convert brainfuck code to C#, and even compile and run it. You can output the C# code and/or executable, or do neither and only run it.

### Arguments
| Argument | Description |
|---|---|
| -f &lt;filename&gt; | Read brainfuck code from file. |
| -t &lt;code&gt; | Read brainfuck code as argument. When both a file and code are supplied, the code is ignored. |
| -c &lt;filename&gt; | Save C# code to file. |
| -x &lt;filename&gt; | Save executable to file. |
| -o | Overwrite existing files. |
| -r | Run the compiled program. |
| -i &lt;input&gt; | Use input when running the program. |

### How it works
BrainSharp first reads the entire brainfuck code, converting every character to an instruction and adding it to a list. It then iterates over that list, merging instructions where possible. For example multiple pointer changes (pointer += value) can always be merged into a single instruction. This greatly reduces the amount of C# code that is generated. All the generated code is inserted into a predefined class with a Main method, like any simple C# program. This class contains some predefined variables, like the pointer, stack and input.

When the C# code is generated it can be compiled at runtime. If that succeeds, the compiled assembly can be used to run the Main method using reflection. Input is taken from the arguments and output is written to the console.
