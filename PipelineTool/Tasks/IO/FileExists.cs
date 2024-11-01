using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File/Exists")]
internal class FileExists : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.OpenFile, "Path", "", "path");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        string path = EvalParameter("Path");
        string var = EvalParameter("Result");
        Runtime.Variables[var] = File.Exists(path);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "File Exists "),
        new(TokenType.Expression, Parameters["Path"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Result"]),
    };
}
