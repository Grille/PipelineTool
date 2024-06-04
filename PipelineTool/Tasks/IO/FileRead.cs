using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File.Read")]
internal class FileRead : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Path", "", "SrcFile");
        Parameters.Def(ParameterTypes.String, "Variable", "", "Var");
    }

    protected override void OnExecute()
    {
        var src = EvalParameter("Path");
        var var = EvalParameter("Variable");

        Runtime.Variables[var] = File.ReadAllText(src);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Read File "),
        new(TokenType.Variable, Parameters["Path"]),
        new(TokenType.Text, " as "),
        new(TokenType.Variable, Parameters["Variable"]),
    };
}
