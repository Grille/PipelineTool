using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Controls;
public partial class PipelineCmdControl : UserControl
{
    public PipelinesControl PipelinesControl => PipelineToolControl.PipelinesControl;

    public PipelineTasksControl TasksControl => PipelineToolControl.TasksControl;


    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ILogger Logger { get => PipelinesControl.Logger; set => PipelinesControl.Logger = value; }

    public bool ConsoleVisible { get => splitContainer1.Panel2Collapsed; set => splitContainer1.Panel2Collapsed = value; }

    public PipelineCmdControl()
    {
        InitializeComponent();

        Logger = consoleControl.GetLogger();
    }
}
