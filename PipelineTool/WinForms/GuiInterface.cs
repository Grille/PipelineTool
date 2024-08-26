using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Xml.Linq;

namespace Grille.PipelineTool.WinForms;
public class GuiInterface : IUserInterface
{
    EditValueDialog _dialog;

    public Form? ParentForm { get; set; }

    public GuiInterface()
    {
        _dialog = new EditValueDialog();
    }

    public bool TryRequestInput(out VariableValue value)
    {
        return TryRequestInput(out value, "Set Value");
    }

    public bool TryRequestInput(out VariableValue value, string message)
    {
        if (ParentForm == null)
        {
            throw new InvalidOperationException();
        }

        _dialog.Text = message;
        _dialog.TextBox.Text = $"Value";

        var result = ParentForm.Invoke(() => _dialog.ShowDialog(ParentForm));
        if (result == DialogResult.OK)
        {
            value = _dialog.TextBox.Text;
            return true;
        }

        value = new VariableValue();
        return false;
    }

    public void SubParentForm(Form? form)
    {
        ParentForm = form;
    }
}
