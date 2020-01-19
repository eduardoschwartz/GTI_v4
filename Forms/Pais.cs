using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Pais : Form {
        private readonly IEnderecoRepository _enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());
        private readonly ICidadaoRepository _cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());

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
                int index =lstMain.FindString(sCod);
                if (index == -1) {
                    Models.Pais reg = new Models.Pais {
                        Nome_pais = sCod.ToUpper()
                    };
                    Exception ex = _enderecoRepository.Incluir_Pais(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", "País já cadastrado.", ex);
                        eBox.ShowDialog();

                    } else
                        Carrega_Lista();
                } else
                    MessageBox.Show("País já cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEdit_Click(object sender, EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            Models.Pais _item = (Models.Pais)lstMain.SelectedItem;
            if (_item.Nome_pais.Substring(0, 1) == "(") return;
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroPais_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InputBox iBox = new InputBox();
            String sCod = iBox.Show(lstMain.Text, "Informação", "Digite o nome do país.", 50);
            if (!string.IsNullOrEmpty(sCod)) {

                bool _find = false;
                foreach (Models.Pais item in lstMain.Items) {
                    if (item.Nome_pais == sCod.ToUpper() && item.Id_pais != Convert.ToInt32(lstMain.SelectedValue))
                        _find = true;
                }
                if (!_find) {

                    Models.Pais reg = new Models.Pais {
                        Id_pais = Convert.ToInt16(lstMain.SelectedValue.ToString()),
                        Nome_pais = sCod.ToUpper()
                    };
                    Exception ex = _enderecoRepository.Alterar_Pais(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", "País já cadastrado.", ex);
                        eBox.ShowDialog();
                    } else
                        Carrega_Lista();
                }else
                    MessageBox.Show("País já cadastrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btDel_Click(object sender, EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            Models.Pais _item = (Models.Pais)lstMain.SelectedItem;
            if (_item.Nome_pais.Substring(0, 1) == "(") return;
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroPais_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool _existePais = _cidadaoRepository.Existe_Pais(Convert.ToInt32( lstMain.SelectedValue));
            if (_existePais) {
                MessageBox.Show("Existem contribuintes com este país cadastrado.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Excluir este país?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Models.Pais reg = new Models.Pais {
                    Id_pais = Convert.ToInt16(lstMain.SelectedValue.ToString())
                };
                Exception ex = _enderecoRepository.Excluir_Pais(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
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
