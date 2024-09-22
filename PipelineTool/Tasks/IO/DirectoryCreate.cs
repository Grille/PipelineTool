using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Directory.Create")]
internal class DirectoryCreate : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Directory, "Directory", "", "dir0");
    }

    protected override void OnExecute()
    {
        string oldFile = EvalParameter("Directory");

        Directory.CreateDirectory(oldFile);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Create Directory "),
        new(TokenType.Expression, Parameters["Directory"]),
    };
}
