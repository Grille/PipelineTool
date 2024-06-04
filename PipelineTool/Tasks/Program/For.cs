using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Flow/For")]
internal class For : PipelineTask
{
    /*
    public Parameter Mode { get; } = new ParameterEnum("Mode", "", "list", new string[] { "List", "Directorys", "Files" });
    public Parameter Collection { get; } = new ParameterString("Collection", "", "1");
    public Parameter Variable { get; } = new ParameterString("Variable", "", "i");
    */

    protected override void OnInit()
    {
        Parameters.Add(
            new ParameterInteger("Start", "", "0"),
            new ParameterInteger("End", "", "1"),
            new ParameterString("Variable", "", "i")
        );
    }

    protected override void OnExecute()
    {
        string start = EvalParameter("Start");
        string end = EvalParameter("End");
        string variable = EvalParameter("Variable");

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
        new Token(TokenType.Variable, Parameters["Start"]),
        new Token(TokenType.Text, $" to "),
        new Token(TokenType.Variable, Parameters["End"]),
        new Token(TokenType.Text, $" as "),
        new Token(TokenType.Variable, Parameters["Variable"]),
        new Token(TokenType.Text, $":"),
    };
}
