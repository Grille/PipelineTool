using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Flow/Return")]
internal class Return : PipelineTask
{
    protected override void OnInit() { }

    protected override void OnExecute()
    {
        Runtime.Return();
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, "Return"),
    };
}
