using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;
using Grille.PipelineTool.Tasks;

using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

using Grille.PipelineTool.IO;

namespace Grille.PipelineTool;

public class Pipeline
{
    const int Magic = 23658;

    public string Name { get; set; }

    public PipelineList Owner { get; }

    //public Dictionary<string, string> Variables { get; }

    //public int TaskPosition { get; set; }

    public PipelineTaskList Tasks { get; private set; }

    //public Stack<Pipeline> CallStack { get; private set; }



    public Pipeline(PipelineList owner, string name)
    {
        Owner = owner;
        Name = name;
        //Variables = new();
        Tasks = new(this);
        //TaskPosition = 0;
    }

    public override string ToString()
    {
        return Name;
    }

    public Pipeline Clone()
    {
        string uname = Owner.GetUniqueName($"{Name} Clone");
        var clone = Owner.CreateUnbound(uname);

        foreach (var task in Tasks)
        {
            task.CloneTo(clone);
        }

        return clone;
    }
}
