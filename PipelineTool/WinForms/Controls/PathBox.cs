using Grille.PipelineTool.WinForms.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;
public partial class PathBox : UserControl
{
    [Browsable(true)]
    public PathBoxMode Mode { get; set; }

    public override string Text { get => TextBox.Text; set => TextBox.Text = value; }

    public override Color ForeColor { get => TextBox.ForeColor; set => TextBox.ForeColor = value; }

    public PathBox(PathBoxMode mode)
    {
        InitializeComponent();

        Mode = mode;

        TextBox.TextChanged += TextBox_TextChanged;
        Button.ForeColor = Color.Black;
    }

    private void TextBox_TextChanged(object? sender, EventArgs e)
    {
        OnTextChanged(e);
    }

    private void Button_Click(object sender, EventArgs e)
    {
        if (Mode == PathBoxMode.Directory)
        {
            Text = DialogUtils.FolderBrowserDialog(Text);
        }
        else if (Mode == PathBoxMode.OpenFile)
        {
            Text = DialogUtils.OpenFileDialog(Text);
        }
        else if (Mode == PathBoxMode.SaveFile)
        {
            Text = DialogUtils.SaveFileDialog(Text);
        }
        else if (Mode == PathBoxMode.Color)
        {
            Text = DialogUtils.ColorDialog(Text);
        }
        else
        {
            using var dialog = new EditValueDialog();
            dialog.TextBox.Text = Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Text = dialog.TextBox.Text;
            }
        }
    }
}

public enum PathBoxMode
{
    Generic,
    OpenFile,
    SaveFile,
    Directory,
    Color,
}
