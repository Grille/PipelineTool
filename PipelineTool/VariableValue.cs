using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;
public struct VariableValue
{
    public object Value { get; set; }

    public VariableValue(object value)
    {
        Value = value;
    }

    public T GetAs<T>() { return (T)Value; }

    public bool TryGetAs<T>([MaybeNullWhen(false)] out T obj)
    {
        if (Value != null && Value is T)
        {
            obj = (T)Value;
            return true;
        }
        obj = default;
        return false;
    }

    public static implicit operator VariableValue(string value) => new VariableValue(value);

    public static implicit operator string(VariableValue value) => value.ToStringOrEmpty();

    string ToStringOrEmpty()
    {
        var value = Value.ToString();
        if (value == null)
            return string.Empty;
        return value;
    }

    public override string ToString() => ToStringOrEmpty();
}