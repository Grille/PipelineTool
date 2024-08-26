using Grille.PipelineTool.IO;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms
{
    public partial class PipelinesControl : UserControl
    {
        public event EventHandler? ItemsChanged;

        protected void OnItemsChanged()
        {
            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }

        public AsyncPipelineExecuter Executer { get; }

        public PipelineList Pipelines { get; }

        public Pipeline? SelectedItem
        {
            get => ListBox.SelectedItem;
            set => ListBox.SelectedItem = value;
        }

        public IReadOnlyList<Pipeline> SelectedItems
        {
            get => ListBox.SelectedItems;
            set => ListBox.SelectedItems = value;
        }

        public PipelineTasksControl? TasksControl { get; set; }

        public EditValueDialog EditValueDialog { get; }

        public ILogger Logger { get => Executer.Runtime.Logger; set => Executer.Runtime.Logger = value; }

        public PipelinesControl()
        {
            InitializeComponent();

            Executer = new AsyncPipelineExecuter();
            Pipelines = new PipelineList();
            ListBox.BoundItems = Pipelines;

            EditValueDialog = new EditValueDialog();
            EditValueDialog.DisableMultiline();

            Executer.Runtime.PositionChanged += Executer_PositionChanged;

            Executer.ExecutionDone += Executer_ExecutionDone;

            ListBox.ItemsChanged += ListBox_ItemsChanged;
        }

        private void ListBox_ItemsChanged(object? sender, EventArgs e)
        {
            OnItemsChanged();
        }

        bool invalidated = false;

        private void Executer_PositionChanged(object? sender, EventArgs e)
        {
            invalidated = true;
        }

        private void Executer_ExecutionDone(object? sender, EventArgs e)
        {
            invalidated = true;
            // Called from executer task
            Invoke(() =>
            {
                UpdateEnabledRuntimeActions();
            });
        }

        public void UpdateEnabledActions()
        {
            bool single = SelectedItems.Count == 1;
            bool multi = SelectedItems.Count > 1;
            bool any = single || multi;

            newToolStripMenuItem.Enabled = ButtonNew.Enabled = !multi;
            copyToolStripMenuItem.Enabled = ButtonCopy.Enabled = any;
            pasteToolStripMenuItem.Enabled = ButtonPaste.Enabled = any;
            editToolStripMenuItem.Enabled = ButtonEdit.Enabled = single;
            deleteToolStripMenuItem.Enabled = ButtonRemove.Enabled = any;

            upToolStripMenuItem.Enabled = ButtonUp.Enabled = any;
            downToolStripMenuItem.Enabled = ButtonDown.Enabled = any;

            UpdateEnabledRuntimeActions();

            if (TasksControl == null)
                return;

            TasksControl.Enabled = single;
            TasksControl.Pipeline = SelectedItem;
            TasksControl.InvalidateItems(false);
        }

        public void UpdateEnabledRuntimeActions()
        {
            bool single = SelectedItems.Count == 1;
            bool running = Executer.Running;

            runToolStripMenuItem.Enabled = ButtonRun.Enabled = single && !running;
            stopToolStripMenuItem.Enabled = ButtonStop.Enabled = running;
        }

        public void InvalidateItems()
        {
            ListBox.UpdateItems(Pipelines);
            UpdateEnabledActions();
        }

        private void ListBoxIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabledActions();
        }

        private void RunClick(object sender, EventArgs e)
        {
            ArgumentNullException.ThrowIfNull(TasksControl);
            ArgumentNullException.ThrowIfNull(SelectedItem);

            Executer.Execute(SelectedItem, ParentForm);
            UpdateEnabledActions();
            TasksControl.SetEnabledActions();
        }

        private void StopClick(object sender, EventArgs e)
        {
            Executer.Cancel();
        }

        private void NewClick(object sender, EventArgs e)
        {
            ArgumentNullException.ThrowIfNull(Pipelines);

            EditValueDialog.Text = "New Pipeline";
            EditValueDialog.TextBox.Text = "Pipeline";

            var result = EditValueDialog.ShowDialog(ParentForm);
            if (result == DialogResult.OK)
            {
                string newname = EditValueDialog.TextBox.Text.Trim();
                string uname = Pipelines.GetUniqueName(newname);
                var pipeline = Pipelines.CreateUnbound(uname);
                Pipelines!.InsertAfter(SelectedItem, pipeline);
                InvalidateItems();
                SelectedItem = pipeline;

                OnItemsChanged();
            }
        }

        private void CopyClick(object sender, EventArgs e)
        {
            ListBox.CopyToClipboard();
        }

        private void EditClick(object sender, EventArgs e)
        {
            ArgumentNullException.ThrowIfNull(SelectedItem);

            string name = SelectedItem.Name;

            EditValueDialog.Text = "Edit Pipeline";
            EditValueDialog.TextBox.Text = name;

            var result = EditValueDialog.ShowDialog(ParentForm);
            if (result == DialogResult.OK)
            {
                string newname = EditValueDialog.TextBox.Text.Trim();
                if (newname == name)
                    return;

                string uname = Pipelines.GetUniqueName(newname);
                Pipelines.Rename(name, uname);
                ListBox.Items.Add(0);
                InvalidateItems();

                OnItemsChanged();
            }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            ListBox.RemoveSelectedItems();
        }

        private void UpClick(object sender, EventArgs e)
        {
            ListBox.MoveSelectedItemsUp();
        }

        private void DownClick(object sender, EventArgs e)
        {
            ListBox.MoveSelectedItemsDown();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (TasksControl == null)
                return;

            if (invalidated)
            {
                invalidated = false;
                ListBox.Invalidate();
                TasksControl.ListBox.Invalidate();
            }
        }

        private void ListBoxDoubleClick(object sender, EventArgs e)
        {
            if (SelectedItem == null)
                return;
            EditClick(sender, e);
        }

        private void ControlLoad(object sender, EventArgs e)
        {
            refreshTimer.Start();
        }

        private void ButtonPaste_Click(object sender, EventArgs e)
        {
            ListBox.PasteFromClipboard();
        }
    }
}
