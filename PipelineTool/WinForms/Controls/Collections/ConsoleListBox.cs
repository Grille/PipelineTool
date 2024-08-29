using Grille.PipelineTool.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Controls.Collections;

public class ConsoleListBox : ListBox<ConsoleListBox.LogEntry>, ILogger
{
    public record LogEntry(DateTime Date, Brush Brush, object Obj);

    public ConsoleListBox()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
    }

    void ILogger.Clear()
    {
        Invoke(Items.Clear);
    }

    string? ILogger.Input()
    {
        return null;
    }

    void ILogger.Error(string obj)
    {
        Add(Brushes.Red, obj);
    }

    void ILogger.Info(string obj)
    {
        Add(Brushes.Black, obj);
    }

    void ILogger.System(string obj)
    {
        Add(Brushes.Gray, obj);
    }

    void ILogger.Warn(string obj)
    {
        Add(Brushes.Blue, obj);
    }

    void Add(Brush brush, string text)
    {
        var entry = new LogEntry(DateTime.Now, brush, text);
        Invoke(() =>
        {
            Items.Add(entry);
            TopIndex = Items.Count - 1;
        });
    }

    protected override void OnDrawItem(DrawItemEventArgs e, LogEntry item)
    {
        e.DrawBackground();

        var g = e.Graphics;

        if (e.State.HasFlag(DrawItemState.Selected))
        {
            g.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
        }

        g.DrawString(item.Obj.ToString(), Font, item.Brush, e.Bounds);
    }

    protected override LogEntry CreateNew()
    {
        return new LogEntry(DateTime.Now, Brushes.Black, string.Empty);
    }

    protected override void OnCopyToClipboard()
    {
        var array = SelectedItems.ToArray();

        var sb = new StringBuilder();

        foreach (var item in array)
        {
            sb.AppendLine(item.Obj.ToString());
        }

        Clipboard.SetText(sb.ToString());
    }

    protected override void OnPasteFromClipboard()
    {
        return;
    }
}
