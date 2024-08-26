using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;
public class Variables : Dictionary<string, VariableValue>
{
    public Variables? Parent { get; }

    public Variables() { }

    public Variables(Variables parent) : base(parent)
    {
        Parent = parent;
    }
}
