namespace TopDon_Phoenix_LogReader
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exportToolStripMenuItem = new ToolStripMenuItem();
            asCSVToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            constrainToolStripMenuItem = new ToolStripMenuItem();
            legendToolStripMenuItem = new ToolStripMenuItem();
            visibleToolStripMenuItem = new ToolStripMenuItem();
            positionToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip1
            //
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(1381, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            //
            // fileToolStripMenuItem
            //
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, toolStripSeparator1, exportToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            //
            // openToolStripMenuItem
            //
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(107, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            //
            // toolStripSeparator1
            //
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(104, 6);
            //
            // exportToolStripMenuItem
            //
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { asCSVToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(107, 22);
            exportToolStripMenuItem.Text = "Export";
            //
            // asCSVToolStripMenuItem
            //
            asCSVToolStripMenuItem.Name = "asCSVToolStripMenuItem";
            asCSVToolStripMenuItem.Size = new Size(111, 22);
            asCSVToolStripMenuItem.Text = "As CSV";
            asCSVToolStripMenuItem.Click += asCSVToolStripMenuItem_Click;
            //
            // toolStripSeparator2
            //
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(104, 6);
            //
            // exitToolStripMenuItem
            //
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(107, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            //
            // viewToolStripMenuItem
            //
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { refreshToolStripMenuItem, constrainToolStripMenuItem, legendToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            //
            // refreshToolStripMenuItem
            //
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(125, 22);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            //
            // constrainToolStripMenuItem
            //
            constrainToolStripMenuItem.Name = "constrainToolStripMenuItem";
            constrainToolStripMenuItem.Size = new Size(125, 22);
            constrainToolStripMenuItem.Text = "Constrain";
            constrainToolStripMenuItem.Click += constrainToolStripMenuItem_Click;
            //
            // legendToolStripMenuItem
            //
            legendToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { visibleToolStripMenuItem, positionToolStripMenuItem });
            legendToolStripMenuItem.Name = "legendToolStripMenuItem";
            legendToolStripMenuItem.Size = new Size(125, 22);
            legendToolStripMenuItem.Text = "Legend";
            //
            // visibleToolStripMenuItem
            //
            visibleToolStripMenuItem.Name = "visibleToolStripMenuItem";
            visibleToolStripMenuItem.Size = new Size(117, 22);
            visibleToolStripMenuItem.Text = "Visible";
            visibleToolStripMenuItem.Click += visibleToolStripMenuItem_Click;
            //
            // positionToolStripMenuItem
            //
            positionToolStripMenuItem.Name = "positionToolStripMenuItem";
            positionToolStripMenuItem.Size = new Size(117, 22);
            positionToolStripMenuItem.Text = "Position";
            //
            // splitContainer1
            //
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            splitContainer1.Panel1.Controls.Add(treeView1);
            //
            // splitContainer1.Panel2
            //
            splitContainer1.Panel2.Controls.Add(formsPlot1);
            splitContainer1.Size = new Size(1381, 601);
            splitContainer1.SplitterDistance = 423;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            splitContainer1.MouseDoubleClick += splitContainer1_MouseDoubleClick;
            //
            // treeView1
            //
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Margin = new Padding(4, 3, 4, 3);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(421, 599);
            treeView1.TabIndex = 0;
            //
            // formsPlot1
            //
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 0);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(951, 599);
            formsPlot1.TabIndex = 0;
            formsPlot1.VisibleChanged += formsPlot1_VisibleChanged;
            formsPlot1.MouseMove += formsPlot1_MouseMove;
            //
            // statusStrip1
            //
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 625);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(1381, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            //
            // toolStripStatusLabel1
            //
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            //
            // frmMain
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1381, 647);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmMain";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem constrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem legendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem positionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}