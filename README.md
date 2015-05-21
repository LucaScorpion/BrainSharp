# BrainSharp
A brainfuck to C# converter and compiler.

BrainSharp is a simple command-line tool that allows you to convert brainfuck code to C#, and even compile and run it. You can output the C# code and/or executable, or do neither and just run it.

### Arguments
| Argument | Description |
|---|---|
| -f &lt;filename&gt; | Read brainfuck code from file. |
| -t &lt;code&gt; | Read brainfuck code as argument. When both a file and code are supplied, the code is ignored. |
| -s &lt;filename&gt; | Save C# code to file |
| -e &lt;filename&gt; | Save executable to file |
| -r | Run the program |
| -o | Overwrite existing files |
