using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.IO;
public interface ILogger
{
    public void Clear();

    public void System(string obj);

    public void Info(string obj);

    public void Warn(string obj);

    public void Error(string obj);

    public string? Input();
}

class ConsoleLogger : ILogger
{
    public static ConsoleLogger Instance { get; } = new ConsoleLogger();

    public void WriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }

    public void System(string obj) => WriteLine(obj, ConsoleColor.DarkGray);

    public void Info(string obj) => WriteLine(obj, ConsoleColor.Gray);

    public void Warn(string obj) => WriteLine(obj, ConsoleColor.DarkYellow);

    public void Error(string obj) => WriteLine(obj, ConsoleColor.Red);

    public string? Input() => Console.ReadLine();

    public void Clear() => Console.Clear();
}