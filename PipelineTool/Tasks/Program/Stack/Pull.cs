using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Stack;

//[PipelineTask("Program/Stack/Pull")]
internal class Pull : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Variable", "Variable to pull from enclosing scope.", "Var");
        Parameters.Def(ParameterTypes.String, "New Name", "Name to use in local scope, leave empty to use original name.", "");
    }

    protected override void OnExecute()
    {
        var name = EvalParameter("Variable");

        if (!string.IsNullOrWhiteSpace(Parameters["New Name"]))
        {
            var newname = EvalParameter("New Name");
        }

        Runtime.Variables[name] = Runtime.ValueStack.Pop();
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, "Pull "),
        new Token(TokenType.Expression, Parameters["Variable"]),
    };
}
