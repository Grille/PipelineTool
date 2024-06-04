using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Show Dialog")]
public class ShowMsgBox : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Text", "", "Message");
        Parameters.Def(ParameterTypes.String, "Variable", "", "Result");
        Parameters.Def(ParameterTypes.Enum, "Mode", "", "Ok", new string[] { "Ok", "Ok/Cancel" });
        Parameters.Def(ParameterTypes.Enum, "Icon", "", "None", new string[] { "None", "Info", "Warn", "Error" });
    }

    protected override void OnExecute()
    {
        var text = EvalParameter("Text");
        var mode = EvalParameter("Mode");
        var icon = EvalParameter("Mode");
        var variable = EvalParameter("Variable");

        var ico = (string)mode switch
        {
            "Error" => MessageBoxIcon.Error,
            "Warn" => MessageBoxIcon.Warning,
            "Info" => MessageBoxIcon.Information,
            _ => MessageBoxIcon.None
        };

        var buttons = (string)mode switch
        {
            "Ok/Cancel" => MessageBoxButtons.OKCancel,
            _ => MessageBoxButtons.OK
        };

        var result = MessageBox.Show(text, string.Empty, buttons, MessageBoxIcon.Information);

        var rstr = result.ToString();

        Runtime.Variables[variable] = rstr;
    }
}
