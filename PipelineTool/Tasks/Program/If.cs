using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Flow/If")]
public class If : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Value1", "", "Value");
        Parameters.Def(ParameterTypes.Enum, "Operator", "", "=", new string[] { "==", "!=", ">", "<", "?" });
        Parameters.Def(ParameterTypes.String, "Value2", "", "Value");
    }

    protected override void OnExecute()
    {
        var op = Parameters["Operator"].ToLower();
        var value1 = EvalParameter("Value1");
        var value2 = EvalParameter("Value2");

        bool result = op switch
        {
            "==" => value1 == value2,
            "!=" => value1 != value2,
            ">" => double.Parse(value1) > double.Parse(value2),
            "<" => double.Parse(value1) < double.Parse(value2),
            _ => throw new ArgumentOutOfRangeException(),
        };

        if (result)
        {
            Runtime.ExecuteNextBlock();
        }
        Runtime.SkipNextBlock();
    }

    public override Token[] ToTokens() => new Token[]
    {
        (TokenType.Flow, "If "),
        (TokenType.Expression, Parameters["Value1"]),
        (TokenType.Text, " "),
        (TokenType.Expression, Parameters["Operator"]),
        (TokenType.Text, " "),
        (TokenType.Expression, Parameters["Value2"]),
    };
}
