using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Math;

[PipelineTask("Program/Comparisons/CmpSize (a < b)", PipelineTaskKind.OperatorCmp)]
internal class CmpSize : MathI2O1Task
{
    protected override string Operator => "<";

    protected override decimal OnExecute(decimal a, decimal b)
    {
        return a < b ? 1 : 0;
    }
}
