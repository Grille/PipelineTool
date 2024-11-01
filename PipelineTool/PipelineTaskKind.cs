using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;
public enum PipelineTaskKind
{
    Method,
    StringOperation,
    Flow,
    Variable,
    Comment,
    Operator,
    OperatorCmp,
}
