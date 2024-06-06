using Grille.PipelineTool.Tasks;
using Grille.PipelineTool.Tasks.Program;
using Grille.PipelineTool.Tasks.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grille.PipelineTool.IO;

public static class TextSerializer
{
    private static class TypeRegistry
    {
        readonly static Dictionary<Type, string> _byType;
        readonly static Dictionary<string, Type> _byKey;

        static TypeRegistry()
        {
            _byType = new();
            _byKey = new();

            Register<Pop>("Pop");
            Register<Push>("Push");
            Register<ExecutePipeline>("Call");
            Register<ForEach>("ForEach");
            Register<For>("For");
            Register<If>("If");
            Register<Input>("In");
            Register<Return>("Return");
            Register<VariableOperation>("$");

            Register<StringContains>("String.Contains");
            Register<StringReplace>("String.Replace");
        }

        public static void Register<T>(string key) where T : PipelineTask
        {
            var type = typeof(T);
            _byKey.Add(key, type);
            _byType.Add(type, key);
        }

        internal static PipelineTask Get(string key, int argCount)
        {
            Type? type;
            if (!_byKey.TryGetValue(key, out type))
            {
                var split = key.Split(':', 2);
                if (split.Length == 2)
                {
                    type = Type.GetType($"{split[1]}, {split[0]}");
                }
                else
                {
                    type = Type.GetType(key);
                }
            }

            if (type == null)
                return new InvalidTypeTask(key, argCount);

            return (PipelineTask?)Activator.CreateInstance(type) ?? throw new InvalidOperationException();
        }

        public static string Get(PipelineTask task)
        {
            if (task is InvalidTypeTask itask)
                return itask.AssemblyQualifiedName;

            var type = task.GetType();
            if (_byType.TryGetValue(type, out var key))
                return key;
            
            return $"{type.Assembly.GetName().Name}:{type.FullName}";
        }
    }

    const int Spaces = 4;

    public static void Register<T>(string key) where T : PipelineTask => Register<T>(key);

    private static StreamWriter CreateWriter(Stream stream)
    {
        var writer = new StreamWriter(stream, leaveOpen: true);
        writer.WriteLine($"#Version 1");
        writer.WriteLine($"#Spaces 4");
        writer.WriteLine();
        writer.Flush();
        return writer;
    }

    public static void Serialize(Stream stream, IList<Pipeline> pipelines)
    {
        using var writer = CreateWriter(stream);
        Serialize(writer, pipelines);
    }

    public static void Serialize(Stream stream, IList<PipelineTask> tasks)
    {
        using var writer = CreateWriter(stream);
        Serialize(writer, tasks);
    }

    private static void Serialize(StreamWriter writer, IList<Pipeline> pipelines)
    {
        foreach (var pipeline in pipelines)
        {
            Serialize(writer, pipeline);
        }
    }

    private static void Serialize(StreamWriter writer, Pipeline pipeline)
    {
        writer.WriteLine($"[{pipeline.Name}]");
        writer.Flush();
        Serialize(writer, pipeline.Tasks);
        writer.WriteLine();
    }

    private static void Serialize(StreamWriter writer, IList<PipelineTask> tasks)
    {
        foreach (var task in tasks)
        {
            Serialize(writer, task);
        }
    }

    private static void Serialize(StreamWriter writer, PipelineTask task)
    {
        for (int i = 0; i < task.Scope * Spaces; i++)
        {
            writer.Write(" ");
        }

        if (task is NopTask)
        {
            var text = task.Parameters["Text"];
            writer.Write("//");
            writer.Write(text);
            writer.WriteLine();
            return;
        }

        if (!task.Enabled)
        {
            writer.Write("!");
        }

        string name = TypeRegistry.Get(task);

        writer.Write(name);
        writer.Write("(");

        var parameters = task.Parameters;
        for (int i = 0; i < parameters.Count; i++)
        {
            var value = parameters[i];
            Write(writer, value);
            if (i < parameters.Count - 1)
            {
                writer.Write(",");
            }
        }

        writer.Write(")");
        writer.WriteLine();
    }

    private static void Write(TextWriter tw, string text) {
        tw.Write('"');
        tw.Write(text.Replace("\"", "\\\""));
        tw.Write('"');
    }

    public class Result
    {
        private List<PipelineTask> _tasks;

        public List<PipelineTask> Headless { get; }

        public Dictionary<string, List<PipelineTask>> Sections { get; }

        public Result()
        {
            _tasks = new List<PipelineTask>();
            Sections = new Dictionary<string, List<PipelineTask>>();
            Headless = _tasks;
        }

        [MemberNotNull(nameof(_tasks))]
        public void Section(string name)
        {
            _tasks = new List<PipelineTask>();
            Sections.Add(name, _tasks);
        }

        public void Add(PipelineTask task)
        {
            _tasks.Add(task);
        }
    }

    public static void Deserialize(Stream stream, PipelineList pipelines)
    {
        var result = Deserialize(stream);

        foreach (var pair in result.Sections)
        {
            var pipeline = pipelines.Create(pair.Key);

            foreach (var task in pair.Value)
            {
                pipeline.Tasks.Add(task);
            }
        }
    }

    public static Result Deserialize(Stream stream)
    {
        using var tr = new StreamReader(stream);

        var result = new Result();

        int spaces = Spaces;

        while (true)
        {
            var line = tr.ReadLine();
            if (line == null)
                break;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            int scope = GetScope(line, spaces);
            var tline = line.Trim();

            if (tline.StartsWith('#'))
            {
                var split = tline.Split(' ', 2);
                var key = split[0].ToLower();

                if (key == "#spaces" && split.Length == 2)
                {
                    spaces = int.Parse(split[1]);
                }

                continue;
            }

            if (tline.StartsWith("//"))
            {
                var text = tline.Substring(2);
                result.Add(new NopTask() { Text = text });
                continue;
            }

            if (tline.StartsWith("["))
            {
                var sectionName = tline.Substring(1, tline.Length-2);
                result.Section(sectionName);
                continue;
            }

            {
                bool enabled = !tline.StartsWith('!');

                var split = tline.Split('(', 2);
                if (split.Length != 2)
                    continue;

                var taskName = enabled ? split[0] : split[0].Substring(1);

                var args = ParseArgs("(" + split[1]);

                var task = TypeRegistry.Get(taskName, args.Length);
                task.Scope = scope;
                for (int i = 0; i < args.Length; i++)
                {
                    task.Parameters[i] = args[i];
                }

                task.Enabled = enabled;

                result.Add(task);
            }
        }

        return result;
    }

    private static string[] ParseArgs(string input)
    {
        var matches = Regex.Matches(input, "\"((?:\\\\\"|[^\"])*)\"");

        string[] result = new string[matches.Count];
        for (int i = 0; i < matches.Count; i++)
        {
            result[i] = matches[i].Groups[1].Value.Replace("\\\"", "\"");
        }

        return result;
    }

    private static int GetScope(string line, int spaces)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] != ' ')
            {
                return i / spaces;
            }
        }
        return 0;
    }


}
