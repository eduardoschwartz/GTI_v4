namespace GTI_v4.Forms {
    partial class ZoomBox {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btSair = new System.Windows.Forms.ToolStripButton();
            this.txtZoom = new System.Windows.Forms.TextBox();
            this.tBar = new System.Windows.Forms.ToolStrip();
            this.tBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSair
            // 
            this.btSair.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSair.Image = global::GTI_v4.Properties.Resources.Exit;
            this.btSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSair.Name = "btSair";
            this.btSair.Size = new System.Drawing.Size(23, 22);
            this.btSair.Text = "toolStripButton5";
            this.btSair.ToolTipText = "Sair";
            this.btSair.Click += new System.EventHandler(this.btSair_Click);
            // 
            // txtZoom
            // 
            this.txtZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtZoom.Location = new System.Drawing.Point(0, 0);
            this.txtZoom.Multiline = true;
            this.txtZoom.Name = "txtZoom";
            this.txtZoom.Size = new System.Drawing.Size(422, 231);
            this.txtZoom.TabIndex = 25;
            // 
            // tBar
            // 
            this.tBar.AllowMerge = false;
            this.tBar.BackColor = System.Drawing.SystemColors.Control;
            this.tBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSair});
            this.tBar.Location = new System.Drawing.Point(0, 231);
            this.tBar.Name = "tBar";
            this.tBar.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.tBar.Size = new System.Drawing.Size(422, 25);
            this.tBar.TabIndex = 24;
            this.tBar.TabStop = true;
            this.tBar.Text = "toolStrip1";
            // 
            // ZoomBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 256);
            this.Controls.Add(this.txtZoom);
            this.Controls.Add(this.tBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ZoomBox";
            this.ShowInTaskbar = false;
            this.Text = "Zoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ZoomBox_FormClosed);
            this.tBar.ResumeLayout(false);
            this.tBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btSair;
        private System.Windows.Forms.TextBox txtZoom;
        private System.Windows.Forms.ToolStrip tBar;
    }
}