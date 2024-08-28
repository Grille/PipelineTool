﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Text;

[PipelineTask("Text/Trim")]
internal class StringTrim : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Value", "", "value");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Result");
        string value = EvalParameter("Value");

        var result = value.Trim();

        Runtime.Variables[var] = result;
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Trim "),
        new(TokenType.Expression, Parameters["Value"]),
        new(TokenType.Text, " as "),
        new(TokenType.Expression, Parameters["Result"]),
    };
}
