using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File.Copy")]
internal class FileCopy : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Src", "", "SrcDir");
        Parameters.Def(ParameterTypes.String, "Dst", "", "DstFile");
    }

    protected override void OnExecute()
    {
        string srcPath = EvalParameter("Src");
        string dstPath = EvalParameter("Dst");

        File.Copy(srcPath, dstPath, true);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Copy File "),
        new(TokenType.Expression, Parameters["Src"]),
        new(TokenType.Text, " to "),
        new(TokenType.Expression, Parameters["Dst"]),
    };
}
