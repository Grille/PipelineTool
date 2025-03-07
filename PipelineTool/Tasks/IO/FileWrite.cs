﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/File/Write")]
internal class FileWrite : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.SaveFile, "Path", "", "DstFile");
        Parameters.Def(ParameterTypes.String, "Value", "", "Var");
    }

    protected override void OnExecute()
    {
        var path = EvalParameter("Path");
        var var = EvalParameter("Value");

        File.WriteAllText(path, var);
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Write File "),
        new(TokenType.Expression, Parameters["Path"]),
        new(TokenType.Text, " from "),
        new(TokenType.Expression, Parameters["Value"]),
    };
}
