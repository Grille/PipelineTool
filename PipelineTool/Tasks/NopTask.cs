using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grille.PipelineTool.Tasks;

internal class NopTask : PipelineTask
{
    public string Text { get => Parameters["Text"]; set => Parameters["Text"] = value; }

    protected override void OnInit()
    {
        CanParse = true;
        Parameters.Def(ParameterTypes.String, "Text", "", "");
    }

    protected override void OnExecute()
    {
    }

    protected override void OnParse(string text)
    {

    }

    public override Token[] ToTokens()
    {
        var value = Parameters["Text"];
        var token = value == "" ? new Token(TokenType.Comment, "") : new Token(TokenType.Comment, $"// {value}");
        return new Token[] { token };
    }
}
