namespace GTI_v4.Forms {
    partial class ErrorBox {
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
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.btDetail = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDetail
            // 
            this.txtDetail.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDetail.Location = new System.Drawing.Point(11, 121);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetail.Size = new System.Drawing.Size(362, 156);
            this.txtDetail.TabIndex = 9;
            // 
            // btDetail
            // 
            this.btDetail.Image = global::GTI_v4.Properties.Resources.Abaixo;
            this.btDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btDetail.Location = new System.Drawing.Point(299, 80);
            this.btDetail.Name = "btDetail";
            this.btDetail.Size = new System.Drawing.Size(75, 23);
            this.btDetail.TabIndex = 8;
            this.btDetail.Text = "Detalhe";
            this.btDetail.UseVisualStyleBackColor = true;
            this.btDetail.Click += new System.EventHandler(this.btDetail_Click);
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOk.Location = new System.Drawing.Point(218, 80);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 7;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMsg.Location = new System.Drawing.Point(75, 14);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(299, 51);
            this.txtMsg.TabIndex = 5;
            this.txtMsg.TabStop = false;
            this.txtMsg.Text = "Mensagem";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::GTI_v4.Properties.Resources.Error;
            this.pictureBox1.Location = new System.Drawing.Point(11, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 51);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // ErrorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 108);
            this.Controls.Add(this.txtDetail);
            this.Controls.Add(this.btDetail);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ErrorBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ErrorBox";
            this.Load += new System.EventHandler(this.ErrorBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Button btDetail;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtMsg;
    }
}