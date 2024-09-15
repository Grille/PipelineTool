using Grille.PipelineTool.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool;

public class PipelineTaskList : IReadOnlyList<PipelineTask>, IList<PipelineTask>//: List<PipelineTask>
{
    readonly List<PipelineTask> _list;    

    public Pipeline Pipeline { get; }

    public int Count => _list.Count;

    private bool _containsInvalid;

    public bool ContainsInvalid => _containsInvalid;

    public bool IsReadOnly => ((ICollection<PipelineTask>)_list).IsReadOnly;

    public PipelineTask this[int index]
    {
        get => _list[index]; 
        set
        {
            _list[index] = value;

            OnChanged();
        }
    }

    public PipelineTaskList(Pipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline);
        Pipeline = pipeline;
        _list = new List<PipelineTask>();
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

    public void Add(PipelineTask task)
    {
        _list.Add(task);

        OnChanged();
    }

    void OnChanged()
    {
        _containsInvalid = false;
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i] is InvalidTypeTask)
            {
                _containsInvalid = true;
                return;
            }
        }
    }

    public IEnumerator<PipelineTask> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    public int IndexOf(PipelineTask item)
    {
        return _list.IndexOf(item);
    }

    public void Insert(int index, PipelineTask item)
    {
        _list.Insert(index, item);

        OnChanged();
    }

    public void RemoveAt(int index)
    {
        _list.RemoveAt(index);

        OnChanged();
    }

    public void Clear()
    {
        _list.Clear();

        OnChanged();
    }

    public bool Contains(PipelineTask item)
    {
        return _list.Contains(item);
    }

    public void CopyTo(PipelineTask[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    public bool Remove(PipelineTask item)
    {
        if (_list.Remove(item))
        {
            OnChanged();
            return true;
        }
        return false;
    }
}
