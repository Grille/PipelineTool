using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grille.PipelineTool.WinForms.Tree;

public class TypeTreeNode : TreeNode
{
    public Type Type { get; }

    public string[] NamespacePath { get; }

    public string Key { get; }

    public string? Description { get; }

    public bool Visible { get; init; }

    public TypeTreeNode(Type type, string key, string? description)
    {
        Type = type;
        Key = key;
        Description = description;
        ToolTipText = description;

        var split = key.Split('/');
        var name = split[split.Length - 1];

        if (split.Length < 1)
            throw new Exception();
        var path = new string[split.Length - 1];
        Array.Copy(split, path, path.Length);

        Name = name;
        NamespacePath = path;

        Text = Name;
    }

    public TypeTreeNode(Type type, string key) :
        this(type, key, "")
    { }

    public TypeTreeNode(Type type, PipelineTaskAttribute attribute) :
        this(type, attribute.Key, attribute.Description)
    { }

    public override string ToString() => Key;

}

