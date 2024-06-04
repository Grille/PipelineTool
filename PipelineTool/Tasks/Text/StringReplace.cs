﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Grille.PipelineTool.Tasks.Text;

[PipelineTask("Text/Replace")]
internal class StringReplace : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Variable", "", "Var");
        Parameters.Def(ParameterTypes.String, "Value", "", "value");
        Parameters.Def(ParameterTypes.String, "Find", "", "find");
        Parameters.Def(ParameterTypes.String, "Replace", "", "replace");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Variable");
        string value = EvalParameter("Value");
        string find = EvalParameter("Find");
        string replace = EvalParameter("Replace");

        var result = value.Replace(find, replace);

        Runtime.Variables[var] = result;
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Replace "),
        new(TokenType.Variable, Parameters["Find"]),
        new(TokenType.Text, " with "),
        new(TokenType.Variable, Parameters["Replace"]),
        new(TokenType.Text, " in "),
        new(TokenType.Variable, Parameters["Value"]),
        new(TokenType.Text, " as "),
        new(TokenType.Variable, Parameters["Variable"]),
    };
}
