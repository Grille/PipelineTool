using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.IO;
public interface ILogger
{
    public void Info(string obj);

    public void Warn(string obj);

    public void Error(string obj);
}

class ConsoleLogger : ILogger
{
    public void WriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }

    public void Info(string obj) => WriteLine(obj, ConsoleColor.Gray);

    public void Warn(string obj) => WriteLine(obj, ConsoleColor.DarkYellow);

    public void Error(string obj) => WriteLine(obj, ConsoleColor.Red);
}