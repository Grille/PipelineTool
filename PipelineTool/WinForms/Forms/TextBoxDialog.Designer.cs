using Grille.PipelineTool.WinForms.Controls;

namespace Grille.PipelineTool
{
    partial class TextBoxDialog
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
            textBox1 = new System.Windows.Forms.TextBox();
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            dialogButtonPanel = new DialogButtonPanel();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox1.Location = new System.Drawing.Point(12, 12);
            textBox1.Margin = new System.Windows.Forms.Padding(12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(299, 21);
            textBox1.TabIndex = 2;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel.Controls.Add(dialogButtonPanel, 0, 1);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.Size = new System.Drawing.Size(323, 92);
            tableLayoutPanel.TabIndex = 3;
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
            dialogButtonPanel.Location = new System.Drawing.Point(0, 45);
            dialogButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            dialogButtonPanel.Name = "dialogButtonPanel";
            dialogButtonPanel.Size = new System.Drawing.Size(323, 47);
            dialogButtonPanel.TabIndex = 3;
            dialogButtonPanel.Text = "okCancelPanel1";
            dialogButtonPanel.Button1Click += okCancelPanel1_Button1Click;
            dialogButtonPanel.Button2Click += okCancelPanel1_Button2Click;
            // 
            // TextBoxDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(323, 92);
            Controls.Add(tableLayoutPanel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TextBoxDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Text Dialog";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private DialogButtonPanel dialogButtonPanel;
    }
}