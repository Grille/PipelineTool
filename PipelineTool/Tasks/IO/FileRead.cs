using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File/Read")]
internal class FileRead : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.OpenFile, "Path", "", "SrcFile");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        var src = EvalParameter("Path");
        var var = EvalParameter("Result");

        Runtime.Variables[var] = File.ReadAllText(src);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Read File "),
        new(TokenType.Expression, Parameters["Path"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Result"]),
    };
}
