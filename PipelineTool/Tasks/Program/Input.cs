using Grille.PipelineTool.IO;
using Grille.PipelineTool.WinForms;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Input", Description = "")]
internal class Input : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Variable, "Name", "Variable that's expected to exists.", "Var");
        Parameters.Def(ParameterTypes.Enum, "User Input", "Conditions, in which the user will be requested to enter a value.", "If Empty", new string[] { "Never", "If Empty", "Always" });
    }

    protected override void OnExecute()
    {
        var name = EvalParameter("Name");
        var mode = EvalParameter("User Input").ToString().ToLower();

        switch (mode)
        {
            case "never":
            {
                if (!Runtime.Variables.ContainsKey(name))
                {
                    ThrowNotFoundException(name);
                }
                break;
            }
            case "if empty":
            {
                if (!Runtime.Variables.ContainsKey(name))
                {
                    RequestInput(name);
                }
                break;
            }
            case "always":
            {
                RequestInput(name);
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void RequestInput(string name)
    {
        if (Runtime.UserInterface.TryRequestInput(out var value, $"Set '{name}'"))
        {
            Runtime.Variables[name] = value;
        }
        else
        {
            ThrowNotFoundException(name);
        }
    }

    private static void ThrowNotFoundException(string name)
    {
        throw new KeyNotFoundException($"Expected variable '{name}' not found.");
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Text, "Input "),
        new Token(TokenType.Expression, Parameters["Name"]),
    };
}
