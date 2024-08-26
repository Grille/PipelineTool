using Grille.PipelineTool.WinForms.Controls;

namespace Grille.PipelineTool.WinForms;

partial class RichTextForm
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
        RichTextBox = new System.Windows.Forms.RichTextBox();
        dialogButtonPanel = new DialogButtonPanel();
        tableLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(RichTextBox, 0, 0);
        tableLayoutPanel1.Controls.Add(dialogButtonPanel, 0, 1);
        tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
        tableLayoutPanel1.Size = new System.Drawing.Size(584, 361);
        tableLayoutPanel1.TabIndex = 3;
        // 
        // RichTextBox
        // 
        RichTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
        RichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
        RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
        RichTextBox.Location = new System.Drawing.Point(0, 0);
        RichTextBox.Margin = new System.Windows.Forms.Padding(0);
        RichTextBox.Name = "RichTextBox";
        RichTextBox.ReadOnly = true;
        RichTextBox.Size = new System.Drawing.Size(584, 314);
        RichTextBox.TabIndex = 3;
        RichTextBox.Text = "";
        // 
        // dialogButtonPanel
        // 
        dialogButtonPanel.BackColor = System.Drawing.SystemColors.ControlLight;
        dialogButtonPanel.Button1Text = "Ok";
        dialogButtonPanel.Button1Visible = true;
        dialogButtonPanel.Button2Text = "Button_2";
        dialogButtonPanel.Button2Visible = false;
        dialogButtonPanel.Button3Text = "Button_3";
        dialogButtonPanel.Button3Visible = false;
        dialogButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        dialogButtonPanel.Location = new System.Drawing.Point(0, 314);
        dialogButtonPanel.Margin = new System.Windows.Forms.Padding(0);
        dialogButtonPanel.Name = "dialogButtonPanel";
        dialogButtonPanel.Size = new System.Drawing.Size(584, 47);
        dialogButtonPanel.TabIndex = 4;
        dialogButtonPanel.Text = "dialogButtonPanel1";
        dialogButtonPanel.Button1Click += button1_Click;
        // 
        // RichTextForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        ClientSize = new System.Drawing.Size(584, 361);
        Controls.Add(tableLayoutPanel1);
        DoubleBuffered = true;
        Name = "RichTextForm";
        SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "RtfTextForm";
        tableLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    public System.Windows.Forms.RichTextBox RichTextBox;
    private DialogButtonPanel dialogButtonPanel;
}