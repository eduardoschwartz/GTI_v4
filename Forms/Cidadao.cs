using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Cidadao : Form {
        readonly string _connection = GtiCore.Connection_Name();
        private readonly ISistemaRepository _sistemaRepository = new SistemaRepository(GtiCore.Connection_Name());
        private readonly ICidadaoRepository _cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());

        bool bAddNew;
        int _CodCidadao = 0;
        

        public int CodCidadao {
            get { return (_CodCidadao); }
            set {
                _CodCidadao = value;
                bAddNew = false;
                if (_CodCidadao > 0)
                    LoadReg(_CodCidadao);
            }
        }

        public Cidadao() {
            InitializeComponent();
            Carrega_Profissao();
            Clear_Reg();
            ControlBehaviour(true);
        }

        private void Carrega_Profissao() {
            List<Profissao> lista = _cidadaoRepository.Lista_Profissao();
            ProfissaoList.DataSource = lista;
            ProfissaoList.DisplayMember = "nome";
            ProfissaoList.ValueMember = "codigo";
        }

        private void ControlBehaviour(bool bStart) {
            Color color_disable = this.BackColor;
            FindCodigoButton.Enabled = bStart;
            AddButton.Enabled = bStart;
            EditButton.Enabled = bStart;
            DelButton.Enabled = bStart;
            ExitButton.Enabled = bStart;
            FindButton.Enabled = bStart;
            GravarButton.Enabled = !bStart;
            CancelarButton.Enabled = !bStart;
            Profissao_EditButton.Enabled = !bStart;
            Profissao_DelButton.Enabled = !bStart;
            AddEnderecoRButton.Enabled = !bStart;
            AddEnderecoCButton.Enabled = !bStart;
            DelEnderecoRButton.Enabled = !bStart;
            DelEnderecoCButton.Enabled = !bStart;

            TextBox box;
            MaskedTextBox mbox;

            foreach (Control c in this.Controls) {
                if (c is TextBox) {
                    box = c as TextBox;
                    box.ReadOnly = bStart;
                } else if (c is MaskedTextBox) {
                    mbox = c as MaskedTextBox;
                    mbox.ReadOnly = bStart;
                }
            }

            foreach (Control c in this.EndRPanel.Controls) {
                if (c is TextBox) {
                    box = c as TextBox;
                    box.ReadOnly = true;
                    box.BackColor = color_disable;
                } else if (c is MaskedTextBox) {
                    mbox = c as MaskedTextBox;
                    mbox.ReadOnly = true;
                }
            }

            foreach (Control c in this.EndCPanel.Controls) {
                if (c is TextBox) {
                    box = c as TextBox;
                    box.ReadOnly = true;
                    box.BackColor = color_disable;
                } else if (c is MaskedTextBox) {
                    mbox = c as MaskedTextBox;
                    mbox.ReadOnly = true;
                }
            }

            PessoaText.ReadOnly = true;
            PessoaText.Visible = bStart;
            PessoaList.Enabled = !bStart;
            PessoaList.Visible = !bStart;
            ProfissaoText.Visible = bStart;
            ProfissaoList.Enabled = !bStart;
            ProfissaoList.Visible = !bStart;
            JuridicaCheck.Enabled = !bStart;
            EtiquetaRCheck.Enabled = !bStart;
            EtiquetaCButton.Enabled = !bStart;
        }

        private void Clear_Reg() {
            CodigoText.Text = "000000";
            TipoCidadaoText.Text = "";
            NomeText.Text = "";
            PessoaList.SelectedIndex = 0;
            PessoaText.Text = "";
            PessoaText.Tag = "";
            CPFMask.Text = "";
            CNPJMask.Text = "";
            RGText.Text = "";
            OrgaoText.Text = "";
            DataNasctoMask.Text = "";
            ProfissaoList.SelectedIndex = -1;
            ProfissaoText.Text = "";
            ProfissaoText.Tag = "";
            EmailRText.Text = "";
            FoneRText.Text = "";
            LogradouroRText.Text = "";
            NumeroRText.Text = "";
            BairroRText.Text = "";
            ComplementoRText.Text = "";
            CidadeRText.Text = "";
            UFRText.Text = "";
            CepRText.Text = "";
            PaisRText.Text = "";
            EtiquetaRCheck.Checked = false;
            TemFoneRCheck.Checked = false;
            WhatsAppRCheck.Checked = false;
            LogradouroCText.Text = "";
            NumeroCText.Text = "";
            BairroCText.Text = "";
            ComplementoCText.Text = "";
            CidadeCText.Text = "";
            UFCText.Text = "";
            CepCText.Text = "";
            PaisCText.Text = "";
            EtiquetaCButton.Checked = false;
            TemFoneCCheck.Checked = false;
            WhatsAppCCheck.Checked = false;
        }

        private void LoadReg(Int32 nCodigo) {
            CidadaoStruct reg = _cidadaoRepository.LoadReg(nCodigo);
            CodigoText.Text = reg.Codigo.ToString("000000");
            NomeText.Text = reg.Nome;
            RGText.Text = reg.Rg ?? "";
            OrgaoText.Text = reg.Orgao ?? "";
            if (reg.DataNascto != null)
                DataNasctoMask.Text = Convert.ToDateTime(reg.DataNascto).ToString("dd/MM/yyyy");
            if (reg.CodigoProfissao == null || reg.CodigoProfissao == 0) {
                ProfissaoList.SelectedIndex = -1;
                ProfissaoText.Text = "";
                ProfissaoText.Tag = "";
            } else {
                ProfissaoList.SelectedValue = reg.CodigoProfissao;
                ProfissaoText.Text = ProfissaoList.Text;
                ProfissaoText.Tag = reg.CodigoProfissao;
            }
            JuridicaCheck.Checked = reg.Juridica;

            if (reg.Tipodoc == 1) {
                PessoaList.SelectedIndex = 0;
                CPFMask.Text = reg.Cpf;
            } else if (reg.Tipodoc == 2) {
                PessoaList.SelectedIndex = 1;
                CNPJMask.Text = reg.Cnpj;
            } else {
                PessoaList.SelectedIndex = 0;
                CPFMask.Text = "";
            }
            PessoaText.Text = PessoaList.Text;
            PessoaText.Tag = reg.Tipodoc;

            LogradouroRText.Text = reg.EnderecoR;
            LogradouroRText.Tag = reg.CodigoLogradouroR.ToString();
            if (!string.IsNullOrWhiteSpace(LogradouroRText.Text)) {
                NumeroRText.Text = reg.NumeroR.ToString();
                ComplementoRText.Text = reg.ComplementoR;
                BairroRText.Text = reg.NomeBairroR;
                BairroRText.Tag = reg.CodigoBairroR.ToString();
                CidadeRText.Text = reg.NomeCidadeR;
                CidadeRText.Tag = reg.CodigoCidadeR.ToString();
                UFRText.Text = reg.UfR;
                PaisRText.Text = reg.PaisR;
                PaisRText.Tag = reg.CodigoPaisR.ToString();
                CepRText.Text = reg.CepR.ToString();
                EmailRText.Text = reg.EmailR;
                EtiquetaRCheck.Checked = reg.EtiquetaR == "S";
                FoneRText.Text = reg.TelefoneR ?? "";
                TemFoneRCheck.Checked = reg.Temfone == null ? false : (bool)reg.Temfone;
                WhatsAppRCheck.Checked = reg.Whatsapp == null ? false : (bool)reg.Whatsapp;
            } else {
                NumeroRText.Text = "";
                ComplementoRText.Text = "";
                BairroRText.Text = "";
                BairroRText.Tag = "";
                CidadeRText.Text = "";
                CidadeRText.Tag = "";
                UFRText.Text = "";
                PaisRText.Text = "";
                PaisRText.Tag = "";
                CepRText.Text = "";
                EmailRText.Text = "";
                EtiquetaRCheck.Checked = false;
                FoneRText.Text = "";
                TemFoneRCheck.Checked = false;
                WhatsAppRCheck.Checked = false;
            }

            LogradouroCText.Text = reg.EnderecoC;
            LogradouroCText.Tag = reg.CodigoLogradouroC.ToString();
            if (!string.IsNullOrWhiteSpace(LogradouroCText.Text)) {
                NumeroCText.Text = reg.NumeroC.ToString();
                ComplementoCText.Text = reg.ComplementoC;
                BairroCText.Text = reg.NomeBairroC;
                BairroCText.Tag = reg.CodigoBairroC.ToString();
                CidadeCText.Text = reg.NomeCidadeC;
                CidadeCText.Tag = reg.CodigoCidadeC.ToString();
                UFCText.Text = reg.UfC;
                PaisCText.Text = reg.PaisC;
                PaisCText.Tag = reg.CodigoPaisC.ToString();
                CepCText.Text = reg.CepC.ToString();
                EmailCText.Text = reg.EmailC;
                EtiquetaCButton.Checked = reg.EtiquetaC == "S";
                FoneCText.Text = reg.TelefoneC ?? "";
                TemFoneCCheck.Checked = reg.Temfone2 == null ? false : (bool)reg.Temfone2;
                WhatsAppCCheck.Checked = reg.Whatsapp2 == null ? false : (bool)reg.Whatsapp2;
            } else {
                LogradouroCText.Text = "";
                LogradouroCText.Tag = "";
                NumeroCText.Text = "";
                ComplementoCText.Text = "";
                BairroCText.Text = "";
                BairroCText.Tag = "";
                CidadeCText.Text = "";
                CidadeCText.Tag = "";
                UFCText.Text = "";
                PaisCText.Text = "";
                PaisCText.Tag = "";
                CepCText.Text = "";
                EmailCText.Text = "";
                FoneCText.Text = "";
                EtiquetaCButton.Checked = false;
                TemFoneCCheck.Checked = false;
                WhatsAppCCheck.Checked = false;
            }

            if (!EtiquetaRCheck.Checked && !EtiquetaCButton.Checked) EtiquetaRCheck.Checked = true;
        }

        private void PessoaList_SelectedIndexChanged(object sender, EventArgs e) {
            if (PessoaList.SelectedIndex == 0) {
                NomeDocText.Text = "CPF....:";
                CPFMask.BringToFront();
                CNPJMask.SendToBack();
            } else {
                NomeDocText.Text = "CNPJ..:";
                CPFMask.SendToBack();
                CNPJMask.BringToFront();
            }
            PessoaText.Text = PessoaList.Text;
            PessoaText.Tag = (PessoaList.SelectedIndex + 1).ToString();
        }

        private void FindCodigoButton_Click(object sender, EventArgs e) {
            InputBox z = new InputBox();
            String sCod = z.Show("", "Informação", "Digite o código do cidadão.", 6, GtiCore.ETweakMode.IntegerPositive);
            if (!string.IsNullOrEmpty(sCod)) {
                GtiCore.Ocupado(this);
                if (_cidadaoRepository.ExisteCidadao(Convert.ToInt32(sCod))) {
                    Clear_Reg();
                    LoadReg(Convert.ToInt32(sCod));
                } else
                    MessageBox.Show("Cidadão não cadastrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GtiCore.Liberado(this);
            }

        }
    }
}
