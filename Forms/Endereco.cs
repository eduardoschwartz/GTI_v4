﻿using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static GTI_v4.Classes.GtiTypes;

namespace GTI_v4.Forms {
    public partial class Endereco : Form {
        private readonly IEnderecoRepository _enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());

        bool _camposObrigatorios;
        bool _telefone;
        string _connection = GtiCore.Connection_Name();

        public Models.Endereco EndRetorno { get; set; }

        public Endereco(Models.Endereco reg, bool EnderecoLocal, bool EditarBairro, bool CamposObrigatorios, bool Telefone) {
            InitializeComponent();
            Carrega_Endereco(reg);
            PaisList.Enabled = !EnderecoLocal;
            UFList.Enabled = !EnderecoLocal;
            CidadeList.Enabled = !EnderecoLocal;
            BairroList.Enabled = EditarBairro;
            PaisButton_Refresh.Enabled = !EnderecoLocal;
            BairroButton_Refresh.Enabled = !EnderecoLocal;
            _camposObrigatorios = CamposObrigatorios;
            _telefone = Telefone;
            TelefoneText.Enabled = _telefone;
            TemFoneCheck.Enabled = _telefone;
            WhatsAppCheck.Enabled = _telefone;
        }

        private void Carrega_Endereco(Models.Endereco reg) {
            Carrega_Pais();
            Carrega_UF();
            if (reg.Id_pais > 0)
                PaisList.SelectedValue = reg.Id_pais;
            if (!string.IsNullOrWhiteSpace(reg.Sigla_uf)) {
                UFList.SelectedValue = reg.Sigla_uf;
                UFList_SelectedIndexChanged(null, null);
            }
            if (reg.Id_cidade > 0) {
                CidadeList.SelectedValue = Convert.ToInt32(reg.Id_cidade);
                CidadeList_SelectedIndexChanged(null, null);
            }
            if (reg.Id_bairro > 0)
                BairroList.SelectedValue = reg.Id_bairro;
            if (reg.Id_logradouro > 0) {
                LogradouroText.Text = _enderecoRepository.Retorna_Logradouro(reg.Id_logradouro);
            } else
                LogradouroText.Text = reg.Nome_logradouro;
            LogradouroText.Tag = reg.Id_logradouro;
            ComplementoText.Text = reg.Complemento;
            EmailText.Text = reg.Email;
            NumeroList.Text = reg.Numero_imovel > 0 ? reg.Numero_imovel.ToString() : "";
            if (reg.Cep > 0)
                CepMask.Text = reg.Cep.ToString();
            else
                CarregaCep();

            TelefoneText.Text = reg.Telefone ?? "";
            if (reg.TemFone == null)
                TemFoneCheck.CheckState = CheckState.Unchecked;
            else {
                if (reg.TemFone == true)
                    TemFoneCheck.CheckState = CheckState.Checked;
            }
            if (reg.WhatsApp == null)
                WhatsAppCheck.CheckState = CheckState.Unchecked;
            else {
                if (reg.WhatsApp == true)
                    WhatsAppCheck.CheckState = CheckState.Checked;
            }
            BairroList.Focus();
        }

        private void Carrega_Pais() {
            PaisList.DataSource = _enderecoRepository.Lista_Pais();
            PaisList.DisplayMember = "nome_pais";
            PaisList.ValueMember = "id_pais";
        }

        private void Carrega_UF() {
            UFList.DataSource = _enderecoRepository.Lista_UF();
            UFList.DisplayMember = "descuf";
            UFList.ValueMember = "siglauf";
        }

        private void UFList_SelectedIndexChanged(object sender, EventArgs e) {
            if (UFList.SelectedIndex > -1) {
                List<Cidade> lista = _enderecoRepository.Lista_Cidade(UFList.SelectedValue.ToString());
                List<CustomListBoxItem> myItems = new List<CustomListBoxItem>();
                foreach (Cidade item in lista) {
                    myItems.Add(new CustomListBoxItem(item.Desccidade, item.Codcidade));
                }
                CidadeList.DisplayMember = "_name";
                CidadeList.ValueMember = "_value";
                CidadeList.DataSource = myItems;
                if (UFList.SelectedIndex > 0 && UFList.SelectedValue.ToString() == "SP") {
                    CidadeList.SelectedValue = 413;
                }
            }
        }

