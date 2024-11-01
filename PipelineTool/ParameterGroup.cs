using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;

public class ParameterGroup : IEnumerable<Parameter>
{
    public bool IsSealed { get; private set; } = false;

    private List<string> keys = new();
    private List<Parameter> values = new();

    public Parameter Def(ParameterTypes type, string name, string? desc = null, string? value = null, object? args = null)
    {
        if (IsSealed == true)
            throw new InvalidOperationException();

        var parameter = ParameterFactory.Create(type, name, desc, value, args);
        Add(parameter);
        return parameter;
    }

    public Parameter DefResult(string name = "Result", string? desc = null)
    {
        if (desc == null)
        {
            desc = $"Variable to write {name.ToLower()} into.";
        }
        return Def(ParameterTypes.Variable, name, desc, name);
    }

    public void Add(Parameter parameter)
    {
        if (IsSealed == true)
            throw new InvalidOperationException();

        if (parameter.Name == null)
            throw new InvalidOperationException();

        if (keys.Contains(parameter.Name))
            throw new InvalidOperationException();

        keys.Add(parameter.Name);
        values.Add(parameter);
    }

    public void Add(params Parameter[] parameters)
    {
        foreach (var parameter in parameters)
        {
            Add(parameter);
        }
    }

    public void Seal()
    {
        IsSealed = true;
    }

    public string? this[int index]
    {
        get
        {
            AssertSealed();
            return values[index].Value;
        }
        set
        {
            AssertSealed();
            values[index].Value = value;
        }
    }

    public string? this[string name]
    {
        get
        {
            int index = keys.IndexOf(name);
            if (index == -1) throw new KeyNotFoundException(name);
            return this[index];
        }
        set
        {
            int index = keys.IndexOf(name);
            if (index == -1) throw new KeyNotFoundException(name);
            this[index] = value;
        }
    }

    public string[] Keys => keys.ToArray();

    public int Count => keys.Count;

    public Parameter GetParameter(int key) => values[key];

    public IEnumerator<Parameter> GetEnumerator() => values.GetEnumerator();

    public void AssertSealed()
    {
        if (IsSealed == false)
            throw new InvalidOperationException();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
