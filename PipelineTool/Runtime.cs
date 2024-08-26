using Grille.PipelineTool.IO;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grille.PipelineTool;

public class Runtime
{
    //public bool Running { get; private set; }

    private bool _cancel;

    private class ReturnException : Exception { }

    public class CallStackEntry
    {
        public Pipeline Pipeline { get; }
        public int Position { get; set; }

        public CallStackEntry(Pipeline pipeline)
        {
            Pipeline = pipeline;
            Position = 0;
        }

        public static implicit operator CallStackEntry(Pipeline pipeline) => new CallStackEntry(pipeline);
    }

    public Pipeline Pipeline => CallStack.Peek().Pipeline;

    public int Position
    {
        get => CallStack.Peek().Position;
        set
        {
            CallStack.Peek().Position = value;
            InvPosChanged();
        }
    }

    public Stack<CallStackEntry> CallStack { get; }

    public Stack<Variables> ScopeStack { get; }

    public Stack<VariableValue> ValueStack { get; }

    public ILogger Logger { get; set; }

    public Variables Variables => ScopeStack.Peek();

    public event EventHandler? PositionChanged;

    public bool InherentParentScopeVariablesEnabled { get; set; } = true;

    public string StackTrace
    {
        get
        {
            var list = CallStack.ToList();
            list.Reverse();
            string stackTrace = "";
            foreach (var item in list)
            {
                stackTrace += $"Pipeline: {item.Pipeline.Name}, Line: {item.Position + 1}\n";
            }
            return stackTrace;
        }
    }

    public Runtime()
    {
        CallStack = new();
        ScopeStack = new();
        ValueStack = new();
        Logger = ConsoleLogger.Instance;
    }

    private void InvPosChanged()
    {
        PositionChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Clear()
    {
        _cancel = false;
        CallStack.Clear();
        ScopeStack.Clear();
        ValueStack.Clear();
        InvPosChanged();
    }

    public void Cancel()
    {
        _cancel = true;
    }

    public void Call(string name)
    {
        var pipeline = Pipeline.Owner[name];
        Call(pipeline);
    }

    public void Call(Pipeline pipeline) 
    {
        if (CallStack.Any(a => a.Pipeline == pipeline))
            throw new InvalidOperationException($"Pipeline already in call stack.");

        if (_cancel)
            return;

        Logger.System($"Call {pipeline.Name}");

        CallStack.Push(pipeline);
        InvPosChanged();
        IncVariableScope();

        try
        {
            ExecuteTasks(pipeline);
        }
        catch (ReturnException) { }

        DecVariableScope();
        CallStack.Pop();
        InvPosChanged();

        Logger.System($"Return");
    }

    public void ExecuteTasks(Pipeline pipeline)
    {
        var tasks = pipeline.Tasks;
        int count = tasks.Count;

        while (Position < count && !_cancel)
        {
            tasks[Position].Execute(this);
            Position += 1;
        }
    }

    public VariableValue EvalParameter(Parameter parameter)
    {
        return EvalParameterValue(parameter.Value);
    }

    public void IncVariableScope()
    {
        var variables = ScopeStack.Count > 0 && InherentParentScopeVariablesEnabled ? new Variables(Variables) : new Variables();
        ScopeStack.Push(variables);
    }

    public void DecVariableScope()
    {
        ScopeStack.Pop();
    }

    public void Return()
    {
        throw new ReturnException();
    }

    public void ExecuteNextBlock()
    {
        var tasks = Pipeline.Tasks;

        var caller = Position;
        int start = Position + 1;
        int length = NextBlockLength();
        int end = start + length;

        for ( int i = start; i < end; i++)
        {
            Position = i;
            tasks[Position].Execute(this);
        }

        Position = caller;
    }

    public void SkipNextBlock()
    {
        int length = NextBlockLength();

        Position += length;
    }

    public int NextBlockLength()
    {
        var tasks = Pipeline.Tasks;
        int scope = tasks[Position].Scope;

        int start = Position + 1;
        var next = tasks[start];
        if (next == null)
            throw new NullReferenceException();

        if (next.Scope <= scope)
        {
            throw new InvalidOperationException("Increased scope block expected.");
        }

        int count = 0;

        while (true)
        {
            int idx = count + start;

            if (idx >= tasks.Count)
                return count;

            if (tasks[idx].Scope <= scope) {
                return count;
            }

            count += 1;
        }
    }

    public VariableValue EvalParameterValue(string value)
    {
        return EvalParameterValue(value.AsMemory());
    }

    public VariableValue EvalParameterValue(ReadOnlyMemory<char> value)
    {
        var tokens = new List<Token>();
        TokenizeParameterValue(value, tokens);

        if (tokens.Count == 1)
        {
            var token = tokens[0];

            switch (token.Type)
            {
                case TokenType.ValueString:
                {
                    return token.Text.ToString();
                }
                case TokenType.ValueVariable:
                {
                    var key = EvalParameterValue(token.Text);
                    return Variables[key];
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }

        var sb = new StringBuilder();

        foreach (var token in tokens) {
            switch (token.Type)
            {
                case TokenType.ValueSymbol:
                {
                    continue;
                }
                case TokenType.ValueString:
                {
                    sb.Append(token.Text);
                    continue;
                }
                case TokenType.ValueVariable:
                {
                    var key = EvalParameterValue(token.Text);
                    sb.Append(Variables[key].Value);
                    continue;
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }

        return sb.ToString();
    }

    public static IReadOnlyList<Token> TokenizeParameterValue(ReadOnlyMemory<char> value)
    {
        var tokens = new List<Token>();
        TokenizeParameterValue(value, tokens);
        return tokens;
    }

    public static void TokenizeParameterValue(ReadOnlyMemory<char> value, List<Token> tokens)
    {
        if (value.Length == 0)
            return;

        if (value.Span[0] == '*')
        {
            tokens.Add(new Token(TokenType.ValueSymbol, value.Slice(0, 1)));
            tokens.Add(new Token(TokenType.ValueVariable, value.Slice(1, value.Length - 1)));
        }
        else if (value.Span[0] == '$')
        {
            tokens.Add(new Token(TokenType.ValueSymbol, value.Slice(0, 1)));

            var slice = value.Slice(1);
            var span = slice.Span;

            int begin = 0;
            bool insideBlock = false;

            for (int i = 0; i < span.Length; i++)
            {
                if (!insideBlock && span[i] == '{')
                {
                    int end = i;
                    int length = end - begin;
                    if (length > 0)
                    {
                        tokens.Add(new Token(TokenType.ValueString, slice.Slice(begin, length)));
                    }
                    tokens.Add(new Token(TokenType.ValueSymbol, slice.Slice(i, 1)));
                    insideBlock = true;
                    begin = i + 1;
                }
                else if (insideBlock && span[i] == '}')
                {
                    int end = i;
                    int length = end - begin;
                    if (length > 0)
                    {
                        TokenizeParameterValue(slice.Slice(begin, length), tokens);
                    }
                    tokens.Add(new Token(TokenType.ValueSymbol, slice.Slice(i, 1)));
                    insideBlock = false;
                    begin = i + 1;
                }
            }
            {
                int end = span.Length;
                int length = end - begin;
                if (length > 0)
                {
                    tokens.Add(new Token(TokenType.ValueString, slice.Slice(begin, length)));
                }
            }
        }
        else
        {
            tokens.Add(new Token(TokenType.ValueString, value));
        }
    }
}
