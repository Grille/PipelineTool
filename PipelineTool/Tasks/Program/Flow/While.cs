using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Program.Flow;

[PipelineTask("Program/Flow/While", PipelineTaskKind.Flow)]
internal class While : PipelineTask
{
    public ParameterVariable Condition { get; } = new() { Value = "1" };

    protected override void OnExecute()
    {
        while (EvalParameter(Condition).Boolean)
        {
            Runtime.ExecuteNextBlock();
        }
        Runtime.SkipNextBlock();
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, $"While "),
        new Token(TokenType.Expression, Condition.Value),
        new Token(TokenType.Text, $":"),
    };
}
