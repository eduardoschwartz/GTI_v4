using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Repository;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Main : Form {

        private readonly IProtocoloRepository _protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        private readonly ITributarioRepository _tributarioRepository = new TributarioRepository(GtiCore.Connection_Name());
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams {
            get {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle |= CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public Main() {
            InitializeComponent();
            this.DoubleBuffered = true;
            DateTimePicker t = new DateTimePicker {
                AutoSize = false,
                Width = 100,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Name = "sbDataBase",
            };
            TopBarToolStrip.Renderer = new MySR();
            t.CloseUp += new System.EventHandler(SbDataBase_CloseUp);

            BarStatus.Items.Insert(17, new ToolStripControlHost(t));
            MaquinaToolStripStatus.Text = Environment.MachineName;
            DataBaseToolStripStatus.Text = Properties.Settings.Default.DataBaseReal;
            
        }

        private void SbDataBase_CloseUp(object sender, EventArgs e) {
            MessageBox.Show(ReturnDataBaseValue().ToString());
        }

        public DateTime ReturnDataBaseValue() {
            DateTime dValue = DateTime.Today;

            DateTimePicker dbox;
            foreach (Control c in BarStatus.Controls) {
                if (c is DateTimePicker) {
                    dbox = c as DateTimePicker;
                    dValue = dbox.Value.Date;
                }
            }
            return dValue;
        }

        private void Main_Load(object sender, EventArgs e) {
            IsMdiContainer = true;
            CorFundo();
            Refresh();
            
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            ServidorToolStripStatus.Text = Properties.Settings.Default.ServerName;
            
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            VersaoToolStripStatus.Text = $"{version.Major}" + "." + $"{version.Minor}" + "." + $"{version.Build}";
            Text += VersaoToolStripStatus.Text;

            LockForm(true);
            Forms.Login login = new Forms.Login();
            login.ShowDialog();

        }

        public void LockForm(bool bLocked) {
            //foreach (ToolStripItem ts in TopBarToolStrip.Items) {
            //    ts.Enabled = !bLocked;
            //}

            //List<ToolStripMenuItem> mItems = GtiCore.GetItems(this.MenuBarStrip);
            //foreach (var item in mItems) {
            //    item.Enabled = !bLocked;
            //}
            Dv1Option.Enabled = !bLocked;
            Dv2Option.Enabled = !bLocked;
            DVText.ReadOnly = bLocked;
            DVLabel.Enabled = !bLocked;
            lblDV2.Enabled = !bLocked;
            DV3label.Enabled = !bLocked;
        }

        private void CorFundo() {
            MdiClient ctlMDI;

            foreach (Control ctl in this.Controls) {
                try {
                    ctlMDI = (MdiClient)ctl;
                    ctlMDI.BackColor = Color.LightSteelBlue;

                } catch {
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            Application.Exit();
        }

        private void SairButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Deseja fechar todas as janelas e retornar a tela de login?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Form[] charr = this.MdiChildren;
                foreach (Form chform in charr) {
                    chform.Close();
                }
                LockForm(true);
                
                Forms.Login login = new Forms.Login();
                login.ShowDialog();
            }
        }

        private void ConfigMenu_Click(object sender, EventArgs e) {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Config);
            if (formToShow != null) {
                formToShow.Show();
            } else {
                Forms.Config f1 = new Forms.Config {
                    Tag = "Menu",
                    MdiParent = this
                };
                f1.Show();
                f1.BringToFront();
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e) {
            ConfigMenu_Click(sender, e);
        }

        private void baseRealToolStripMenuItem_Click(object sender, EventArgs e) {
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.Close();

            DataBaseToolStripStatus.Text = Properties.Settings.Default.DataBaseReal;
            GtiCore.UpdateUserBinary();
            this.Refresh();
        }

        private void baseDeTestesToolStripMenuItem_Click(object sender, EventArgs e) {
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.Close();

            DataBaseToolStripStatus.Text = Properties.Settings.Default.DataBaseTeste;
            GtiCore.UpdateUserBinary();
            this.Refresh();
        }

        private void MinimizarTodasToolStripMenuItem_Click(object sender, EventArgs e) {
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.WindowState = FormWindowState.Minimized;
        }

        private void RestaurarTodasToolStripMenuItem_Click(object sender, EventArgs e) {
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.WindowState = FormWindowState.Normal;
        }

        private void FecharTodasToolStripMenuItem_Click(object sender, EventArgs e) {
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.Close();
        }

        private void EmCascataToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void LadoALadoToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void Dv1Option_CheckedChanged(object sender, EventArgs e) {
            DVLabel.Text = RetornaDV().ToString();
            DVText.Focus();
        }

        private void Dv2Option_CheckedChanged(object sender, EventArgs e) {
            DVLabel.Text = RetornaDV().ToString();
            DVText.Focus();
        }

        private short RetornaDV() {
            short ret ;
            if (DVText.Text == "") DVText.Text = "0";
            int Numero = Convert.ToInt32(DVText.Text);
            if (Dv1Option.Checked) {
                ret = Convert.ToInt16(_protocoloRepository.DvProcesso(Numero));
            } else {
                ret = Convert.ToInt16(_tributarioRepository.DvDocumento(Numero));
            }

            return ret;
        }

        private void DVText_Enter(object sender, EventArgs e) {
            DVText.SelectionStart = 0;
            DVText.SelectionLength = DVText.Text.Length;
        }

        private void CadastroCidadaoMenu_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroCidadao);
            if (bAllow) {
                var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Cidadao);
                if (formToShow != null) {
                    formToShow.Show();
                } else {
                    Cidadao f1 = new Cidadao {
                        Tag = "Menu",
                        MdiParent = this
                    };
                    f1.Show();
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void CidadaoButton_Click(object sender, EventArgs e) {
            CadastroCidadaoMenu_Click(sender, e);
        }

        private void AtribuicaoDeAcessoMenu_Click(object sender, EventArgs e) {
            AtribuicaoAcessoMenu_Click(sender, e);
        }

        private void AtribuicaoAcessoMenu_Click(object sender, EventArgs e) {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.SecurityUserForm);
            if (formToShow != null) {
                formToShow.Show();
            } else {
                Forms.SecurityUserForm f1 = new Forms.SecurityUserForm {
                    Tag = "Menu",
                    MdiParent = this
                };
                f1.Show();
                f1.BringToFront();
            }
        }

        private void DVText_TextChanged(object sender, EventArgs e) {
            if (DVText.Text == "")
                DVLabel.Text = "X";
            else
                DVLabel.Text = RetornaDV().ToString();
        }

        private void DVText_KeyPress(object sender, KeyPressEventArgs e) {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void CadastroBairroMenu_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro);
            if (bAllow) {
                var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Bairro);
                if (formToShow != null) {
                    formToShow.Show();
                } else {
                    Forms.Bairro f1 = new Forms.Bairro {
                        Tag = "Menu",
                        MdiParent = this
                    };
                    f1.Show();
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CadastroPaisMenu_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroPais);
            if (bAllow) {
                var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Pais);
                if (formToShow != null) {
                    formToShow.Show();
                } else {
                    Pais f1 = new Pais {
                        Tag = "Menu",
                        MdiParent = this
                    };
                    f1.Show();
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void CadastroProfissaoMenu_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProfissao);
            if (bAllow) {
                var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Profissao);
                if (formToShow != null) {
                    formToShow.Show();
                } else {
                    Profissao f1 = new Profissao {
                        Tag = "Menu",
                        MdiParent = this
                    };
                    f1.Show();
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ControleProcessoMenu_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProcesso);
            if (bAllow) {

                var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Processo);
                if (formToShow != null) {
                    formToShow.Show();
                } else {
                    Processo f1 = new Processo {
                        Tag = "Menu",
                        MdiParent = this
                    };
                    f1.Show();
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ProtocoloButton_Click(object sender, EventArgs e) {
            ControleProcessoMenu_Click(sender, e);
        }

        private void DocumentacaoProcessoMenu_Click(object sender, EventArgs e) {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Processo_Documento);
            if (formToShow != null) {
                formToShow.Show();
            } else {
                Processo_Documento f1 = new Processo_Documento {
                    Tag = "Menu",
                    MdiParent = this
                };
                f1.Show();
            }
        }

        private void DespachoMenu_Click(object sender, EventArgs e) {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Processo_Despacho);
            if (formToShow != null) {
                formToShow.Show();
            } else {
                Processo_Despacho f1 = new Processo_Despacho {
                    Tag = "Menu",
                    MdiParent = this
                };
                f1.Show();
            }
        }

        private void AssuntoProcessoMenu_Click(object sender, EventArgs e) {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Processo_Assunto);
            if (formToShow != null) {
                formToShow.Show();
            } else {
                Processo_Assunto f1 = new Processo_Assunto {
                    Tag = "Menu",
                    MdiParent = this
                };
                f1.Show();
            }
        }

        private void LocalTramiteMenu_Click(object sender, EventArgs e) {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Processo_Local);
            if (formToShow != null) {
                formToShow.Show();
            } else {
                Processo_Local f1 = new Processo_Local {
                    Tag = "Menu",
                    MdiParent = this
                };
                f1.Show();
            }
        }



    }
}
