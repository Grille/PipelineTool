using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Tree;

internal class TaskTypeTreeView : TreeView
{
    public Type? SelectedType { 
        get {
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
        EndUpdate();

        ShowNodeToolTips = true;
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
