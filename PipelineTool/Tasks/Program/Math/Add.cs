using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Math;

[PipelineTask("Program/Math/Add (a + b)", PipelineTaskKind.Operator)]
internal class Add : MathI2O1Task
{
    protected override string Operator => "+";

    protected override decimal OnExecute(decimal a, decimal b)
    {
        return a + b;
    }
}
