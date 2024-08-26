namespace Grille.PipelineTool.WinForms
{
    partial class PipelineToolControl
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
            IO.ConsoleLogger consoleLogger1 = new IO.ConsoleLogger();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            PipelinesControl = new PipelinesControl();
            TasksControl = new PipelineTasksControl();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(PipelinesControl);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(TasksControl);
            splitContainer1.Size = new System.Drawing.Size(1321, 811);
            splitContainer1.SplitterDistance = 241;
            splitContainer1.TabIndex = 0;
            // 
            // PipelinesControl
            // 
            PipelinesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            PipelinesControl.Location = new System.Drawing.Point(0, 0);
            PipelinesControl.Logger = consoleLogger1;
            PipelinesControl.Name = "PipelinesControl";
            PipelinesControl.SelectedItem = null;
            PipelinesControl.Size = new System.Drawing.Size(241, 811);
            PipelinesControl.TabIndex = 0;
            PipelinesControl.TasksControl = null;
            // 
            // TasksControl
            // 
            TasksControl.Dock = System.Windows.Forms.DockStyle.Fill;
            TasksControl.Location = new System.Drawing.Point(0, 0);
            TasksControl.Name = "TasksControl";
            TasksControl.Pipeline = null;
            TasksControl.SelectedItem = null;
            TasksControl.Size = new System.Drawing.Size(1076, 811);
            TasksControl.TabIndex = 0;
            // 
            // PipelineToolControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "PipelineToolControl";
            Size = new System.Drawing.Size(1321, 811);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public PipelinesControl PipelinesControl;
        public PipelineTasksControl TasksControl;
    }
}
