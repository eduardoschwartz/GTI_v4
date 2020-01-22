namespace GTI_v4.Forms {
    partial class Processo_Lista {
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
            this.AssuntoList = new System.Windows.Forms.ComboBox();
            this.SetorList = new System.Windows.Forms.ComboBox();
            this.DataEntrada = new System.Windows.Forms.MaskedTextBox();
            this.NumeroProcesso = new System.Windows.Forms.TextBox();
            this.Requerente = new System.Windows.Forms.TextBox();
            this.AnoFinal = new System.Windows.Forms.TextBox();
            this.AnoInicial = new System.Windows.Forms.TextBox();
            this.ProprietarioDelButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.InternoList = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.FisicoList = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Complemento = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.ProprietarioAddButton = new System.Windows.Forms.ToolStripButton();
            this.ProprietarioToolStrip = new System.Windows.Forms.ToolStrip();
            this.MainListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tBar = new System.Windows.Forms.ToolStrip();
            this.FindButton = new System.Windows.Forms.ToolStripButton();
            this.SelectButton = new System.Windows.Forms.ToolStripButton();
            this.ClearButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Total = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ExcelButton = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.ProprietarioToolStrip.SuspendLayout();
            this.tBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // AssuntoList
            // 
            this.AssuntoList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AssuntoList.FormattingEnabled = true;
            this.AssuntoList.Location = new System.Drawing.Point(96, 84);
            this.AssuntoList.Name = "AssuntoList";
            this.AssuntoList.Size = new System.Drawing.Size(479, 21);
            this.AssuntoList.TabIndex = 210;
            // 
            // SetorList
            // 
            this.SetorList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SetorList.FormattingEnabled = true;
            this.SetorList.Location = new System.Drawing.Point(96, 58);
            this.SetorList.Name = "SetorList";
            this.SetorList.Size = new System.Drawing.Size(479, 21);
            this.SetorList.TabIndex = 209;
            // 
            // DataEntrada
            // 
            this.DataEntrada.Location = new System.Drawing.Point(499, 7);
            this.DataEntrada.Mask = "00/00/0000";
            this.DataEntrada.Name = "DataEntrada";
            this.DataEntrada.Size = new System.Drawing.Size(76, 20);
            this.DataEntrada.TabIndex = 3;
            this.DataEntrada.ValidatingType = typeof(System.DateTime);
            // 
            // NumeroProcesso
            // 
            this.NumeroProcesso.Location = new System.Drawing.Point(96, 7);
            this.NumeroProcesso.MaxLength = 15;
            this.NumeroProcesso.Name = "NumeroProcesso";
            this.NumeroProcesso.Size = new System.Drawing.Size(75, 20);
            this.NumeroProcesso.TabIndex = 0;
            this.NumeroProcesso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumeroProcesso_KeyPress);
            // 
            // Requerente
            // 
            this.Requerente.Location = new System.Drawing.Point(96, 33);
            this.Requerente.MaxLength = 6;
            this.Requerente.Name = "Requerente";
            this.Requerente.Size = new System.Drawing.Size(75, 20);
            this.Requerente.TabIndex = 208;
            this.Requerente.TabStop = false;
            this.Requerente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Requerente_KeyPress);
            // 
            // AnoFinal
            // 
            this.AnoFinal.Location = new System.Drawing.Point(350, 7);
            this.AnoFinal.MaxLength = 4;
            this.AnoFinal.Name = "AnoFinal";
            this.AnoFinal.Size = new System.Drawing.Size(45, 20);
            this.AnoFinal.TabIndex = 2;
            this.AnoFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AnoFinal_KeyPress);
            // 
            // AnoInicial
            // 
            this.AnoInicial.Location = new System.Drawing.Point(242, 7);
            this.AnoInicial.MaxLength = 4;
            this.AnoInicial.Name = "AnoInicial";
            this.AnoInicial.Size = new System.Drawing.Size(45, 20);
            this.AnoInicial.TabIndex = 1;
            this.AnoInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AnoInicial_KeyPress);
            // 
            // ProprietarioDelButton
            // 
            this.ProprietarioDelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ProprietarioDelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProprietarioDelButton.Name = "ProprietarioDelButton";
            this.ProprietarioDelButton.Size = new System.Drawing.Size(23, 22);
            this.ProprietarioDelButton.Text = "toolStripButton3";
            this.ProprietarioDelButton.ToolTipText = "Remover o proprietário";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AssuntoList);
            this.panel1.Controls.Add(this.SetorList);
            this.panel1.Controls.Add(this.DataEntrada);
            this.panel1.Controls.Add(this.NumeroProcesso);
            this.panel1.Controls.Add(this.Requerente);
            this.panel1.Controls.Add(this.AnoFinal);
            this.panel1.Controls.Add(this.AnoInicial);
            this.panel1.Controls.Add(this.InternoList);
            this.panel1.Controls.Add(this.Label9);
            this.panel1.Controls.Add(this.FisicoList);
            this.panel1.Controls.Add(this.Label8);
            this.panel1.Controls.Add(this.Label13);
            this.panel1.Controls.Add(this.Label7);
            this.panel1.Controls.Add(this.Label6);
            this.panel1.Controls.Add(this.Complemento);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Controls.Add(this.Label4);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 140);
            this.panel1.TabIndex = 210;
            // 
            // InternoList
            // 
            this.InternoList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InternoList.FormattingEnabled = true;
            this.InternoList.Items.AddRange(new object[] {
            "(Todos)",
            "Sim",
            "Não"});
            this.InternoList.Location = new System.Drawing.Point(431, 32);
            this.InternoList.Name = "InternoList";
            this.InternoList.Size = new System.Drawing.Size(84, 21);
            this.InternoList.TabIndex = 5;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(377, 35);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(49, 13);
            this.Label9.TabIndex = 45;
            this.Label9.Text = "Interno..:";
            // 
            // FisicoList
            // 
            this.FisicoList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FisicoList.FormattingEnabled = true;
            this.FisicoList.Items.AddRange(new object[] {
            "(Todos)",
            "Sim",
            "Não"});
            this.FisicoList.Location = new System.Drawing.Point(265, 32);
            this.FisicoList.Name = "FisicoList";
            this.FisicoList.Size = new System.Drawing.Size(84, 21);
            this.FisicoList.TabIndex = 4;
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(211, 36);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(48, 13);
            this.Label8.TabIndex = 44;
            this.Label8.Text = "Físico...:";
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(402, 10);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(91, 13);
            this.Label13.TabIndex = 43;
            this.Label13.Text = "Data de Entrada.:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(292, 10);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(57, 13);
            this.Label7.TabIndex = 42;
            this.Label7.Text = "Ano Final.:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(177, 10);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(62, 13);
            this.Label6.TabIndex = 41;
            this.Label6.Text = "Ano Inicial.:";
            // 
            // Complemento
            // 
            this.Complemento.Location = new System.Drawing.Point(96, 109);
            this.Complemento.MaxLength = 40;
            this.Complemento.Name = "Complemento";
            this.Complemento.Size = new System.Drawing.Size(479, 20);
            this.Complemento.TabIndex = 6;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(13, 112);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 13);
            this.Label5.TabIndex = 35;
            this.Label5.Text = "Complemento:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(13, 86);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(72, 13);
            this.Label4.TabIndex = 32;
            this.Label4.Text = "Assunto........:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(13, 60);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(72, 13);
            this.Label3.TabIndex = 31;
            this.Label3.Text = "Setor/Depto.:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(13, 35);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(72, 13);
            this.Label2.TabIndex = 29;
            this.Label2.Text = "Requerente..:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(72, 13);
            this.Label1.TabIndex = 26;
            this.Label1.Text = "Nº Processo.:";
            // 
            // ProprietarioAddButton
            // 
            this.ProprietarioAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ProprietarioAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProprietarioAddButton.Name = "ProprietarioAddButton";
            this.ProprietarioAddButton.Size = new System.Drawing.Size(23, 22);
            this.ProprietarioAddButton.Text = "toolStripButton1";
            this.ProprietarioAddButton.ToolTipText = "Adicionar um proprietário";
            // 
            // ProprietarioToolStrip
            // 
            this.ProprietarioToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.ProprietarioToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ProprietarioToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ProprietarioToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProprietarioAddButton,
            this.ProprietarioDelButton});
            this.ProprietarioToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ProprietarioToolStrip.Location = new System.Drawing.Point(422, 31);
            this.ProprietarioToolStrip.Name = "ProprietarioToolStrip";
            this.ProprietarioToolStrip.Size = new System.Drawing.Size(49, 25);
            this.ProprietarioToolStrip.TabIndex = 214;
            this.ProprietarioToolStrip.Text = "toolStrip2";
            // 
            // MainListView
            // 
            this.MainListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.MainListView.FullRowSelect = true;
            this.MainListView.HideSelection = false;
            this.MainListView.Location = new System.Drawing.Point(0, 144);
            this.MainListView.Name = "MainListView";
            this.MainListView.Size = new System.Drawing.Size(789, 375);
            this.MainListView.TabIndex = 211;
            this.MainListView.UseCompatibleStateImageBehavior = false;
            this.MainListView.View = System.Windows.Forms.View.Details;
            this.MainListView.VirtualMode = true;
            this.MainListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.MainListView_RetrieveVirtualItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ano";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Número";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Requerente";
            this.columnHeader4.Width = 220;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Assunto";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Dt.Entrada";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Dt.Cancel.";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Dt.Arquiv.";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Dt.Reativ.";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 70;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Fís.";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 40;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Int.";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 40;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Endereço";
            this.columnHeader11.Width = 200;
            // 
            // tBar
            // 
            this.tBar.AllowMerge = false;
            this.tBar.BackColor = System.Drawing.SystemColors.Control;
            this.tBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindButton,
            this.SelectButton,
            this.ClearButton,
            this.toolStripSeparator1,
            this.Total,
            this.toolStripLabel2,
            this.ExcelButton});
            this.tBar.Location = new System.Drawing.Point(0, 522);
            this.tBar.Name = "tBar";
            this.tBar.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.tBar.Size = new System.Drawing.Size(789, 25);
            this.tBar.TabIndex = 212;
            this.tBar.Text = "toolStrip1";
            // 
            // FindButton
            // 
            this.FindButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FindButton.Image = global::GTI_v4.Properties.Resources.Consultar;
            this.FindButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(23, 22);
            this.FindButton.Text = "toolStripButton1";
            this.FindButton.ToolTipText = "Pesquisar";
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectButton.Image = global::GTI_v4.Properties.Resources.rightarrow;
            this.SelectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(23, 22);
            this.SelectButton.Text = "toolStripButton2";
            this.SelectButton.ToolTipText = "Retornar";
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearButton.Image = global::GTI_v4.Properties.Resources.cancel2;
            this.ClearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(23, 22);
            this.ClearButton.Text = "toolStripButton1";
            this.ClearButton.ToolTipText = "Limpar a tela de consulta";
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Total
            // 
            this.Total.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Total.ForeColor = System.Drawing.Color.Maroon;
            this.Total.Name = "Total";
            this.Total.Size = new System.Drawing.Size(13, 22);
            this.Total.Text = "0";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel2.Text = "Total encontrado:";
            // 
            // ExcelButton
            // 
            this.ExcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExcelButton.Image = global::GTI_v4.Properties.Resources.icon_excel;
            this.ExcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExcelButton.Name = "ExcelButton";
            this.ExcelButton.Size = new System.Drawing.Size(23, 22);
            this.ExcelButton.Text = "toolStripButton1";
            this.ExcelButton.ToolTipText = "Exportar resultado para o Excel";
            this.ExcelButton.Click += new System.EventHandler(this.ExcelButton_Click);
            // 
            // Processo_Lista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 547);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ProprietarioToolStrip);
            this.Controls.Add(this.MainListView);
            this.Controls.Add(this.tBar);
            this.MaximizeBox = false;
            this.Name = "Processo_Lista";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista dos processos cadastrados";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Processo_Lista_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ProprietarioToolStrip.ResumeLayout(false);
            this.ProprietarioToolStrip.PerformLayout();
            this.tBar.ResumeLayout(false);
            this.tBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox AssuntoList;
        private System.Windows.Forms.ComboBox SetorList;
        private System.Windows.Forms.MaskedTextBox DataEntrada;
        public System.Windows.Forms.TextBox NumeroProcesso;
        private System.Windows.Forms.TextBox Requerente;
        internal System.Windows.Forms.TextBox AnoFinal;
        internal System.Windows.Forms.TextBox AnoInicial;
        private System.Windows.Forms.ToolStripButton ProprietarioDelButton;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ComboBox InternoList;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ComboBox FisicoList;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox Complemento;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ToolStripButton ProprietarioAddButton;
        private System.Windows.Forms.ToolStrip ProprietarioToolStrip;
        private System.Windows.Forms.ListView MainListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ToolStrip tBar;
        private System.Windows.Forms.ToolStripButton FindButton;
        private System.Windows.Forms.ToolStripButton SelectButton;
        private System.Windows.Forms.ToolStripButton ClearButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel Total;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton ExcelButton;
    }
}