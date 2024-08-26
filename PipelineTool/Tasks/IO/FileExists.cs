using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File.Exists")]
internal class FileExists : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.OpenFile, "Path", "", "path");
        Parameters.Def(ParameterTypes.Variable, "Variable", "", "Var");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Variable");
        string path = EvalParameter("Path");

        Runtime.Variables[var] = new VariableValue(File.Exists(path));
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "File Exists "),
        new(TokenType.Expression, Parameters["Path"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Variable"]),
    };
}
