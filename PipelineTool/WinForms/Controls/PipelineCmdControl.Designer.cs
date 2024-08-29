namespace Grille.PipelineTool.WinForms.Controls;

partial class PipelineCmdControl
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
        splitContainer1 = new System.Windows.Forms.SplitContainer();
        PipelineToolControl = new PipelineToolControl();
        consoleControl = new ConsoleControl();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        SuspendLayout();
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
        splitContainer1.Location = new System.Drawing.Point(0, 0);
        splitContainer1.Name = "splitContainer1";
        splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(PipelineToolControl);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(consoleControl);
        splitContainer1.Size = new System.Drawing.Size(800, 600);
        splitContainer1.SplitterDistance = 400;
        splitContainer1.TabIndex = 0;
        // 
        // PipelineToolControl
        // 
        PipelineToolControl.Dock = System.Windows.Forms.DockStyle.Fill;
        PipelineToolControl.Location = new System.Drawing.Point(0, 0);
        PipelineToolControl.Name = "PipelineToolControl";
        PipelineToolControl.Size = new System.Drawing.Size(800, 400);
        PipelineToolControl.TabIndex = 0;
        // 
        // consoleControl
        // 
        consoleControl.Dock = System.Windows.Forms.DockStyle.Fill;
        consoleControl.Location = new System.Drawing.Point(0, 0);
        consoleControl.Name = "consoleControl";
        consoleControl.Size = new System.Drawing.Size(800, 196);
        consoleControl.TabIndex = 0;
        // 
        // PipelineCmdControl
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(splitContainer1);
        Name = "PipelineCmdControl";
        Size = new System.Drawing.Size(800, 600);
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    public PipelineToolControl PipelineToolControl;
    private ConsoleControl consoleControl;
}
