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

namespace Grille.PipelineTool;


public partial class PipelineToolForm : Form
{
    public PipelineList Pipelines => PipelineToolControl.Pipelines;

    public PipelinesControl PipelinesControl => PipelineToolControl.PipelinesControl;

    public PipelineTasksControl TasksControl => PipelineToolControl.TasksControl!;


    public PipelineToolForm()
    {
        InitializeComponent();

        var piplines = PipelinesControl.Pipelines;

        if (File.Exists(PipelineToolControl.FilePath))
            PipelineToolControl.LoadFile();

        PipelinesControl.InvalidateItems();
        if (piplines.Count > 0)
            PipelinesControl.SelectedItem = piplines[0];

        PipelineToolControl.UnsavedChangesChanged += PipelineToolControl_UnsavedChangesChanged;
    }

    private void PipelineToolControl_UnsavedChangesChanged(object? sender, EventArgs e)
    {
        if (PipelineToolControl.UnsavedChanges)
        {
            Text = "Pipeline Tool *unsaved changes";
        }
        else
        {
            Text = "Pipeline Tool";
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
}
