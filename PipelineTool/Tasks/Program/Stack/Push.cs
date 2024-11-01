using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Stack;

[PipelineTask("Program/Stack/Push", PipelineTaskKind.Method)]
internal class Push : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Value", "Value to push on the global stack.", "Var");
    }

    protected override void OnExecute()
    {
        var value = EvalParameter("Value");

        Runtime.ValueStack.Push(value);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, "Push "),
        new Token(TokenType.Expression, Parameters["Value"]),
    };
}