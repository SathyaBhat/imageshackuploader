namespace imguruploader
{
    partial class frmISUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmISUpload));
            this.niSnap = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyThumbnailLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // niSnap
            // 
            this.niSnap.ContextMenuStrip = this.contextMenuStrip1;
            this.niSnap.Icon = ((System.Drawing.Icon)(resources.GetObject("niSnap.Icon")));
            this.niSnap.Visible = true;
            this.niSnap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niSnap_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.SelModeToolStripMenuItem,
            this.copyThumbnailLinkToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 92);
           
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // SelModeToolStripMenuItem
            // 
            this.SelModeToolStripMenuItem.Name = "SelModeToolStripMenuItem";
            this.SelModeToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.SelModeToolStripMenuItem.Text = "Selection Mode";
            this.SelModeToolStripMenuItem.Click += new System.EventHandler(this.SelModeToolStripMenuItem_Click);
            // 
            // copyThumbnailLinkToolStripMenuItem
            // 
            this.copyThumbnailLinkToolStripMenuItem.Name = "copyThumbnailLinkToolStripMenuItem";
            this.copyThumbnailLinkToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.copyThumbnailLinkToolStripMenuItem.Text = "Copy Thumbnail Link";
            this.copyThumbnailLinkToolStripMenuItem.Click += new System.EventHandler(this.copyThumbnailLinkToolStripMenuItem_Click);
            // 
            // frmISUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmISUpload";
            this.Text = "ImageShack Uploader";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmISUpload_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niSnap;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyThumbnailLinkToolStripMenuItem;
    }
}

