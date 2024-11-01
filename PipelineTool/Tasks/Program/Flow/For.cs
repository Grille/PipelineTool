using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Program.Flow;

[PipelineTask("Program/Flow/For", PipelineTaskKind.Flow)]
internal class For : PipelineTask
{
    public ParameterInteger Start { get; } = new() { Value = "0" };
    public ParameterInteger End { get; } = new() { Value = "1" };
    public ParameterVariable Variable { get; } = new() { Value = "Index" };

    protected override void OnExecute()
    {
        string start = EvalParameter(Start);
        string end = EvalParameter(End);
        string variable = EvalParameter(Variable);

        int istart = int.Parse(start);
        int iend = int.Parse(end);

        for (int i = istart; i <= iend; i++)
        {
            Runtime.Variables[variable] = i.ToString();
            Runtime.ExecuteNextBlock();
        }
        Runtime.SkipNextBlock();
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, $"For "),
        new Token(TokenType.Expression, Parameters["Start"]),
        new Token(TokenType.Text, $" to "),
        new Token(TokenType.Expression, Parameters["End"]),
        new Token(TokenType.Text, $" as "),
        new Token(TokenType.Expression, Parameters["Variable"]),
        new Token(TokenType.Text, $":"),
    };
}
