using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Pais : Form {
        readonly string _connection = GtiCore.Connection_Name();
        private readonly IEnderecoRepository _enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());

        public Pais() {
            InitializeComponent();
            Carrega_Lista();
        }

        private void btAdd_Click(object sender, EventArgs e) {
            InputBox iBox = new InputBox();

            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroPais_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String sCod = iBox.Show("", "Informação", "Digite o nome do país.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Models.Pais reg = new Models.Pais {
                    Nome_pais = sCod.ToUpper()
                };
                Exception ex = _enderecoRepository.Incluir_Pais(reg);
                if (ex != null) {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    Carrega_Lista();
            }
        }

        private void btEdit_Click(object sender, EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroPais_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InputBox iBox = new InputBox();
            String sCod = iBox.Show(lstMain.Text, "Informação", "Digite o nome do país.", 50);
            if (!string.IsNullOrEmpty(sCod)) {
                Models.Pais reg = new Models.Pais {
                    Id_pais = Convert.ToInt16(lstMain.SelectedValue.ToString()),
                    Nome_pais = sCod.ToUpper()
                };
                Exception ex = _enderecoRepository.Alterar_Pais(reg);
                if (ex != null) {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    Carrega_Lista();
            }
        }

        private void btDel_Click(object sender, EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroPais_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Excluir este país?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Models.Pais reg = new Models.Pais {
                    Id_pais = Convert.ToInt16(lstMain.SelectedValue.ToString())
                };
                Exception ex = _enderecoRepository.Excluir_Pais(reg);
                if (ex != null) {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    Carrega_Lista();
            }
        }

        private void btExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void Carrega_Lista() {
            GtiCore.Ocupado(this);
            List<Models.Pais> lista = _enderecoRepository.Lista_Pais();
            lstMain.DataSource = lista;
            lstMain.DisplayMember = "Nome_pais";
            lstMain.ValueMember = "Id_pais";
            GtiCore.Liberado(this);
        }

        private void lstMain_DoubleClick(object sender, EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            btEdit.PerformClick();
        }
    }
}
