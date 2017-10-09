namespace Ksu.Cis300.MapViewer
{
    partial class UserInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.uxButtonBar = new System.Windows.Forms.ToolStrip();
            this.uxOpenMap = new System.Windows.Forms.ToolStripButton();
            this.uxZoomIn = new System.Windows.Forms.ToolStripButton();
            this.uxZoomOut = new System.Windows.Forms.ToolStripButton();
            this.uxMapContainer = new System.Windows.Forms.Panel();
            this.uxOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxButtonBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxButtonBar
            // 
            this.uxButtonBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.uxButtonBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxOpenMap,
            this.uxZoomIn,
            this.uxZoomOut});
            this.uxButtonBar.Location = new System.Drawing.Point(0, 0);
            this.uxButtonBar.Name = "uxButtonBar";
            this.uxButtonBar.Size = new System.Drawing.Size(553, 27);
            this.uxButtonBar.TabIndex = 0;
            this.uxButtonBar.Text = "toolStrip1";
            // 
            // uxOpenMap
            // 
            this.uxOpenMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxOpenMap.Image = ((System.Drawing.Image)(resources.GetObject("uxOpenMap.Image")));
            this.uxOpenMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxOpenMap.Name = "uxOpenMap";
            this.uxOpenMap.Size = new System.Drawing.Size(83, 24);
            this.uxOpenMap.Text = "Open Map";
            // 
            // uxZoomIn
            // 
            this.uxZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("uxZoomIn.Image")));
            this.uxZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxZoomIn.Name = "uxZoomIn";
            this.uxZoomIn.Size = new System.Drawing.Size(69, 24);
            this.uxZoomIn.Text = "Zoom In";
            // 
            // uxZoomOut
            // 
            this.uxZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("uxZoomOut.Image")));
            this.uxZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxZoomOut.Name = "uxZoomOut";
            this.uxZoomOut.Size = new System.Drawing.Size(81, 24);
            this.uxZoomOut.Text = "Zoom Out";
            // 
            // uxMapContainer
            // 
            this.uxMapContainer.AutoScroll = true;
            this.uxMapContainer.Location = new System.Drawing.Point(0, 30);
            this.uxMapContainer.MinimumSize = new System.Drawing.Size(246, 171);
            this.uxMapContainer.Name = "uxMapContainer";
            this.uxMapContainer.Size = new System.Drawing.Size(553, 447);
            this.uxMapContainer.TabIndex = 1;
            // 
            // uxOpenDialog
            // 
            this.uxOpenDialog.Filter = "CSV files|*.csv|All Files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 476);
            this.Controls.Add(this.uxMapContainer);
            this.Controls.Add(this.uxButtonBar);
            this.Name = "Form1";
            this.Text = "Map Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.uxButtonBar.ResumeLayout(false);
            this.uxButtonBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip uxButtonBar;
        private System.Windows.Forms.ToolStripButton uxOpenMap;
        private System.Windows.Forms.ToolStripButton uxZoomIn;
        private System.Windows.Forms.ToolStripButton uxZoomOut;
        private System.Windows.Forms.Panel uxMapContainer;
        private System.Windows.Forms.OpenFileDialog uxOpenDialog;
    }
}

