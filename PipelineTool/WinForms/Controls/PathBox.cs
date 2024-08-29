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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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

        UpdateButtonColor();
    }

    private void TextBox_TextChanged(object? sender, EventArgs e)
    {
        UpdateButtonColor();
        OnTextChanged(e);
    }

    private bool TryParseColor(out Color color)
    {
        var result = int.TryParse(Text, System.Globalization.NumberStyles.HexNumber, null, out int argb);
        color = Color.FromArgb(argb);
        return result;
    }

    private void UpdateButtonColor()
    {
        bool isMode = Mode == PathBoxMode.Color || Mode == PathBoxMode.Generic;
        if (isMode && TryParseColor(out var color))
        {
            var invcolor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
            Button.BackColor = color;
            Button.ForeColor = invcolor;
        }
        else
        {
            Button.ForeColor = Color.Black;
            Button.BackColor = Color.Transparent;
        }
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
        UpdateButtonColor();
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
