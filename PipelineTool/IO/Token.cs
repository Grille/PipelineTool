using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.IO;

public struct Token
{
    readonly public TokenType Type;
    readonly public string Text;

    public Token(TokenType type, string text)
    {
        Text = text;
        Type = type;
    }

    public static implicit operator Token((TokenType, string) value) => new Token(value.Item1, value.Item2);
}

public enum TokenType
{
    Text,
    Expression,
    Comment,
    Error,
    Flow,
}