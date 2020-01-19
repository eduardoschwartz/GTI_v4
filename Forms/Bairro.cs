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
        private readonly IEnderecoRepository _enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());
        private readonly IImobiliarioRepository _imovelRepository = new ImobiliarioRepository(GtiCore.Connection_Name());
        private readonly ICidadaoRepository _cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());
        private readonly IEmpresaRepository _empresaRepository = new EmpresaRepository(GtiCore.Connection_Name());


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

            string sCod = iBox.Show("", "Informação", "Digite o nome do bairro.", 40, GtiCore.ETweakMode.AlphaNumericAllCaps);
            if (!string.IsNullOrEmpty(sCod)) {
                int index = BairroListBox.FindString(sCod);
                if (index==-1) {
                    Models.Bairro reg = new Models.Bairro {
                        Siglauf = UFCombo.SelectedValue.ToString(),
                        Codcidade = Convert.ToInt16(CidadeCombo.SelectedValue.ToString()),
                        Descbairro = sCod.ToUpper()
                    };
                    Exception ex = _enderecoRepository.Incluir_bairro(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", "Bairro já cadastrado.", ex);
                        eBox.ShowDialog();
                    } else
                        CidadeCombo_SelectedIndexChanged(sender, e);
                } else 
                    MessageBox.Show("Bairro já cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditButton_Click(object sender, EventArgs e) {
            if (BairroListBox.SelectedItem == null) return;
            Models.Bairro _item = (Models.Bairro)BairroListBox.SelectedItem;
            if (_item.Descbairro.Substring(0, 1) == "(") return;
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
            string sCod = iBox.Show(BairroListBox.Text, "Informação", "Digite o nome do bairro.", 40,GtiCore.ETweakMode.AlphaNumericAllCaps);

            if (!string.IsNullOrEmpty(sCod)) {
                bool _find = false;
                foreach (Models.Bairro item in BairroListBox.Items) {
                    if (item.Descbairro == sCod.ToUpper() && item.Codbairro!=Convert.ToInt32(BairroListBox.SelectedValue))
                        _find = true;
                }
                if (!_find) {
                    Models.Bairro reg = new Models.Bairro {
                        Siglauf = UFCombo.SelectedValue.ToString(),
                        Codcidade = Convert.ToInt16(CidadeCombo.SelectedValue.ToString()),
                        Codbairro = Convert.ToInt16(BairroListBox.SelectedValue.ToString()),
                        Descbairro = sCod.ToUpper()
                    };
                    Exception ex = _enderecoRepository.Alterar_Bairro(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", "Bairro já cadastrado.", ex);
                        eBox.ShowDialog();
                    } else
                        CidadeCombo_SelectedIndexChanged(sender, e);
                } else
                    MessageBox.Show("Bairro já cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DelButton_Click(object sender, EventArgs e) {
            if (BairroListBox.SelectedItem == null) return;
            Models.Bairro _item = (Models.Bairro)BairroListBox.SelectedItem;
            if (_item.Descbairro.Substring(0, 1) == "(") return;
            string _uf = UFCombo.SelectedValue.ToString();
            int _cidade = Convert.ToInt32(CidadeCombo.SelectedValue);
            int _bairro = Convert.ToInt32(BairroListBox.SelectedValue);

            bool bAllowLocal = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Local);
            bool bAllowFora = GtiCore.GetBinaryAccess((int)TAcesso.CadastroBairro_Alterar_Fora);

            if (!bAllowLocal && !bAllowFora) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_uf == "SP" && _cidade == 413 && !bAllowLocal) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool _existeBairro = _imovelRepository.Existe_Bairro_Localizacao(_uf, _cidade, _bairro);
            if (_existeBairro) {
                MessageBox.Show("Existem imóveis com este bairro no endereço de localização.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _existeBairro = _imovelRepository.Existe_Bairro_Entrega(_uf, _cidade, _bairro);
            if (_existeBairro) {
                MessageBox.Show("Existem imóveis com este bairro no endereço de entrega.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _existeBairro = _cidadaoRepository.Existe_Bairro(_uf, _cidade, _bairro);
            if (_existeBairro) {
                MessageBox.Show("Existem contribuintes com este bairro cadastrado.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _existeBairro = _imovelRepository.Existe_Bairro_Condominio(_uf, _cidade, _bairro);
            if (_existeBairro) {
                MessageBox.Show("Existem condomínios com este bairro cadastrado.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _existeBairro = _empresaRepository.Existe_Bairro(_uf, _cidade, _bairro);
            if (_existeBairro) {
                MessageBox.Show("Existem empresas com este bairro cadastrado.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _existeBairro = _empresaRepository.Existe_Bairro_Entrega(_uf, _cidade, _bairro);
            if (_existeBairro) {
                MessageBox.Show("Existem empresas com este bairro de entrega cadastrado.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
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
