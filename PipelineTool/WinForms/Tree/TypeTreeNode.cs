using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Drawing;

namespace Grille.PipelineTool.WinForms.Tree;

public class TypeTreeNode : TreeNode
{
    public Type Type { get; }

    public string[] NamespacePath { get; }

    public string Key { get; }

    public string? Description { get; }

    public bool Visible { get; init; }

    public TypeTreeNode(Type type, PipelineTaskKind kind, string key, string? description)
    {
        Type = type;
        Key = key;
        Description = description;
        ToolTipText = description;

        (ForeColor, ImageIndex) = kind switch
        {
            PipelineTaskKind.Flow => (Color.Black, 1),
            PipelineTaskKind.Variable => (Color.Navy, 2),
            PipelineTaskKind.Operator => (Color.Navy, 3),
            PipelineTaskKind.Method => (Color.Purple, 4),
            PipelineTaskKind.StringOperation => (Color.Purple, 5),
            PipelineTaskKind.Comment => (Color.Green, 6),
            PipelineTaskKind.OperatorCmp => (Color.Black, 7),
            _ => throw new ArgumentException(),
        };

        SelectedImageIndex = ImageIndex;

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

    public TypeTreeNode(Type type, PipelineTaskAttribute attribute) :
        this(type, attribute.Kind, attribute.Key, attribute.Description)
    { }

    public override string ToString() => Key;

}

