using GTI_v4.Interfaces;
using GTI_v4.Repository;
using GTI_v4.Classes;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using static GTI_v4.Classes.GtiTypes;
using GTI_v4.Models;

namespace GTI_v4.Forms {
    public partial class Processo : Form {
        IProtocoloRepository _protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        ICidadaoRepository _cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());
        ISistemaRepository _sistemaRepository = new SistemaRepository(GtiCore.Connection_Name());

        bool bExec;
        bool bAssunto;
        bool bAddNew;
        string EmptyDateText = "  /  /    ";
        List<CustomListBoxItem> lstButtonState;

        public int _numero_processo { get; set; }
        public short _ano_processo { get; set; }
        public string ObsArquiva { get; set; }
        public string ObsCancela { get; set; }
        public string ObsReativa { get; set; }
        public string ObsSuspende { get; set; }

        //State Control
        public bool _addEnderecoButton { get; set; }
        public bool _delEnderecoButton { get; set; }
        public bool _tbar { get; set; }
        public bool _zoombutton { get; set; }
        public bool _cidadaoeditbutton { get; set; }
        public bool _cidadaooldbutton { get; set; }
        public bool _guiabutton { get; set; }
        public bool _documentoeditbutton { get; set; }
        public bool _arquivalabel { get; set; }
        public bool _cancelalabel { get; set; }
        public bool _reativalabel { get; set; }
        public bool _suspensaolabel { get; set; }
        public bool _anexolabel { get; set; }
        public bool _numproc { get; set; }

        public Processo() {
            GtiCore.Ocupado(this);
            InitializeComponent();
            OrigemCombo.Items.Add(new CustomListBoxItem("CENTRO DE CUSTOS", 1));
            OrigemCombo.Items.Add(new CustomListBoxItem("SISTEMA PRÁTICO", 2));
            lstButtonState = new List<CustomListBoxItem>();
            DocPanel.Hide();

            bAssunto = false;
            List<CustomListBoxItem2> myItems = new List<CustomListBoxItem2>();
            List<Models.Assunto> lista = _protocoloRepository.Lista_Assunto(true, false);
            foreach (Models.Assunto item in lista) {
                myItems.Add(new CustomListBoxItem2(item.Nome, item.Codigo, item.Ativo));
            }
            AssuntoCombo.DisplayMember = "_name";
            AssuntoCombo.ValueMember = "_value";
            AssuntoCombo.DataSource = myItems;
            bAssunto = true;

            List<Models.Centrocusto> listalocal = _protocoloRepository.Lista_Local(true, false);
            CCustoCombo.DataSource = listalocal;
            CCustoCombo.DisplayMember = "Descricao";
            CCustoCombo.ValueMember = "Codigo";

            ClearFields();
            bExec = true;
            ControlBehaviour(true);
            bExec = false;
            GtiCore.Liberado(this);
        }

        private void GetButtonState() {
            lstButtonState.Clear();
            lstButtonState.Add(new CustomListBoxItem("btAdd", AddButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btEdit", EditButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btDel", DelButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btGravar", GravarButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btCancelar", CancelarButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btZoom", ZoomButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btAddEndereco", AddEnderecoButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btDelEndereco", DelEnderecoButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btFind", FindButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btDocumentoEdit", DocumentoEditButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btCidadaoEdit", CidadaoEditButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btCidadaoOld", CidadaoOldButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btGuia", GuiaButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btOpcao", OpcaoButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btSair", SairButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btTramitar", TramitarButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btPrint", PrintButton.Enabled ? 1 : 0));
            lstButtonState.Add(new CustomListBoxItem("btPrintDoc", PrintDocButton.Enabled ? 1 : 0));
        }

        private void SetButtonState() {
            if (lstButtonState.Count == 0) return;
            CustomListBoxItem r = lstButtonState.Find(item => item._name == "btAdd");
            AddButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btEdit");
            EditButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btDel");
            DelButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btGravar");
            GravarButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btCancelar");
            CancelarButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btZoom");
            ZoomButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btAddEndereco");
            AddEnderecoButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btDelEndereco");
            DelEnderecoButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btFind");
            FindButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btDocumentoEdit");
            DocumentoEditButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btFind");
            FindButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btCidadaoEdit");
            CidadaoEditButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btCidadaoOld");
            CidadaoOldButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btGuia");
            GuiaButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btOpcao");
            OpcaoButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btSair");
            SairButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btTramitar");
            TramitarButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btPrint");
            PrintButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btPrintDoc");
            PrintDocButton.Enabled = Convert.ToBoolean(r._value);
        }

