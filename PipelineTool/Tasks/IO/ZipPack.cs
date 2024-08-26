using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Zip.Pack")]
internal class ZipPack : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Directory, "Src Directory", "", "data");
        Parameters.Def(ParameterTypes.SaveFile, "Dst File", "", "data.zip");
    }

    protected override void OnExecute()
    {
        var src = EvalParameter("Src Directory");
        var dst = EvalParameter("Dst File");

        ZipFile.CreateFromDirectory(src, dst);
    }
}