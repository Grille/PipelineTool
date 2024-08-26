using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Stack;

[PipelineTask("Program/Stack/Pop")]
internal class Pop : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Variable", "Variable to write the top value of the global stack into.", "Var");
    }

    protected override void OnExecute()
    {
        var name = EvalParameter("Variable");

        Runtime.Variables[name] = Runtime.ValueStack.Pop();
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, "Pop "),
        new Token(TokenType.Expression, Parameters["Variable"]),
    };
}
