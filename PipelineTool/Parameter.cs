using Grille.PipelineTool.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Grille.PipelineTool.Expressions;
using System.Diagnostics.CodeAnalysis;
using Grille.PipelineTool.WinForms.Controls;

namespace Grille.PipelineTool;

public static class ParameterFactory
{
    public static Parameter Create(ParameterTypes type, string? name, string? desc = null, string? value = null, object? args = null) 
    {
        var para = Create(type, args);
        para.Name = name;
        para.Description = desc;
        para.Value = value;

        return para;
    }

    private static Parameter Create(ParameterTypes type, object? args) => type switch
    {
        ParameterTypes.Enum => new ParameterEnum((string[])args!),
        ParameterTypes.Integer => new ParameterInteger(),
        ParameterTypes.Single => new ParameterSingle(),
        ParameterTypes.Number => new ParameterNumber(),
        ParameterTypes.Boolean => new ParameterBoolean(),
        ParameterTypes.String => new ParameterString(),
        ParameterTypes.OpenFile => new ParameterPath(PathBoxMode.OpenFile),
        ParameterTypes.SaveFile => new ParameterPath(PathBoxMode.SaveFile),
        ParameterTypes.Directory => new ParameterPath(PathBoxMode.Directory),
        ParameterTypes.Generic => new ParameterPath(PathBoxMode.Generic),
        ParameterTypes.Color => new ParameterPath(PathBoxMode.Color),
        ParameterTypes.Variable => new ParameterVariable(),
        ParameterTypes.Object => new ParameterObject(),
        _ => throw new NotImplementedException()
    };
}

public abstract class Parameter
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; } = true;
    public string? Value { get; set; }

    public virtual bool ValidateValue()
    {
        return true;
    }

    public virtual Control CreateControl()
    {
        var obj = new TextBox();
        obj.Text = Value;
        return obj;
    }
}

public class ParameterObject : Parameter
{
    public override bool ValidateValue()
    {
        if (Value == null) return false;
        return Value.Length > 0 && Value[0] == '*';
    }
}

public class ParameterString : Parameter
{
}

public class ParameterInteger : Parameter
{
    public override bool ValidateValue()
    {
        return int.TryParse(Value, out _);
    }
}

public class ParameterSingle : Parameter
{
    public override bool ValidateValue()
    {
        return float.TryParse(Value, out _);
    }
}

public class ParameterNumber : Parameter
{
    public override bool ValidateValue()
    {
        return decimal.TryParse(Value, out _);
    }
}

public class ParameterVariable : Parameter
{
    public override bool ValidateValue()
    {
        if (string.IsNullOrEmpty(Value))
        {
            return false;
        }
        var trim = Value.Trim();
        var char0 = Value[0];
        return Value.Length == trim.Length && Char(char0);
    }

    static bool Char(char char0)
    {
        return (char0 >= 'A' && char0 <= 'Z') || (char0 >= 'a' && char0 <= 'z') || (char0 >= '0' && char0 <= '9') || char0 == '_';
    }
}

public class ParameterBoolean : ParameterEnum
{
    public ParameterBoolean() : base(new string[] { "false", "true"})
    {
    }
}

public class ParameterPath : Parameter
{
    private PathBoxMode _mode;
    public ParameterPath(PathBoxMode mode)
    {
        _mode = mode;
    }

    public override bool ValidateValue() => _mode switch
    {
        PathBoxMode.Directory => Directory.Exists(Value),
        PathBoxMode.OpenFile => File.Exists(Value),
        PathBoxMode.Color => int.TryParse(Value, NumberStyles.HexNumber, null, out _),
        _ => true,
    };

    public override Control CreateControl()
    {
        var obj = new PathBox(_mode);
        if (Value != null)
        {
            obj.Text = Value;
        }
        return obj;
    }
}

public class ParameterEnum : Parameter
{
    public readonly string[] Args;
    public ParameterEnum(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        Args = args;
    }

    public override bool ValidateValue()
    {
        return Args.Contains(Value);
    }

    public override Control CreateControl()
    {
        var obj = new ComboBox();
        obj.BeginUpdate();
        foreach (var it in Args)
        {
            obj.Items.Add(it);
        }
        obj.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        if (ValidateValue())
            obj.SelectedItem = Value;
        else
            obj.SelectedText = Value;
        obj.EndUpdate();
        return obj;
    }
}

public enum ParameterTypes
{
    String,
    OpenFile,
    SaveFile,
    Directory,
    Integer,
    Single,
    Boolean,
    Enum,
    Generic,
    Number,
    Color,
    Object,
    Variable,
}
