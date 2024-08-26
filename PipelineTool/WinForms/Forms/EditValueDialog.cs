using Grille.PipelineTool.WinForms.Forms;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;
public partial class EditValueDialog : Form
{
    public EditValueDialog()
    {
        InitializeComponent();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        TextBox.Focus();
        TextBox.SelectAll();
    }

    public void DisableMultiline()
    {
        TextBox.Multiline = false;
        MaximumSize = new Size(int.MaxValue, MinimumSize.Height);
        toolStripButton1.Enabled = false;
    }

    private void okCancelPanel1_Button1Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void okCancelPanel1_Button2Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        TextBox.Text = DialogUtils.OpenFileDialog(TextBox.Text);
    }

    private void folderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        TextBox.Text = DialogUtils.FolderBrowserDialog(TextBox.Text);
    }

    private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                TextBox.Text = File.ReadAllText(dialog.FileName);
            }
            catch (Exception ex)
            {
                ExceptionBox.Show(this, ex);
            }
        }
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {
        TextBox.Text = DialogUtils.ColorDialog(TextBox.Text);
    }

    private void EditValueDialog_ResizeEnd(object sender, EventArgs e)
    {
        TextBox.ScrollBars = Height > 155 ? ScrollBars.Both : ScrollBars.None;
    }
}
