using Grille.PipelineTool.WinForms;
using Grille.PipelineTool.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grille.PipelineTool.WinForms.Tree;
using System.Diagnostics.CodeAnalysis;
using Grille.PipelineTool.Tasks.Program;

namespace Grille.PipelineTool;

public partial class EditTaskForm : Form
{
    public Pipeline? Pipeline { get; private set; }
    public PipelineTask? Task { get; private set; }
    public PipelineTask? OriginalTask { get; private set; }

    List<Control> inputs;

    bool setup = true;

    bool keepValues = false;

    public EditTaskForm()
    {
        InitializeComponent();

        inputs = new List<Control>();
    }

    protected override void OnShown(EventArgs e)
    {
        treeViewTypes.Restore();
        base.OnShown(e);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        treeViewTypes.Save();
        base.OnClosing(e);
    }

    [MemberNotNull(nameof(Pipeline),nameof(Task), nameof(OriginalTask))]
    public void Init(Pipeline pipeline, PipelineTask task)
    {
        if (pipeline == null)
            throw new ArgumentNullException(nameof(pipeline));

        setup = true;
        keepValues = false;

        Pipeline = pipeline;
        OriginalTask = task;

        if (task is InvalidTypeTask)
            keepValues = true;

        if (task != null)
            Task = task.Clone();
        else
            Task = new NopTask();

        SelectCurrentType();
        DisplayParameters();

        setup = false;
    }

    [MemberNotNull(nameof(Pipeline), nameof(Task), nameof(OriginalTask))]
    public DialogResult ShowDialog(IWin32Window owner, Pipeline pipeline, PipelineTask task)
    {
        Init(pipeline, task);
        return ShowDialog(owner);
    }

    public void SelectCurrentType()
    {
        if (Task == null)
        {
            treeViewTypes.SelectedNode = null;
            return;
        }

        var type = Task.GetType();
        treeViewTypes.SelectedType = type;
        var node = treeViewTypes.SelectedNode;
        if (node != null)
        {
            textBoxType.Text = node.ToString();
        }
    }

    public void DisplayParameters()
    {
        var panel = panelParameters;

        if (Task == null)
        {
            panel.Controls.Clear();
            return;
        }

        var type = Task.GetType();
        panel.Controls.Clear();
        inputs.Clear();
        int posY = 0;
        foreach (var param in Task.Parameters)
        {
            var label = new Label();
            label.Text = $"{param.Name}:";
            label.Top = posY + 2;
            label.Width = 100;

            var input = param.CreateControl();

            input.Left = label.Width;
            input.Width = panel.Width - label.Width - 10;

            input.Top = posY;
            input.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            input.TextChanged += Input_TextChanged;

            inputs.Add(input);

            panel.Controls.Add(label);
            panel.Controls.Add(input);

            if (!string.IsNullOrEmpty(param.Description))
            {
                var desc = new Label();
                desc.Text = param.Description;
                posY += 25;
                desc.Top = posY;
                desc.Width = panel.Width;
                desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                panel.Controls.Add(desc);
            }

            posY += 30;
        }

        ApplyInputs();
    }

    private void Input_TextChanged(object? sender, EventArgs e)
    {
        ApplyInputs();
    }

    static void ValidateInput(Control control, Parameter param)
    {
        if (param.Value.Length > 0 && (param.Value[0] == '$' || param.Value[0] == '*'))
        {
            control.ForeColor = Color.Blue;
        }
        else if (param.ValidateValue())
        {
            control.ForeColor = Color.Black;
        }
        else
        {
            control.ForeColor = Color.Red;
        }
    }

    public void ApplyInputs()
    {
        ArgumentNullException.ThrowIfNull(Task);

        for (int i = 0; i < Task.Parameters.Count; i++)
        {
            var param = Task.Parameters.GetParameter(i);
            var input = inputs[i];
            param.Value = inputs[i].Text;

            ValidateInput(input, param);
        }

        textBox.Text = Task.ToString();

        Task.Update();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        ApplyInputs();
        DialogResult = DialogResult.OK;
        Hide();
        //Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Hide();
        //Close();
    }

    private void treeViewTypes_AfterSelect(object sender, TreeViewEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Pipeline);

        if (Task == null)
            return;

        if (setup)
            return;

        var type = treeViewTypes.SelectedType;

        if (type == null) 
            return;

        if (type == Task.GetType())
            return;

        textBoxType.Text = treeViewTypes.SelectedNode.ToString();

        var oldTask = Task;
        Task = PipelineTask.FromType(type);

        if (keepValues)
        {
            int count = Math.Min(oldTask.Parameters.Count, Task.Parameters.Count);
            for (int i = 0; i < count; i++)
            {
                Task.Parameters[i] = oldTask.Parameters[i];
            }
        }

        DisplayParameters();
    }

    private void buttonReload_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Pipeline);
        ArgumentNullException.ThrowIfNull(OriginalTask);

        Init(Pipeline, OriginalTask);
    }

    private void treeViewTypes_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
        e.Cancel = e.Node is not TypeTreeNode;
    }
}
