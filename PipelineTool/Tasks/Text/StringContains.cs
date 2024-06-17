using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Text;

[PipelineTask("Text/Conatins")]
internal class StringContains : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Variable", "", "Var");
        Parameters.Def(ParameterTypes.String, "Value", "", "value");
        Parameters.Def(ParameterTypes.String, "Find", "", "find");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Variable");
        string value = EvalParameter("Value");
        string find = EvalParameter("Find");

        Runtime.Variables[var] = new VariableValue(value.Contains(find));
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Contains "),
        new(TokenType.Expression, Parameters["Find"]),
        new(TokenType.Text, " in "),
        new(TokenType.Expression, Parameters["Value"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Variable"]),
    };
}
