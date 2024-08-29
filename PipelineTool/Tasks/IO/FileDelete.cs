using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File.Delete")]
internal class FileDelete : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.OpenFile, "Path", "", "File");
    }

    protected override void OnExecute()
    {
        string path = EvalParameter("Path");

        File.Delete(path);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Text, $"Delete File "),
        new Token(TokenType.Expression, Parameters["Path"]),
    };
}
