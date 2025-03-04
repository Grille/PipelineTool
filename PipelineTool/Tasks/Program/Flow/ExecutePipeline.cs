﻿using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Flow;

[PipelineTask("Program/Flow/Call", PipelineTaskKind.Flow, Description = "Call another pipeline by its name.")]
internal class ExecutePipeline : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Variable, "Name", "", "Pipeline");
    }

    protected override void OnExecute()
    {
        string name = EvalParameter("Name");

        Runtime.Call(name);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Flow, "Call "),
        new(TokenType.Expression, Parameters["Name"]),
    };
}