        private void ControlBehaviour(bool bStart) {
            AnexoLabel.Enabled = bStart;
            ArquivaLabel.Enabled = bStart;
            CancelaLabel.Enabled = bStart;
            EntradaLabel.Enabled = bStart;
            ReativaLabel.Enabled = bStart;
            SuspensaoLabel.Enabled = bStart;
            AddButton.Enabled = bStart;
            EditButton.Enabled = bStart;
            DelButton.Enabled = bStart;
            SairButton.Enabled = bStart;
            PrintButton.Enabled = bStart;
            FindButton.Enabled = bStart;
            GravarButton.Enabled = !bStart;
            CancelarButton.Enabled = !bStart;
            OpcaoButton.Enabled = bStart;
            TramitarButton.Enabled = bStart;

            ComplementoText.ReadOnly = bStart;

            if (!bAddNew) {
                if (!gtiCore.IsEmptyDate(ArquivaLabel.Text) || !gtiCore.IsEmptyDate(CancelaLabel.Text) || !gtiCore.IsEmptyDate(SuspensaoLabel.Text))
                    bStart = true;

                bool bAllow = gtiCore.GetBinaryAccess((int)TAcesso.CadastroProcesso_Alterar_Avancado);
                if (bAllow) {
                    Fisicocheckbox.Enabled = !bStart;
                    Internocheckbox.Enabled = !bStart;
                    ComOption.Enabled = !bStart;
                    ResOption.Enabled = !bStart;
                    ObsText.ReadOnly = bStart;
                    CidadaoEditButton.Enabled = !bStart;
                    DelEnderecoButton.Enabled = !bStart;
                    AddEnderecoButton.Enabled = !bStart;
                    AssuntoText.Visible = bStart;
                    AssuntoText.ReadOnly = true;
                    AssuntoCombo.Visible = !bStart;
                    InscricaoText.ReadOnly = bStart;
                    OrigemText.Visible = bStart;
                    OrigemText.ReadOnly = true;
                    OrigemCombo.Visible = !bStart;
                    ObsText.ReadOnly = bStart;
                    NumProcText.ReadOnly = !bStart;
                    CCustoText.Visible = bStart;
                    CCustoText.ReadOnly = true;
                    CCustoCombo.Visible = !bStart;
                    DocListView.Enabled = !bStart;
                } else {
                    bAllow = gtiCore.GetBinaryAccess((int)TAcesso.CadastroProcesso_Alterar_Basico);
                    if (bAllow) {
                        ObsText.ReadOnly = bStart;
                        InscricaoText.ReadOnly = bStart;
                    }
                }
            } else {
                Fisicocheckbox.Enabled = !bStart;
                Internocheckbox.Enabled = !bStart;
                ComOption.Enabled = !bStart;
                ResOption.Enabled = !bStart;
                ObsText.ReadOnly = bStart;
                CidadaoEditButton.Enabled = !bStart;
                DelEnderecoButton.Enabled = !bStart;
                AddEnderecoButton.Enabled = !bStart;
                AssuntoText.Visible = bStart;
                AssuntoText.ReadOnly = true;
                AssuntoCombo.Visible = !bStart;
                InscricaoText.ReadOnly = bStart;
                OrigemText.Visible = bStart;
                OrigemText.ReadOnly = true;
                OrigemCombo.Visible = !bStart;
                ObsText.ReadOnly = bStart;
                NumProcText.ReadOnly = !bStart;
                CCustoText.Visible = bStart;
                CCustoText.ReadOnly = true;
                CCustoCombo.Visible = !bStart;
                DocListView.Enabled = !bStart;
            }
        }