        private void CidadeList_SelectedIndexChanged(object sender, EventArgs e) {
            Carrega_Bairro();
            BairroList.SelectedIndex = -1;
            if (Convert.ToInt32(CidadeList.SelectedValue) == 413)
                CepMask.ReadOnly = true;
            else
                CepMask.ReadOnly = false;
        }

        private void Carrega_Bairro() {
            if (CidadeList.SelectedIndex > -1) {
                List<Models.Bairro> lista = _enderecoRepository.Lista_Bairro(UFList.SelectedValue.ToString(), Convert.ToInt32(CidadeList.SelectedValue));
                List<CustomListBoxItem> myItems = new List<CustomListBoxItem>();
                foreach (Models.Bairro item in lista) {
                    myItems.Add(new CustomListBoxItem(item.Descbairro, item.Codbairro));
                }
                BairroList.DisplayMember = "_name";
                BairroList.ValueMember = "_value";
                BairroList.DataSource = myItems;
            }
        }

        private void CarregaCep() {
            if (Convert.ToInt32(LogradouroText.Tag.ToString()) == 0)
                LogradouroText.Tag = "0";

            if (UFList.SelectedValue.ToString() == "SP" && Convert.ToInt32(CidadeList.SelectedValue) == 413) {
                int nCep = _enderecoRepository.RetornaCep(Convert.ToInt32(LogradouroText.Tag.ToString()), NumeroList.Text == "" ? (short)0 : Convert.ToInt16(NumeroList.Text));
                CepMask.Text = nCep.ToString("00000-000");
            }
        }

        private void PaisButton_Refresh_Click(object sender, EventArgs e) {
            Pais frmPais = new Pais();
            frmPais.ShowDialog();
            Carrega_Pais();
        }

        private void BairroButton_Refresh_Click(object sender, EventArgs e) {
            Bairro frmBairro = new Bairro();
            frmBairro.ShowDialog();
            Carrega_Bairro();
        }

