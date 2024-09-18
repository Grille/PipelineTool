using System;
using System.Collections;
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

    public static implicit operator string(VariableValue value) => value.ToString();

    public IEnumerable<object> GetEnumerator()
    {
        bool isIEnumerable = Value is IEnumerable;
        bool isString = Value is string;

        if (isString)
        {
            var split = ToString().Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in split)
            {
                yield return item.Trim();
            }
        }
        else if (isIEnumerable)
        {
            foreach (var item in (IEnumerable)Value)
            {
                yield return item;
            }
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public override string ToString()
    {
        if (Value == null)
        {
            return string.Empty;
        }

        if (Value is string str)
        {
            return str;
        }

        if (Value is IEnumerable enumerable)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var item in enumerable)
            {
                if (!first)
                {
                    sb.Append(",");
                }
                first = false;
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }

        var value = Value.ToString();

        return value == null ? string.Empty : value;
    }
}