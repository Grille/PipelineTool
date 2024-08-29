using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Directory.Delete")]
internal class DirectoryDelete : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Directory, "Path", "", "Dir");
    }

    protected override void OnExecute()
    {
        string path = EvalParameter("Path");

        Directory.Delete(path, true);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Text, $"Delete Directory "),
        new Token(TokenType.Expression, Parameters["Path"]),
    };
}
