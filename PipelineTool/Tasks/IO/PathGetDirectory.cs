using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Path.GetDirectoryName")]
internal class PathGetDirectory : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Generic, "Path", "", "Directory/File.Ext");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        var path = EvalParameter("Path");
        var name = EvalParameter("Result");

        var dirname = Path.GetDirectoryName(path);
        Runtime.Variables[name] = dirname == null ? string.Empty : dirname;
    }
}