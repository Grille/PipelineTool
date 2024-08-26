using Grille.PipelineTool.WinForms.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Controls;

internal class TaskTypeTreeView : TreeView
{
    List<TreeNode> _savedNodes;

    public Type? SelectedType
    {
        get
        {
            var node = SelectedNode;

            if (node is not TypeTreeNode)
                return null;

            return ((TypeTreeNode)node).Type;
        }
        set
        {
            if (AssemblyTaskTypeTree.TryGetTypeInfo(value, out var node))
                SelectedNode = node;
            else
                SelectedNode = null;
        }
    }

    public TaskTypeTreeView()
    {
        BeginUpdate();
        Nodes.Clear();
        AssemblyTaskTypeTree.ApplyTo(Nodes);
        ExpandAll();
        EndUpdate();

        ShowNodeToolTips = true;

        _savedNodes = new List<TreeNode>();
    }

    public void Save()
    {
        _savedNodes.Clear();
        foreach (var node in Nodes)
        {
            Save((TreeNode)node);
        }
    }

    public void Save(TreeNode node)
    {
        if (node.IsExpanded)
        {
            _savedNodes.Add(node);
            if (node.Nodes.Count > 0)
            {
                foreach (var cnode in node.Nodes)
                {
                    Save((TreeNode)cnode);
                }
            }
        }

    }

    public void Restore()
    {
        foreach (var node in _savedNodes)
        {
            node.Expand();
        }
    }

    protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
    {
        e.Cancel = e.Node is not TypeTreeNode;

        base.OnBeforeSelect(e);
    }

    protected override void OnAfterSelect(TreeViewEventArgs e)
    {
        base.OnAfterSelect(e);
    }
}
