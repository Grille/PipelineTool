namespace Grille.PipelineTool.WinForms.Controls;

partial class ConsoleControl
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
        consoleListBox1 = new Collections.ConsoleListBox();
        contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
        clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStrip1 = new System.Windows.Forms.ToolStrip();
        toolStripButton1 = new System.Windows.Forms.ToolStripButton();
        toolStripButton2 = new System.Windows.Forms.ToolStripButton();
        toolStripContainer1.ContentPanel.SuspendLayout();
        toolStripContainer1.LeftToolStripPanel.SuspendLayout();
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
        toolStripContainer1.ContentPanel.Controls.Add(consoleListBox1);
        toolStripContainer1.ContentPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(536, 388);
        toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        // 
        // toolStripContainer1.LeftToolStripPanel
        // 
        toolStripContainer1.LeftToolStripPanel.Controls.Add(toolStrip1);
        toolStripContainer1.Location = new System.Drawing.Point(0, 0);
        toolStripContainer1.Name = "toolStripContainer1";
        toolStripContainer1.Size = new System.Drawing.Size(560, 388);
        toolStripContainer1.TabIndex = 0;
        toolStripContainer1.Text = "toolStripContainer1";
        // 
        // consoleListBox1
        // 
        consoleListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        consoleListBox1.ContextMenuStrip = contextMenuStrip1;
        consoleListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        consoleListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        consoleListBox1.FormattingEnabled = true;
        consoleListBox1.IntegralHeight = false;
        consoleListBox1.ItemHeight = 14;
        consoleListBox1.Location = new System.Drawing.Point(0, 0);
        consoleListBox1.Margin = new System.Windows.Forms.Padding(0);
        consoleListBox1.Name = "consoleListBox1";
        consoleListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        consoleListBox1.Size = new System.Drawing.Size(536, 388);
        consoleListBox1.TabIndex = 0;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { clearToolStripMenuItem, copyToolStripMenuItem });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
        // 
        // clearToolStripMenuItem
        // 
        clearToolStripMenuItem.Image = Properties.Resources.Delete;
        clearToolStripMenuItem.Name = "clearToolStripMenuItem";
        clearToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
        clearToolStripMenuItem.Text = "Clear";
        clearToolStripMenuItem.Click += toolStripButton1_Click;
        // 
        // copyToolStripMenuItem
        // 
        copyToolStripMenuItem.Image = Properties.Resources.Copy;
        copyToolStripMenuItem.Name = "copyToolStripMenuItem";
        copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
        copyToolStripMenuItem.Text = "Copy";
        copyToolStripMenuItem.Click += toolStripButton2_Click;
        // 
        // toolStrip1
        // 
        toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton1, toolStripButton2 });
        toolStrip1.Location = new System.Drawing.Point(0, 3);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new System.Drawing.Size(24, 57);
        toolStrip1.TabIndex = 0;
        // 
        // toolStripButton1
        // 
        toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton1.Image = Properties.Resources.Delete;
        toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripButton1.Name = "toolStripButton1";
        toolStripButton1.Size = new System.Drawing.Size(22, 20);
        toolStripButton1.Text = "Clear";
        toolStripButton1.Click += toolStripButton1_Click;
        // 
        // toolStripButton2
        // 
        toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton2.Image = Properties.Resources.Copy;
        toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripButton2.Name = "toolStripButton2";
        toolStripButton2.Size = new System.Drawing.Size(22, 20);
        toolStripButton2.Text = "Copy";
        toolStripButton2.Click += toolStripButton2_Click;
        // 
        // ConsoleControl
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(toolStripContainer1);
        DoubleBuffered = true;
        Name = "ConsoleControl";
        Size = new System.Drawing.Size(560, 388);
        toolStripContainer1.ContentPanel.ResumeLayout(false);
        toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
        toolStripContainer1.LeftToolStripPanel.PerformLayout();
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
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    public Collections.ConsoleListBox ConsoleListBox;
    private Collections.ConsoleListBox consoleListBox1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
}
