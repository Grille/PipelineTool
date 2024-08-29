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
public partial class ConsoleControl : UserControl
{
    public ConsoleControl()
    {
        InitializeComponent();
    }

    public ILogger GetLogger()
    {
        return consoleListBox1;
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
        GetLogger().Clear();
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
        consoleListBox1.CopyToClipboard();
    }
}
