using Grille.PipelineTool.IO;
using Grille.PipelineTool.Tasks;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;

public class PipelineTaskListBox : ListBox<PipelineTask>
{
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
            g.DrawString(text, font, Brushes.Gray, pos);
            return;
        }

        var tokens = task.ToTokens();
        var format = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
        foreach (var token in tokens)
        {
            var text = token.Text;

            if (string.IsNullOrEmpty(text))
                continue;

            var size = g.MeasureString(text, font, 0, format);
            //var size = TextRenderer.MeasureText(text, e.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);

            var drawrect = new RectangleF(position, boundsText.Y, size.Width, size.Height);

            var color = token.Type switch
            {
                PipelineTask.TokenType.Text => Color.Black,
                PipelineTask.TokenType.Comment => Color.Gray,
                PipelineTask.TokenType.Variable => text[0] == '*' || text[0] == '$' ? Color.Blue : Color.FromArgb(0,100,100),
                PipelineTask.TokenType.Flow => Color.DarkMagenta,
                _ => Color.Red,
            };

            var brush = new SolidBrush(color);

            //g.DrawRectangle(Pens.Red, drawrect.X,drawrect.Y,drawrect.Width-1,drawrect.Height-1);
            g.DrawString(token.Text, font, brush, drawrect);

            position += size.Width - margin;
        }

        //g.DrawString(task.ToString(), e.Font, Brushes.Magenta, boundsText);
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
