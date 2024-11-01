using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File/Move")]
internal class FileMove : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.OpenFile, "Src FileName", "", "file0.txt");
        Parameters.Def(ParameterTypes.SaveFile, "Dst FileName", "", "file1.txt");
    }

    protected override void OnExecute()
    {
        string oldFile = EvalParameter("Src FileName");
        string newFile = EvalParameter("Dst FileName");

        File.Move(oldFile, newFile);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Rename File "),
        new(TokenType.Expression, Parameters["Src FileName"]),
        new(TokenType.Text, " -> "),
        new(TokenType.Expression, Parameters["Dst FileName"]),
    };
}
