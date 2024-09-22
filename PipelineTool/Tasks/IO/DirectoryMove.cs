using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Directory.Move")]
internal class DirectoryMove : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Directory, "Src Directory", "", "dir0");
        Parameters.Def(ParameterTypes.Directory, "Dst Directory", "", "dir1");
    }

    protected override void OnExecute()
    {
        string oldFile = EvalParameter("Src Directory");
        string newFile = EvalParameter("Dst Directory");

        Directory.Move(oldFile, newFile);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Rename Directory "),
        new(TokenType.Expression, Parameters["Src Directory"]),
        new(TokenType.Text, " -> "),
        new(TokenType.Expression, Parameters["Dst Directory"]),
    };
}
