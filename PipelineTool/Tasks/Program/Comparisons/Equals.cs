using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Math;

[PipelineTask("Program/Comparisons/Equals (a == b)", PipelineTaskKind.OperatorCmp)]
internal class Equals : CmpI2O1Task
{
    protected override string Operator => "==";

    protected override bool OnExecute(object a, object b)
    {
        return a.Equals(b);
    }
}
