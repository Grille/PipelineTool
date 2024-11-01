using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;
public struct VariableValue
{
    public object Value;

    public VariableValue(object value)
    {
        Value = value;
    }

    public VariableValue(bool value)
    {
        Unsafe.SkipInit(out Value);
        Boolean = value;
    }

    public VariableValue(decimal value)
    {
        Unsafe.SkipInit(out Value);
        Number = value;
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

    public static implicit operator VariableValue(bool value) => new VariableValue(value);

    public static implicit operator VariableValue(decimal value) => new VariableValue(value);

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

    public bool Boolean
    {
        get
        {
            if (Value is decimal d)
            {
                return d > 0;
            }
            if (Value is string s)
            {
                if (s.Length == 0) return false;
                else if (s[0] == 't' || s[0] == 'T') return true;
                else if (s[0] == 'f' || s[0] == 'F') return false;
                else if (decimal.TryParse(Value.ToString(), out d))
                {
                    return d > 0;
                }
            }
            return false;
        }
        set
        {
            Value = value ? 1m : 0m;
        }
    }

    public decimal Number
    {
        get
        {
            if (Value is decimal d)
            {
                return d;
            }
            else if (decimal.TryParse(Value.ToString(), out d))
            {
                return d;
            }
            return 0;
        }
        set
        {
            Value = value;
        }
    }
}