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

[PipelineTask("Program/Flow/For Each")]
internal class ForEach : PipelineTask
{
    /*
    public Parameter Mode { get; } = new ParameterEnum("Mode", "", "list", new string[] { "List", "Directorys", "Files" });
    public Parameter Collection { get; } = new ParameterString("Collection", "", "1");
    public Parameter Variable { get; } = new ParameterString("Variable", "", "i");
    */

    protected override void OnInit()
    {
        Parameters.Add(
            new ParameterEnum("Mode", "", "List", new string[] { "List", "Directories", "Files" }),
            new ParameterPath("Collection", "", "1", WinForms.PathBoxMode.Generic),
            new ParameterVariable("Variable", "", "Item")
        );
    }

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