        private void ClearFields() {
            AtendenteLabel.Text = "";
            HoraLabel.Text = "00:00";
            AssuntoText.Text = "";
            Fisicocheckbox.Checked = true;
            Internocheckbox.Checked = false;
            AssuntoCombo.SelectedIndex = -1;
            OrigemCombo.SelectedIndex = 1;
            ComplementoText.Text = "";
            InscricaoText.Text = "";
            EntradaLabel.Text = DateTime.Parse(DateTime.Now.ToString()).ToString("dd/MM/yyyy");
            ReativaLabel.Text = EmptyDateText;
            SuspensaoLabel.Text = EmptyDateText;
            ArquivaLabel.Text = EmptyDateText;
            CancelaLabel.Text = EmptyDateText;
            AnexoLabel.Text = "";
            AnexoLogListView.Items.Clear();
            ObsText.Text = "";
            CodCidadaoLabel.Text = "000000";
            NomeCidadaoLabel.Text = "";
            CCustoCombo.SelectedIndex = -1;
            EnderecoListView.Items.Clear();
            DocListView.Items.Clear();
            SituacaoLabel.Text = "NORMAL";
            NomeLabel.Text = "";
            DocLabel.Text = "";
            EndLabel.Text = "";
            ComplLabel.Text = "";
            BairroLabel.Text = "";
            CidadeLabel.Text = "";
            RGLabel.Text = "";
            DocEntregueLabel.Text = "0";
            DocPendenteLabel.Text = "0";
        }

        private void UnlockForm() {
            AddEnderecoButton.Enabled = _addEnderecoButton;
            DelEnderecoButton.Enabled = _delEnderecoButton;
            tBar.Enabled = _tbar;
            ZoomButton.Enabled = _zoombutton;
            CidadaoEditButton.Enabled = _cidadaoeditbutton;
            CidadaoOldButton.Enabled = _cidadaooldbutton;
            GuiaButton.Enabled = _guiabutton;
            DocumentoEditButton.Enabled = _documentoeditbutton;
            ArquivaLabel.Enabled = _arquivalabel;
            CancelaLabel.Enabled = _cancelalabel;
            ReativaLabel.Enabled = _reativalabel;
            SuspensaoLabel.Enabled = _suspensaolabel;
            AnexoLabel.Enabled = _anexolabel;
            NumProcText.ReadOnly = _numproc;
        }

        private void LockForm() {
            _addEnderecoButton = AddEnderecoButton.Enabled;
            _delEnderecoButton = DelEnderecoButton.Enabled;
            _tbar = tBar.Enabled;
            _zoombutton = ZoomButton.Enabled;
            _cidadaoeditbutton = CidadaoEditButton.Enabled;
            _cidadaooldbutton = CidadaoOldButton.Enabled;
            _guiabutton = GuiaButton.Enabled;
            _documentoeditbutton = DocumentoEditButton.Enabled;
            _arquivalabel = ArquivaLabel.Enabled;
            _cancelalabel = CancelaLabel.Enabled;
            _reativalabel = ReativaLabel.Enabled;
            _suspensaolabel = SuspensaoLabel.Enabled;
            _anexolabel = AnexoLabel.Enabled;
            _numproc = NumProcText.ReadOnly;

            AddEnderecoButton.Enabled = false;
            DelEnderecoButton.Enabled = false;
            tBar.Enabled = false;
            ZoomButton.Enabled = false;
            CidadaoEditButton.Enabled = false;
            CidadaoOldButton.Enabled = false;
            GuiaButton.Enabled = false;
            DocumentoEditButton.Enabled = false;
            ArquivaLabel.Enabled = false;
            CancelaLabel.Enabled = false;
            ReativaLabel.Enabled = false;
            SuspensaoLabel.Enabled = false;
            AnexoLabel.Enabled = false;
            NumProcText.ReadOnly = false;

            Doc1Check.Checked = true;
            Doc2Check.Checked = false;
            Doc3Check.Checked = false;
            Doc4Check.Checked = false;
            Doc5Check.Checked = false;
            Doc1Check.Focus();
        }

