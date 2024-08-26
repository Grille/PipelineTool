using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Text;

[PipelineTask("Text/Replace")]
internal class StringReplace : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Value", "", "value");
        Parameters.Def(ParameterTypes.String, "Find", "", "find");
        Parameters.Def(ParameterTypes.String, "Replace", "", "replace");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Result");
        string value = EvalParameter("Value");
        string find = EvalParameter("Find");
        string replace = EvalParameter("Replace");

        var result = value.Replace(find, replace);

        Runtime.Variables[var] = result;
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Replace "),
        new(TokenType.Expression, Parameters["Find"]),
        new(TokenType.Text, " with "),
        new(TokenType.Expression, Parameters["Replace"]),
        new(TokenType.Text, " in "),
        new(TokenType.Expression, Parameters["Value"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Result"]),
    };
}
