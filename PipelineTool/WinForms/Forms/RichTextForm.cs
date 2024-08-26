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
public partial class RichTextForm : Form
{
    public RichTextForm()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Close();
    }

    public static RichTextForm ShowTutorial(Form owner, string title)
    {
        using var stream = new MemoryStream(Properties.Resources.Tutorial);
        using var reader = new StreamReader(stream, leaveOpen: true);

        var rtf = reader.ReadToEnd();
        var form = new RichTextForm();
        form.Icon = owner.Icon;
        form.Text = title;
        form.RichTextBox.Rtf = rtf;
        form.Show(owner);

        return form;
    }
}
