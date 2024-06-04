using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Directory.Delete")]
internal class DirectoryDelete : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Path", "", "Dir");
    }

    protected override void OnExecute()
    {
        string path = EvalParameter("Path");

        Directory.Delete(path, true);

        Runtime.Log($"Remove dir {path}");
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Text, $"Delete Directory "),
        new Token(TokenType.Variable, Parameters["Path"]),
    };
}
