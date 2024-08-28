using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Expressions;

using static ExpressionParser.TokenType;

public static class ExpressionEvaluator
{
    public static VariableValue Eval(ReadOnlySpan<char> expression, Runtime runtime)
    {
        return ExpressionParser.Tokenize(expression, false).Eval(runtime);
    }

    public static VariableValue Eval(this Expression expression, Runtime runtime)
    {
        var variables = runtime.Variables;
        var tokens = expression.Tokens;

        var token0 = tokens[0];

        if (token0.Type == Symbol) 
        {
            if (token0.Text == "*")
            {
                var key = Eval(tokens[1].Text, runtime);
                return variables[key];
            }
            else if (token0.Text == "$")
            {
                var sb = new StringBuilder();

                foreach (var token in tokens.Slice(1))
                {
                    if (token.Type == Symbol)
                    {
                        continue;
                    }
                    else if (token.Type == Literal)
                    {
                        sb.Append(token.Text);
                    }
                    else if (token.Type == UntokenizedExpression)
                    {
                        sb.Append(Eval(token.Text, runtime));
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }

                return new VariableValue(sb.ToString());
            }
        }

        return new VariableValue(token0.Text);
    }
}
