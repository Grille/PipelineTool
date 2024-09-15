using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;
internal static class ExceptionBox
{
    public static DialogResult Show(IWin32Window owner, Action action, MessageBoxButtons buttons = MessageBoxButtons.OK)
    {
        try
        {
            action();
            return DialogResult.Continue;
        }
        catch (Exception e)
        {
            return Show(owner, e, buttons);
        }
    }

    public static DialogResult Show(IWin32Window owner, Exception e, MessageBoxButtons buttons = MessageBoxButtons.OK)
    {
        var title = e.GetType().Name;

        var sb = new StringBuilder();
        sb.AppendLine(e.Message);

        while (e.InnerException != null)
        {
            e = e.InnerException;

            sb.AppendLine();
            sb.Append(e.GetType().Name);
            sb.Append(":");
            sb.AppendLine();
            sb.AppendLine(e.Message);
        }

        var message = sb.ToString();

        return MessageBox.Show(owner, message, title, buttons, MessageBoxIcon.Error);
    }
}
