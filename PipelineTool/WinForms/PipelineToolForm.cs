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
    public const string DefaultPath = "pipelines.txt";

    public string FilePath { get; set; } = DefaultPath;

    public PipelineList Pipelines => pipelineToolControl1.Pipelines;

    public PipelinesControl PipelinesControl => pipelineToolControl1.PipelinesControl;

    public PipelineTasksControl TasksControl => pipelineToolControl1.TasksControl!;


    public PipelineToolForm()
    {
        InitializeComponent();

        var piplines = PipelinesControl.Pipelines;

        if (File.Exists(FilePath))
            LoadFile();

        PipelinesControl.InvalidateItems();
        if (piplines.Count > 0)
            PipelinesControl.SelectedItem = piplines[0];
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
        using var dialog = new OpenFileDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            FilePath = dialog.FileName;
            Catch(LoadFile);
            PipelinesControl.InvalidateItems();
        }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Catch(SaveFile);
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            FilePath = dialog.FileName;
            Catch(SaveFile);
        }
    }

    public void SaveFile()
    {
        using var stream = File.Create(FilePath);
        TextSerializer.Serialize(stream, Pipelines);
    }

    public void LoadFile()
    {
        using var stream = File.OpenRead(FilePath);
        Pipelines.Clear();
        TextSerializer.Deserialize(stream, Pipelines);
    }

    private void Catch(Action action)
    {
        ExceptionBox.Show(this, action);
    }
}
