using Grille.PipelineTool.WinForms;
using Grille.PipelineTool.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grille.PipelineTool.IO;
using System.IO;
using System.Diagnostics;

namespace Grille.PipelineTool;


public partial class PipelineToolForm : Form
{
    public PipelineToolControl PipelineToolControl => PipelineCmdControl.PipelineToolControl;

    public PipelineList Pipelines => PipelineToolControl.Pipelines;

    public PipelinesControl PipelinesControl => PipelineToolControl.PipelinesControl;

    public PipelineTasksControl TasksControl => PipelineToolControl.TasksControl!;

    [Browsable(true)]
    public string Title { get; set; }


    public PipelineToolForm()
    {
        InitializeComponent();

        Title = "Pipeline Tool";
        Icon = Icon.FromHandle(Properties.Resources.Icon.GetHicon());

        var piplines = PipelinesControl.Pipelines;

        if (File.Exists(PipelineToolControl.FilePath))
        {
            PipelineToolControl.LoadFile();
        }
        else
        {
            LoadExample();
        }

        PipelinesControl.InvalidateItems();
        if (piplines.Count > 0)
            PipelinesControl.SelectedItem = piplines[0];

        PipelineToolControl.UnsavedChangesChanged += PipelineToolControl_UnsavedChangesChanged;

        UpdateTitle();
    }

    private void PipelineToolControl_UnsavedChangesChanged(object? sender, EventArgs e)
    {
        UpdateTitle();
    }

    private void UpdateTitle()
    {
        if (PipelineToolControl.UnsavedChanges)
        {
            Text = $"{Title} '{PipelineToolControl.FilePath}' *unsaved changes";
        }
        else
        {
            Text = $"{Title} '{PipelineToolControl.FilePath}'";
        }
    }

    private void quitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Pipelines.Clear();
        PipelinesControl.InvalidateItems();
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        PipelineToolControl.ShowOpenFileDialog();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        PipelineToolControl.SaveFile();
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        PipelineToolControl.ShowSaveFileDialog();
    }

    private void PipelineToolForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (PipelineToolControl.ShowSaveExitDialog() == DialogResult.Cancel)
        {
            e.Cancel = true;
        }
    }

    private void loadExampleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LoadExample();
        PipelinesControl.InvalidateItems();
    }

    void LoadExample()
    {
        var bytes = Properties.Resources.Example;
        using var stream = new MemoryStream(bytes);
        PipelineToolControl.LoadFile(stream);
    }

    private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        RichTextForm.ShowTutorial(this, $"{Title} Tutorial");
    }

    private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        PipelineCmdControl.ConsoleVisible = !consoleToolStripMenuItem.Checked;
    }
}
