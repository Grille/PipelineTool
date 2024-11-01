using Grille.PipelineTool.WinForms;
using System.Windows.Forms;

namespace Grille.PipelineTool
{
    partial class PipelineToolForm
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
            MenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            consoleToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            loadExampleToolStripMenuItem = new ToolStripMenuItem();
            showInfoToolStripMenuItem = new ToolStripMenuItem();
            PipelineCmdControl = new WinForms.Controls.PipelineCmdControl();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MenuStrip
            // 
            MenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem });
            MenuStrip.Location = new System.Drawing.Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new System.Drawing.Size(784, 24);
            MenuStrip.TabIndex = 0;
            MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator2, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = Properties.Resources.New;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = Properties.Resources.OpenFile;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = Properties.Resources.Save;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Image = Properties.Resources.SaveAs;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Image = Properties.Resources.Exit;
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            quitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            quitToolStripMenuItem.Text = "Exit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { consoleToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Checked = true;
            consoleToolStripMenuItem.CheckOnClick = true;
            consoleToolStripMenuItem.CheckState = CheckState.Checked;
            consoleToolStripMenuItem.Image = Properties.Resources.Console;
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            consoleToolStripMenuItem.Text = "Console";
            consoleToolStripMenuItem.Click += consoleToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadExampleToolStripMenuItem, showInfoToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // loadExampleToolStripMenuItem
            // 
            loadExampleToolStripMenuItem.Image = Properties.Resources.TextFile;
            loadExampleToolStripMenuItem.Name = "loadExampleToolStripMenuItem";
            loadExampleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            loadExampleToolStripMenuItem.Text = "Load Example";
            loadExampleToolStripMenuItem.Click += loadExampleToolStripMenuItem_Click;
            // 
            // showInfoToolStripMenuItem
            // 
            showInfoToolStripMenuItem.Image = Properties.Resources.Help;
            showInfoToolStripMenuItem.Name = "showInfoToolStripMenuItem";
            showInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            showInfoToolStripMenuItem.Text = "Show Tutorial";
            showInfoToolStripMenuItem.Click += showInfoToolStripMenuItem_Click;
            // 
            // PipelineCmdControl
            // 
            PipelineCmdControl.ConsoleVisible = false;
            PipelineCmdControl.Dock = DockStyle.Fill;
            PipelineCmdControl.Location = new System.Drawing.Point(0, 24);
            PipelineCmdControl.Name = "PipelineCmdControl";
            PipelineCmdControl.Size = new System.Drawing.Size(784, 537);
            PipelineCmdControl.TabIndex = 1;
            // 
            // PipelineToolForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 561);
            Controls.Add(PipelineCmdControl);
            Controls.Add(MenuStrip);
            DoubleBuffered = true;
            HelpButton = true;
            MainMenuStrip = MenuStrip;
            Name = "PipelineToolForm";
            Text = "Synetic Pipeline Tool";
            FormClosing += PipelineToolForm_FormClosing;
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem loadExampleToolStripMenuItem;
        private ToolStripMenuItem showInfoToolStripMenuItem;
        public MenuStrip MenuStrip;
        private WinForms.Controls.PipelineCmdControl PipelineCmdControl;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem consoleToolStripMenuItem;
    }
}