﻿using Grille.PipelineTool.WinForms.Controls.Collections;

namespace Grille.PipelineTool.WinForms
{
    partial class PipelineTasksControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            ListBox = new PipelineTaskListBox();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            enabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            downToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            ButtonNew = new System.Windows.Forms.ToolStripButton();
            ButtonEdit = new System.Windows.Forms.ToolStripButton();
            ButtonRemove = new System.Windows.Forms.ToolStripButton();
            ButtonCopy = new System.Windows.Forms.ToolStripButton();
            ButtonPaste = new System.Windows.Forms.ToolStripButton();
            ButtonEnabled = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ButtonUp = new System.Windows.Forms.ToolStripButton();
            ButtonDown = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ButtonLeft = new System.Windows.Forms.ToolStripButton();
            ButtonRight = new System.Windows.Forms.ToolStripButton();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(ListBox);
            toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(500, 475);
            toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new System.Drawing.Size(500, 500);
            toolStripContainer1.TabIndex = 0;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // ListBox
            // 
            ListBox.BoundItems = null;
            ListBox.ContextMenuStrip = contextMenuStrip1;
            ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            ListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            ListBox.Executer = null;
            ListBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ListBox.FormattingEnabled = true;
            ListBox.IntegralHeight = false;
            ListBox.Location = new System.Drawing.Point(0, 0);
            ListBox.Name = "ListBox";
            ListBox.SelectedItem = null;
            ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            ListBox.Size = new System.Drawing.Size(500, 475);
            ListBox.TabIndex = 0;
            ListBox.SelectedIndexChanged += ListBoxIndexChanged;
            ListBox.DoubleClick += ListBoxDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, editToolStripMenuItem, removeToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, enabledToolStripMenuItem, toolStripSeparator2, upToolStripMenuItem, downToolStripMenuItem, toolStripSeparator3, leftToolStripMenuItem, rightToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(117, 236);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = Properties.Resources.New;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += NewClick;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Image = Properties.Resources.Edit;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.Click += EditClick;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Image = Properties.Resources.Delete;
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            removeToolStripMenuItem.Text = "Delete";
            removeToolStripMenuItem.Click += DeleteClick;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Image = Properties.Resources.Copy;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += CopyClick;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Image = Properties.Resources.Paste;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += ButtonPaste_Click;
            // 
            // enabledToolStripMenuItem
            // 
            enabledToolStripMenuItem.Image = Properties.Resources.ToggleButton;
            enabledToolStripMenuItem.Name = "enabledToolStripMenuItem";
            enabledToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            enabledToolStripMenuItem.Text = "Enabled";
            enabledToolStripMenuItem.Click += EnabledClick;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // upToolStripMenuItem
            // 
            upToolStripMenuItem.Image = Properties.Resources.MoveUp;
            upToolStripMenuItem.Name = "upToolStripMenuItem";
            upToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            upToolStripMenuItem.Text = "Up";
            upToolStripMenuItem.Click += UpClick;
            // 
            // downToolStripMenuItem
            // 
            downToolStripMenuItem.Image = Properties.Resources.MoveDown;
            downToolStripMenuItem.Name = "downToolStripMenuItem";
            downToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            downToolStripMenuItem.Text = "Down";
            downToolStripMenuItem.Click += DownClick;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(113, 6);
            // 
            // leftToolStripMenuItem
            // 
            leftToolStripMenuItem.Image = Properties.Resources.MoveLeft;
            leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            leftToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            leftToolStripMenuItem.Text = "Left";
            leftToolStripMenuItem.Click += LeftClick;
            // 
            // rightToolStripMenuItem
            // 
            rightToolStripMenuItem.Image = Properties.Resources.MoveRight;
            rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            rightToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            rightToolStripMenuItem.Text = "Right";
            rightToolStripMenuItem.Click += RightClick;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ButtonNew, ButtonEdit, ButtonRemove, ButtonCopy, ButtonPaste, ButtonEnabled, toolStripSeparator1, ButtonUp, ButtonDown, toolStripSeparator4, ButtonLeft, ButtonRight });
            toolStrip1.Location = new System.Drawing.Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(254, 25);
            toolStrip1.TabIndex = 0;
            // 
            // ButtonNew
            // 
            ButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonNew.Image = Properties.Resources.New;
            ButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonNew.Name = "ButtonNew";
            ButtonNew.Size = new System.Drawing.Size(23, 22);
            ButtonNew.Text = "New";
            ButtonNew.Click += NewClick;
            // 
            // ButtonEdit
            // 
            ButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonEdit.Image = Properties.Resources.Edit;
            ButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonEdit.Name = "ButtonEdit";
            ButtonEdit.Size = new System.Drawing.Size(23, 22);
            ButtonEdit.Text = "Edit";
            ButtonEdit.Click += EditClick;
            // 
            // ButtonRemove
            // 
            ButtonRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonRemove.Image = Properties.Resources.Delete;
            ButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonRemove.Name = "ButtonRemove";
            ButtonRemove.Size = new System.Drawing.Size(23, 22);
            ButtonRemove.Text = "Delete";
            ButtonRemove.Click += DeleteClick;
            // 
            // ButtonCopy
            // 
            ButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonCopy.Image = Properties.Resources.Copy;
            ButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonCopy.Name = "ButtonCopy";
            ButtonCopy.Size = new System.Drawing.Size(23, 22);
            ButtonCopy.Text = "Copy";
            ButtonCopy.Click += CopyClick;
            // 
            // ButtonPaste
            // 
            ButtonPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonPaste.Image = Properties.Resources.Paste;
            ButtonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonPaste.Name = "ButtonPaste";
            ButtonPaste.Size = new System.Drawing.Size(23, 22);
            ButtonPaste.Text = "Paste";
            ButtonPaste.Click += ButtonPaste_Click;
            // 
            // ButtonEnabled
            // 
            ButtonEnabled.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonEnabled.Image = Properties.Resources.ToggleButton;
            ButtonEnabled.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonEnabled.Name = "ButtonEnabled";
            ButtonEnabled.Size = new System.Drawing.Size(23, 22);
            ButtonEnabled.Text = "Enabled";
            ButtonEnabled.Click += EnabledClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonUp
            // 
            ButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonUp.Image = Properties.Resources.MoveUp;
            ButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonUp.Name = "ButtonUp";
            ButtonUp.Size = new System.Drawing.Size(23, 22);
            ButtonUp.Text = "Up";
            ButtonUp.Click += UpClick;
            // 
            // ButtonDown
            // 
            ButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonDown.Image = Properties.Resources.MoveDown;
            ButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonDown.Name = "ButtonDown";
            ButtonDown.Size = new System.Drawing.Size(23, 22);
            ButtonDown.Text = "Down";
            ButtonDown.Click += DownClick;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonLeft
            // 
            ButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonLeft.Image = Properties.Resources.MoveLeft;
            ButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonLeft.Name = "ButtonLeft";
            ButtonLeft.Size = new System.Drawing.Size(23, 22);
            ButtonLeft.Text = "Left";
            ButtonLeft.Click += LeftClick;
            // 
            // ButtonRight
            // 
            ButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            ButtonRight.Image = Properties.Resources.MoveRight;
            ButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            ButtonRight.Name = "ButtonRight";
            ButtonRight.Size = new System.Drawing.Size(23, 22);
            ButtonRight.Text = "Right";
            ButtonRight.Click += RightClick;
            // 
            // PipelineTasksControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(toolStripContainer1);
            DoubleBuffered = true;
            Name = "PipelineTasksControl";
            Size = new System.Drawing.Size(500, 500);
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ButtonNew;
        private System.Windows.Forms.ToolStripButton ButtonEdit;
        private System.Windows.Forms.ToolStripButton ButtonRemove;
        private System.Windows.Forms.ToolStripButton ButtonDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ButtonUp;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ButtonCopy;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        public PipelineTaskListBox ListBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton ButtonLeft;
        private System.Windows.Forms.ToolStripButton ButtonRight;
        private System.Windows.Forms.ToolStripButton ButtonEnabled;
        private System.Windows.Forms.ToolStripMenuItem enabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ButtonPaste;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}
