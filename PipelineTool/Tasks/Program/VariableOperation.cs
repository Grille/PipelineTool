﻿using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Tasks.Program;

[PipelineTask("Program/Variable")]
internal class VariableOperation : PipelineTask
{

    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Name", "", "Var");
        Parameters.Def(ParameterTypes.Enum, "Operator", "", "=", new string[] { "=", "+" });
        Parameters.Def(ParameterTypes.String, "Value", "", "Value");
    }

    protected override void OnExecute()
    {
        var op = Parameters["Operator"].ToLower();
        var name = EvalParameter("Name");
        var value = EvalParameter("Value");
        switch (op)
        {
            case "=":
            {
                Runtime.Variables[name] = value;
                break;
            }
            case "+":
            {
                Runtime.Variables[name] += value;
                break;
            }
            case "Replace":
            {
                Runtime.Variables[name] += value;
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public override Token[] ToTokens() => new Token[]
    {
        new Token(TokenType.Expression, Parameters["Name"]),
        new Token(TokenType.Text, " "),
        new Token(TokenType.Expression, Parameters["Operator"]),
        new Token(TokenType.Text, " "),
        new Token(TokenType.Expression, Parameters["Value"]),
    };
}
