using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.Tasks.Program.Windows;

[PipelineTask("Program/Windows/Dialog")]
internal class ShowMsgBox : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Title", "", "Title");
        Parameters.Def(ParameterTypes.String, "Text", "", "Message");
        Parameters.Def(ParameterTypes.String, "Variable", "", "Result");
        Parameters.Def(ParameterTypes.Enum, "Mode", "", "Ok", new string[] { "Ok", "Ok/Cancel" });
        Parameters.Def(ParameterTypes.Enum, "Icon", "", "None", new string[] { "None", "Info", "Warn", "Error" });
    }

    protected override void OnExecute()
    {
        var title = EvalParameter("Title");
        var text = EvalParameter("Text");
        var mode = EvalParameter("Mode");
        var icon = EvalParameter("Icon");
        var variable = EvalParameter("Variable");

        var ico = ((string)icon).ToLower() switch
        {
            "none" => MessageBoxIcon.None,
            "error" => MessageBoxIcon.Error,
            "warn" => MessageBoxIcon.Warning,
            "info" => MessageBoxIcon.Information,
            _ => throw new ArgumentOutOfRangeException(),
        };

        var buttons = ((string)mode).ToLower() switch
        {
            "ok" => MessageBoxButtons.OK,
            "ok/cancel" => MessageBoxButtons.OKCancel,
            _ => throw new ArgumentOutOfRangeException(),
        };

        var result = Invoke((form) => MessageBox.Show(form, text, title, buttons, ico));
        var rstr = result.ToString();

        Runtime.Variables[variable] = rstr;
    }

    T Invoke<T>(Func<Form, T> func)
    {
        var form = Runtime.UserInterface.ParentForm;
        if (form != null)
        {
            return form.Invoke(()=>func(form));
        }
        throw new InvalidOperationException();
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Text, "Dialog("),
        new Token(TokenType.Expression, Parameters["Title"]),
        new Token(TokenType.Text, ","),
        new Token(TokenType.Expression, Parameters["Text"]),
        new Token(TokenType.Text, ")"),
    };
}
