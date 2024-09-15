using Grille.PipelineTool.IO;
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
using static Grille.PipelineTool.Expressions.ExpressionParser;

namespace Grille.PipelineTool.WinForms.Controls.Collections;

public class PipelineListBox : ListBox<Pipeline>
{
    protected override void OnDrawItem(DrawItemEventArgs e, Pipeline pipeline)
    {
        var g = e.Graphics;

        var font = e.Font;
        if (font == null)
            return;

        Brush brushLineBack = Brushes.Gainsboro;
        Brush brushLine = Brushes.DimGray;
        Brush brushText;

        brushText = pipeline.Tasks.ContainsInvalid ? Brushes.Red : Brushes.Black;

        if (Executer == null)
            return;

        bool stackContains = Executer.Runtime.CallStack.ToList().Any(a => a.Pipeline == pipeline);

        if (stackContains)
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

        if (e.State.HasFlag(DrawItemState.Selected))
        {
            var srect = new RectangleF(boundsText.X, boundsText.Y, boundsText.Width, boundsText.Height);
            g.FillRectangle(new SolidBrush(Color.LightBlue), srect);
        }

        if (stackContains)
        {
            var list = Executer.Runtime.CallStack.ToList();
            list.Reverse();
            int stackIdx = list.FindIndex(a => a.Pipeline == pipeline);
            g.DrawString((stackIdx + 1).ToString(), font, brushLine, boundsLine);
        }

        g.DrawString(pipeline.ToString(), font, brushText, boundsText);
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

        var items = (PipelineList)BoundItems;

        var pipelines = new List<Pipeline>();

        foreach (var pair in result.Sections)
        {
            var name = items.GetUniqueName(pair.Key);
            var pipeline = items.CreateUnbound(name);
            foreach (var task in pair.Value)
            {
                pipeline.Tasks.Add(task);
            }

            pipelines.Add(pipeline);
        }

        InsertItems(pipelines);
    }

    protected override Pipeline CreateNew()
    {
        var name = ((PipelineList)BoundItems).GetUniqueName("Pipeline");
        return new Pipeline((PipelineList)BoundItems, name);
    }
}
