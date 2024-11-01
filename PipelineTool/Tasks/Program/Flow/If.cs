using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Flow;

[PipelineTask("Program/Flow/If", PipelineTaskKind.Flow)]
internal class If : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Value", "", "1");
    }

    protected override void OnExecute()
    {
        var value = EvalParameter("Value").Boolean;
        if (value)
        {
            Runtime.ExecuteNextBlock();
        }
        Runtime.SkipNextBlock();
    }

    public override Token[] ToTokens() => new Token[]
    {
        (TokenType.Flow, "If "),
        (TokenType.Expression, Parameters["Value"]),
        (TokenType.Text, $":"),
    };
}
