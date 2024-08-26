using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.IO;
public static partial class TextSerializer
{
    public class Result
    {
        private List<PipelineTask> _tasks;

        public List<PipelineTask> Headless { get; }

        public Dictionary<string, List<PipelineTask>> Sections { get; }

        public Result()
        {
            _tasks = new List<PipelineTask>();
            Sections = new Dictionary<string, List<PipelineTask>>();
            Headless = _tasks;
        }

        [MemberNotNull(nameof(_tasks))]
        public void Section(string name)
        {
            _tasks = new List<PipelineTask>();
            Sections.Add(name, _tasks);
        }

        public void Add(PipelineTask task)
        {
            _tasks.Add(task);
        }
    }
}
