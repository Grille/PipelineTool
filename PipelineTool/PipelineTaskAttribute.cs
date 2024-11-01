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

    public PipelineTaskKind Kind { get; init; }

    public PipelineTaskAttribute(string key)
    {
        Key = key;
        Visible = true;
        Kind = PipelineTaskKind.Method;
    }

    public PipelineTaskAttribute(string key, PipelineTaskKind type)
    {
        Key = key;
        Visible = true;
        Kind = type;
    }
}
