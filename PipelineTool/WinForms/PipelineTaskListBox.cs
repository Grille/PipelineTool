using Grille.PipelineTool.IO;
using Grille.PipelineTool.Tasks;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Grille.PipelineTool.WinForms;

public class PipelineTaskListBox : ListBox<PipelineTask>
{
    readonly static StringFormat StringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);

    protected override void OnDrawItem(DrawItemEventArgs e, PipelineTask task)
    {
        if (Executer == null)
            return;

        var font = e.Font;
        if (font == null)
            return;

        var pipeline = ((PipelineTaskList)BoundItems).Pipeline;

        var g = e.Graphics;

        Brush brushLineBack = Brushes.Gainsboro;
        Brush brushLine = Brushes.DimGray;
        //Brush brushText;

        /*
        if (task is NopTask)
            brushText = new SolidBrush(Color.Gray);
        else if (task is InvalidTypeTask)
            brushText = new SolidBrush(Color.Red);
        else
            brushText = new SolidBrush(Color.Black);
        */

        
        var entry = Executer.Runtime.CallStack.ToList().FirstOrDefault(a => a?.Pipeline == pipeline, null);
        if (entry != null && entry.Position == e.Index)
        {
            brushLineBack = Brushes.LightGreen;
            brushLine = Brushes.DarkGreen;
        }
        


        int lineColumnWidth = 24;

        var boundsLine = (RectangleF)e.Bounds;
        boundsLine.Width = lineColumnWidth;
        var boundsText = (RectangleF)e.Bounds;
        boundsText.Width -= lineColumnWidth;
        boundsText.X += lineColumnWidth;

        g.FillRectangle(brushLineBack, boundsLine);

        g.DrawString((e.Index + 1).ToString(), font, brushLine, boundsLine);

        float margin = g.MeasureString(" ", font).Width;
        float charsize = g.MeasureString("O", font).Width - margin;

        float position = boundsText.X + (charsize * task.Scope * 4);

        if (!task.Enabled)
        {
            var text = task.ToString();
            var pos = new PointF(position, boundsText.Y);
            g.DrawString(text, font, TokenBrushes.Comment, pos);
            return;
        }

        var tokens = task.ToTokens();
        foreach (var token in tokens)
        {
            DrawToken(e, token, ref position, boundsText.Y, margin);
        }

        //g.DrawString(task.ToString(), e.Font, Brushes.Magenta, boundsText);
    }

    void DrawToken(DrawItemEventArgs e, Token token, ref float position, float posY, float margin)
    {
        var g = e.Graphics;
        var font = e.Font!;
        var text = token.Text.ToString();

        if (string.IsNullOrEmpty(text))
            return;

        var size = g.MeasureString(text, font, 0, StringFormat);

        var drawrect = new RectangleF(position, posY, size.Width, size.Height);

        if (token.Type == TokenType.Expression)
        {
            var list = new List<Token>();
            Runtime.TokenizeParameterValue(token.Text, list);
            foreach (var subtoken in list) {
                DrawToken(e, subtoken, ref position, posY, margin);
            }
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

        position += size.Width - margin;
    }

    bool HandleKeyDown(KeyEventArgs e)
    {
        if (e.Alt)
        {
            if (e.KeyCode == Keys.Left)
            {
                MoveSelectedItemsLeft();
                return true;
            }
            if (e.KeyCode == Keys.Right)
            {
                MoveSelectedItemsRight();
                return true;
            }
        }

        return false;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        e.SuppressKeyPress |= HandleKeyDown(e);
        base.OnKeyDown(e);
    }

    public void MoveSelectedItemsLeft()
    {
        var selected = SelectedItems;
        foreach (var task in selected)
        {
            if (task.Scope > 0)
            {
                task.Scope -= 1;
            }
        }
        Invalidate();

        OnItemsChanged();
    }

    public void MoveSelectedItemsRight()
    {
        var selected = SelectedItems;
        selected.Reverse();
        foreach (var task in selected)
        {
            task.Scope += 1;
        }
        Invalidate();

        OnItemsChanged();
    }

    protected override void OnCopyToClipboard()
    {
        var array = SelectedItems.ToArray();

        using var stream = new MemoryStream();
        using var tr = new StreamReader(stream);

        TextSerializer.Serialize(stream, array);
        stream.Seek(0, SeekOrigin.Begin);
        var text = tr.ReadToEnd();

        Clipboard.SetText(text);
    }

    protected override void OnPasteFromClipboard()
    {
        var text = Clipboard.GetText();
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        var result = TextSerializer.Deserialize(stream);

        InsertItems(result.Headless);
    }

    protected override PipelineTask CreateNew()
    {
        return new NopTask();
    }
}
