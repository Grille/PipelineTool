﻿using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program.Math;

internal abstract class CmpI2O1Task : PipelineTask
{
    protected abstract string Operator { get; }

    protected sealed override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "A", "", "0");
        Parameters.Def(ParameterTypes.String, "B", "", "0");
        Parameters.DefResult();
    }

    protected sealed override void OnExecute()
    {
        var a = EvalParameter("A").Value;
        var b = EvalParameter("B").Value;
        var var = EvalParameter("Result");

        Runtime.Variables[var] = OnExecute(a, b);
    }

    protected abstract bool OnExecute(object a, object b);

    public sealed override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Expression, Parameters["Result"]),
        new Token(TokenType.Text, " = "),
        new Token(TokenType.Expression, Parameters["A"]),
        new Token(TokenType.Text, " "),
        new Token(TokenType.Text, Operator),
        new Token(TokenType.Text, " "),
        new Token(TokenType.Expression, Parameters["B"]),
    };
}