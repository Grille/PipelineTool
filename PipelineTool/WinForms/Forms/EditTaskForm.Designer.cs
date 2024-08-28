using Grille.PipelineTool.WinForms.Controls;

namespace Grille.PipelineTool
{
    partial class EditTaskForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox = new System.Windows.Forms.TextBox();
            groupBoxParameters = new System.Windows.Forms.GroupBox();
            panelParameters = new System.Windows.Forms.Panel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            treeViewTypes = new TaskTypeTreeView();
            dialogButtonPanel1 = new DialogButtonPanel();
            textBoxType = new System.Windows.Forms.TextBox();
            groupBoxParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox.Location = new System.Drawing.Point(6, 32);
            textBox.Name = "textBox";
            textBox.ReadOnly = true;
            textBox.Size = new System.Drawing.Size(488, 22);
            textBox.TabIndex = 0;
            // 
            // groupBoxParameters
            // 
            groupBoxParameters.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxParameters.Controls.Add(panelParameters);
            groupBoxParameters.Location = new System.Drawing.Point(3, 61);
            groupBoxParameters.Name = "groupBoxParameters";
            groupBoxParameters.Size = new System.Drawing.Size(491, 450);
            groupBoxParameters.TabIndex = 3;
            groupBoxParameters.TabStop = false;
            groupBoxParameters.Text = "Parameters";
            // 
            // panelParameters
            // 
            panelParameters.AutoScroll = true;
            panelParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            panelParameters.Location = new System.Drawing.Point(3, 19);
            panelParameters.Name = "panelParameters";
            panelParameters.Size = new System.Drawing.Size(485, 428);
            panelParameters.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeViewTypes);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dialogButtonPanel1);
            splitContainer1.Panel2.Controls.Add(textBoxType);
            splitContainer1.Panel2.Controls.Add(groupBoxParameters);
            splitContainer1.Panel2.Controls.Add(textBox);
            splitContainer1.Size = new System.Drawing.Size(784, 561);
            splitContainer1.SplitterDistance = 283;
            splitContainer1.TabIndex = 7;
            // 
            // treeViewTypes
            // 
            treeViewTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            treeViewTypes.HideSelection = false;
            treeViewTypes.Location = new System.Drawing.Point(0, 0);
            treeViewTypes.Name = "treeViewTypes";
            treeViewTypes.SelectedType = null;
            treeViewTypes.ShowNodeToolTips = true;
            treeViewTypes.Size = new System.Drawing.Size(283, 561);
            treeViewTypes.TabIndex = 0;
            treeViewTypes.BeforeSelect += treeViewTypes_BeforeSelect;
            treeViewTypes.AfterSelect += treeViewTypes_AfterSelect;
            // 
            // dialogButtonPanel1
            // 
            dialogButtonPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            dialogButtonPanel1.Button1Text = "Cancel";
            dialogButtonPanel1.Button1Visible = true;
            dialogButtonPanel1.Button2Text = "Ok";
            dialogButtonPanel1.Button2Visible = true;
            dialogButtonPanel1.Button3Text = "Reload";
            dialogButtonPanel1.Button3Visible = true;
            dialogButtonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            dialogButtonPanel1.Location = new System.Drawing.Point(0, 514);
            dialogButtonPanel1.Margin = new System.Windows.Forms.Padding(0);
            dialogButtonPanel1.Name = "dialogButtonPanel1";
            dialogButtonPanel1.Size = new System.Drawing.Size(497, 47);
            dialogButtonPanel1.TabIndex = 9;
            dialogButtonPanel1.Text = "dialogButtonPanel1";
            dialogButtonPanel1.Button1Click += buttonCancel_Click;
            dialogButtonPanel1.Button2Click += buttonOK_Click;
            dialogButtonPanel1.Button3Click += buttonReload_Click;
            // 
            // textBoxType
            // 
            textBoxType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxType.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxType.Location = new System.Drawing.Point(6, 3);
            textBoxType.Name = "textBoxType";
            textBoxType.ReadOnly = true;
            textBoxType.Size = new System.Drawing.Size(488, 22);
            textBoxType.TabIndex = 8;
            // 
            // EditTaskForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 561);
            Controls.Add(splitContainer1);
            DoubleBuffered = true;
            Name = "EditTaskForm";
            ShowInTaskbar = false;
            Text = "EditTaksForm";
            groupBoxParameters.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Panel panelParameters;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TaskTypeTreeView treeViewTypes;
        private System.Windows.Forms.TextBox textBoxType;
        private DialogButtonPanel dialogButtonPanel1;
    }
}