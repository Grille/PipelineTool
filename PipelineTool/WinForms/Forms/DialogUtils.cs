using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Forms;
public static class DialogUtils
{
    public static string FolderBrowserDialog(string text)
    {
        using var dialog = new FolderBrowserDialog();
        dialog.SelectedPath = text;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            text = dialog.SelectedPath;
        }
        return text;
    }

    public static string OpenFileDialog(string text)
    {
        using var dialog = new OpenFileDialog();
        dialog.InitialDirectory = Path.GetDirectoryName(text);
        dialog.FileName = text;
        dialog.CheckFileExists = false;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            text = dialog.FileName;
        }
        return text;
    }

    public static string SaveFileDialog(string text)
    {
        using var dialog = new SaveFileDialog();
        dialog.InitialDirectory = Path.GetDirectoryName(text);
        dialog.FileName = text;
        dialog.CheckFileExists = false;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            text = dialog.FileName;
        }
        return text;
    }

    public static string ColorDialog(string text)
    {
        using var dialog = new ColorDialog();
        dialog.FullOpen = true;
        if (int.TryParse(text, System.Globalization.NumberStyles.HexNumber, null, out int argb))
        {
            dialog.Color = Color.FromArgb(argb);
        }
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            text = $"{dialog.Color.ToArgb():x}";
        }
        return text;
    }
}
