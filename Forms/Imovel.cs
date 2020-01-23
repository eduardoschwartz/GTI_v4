using GTI_v4.Interfaces;
using GTI_v4.Repository;
using GTI_v4.Classes;
using GTI_v4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Imovel : Form {
        readonly IProtocoloRepository protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        readonly ICidadaoRepository cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());
        readonly ISistemaRepository sistemaRepository = new SistemaRepository(GtiCore.Connection_Name());
        readonly IImobiliarioRepository imobiliarioRepository = new ImobiliarioRepository(GtiCore.Connection_Name());

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        bool bAddNew, bNovaArea;

        private void ImovelTab_DrawItem(object sender, DrawItemEventArgs e) {
            TabPage page = ImovelTab.TabPages[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, Font, paddedBounds, page.ForeColor);
        }

        public Imovel() {
            GtiCore.Ocupado(this);
            InitializeComponent();
            HistoricoBar.Renderer = new MySR();
            AreasBar.Renderer = new MySR();
            ClearFields();
            Carrega_Lista();
            bAddNew = false;
            ControlBehaviour(true);
            GtiCore.Liberado(this);
        }

        private void ClearFields() {
            IptuChart.Series.Clear();
            bNovaArea = false;
            Inscricao.Text = "";
            SomaArea.Text = "0,00";
            Matricula.Text = "";
            Condominio.Text = "";
            MT1Check.Checked = true;
            MT2Check.Checked = false;
            End1Option.Checked = true;
            End2Option.Checked = false;
            End3Option.Checked = false;
            ProprietarioListView.Items.Clear();
            TestadaListView.Items.Clear();
            AreaListView.Items.Clear();
            HistoricoListView.Items.Clear();
            Distrito.Text = "0";
            Setor.Text = "00";
            Quadra.Text = "0000";
            Lote.Text = "00000";
            Face.Text = "00";
            Unidade.Text = "00";
            SubUnidade.Text = "000";
            Logradouro.Text = "";
            Logradouro.Tag = "";
            Complemento.Text = "";
            Numero.Text = "";
            Bairro.Text = "";
            Bairro.Tag = "";
            Quadras.Text = "";
            Lotes.Text = "";
            Ativo.Text = "";
            ResideCheck.Checked = false;
            ImuneCheck.Checked = false;
            IsentoCIPCheck.Checked = false;
            ConjugadoCheck.Checked = false;
            BenfeitoriaList.SelectedIndex = -1;
            CategoriaTerrenoList.SelectedIndex = -1;
            PedologiaList.SelectedIndex = -1;
            SituacaoList.SelectedIndex = -1;
            TopografiaList.SelectedIndex = -1;
            UsoTerrenoList.SelectedIndex = -1;
            TipoConstrucaoList.SelectedIndex = -1;
            UsoConstrucaoList.SelectedIndex = -1;
            CategoriaConstrucaoList.SelectedIndex = -1;
            ImovelTab.SelectedTab = ImovelTab.TabPages[0];
            Limpa_endereco_Entrega();
            Refresh();
        }

        private void Limpa_endereco_Entrega() {
            Logradouro_EE.Text = "";
            Logradouro_EE.Tag = "";
            Numero_EE.Text = "";
            Complemento_EE.Text = "";
            CEP_EE.Text = "";
            Bairro_EE.Text = "";
            Bairro_EE.Tag = "";
            UF_EE.Text = "";
            Cidade_EE.Text = "";
            Cidade_EE.Tag = "";
        }

        private void ControlBehaviour(bool bStart) {
            Color cor_enabled = Color.White, cor_disabled = SystemColors.ButtonFace;

            NovoButton.Enabled = bStart;
            AlterarButton.Enabled = bStart;
            InativarButton.Enabled = bStart;
            SairButton.Enabled = bStart;
            ImprimirButton.Enabled = bStart;
            LocalizarButton.Enabled = bStart;
            GravarButton.Enabled = !bStart;
            CancelarButton.Enabled = !bStart;
            CodigoButton.Enabled = bStart;
            AdicionarProprietarioMenu.Enabled = !bStart;
            RemoverProprietarioMenu.Enabled = !bStart;
            ConsultarProprietarioMenu.Enabled = !bStart;
            PrincipalProprietarioMenu.Enabled = !bStart;
            AddAreaButton.Enabled = !bStart;
            EditAreaButton.Enabled = !bStart;
            DelAreaButton.Enabled = !bStart;
            AddHistoricoButton.Enabled = !bStart;
            EditHistoricoButton.Enabled = !bStart;
            DelHistoricoButton.Enabled = !bStart;
            ZoomHistoricoButton.Enabled = true;
            ResideCheck.AutoCheck = !bStart;
            ImuneCheck.AutoCheck = !bStart;
            IsentoCIPCheck.AutoCheck = !bStart;
            ConjugadoCheck.AutoCheck = !bStart;
            MT1Check.AutoCheck = !bStart;
            MT2Check.AutoCheck = !bStart;
            LocalImovelButton.Enabled = !bStart;
            Quadras.ReadOnly = bStart;
            Quadras.BackColor = bStart ? cor_disabled : cor_enabled;
            Lotes.ReadOnly = bStart;
            Lotes.BackColor = bStart ? cor_disabled : cor_enabled;
            Matricula.ReadOnly = bStart;
            Matricula.BackColor = bStart ? cor_disabled : cor_enabled;
            AreaPnl.Visible = false;
            OkAreaButton.Enabled = !bStart;
            End1Option.AutoCheck = !bStart;
            End2Option.AutoCheck = !bStart;
            End3Option.AutoCheck = !bStart;
            if (End1Option.Checked || End2Option.Checked)
                EndEntregaButton.Enabled = false;
            else
                EndEntregaButton.Enabled = !bStart;
            AddTestada.Enabled = !bStart;
            DelTestada.Enabled = !bStart;
            Testada_Face.ReadOnly = bStart;
            Testada_Face.BackColor = bStart ? cor_disabled : cor_enabled;
            Testada_Metro.ReadOnly = bStart;
            Testada_Metro.BackColor = bStart ? cor_disabled : cor_enabled;
            AreaTerreno.ReadOnly = bStart;
            AreaTerreno.BackColor = bStart ? cor_disabled : cor_enabled;
            FracaoIdeal.ReadOnly = bStart;
            FracaoIdeal.BackColor = bStart ? cor_disabled : cor_enabled;

            BenfeitoriaList.Visible = !bStart;
            CategoriaTerrenoList.Visible = !bStart;
            PedologiaList.Visible = !bStart;
            SituacaoList.Visible = !bStart;
            TopografiaList.Visible = !bStart;
            UsoTerrenoList.Visible = !bStart;
            Benfeitoria.Visible = bStart;
            Categoria.Visible = bStart;
            Pedologia.Visible = bStart;
            Situacao.Visible = bStart;
            Topografia.Visible = bStart;
            UsoTerreno.Visible = bStart;
            IPTUButton.Visible = bStart;
            IptuChart.Visible = false;
        }

        private void Matricula_KeyPress(object sender, KeyPressEventArgs e) {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void NovoButton_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroImovel_Novo);
            if (bAllow) {
                using (var form = new Imovel_Novo()) {
                    var result = form.ShowDialog(this);
                    if (result == DialogResult.OK) {
                        ClearFields();
                        Inscricao.Text = form.ReturnInscricao;
                        Distrito.Text = Inscricao.Text.Substring(0, 1);
                        Setor.Text = Inscricao.Text.Substring(2, 2);
                        Quadra.Text = Inscricao.Text.Substring(5, 4);
                        Lote.Text = Inscricao.Text.Substring(10, 5);
                        Face.Text = Inscricao.Text.Substring(16, 2);
                        Unidade.Text = Inscricao.Text.Substring(19, 2);
                        SubUnidade.Text = Inscricao.Text.Substring(22, 3);
                        Condominio.Text = form.ReturnCondominio;
                        int _condominio = form.ReturnCondominioCodigo;
                        this.Condominio.Tag = _condominio.ToString();
                        if (_condominio > 0)
                            Carrega_Dados_Condominio(_condominio);
                        bAddNew = true;
                        ControlBehaviour(false);
                    }
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Carrega_Lista() {
            CategoriaTerrenoList.DataSource = imobiliarioRepository.Lista_Categoria_Propriedade();
            CategoriaTerrenoList.DisplayMember = "Desccategprop";
            CategoriaTerrenoList.ValueMember = "Codcategprop";

            TopografiaList.DataSource = imobiliarioRepository.Lista_Topografia();
            TopografiaList.DisplayMember = "Desctopografia";
            TopografiaList.ValueMember = "Codtopografia";

            SituacaoList.DataSource = imobiliarioRepository.Lista_Situacao();
            SituacaoList.DisplayMember = "Descsituacao";
            SituacaoList.ValueMember = "Codsituacao";

            BenfeitoriaList.DataSource = imobiliarioRepository.Lista_Benfeitoria();
            BenfeitoriaList.DisplayMember = "Descbenfeitoria";
            BenfeitoriaList.ValueMember = "Codbenfeitoria";

            PedologiaList.DataSource = imobiliarioRepository.Lista_Pedologia();
            PedologiaList.DisplayMember = "Descpedologia";
            PedologiaList.ValueMember = "Codpedologia";

            UsoTerrenoList.DataSource = imobiliarioRepository.Lista_Uso_Terreno();
            UsoTerrenoList.DisplayMember = "Descusoterreno";
            UsoTerrenoList.ValueMember = "Codusoterreno";

            UsoConstrucaoList.DataSource = imobiliarioRepository.Lista_Uso_Construcao();
            UsoConstrucaoList.DisplayMember = "Descusoconstr";
            UsoConstrucaoList.ValueMember = "Codusoconstr";

            CategoriaConstrucaoList.DataSource = imobiliarioRepository.Lista_Categoria_Construcao();
            CategoriaConstrucaoList.DisplayMember = "Desccategconstr";
            CategoriaConstrucaoList.ValueMember = "Codcategconstr";

            TipoConstrucaoList.DataSource = imobiliarioRepository.Lista_Tipo_Construcao();
            TipoConstrucaoList.DisplayMember = "Desctipoconstr";
            TipoConstrucaoList.ValueMember = "Codtipoconstr";
        }

        private void AlterarButton_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroImovel_Alterar_Total);
            if (bAllow) {
                bAddNew = false;
                if (String.IsNullOrEmpty(Inscricao.Text))
                    MessageBox.Show("Nenhum imóvel carregado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    ControlBehaviour(false);
                    CodigoButton.Focus();
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GravarButton_Click(object sender, EventArgs e) {
            if (ValidateReg()) {
                SaveReg();
                ControlBehaviour(true);
            }
        }

        private void Carrega_Dados_Condominio(int _codigo_condominio) {
            CondominioStruct regImovel = imobiliarioRepository.Dados_Condominio(_codigo_condominio);
            Logradouro.Text = regImovel.Nome_Logradouro;
            Logradouro.Tag = regImovel.Codigo_Logradouro.ToString();
            Numero.Text = regImovel.Numero.ToString();
            Complemento.Text = regImovel.Complemento;
            Bairro.Text = regImovel.Nome_Bairro;
            Bairro.Tag = regImovel.Codigo_Bairro;
            Lotes.Text = regImovel.Lote_Original;
            Quadras.Text = regImovel.Quadra_Original;
            Cep.Text = regImovel.Cep;
            AreaTerreno.Text = string.Format("{0:0.00}", regImovel.Area_Terreno);
            UsoTerreno.Text = regImovel.Uso_terreno_Nome;
            Situacao.Text = regImovel.Situacao_Nome;
            Pedologia.Text = regImovel.Pedologia_Nome;
            Benfeitoria.Text = regImovel.Benfeitoria_Nome;
            Topografia.Text = regImovel.Topografia_Nome;
            Categoria.Text = regImovel.Categoria_Nome;
            BenfeitoriaList.SelectedValue = regImovel.Benfeitoria == 0 ? -1 : regImovel.Benfeitoria;
            CategoriaTerrenoList.SelectedValue = regImovel.Categoria == 0 ? -1 : regImovel.Categoria;
            PedologiaList.SelectedValue = regImovel.Pedologia == 0 ? -1 : regImovel.Pedologia;
            SituacaoList.SelectedValue = regImovel.Situacao == 0 ? -1 : regImovel.Situacao;
            TopografiaList.SelectedValue = regImovel.Topografia == 0 ? -1 : regImovel.Topografia;
            UsoTerrenoList.SelectedValue = regImovel.Uso_terreno == 0 ? -1 : regImovel.Uso_terreno;
            FracaoIdeal.Text = string.Format("{0:0.00}", regImovel.Fracao_Ideal);

            string sNome = cidadaoRepository.Retorna_Nome_Cidadao((int)regImovel.Codigo_Proprietario);
            ListViewItem lvProp = new ListViewItem {
                Group = ProprietarioListView.Groups["groupPP"]
            };
            lvProp.Text = sNome + " (Principal)";
            lvProp.Tag = regImovel.Codigo_Proprietario.ToString();
            ProprietarioListView.Items.Add(lvProp);

            List<AreaStruct> ListaArea = imobiliarioRepository.Lista_Area_Condominio(_codigo_condominio);
            short n = 1;
            decimal SomaArea = 0;
            foreach (AreaStruct reg in ListaArea) {
                ListViewItem lvItem = new ListViewItem(n.ToString("00"));
                lvItem.SubItems.Add(string.Format("{0:0.00}", (decimal)reg.Area));
                lvItem.SubItems.Add(reg.Uso_Nome);
                lvItem.SubItems.Add(reg.Tipo_Nome);
                lvItem.SubItems.Add(reg.Categoria_Nome);
                lvItem.SubItems.Add(reg.Pavimentos.ToString());
                if (reg.Data_Aprovacao != null)
                    lvItem.SubItems.Add(Convert.ToDateTime(reg.Data_Aprovacao).ToString("dd/MM/yyyy"));
                else
                    lvItem.SubItems.Add("");
                if (string.IsNullOrWhiteSpace(reg.Numero_Processo))
                    lvItem.SubItems.Add("");
                else {
                    if (reg.Numero_Processo.Contains("-"))//se já tiver DV não precisa inserir novamente
                        lvItem.SubItems.Add(reg.Numero_Processo);
                    else {
                        lvItem.SubItems.Add(protocoloRepository.Retorna_Processo_com_DV(reg.Numero_Processo));//corrige o DV
                    }
                }
                lvItem.Tag = reg.Seq.ToString();
                lvItem.SubItems[2].Tag = reg.Uso_Codigo.ToString();
                lvItem.SubItems[3].Tag = reg.Tipo_Codigo.ToString();
                lvItem.SubItems[4].Tag = reg.Categoria_Codigo.ToString();
                AreaListView.Items.Add(lvItem);
                SomaArea += reg.Area;
                n++;
            }
            if (AreaListView.Items.Count > 0)
                AreaListView.Items[0].Selected = true;
            this.SomaArea.Text = string.Format("{0:0.00}", SomaArea);

            EnderecoStruct regEntrega = imobiliarioRepository.Dados_Endereco(regImovel.Codigo, GtiCore.TipoEndereco.Local);
            if (regEntrega != null) {
                Logradouro_EE.Text = regEntrega.Endereco.ToString();
                Logradouro_EE.Tag = regEntrega.CodLogradouro.ToString();
                Numero_EE.Text = regEntrega.Numero.ToString();
                Complemento_EE.Text = regEntrega.Complemento.ToString();
                UF_EE.Text = regEntrega.UF.ToString();
                Cidade_EE.Text = regEntrega.NomeCidade.ToString();
                Cidade_EE.Tag = regEntrega.CodigoCidade.ToString();
                Bairro_EE.Text = regEntrega.NomeBairro.ToString();
                Bairro_EE.Tag = regEntrega.CodigoBairro.ToString();
                CEP_EE.Text = regEntrega.Cep == null ? "00000-000" : Convert.ToInt32(regEntrega.Cep.ToString()).ToString("00000-000");
            }

            //Carrega testada
            List<Models.Testada> ListaT = imobiliarioRepository.Lista_Testada(regImovel.Codigo);
            foreach (Models.Testada reg in ListaT) {
                ListViewItem lvItem = new ListViewItem(reg.Numface.ToString("00"));
                lvItem.SubItems.Add(string.Format("{0:0.00}", (decimal)reg.Areatestada));
                TestadaListView.Items.Add(lvItem);
            }
        }

        private void SairButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void CancelarButton_Click(object sender, EventArgs e) {
            ControlBehaviour(true);
        }

        private void CodigoButton_Click(object sender, EventArgs e) {
            InputBox z = new InputBox();
            String sCod = z.Show("", "Informação", "Digite o código do imóvel.", 6, GtiCore.ETweakMode.IntegerPositive);
            if (!string.IsNullOrEmpty(sCod)) {
                GtiCore.Ocupado(this);
                if (imobiliarioRepository.Existe_Imovel(Convert.ToInt32(sCod))) {
                    int Codigo = Convert.ToInt32(sCod);
                    this.Codigo.Text = Codigo.ToString("000000");
                    ControlBehaviour(true);
                    ClearFields();
                    CarregaImovel(Codigo);
                } else
                    MessageBox.Show("Imóvel não cadastrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GtiCore.Liberado(this);
            }
        }

        private void Numero_KeyPress(object sender, KeyPressEventArgs e) {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void ProprietarioListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e) {
            if (ProprietarioListView.SelectedIndices.Count > 0)
                ProprietarioListView.Items[ProprietarioListView.SelectedIndices[0]].ToolTipText = ProprietarioListView.Items[ProprietarioListView.SelectedIndices[0]].Tag.ToString();
        }

        private void ProprietarioListView_MouseHover(object sender, EventArgs e) {
            ProprietarioListView.Focus();
        }

        private void SaveReg() {
            Cadimob reg = new Cadimob();
            reg.Cip = IsentoCIPCheck.Checked;
            reg.Codcondominio = Convert.ToInt32(Condominio.Tag.ToString());
            reg.Conjugado = ConjugadoCheck.Checked;
            reg.Datainclusao = DateTime.Now;
            reg.Dc_qtdeedif = (short)AreaListView.Items.Count;
            reg.Distrito = Convert.ToInt16(Distrito.Text);
            reg.Dt_areaterreno = Convert.ToDecimal(AreaTerreno.Text);
            reg.Dt_codbenf = (short)BenfeitoriaList.SelectedValue;
            reg.Dt_codcategprop = (short)CategoriaTerrenoList.SelectedValue;
            reg.Dt_codpedol = (short)PedologiaList.SelectedValue;
            reg.Dt_codsituacao = (short)SituacaoList.SelectedValue;
            reg.Dt_codtopog = (short)TopografiaList.SelectedValue;
            reg.Dt_codusoterreno = (short)UsoTerrenoList.SelectedValue;
            if (FracaoIdeal.Text != "")
                reg.Dt_fracaoideal = Convert.ToDecimal(FracaoIdeal.Text);
            else
                reg.Dt_fracaoideal = 0;
            reg.Ee_tipoend = End1Option.Checked ? (short)0 : End2Option.Checked ? (short)1 : (short)2;
            reg.Imune = ImuneCheck.Checked;
            reg.Inativo = Ativo.Text == "ATIVO" ? false : true;
            reg.Li_cep = Cep.Text;
            reg.Li_codbairro = Convert.ToInt16(Bairro.Tag.ToString());
            reg.Li_codcidade = 413;
            reg.Li_compl = Complemento.Text;
            reg.Li_lotes = Lotes.Text;
            reg.Li_num = Convert.ToInt16(Numero.Text);
            reg.Li_quadras = Quadras.Text;
            reg.Li_uf = "SP";
            reg.Lote = Convert.ToInt32(Lote.Text);
            if (Matricula.Text != "")
                reg.Nummat = Convert.ToInt32(Matricula.Text);
            else
                reg.Nummat = 0;
            reg.Quadra = Convert.ToInt16(Quadra.Text);
            reg.Resideimovel = ResideCheck.Checked;
            reg.Seq = Convert.ToInt16(Face.Text);
            reg.Setor = Convert.ToInt16(Setor.Text);
            reg.Subunidade = Convert.ToInt16(SubUnidade.Text);
            reg.Tipomat = MT1Check.Checked ? "M" : "T";
            reg.Unidade = Convert.ToInt16(Unidade.Text);

            Exception ex;
            if (bAddNew) {
                reg.Codreduzido = imobiliarioRepository.Retorna_Codigo_Disponivel();
                ex = imobiliarioRepository.Incluir_Imovel(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                    goto Final;
                } else {
                    Codigo.Text = reg.Codreduzido.ToString("000000");
                }
            } else {
                reg.Codreduzido = Convert.ToInt32(Codigo.Text);
                ex = imobiliarioRepository.Alterar_Imovel(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                    goto Final;
                }
            }
            int nCodReduzido = reg.Codreduzido;

            //grava proprietário
            List<Proprietario> Lista = new List<Proprietario>();
            foreach (ListViewItem item in ProprietarioListView.Items) {
                Proprietario regProp = new Proprietario();
                regProp.Codreduzido = nCodReduzido;
                regProp.Codcidadao = Convert.ToInt32(item.Tag.ToString());
                regProp.Principal = item.Text.Substring(item.Text.Length - 1, 1) == ")" ? true : false;
                regProp.Tipoprop = item.Group.Name == "groupPP" ? "P" : "C";
                Lista.Add(regProp);
            }
            ex = imobiliarioRepository.Incluir_Proprietario(Lista);
            if (ex != null) {
                ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                eBox.ShowDialog();
                goto Final;
            }

            //grava testada
            List<Testada> ListaTestada = new List<Testada>();
            foreach (ListViewItem item in TestadaListView.Items) {
                Testada regT = new Testada();
                regT.Codreduzido = nCodReduzido;
                regT.Numface = Convert.ToInt16(item.Text.ToString());
                regT.Areatestada = Convert.ToDecimal(item.SubItems[1].Text.ToString());
                ListaTestada.Add(regT);
            }
            if (ListaTestada.Count > 0) {
                ex = imobiliarioRepository.Incluir_Testada(ListaTestada);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                    goto Final;
                }
            }

            //grava historico
            List<Historico> ListaHist = new List<Historico>();
            foreach (ListViewItem item in HistoricoListView.Items) {
                Historico regH = new Historico();
                regH.Codreduzido = nCodReduzido;
                regH.Seq = Convert.ToInt16(item.Text.ToString());
                regH.Datahist2 = Convert.ToDateTime(item.SubItems[1].Text.ToString());
                regH.Deschist = item.SubItems[2].Text;
                regH.Userid = Convert.ToInt32(item.Tag.ToString());
                ListaHist.Add(regH);
            }
            if (ListaHist.Count > 0) {
                ex = imobiliarioRepository.Incluir_Historico(ListaHist);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                    goto Final;
                }
            }

            //grava area
            List<Areas> ListaArea = new List<Areas>();
            foreach (ListViewItem item in AreaListView.Items) {
                Areas regA = new Areas();
                regA.Codreduzido = nCodReduzido;
                regA.Seqarea = Convert.ToInt16(item.Text.ToString());
                regA.Areaconstr = Convert.ToDecimal(item.SubItems[1].Text.ToString());
                regA.Usoconstr = Convert.ToInt16(item.SubItems[2].Tag.ToString());
                regA.Tipoconstr = Convert.ToInt16(item.SubItems[3].Tag.ToString());
                regA.Catconstr = Convert.ToInt16(item.SubItems[4].Tag.ToString());
                regA.Tipoarea = "";
                regA.Qtdepav = Convert.ToInt16(item.SubItems[5].Text);
                regA.Dataaprova = Convert.ToDateTime(item.SubItems[6].Text);
                regA.Numprocesso = item.SubItems[7].Text;
                ListaArea.Add(regA);
            }
            if (ListaArea.Count > 0) {
                ex = imobiliarioRepository.Incluir_Area(ListaArea);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                    goto Final;
                }
            }

        Final:;
            ControlBehaviour(true);
        }

        private void ProprietarioPrincipalMenu_Click(object sender, EventArgs e) {
            InputBox z = new InputBox();
            int nContaP = 0;
            String sCod = z.Show("", "Incluir proprietário", "Digite o código do cidadão.", 6, GtiCore.ETweakMode.IntegerPositive);
            if (!string.IsNullOrEmpty(sCod)) {
                int nCod = Convert.ToInt32(sCod);
                if (nCod < 500000 || nCod > 700000)
                    MessageBox.Show("Código de cidadão inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    if (!cidadaoRepository.Existe_Cidadao(nCod))
                        MessageBox.Show("Código não cadastrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        bool bFind = false;
                        foreach (ListViewItem item in ProprietarioListView.Items) {
                            if (item.Tag.ToString() == sCod) {
                                bFind = true;
                                break;
                            }
                        }
                        if (bFind)
                            MessageBox.Show("Código já cadastrado no imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else {
                            if (ProprietarioListView.Groups["groupPP"].Items.Count > 0)
                                nContaP = ProprietarioListView.Groups["groupPP"].Items.Count;
                            else
                                nContaP = 0;

                            string sNome = cidadaoRepository.Retorna_Nome_Cidadao(nCod);
                            ListViewItem lvItem = new ListViewItem {
                                Group = ProprietarioListView.Groups["groupPP"]
                            };
                            if (nContaP == 0)
                                lvItem.Text = sNome + " (Principal)";
                            else
                                lvItem.Text = sNome;
                            lvItem.Tag = sCod;
                            ProprietarioListView.Items.Add(lvItem);
                        }
                    }
                }
            }
        }

        private void ProprietarioSolidarioMenu_Click(object sender, EventArgs e) {
            InputBox z = new InputBox();
            String sCod = z.Show("", "Incluir proprietário solidário", "Digite o código do cidadão.", 6, GtiCore.ETweakMode.IntegerPositive);
            if (!string.IsNullOrEmpty(sCod)) {
                int nCod = Convert.ToInt32(sCod);
                if (nCod < 500000 || nCod > 700000)
                    MessageBox.Show("Código de cidadão inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    if (!cidadaoRepository.Existe_Cidadao(nCod))
                        MessageBox.Show("Código não cadastrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        bool bFind = false;
                        foreach (ListViewItem item in ProprietarioListView.Items) {
                            if (item.Tag.ToString() == sCod) {
                                bFind = true;
                                break;
                            }
                        }
                        if (bFind)
                            MessageBox.Show("Código já cadastrado no imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else {
                            string sNome = cidadaoRepository.Retorna_Nome_Cidadao(nCod);
                            ListViewItem lvItem = new ListViewItem {
                                Group = ProprietarioListView.Groups["groupPS"],
                                Text = sNome,
                                Tag = sCod
                            };
                            ProprietarioListView.Items.Add(lvItem);
                        }
                    }
                }
            }
        }

        private void RemoverProprietarioMenu_Click(object sender, EventArgs e) {
            if (ProprietarioListView.SelectedItems.Count == 0)
                MessageBox.Show("Selecione o cidadão a ser removido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                if (MessageBox.Show("Remover este cidadão?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ProprietarioListView.SelectedItems[0].Remove();
            }
        }

        private void ConsultarProprietarioMenu_Click(object sender, EventArgs e) {
            if (ProprietarioListView.SelectedItems.Count == 0)
                MessageBox.Show("Selecione o cidadão que deseja consultar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                int nCod = Convert.ToInt32(ProprietarioListView.SelectedItems[0].Tag.ToString());
                Cidadao f1 = (Cidadao)Application.OpenForms["Cidadao"];
                if (f1 != null)
                    f1.Close();
                Cidadao f2 = new Cidadao {
                    Tag = "Imovel",
                    CodCidadao = nCod
                };
                f2.ShowDialog();
            }
        }

        private void PrincipalProprietarioMenu_Click(object sender, EventArgs e) {
            int nContaP = 0;

            if (ProprietarioListView.SelectedItems.Count == 0)
                MessageBox.Show("Selecione o cidadão a ser promovido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                if (ProprietarioListView.SelectedItems[0].Group.Name == "groupPS")
                    MessageBox.Show("Proprietário solidário não pode ser o proprietário principal do imóvel. É necessário remover ele do grupo solidário e adicioná-lo ao grupo proprietário.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    //verifica se o grupo principal esta criado
                    if (ProprietarioListView.Groups["groupPP"].Items.Count > 0)
                        nContaP = ProprietarioListView.Groups["groupPP"].Items.Count;
                    //porque se existir remove o atributo do proprietário principal
                    if (nContaP > 0) {
                        foreach (ListViewItem item in ProprietarioListView.Groups["groupPP"].Items) {
                            if (item.Text.Contains("(Principal)")) {
                                item.Text = item.Text.Substring(0, item.Text.IndexOf("("));
                                break;
                            }
                        }
                    }
                    ProprietarioListView.SelectedItems[0].Text = ProprietarioListView.SelectedItems[0].Text + " (Principal)";
                }
            }
        }

        private void LocalImovelButton_Click(object sender, EventArgs e) {
            Models.Endereco reg = new Models.Endereco {
                Id_pais = 1,
                Sigla_uf = "SP",
                Id_cidade = 413,
                Id_bairro = string.IsNullOrWhiteSpace(Bairro.Text) ? 0 : Convert.ToInt32(Bairro.Tag.ToString())
            };
            if (Logradouro.Tag == null) Logradouro.Tag = "0";
            if (string.IsNullOrWhiteSpace(Logradouro.Tag.ToString()))
                Logradouro.Tag = "0";
            reg.Id_logradouro = string.IsNullOrWhiteSpace(Logradouro.Text) ? 0 : Convert.ToInt32(Logradouro.Tag.ToString());
            reg.Nome_logradouro = reg.Id_cidade != 413 ? Logradouro.Text : "";
            reg.Numero_imovel = Numero.Text == "" ? 0 : Convert.ToInt32(Numero.Text);
            reg.Complemento = Complemento.Text;
            reg.Email = "";

            Forms.Endereco f1 = new Forms.Endereco(reg, true, true, true, true);
            f1.ShowDialog();
            if (!f1.EndRetorno.Cancelar) {
                Bairro.Text = f1.EndRetorno.Nome_bairro;
                Bairro.Tag = f1.EndRetorno.Id_bairro.ToString();
                Logradouro.Text = f1.EndRetorno.Nome_logradouro;
                Logradouro.Tag = f1.EndRetorno.Id_logradouro.ToString();
                Numero.Text = f1.EndRetorno.Numero_imovel.ToString();
                Complemento.Text = f1.EndRetorno.Complemento;
                Cep.Text = f1.EndRetorno.Cep.ToString("00000-000");
                if (End1Option.Checked) {
                    Carrega_Endereco_Entrega_Imovel();
                }
            }
        }

        private void EndEntregaButton_Click(object sender, EventArgs e) {
            Models.Endereco reg = new Models.Endereco {
                Id_pais = 1,
                Sigla_uf = UF_EE.Text == "" ? "SP" : UF_EE.Text,
                Id_cidade = string.IsNullOrWhiteSpace(Cidade_EE.Text) ? 413 : Convert.ToInt32(Cidade_EE.Tag.ToString()),
                Id_bairro = string.IsNullOrWhiteSpace(Bairro_EE.Text) ? 0 : Convert.ToInt32(Bairro_EE.Tag.ToString())
            };
            if (Logradouro_EE.Tag == null) Logradouro_EE.Tag = "0";
            if (string.IsNullOrWhiteSpace(Logradouro_EE.Tag.ToString()))
                Logradouro_EE.Tag = "0";
            reg.Id_logradouro = string.IsNullOrWhiteSpace(Logradouro_EE.Text) ? 0 : Convert.ToInt32(Logradouro_EE.Tag.ToString());
            reg.Nome_logradouro = reg.Id_cidade != 413 ? Logradouro_EE.Text : "";
            reg.Numero_imovel = Numero_EE.Text == "" ? 0 : Convert.ToInt32(Numero_EE.Text);
            reg.Complemento = Complemento_EE.Text;
            reg.Email = "";


            Endereco f1 = new Endereco(reg, false, true, true, false);
            f1.ShowDialog();
            if (!f1.EndRetorno.Cancelar) {
                UF_EE.Text = f1.EndRetorno.Sigla_uf;
                Cidade_EE.Text = f1.EndRetorno.Nome_cidade;
                Cidade_EE.Tag = f1.EndRetorno.Id_cidade.ToString();
                Bairro_EE.Text = f1.EndRetorno.Nome_bairro;
                Bairro_EE.Tag = f1.EndRetorno.Id_bairro.ToString();
                Logradouro_EE.Text = f1.EndRetorno.Nome_logradouro;
                Logradouro_EE.Tag = f1.EndRetorno.Id_logradouro.ToString();
                Numero_EE.Text = f1.EndRetorno.Numero_imovel.ToString();
                Complemento_EE.Text = f1.EndRetorno.Complemento;
                CEP_EE.Text = f1.EndRetorno.Cep.ToString("00000-000");
            }
        }

        private void Testada_Face_KeyPress(object sender, KeyPressEventArgs e) {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void Testada_Metro_KeyPress(object sender, KeyPressEventArgs e) {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 44)) {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == 44) {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void AddTestada_Click(object sender, EventArgs e) {
            if (Testada_Face.Text.Trim() == "")
                MessageBox.Show("Digite o nº da face.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                if (GtiCore.ExtractNumber(Testada_Metro.Text.Trim()) == "")
                    MessageBox.Show("Digite o comprimento da testada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    bool bFind = false;
                    foreach (ListViewItem item in TestadaListView.Items) {
                        if (Convert.ToInt32(item.Text) == Convert.ToInt32(Testada_Face.Text)) {
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind)
                        MessageBox.Show("Face já cadastrada.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        ListViewItem reg = new ListViewItem(Convert.ToInt32(Testada_Face.Text).ToString("00"));
                        reg.SubItems.Add(string.Format("{0:0.00}", Convert.ToDecimal(Testada_Metro.Text)));
                        TestadaListView.Items.Add(reg);
                        Testada_Face.Text = "";
                        Testada_Metro.Text = "";
                    }
                }
            }
        }

        private void DelTestada_Click(object sender, EventArgs e) {
            if (TestadaListView.SelectedItems.Count == 0)
                MessageBox.Show("Selecione a testada a ser removida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                TestadaListView.SelectedItems[0].Remove();
        }

        private bool ValidateReg() {
            if (ProprietarioListView.Items.Count == 0) {
                MessageBox.Show("Cadastre o(s) proprietário(s) do imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Cep.Text == "") {
                MessageBox.Show("Digite o CEP do imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (AreaTerreno.Text == "")
                AreaTerreno.Text = "0";
            if (Convert.ToDecimal(AreaTerreno.Text) == 0) {
                MessageBox.Show("Digite a área do terreno.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool bFind = false;
            foreach (ListViewItem item in TestadaListView.Items) {
                if (Convert.ToInt32(item.Text) == Convert.ToInt32(Face.Text)) {
                    bFind = true;
                    break;
                }
            }
            if (!bFind) {
                MessageBox.Show("Digite a testada correspondente a face do imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Logradouro.Text == "") {
                MessageBox.Show("Digite o endereço do imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Bairro.Text == "") {
                MessageBox.Show("Digite o bairro do imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Logradouro_EE.Text == "") {
                MessageBox.Show("Digite o endereço de entrega do imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Bairro_EE.Text == "") {
                MessageBox.Show("Digite o bairro de entrega do imóvel.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (BenfeitoriaList.SelectedIndex == -1 || CategoriaTerrenoList.SelectedIndex == -1 || PedologiaList.SelectedIndex == -1 || SituacaoList.SelectedIndex == -1 || TopografiaList.SelectedIndex == -1 || UsoTerrenoList.SelectedIndex == -1) {
                MessageBox.Show("Selecione todas as opções dos dados do terreno.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Condominio.Tag == null) Condominio.Tag = "999";


            return true;
        }

















    }
}
