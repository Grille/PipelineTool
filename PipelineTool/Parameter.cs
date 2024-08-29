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
    public static Parameter Create(ParameterTypes type, string name, string desc, string value, object? args) => type switch
    {
        ParameterTypes.Enum => new ParameterEnum(name, desc, value, (string[])args!),
        ParameterTypes.Integer => new ParameterInteger(name, desc, value),
        ParameterTypes.Single => new ParameterSingle(name, desc, value),
        ParameterTypes.Boolean => new ParameterBoolean(name, desc, value),
        ParameterTypes.String => new ParameterString(name, desc, value),
        ParameterTypes.OpenFile => new ParameterPath(name, desc, value, PathBoxMode.OpenFile),
        ParameterTypes.SaveFile => new ParameterPath(name, desc, value, PathBoxMode.SaveFile),
        ParameterTypes.Directory => new ParameterPath(name, desc, value, PathBoxMode.Directory),
        ParameterTypes.Generic => new ParameterPath(name, desc, value, PathBoxMode.Generic),
        ParameterTypes.Color => new ParameterPath(name, desc, value, PathBoxMode.Color),
        ParameterTypes.Variable => new ParameterVariable(name, desc, value),
        _ => throw new NotImplementedException()
    };
}

public abstract class Parameter
{
    public string Name { get; }
    public string Description { get; }
    public bool Enabled { get; set; } = true;
    public string Value { get; set; }

    public Parameter(string name, string desc, string value)
    {
        Name = name;
        Description = desc;
        Value = value;
    }

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

public class ParameterString : Parameter
{
    public ParameterString(string name, string desc, string value) : base(name, desc, value)
    {
    }
}

public class ParameterInteger : Parameter
{
    public ParameterInteger(string name, string desc, string value) : base(name, desc, value)
    {
    }

    public override bool ValidateValue()
    {
        return int.TryParse(Value, out _);
    }
}

public class ParameterSingle : Parameter
{
    public ParameterSingle(string name, string desc, string value) : base(name, desc, value)
    {
    }

    public override bool ValidateValue()
    {
        return float.TryParse(Value, out _);
    }
}

public class ParameterVariable : Parameter
{
    public ParameterVariable(string name, string desc, string value) : base(name, desc, value) { }

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
    public ParameterBoolean(string name, string desc, string value) : base(name, desc, value, new string[] { "false", "true"})
    {
    }
}

public class ParameterPath : Parameter
{
    private PathBoxMode _mode;
    public ParameterPath(string name, string desc, string value, PathBoxMode mode) : base(name, desc, value)
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
        obj.Text = Value;
        return obj;
    }
}

public class ParameterEnum : Parameter
{
    public readonly string[] Args;
    public ParameterEnum(string name, string desc, string value, string[] args) : base(name, desc, value)
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
    Color,
    Variable,
}
