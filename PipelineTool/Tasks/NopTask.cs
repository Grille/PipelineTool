using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks;

internal class NopTask : PipelineTask
{
    public ParameterString Text { get; } = new();

    protected override void OnExecute() { }

    public override Token[] ToTokens()
    {
        var value = Text.Value;
        var token = string.IsNullOrEmpty(value) ? new Token(TokenType.Comment, "") : new Token(TokenType.Comment, $"// {value}");
        return new Token[] { token };
    }
}
