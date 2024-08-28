using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.Expressions;
public static class ExpressionParser
{
    public record struct Token(TokenType Type, string Text);

    public enum TokenType
    {
        Symbol,
        Literal,
        UntokenizedExpression,
    }

    private static void Add(this List<Token> list, TokenType type, ReadOnlySpan<char> text)
    {
        var token = new Token(type, new string(text));
        list.Add(token);
    }

    private static void Add(this List<Token> list, TokenType type, ReadOnlySpan<char> text, int begin, int length)
    {
        var slice = text.Slice(begin, length);
        var token = new Token(type, new string(slice));
        list.Add(token);
    }

    public static Expression Tokenize(ReadOnlySpan<char> value, bool recursive)
    {
        var tokens = new List<Token>();
        Tokenize(value, tokens, recursive);
        return new Expression(tokens);
    }

    public static void Tokenize(ReadOnlySpan<char> value, List<Token> tokens, bool recursive)
    {
        if (value.Length == 0)
            return;

        if (value[0] == '*')
        {
            tokens.Add(TokenType.Symbol, value, 0, 1);
            var slice = value.Slice(1);
            if (recursive)
            {
                Tokenize(slice, tokens, true);
            }
            else
            {
                tokens.Add(TokenType.UntokenizedExpression, slice);
            }
        }
        else if (value[0] == '$')
        {
            tokens.Add(TokenType.Symbol, value, 0, 1);

            var slice = value.Slice(1);

            TokenizeInterpolatedString(slice, tokens, recursive);
        }
        else
        {
            tokens.Add(TokenType.Literal, value);
        }
    }

    public static void TokenizeInterpolatedString(ReadOnlySpan<char> text, List<Token> tokens, bool recursive)
    {
        int begin = 0;
        int scope = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '{')
            {
                scope += 1;
            }
            else if (text[i] == '}')
            {
                scope -= 1;
            }

            if (scope == 1 && text[i] == '{')
            {
                int end = i;
                int length = end - begin;
                if (length > 0)
                {
                    tokens.Add(TokenType.Literal, text, begin, length);
                }
                tokens.Add(TokenType.Symbol, text, i, 1);
                begin = i + 1;
            }
            else if (scope == 0 && text[i] == '}')
            {
                int end = i;
                int length = end - begin;
                if (length > 0)
                {
                    var slice = text.Slice(begin, length);
                    if (recursive)
                    {
                        Tokenize(slice, tokens, recursive);
                    }
                    else
                    {
                        tokens.Add(TokenType.UntokenizedExpression, slice);
                    }
                }
                tokens.Add(TokenType.Symbol, text, i, 1);
                begin = i + 1;
            }
        }
        {
            int end = text.Length;
            int length = end - begin;
            if (length > 0)
            {
                tokens.Add(TokenType.Literal, text, begin, length);
            }
        }
    }
}
