namespace PLSDesktopApi
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
            this.components = new System.ComponentModel.Container();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.userBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.changeToSaviorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToTouristToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeUserConditionToSavedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeUserConditionToEmergencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.gMapControl1.Location = new System.Drawing.Point(-3, -1);
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeToSaviorToolStripMenuItem,
            this.changeToTouristToolStripMenuItem,
            this.changeUserConditionToSavedToolStripMenuItem,
            this.changeUserConditionToEmergencyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(271, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // changeUserConditionToSavedToolStripMenuItem
            // 
            this.changeUserConditionToSavedToolStripMenuItem.Name = "changeUserConditionToSavedToolStripMenuItem";
            this.changeUserConditionToSavedToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 465);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gMapControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
    }
}

