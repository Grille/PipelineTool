using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;

public class PipelineTaskList : List<PipelineTask>
{
    public Pipeline Pipeline { get; }

    public PipelineTaskList(Pipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline);
        Pipeline = pipeline;
    }

    public PipelineTask Create(string assemblyQualifiedName)
    {
        var task = PipelineTask.FromType(assemblyQualifiedName);
        Add(task);
        return task;
    }

    public PipelineTask Create(Type type)
    {
        var task = PipelineTask.FromType(type);
        Add(task);
        return task;
    }

    public void Link(PipelineTask task)
    {
        Add(task);
    }
}
