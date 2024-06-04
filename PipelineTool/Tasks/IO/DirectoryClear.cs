using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Directory.Clear")]
internal class DirectoryClear : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Dir", "", "Dir");
    }

    protected override void OnExecute()
    {
        string path = EvalParameter("Dir");

        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException(path);

        foreach (var file in Directory.GetFiles(path))
            File.Delete(file);

        foreach (var dir in Directory.GetDirectories(path))
            Directory.Delete(dir, true);

        Runtime.Log($"Clear dir {path}");
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, $"Clear Directory "),
        new(TokenType.Variable, Parameters["Dir"]),
    };
}
