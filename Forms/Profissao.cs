using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Profissao : Form {
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
                int index = lstMain.FindString(sCod);
                if (index == -1) {

                    Models.Profissao reg = new Models.Profissao {
                        Nome = sCod.ToUpper()
                    };
                    Exception ex = _cidadaoRepository.Incluir_profissao(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", "Profissão já cadastrada.", ex);
                        eBox.ShowDialog();
                    } else
                        Carrega_Lista();
                }else
                    MessageBox.Show("Profissão já cadastrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEdit_Click(object sender, System.EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            Models.Profissao _item =  (Models.Profissao)lstMain.SelectedItem;
            if (_item.Nome.Substring(0,1)=="(") return;

            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProfissao_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InputBox iBox = new InputBox();
            String sCod = iBox.Show(lstMain.Text, "Informação", "Digite o nome da profissão.", 100);
            if (!string.IsNullOrEmpty(sCod)) {

                bool _find = false;
                foreach (Models.Pais item in lstMain.Items) {
                    if (item.Nome_pais == sCod.ToUpper() && item.Id_pais != Convert.ToInt32(lstMain.SelectedValue))
                        _find = true;
                }
                if (!_find) {
                    Models.Profissao reg = new Models.Profissao {
                        Codigo = Convert.ToInt16(lstMain.SelectedValue.ToString()),
                        Nome = sCod.ToUpper()
                    };
                    Exception ex = _cidadaoRepository.Alterar_Profissao(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", "Profissão já cadastrada.", ex);
                        eBox.ShowDialog();
                    } else
                        Carrega_Lista();
                }else
                    MessageBox.Show("Profissão já cadastrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btDel_Click(object sender, System.EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            Models.Profissao _item = (Models.Profissao)lstMain.SelectedItem;
            if (_item.Nome.Substring(0, 1) == "(") return;
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProfissao_Alterar);
            if (!bAllow) {
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool _existe = _cidadaoRepository.Existe_Profissao(Convert.ToInt32(lstMain.SelectedValue));
            if (_existe) {
                MessageBox.Show("Existem contribuintes com esta profissão cadastrada.", "Exclusão não permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Excluir esta profissão?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Models.Profissao reg = new Models.Profissao {
                    Codigo = Convert.ToInt16(lstMain.SelectedValue.ToString())
                };
                Exception ex = _cidadaoRepository.Excluir_Profissao(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista();
            }
        }

        private void btExit_Click(object sender, System.EventArgs e) {
            Close();
        }

        private void lstMain_DoubleClick(object sender, System.EventArgs e) {
            if (lstMain.SelectedItem == null) return;
            btEdit.PerformClick();
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

        private void Profissao_Load(object sender, EventArgs e) {
            Carrega_Lista();
        }
    }
}
