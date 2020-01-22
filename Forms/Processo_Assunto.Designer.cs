namespace GTI_v4.Forms {
    partial class Processo_Assunto {
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
            this.Doc1button = new System.Windows.Forms.Button();
            this.CC2button = new System.Windows.Forms.Button();
            this.CC1button = new System.Windows.Forms.Button();
            this.tTp = new System.Windows.Forms.ToolTip(this.components);
            this.Doc2button = new System.Windows.Forms.Button();
            this.Filterbutton = new System.Windows.Forms.Button();
            this.CC2List = new System.Windows.Forms.ListBox();
            this.Doc2List = new System.Windows.Forms.ListBox();
            this.Doc1List = new System.Windows.Forms.ListBox();
            this.CC1List = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FilterText = new System.Windows.Forms.TextBox();
            this.SomenteOsInativosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SomenteOsAtivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExibirTodosOsAssuntosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.Ativarbutton = new System.Windows.Forms.ToolStripButton();
            this.Delbutton = new System.Windows.Forms.ToolStripButton();
            this.Editbutton = new System.Windows.Forms.ToolStripButton();
            this.Addbutton = new System.Windows.Forms.ToolStripButton();
            this.tBar = new System.Windows.Forms.ToolStrip();
            this.Exitbutton = new System.Windows.Forms.ToolStripButton();
            this.MainCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.tBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Doc1button
            // 
            this.Doc1button.Image = global::GTI_v4.Properties.Resources.rightarrow;
            this.Doc1button.Location = new System.Drawing.Point(560, 267);
            this.Doc1button.Name = "Doc1button";
            this.Doc1button.Size = new System.Drawing.Size(26, 22);
            this.Doc1button.TabIndex = 47;
            this.tTp.SetToolTip(this.Doc1button, "Influir documento");
            this.Doc1button.UseVisualStyleBackColor = true;
            this.Doc1button.Click += new System.EventHandler(this.Doc1Button_Click);
            // 
            // CC2button
            // 
            this.CC2button.Image = global::GTI_v4.Properties.Resources.leftarrow;
            this.CC2button.Location = new System.Drawing.Point(561, 103);
            this.CC2button.Name = "CC2button";
            this.CC2button.Size = new System.Drawing.Size(26, 22);
            this.CC2button.TabIndex = 44;
            this.tTp.SetToolTip(this.CC2button, "Remover local de tramitação");
            this.CC2button.UseVisualStyleBackColor = true;
            this.CC2button.Click += new System.EventHandler(this.CC2Button_Click);
            // 
            // CC1button
            // 
            this.CC1button.Image = global::GTI_v4.Properties.Resources.rightarrow;
            this.CC1button.Location = new System.Drawing.Point(562, 75);
            this.CC1button.Name = "CC1button";
            this.CC1button.Size = new System.Drawing.Size(26, 22);
            this.CC1button.TabIndex = 43;
            this.tTp.SetToolTip(this.CC1button, "Incluir local de tramitação");
            this.CC1button.UseVisualStyleBackColor = true;
            this.CC1button.Click += new System.EventHandler(this.CC1Button_Click);
            // 
            // Doc2button
            // 
            this.Doc2button.Image = global::GTI_v4.Properties.Resources.leftarrow;
            this.Doc2button.Location = new System.Drawing.Point(559, 295);
            this.Doc2button.Name = "Doc2button";
            this.Doc2button.Size = new System.Drawing.Size(26, 22);
            this.Doc2button.TabIndex = 48;
            this.tTp.SetToolTip(this.Doc2button, "Remover documento");
            this.Doc2button.UseVisualStyleBackColor = true;
            this.Doc2button.Click += new System.EventHandler(this.Doc2Button_Click);
            // 
            // Filterbutton
            // 
            this.Filterbutton.Image = global::GTI_v4.Properties.Resources.funnel;
            this.Filterbutton.Location = new System.Drawing.Point(266, 7);
            this.Filterbutton.Name = "Filterbutton";
            this.Filterbutton.Size = new System.Drawing.Size(32, 22);
            this.Filterbutton.TabIndex = 40;
            this.tTp.SetToolTip(this.Filterbutton, "Filtrar os assuntos");
            this.Filterbutton.UseVisualStyleBackColor = true;
            this.Filterbutton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // CC2List
            // 
            this.CC2List.AllowDrop = true;
            this.CC2List.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CC2List.FormattingEnabled = true;
            this.CC2List.Location = new System.Drawing.Point(591, 30);
            this.CC2List.Name = "CC2List";
            this.CC2List.Size = new System.Drawing.Size(242, 147);
            this.CC2List.TabIndex = 45;
            this.tTp.SetToolTip(this.CC2List, "(Arraste os ítens para ordenar)");
            this.CC2List.DragDrop += new System.Windows.Forms.DragEventHandler(this.CC2List_DragDrop);
            this.CC2List.DragOver += new System.Windows.Forms.DragEventHandler(this.CC2List_DragOver);
            this.CC2List.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CC2List_MouseDown);
            this.CC2List.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CC2List_MouseMove);
            // 
            // Doc2List
            // 
            this.Doc2List.FormattingEnabled = true;
            this.Doc2List.Location = new System.Drawing.Point(591, 217);
            this.Doc2List.Name = "Doc2List";
            this.Doc2List.Size = new System.Drawing.Size(242, 147);
            this.Doc2List.TabIndex = 49;
            this.Doc2List.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Doc2List_MouseMove);
            // 
            // Doc1List
            // 
            this.Doc1List.FormattingEnabled = true;
            this.Doc1List.Location = new System.Drawing.Point(314, 217);
            this.Doc1List.Name = "Doc1List";
            this.Doc1List.Size = new System.Drawing.Size(242, 147);
            this.Doc1List.TabIndex = 46;
            // 
            // CC1List
            // 
            this.CC1List.FormattingEnabled = true;
            this.CC1List.Location = new System.Drawing.Point(314, 30);
            this.CC1List.Name = "CC1List";
            this.CC1List.Size = new System.Drawing.Size(242, 147);
            this.CC1List.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Maroon;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(314, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(519, 16);
            this.label2.TabIndex = 52;
            this.label2.Text = "Documentos necessários por Assunto";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Maroon;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(314, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(519, 16);
            this.label1.TabIndex = 51;
            this.label1.Text = "Locais de tramitação padrão por Assunto";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FilterText
            // 
            this.FilterText.Location = new System.Drawing.Point(6, 7);
            this.FilterText.Name = "FilterText";
            this.FilterText.Size = new System.Drawing.Size(251, 20);
            this.FilterText.TabIndex = 39;
            this.FilterText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterText_KeyPress);
            // 
            // SomenteOsInativosToolStripMenuItem
            // 
            this.SomenteOsInativosToolStripMenuItem.Name = "SomenteOsInativosToolStripMenuItem";
            this.SomenteOsInativosToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.SomenteOsInativosToolStripMenuItem.Text = "Somente os inativos";
            this.SomenteOsInativosToolStripMenuItem.Click += new System.EventHandler(this.SomenteOsInativosToolStripMenuItem_Click);
            // 
            // SomenteOsAtivosToolStripMenuItem
            // 
            this.SomenteOsAtivosToolStripMenuItem.Name = "SomenteOsAtivosToolStripMenuItem";
            this.SomenteOsAtivosToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.SomenteOsAtivosToolStripMenuItem.Text = "Somente os ativos";
            this.SomenteOsAtivosToolStripMenuItem.Click += new System.EventHandler(this.SomenteOsAtivosToolStripMenuItem_Click);
            // 
            // ExibirTodosOsAssuntosToolStripMenuItem
            // 
            this.ExibirTodosOsAssuntosToolStripMenuItem.Name = "ExibirTodosOsAssuntosToolStripMenuItem";
            this.ExibirTodosOsAssuntosToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ExibirTodosOsAssuntosToolStripMenuItem.Text = "(Exibir todos os assuntos)";
            this.ExibirTodosOsAssuntosToolStripMenuItem.Click += new System.EventHandler(this.ExibirTodosOsAssuntosToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExibirTodosOsAssuntosToolStripMenuItem,
            this.SomenteOsAtivosToolStripMenuItem,
            this.SomenteOsInativosToolStripMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(13, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ToolTipText = "Filtrar os assuntos";
            // 
            // Ativarbutton
            // 
            this.Ativarbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Ativarbutton.Image = global::GTI_v4.Properties.Resources.more_1_;
            this.Ativarbutton.ImageTransparentColor = System.Drawing.Color.White;
            this.Ativarbutton.Name = "Ativarbutton";
            this.Ativarbutton.Size = new System.Drawing.Size(23, 22);
            this.Ativarbutton.Text = "toolStripButton1";
            this.Ativarbutton.ToolTipText = "Ativar ou desativar o assunto";
            this.Ativarbutton.Click += new System.EventHandler(this.AtivarButton_Click);
            // 
            // Delbutton
            // 
            this.Delbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Delbutton.Image = global::GTI_v4.Properties.Resources.delete;
            this.Delbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Delbutton.Name = "Delbutton";
            this.Delbutton.Size = new System.Drawing.Size(23, 22);
            this.Delbutton.Text = "toolStripButton3";
            this.Delbutton.ToolTipText = "Excluir";
            this.Delbutton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // Editbutton
            // 
            this.Editbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Editbutton.Image = global::GTI_v4.Properties.Resources.Alterar;
            this.Editbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(23, 22);
            this.Editbutton.Text = "toolStripButton2";
            this.Editbutton.ToolTipText = "Alterar";
            this.Editbutton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // Addbutton
            // 
            this.Addbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Addbutton.Image = global::GTI_v4.Properties.Resources.add_file;
            this.Addbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Addbutton.Name = "Addbutton";
            this.Addbutton.Size = new System.Drawing.Size(23, 22);
            this.Addbutton.Text = "toolStripButton1";
            this.Addbutton.ToolTipText = "Novo";
            this.Addbutton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // tBar
            // 
            this.tBar.AllowMerge = false;
            this.tBar.BackColor = System.Drawing.SystemColors.Control;
            this.tBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Addbutton,
            this.Editbutton,
            this.Delbutton,
            this.Ativarbutton,
            this.toolStripDropDownButton1,
            this.Exitbutton});
            this.tBar.Location = new System.Drawing.Point(0, 373);
            this.tBar.Name = "tBar";
            this.tBar.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.tBar.Size = new System.Drawing.Size(839, 25);
            this.tBar.TabIndex = 50;
            this.tBar.Text = "toolStrip1";
            // 
            // Exitbutton
            // 
            this.Exitbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Exitbutton.Image = global::GTI_v4.Properties.Resources.Exit;
            this.Exitbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Exitbutton.Name = "Exitbutton";
            this.Exitbutton.Size = new System.Drawing.Size(23, 22);
            this.Exitbutton.Text = "toolStripButton5";
            this.Exitbutton.ToolTipText = "Sair";
            this.Exitbutton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // MainCheckedListBox
            // 
            this.MainCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MainCheckedListBox.FormattingEnabled = true;
            this.MainCheckedListBox.Location = new System.Drawing.Point(6, 30);
            this.MainCheckedListBox.Name = "MainCheckedListBox";
            this.MainCheckedListBox.Size = new System.Drawing.Size(292, 334);
            this.MainCheckedListBox.TabIndex = 41;
            this.MainCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.MainCheckBoxList_ItemCheck);
            this.MainCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.MainCheckBoxList_SelectedIndexChanged);
            this.MainCheckedListBox.DoubleClick += new System.EventHandler(this.MainCheckBoxList_DoubleClick);
            this.MainCheckedListBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainCheckBoxList_MouseMove);
            // 
            // Processo_Assunto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 398);
            this.Controls.Add(this.Doc1button);
            this.Controls.Add(this.CC2button);
            this.Controls.Add(this.CC1button);
            this.Controls.Add(this.Doc2button);
            this.Controls.Add(this.Filterbutton);
            this.Controls.Add(this.Doc2List);
            this.Controls.Add(this.Doc1List);
            this.Controls.Add(this.CC2List);
            this.Controls.Add(this.CC1List);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilterText);
            this.Controls.Add(this.tBar);
            this.Controls.Add(this.MainCheckedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Processo_Assunto";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Documentos necessários e locais de tramitação para cada assunto";
            this.tBar.ResumeLayout(false);
            this.tBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Doc1button;
        private System.Windows.Forms.ToolTip tTp;
        private System.Windows.Forms.Button CC2button;
        private System.Windows.Forms.Button CC1button;
        private System.Windows.Forms.Button Doc2button;
        private System.Windows.Forms.Button Filterbutton;
        private System.Windows.Forms.ListBox CC2List;
        private System.Windows.Forms.ListBox Doc2List;
        private System.Windows.Forms.ListBox Doc1List;
        private System.Windows.Forms.ListBox CC1List;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilterText;
        private System.Windows.Forms.ToolStripMenuItem SomenteOsInativosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SomenteOsAtivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExibirTodosOsAssuntosToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton Ativarbutton;
        private System.Windows.Forms.ToolStripButton Delbutton;
        private System.Windows.Forms.ToolStripButton Editbutton;
        private System.Windows.Forms.ToolStripButton Addbutton;
        private System.Windows.Forms.ToolStrip tBar;
        private System.Windows.Forms.ToolStripButton Exitbutton;
        private System.Windows.Forms.CheckedListBox MainCheckedListBox;
    }
}