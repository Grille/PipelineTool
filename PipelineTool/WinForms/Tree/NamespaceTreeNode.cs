using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grille.PipelineTool.WinForms.Tree;

public class NamespaceTreeNode : TreeNode
{
    public Dictionary<string, TypeTreeNode> Values { get; set; } = new();
    public Dictionary<string, NamespaceTreeNode> Groups { get; set; } = new();

    public NamespaceTreeNode(string name)
    {
        Name = name;
        Text = name;
    }

    public void Init()
    {
        foreach (var group in Groups.Values)
        {
            Nodes.Add(group);

            group.Init();
        }

        foreach (var type in Values.Values)
        {
            Nodes.Add(type);
        }
    }
}
