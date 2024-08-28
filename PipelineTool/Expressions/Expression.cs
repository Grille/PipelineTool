using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Expressions;
public class Expression
{
    readonly ExpressionParser.Token[] _tokens;

    public ReadOnlySpan<ExpressionParser.Token> Tokens => _tokens.AsSpan();

    public int Count => ((IReadOnlyCollection<ExpressionParser.Token>)_tokens).Count;

    public ExpressionParser.Token First => _tokens[0];

    public Expression(IEnumerable<ExpressionParser.Token> tokens)
    {
        _tokens = tokens.ToArray();
    }

    public IEnumerator<ExpressionParser.Token> GetEnumerator()
    {
        return ((IEnumerable<ExpressionParser.Token>)_tokens).GetEnumerator();
    }
}
