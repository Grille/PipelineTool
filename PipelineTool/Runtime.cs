using Grille.PipelineTool.Expressions;
using Grille.PipelineTool.IO;
using Grille.PipelineTool.Tasks;
using Grille.PipelineTool.WinForms;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grille.PipelineTool;

public class Runtime
{
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

    public IUserInterface UserInterface { get; set; }

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
        UserInterface = new GuiInterface();
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
        return ExpressionEvaluator.Eval(parameter.Value, this);
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
            {
                return count;
            }

            var task = tasks[idx];

            if (task.Scope <= scope && task is not NopTask)
            {
                return count;
            }

            count += 1;
        }
    }
}
