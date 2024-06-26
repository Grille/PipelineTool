﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Program;

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
            new ParameterString("Collection", "", "1"),
            new ParameterString("Variable", "", "i")
        );
    }

    protected override void OnExecute()
    {
        string mode = EvalParameter("Mode");
        string collection = EvalParameter("Collection");
        string variable = EvalParameter("Variable");

        string[] items = mode switch
        {
            "List" => Parameter.ValueToList(collection).ToArray(),
            "Directorys" => Directory.GetDirectories(collection),
            "Files" => Directory.GetFiles(collection),
            _ => throw new ArgumentOutOfRangeException(mode)
        };

        foreach (var item in items)
        {
            Runtime.Variables[variable] = item.Trim();
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
