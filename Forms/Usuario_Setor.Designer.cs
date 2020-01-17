namespace GTI_v4.Forms {
    partial class Usuario_Setor {
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
            this.CancelarButton = new System.Windows.Forms.Button();
            this.GravarButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SetorComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CancelarButton
            // 
            this.CancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelarButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CancelarButton.Image = global::GTI_v4.Properties.Resources.cancel2;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(349, 70);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(74, 22);
            this.CancelarButton.TabIndex = 162;
            this.CancelarButton.Text = "Cancelar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // GravarButton
            // 
            this.GravarButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GravarButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GravarButton.Image = global::GTI_v4.Properties.Resources.gravar;
            this.GravarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GravarButton.Location = new System.Drawing.Point(269, 70);
            this.GravarButton.Name = "GravarButton";
            this.GravarButton.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.GravarButton.Size = new System.Drawing.Size(74, 22);
            this.GravarButton.TabIndex = 161;
            this.GravarButton.Text = "&Gravar ";
            this.GravarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GravarButton.UseVisualStyleBackColor = true;
            this.GravarButton.Click += new System.EventHandler(this.GravarButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 160;
            this.label1.Text = "Informe o local atual em que esta trabalhando.";
            // 
            // SetorComboBox
            // 
            this.SetorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SetorComboBox.FormattingEnabled = true;
            this.SetorComboBox.Location = new System.Drawing.Point(13, 36);
            this.SetorComboBox.Name = "SetorComboBox";
            this.SetorComboBox.Size = new System.Drawing.Size(410, 21);
            this.SetorComboBox.TabIndex = 159;
            // 
            // Usuario_Setor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 108);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.GravarButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SetorComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Usuario_Setor";
            this.ShowInTaskbar = false;
            this.Text = "Informação necessária";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelarButton;
        private System.Windows.Forms.Button GravarButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SetorComboBox;
    }
}