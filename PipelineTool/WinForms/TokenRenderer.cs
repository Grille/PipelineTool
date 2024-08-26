using Grille.PipelineTool.IO;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;

public static class TokenRenderer
{
    public record struct Context(Graphics Graphics, Font Font, float Margin, float CharSize);

    readonly static StringFormat StringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);

    public static Context CreateContext(Graphics g, Font font)
    {
        float margin = g.MeasureString(" ", font).Width;
        float charsize = g.MeasureString("O", font).Width - margin;
        return new Context(g, font, margin, charsize);
    }

    public static void DrawTokens(in Context ctx, IReadOnlyList<Token> tokens, ref PointF position)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            DrawToken(ctx, tokens[i], ref position);
        }
    }

    public static void DrawToken(in Context ctx, Token token, ref PointF position)
    {
        var text = token.Text.ToString();

        if (string.IsNullOrEmpty(text))
            return;

        (var g, var font, var margin, var _) = ctx;

        var size = g.MeasureString(text, font, 0, StringFormat);

        var drawrect = new RectangleF(position, size);

        if (token.Type == TokenType.Expression)
        {
            var tokens = Runtime.TokenizeParameterValue(token.Text);
            DrawTokens(ctx, tokens, ref position);
            return;
        }

        var brush = token.Type switch
        {
            TokenType.Text => TokenBrushes.Text,
            TokenType.Comment => TokenBrushes.Comment,
            TokenType.Flow => TokenBrushes.Flow,
            TokenType.ValueString => TokenBrushes.String,
            TokenType.ValueSymbol => TokenBrushes.Symbol,
            TokenType.ValueVariable => TokenBrushes.Variable,
            _ => TokenBrushes.Error,
        };

        g.DrawString(text, font, brush, drawrect);

        position.X += size.Width - margin;
    }
}
