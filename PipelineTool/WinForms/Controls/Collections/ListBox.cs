using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Controls.Collections;

public abstract class ListBox<T> : ListBox where T : class
{
    public event EventHandler? ItemsChanged;

    protected void OnItemsChanged()
    {
        ItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IList<T> BoundItems { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new T? SelectedItem
    {
        get => (T?)base.SelectedItem;
        set => base.SelectedItem = value;
    }

    class SelectedItemsReadOnlyView : IReadOnlyList<T>
    {
        readonly T[] list;

        public SelectedItemsReadOnlyView(SelectedObjectCollection collection)
        {
            list = new T[collection.Count];
            for (int i = 0; i < collection.Count; i++)
            {
                list[i] = (T)collection[i];
            }
        }

        public T this[int index] => list[index];

        public int Count => list.Length;

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)list).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }

    public new IReadOnlyList<T> SelectedItems
    {
        get
        {
            return new SelectedItemsReadOnlyView(base.SelectedItems);
        }
        set
        {
            ClearSelected();
            foreach (var task in value)
            {
                int idx = Items.IndexOf(task);
                SetSelected(idx, true);
            }
        }
    }

    AsyncPipelineExecuter? _executer;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AsyncPipelineExecuter? Executer
    {
        get => _executer;
        set
        {
            if (value == null)
                return;

            _executer = value;
        }
    }

    public ListBox()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        DrawMode = DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;

        BoundItems = new List<T>(0);
    }

    protected sealed override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.FillRectangle(new SolidBrush(BackColor), e.ClipRectangle);

        for (int i = 0; i < Items.Count; ++i)
        {
            Rectangle irect = GetItemRectangle(i);
            if (e.ClipRectangle.IntersectsWith(irect))
            {
                var itemState = Enabled ? IsSelected(i) ? DrawItemState.Selected : DrawItemState.Default : DrawItemState.Disabled;
                var eventArgs = new DrawItemEventArgs(g, Font, irect, i, itemState, ForeColor, BackColor);
                OnDrawItem(eventArgs);
            }
        }
    }

    private bool IsSelected(int i) => SelectionMode switch
    {
        SelectionMode.One => SelectedIndex == i,
        SelectionMode.MultiSimple => SelectedIndices.Contains(i),
        SelectionMode.MultiExtended => SelectedIndices.Contains(i),
        _ => false
    };

    protected abstract T CreateNew();

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {

    }

    protected sealed override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index == -1)
            return;

        e.Graphics.FillRectangle(Brushes.White, e.Bounds);

        var item = (T)Items[e.Index];

        try
        {
            OnDrawItem(e, item);
        }
        catch { }
    }

    protected abstract void OnDrawItem(DrawItemEventArgs e, T item);

    public void UpdateItems(IList<T> list, bool invalidate = true)
    {
        BoundItems = list;

        var items = Items;

        BeginUpdate();

        var selectet = SelectedItem;

        if (items.Count == list.Count)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (items[i] != list[i])
                {
                    items[i] = list[i];
                }
            }
        }
        else
        {
            items.Clear();
            foreach (var item in list)
            {
                items.Add(item);
            }
            SelectedItem = selectet;
        }

        if (selectet != null && SelectedItem != selectet)
            SelectedItem = selectet;

        EndUpdate();

        if (invalidate)
        {
            Invalidate();
        }
    }

    bool HandleKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            var item = CreateNew();
            InsertItems(new[] { item });
        }
        else if (e.KeyCode == Keys.Delete)
        {
            RemoveSelectedItems();
            return true;
        }
        else if (e.Control)
        {
            if (e.KeyCode == Keys.A)
            {
                SelectAll();
                return true;
            }
            else if (e.KeyCode == Keys.C)
            {
                CopyToClipboard();
                return true;
            }
            else if (e.KeyCode == Keys.V)
            {
                PasteFromClipboard();
                return true;
            }
            else if (e.KeyCode == Keys.X)
            {
                CopyToClipboard();
                RemoveSelectedItems();
                return true;
            }
        }
        else if (e.Alt)
        {
            if (e.KeyCode == Keys.Up)
            {
                MoveSelectedItemsUp();
                return true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                MoveSelectedItemsDown();
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

    public void CopyToClipboard()
    {
        OnCopyToClipboard();
    }

    public void PasteFromClipboard()
    {
        try
        {
            OnPasteFromClipboard();
        }
        catch (Exception e)
        {
            ExceptionBox.Show(FindForm(), e);
        }
    }

    protected abstract void OnCopyToClipboard();

    protected abstract void OnPasteFromClipboard();

    public void MoveSelectedItemsUp()
    {
        var selected = SelectedItems;
        foreach (var item in selected)
        {
            BoundItems.UpItem(item);
        }
        UpdateItems(BoundItems);
        SelectedItems = selected;

        OnItemsChanged();
    }

    public void MoveSelectedItemsDown()
    {
        var selected = SelectedItems;
        var reverse = selected.Reverse();
        foreach (var item in reverse)
        {
            BoundItems.DownItem(item);
        }
        UpdateItems(BoundItems);
        SelectedItems = selected;

        OnItemsChanged();
    }

    public void RemoveSelectedItems()
    {
        var selected = SelectedItems;
        foreach (var item in selected)
        {
            BoundItems.Remove(item);
        }
        UpdateItems(BoundItems);

        OnItemsChanged();
    }

    public void SelectAll()
    {
        SelectedItems = (IReadOnlyList<T>)BoundItems;
    }

    public void InsertItems(IReadOnlyList<T> values)
    {
        var nselect = new List<T>();

        int count = SelectedItems.Count;
        if (count == 0)
        {
            foreach (var item in values)
            {
                BoundItems.Add(item);
            }

        }

        else
        {
            var selected = SelectedItem!;

            var reverse = values.Reverse();
            foreach (var item in reverse)
            {
                BoundItems.InsertAfter(selected, item);
            }

        }


        UpdateItems(BoundItems);
        SelectedItems = values;

        OnItemsChanged();
    }
}
