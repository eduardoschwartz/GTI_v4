using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static GTI_v4.Classes.GtiTypes;

namespace GTI_v4.Forms {
    public partial class Bairro : Form {
        readonly string _connection = GtiCore.Connection_Name();
        private readonly IEnderecoRepository _enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());

        public Bairro() {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e) {
            InputBox iBox = new InputBox();

            bool bAllowLocal = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Local);
            bool bAllowFora = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Fora);

            if (!bAllowLocal && !bAllowFora) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UFCombo.SelectedValue.ToString() == "SP" && Convert.ToInt32(CidadeCombo.SelectedValue) == 413 && !bAllowLocal) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String sCod = iBox.Show("", "Informação", "Digite o nome do bairro.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Models.Bairro reg = new Models.Bairro {
                    Siglauf = UFCombo.SelectedValue.ToString(),
                    Codcidade = Convert.ToInt16(CidadeCombo.SelectedValue.ToString()),
                    Descbairro = sCod.ToUpper()
                };
                Exception ex = _enderecoRepository.Incluir_bairro(reg);
                if (ex != null) {
                    MessageBox.Show("Bairro já cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    CidadeCombo_SelectedIndexChanged(sender, e);
            }
        }

        private void EditButton_Click(object sender, EventArgs e) {
            if (BairroListBox.SelectedItem == null) return;
            bool bAllowLocal = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Local);
            bool bAllowFora = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Fora);

            if (!bAllowLocal && !bAllowFora) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UFCombo.SelectedValue.ToString() == "SP" && Convert.ToInt32(CidadeCombo.SelectedValue) == 413 && !bAllowLocal) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InputBox iBox = new InputBox();
            String sCod = iBox.Show(BairroListBox.Text, "Informação", "Digite o nome do bairro.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Models.Bairro reg = new Models.Bairro {
                    Siglauf = UFCombo.SelectedValue.ToString(),
                    Codcidade = Convert.ToInt16(CidadeCombo.SelectedValue.ToString()),
                    Codbairro = Convert.ToInt16(BairroListBox.SelectedValue.ToString()),
                    Descbairro = sCod.ToUpper()
                };
                Exception ex = _enderecoRepository.Alterar_Bairro(reg);
                if (ex != null) {
                    MessageBox.Show("Bairro já cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    CidadeCombo_SelectedIndexChanged(sender, e);
            }
        }

        private void DelButton_Click(object sender, EventArgs e) {
            if (BairroListBox.SelectedItem == null) return;

            bool bAllowLocal = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Local);
            bool bAllowFora = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Fora);

            if (!bAllowLocal && !bAllowFora) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UFCombo.SelectedValue.ToString() == "SP" && Convert.ToInt32(CidadeCombo.SelectedValue) == 413 && !bAllowLocal) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Excluir este bairro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Models.Bairro reg = new Models.Bairro {
                    Siglauf = UFCombo.SelectedValue.ToString(),
                    Codcidade = Convert.ToInt16(CidadeCombo.SelectedValue.ToString()),
                    Codbairro = Convert.ToInt16(BairroListBox.SelectedValue.ToString())
                };
                Exception ex = _enderecoRepository.Excluir_Bairro(reg);
                if (ex != null) {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    CidadeCombo_SelectedIndexChanged(sender, e);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void UFCombo_SelectedIndexChanged(object sender, EventArgs e) {
            BairroListBox.DataSource = null;
            if (UFCombo.SelectedIndex == -1) return;
            GtiCore.Ocupado(this);
            Uf Estado = (Uf)UFCombo.SelectedItem;
            String sUF = Estado.Siglauf;

            List<Cidade> lista = _enderecoRepository.Lista_Cidade(sUF);

            List<CustomListBoxItem> myItems = new List<CustomListBoxItem>();
            foreach (Cidade item in lista) {
                myItems.Add(new CustomListBoxItem(item.Desccidade, item.Codcidade));
            }
            CidadeCombo.DisplayMember = "_name";
            CidadeCombo.ValueMember = "_value";
            CidadeCombo.DataSource = myItems;

            if (UFCombo.SelectedIndex > 0 && UFCombo.SelectedValue.ToString() == "SP") {
                CidadeCombo.SelectedValue = 413;
            }

            GtiCore.Liberado(this);
        }

        private void CidadeCombo_SelectedIndexChanged(object sender, EventArgs e) {
            BairroListBox.DataSource = null;
            if (CidadeCombo.SelectedIndex == -1) return;
            GtiCore.Ocupado(this);
            String sUF = UFCombo.SelectedValue.ToString();
            CustomListBoxItem city = (CustomListBoxItem)CidadeCombo.SelectedItem;
            Int32 nCodCidade = city._value;

            List<Models.Bairro> lista = _enderecoRepository.Lista_Bairro(sUF, nCodCidade);
            BairroListBox.DataSource = lista;
            BairroListBox.DisplayMember = "descbairro";
            BairroListBox.ValueMember = "codbairro";

            GtiCore.Liberado(this);
        }

        private void Bairro_Load(object sender, EventArgs e) {
            UFCombo.DataSource = _enderecoRepository.Lista_UF();
            UFCombo.DisplayMember = "siglauf";
            UFCombo.ValueMember = "siglauf";
            UFCombo.SelectedValue = "SP";
        }

        private void BairroListBox_DoubleClick(object sender, EventArgs e) {
            if (BairroListBox.SelectedItem == null) return;
            EditButton.PerformClick();
        }
    }
}
