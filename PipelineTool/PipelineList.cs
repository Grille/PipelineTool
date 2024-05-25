using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Grille.PipelineTool.IO;

namespace Grille.PipelineTool;

public class PipelineList : List<Pipeline>
{
    public Pipeline this[string name]
    {
        get => Find(x => x.Name == name) ?? throw new KeyNotFoundException();
    }

    public bool ContainsName(string name) => PipelineListExtension.ContainsName(this, name);

    public Pipeline CreateUnbound(string name)
    {
        if (ContainsName(name))
            throw new InvalidOperationException();

        var pipeline = new Pipeline(this, name);
        return pipeline;
    }

    public Pipeline Create(string name)
    {
        if (ContainsName(name))
            throw new InvalidOperationException();

        var pipeline = new Pipeline(this, name);
        Add(pipeline);
        return pipeline;
    }

    public void Rename(string name, string newname)
    {
        if (!ContainsName(name))
            throw new InvalidOperationException();

        if (name == newname)
            return;

        if (ContainsName(newname))
            throw new InvalidOperationException();

        this[name].Name= newname;
    }

    public string GetUniqueName(string name) => PipelineListExtension.GetUniqueName(this, name);
}

public static class PipelineListExtension
{
    public static bool ContainsName(this List<Pipeline> list, string name)
    {
        return list.FindIndex(x => x.Name == name) != -1;
    }

    public static string GetUniqueName(this List<Pipeline> list, string name)
    {
        if (!list.ContainsName(name))
            return name;

        int i = 1;
        while (true)
        {
            string uname = $"{name} #{i++}";
            if (!list.ContainsName(uname))
                return uname;
        }
    }
}
