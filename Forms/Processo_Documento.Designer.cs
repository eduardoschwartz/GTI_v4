namespace GTI_v4.Forms {
    partial class Processo_Documento {
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
            this.tBar = new System.Windows.Forms.ToolStrip();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.EditButton = new System.Windows.Forms.ToolStripButton();
            this.ExcluirButton = new System.Windows.Forms.ToolStripButton();
            this.SairButton = new System.Windows.Forms.ToolStripButton();
            this.MainList = new System.Windows.Forms.ListBox();
            this.tBar.SuspendLayout();
            this.SuspendLayout();
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
            this.ExcluirButton,
            this.SairButton});
            this.tBar.Location = new System.Drawing.Point(0, 274);
            this.tBar.Name = "tBar";
            this.tBar.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.tBar.Size = new System.Drawing.Size(304, 25);
            this.tBar.TabIndex = 29;
            this.tBar.Text = "toolStrip1";
            // 
            // AddButton
            // 
            this.AddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddButton.Image = global::GTI_v4.Properties.Resources.add_file;
            this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(23, 22);
            this.AddButton.Text = "toolStripButton1";
            this.AddButton.ToolTipText = "Novo";
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
            // 
            // ExcluirButton
            // 
            this.ExcluirButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExcluirButton.Image = global::GTI_v4.Properties.Resources.delete;
            this.ExcluirButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExcluirButton.Name = "ExcluirButton";
            this.ExcluirButton.Size = new System.Drawing.Size(23, 22);
            this.ExcluirButton.Text = "toolStripButton3";
            this.ExcluirButton.ToolTipText = "Excluir";
            // 
            // SairButton
            // 
            this.SairButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SairButton.Image = global::GTI_v4.Properties.Resources.Exit;
            this.SairButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SairButton.Name = "SairButton";
            this.SairButton.Size = new System.Drawing.Size(23, 22);
            this.SairButton.Text = "toolStripButton5";
            this.SairButton.ToolTipText = "Sair";
            // 
            // MainList
            // 
            this.MainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainList.FormattingEnabled = true;
            this.MainList.Location = new System.Drawing.Point(6, 3);
            this.MainList.Name = "MainList";
            this.MainList.Size = new System.Drawing.Size(291, 264);
            this.MainList.Sorted = true;
            this.MainList.TabIndex = 28;
            // 
            // Processo_Documento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 299);
            this.Controls.Add(this.tBar);
            this.Controls.Add(this.MainList);
            this.MaximizeBox = false;
            this.Name = "Processo_Documento";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Documentação para processos";
            this.tBar.ResumeLayout(false);
            this.tBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tBar;
        private System.Windows.Forms.ToolStripButton AddButton;
        private System.Windows.Forms.ToolStripButton EditButton;
        private System.Windows.Forms.ToolStripButton ExcluirButton;
        private System.Windows.Forms.ToolStripButton SairButton;
        private System.Windows.Forms.ListBox MainList;
    }
}