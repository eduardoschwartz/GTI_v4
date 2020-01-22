namespace GTI_v4.Forms {
    partial class Processo_Despacho {
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
            this.MainList = new System.Windows.Forms.CheckedListBox();
            this.tBar = new System.Windows.Forms.ToolStrip();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.EditButton = new System.Windows.Forms.ToolStripButton();
            this.DelButton = new System.Windows.Forms.ToolStripButton();
            this.AtivarButton = new System.Windows.Forms.ToolStripButton();
            this.ExitButton = new System.Windows.Forms.ToolStripButton();
            this.tBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainList
            // 
            this.MainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainList.FormattingEnabled = true;
            this.MainList.Location = new System.Drawing.Point(6, 3);
            this.MainList.Name = "MainList";
            this.MainList.Size = new System.Drawing.Size(292, 259);
            this.MainList.TabIndex = 31;
            this.MainList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.MainList_ItemCheck);
            this.MainList.DoubleClick += new System.EventHandler(this.MainList_DoubleClick);
            // 
            // tBar
            // 
            this.tBar.AllowMerge = false;
            this.tBar.BackColor = System.Drawing.SystemColors.Control;
            this.tBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.EditButton,
            this.DelButton,
            this.AtivarButton,
            this.ExitButton});
            this.tBar.Location = new System.Drawing.Point(0, 274);
            this.tBar.Name = "tBar";
            this.tBar.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.tBar.Size = new System.Drawing.Size(304, 25);
            this.tBar.TabIndex = 30;
            this.tBar.Text = "toolStrip1";
            // 
            // AddButton
            // 
            this.AddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddButton.Image = global::GTI_v4.Properties.Resources.add;
            this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(23, 22);
            this.AddButton.Text = "toolStripButton1";
            this.AddButton.ToolTipText = "Novo";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditButton.Image = global::GTI_v4.Properties.Resources.Alterar;
            this.EditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(23, 22);
            this.EditButton.Text = "toolStripButton2";
            this.EditButton.ToolTipText = "Alterar";
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // DelButton
            // 
            this.DelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DelButton.Image = global::GTI_v4.Properties.Resources.delete;
            this.DelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(23, 22);
            this.DelButton.Text = "toolStripButton3";
            this.DelButton.ToolTipText = "Excluir";
            this.DelButton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // AtivarButton
            // 
            this.AtivarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AtivarButton.Image = global::GTI_v4.Properties.Resources.more_1_;
            this.AtivarButton.ImageTransparentColor = System.Drawing.Color.White;
            this.AtivarButton.Name = "AtivarButton";
            this.AtivarButton.Size = new System.Drawing.Size(23, 22);
            this.AtivarButton.Text = "toolStripButton1";
            this.AtivarButton.ToolTipText = "Ativar ou desativar o despacho";
            this.AtivarButton.Click += new System.EventHandler(this.AtivarButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExitButton.Image = global::GTI_v4.Properties.Resources.Exit;
            this.ExitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(23, 22);
            this.ExitButton.Text = "toolStripButton5";
            this.ExitButton.ToolTipText = "Sair";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // Processo_Despacho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 299);
            this.Controls.Add(this.MainList);
            this.Controls.Add(this.tBar);
            this.MaximizeBox = false;
            this.Name = "Processo_Despacho";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Despacho dos Processos";
            this.tBar.ResumeLayout(false);
            this.tBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox MainList;
        private System.Windows.Forms.ToolStrip tBar;
        private System.Windows.Forms.ToolStripButton AddButton;
        private System.Windows.Forms.ToolStripButton EditButton;
        private System.Windows.Forms.ToolStripButton DelButton;
        private System.Windows.Forms.ToolStripButton AtivarButton;
        private System.Windows.Forms.ToolStripButton ExitButton;
    }
}