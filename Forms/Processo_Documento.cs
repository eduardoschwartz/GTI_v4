using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Processo_Documento : Form {
        IProtocoloRepository protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        public Processo_Documento() {
            InitializeComponent();
            Carrega_Lista();
        }

        private void SairButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void Carrega_Lista() {
            GtiCore.Ocupado(this);
            List<Documento> lista = protocoloRepository.Lista_Documento();
            MainList.DataSource = lista;
            MainList.DisplayMember = "nome";
            MainList.ValueMember = "codigo";
            GtiCore.Liberado(this);
        }

        private void AddButton_Click(object sender, EventArgs e) {
            InputBox iBox = new InputBox();
            String sCod = iBox.Show("", "Informação", "Digite o nome do Documento.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Documento reg = new Documento {
                    Nome = sCod.ToUpper()
                };
                Exception ex = protocoloRepository.Incluir_Documento(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Documento já cadastrado.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista();
            }
        }

        private void EditButton_Click(object sender, EventArgs e) {
            if (MainList.SelectedItem == null) return;
            InputBox iBox = new InputBox();
            String sCod = iBox.Show(MainList.Text, "Informação", "Digite o nome do Documento.", 50);
            if (!string.IsNullOrEmpty(sCod)) {
                Documento reg = new Documento();
                Documento dRow = (Documento)MainList.SelectedItem;
                reg.Codigo = dRow.Codigo;
                reg.Nome = sCod.ToUpper();
                Exception ex = protocoloRepository.Alterar_Documento(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Documento já cadastrado.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista();
            }
        }

        private void ExcluirButton_Click(object sender, EventArgs e) {
            if (MainList.SelectedItem == null) return;
            if (MessageBox.Show("Excluir este Documento?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Documento reg = new Documento();
                Documento dRow = (Documento)MainList.SelectedItem;
                reg.Codigo = dRow.Codigo;
                Exception ex = protocoloRepository.Excluir_Documento(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Não foi possível excluir este Documento, consulte o detalhe para mais informações.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista();
            }
        }

        private void MainList_DoubleClick(object sender, EventArgs e) {
            if (MainList.SelectedItem == null) return;
            EditButton.PerformClick();
        }

    }
}
