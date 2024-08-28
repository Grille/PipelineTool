using Grille.PipelineTool.Expressions;
using Grille.PipelineTool.IO;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;

public struct TokenRenderer
{
    readonly static StringFormat StringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);

    public Graphics Graphics { get; }
    public Font Font { get; }
    public float Margin { get; }
    public float CharSize { get; }

    public TokenRenderer(Graphics g, Font font)
    {
        Graphics = g;
        Font = font;
        Margin = g.MeasureString(" ", font).Width;
        CharSize = g.MeasureString("O", font).Width - Margin;
    }

    public void DrawTokensSingleColor(IReadOnlyList<Token> tokens, Brush brush, ref PointF position)
    {
        foreach (Token token in tokens)
        {
            DrawText(token.Text, brush, ref position);
        }
    }

    public void DrawTokens(IReadOnlyList<Token> tokens, ref PointF position)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            DrawToken(tokens[i], ref position);
        }
    }

    public void DrawToken(Token token, ref PointF position)
    {
        var text = token.Text.ToString();

        if (string.IsNullOrEmpty(text))
            return;

        if (token.Type == TokenType.Expression)
        {
            var expression = ExpressionParser.Tokenize(token.Text, true);
            DrawExpression(expression, ref position);
            return;
        }

        var brush = token.Type switch
        {
            TokenType.Text => TokenBrushes.Text,
            TokenType.Comment => TokenBrushes.Comment,
            TokenType.Flow => TokenBrushes.Flow,
            _ => TokenBrushes.Error,
        };

        DrawText(text, brush, ref position);
    }

    public void DrawExpression(Expression exp, ref PointF position)
    {
        var lastToken = new ExpressionParser.Token(ExpressionParser.TokenType.Literal, string.Empty);

        foreach (var token in exp.Tokens)
        {
            var isDereference = lastToken.Type == ExpressionParser.TokenType.Symbol && lastToken.Text == "*";
            var brush = token.Type switch
            {
                ExpressionParser.TokenType.Literal => isDereference ? TokenBrushes.Variable : TokenBrushes.String,
                ExpressionParser.TokenType.Symbol => token.Text == "*" ? Brushes.Green : TokenBrushes.Symbol,
                _ => TokenBrushes.Error,
            };

            lastToken = token;

            DrawText(token.Text, brush, ref position);
        }
    }

    public void DrawText(string text, Brush brush, ref PointF position)
    {
        var size = Graphics.MeasureString(text, Font, 0, StringFormat);
        var drawrect = new RectangleF(position, size);
        position.X += size.Width - Margin;

        Graphics.DrawString(text, Font, brush, drawrect);
    }
}
