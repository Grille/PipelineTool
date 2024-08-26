using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool;
public interface IUserInterface
{
    public Form? ParentForm { get; set; }

    public bool TryRequestInput(out VariableValue value);

    public bool TryRequestInput(out VariableValue value, string message);
}
