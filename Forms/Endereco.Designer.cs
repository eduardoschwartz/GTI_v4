namespace GTI_v4.Forms {
    partial class Endereco {
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
            this.components = new System.ComponentModel.Container();
            this.TemFoneCheck = new System.Windows.Forms.CheckBox();
            this.WhatsAppCheck = new System.Windows.Forms.CheckBox();
            this.TelefoneText = new System.Windows.Forms.TextBox();
            this.EmailText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CancelarButton = new System.Windows.Forms.Button();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.BairroButton_Refresh = new System.Windows.Forms.Button();
            this.PaisButton_Refresh = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.LogradouroList = new System.Windows.Forms.ComboBox();
            this.CepMask = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ComplementoText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.NumeroList = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LogradouroText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PaisList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BairroList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CidadeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UFList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TemFoneCheck
            // 
            this.TemFoneCheck.AutoSize = true;
            this.TemFoneCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TemFoneCheck.Image = global::GTI_v4.Properties.Resources.PhoneCancel;
            this.TemFoneCheck.Location = new System.Drawing.Point(308, 146);
            this.TemFoneCheck.Name = "TemFoneCheck";
            this.TemFoneCheck.Size = new System.Drawing.Size(28, 16);
            this.TemFoneCheck.TabIndex = 188;
            this.toolTip1.SetToolTip(this.TemFoneCheck, "Não possui telefone");
            this.TemFoneCheck.UseVisualStyleBackColor = true;
            this.TemFoneCheck.CheckedChanged += new System.EventHandler(this.TemFoneCheck_CheckedChanged);
            // 
            // WhatsAppCheck
            // 
            this.WhatsAppCheck.AutoSize = true;
            this.WhatsAppCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WhatsAppCheck.Image = global::GTI_v4.Properties.Resources.whatsapp;
            this.WhatsAppCheck.Location = new System.Drawing.Point(264, 145);
            this.WhatsAppCheck.Name = "WhatsAppCheck";
            this.WhatsAppCheck.Size = new System.Drawing.Size(28, 17);
            this.WhatsAppCheck.TabIndex = 187;
            this.toolTip1.SetToolTip(this.WhatsAppCheck, "Possui WhatsApp");
            this.WhatsAppCheck.UseVisualStyleBackColor = true;
            // 
            // TelefoneText
            // 
            this.TelefoneText.Location = new System.Drawing.Point(75, 143);
            this.TelefoneText.MaxLength = 50;
            this.TelefoneText.Name = "TelefoneText";
            this.TelefoneText.Size = new System.Drawing.Size(171, 20);
            this.TelefoneText.TabIndex = 186;
            // 
            // EmailText
            // 
            this.EmailText.Location = new System.Drawing.Point(75, 119);
            this.EmailText.MaxLength = 50;
            this.EmailText.Name = "EmailText";
            this.EmailText.Size = new System.Drawing.Size(376, 20);
            this.EmailText.TabIndex = 177;
            this.EmailText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmailText_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 184;
            this.label9.Text = "E-mail..........:";
            // 
            // CancelarButton
            // 
            this.CancelarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelarButton.Image = global::GTI_v4.Properties.Resources.cancel2;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(419, 143);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.CancelarButton.Size = new System.Drawing.Size(32, 25);
            this.CancelarButton.TabIndex = 179;
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.CancelarButton, "Cancelar operação");
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // ReturnButton
            // 
            this.ReturnButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReturnButton.Image = global::GTI_v4.Properties.Resources.OK;
            this.ReturnButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReturnButton.Location = new System.Drawing.Point(386, 143);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ReturnButton.Size = new System.Drawing.Size(32, 25);
            this.ReturnButton.TabIndex = 178;
            this.ReturnButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.ReturnButton, "Retornar o endereço");
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // BairroButton_Refresh
            // 
            this.BairroButton_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BairroButton_Refresh.Image = global::GTI_v4.Properties.Resources.Alterar;
            this.BairroButton_Refresh.Location = new System.Drawing.Point(426, 40);
            this.BairroButton_Refresh.Name = "BairroButton_Refresh";
            this.BairroButton_Refresh.Size = new System.Drawing.Size(25, 25);
            this.BairroButton_Refresh.TabIndex = 169;
            this.BairroButton_Refresh.TabStop = false;
            this.BairroButton_Refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.BairroButton_Refresh, "Atualizar lista de bairros");
            this.BairroButton_Refresh.UseVisualStyleBackColor = true;
            this.BairroButton_Refresh.Click += new System.EventHandler(this.BairroButton_Refresh_Click);
            // 
            // PaisButton_Refresh
            // 
            this.PaisButton_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PaisButton_Refresh.Image = global::GTI_v4.Properties.Resources.Alterar;
            this.PaisButton_Refresh.Location = new System.Drawing.Point(209, 15);
            this.PaisButton_Refresh.Name = "PaisButton_Refresh";
            this.PaisButton_Refresh.Size = new System.Drawing.Size(25, 25);
            this.PaisButton_Refresh.TabIndex = 175;
            this.PaisButton_Refresh.TabStop = false;
            this.PaisButton_Refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.PaisButton_Refresh, "Atualizar lista de países");
            this.PaisButton_Refresh.UseVisualStyleBackColor = true;
            this.PaisButton_Refresh.Click += new System.EventHandler(this.PaisButton_Refresh_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 185;
            this.label10.Text = "Telefone.....:";
            // 
            // LogradouroList
            // 
            this.LogradouroList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LogradouroList.FormattingEnabled = true;
            this.LogradouroList.Location = new System.Drawing.Point(75, 69);
            this.LogradouroList.Name = "LogradouroList";
            this.LogradouroList.Size = new System.Drawing.Size(288, 21);
            this.LogradouroList.TabIndex = 171;
            this.LogradouroList.TabStop = false;
            this.LogradouroList.Visible = false;
            this.LogradouroList.TextChanged += new System.EventHandler(this.LogradouroList_TextChanged);
            this.LogradouroList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogradouroList_KeyDown);
            this.LogradouroList.Leave += new System.EventHandler(this.LogradouroList_Leave);
            // 
            // CepMask
            // 
            this.CepMask.Location = new System.Drawing.Point(382, 95);
            this.CepMask.Mask = "99999-999";
            this.CepMask.Name = "CepMask";
            this.CepMask.Size = new System.Drawing.Size(68, 20);
            this.CepMask.TabIndex = 176;
            this.CepMask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CepMask_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(342, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 183;
            this.label8.Text = "CEP..:";
            // 
            // ComplementoText
            // 
            this.ComplementoText.Location = new System.Drawing.Point(75, 95);
            this.ComplementoText.MaxLength = 50;
            this.ComplementoText.Name = "ComplementoText";
            this.ComplementoText.Size = new System.Drawing.Size(261, 20);
            this.ComplementoText.TabIndex = 174;
            this.ComplementoText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComplementoText_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 182;
            this.label7.Text = "Complemen.:";
            // 
            // NumeroList
            // 
            this.NumeroList.Location = new System.Drawing.Point(397, 69);
            this.NumeroList.MaxLength = 4;
            this.NumeroList.Name = "NumeroList";
            this.NumeroList.Size = new System.Drawing.Size(54, 20);
            this.NumeroList.TabIndex = 172;
            this.NumeroList.TextChanged += new System.EventHandler(this.NumeroList_TextChanged);
            this.NumeroList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NumeroList_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(369, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 181;
            this.label6.Text = "Nº..:";
            // 
            // LogradouroText
            // 
            this.LogradouroText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.LogradouroText.Location = new System.Drawing.Point(75, 69);
            this.LogradouroText.MaxLength = 50;
            this.LogradouroText.Name = "LogradouroText";
            this.LogradouroText.Size = new System.Drawing.Size(288, 20);
            this.LogradouroText.TabIndex = 170;
            this.LogradouroText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogradouroText_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 180;
            this.label5.Text = "Logradouro.:";
            // 
            // PaisList
            // 
            this.PaisList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaisList.FormattingEnabled = true;
            this.PaisList.Location = new System.Drawing.Point(52, 17);
            this.PaisList.Name = "PaisList";
            this.PaisList.Size = new System.Drawing.Size(157, 21);
            this.PaisList.TabIndex = 162;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 173;
            this.label4.Text = "País.....:";
            // 
            // BairroList
            // 
            this.BairroList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BairroList.FormattingEnabled = true;
            this.BairroList.Location = new System.Drawing.Point(264, 42);
            this.BairroList.Name = "BairroList";
            this.BairroList.Size = new System.Drawing.Size(162, 21);
            this.BairroList.TabIndex = 167;
            this.BairroList.SelectedIndexChanged += new System.EventHandler(this.BairroList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 168;
            this.label3.Text = "Bairro...:";
            // 
            // CidadeList
            // 
            this.CidadeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CidadeList.FormattingEnabled = true;
            this.CidadeList.Location = new System.Drawing.Point(52, 42);
            this.CidadeList.Name = "CidadeList";
            this.CidadeList.Size = new System.Drawing.Size(157, 21);
            this.CidadeList.TabIndex = 165;
            this.CidadeList.SelectedIndexChanged += new System.EventHandler(this.CidadeList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 166;
            this.label2.Text = "Cidade.:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 163;
            this.label1.Text = "Estado.:";
            // 
            // UFList
            // 
            this.UFList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UFList.FormattingEnabled = true;
            this.UFList.Location = new System.Drawing.Point(287, 18);
            this.UFList.Name = "UFList";
            this.UFList.Size = new System.Drawing.Size(163, 21);
            this.UFList.TabIndex = 164;
            this.UFList.SelectedIndexChanged += new System.EventHandler(this.UFList_SelectedIndexChanged);
            // 
            // Endereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(457, 175);
            this.ControlBox = false;
            this.Controls.Add(this.TemFoneCheck);
            this.Controls.Add(this.WhatsAppCheck);
            this.Controls.Add(this.TelefoneText);
            this.Controls.Add(this.EmailText);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.LogradouroList);
            this.Controls.Add(this.CepMask);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ComplementoText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.NumeroList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LogradouroText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BairroButton_Refresh);
            this.Controls.Add(this.PaisButton_Refresh);
            this.Controls.Add(this.PaisList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BairroList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CidadeList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UFList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Endereco";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de endereço";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox TemFoneCheck;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox WhatsAppCheck;
        private System.Windows.Forms.TextBox TelefoneText;
        private System.Windows.Forms.TextBox EmailText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button CancelarButton;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button BairroButton_Refresh;
        private System.Windows.Forms.Button PaisButton_Refresh;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox LogradouroList;
        private System.Windows.Forms.MaskedTextBox CepMask;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ComplementoText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox NumeroList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox LogradouroText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox PaisList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox BairroList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CidadeList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox UFList;
    }
}