using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Path/GetExtension")]
internal class PathGetExtension : PipelineTask
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

        Runtime.Variables[name] = Path.GetExtension(path).Substring(1);
    }
}