        private void NumeroList_TextChanged(object sender, EventArgs e) {
            CepMask.Text = "";
            if (System.Text.RegularExpressions.Regex.IsMatch(NumeroList.Text, "[^0-9]")) {
                MessageBox.Show("Digite apenas números.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NumeroList.Text = NumeroList.Text.Remove(NumeroList.Text.Length - 1);
                if (NumeroList.Text.Length > 0) {
                    NumeroList.SelectionStart = NumeroList.Text.Length;
                }
                NumeroList.SelectionLength = 0;
            } else {
                if (string.IsNullOrEmpty(LogradouroText.Tag.ToString())) LogradouroText.Tag = "0";
            }
            CarregaCep();
        }

        private void ReturnButton_Click(object sender, EventArgs e) {
            if (_camposObrigatorios) {
                if (Convert.ToInt32(CidadeList.SelectedValue) == 413) {
                    if (LogradouroText.Tag.ToString() == "") LogradouroText.Tag = "0";
                    if (Convert.ToInt32(LogradouroText.Tag.ToString()) == 0) {
                        MessageBox.Show("Selecione um logradouro válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                } else {
                    if (string.IsNullOrWhiteSpace(LogradouroText.Text)) {
                        MessageBox.Show("Digite o nome do logradouro!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (CepMask.Text.Trim() != "-") {
                    if (!CepMask.MaskFull) {
                        MessageBox.Show("Cep inválido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (!string.IsNullOrWhiteSpace(EmailText.Text) & !GtiCore.Valida_Email(EmailText.Text)) {
                    MessageBox.Show("Endereço de email inválido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (TelefoneText.Text.Trim() == "" && WhatsAppCheck.Checked) {
                    MessageBox.Show("Digite o nº do WhatsApp, ou desmarque esta opção.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            EndRetorno = new Models.Endereco();
            if (PaisList.SelectedIndex > -1) {
                EndRetorno.Id_pais = Convert.ToInt32(PaisList.SelectedValue);
                EndRetorno.Nome_pais = PaisList.Text;
            } else {
                EndRetorno.Id_pais = 0;
                EndRetorno.Nome_pais = "";
            }
            if (UFList.SelectedIndex > -1) {
                EndRetorno.Sigla_uf = UFList.SelectedValue.ToString();
                EndRetorno.Nome_uf = UFList.Text;
            } else {
                EndRetorno.Sigla_uf = "";
                EndRetorno.Nome_uf = "";
            }
            if (CidadeList.SelectedIndex > -1) {
                EndRetorno.Id_cidade = Convert.ToInt32(CidadeList.SelectedValue);
                EndRetorno.Nome_cidade = CidadeList.Text;
            } else {
                EndRetorno.Id_cidade = 0;
                EndRetorno.Nome_cidade = "";
            }
            if (BairroList.SelectedIndex > -1) {
                EndRetorno.Id_bairro = Convert.ToInt32(BairroList.SelectedValue);
                EndRetorno.Nome_bairro = BairroList.Text;
            } else {
                EndRetorno.Id_bairro = 0;
                EndRetorno.Nome_bairro = "";
            }

            if (string.IsNullOrEmpty(LogradouroText.Tag.ToString())) LogradouroText.Tag = "0";
            EndRetorno.Id_logradouro = Convert.ToInt32(LogradouroText.Tag.ToString());
            EndRetorno.Nome_logradouro = LogradouroText.Text;
            if (string.IsNullOrEmpty(NumeroList.Text.ToString())) NumeroList.Text = "0";
            EndRetorno.Numero_imovel = Convert.ToInt32(NumeroList.Text);
            EndRetorno.Complemento = ComplementoText.Text;
            EndRetorno.Email = EmailText.Text;
            string _cep = GtiCore.ExtractNumber(CepMask.Text);
            EndRetorno.Cep = _cep == "" ? 0 : Convert.ToInt32(_cep);
            EndRetorno.Cancelar = false;
            EndRetorno.Telefone = TelefoneText.Text;
            EndRetorno.TemFone = TemFoneCheck.Checked;
            EndRetorno.WhatsApp = WhatsAppCheck.Checked;
            Close();
            return;

        }

        private void LogradouroText_KeyDown(object sender, KeyEventArgs e) {
            if (Convert.ToInt32(CidadeList.SelectedValue) != 413) {
                CepMask.Text = "";
                return;
            }
            if (!String.IsNullOrEmpty(LogradouroText.Text) && e.KeyCode == Keys.Enter) {
                List<Logradouro> Listalogradouro = _enderecoRepository.Lista_Logradouro(LogradouroText.Text);

                LogradouroList.DataSource = Listalogradouro;
                LogradouroList.DisplayMember = "endereco";
                LogradouroList.ValueMember = "codlogradouro";
                if (LogradouroList.Items.Count > 0) {
                    LogradouroList.Visible = true;
                    LogradouroList.BringToFront();
                    LogradouroList.DroppedDown = true;
                    LogradouroList.Focus();
                } else {
                    MessageBox.Show("Logradouro não localizado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogradouroText.Focus();
                }
            } else
                LogradouroText.Tag = "";
        }

        private void LogradouroList_KeyDown(object sender, KeyEventArgs e) {
            if (LogradouroList.SelectedValue == null) return;
            if (e.KeyCode == Keys.Escape) {
                LogradouroList.Visible = false;
                LogradouroText.Focus();
                return;
            }
            if (e.KeyCode == Keys.Enter) {
                LogradouroText.Text = LogradouroList.Text;
                LogradouroText.Tag = LogradouroList.SelectedValue.ToString();
                LogradouroList.Visible = false;
                CarregaCep();
                NumeroList.Focus();
            }
        }

        private void LogradouroList_Leave(object sender, EventArgs e) {
            if (LogradouroList.SelectedValue == null) {
                LogradouroText.Text = "";
                LogradouroText.Tag = "";
            } else {
                LogradouroText.Text = LogradouroList.Text;
                LogradouroText.Tag = LogradouroList.SelectedValue.ToString();
            }
            LogradouroList.Visible = false;
            NumeroList.Focus();
            CarregaCep();
        }

        private void BairroList_SelectedIndexChanged(object sender, EventArgs e) {
            LogradouroText.Focus();
        }

        private void NumeroList_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                ComplementoText.Focus();
        }

        private void ComplementoText_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                CepMask.Focus();
        }

        private void CepMask_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                if (EmailText.Enabled)
                    EmailText.Focus();
                else
                    ReturnButton.Focus();
        }

        private void LogradouroList_TextChanged(object sender, EventArgs e) {
            CepMask.Text = "";
        }

        private void CancelarButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Cancelar a edição do endereço?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                EndRetorno = new Models.Endereco {
                    Cancelar = true
                };
                this.Close();
                return;
            }
        }

        private void EmailText_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                if (TelefoneText.Enabled)
                    TelefoneText.Focus();
                else
                    ReturnButton.Focus();
        }

        private void TemFoneCheck_CheckedChanged(object sender, EventArgs e) {
            if (TemFoneCheck.Checked)
                TelefoneText.Text = "";
        }
    }
}