        private void AddButton_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProcesso_Novo);
            if (bAllow) {
                _ano_processo = 0;
                _numero_processo = 0;
                bAddNew = true;
                ClearFields();
                AtendenteLabel.Text = GtiCore.Retorna_Last_User();
                AtendenteLabel.Tag = _sistemaRepository.Retorna_User_LoginId(AtendenteLabel.Text);
                NumProcText.Text = "";
                ControlBehaviour(false);
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EditButton_Click(object sender, EventArgs e) {
            bool bAllow = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProcesso_Alterar_Avancado);
            bool bAllow2 = GtiCore.GetBinaryAccess((int)TAcesso.CadastroProcesso_Alterar_Basico);
            if (bAllow || bAllow2) {
                bAddNew = false;
                if (String.IsNullOrEmpty(AssuntoText.Text))
                    MessageBox.Show("Nenhum processo carregado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    ControlBehaviour(false);
                    ObsText.Focus();
                }
            } else
                MessageBox.Show("Acesso não permitido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GravarButton_Click(object sender, EventArgs e) {
            if (ValidateReg()) {
                Exception ex = Grava_Processo();
                if (ex == null) {
                    ex = Grava_Endereco();
                    if (ex == null) {
                        ex = Grava_Documento();
                        CarregaProcesso();
                        ControlBehaviour(true);
                    }
                }
            }
        }

        private bool ValidateReg() {
            if (AssuntoCombo.SelectedIndex == -1) {
                MessageBox.Show("Selecione o assunto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(ComplementoText.Text.Trim())) {
                MessageBox.Show("Digite o complemento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (OrigemCombo.SelectedIndex == 0 && CCustoCombo.SelectedIndex == -1) {
                MessageBox.Show("Selecione o Centro de Custos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (OrigemCombo.SelectedIndex == 1 && NomeCidadaoLabel.Text == "") {
                MessageBox.Show("Selecione o requerente do processo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(InscricaoText.Text.Trim()))
                InscricaoText.Text = "0";

            return true;
        }

        private Exception Grava_Processo() {
            Processogti Reg = new Processogti {
                Numero = _numero_processo,
                Ano = _ano_processo,
                Complemento = ComplementoText.Text.Trim(),
                Observacao = ObsText.Text.Trim(),
                Insc = Convert.ToInt32(InscricaoText.Text),
                Dataentrada = Convert.ToDateTime(EntradaLabel.Text),
                Hora = HoraLabel.Text
            };
            if (GtiCore.IsEmptyDate(SuspensaoLabel.Text))
                Reg.Datasuspenso = null;
            else
                Reg.Datasuspenso = Convert.ToDateTime(SuspensaoLabel.Text);
            if (GtiCore.IsEmptyDate(ReativaLabel.Text))
                Reg.Datareativa = null;
            else
                Reg.Datareativa = Convert.ToDateTime(ReativaLabel.Text);
            if (GtiCore.IsEmptyDate(ArquivaLabel.Text))
                Reg.Dataarquiva = null;
            else
                Reg.Dataarquiva = Convert.ToDateTime(ArquivaLabel.Text);
            if (GtiCore.IsEmptyDate(CancelaLabel.Text))
                Reg.Datacancel = null;
            else
                Reg.Datacancel = Convert.ToDateTime(CancelaLabel.Text);
            Reg.Interno = Internocheckbox.Checked ? true : false;
            Reg.Fisico = Fisicocheckbox.Checked ? true : false;
            Reg.Codassunto = Convert.ToInt16(AssuntoText.Tag.ToString());
            Reg.Userid = Convert.ToInt32(AtendenteLabel.Tag.ToString());
            CustomListBoxItem selectedData = (CustomListBoxItem)OrigemCombo.SelectedItem;
            Reg.Origem = Convert.ToInt16(selectedData._value);
            if (OrigemCombo.SelectedIndex == 0) {
                Reg.Centrocusto = Convert.ToInt32(CCustoCombo.SelectedValue);
                Reg.Codcidadao = 0;
            } else {
                Reg.Centrocusto = 0;
                Reg.Codcidadao = Convert.ToInt32(CodCidadaoLabel.Text);
            }

            Exception ex;
            if (bAddNew) {
                Reg.Hora = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00");
                Reg.Ano = (short)DateTime.Now.Year;
                Reg.Numero = _protocoloRepository.Retorna_Numero_Disponivel(DateTime.Now.Year);
                _ano_processo = Reg.Ano;
                _numero_processo = Reg.Numero;
                ex = _protocoloRepository.Incluir_Processo(Reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                    return ex;
                } else {
                    bExec = false;
                    NumProcText.Text = $"{ _numero_processo}-{_protocoloRepository.DvProcesso(_numero_processo)}/{_ano_processo}";
                    bExec = true;
                }
                Reg.Tipoend = ComOption.Checked ? "C" : "R";
            } else {
                ex = _protocoloRepository.Alterar_Processo(Reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                } else {
                    ControlBehaviour(true);
                }
            }
            return null;
        }

        private Exception Grava_Endereco() {
            List<Processoend> ListaEndereco = new List<Processoend>();
            foreach (ListViewItem item in EnderecoListView.Items) {
                Processoend regEnd = new Processoend {
                    Ano = _ano_processo,
                    Numprocesso = _numero_processo,
                    Codlogr = Convert.ToInt16(item.SubItems[1].Text),
                    Numero = item.SubItems[2].Text
                };
                ListaEndereco.Add(regEnd);
            }

            Exception ex = _protocoloRepository.Incluir_Processo_Endereco(ListaEndereco, _ano_processo, _numero_processo);
            if (ex != null) {
                ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                eBox.ShowDialog();
            }
            return ex;
        }

        private Exception Grava_Documento() {
            List<Processodoc> ListaDoc = new List<Processodoc>();
            foreach (ListViewItem item in DocListView.Items) {
                Processodoc reg = new Processodoc {
                    Ano = _ano_processo,
                    Numero = _numero_processo,
                    Coddoc = Convert.ToInt16(item.SubItems[1].Text)
                };
                if (!string.IsNullOrWhiteSpace(item.SubItems[2].Text))
                    reg.Data = Convert.ToDateTime(item.SubItems[2].Text);

                ListaDoc.Add(reg);
            }

            Exception ex = _protocoloRepository.Incluir_Processo_Documento(ListaDoc, _ano_processo, _numero_processo);
            if (ex != null) {
                ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                eBox.ShowDialog();
            }
            return ex;
        }

        private void CarregaProcesso() {
            if (!String.IsNullOrEmpty(NumProcText.Text)) {
                Exception ex = _protocoloRepository.ValidaProcesso(NumProcText.Text);
                if (ex == null) {
                    bExec = false;
                    LoadReg();
                    bExec = true;
                    tBar.Focus();
                } else {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                }
            } else
                MessageBox.Show("Digite um processo cadastrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadReg() {
            GtiCore.Ocupado(this);
            _numero_processo = _protocoloRepository.ExtractNumeroProcessoNoDV(NumProcText.Text);
            _ano_processo = _protocoloRepository.ExtractAnoProcesso(NumProcText.Text);
            ProcessoStruct Reg = _protocoloRepository.Dados_Processo(_ano_processo, _numero_processo);
            AssuntoCombo.SelectedValue = Convert.ToInt32(Reg.CodigoAssunto);
            AssuntoText.Text = AssuntoCombo.Text;
            ComplementoText.Text = Reg.Complemento;
            AtendenteLabel.Text = Reg.AtendenteNome;
            AtendenteLabel.Tag = Reg.AtendenteId.ToString();
            ObsText.Text = Reg.Observacao;
            HoraLabel.Text = Reg.Hora;
            InscricaoText.Text = Reg.Inscricao.ToString();
            EntradaLabel.Text = Reg.DataEntrada == null ? EmptyDateText : DateTime.Parse(Reg.DataEntrada.ToString()).ToString("dd/MM/yyyy");
            ArquivaLabel.Text = Reg.DataArquivado == null ? EmptyDateText : DateTime.Parse(Reg.DataArquivado.ToString()).ToString("dd/MM/yyyy");
            ReativaLabel.Text = Reg.DataReativacao == null ? EmptyDateText : DateTime.Parse(Reg.DataReativacao.ToString()).ToString("dd/MM/yyyy");
            SuspensaoLabel.Text = Reg.DataSuspensao == null ? EmptyDateText : DateTime.Parse(Reg.DataSuspensao.ToString()).ToString("dd/MM/yyyy");
            CancelaLabel.Text = Reg.DataCancelado == null ? EmptyDateText : DateTime.Parse(Reg.DataCancelado.ToString()).ToString("dd/MM/yyyy");

            if (Reg.DataCancelado != null)
                SituacaoLabel.Text = "CANCELADO";
            else {
                if (Reg.DataArquivado != null)
                    SituacaoLabel.Text = "ARQUIVADO";
                else {
                    if (Reg.DataSuspensao != null)
                        SituacaoLabel.Text = "SUSPENSO";
                    else
                        SituacaoLabel.Text = "NORMAL";
                }
            }

            AnexoLabel.Text = Reg.Anexo;
            Internocheckbox.Checked = Reg.Interno;
            Fisicocheckbox.Checked = Reg.Fisico;

            for (int r = 0; r < OrigemCombo.Items.Count; r++) {
                CustomListBoxItem selectedData = (CustomListBoxItem)OrigemCombo.Items[r];
                if (Reg.Origem == selectedData._value) {
                    OrigemCombo.SelectedIndex = r;
                    OrigemText.Text = OrigemCombo.Text;
                    break;
                }
            }

            CCustoCombo.SelectedValue = Convert.ToInt16(Reg.CentroCusto);
            CCustoText.Text = CCustoCombo.Text;
            CodCidadaoLabel.Text = Reg.CodigoCidadao.ToString();
            NomeCidadaoLabel.Text = Reg.NomeCidadao;

            if (Reg.CentroCusto > 0) {
                pnlCCusto.Visible = true;
                RequerentePanel.Visible = false;
            } else {
                pnlCCusto.Visible = false;
                RequerentePanel.Visible = true;
            }

            EnderecoListView.Items.Clear();
            foreach (var item in Reg.ListaProcessoEndereco) {
                ListViewItem lvi = new ListViewItem(item.NomeLogradouro);
                lvi.SubItems.Add(item.CodigoLogradouro.ToString());
                lvi.SubItems.Add(item.Numero);
                EnderecoListView.Items.Add(lvi);
            }

            MainListView.Items.Clear();
            foreach (var item in Reg.ListaAnexo) {
                String sNumProc = item.NumeroAnexo.ToString() + "-" + _protocoloRepository.DvProcesso(item.NumeroAnexo).ToString() + "/" + item.AnoAnexo.ToString();
                ListViewItem lvi = new ListViewItem(sNumProc);
                lvi.SubItems.Add(item.Requerente);
                lvi.SubItems.Add(item.Complemento);
                MainListView.Items.Add(lvi);
            }

            foreach (var item in Reg.ListaAnexoLog) {
                String sNumProc = item.Numero_anexo.ToString() + "-" + _protocoloRepository.DvProcesso(item.Numero_anexo).ToString() + "/" + item.Ano_anexo.ToString();
                ListViewItem lvi = new ListViewItem(item.Data.ToString("dd/MM/yyyy"));
                lvi.SubItems.Add(sNumProc);
                lvi.SubItems.Add(item.Ocorrencia);
                lvi.SubItems.Add(item.UserName);
                AnexoLogListView.Items.Add(lvi);
            }

            ObsArquiva = Reg.ObsArquiva;
            //pnlDoc.Show();
            DocListView.Items.Clear();
            if (Reg.ListaProcessoDoc.Count == 0) {
                //se não houver documentos gravados no processo carrega lista padrão de documentos do assunto selecionado.
                List<AssuntoDocStruct> ListaDoc = new List<AssuntoDocStruct>();
                ListaDoc = _protocoloRepository.Lista_Assunto_Documento((short)Reg.CodigoAssunto);
                foreach (AssuntoDocStruct item in ListaDoc) {
                    ProcessoDocStruct reg = new ProcessoDocStruct {
                        CodigoDocumento = item.Codigo,
                        NomeDocumento = item.Nome
                    };
                    Reg.ListaProcessoDoc.Add(reg);
                }
            }


            foreach (var item in Reg.ListaProcessoDoc) {
                ListViewItem lvi = new ListViewItem(item.NomeDocumento);
                lvi.SubItems.Add(item.CodigoDocumento.ToString());
                if (item.DataEntrega == null) {
                    lvi.SubItems.Add("");
                } else {
                    lvi.Checked = true;
                    lvi.SubItems.Add(DateTime.Parse(item.DataEntrega.ToString()).ToString("dd/MM/yyyy"));
                }
                DocListView.Items.Add(lvi);
            }
            GtiCore.Liberado(this);
            DocPanel.Hide();
            UpdateDocNumber();

        }

        private void UpdateDocNumber() {
            int DocEntregue = 0;
            int DocPendente = 0;
            foreach (ListViewItem lvItem in DocListView.Items) {
                if (lvItem.Checked)
                    DocEntregue++;
                else
                    DocPendente++;
            }
            DocEntregueLabel.Text = DocEntregue.ToString();
            DocPendenteLabel.Text = DocPendente.ToString();
        }

    }
}
