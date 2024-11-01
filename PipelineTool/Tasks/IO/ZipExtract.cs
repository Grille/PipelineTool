using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Zip/Extract")]
internal class ZipExtract : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.OpenFile, "Src File", "", "data.zip");
        Parameters.Def(ParameterTypes.Directory, "Dst Directory", "", "data");
    }

    protected override void OnExecute()
    {
        var src = EvalParameter("Src File");
        var dst = EvalParameter("Dst Directory");

        ZipFile.ExtractToDirectory(src, dst);
    }
}