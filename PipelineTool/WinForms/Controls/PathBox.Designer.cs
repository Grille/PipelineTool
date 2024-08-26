namespace Grille.PipelineTool.WinForms;

partial class PathBox
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
        Button = new System.Windows.Forms.Button();
        TextBox = new System.Windows.Forms.TextBox();
        SuspendLayout();
        // 
        // Button
        // 
        Button.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        Button.Location = new System.Drawing.Point(172, 0);
        Button.Name = "Button";
        Button.Size = new System.Drawing.Size(28, 23);
        Button.TabIndex = 0;
        Button.Text = "...";
        Button.UseVisualStyleBackColor = true;
        Button.Click += Button_Click;
        // 
        // TextBox
        // 
        TextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        TextBox.Location = new System.Drawing.Point(0, 0);
        TextBox.Margin = new System.Windows.Forms.Padding(0);
        TextBox.Name = "TextBox";
        TextBox.Size = new System.Drawing.Size(169, 23);
        TextBox.TabIndex = 1;
        // 
        // PathBox
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(TextBox);
        Controls.Add(Button);
        Name = "PathBox";
        Size = new System.Drawing.Size(200, 23);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    public System.Windows.Forms.Button Button;
    public System.Windows.Forms.TextBox TextBox;
}
