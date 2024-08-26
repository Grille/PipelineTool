using Grille.PipelineTool.Tasks.Program;
using Grille.PipelineTool.Tasks.Text;
using Grille.PipelineTool.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grille.PipelineTool.Tasks.Program.Flow;
using Grille.PipelineTool.Tasks.Program.Stack;

namespace Grille.PipelineTool.IO;
public static partial class TextSerializer
{
    public static class TypeRegistry
    {
        readonly static Dictionary<Type, string> _byType;
        readonly static Dictionary<string, Type> _byKey;

        static TypeRegistry()
        {
            _byType = new();
            _byKey = new();

            Register<Pop>("Pop");
            Register<Push>("Push");
            Register<Exch>("Exch");
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
}