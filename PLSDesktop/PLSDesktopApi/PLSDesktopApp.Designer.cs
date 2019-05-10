namespace PLSDesktopApi
{
    partial class PLSDesktopApp
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
            this.components = new System.ComponentModel.Container();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.userBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeToSaviorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToTouristToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeUserConditionToSavedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeUserConditionToEmergencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gMapControl1
            // 
            this.gMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(-5, -8);
            this.gMapControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 100;
            this.gMapControl1.MinZoom = 5;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(797, 475);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 5D;
            this.gMapControl1.Load += new System.EventHandler(this.gMapControl1_Load);
            // 
            // userBox
            // 
            this.userBox.BackColor = System.Drawing.SystemColors.Window;
            this.userBox.ContextMenuStrip = this.contextMenuStrip1;
            this.userBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.userBox.FormattingEnabled = true;
            this.userBox.ItemHeight = 20;
            this.userBox.Items.AddRange(new object[] {
            "All users"});
            this.userBox.Location = new System.Drawing.Point(0, 26);
            this.userBox.Name = "userBox";
            this.userBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.userBox.Size = new System.Drawing.Size(121, 444);
            this.userBox.TabIndex = 1;
            this.userBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeToSaviorToolStripMenuItem,
            this.changeToTouristToolStripMenuItem,
            this.changeUserConditionToSavedToolStripMenuItem,
            this.changeUserConditionToEmergencyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(271, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // changeToSaviorToolStripMenuItem
            // 
            this.changeToSaviorToolStripMenuItem.Name = "changeToSaviorToolStripMenuItem";
            this.changeToSaviorToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.changeToSaviorToolStripMenuItem.Text = "Change to savior";
            this.changeToSaviorToolStripMenuItem.Click += new System.EventHandler(this.changeToSaviorToolStripMenuItem_Click);
            // 
            // changeToTouristToolStripMenuItem
            // 
            this.changeToTouristToolStripMenuItem.Name = "changeToTouristToolStripMenuItem";
            this.changeToTouristToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.changeToTouristToolStripMenuItem.Text = "Change to tourist";
            this.changeToTouristToolStripMenuItem.Click += new System.EventHandler(this.changeToTouristToolStripMenuItem_Click);
            // 
            // changeUserConditionToSavedToolStripMenuItem
            // 
            this.changeUserConditionToSavedToolStripMenuItem.Name = "changeUserConditionToSavedToolStripMenuItem";
            this.changeUserConditionToSavedToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.changeUserConditionToSavedToolStripMenuItem.Text = "Change user condition to saved";
            this.changeUserConditionToSavedToolStripMenuItem.Click += new System.EventHandler(this.changeUserConditionToSavedToolStripMenuItem_Click);
            // 
            // changeUserConditionToEmergencyToolStripMenuItem
            // 
            this.changeUserConditionToEmergencyToolStripMenuItem.Name = "changeUserConditionToEmergencyToolStripMenuItem";
            this.changeUserConditionToEmergencyToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.changeUserConditionToEmergencyToolStripMenuItem.Text = "Change user condition to emergency";
            this.changeUserConditionToEmergencyToolStripMenuItem.Click += new System.EventHandler(this.changeUserConditionToEmergencyToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuText;
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Controls.Add(this.userBox);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(121, 475);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(0, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(121, 20);
            this.searchBox.TabIndex = 3;
            this.searchBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Location = new System.Drawing.Point(224, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(382, 101);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(77, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(183, 34);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(59, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Password:";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(14, 34);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(57, 20);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "Username:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(248, 34);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 3;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(151, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(113, 4);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(179, 20);
            this.textBox5.TabIndex = 5;
            this.textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // PLSDesktopApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 465);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gMapControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PLSDesktopApp";
            this.Text = "Public-Localization-Services";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.ListBox userBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeToSaviorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeToTouristToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeUserConditionToSavedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeUserConditionToEmergencyToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.TextBox textBox5;
    }
}

