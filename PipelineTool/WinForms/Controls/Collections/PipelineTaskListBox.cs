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

namespace Grille.PipelineTool.WinForms.Controls.Collections;

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

        var tokens = task.ToTokens();

        var renderer = new TokenRenderer(g, font);
        var indentationWidth = renderer.CharSize * task.Scope * 4;
        var selectionWidth = indentationWidth + renderer.CharSize * (tokens.GetTextLength() + 1);
        var position = new PointF(boundsText.X + indentationWidth, boundsText.Y);

        if (e.State.HasFlag(DrawItemState.Selected))
        {
            var srect = new RectangleF(boundsText.X, boundsText.Y, selectionWidth, boundsText.Height);
            g.FillRectangle(new SolidBrush(Color.LightBlue), srect);
        }

        if (task.Enabled)
        {
            renderer.DrawTokens(tokens, ref position);
        }
        else
        {
            renderer.DrawTokensSingleColor(tokens, TokenBrushes.Comment, ref position);
        }
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
