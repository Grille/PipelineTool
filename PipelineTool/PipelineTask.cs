﻿using Grille.PipelineTool.Expressions;
using Grille.PipelineTool.IO;
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

    public static PipelineTask FromType(string assemblyQualifiedName)
    {
        var type = Type.GetType(assemblyQualifiedName);
        if (type == null)
            throw new ArgumentException($"Type {assemblyQualifiedName} could not be resolved.");

        return FromType(type);
    }

    public static PipelineTask FromType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var obj = Activator.CreateInstance(type);
        if (obj == null)
            throw new ArgumentException("Instance creation failed.");

        var task = (PipelineTask)obj;
        return task;
    }

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
                    {
                        throw new NullReferenceException();
                    }

                    if (obj.Name == null)
                    {
                        obj.Name = info.Name;
                    }

                    Parameters.Add(obj);
                }

            }
        }

        Parameters.Seal();
    }

    protected virtual void OnInit() { }

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
        var clone = FromType(type);

        var keys = Parameters.Keys;
        foreach (var key in keys)
        {
            clone.Parameters[key] = Parameters[key];
        }

        clone.Enabled = Enabled;
        clone.Scope = Scope;

        return clone;
    }

    public void CloneTo(Pipeline target)
    {
        var clone = Clone();
        target.Tasks.Add(clone);
    }

    public virtual Token[] ToTokens()
    {
        string name = GetType().Name;
        var values = new List<string>();
        foreach (var para in Parameters)
        {
            if (para.Value != null)
            {
                values.Add(para.Value);
            }
        }

        var tokens = new List<Token>();

        int count = Parameters.Count;

        void Add(TokenType type, string text) =>
            tokens.Add(new Token(type, text));

        Add(TokenType.Text, name);
        Add(TokenType.Text, "(");
        for (int i = 0; i < count; i++)
        {
            Add(TokenType.Expression, values[i]);
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
            if (string.IsNullOrEmpty(token.Text)) continue;
            sb.Append(token.Text.ToString());
        }
        return sb.ToString();
    }

    protected VariableValue EvalParameter(Parameter parameter)
    {
        ArgumentNullException.ThrowIfNull(Runtime);

        return Runtime.EvalParameter(parameter);
    }

    protected VariableValue EvalParameter(in string name)
    {
        ArgumentNullException.ThrowIfNull(Runtime);

        var value = Parameters[name];
        return ExpressionEvaluator.Eval(value, Runtime);
    }
}
