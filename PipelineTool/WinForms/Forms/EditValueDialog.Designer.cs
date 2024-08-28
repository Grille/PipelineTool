using Grille.PipelineTool.WinForms.Controls;

namespace Grille.PipelineTool.WinForms;

partial class EditValueDialog
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
        tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        dialogButtonPanel = new DialogButtonPanel();
        TextBox = new System.Windows.Forms.TextBox();
        toolStrip1 = new System.Windows.Forms.ToolStrip();
        toolStripButton1 = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        toolStripButton2 = new System.Windows.Forms.ToolStripButton();
        toolStripButton3 = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        toolStripButton4 = new System.Windows.Forms.ToolStripButton();
        tableLayoutPanel1.SuspendLayout();
        toolStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(dialogButtonPanel, 0, 1);
        tableLayoutPanel1.Controls.Add(TextBox, 0, 0);
        tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        tableLayoutPanel1.Size = new System.Drawing.Size(284, 91);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // dialogButtonPanel
        // 
        dialogButtonPanel.BackColor = System.Drawing.SystemColors.ControlLight;
        dialogButtonPanel.Button1Text = "Cancel";
        dialogButtonPanel.Button1Visible = true;
        dialogButtonPanel.Button2Text = "Ok";
        dialogButtonPanel.Button2Visible = true;
        dialogButtonPanel.Button3Text = "Button_3";
        dialogButtonPanel.Button3Visible = false;
        dialogButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        dialogButtonPanel.Location = new System.Drawing.Point(0, 44);
        dialogButtonPanel.Margin = new System.Windows.Forms.Padding(0);
        dialogButtonPanel.Name = "dialogButtonPanel";
        dialogButtonPanel.Size = new System.Drawing.Size(284, 47);
        dialogButtonPanel.TabIndex = 0;
        dialogButtonPanel.Text = "dialogButtonPanel1";
        dialogButtonPanel.Button1Click += okCancelPanel1_Button1Click;
        dialogButtonPanel.Button2Click += okCancelPanel1_Button2Click;
        // 
        // TextBox
        // 
        TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
        TextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        TextBox.Location = new System.Drawing.Point(12, 12);
        TextBox.Margin = new System.Windows.Forms.Padding(12);
        TextBox.Multiline = true;
        TextBox.Name = "TextBox";
        TextBox.Size = new System.Drawing.Size(260, 20);
        TextBox.TabIndex = 1;
        TextBox.WordWrap = false;
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton1, toolStripSeparator1, toolStripButton2, toolStripButton3, toolStripSeparator2, toolStripButton4 });
        toolStrip1.Location = new System.Drawing.Point(0, 0);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new System.Drawing.Size(284, 25);
        toolStrip1.TabIndex = 2;
        toolStrip1.Text = "toolStrip1";
        // 
        // toolStripButton1
        // 
        toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton1.Image = Properties.Resources.OpenFile;
        toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripButton1.Name = "toolStripButton1";
        toolStripButton1.Size = new System.Drawing.Size(23, 22);
        toolStripButton1.Text = "Load File";
        toolStripButton1.Click += loadFileToolStripMenuItem_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
        // 
        // toolStripButton2
        // 
        toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton2.Image = Properties.Resources.FileDestination;
        toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripButton2.Name = "toolStripButton2";
        toolStripButton2.Size = new System.Drawing.Size(23, 22);
        toolStripButton2.Text = "File Path";
        toolStripButton2.Click += openFileToolStripMenuItem_Click;
        // 
        // toolStripButton3
        // 
        toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton3.Image = Properties.Resources.FolderDestination;
        toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripButton3.Name = "toolStripButton3";
        toolStripButton3.Size = new System.Drawing.Size(23, 22);
        toolStripButton3.Text = "Folder Path";
        toolStripButton3.Click += folderToolStripMenuItem_Click;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
        // 
        // toolStripButton4
        // 
        toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton4.Image = Properties.Resources.ColorDialog;
        toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripButton4.Name = "toolStripButton4";
        toolStripButton4.Size = new System.Drawing.Size(23, 22);
        toolStripButton4.Text = "Select Color";
        toolStripButton4.Click += toolStripButton4_Click;
        // 
        // EditValueDialog
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(284, 116);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(toolStrip1);
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new System.Drawing.Size(200, 155);
        Name = "EditValueDialog";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Edit Value";
        ResizeEnd += EditValueDialog_ResizeEnd;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    public System.Windows.Forms.TextBox TextBox;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private DialogButtonPanel dialogButtonPanel;
}