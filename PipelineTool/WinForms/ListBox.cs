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

namespace Grille.PipelineTool.WinForms;

public abstract class ListBox<T> : ListBox where T : class
{
    public List<T> BoundItems { get; set; }

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
    public AsyncPipelineExecuter? Executer { 
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

    protected override void OnPaint(PaintEventArgs e)
    {
        Region iRegion = new Region(e.ClipRectangle);
        e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
        if (Items.Count == 0)
            return;

        for (int i = 0; i < this.Items.Count; ++i)
        {
            System.Drawing.Rectangle irect = this.GetItemRectangle(i);
            if (e.ClipRectangle.IntersectsWith(irect))
            {
                if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
                || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
                || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                {
                    OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                        irect, i,
                        DrawItemState.Selected, this.ForeColor,
                        this.BackColor));
                }
                else
                {
                    OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                        irect, i,
                        DrawItemState.Default, this.ForeColor,
                        this.BackColor));
                }
                iRegion.Complement(irect);
            }
        }

        //base.OnPaint(e);
    }

    protected abstract T CreateNew();

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (Executer == null)
            return;

        if (e.Index == -1)
            return;

        if (e.State.HasFlag(DrawItemState.Selected))
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
        }
        else
        {
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
        }

        var item = (T)Items[e.Index];

        try
        {
            OnDrawItem(e, item);
        }
        catch { }
    }

    protected abstract void OnDrawItem(DrawItemEventArgs e, T item);

    public void UpdateItems(List<T> list)
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
        Invalidate();
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
    }

    public void RemoveSelectedItems()
    {
        var selected = SelectedItems;
        foreach (var item in selected)
        {
            BoundItems.Remove(item);
        }
        UpdateItems(BoundItems);
    }

    public void SelectAll()
    {
        SelectedItems = BoundItems;
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
    }
}
