using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program;

//[PipelineTask("Program/Input")]
internal class Input : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Name", "", "Var");
    }

    protected override void OnExecute()
    {
        var name = EvalParameter("Name");

        if (!Runtime.Variables.ContainsKey(name))
            throw new Exception($"Expected variable '{name}' not found.");
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Text, "In "),
        new Token(TokenType.Expression, Parameters["Name"]),
    };
}
