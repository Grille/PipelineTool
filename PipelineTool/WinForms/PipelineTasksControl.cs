using Grille.PipelineTool.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms
{
    public partial class PipelineTasksControl : UserControl
    {
        Pipeline? _pipeline;
        public Pipeline? Pipeline
        {
            get => _pipeline; set
            {
                _pipeline = value;
                if (_pipeline != null)
                {
                    ListBox.BoundItems = _pipeline.Tasks;
                }
            }
        }

        public PipelineTask? SelectedItem
        {
            get => ListBox.SelectedItem;
            set => ListBox.SelectedItem = value;
        }

        public IReadOnlyList<PipelineTask> SelectedItems
        {
            get => ListBox.SelectedItems;
            set => ListBox.SelectedItems = value;
        }

        EditTaksForm EditTaksForm { get; }


        public PipelineTasksControl()
        {
            InitializeComponent();
            EditTaksForm = new EditTaksForm();
        }


        public void SetEnabledActions()
        {
            bool single = SelectedItems.Count == 1;
            bool multi = SelectedItems.Count > 1;
            bool any = single || multi;
            bool invalid = SelectedItem is InvalidTypeTask;

            newToolStripMenuItem.Enabled = ButtonNew.Enabled = !multi;
            copyToolStripMenuItem.Enabled = ButtonCopy.Enabled = any;
            pasteToolStripMenuItem.Enabled = ButtonPaste.Enabled = any;
            editToolStripMenuItem.Enabled = ButtonEdit.Enabled = single;
            removeToolStripMenuItem.Enabled = ButtonRemove.Enabled = any;
            enabledToolStripMenuItem.Enabled = ButtonEnabled.Enabled = any;

            upToolStripMenuItem.Enabled = ButtonUp.Enabled = any;
            downToolStripMenuItem.Enabled = ButtonDown.Enabled = any;

            leftToolStripMenuItem.Enabled = ButtonLeft.Enabled = any;
            rightToolStripMenuItem.Enabled = ButtonRight.Enabled = any;
        }

        public void InvalidateItems(bool save = true)
        {
            if (Pipeline == null)
            {
                ListBox.Items.Clear();
            }
            else
            {
                ListBox.UpdateItems(Pipeline.Tasks);
            }
            SetEnabledActions();

            //if (save)
            //    Pipeline.Owner.Save();
        }


        private void NewClick(object sender, EventArgs e)
        {
            AssertPipelineNotNull();

            var task = new NopTask();

            Pipeline.Tasks.InsertAfter(SelectedItem, task);
            InvalidateItems();
            SelectedItem = null!;
            SelectedItem = task;

            /*
            var result = EditTaksForm.ShowDialog(this, Pipeline);
            if (result == DialogResult.OK)
            {
                Pipeline.Tasks.InsertAfter(SelectedItem, EditTaksForm.Task);
                InvalidateItems();
                SelectedItem = EditTaksForm.Task;
                SelectedItem.Pipeline = Pipeline;
            }
            */
        }


        private void EditClick(object sender, EventArgs e)
        {
            AssertPipelineNotNull();
            ArgumentNullException.ThrowIfNull(SelectedItem);

            var result = EditTaksForm.ShowDialog(this, Pipeline, SelectedItem);
            if (result == DialogResult.OK)
            {
                int idx = Pipeline.Tasks.IndexOf(SelectedItem);
                Pipeline.Tasks[idx] = EditTaksForm.Task;
                InvalidateItems();
            }
        }

        private void EnabledClick(object sender, EventArgs e)
        {
            foreach (var task in SelectedItems)
            {
                task.Enabled = !task.Enabled;
            }
            InvalidateItems();
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            AssertPipelineNotNull();

            foreach (var task in SelectedItems)
            {
                Pipeline.Tasks.Remove(task);
            }
            InvalidateItems();
        }

        private void CopyClick(object sender, EventArgs e)
        {
            AssertPipelineNotNull();

            ListBox.CopyToClipboard();
        }

        private void UpClick(object sender, EventArgs e)
        {
            AssertPipelineNotNull();

            ListBox.MoveSelectedItemsUp();
        }

        private void DownClick(object sender, EventArgs e)
        {
            AssertPipelineNotNull();

            ListBox.MoveSelectedItemsDown();
        }

        private void LeftClick(object sender, EventArgs e)
        {
            ListBox.MoveSelectedItemsLeft();
        }

        private void RightClick(object sender, EventArgs e)
        {
            ListBox.MoveSelectedItemsRight();
        }

        private void ListBoxDoubleClick(object sender, EventArgs e)
        {
            if (SelectedItem == null)
                return;
            EditClick(sender, e);
        }

        private void ListBoxIndexChanged(object sender, EventArgs e)
        {
            SetEnabledActions();
        }

        [MemberNotNull(nameof(Pipeline))]
        void AssertPipelineNotNull()
        {
            if (Pipeline == null)
                throw new InvalidOperationException();
        }

        private void ButtonPaste_Click(object sender, EventArgs e)
        {
            ListBox.PasteFromClipboard();
        }
    }
}
