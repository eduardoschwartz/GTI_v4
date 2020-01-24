namespace GTI_v4.Forms {
    partial class Imovel_Novo {
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
            this.OkButton = new System.Windows.Forms.ToolStripButton();
            this.CancButton = new System.Windows.Forms.ToolStripButton();
            this.SubUnidadeList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UnidadeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Inscricao = new System.Windows.Forms.MaskedTextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.TipoList = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.tBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tBar
            // 
            this.tBar.BackColor = System.Drawing.Color.Transparent;
            this.tBar.Dock = System.Windows.Forms.DockStyle.None;
            this.tBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OkButton,
            this.CancButton});
            this.tBar.Location = new System.Drawing.Point(307, 105);
            this.tBar.Name = "tBar";
            this.tBar.Size = new System.Drawing.Size(80, 25);
            this.tBar.TabIndex = 211;
            this.tBar.Text = "toolStrip1";
            // 
            // OkButton
            // 
            this.OkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OkButton.Image = global::GTI_v4.Properties.Resources.OK;
            this.OkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(23, 22);
            this.OkButton.Text = "toolStripButton1";
            this.OkButton.ToolTipText = "Criar imóvel";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancButton
            // 
            this.CancButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CancButton.Image = global::GTI_v4.Properties.Resources.cancel2;
            this.CancButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancButton.Name = "CancButton";
            this.CancButton.Size = new System.Drawing.Size(23, 22);
            this.CancButton.Text = "toolStripButton2";
            this.CancButton.ToolTipText = "Cancelar";
            this.CancButton.Click += new System.EventHandler(this.CancButton_Click);
            // 
            // SubUnidadeList
            // 
            this.SubUnidadeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubUnidadeList.FormattingEnabled = true;
            this.SubUnidadeList.Location = new System.Drawing.Point(307, 72);
            this.SubUnidadeList.Name = "SubUnidadeList";
            this.SubUnidadeList.Size = new System.Drawing.Size(52, 21);
            this.SubUnidadeList.TabIndex = 209;
            this.SubUnidadeList.SelectedIndexChanged += new System.EventHandler(this.SubUnidadeList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(229, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 210;
            this.label3.Text = "SubUnidade.:";
            // 
            // UnidadeList
            // 
            this.UnidadeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UnidadeList.FormattingEnabled = true;
            this.UnidadeList.Location = new System.Drawing.Point(162, 72);
            this.UnidadeList.Name = "UnidadeList";
            this.UnidadeList.Size = new System.Drawing.Size(52, 21);
            this.UnidadeList.TabIndex = 207;
            this.UnidadeList.SelectedIndexChanged += new System.EventHandler(this.UnidadeList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(94, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 208;
            this.label2.Text = "Unidade....:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 20);
            this.label1.TabIndex = 206;
            this.label1.Text = "Cadastrar um novo imóvel";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Inscricao
            // 
            this.Inscricao.Location = new System.Drawing.Point(97, 110);
            this.Inscricao.Mask = "9.99.9999.99999.99.99.999";
            this.Inscricao.Name = "Inscricao";
            this.Inscricao.Size = new System.Drawing.Size(161, 20);
            this.Inscricao.TabIndex = 203;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(3, 113);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(88, 13);
            this.label48.TabIndex = 205;
            this.label48.Text = "Nº de inscrição..:";
            // 
            // TipoList
            // 
            this.TipoList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TipoList.FormattingEnabled = true;
            this.TipoList.Location = new System.Drawing.Point(97, 45);
            this.TipoList.Name = "TipoList";
            this.TipoList.Size = new System.Drawing.Size(262, 21);
            this.TipoList.TabIndex = 202;
            this.TipoList.SelectedIndexChanged += new System.EventHandler(this.TipoList_SelectedIndexChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.Black;
            this.label46.Location = new System.Drawing.Point(3, 48);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(88, 13);
            this.label46.TabIndex = 204;
            this.label46.Text = "Tipo de imóvel...:";
            // 
            // Imovel_Novo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(388, 142);
            this.Controls.Add(this.tBar);
            this.Controls.Add(this.SubUnidadeList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UnidadeList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Inscricao);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.TipoList);
            this.Controls.Add(this.label46);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Imovel_Novo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imovel_Novo";
            this.tBar.ResumeLayout(false);
            this.tBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tBar;
        private System.Windows.Forms.ToolStripButton OkButton;
        private System.Windows.Forms.ToolStripButton CancButton;
        private System.Windows.Forms.ComboBox SubUnidadeList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox UnidadeList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.MaskedTextBox Inscricao;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox TipoList;
        private System.Windows.Forms.Label label46;
    }
}