using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Stack;

[PipelineTask("Program/Stack/Exch")]
internal class Exch : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Variable, "Variable", "", "Var");
    }

    protected override void OnExecute()
    {
        var name = EvalParameter("Variable");

        var value = Runtime.Variables[name];
        Runtime.Variables[name] = Runtime.ValueStack.Pop();
        Runtime.ValueStack.Push(value);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, "Exch "),
        new Token(TokenType.Expression, Parameters["Variable"]),
    };
}