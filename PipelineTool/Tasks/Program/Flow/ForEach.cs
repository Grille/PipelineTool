using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Program.Flow;

[PipelineTask("Program/Flow/For Each", PipelineTaskKind.Flow)]
internal class ForEach : PipelineTask
{
    public ParameterEnum Mode { get; } = new(new string[] { "List", "Directorys", "Files" }) { Value = "List" };
    public ParameterPath Collection { get; } = new(WinForms.PathBoxMode.Generic) { Value = "1" };
    public ParameterVariable Variable { get; } = new() { Value = "i" };

    protected override void OnExecute()
    {
        var mode = EvalParameter("Mode");
        var collection = EvalParameter("Collection");
        var variable = EvalParameter("Variable");

        IEnumerable items = (string)mode switch
        {
            "List" => collection.GetEnumerator(),
            "Directorys" => Directory.GetDirectories(collection),
            "Files" => Directory.GetFiles(collection),
            _ => throw new ArgumentOutOfRangeException(mode)
        };

        foreach (var item in items)
        {
            Runtime.Variables[variable] = new VariableValue(item);
            Runtime.ExecuteNextBlock();
        }
        Runtime.SkipNextBlock();
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Flow, $"ForEach "),
        new Token(TokenType.Expression, Parameters["Variable"]),
        new Token(TokenType.Text, $" in "),
        new Token(TokenType.Expression, Parameters["Collection"]),
        new Token(TokenType.Text, $":"),
    };
}
