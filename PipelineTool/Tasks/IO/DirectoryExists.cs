using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Directory.Exists")]
internal class DirectoryExists : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Directory, "Path", "", "path");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Variable");
        string path = EvalParameter("Result");

        Runtime.Variables[var] = new VariableValue(Directory.Exists(path));
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "File Exists "),
        new(TokenType.Expression, Parameters["Path"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Result"]),
    };
}
