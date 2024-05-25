using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;

public partial class PipelineToolControl : UserControl
{
    public PipelineList Pipelines => PipelinesControl.Pipelines;

    public PipelineToolControl()
    {
        InitializeComponent();

        var executer = PipelinesControl.Executer;
        var piplines = PipelinesControl.Pipelines;

        PipelinesControl.ListBox.Executer = executer;
        TasksControl.ListBox.Executer = executer;

        PipelinesControl.TasksControl = TasksControl;
    }
}
