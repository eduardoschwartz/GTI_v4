using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static GTI_v4.Classes.GtiTypes;

namespace GTI_v4.Forms {
    public partial class Processo_Despacho : Form {
        IProtocoloRepository protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        bool bCheck;

        public Processo_Despacho() {
            InitializeComponent();
            bCheck = false;
            Carrega_Lista();
        }

        private void Carrega_Lista() {
            GtiCore.Ocupado(this);
            List<Despacho> lista = protocoloRepository.Lista_Despacho();
            MainList.Sorted = false;
            MainList.Items.Clear();
            foreach (Despacho item in lista) {
                MainList.Items.Add(new GtiTypes.CustomListBoxItem(item.Descricao.ToString(), item.Codigo));
                MainList.SetItemChecked(MainList.Items.Count - 1, Convert.ToBoolean(item.Ativo));
            }
            MainList.Sorted = true;
            GtiCore.Liberado(this);
        }

        private void AddButton_Click(object sender, EventArgs e) {
            InputBox iBox = new InputBox();
            String sCod = iBox.Show("", "Informação", "Digite o nome do despacho.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Despacho reg = new Despacho {
                    Ativo = true,
                    Descricao = sCod.ToUpper()
                };
                Exception ex = protocoloRepository.Incluir_Despacho(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Despacho já cadastrado.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista();
            }
        }

        private void EditButton_Click(object sender, EventArgs e) {
            if (MainList.SelectedItem == null) return;
            InputBox iBox = new InputBox();
            String sCod = iBox.Show(MainList.Text, "Informação", "Digite o nome do despacho.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Despacho reg = new Despacho();
                CustomListBoxItem selectedData = (CustomListBoxItem)MainList.SelectedItem;
                reg.Codigo = Convert.ToInt16(selectedData._value);
                reg.Descricao = sCod.ToUpper();
                reg.Ativo = MainList.GetItemChecked(MainList.SelectedIndex);
                Exception ex = protocoloRepository.Alterar_Despacho(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Despacho já cadastrado.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista();
            }
        }

        private void DelButton_Click(object sender, EventArgs e) {
            if (MainList.SelectedItem == null) return;
            if (MessageBox.Show("Excluir este despacho?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Despacho reg = new Despacho();
                GtiTypes.CustomListBoxItem selectedData = (GtiTypes.CustomListBoxItem)MainList.SelectedItem;
                reg.Codigo = Convert.ToInt16(selectedData._value);
                Exception ex = protocoloRepository.Excluir_Despacho(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Não foi possível excluir este despacho, consulte o detalhe para mais informações.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista();
            }
        }

        private void MainList_DoubleClick(object sender, EventArgs e) {
            if (MainList.SelectedItem == null) return;
            EditButton.PerformClick();
        }

        private void AtivarButton_Click(object sender, EventArgs e) {
            if (MainList.SelectedItem == null) return;
            bCheck = true;
            MainList.SetItemChecked(MainList.SelectedIndex, !MainList.GetItemChecked(MainList.SelectedIndex));

            Despacho reg = new Despacho();
            GtiTypes.CustomListBoxItem selectedData = (GtiTypes.CustomListBoxItem)MainList.SelectedItem;
            reg.Codigo = Convert.ToInt16(selectedData._value);
            reg.Descricao = selectedData._name;
            reg.Ativo = MainList.GetItemChecked(MainList.SelectedIndex);
            Exception ex = protocoloRepository.Alterar_Despacho(reg);
            if (ex != null) {
                ErrorBox eBox = new ErrorBox("Atenção", "Erro desconhecido.", ex);
                eBox.ShowDialog();

            }
        }

        private void MainList_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (bCheck) {
                bCheck = false;
                return;
            }
            if (MainList.SelectedItem == null) return;
            e.NewValue = e.CurrentValue;
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
