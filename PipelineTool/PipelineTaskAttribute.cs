using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;

public class PipelineTaskAttribute : Attribute
{
    public string Key { get; init; }
    public string? Description { get; init; }
    public bool Visible { get; init; }

    public PipelineTaskAttribute(string key)
    {
        Key = key;
        Visible = true;
    }
}
