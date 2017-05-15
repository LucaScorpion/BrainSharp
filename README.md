# BrainSharp
A brainfuck to C# converter and compiler.

BrainSharp is a simple command-line tool that allows you to convert brainfuck code to C#, and even compile and run it. You can output the C# code and/or executable, or do neither and only run it.

### Arguments
| Argument | Description |
|---|---|
| -h, --help | Display the help. |
| -f, --file &lt;file&gt; | Read brainfuck code from &lt;file&gt;. Cannot be used in combination with -c. |
| -c, --code &lt;code&gt; | The brainfuck code to use. Cannot be used in combination with -f. |
| -r, --run | Run the brainfuck code. |
| -i, --input &lt;input&gt; | Use input when running the program. |
| -s, --save &lt;path&gt; | Save the generated C# code to &lt;path&gt;. |
| -x, --executable &lt;path&gt; | Save the generated executable to &lt;path&gt;. |
| -o, --overwrite | Overwrite any existing files. |

### How it works
BrainSharp first reads the entire brainfuck code, converting every character to an instruction and adding it to a list. It then iterates over that list, merging instructions where possible. For example multiple pointer changes (pointer += value) can always be merged into a single instruction. This greatly reduces the amount of C# code that is generated. All the generated code is inserted into a predefined class with a Main method, like any simple C# program. This class contains some predefined variables, like the pointer, stack and input.

When the C# code is generated it can be compiled at runtime. If that succeeds, the compiled assembly can be used to run the Main method using reflection. Input is taken from the arguments and output is written to the console.
