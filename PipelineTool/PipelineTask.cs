using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;

public abstract class PipelineTask
{
    public ParameterGroup Parameters { get; } = new();

    protected Runtime Runtime { get; private set; }

    public bool CanParse { get; protected set; }

    public int Scope { get; set; }

    public bool Enabled { get; set; } = true;

    protected PipelineTask(bool autoinit)
    {
        Runtime = null!;

        if (autoinit)
            Init();
    }

    public PipelineTask() : this(true) { }

    public void Init()
    {
        OnInit();

        if (Parameters.Count == 0)
        {
            var infos = GetType().GetProperties();

            foreach (var info in infos)
            {
                if (info.PropertyType.IsAssignableTo(typeof(Parameter)))
                {
                    var obj = (Parameter?)info.GetValue(this);

                    if (obj == null)
                        throw new NullReferenceException();

                    Parameters.Add(obj);
                }

            }
        }

        Parameters.Seal();
    }

    protected abstract void OnInit();

    public void Execute(Runtime runtime)
    {
        if (!Enabled) 
            return;

        ArgumentNullException.ThrowIfNull(nameof(runtime));

        Parameters.AssertSealed();
        Runtime = runtime;
        OnExecute();
    }

    public void Update() => OnUpdate();
    protected virtual void OnUpdate()
    {

    }

    protected abstract void OnExecute();

    public virtual void Validate()
    {

    }

    public void Parse(string text)
    {
        OnParse(text);
    }

    protected virtual void OnParse(string text)
    {
        throw new NotImplementedException();
    }

    public virtual PipelineTask Clone()
    {
        var type = GetType();
        var clone = PipelineTaskList.CreateUnbound(type);

        var keys = Parameters.Keys;
        foreach (var key in keys)
        {
            clone.Parameters[key] = Parameters[key];
        }

        return clone;
    }

    public void CloneTo(Pipeline target)
    {
        var clone = Clone();
        target.Tasks.Add(clone);
    }

    public enum TokenType
    {
        Text,
        Variable,
        Comment,
        Error,
        Flow,
    }

    public record class Token(TokenType Type, string Text)
    {
        public static implicit operator Token((TokenType, string) tuple)
        {
            return new Token(tuple.Item1, tuple.Item2);
        }

        public static implicit operator Token(string text)
        {
            return new Token(TokenType.Text, text);
        }
    }

    public virtual Token[] ToTokens()
    {
        string name = GetType().Name;
        var values = new List<string>();
        foreach (var para in Parameters)
        {
            values.Add(para.Value);
        }

        var tokens = new List<Token>();

        int count = Parameters.Count;

        void Add(TokenType type, string text) =>
            tokens.Add(new Token(type, text));

        Add(TokenType.Text, name);
        Add(TokenType.Text, "(");
        for (int i = 0; i < count; i++)
        {
            Add(TokenType.Variable, values[i]);
            if (i < count - 1)
            {
                Add(TokenType.Text, ",");
            }
        }
        Add(TokenType.Text, ")");

        return tokens.ToArray();
    }

    public sealed override string ToString()
    {
        var tokens = ToTokens();
        var sb = new StringBuilder();
        foreach (var token in tokens)
        {
            sb.Append(token.Text.ToString());
        }
        return sb.ToString();
    }

    protected string EvalParameter(Parameter parameter)
    {
        ArgumentNullException.ThrowIfNull(Runtime);

        return Runtime.EvalParameter(parameter);
    }
    protected string EvalParameter(in string name)
    {
        ArgumentNullException.ThrowIfNull(Runtime);

        var value = Parameters[name];
        return Runtime.EvalParameterValue(value);
    }
}
