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

    public new void Add(PipelineTask task)
    {
        base.Add(task);
    }

    public static PipelineTask CreateUnbound(string assemblyQualifiedName)
    {
        var type = Type.GetType(assemblyQualifiedName);
        return CreateUnbound(type!);
    }

    public static PipelineTask CreateUnbound(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var obj = Activator.CreateInstance(type);
        if (obj == null)
            throw new InvalidOperationException("Instance creation failed.");

        var task = (PipelineTask)obj;
        return task;
    }

    public PipelineTask Create(string assemblyQualifiedName)
    {
        var task = CreateUnbound(assemblyQualifiedName);
        Add(task);
        return task;
    }

    public PipelineTask Create(Type type)
    {
        var task = CreateUnbound(type);
        Add(task);
        return task;
    }

    public void Link(PipelineTask task)
    {
        Add(task);
    }
}
