using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Profissao : Form {
        readonly string _connection = GtiCore.Connection_Name();
        private readonly ICidadaoRepository _cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());

        public Profissao() {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, System.EventArgs e) {
            InputBox iBox = new InputBox();
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProfissao_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sCod = iBox.Show("", "Informação", "Digite o nome da profissão.", 100);
            if (!string.IsNullOrEmpty(sCod)) {
                Models.Profissao reg = new Models.Profissao {
                    Nome = sCod.ToUpper()
                };
                Exception ex = _cidadaoRepository.Incluir_Profissao(reg);
                if (ex != null) {
                    MessageBox.Show("Profissão já cadastrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    Carrega_Lista();
            }
        }

        private void btEdit_Click(object sender, System.EventArgs e) {

        }

        private void btDel_Click(object sender, System.EventArgs e) {

        }

        private void btExit_Click(object sender, System.EventArgs e) {
            Close();
        }

        private void lstMain_SelectedIndexChanged(object sender, System.EventArgs e) {

        }

        private void lstMain_DoubleClick(object sender, System.EventArgs e) {

        }

        private void Carrega_Lista() {
            lstMain.DataSource = null;
            GtiCore.Ocupado(this);
            List<Models.Profissao> lista = _cidadaoRepository.Lista_Profissao();
            lstMain.DataSource = lista;
            lstMain.DisplayMember = "Nome";
            lstMain.ValueMember = "Codigo";
            GtiCore.Liberado(this);
        }
    }
}
