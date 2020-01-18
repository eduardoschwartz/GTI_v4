using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Cidadao_Historico : Form {
        readonly string _connection = GtiCore.Connection_Name();
        private readonly ICidadaoRepository _cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());
        private readonly ISistemaRepository _sistemaRepository = new SistemaRepository(GtiCore.Connection_Name());

        string _tipo = "";
        int _codigo;
        bool bDirty, bExec;

        public Cidadao_Historico(int Codigo, string Tipo) {
            InitializeComponent();
            _tipo = Tipo;
            _codigo = Codigo;
            GravarButton.Enabled = false;
            if (_tipo == "H")
                Text = "Histórico do contribuinte";
            else {
                Text = "Observação do contribuinte";
                HistoricoText.ReadOnly = false;
            }
            bDirty = false;
            Carrega_Lista();
        }

        private void MainListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (MainListView.Items.Count > 0 && MainListView.SelectedItems.Count > 0)
                HistoricoText.Text = MainListView.SelectedItems[0].SubItems[2].Text.ToString();
        }

        private void GravarButton_Click(object sender, EventArgs e) {
            if (bDirty) {
                bDirty = false;

                ObsCidadao reg = new ObsCidadao();
                reg.Codigo = _codigo;
                reg.Userid = _sistemaRepository.Retorna_User_LoginId(Properties.Settings.Default.LastUser);
                reg.timestamp = DateTime.Now;
                reg.Obs = HistoricoText.Text;

                Exception ex = _cidadaoRepository.Incluir_observacao_cidadao(reg);
                if (ex != null) {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    Carrega_Lista();
            }
        }

        private void HistoricoText_KeyPress(object sender, KeyPressEventArgs e) {
            bDirty = true;
        }

        private void HistoricoText_TextChanged(object sender, EventArgs e) {
            if (_tipo == "O" && bExec && bDirty)
                GravarButton.Enabled = true;
        }

        private void Carrega_Lista() {
            MainListView.Items.Clear();
            bDirty = false;
            bExec = false;
            if (_tipo == "H") {
                List<Historico_CidadaoStruct> Lista = _cidadaoRepository.Lista_Historico(_codigo);
                foreach (Historico_CidadaoStruct item in Lista) {
                    ListViewItem lvItem = new ListViewItem(Convert.ToDateTime(item.Data).ToString("dd/MM/yyyy"));
                    lvItem.SubItems.Add(item.Nome_Usuario);
                    lvItem.SubItems.Add(item.Obs);
                    MainListView.Items.Add(lvItem);
                }
            } else {
                List<Observacao_CidadaoStruct> Lista = _cidadaoRepository.Lista_Observacao(_codigo);
                foreach (Observacao_CidadaoStruct item in Lista) {
                    ListViewItem lvItem = new ListViewItem(Convert.ToDateTime(item.Data_Hora).ToString("dd/MM/yyyy hh:mm"));
                    lvItem.SubItems.Add(item.Nome_Usuario);
                    lvItem.SubItems.Add(item.Obs);
                    MainListView.Items.Add(lvItem);
                }
            }
            bExec = true;
            if (MainListView.Items.Count > 0)
                MainListView.Items[0].Selected = true;

        }



    }
}
