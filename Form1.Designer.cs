namespace WindowsFormsApp1
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGestureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateSamplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.generateXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(596, 52);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGestureToolStripMenuItem,
            this.readFromXMLToolStripMenuItem,
            this.offlineTrainingToolStripMenuItem,
            this.generateSamplesToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 48);
            this.toolStripMenuItem1.Text = "Operations";
            // 
            // loadGestureToolStripMenuItem
            // 
            this.loadGestureToolStripMenuItem.Name = "loadGestureToolStripMenuItem";
            this.loadGestureToolStripMenuItem.Size = new System.Drawing.Size(396, 46);
            this.loadGestureToolStripMenuItem.Text = "Load Gesture";
            this.loadGestureToolStripMenuItem.Click += new System.EventHandler(this.loadGestureToolStripMenuItem_Click);
            // 
            // readFromXMLToolStripMenuItem
            // 
            this.readFromXMLToolStripMenuItem.Name = "readFromXMLToolStripMenuItem";
            this.readFromXMLToolStripMenuItem.Size = new System.Drawing.Size(396, 46);
            this.readFromXMLToolStripMenuItem.Text = "Read from XML";
            this.readFromXMLToolStripMenuItem.Click += new System.EventHandler(this.readFromXMLToolStripMenuItem_Click);
            // 
            // offlineTrainingToolStripMenuItem
            // 
            this.offlineTrainingToolStripMenuItem.Name = "offlineTrainingToolStripMenuItem";
            this.offlineTrainingToolStripMenuItem.Size = new System.Drawing.Size(396, 46);
            this.offlineTrainingToolStripMenuItem.Text = "Offline training";
            this.offlineTrainingToolStripMenuItem.Click += new System.EventHandler(this.offlineTrainingToolStripMenuItem_Click);
            // 
            // generateSamplesToolStripMenuItem
            // 
            this.generateSamplesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slowToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.fastToolStripMenuItem,
            this.toolStripMenuItem2,
            this.generateXMLToolStripMenuItem});
            this.generateSamplesToolStripMenuItem.Name = "generateSamplesToolStripMenuItem";
            this.generateSamplesToolStripMenuItem.Size = new System.Drawing.Size(396, 46);
            this.generateSamplesToolStripMenuItem.Text = "Generate Samples";
            this.generateSamplesToolStripMenuItem.Click += new System.EventHandler(this.generateSamplesToolStripMenuItem_Click);
            // 
            // slowToolStripMenuItem
            // 
            this.slowToolStripMenuItem.Name = "slowToolStripMenuItem";
            this.slowToolStripMenuItem.Size = new System.Drawing.Size(312, 46);
            this.slowToolStripMenuItem.Text = "Slow";
            this.slowToolStripMenuItem.Click += new System.EventHandler(this.slowToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(312, 46);
            this.mediumToolStripMenuItem.Text = "Medium";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click);
            // 
            // fastToolStripMenuItem
            // 
            this.fastToolStripMenuItem.Name = "fastToolStripMenuItem";
            this.fastToolStripMenuItem.Size = new System.Drawing.Size(312, 46);
            this.fastToolStripMenuItem.Text = "Fast";
            this.fastToolStripMenuItem.Click += new System.EventHandler(this.fastToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(312, 46);
            // 
            // generateXMLToolStripMenuItem
            // 
            this.generateXMLToolStripMenuItem.Name = "generateXMLToolStripMenuItem";
            this.generateXMLToolStripMenuItem.Size = new System.Drawing.Size(312, 46);
            this.generateXMLToolStripMenuItem.Text = "GenerateXML";
            this.generateXMLToolStripMenuItem.Click += new System.EventHandler(this.generateXMLToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(463, 415);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 38);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(596, 465);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "$1 Recognizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadGestureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readFromXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineTrainingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateSamplesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem generateXMLToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
    }
}

