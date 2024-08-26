using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Controls;
public class ConsoleListBox : ListBox<Token[]>
{
    protected override Token[] CreateNew()
    {
        return new Token[0];
        //throw new NotImplementedException();
    }

    protected override void OnCopyToClipboard()
    {
        //throw new NotImplementedException();
    }

    protected override void OnDrawItem(DrawItemEventArgs e, Token[] item)
    {
        throw new NotImplementedException();
    }

    protected override void OnPasteFromClipboard()
    {
        return;
    }
}
