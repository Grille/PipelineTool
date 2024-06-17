using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Flow/GoTo")]
internal class GoTo : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Integer, "Line", "", "0");
    }

    protected override void OnExecute()
    {
        var line = EvalParameter("Line");

        int count = Runtime.Pipeline.Tasks.Count;

        int lineNr = int.Parse(line);

        Runtime.Logger.Info($"GoTo: {lineNr}");

        if (lineNr < 1 || lineNr > count)
            throw new InvalidOperationException($"GoTo must go to a value between 1 and {count}.");

        Runtime.Position = lineNr - 2;

    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, "GoTo: "),
        new Token(TokenType.Expression, Parameters["Line"]),
    };
}
