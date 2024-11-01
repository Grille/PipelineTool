using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Variable", PipelineTaskKind.Variable)]
internal class VariableOperation : PipelineTask
{

    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Variable, "Name", "", "Var");
        Parameters.Def(ParameterTypes.Generic, "Value", "", "Value");
    }

    protected override void OnExecute()
    {
        var name = EvalParameter("Name");
        var value = EvalParameter("Value");

        Runtime.Variables[name] = value;
    }

    private string GetValueText()
    {
        var raw = Parameters["Value"];
        if (raw == null)
        {
            return string.Empty;
        }
        for (int i = 0; i < raw.Length; i++)
        {
            if (raw[i] == '\n' || raw[i] == '\r')
            {
                return raw.Substring(0, i);
            }
        }
        return raw;
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Expression, Parameters["Name"]),
        new Token(TokenType.Text, " = "),
        new Token(TokenType.Expression, GetValueText()),
    };
}
