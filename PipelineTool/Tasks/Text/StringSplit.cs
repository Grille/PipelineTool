using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Grille.PipelineTool.IO;
using Grille.PipelineTool.Expressions;

namespace Grille.PipelineTool.Tasks.Text;

[PipelineTask("Text/Split")]
internal class StringSplit : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Value", "", "value0, value1");
        Parameters.Def(ParameterTypes.String, "Seperator", "", ",");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Result");
        string value = EvalParameter("Value");
        string seperator = EvalParameter("Seperator");

        var split = value.Split(seperator);
        var result = new VariableValue(split);

        Runtime.Variables[var] = result;
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Split "),
        new(TokenType.Expression, Parameters["Value"]),
        new(TokenType.Text, " by "),
        new(TokenType.Expression, Parameters["Seperator"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Result"]),
    };
}
