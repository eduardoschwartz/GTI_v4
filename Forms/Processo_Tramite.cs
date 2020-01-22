using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GTI_v4.Classes.GtiTypes;

namespace GTI_v4.Forms {
    public partial class Processo_Tramite : Form {
        readonly IProtocoloRepository _protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        readonly ISistemaRepository _sistemaRepository = new SistemaRepository(GtiCore.Connection_Name());
        List<CustomListBoxItem> lstButtonState;
        public int Ano_Processo { get; set; }
        public int Num_Processo { get; set; }
        private bool bFechado;

        public Processo_Tramite(int AnoProcesso, int NumProcesso) {
            InitializeComponent();
            Size = new Size(Properties.Settings.Default.Form_Processo_Tramite_Size.Width, Properties.Settings.Default.Form_Processo_Tramite_Size.Height);

            tBar.Renderer = new MySR();
            Ano_Processo = AnoProcesso;
            Num_Processo = NumProcesso;
            lstButtonState = new List<GtiTypes.CustomListBoxItem>();
            ProcessoStruct Reg = _protocoloRepository.Dados_Processo(Ano_Processo, Num_Processo);
            List<Despacho> lstDespacho = _protocoloRepository.Lista_Despacho();
            List<Despacho> lstDespacho2 = _protocoloRepository.Lista_Despacho();
            DespachoList.DataSource = lstDespacho;
            DespachoList.DisplayMember = "descricao";
            DespachoList.ValueMember = "codigo";
            Despacho2List.DataSource = lstDespacho2;
            Despacho2List.DisplayMember = "descricao";
            Despacho2List.ValueMember = "codigo";

            Centrocusto tblCCusto = new Centrocusto();
            List<Centrocusto> lstCCusto = _protocoloRepository.Lista_Local(true, false);
            CCustoList.DataSource = lstCCusto;
            CCustoList.DisplayMember = "descricao";
            CCustoList.ValueMember = "codigo";

            NumProcLabel.Text = NumProcesso.ToString() + "-" + Reg.Dv.ToString() + "/" + AnoProcesso.ToString();
            ComplementoLabel.Text = Reg.Assunto;
            ComplementoLabel.Tag = Reg.CodigoAssunto.ToString();
            RequerenteLabel.Text = Reg.NomeCidadao;
            Forms.Processo f3 = (Forms.Processo)Application.OpenForms["Processo"];
            SitLabel.Text = f3.SituacaoLabel.Text;
            bFechado = SitLabel.Text == "NORMAL" ? false : true;
            CarregaTramite();
        }

        private void CarregaTramite() {
            MainListView.Items.Clear();
            GtiCore.Ocupado(this);
            List<TramiteStruct> Lista = _protocoloRepository.Dados_Tramite((short)Ano_Processo, Num_Processo, Convert.ToInt32(ComplementoLabel.Tag.ToString()));

            int nPos = 0;
            foreach (TramiteStruct Reg in Lista) {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(Reg.Seq.ToString("00"));
                lvi.SubItems.Add(Reg.CentroCustoCodigo.ToString());
                lvi.SubItems.Add(Reg.CentroCustoNome ?? "");
                lvi.SubItems.Add(Reg.DataEntrada ?? "");
                lvi.SubItems.Add(Reg.HoraEntrada ?? "");
                lvi.SubItems.Add(Reg.Usuario1 ?? "");
                lvi.SubItems.Add(Reg.DespachoNome ?? "");
                lvi.SubItems.Add("0");
                lvi.SubItems.Add(Reg.DataEnvio ?? "");
                lvi.SubItems.Add(Reg.Usuario2 ?? "");
                lvi.SubItems.Add(Reg.Obs ?? "");
                lvi.Tag = Reg.Obs ?? "";
                if (!string.IsNullOrEmpty(Reg.Obs)) lvi.ImageIndex = 0;
                MainListView.Items.Add(lvi);
                nPos++;
            }
            CalculoDias();
            GtiCore.Liberado(this);
        }

        private void LockForm(bool bLock) {
            tBar.Enabled = bLock;
            AddLocalButton.Enabled = bLock;
            RemoveLocalButton.Enabled = bLock;
            UpButton.Enabled = bLock;
            DownButton.Enabled = bLock;
            EmprestimoButton.Enabled = bLock;
            AlterarButton.Enabled = bLock;
            ObsButton.Enabled = bLock;
            ReceberButton.Enabled = bLock;
            EnviarButton.Enabled = bLock;
        }

        private void GetButtonState() {
            lstButtonState.Clear();
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btAddLocal", AddLocalButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btRemoveLocal", RemoveLocalButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btUp", UpButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btDown", DownButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btEmprestimo", EmprestimoButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btAlterar", AlterarButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btObs", ObsButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btReceber", ReceberButton.Enabled ? 1 : 0));
            lstButtonState.Add(new GtiTypes.CustomListBoxItem("btEnviar", EnviarButton.Enabled ? 1 : 0));
        }

        private void SetButtonState() {
            if (lstButtonState.Count == 0) return;
            GtiTypes.CustomListBoxItem r = lstButtonState.Find(item => item._name == "btAddLocal");
            AddLocalButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btRemoveLocal");
            RemoveLocalButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btUp");
            UpButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btDown");
            DownButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btEmprestimo");
            EmprestimoButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btAlterar");
            AlterarButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btObs");
            ObsButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btReceber");
            ReceberButton.Enabled = Convert.ToBoolean(r._value);
            r = lstButtonState.Find(item => item._name == "btEnviar");
            EnviarButton.Enabled = Convert.ToBoolean(r._value);
        }

        private void ReorderList() {
            int nPos = 1;
            foreach (ListViewItem lvItem in MainListView.Items) {
                lvItem.SubItems[1].Text = nPos.ToString("00");
                nPos++;
            }
        }

        private void UpButton_Click(object sender, EventArgs e) {
            if (MainListView.SelectedItems[0].Index == 0)
                MessageBox.Show("Não é possível mover para cima o 1º local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                if (String.IsNullOrEmpty(MainListView.SelectedItems[0].SubItems[4].Text)) {
                    if (String.IsNullOrEmpty(MainListView.Items[MainListView.SelectedItems[0].Index - 1].SubItems[4].Text)) {

                        ListViewItem lvOld = MainListView.SelectedItems[0];
                        int nInd = MainListView.SelectedItems[0].Index;
                        MainListView.Items.Remove(MainListView.SelectedItems[0]);
                        MainListView.Items.Insert(nInd - 1, lvOld);
                        MainListView.Items[nInd - 1].SubItems[1].Text = (nInd).ToString();
                        MainListView.Items[nInd].SubItems[1].Text = (nInd + 1).ToString();
                    } else
                        MessageBox.Show("Não é possível mover este local porque já houve recebimento de processo no local acima.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    MessageBox.Show("Não é possível mover este local porque já houve recebimento de processo no mesmo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownButton_Click(object sender, EventArgs e) {
            if (MainListView.SelectedItems[0].Index == MainListView.Items.Count - 1)
                MessageBox.Show("Não é possível mover para baixo o último local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                if (String.IsNullOrEmpty(MainListView.SelectedItems[0].SubItems[4].Text)) {
                    if (String.IsNullOrEmpty(MainListView.Items[MainListView.SelectedItems[0].Index + 1].SubItems[4].Text)) {
                        ListViewItem lvOld = MainListView.SelectedItems[0];
                        int nInd = MainListView.SelectedItems[0].Index;
                        MainListView.Items.Remove(MainListView.SelectedItems[0]);
                        MainListView.Items.Insert(nInd + 1, lvOld);
                        MainListView.Items[nInd + 1].SubItems[1].Text = (nInd + 2).ToString();
                        MainListView.Items[nInd].SubItems[1].Text = (nInd + 1).ToString();
                    } else
                        MessageBox.Show("Não é possível mover este local porque já houve recebimento de processo no local abaixo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    MessageBox.Show("Não é possível mover este local porque já houve recebimento de processo no mesmo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Processo_Tramite_FormClosing(object sender, FormClosingEventArgs e) {
            GtiCore.Ocupado(this);
            List<TramiteStruct> Lista = new List<TramiteStruct>();
            foreach (ListViewItem lvItem in MainListView.Items) {
                TramiteStruct Reg = new TramiteStruct {
                    Ano = Ano_Processo,
                    Numero = Num_Processo,
                    Seq = Convert.ToInt32(lvItem.SubItems[1].Text),
                    CentroCustoCodigo = Convert.ToInt16(lvItem.SubItems[2].Text)
                };
                Lista.Add(Reg);
            }
            _protocoloRepository.Incluir_MovimentoCC((short)Ano_Processo, Num_Processo, Lista);
            GtiCore.Liberado(this);
            Properties.Settings.Default.Form_Processo_Tramite_Size = new Size(Width, Height);
            Properties.Settings.Default.Save();
        }

        private void AddLocalButton_Click(object sender, EventArgs e) {
            if (MainListView.SelectedItems.Count == 0) {
                MessageBox.Show("Selecione a posição na lista que deseja inserir.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bFechado) {
                MessageBox.Show("O processo não está aberto.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {
                if (MainListView.Items.Count == 1)
                    goto Adicionar;

                if (MainListView.Items.Count - 1 == MainListView.SelectedItems[0].Index)
                    goto Adicionar;

                if (String.IsNullOrEmpty(MainListView.Items[MainListView.SelectedItems[0].Index + 1].SubItems[4].Text))
                    goto Adicionar;
            }

            MessageBox.Show("Não é possível inserir neste local porque já houve recebimento de processo no local abaixo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;

        Adicionar:;
            GetButtonState();
            LockForm(false);
            MainListView.Enabled = false;
            LocalPanel.Visible = true;
            LocalPanel.BringToFront();
        }

        private void RemoveLocalButton_Click(object sender, EventArgs e) {
            if (MainListView.SelectedItems.Count == 0) {
                MessageBox.Show("Selecione a posição na lista que deseja remover.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bFechado)
                MessageBox.Show("O processo não está aberto.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {

                int nIndex;
                nIndex = MainListView.SelectedItems[0].Index;

                if (String.IsNullOrEmpty(MainListView.Items[nIndex].SubItems[4].Text)) {
                    MainListView.Items.RemoveAt(MainListView.SelectedItems[0].Index);
                    ReorderList();
                } else
                    MessageBox.Show("Não é possível remover este local porque já houve recebimento neste local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelLocalButton_Click(object sender, EventArgs e) {
            LockForm(true);
            SetButtonState();
            LocalPanel.Hide();
            MainListView.Enabled = true;
        }

        private void OkLocalButton_Click(object sender, EventArgs e) {
            ListViewItem lvItem = new ListViewItem();
            lvItem.SubItems.Add("00");
            lvItem.SubItems.Add(CCustoList.SelectedValue.ToString());
            lvItem.SubItems.Add(CCustoList.Text);
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add("");
            lvItem.SubItems.Add("");
            if (MainListView.Items.Count == 1 || MainListView.Items.Count - 1 == MainListView.SelectedItems[0].Index)
                MainListView.Items.Add(lvItem);
            else
                MainListView.Items.Insert(MainListView.SelectedItems[0].Index + 1, lvItem);
            ReorderList();
            LockForm(true);
            SetButtonState();
            LocalPanel.Hide();
            MainListView.Enabled = true;

        }

        private void SairButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void CalculoDias() {
            TimeSpan t;
            double NumDias = 0;
            DateTime Data1;
            DateTime Data2;

            foreach (ListViewItem lvItem in MainListView.Items) {
                if (lvItem.Index == 0) {//Se for a primeira linha
                    if (lvItem.SubItems[4].Text != "") {//Se tiver Data de Entrada 
                        if (lvItem.SubItems[9].Text != "") {//Se tiver data de envio
                            Data1 = DateTime.Parse(lvItem.SubItems[4].Text);
                            Data2 = DateTime.Parse(lvItem.SubItems[9].Text);
                            t = (Data2 - Data1);
                            NumDias = t.TotalDays;
                        } else {//Não tem data de envio
                            Data1 = DateTime.Parse(lvItem.SubItems[4].Text);
                            Data2 = DateTime.Now;
                            t = (Data2 - Data1);
                            NumDias = t.TotalDays;
                        }
                    } else {//Não tem data de entrada
                        NumDias = 0;
                    }
                } else {//Se não for a primeira linha
                        //Data de Entrada será a data de envio da linha anterior 
                    if (MainListView.Items[lvItem.Index - 1].SubItems[9].Text != "") {
                        if (lvItem.SubItems[9].Text != "") {//Se tiver data de envio
                            Data1 = DateTime.Parse(MainListView.Items[lvItem.Index - 1].SubItems[9].Text);
                            Data2 = DateTime.Parse(lvItem.SubItems[9].Text);
                            t = (Data2 - Data1);
                            NumDias = t.TotalDays;
                        } else {//Não tem data de envio
                            Data1 = DateTime.Parse(MainListView.Items[lvItem.Index - 1].SubItems[9].Text);
                            Data2 = DateTime.Now;
                            t = (Data2 - Data1);
                            NumDias = t.TotalDays;
                        }
                    } else {//Não tem data de entrada
                        NumDias = 0;
                    }
                }
                if (lvItem.Index == MainListView.Items.Count - 1 && SitLabel.Text == "ARQUIVADO")
                    MainListView.Items[lvItem.Index].SubItems[8].Text = "0";
                else
                    MainListView.Items[lvItem.Index].SubItems[8].Text = (Math.Round(NumDias)).ToString();

                if (lvItem.SubItems[4].Text == "")
                    MainListView.Items[lvItem.Index].SubItems[8].Text = "";
            }
        }

        private void MainListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (MainListView.Items.Count > 0 && MainListView.SelectedItems.Count > 0)
                ObsText.Text = MainListView.SelectedItems[0].SubItems[11].Text.ToString();
        }

        private void ObsButton_Click(object sender, EventArgs e) {
            if (MainListView.Items.Count == 0 || MainListView.SelectedItems.Count == 0) return;

            if (String.IsNullOrEmpty(MainListView.SelectedItems[0].SubItems[2].Text)) {
                MessageBox.Show("Local ainda não tramitado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool bReadOnly = true;
            int CodCC = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[2].Text);
            List<UsuariocentroCusto> Lista = _protocoloRepository.Lista_CentroCusto_Usuario(_sistemaRepository.Retorna_User_LoginId(GtiCore.Retorna_Last_User()));
            foreach (UsuariocentroCusto item in Lista) {
                if (item.Codigo == CodCC) {
                    bReadOnly = false;
                    break;
                }
            }

            ZoomBox f1 = new ZoomBox("Observação do trâmite", this, ObsText.Text, bReadOnly);
            f1.ShowDialog();
            ObsText.Text = f1.ReturnText;

            if (!bReadOnly) {
                MainListView.SelectedItems[0].SubItems[11].Text = f1.ReturnText;
                int Ano = _protocoloRepository.Extract_Ano_Processo(NumProcLabel.Text);
                int Numero = _protocoloRepository.Extract_Numero_ProcessoNoDV(NumProcLabel.Text);
                int Seq = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[1].Text);
                Exception ex = _protocoloRepository.Alterar_Observacao_Tramite(Ano, Numero, Seq, ObsText.Text);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Erro!", ex.Message, ex);
                    eBox.ShowDialog();
                }
            }
        }

        private void AlterarButton_Click(object sender, EventArgs e) {
            if (MainListView.SelectedItems.Count == 0) {
                MessageBox.Show("Selecione o trâmite que deseja alterar.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bFechado) {
                MessageBox.Show("O processo não está aberto.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {
                bool bFind = false;
                int CodCC = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[2].Text);
                List<UsuariocentroCusto> Lista = _protocoloRepository.Lista_CentroCusto_Usuario(_sistemaRepository.Retorna_User_LoginId(GtiCore.Retorna_Last_User()));
                foreach (UsuariocentroCusto item in Lista) {
                    if (item.Codigo == CodCC) {
                        bFind = true;
                        break;
                    }
                }

                if (!bFind) {
                    MessageBox.Show("Você não pode alterar o despacho deste local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (String.IsNullOrEmpty(MainListView.SelectedItems[0].SubItems[7].Text)) {
                    MessageBox.Show("Este trâmite não possui despacho.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MainListView.Items.Count == 1)
                    goto Alterar;

                if (MainListView.Items.Count - 1 == MainListView.SelectedItems[0].Index)
                    goto Alterar;

                if (String.IsNullOrEmpty(MainListView.Items[MainListView.SelectedItems[0].Index + 1].SubItems[4].Text))
                    goto Alterar;
            }

            MessageBox.Show("Não é possível alterar o despacho porque já houve recebimento de processo no local abaixo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        Alterar:;
            GetButtonState();
            LockForm(false);
            MainListView.Enabled = false;
            DespachoPanel.Visible = true;
            DespachoPanel.BringToFront();
            String sDespacho = MainListView.SelectedItems[0].SubItems[7].Text;
            DespachoList.Text = sDespacho;
        }

        private void CancelDespachoButton_Click(object sender, EventArgs e) {
            LockForm(true);
            SetButtonState();
            DespachoPanel.Hide();
            MainListView.Enabled = true;
        }

        private void OKDespachoButton_Click(object sender, EventArgs e) {
            String sDespacho = MainListView.SelectedItems[0].SubItems[7].Text;
            if (sDespacho != DespachoList.Text) {
                if (MessageBox.Show("Alterar o despacho?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    int Ano = _protocoloRepository.Extract_Ano_Processo(NumProcLabel.Text);
                    int Numero = _protocoloRepository.Extract_Numero_ProcessoNoDV(NumProcLabel.Text);
                    int Seq = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[1].Text);
                    _protocoloRepository.Alterar_Despacho(Ano, Numero, Seq, Convert.ToInt16(DespachoList.SelectedValue));
                    MainListView.SelectedItems[0].SubItems[7].Text = DespachoList.Text;
                    LockForm(true);
                    SetButtonState();
                    DespachoPanel.Hide();
                    MainListView.Enabled = true;
                }
            }
        }

        private void ReceberButton_Click(object sender, EventArgs e) {
            if (MainListView.SelectedItems.Count == 0) {
                MessageBox.Show("Selecione o trâmite que deseja receber.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int nUserId = _sistemaRepository.Retorna_User_LoginId(Properties.Settings.Default.LastUser);

            if (bFechado) {
                MessageBox.Show("O processo não está aberto.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {
                bool bFind = false;
                int CodCC = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[2].Text);
                List<UsuariocentroCusto> Lista = _protocoloRepository.Lista_CentroCusto_Usuario(_sistemaRepository.Retorna_User_LoginId(GtiCore.Retorna_Last_User()));
                foreach (UsuariocentroCusto item in Lista) {
                    if (item.Codigo == CodCC) {
                        bFind = true;
                        break;
                    }
                }

                if (!bFind && nUserId != 433) {
                    MessageBox.Show("Você não pode trâmitar deste local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(MainListView.SelectedItems[0].SubItems[4].Text)) {
                    MessageBox.Show("Já houve recebimento neste local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MainListView.Items.Count == 1)
                    goto Receber;

                if (MainListView.Items.Count - 1 == MainListView.SelectedItems[0].Index)
                    goto Receber;

            }

            if (MainListView.SelectedItems[0].Index > 0 && string.IsNullOrEmpty(MainListView.Items[MainListView.SelectedItems[0].Index - 1].SubItems[9].Text)) {
                MessageBox.Show("O local anterior ainda não foi tramitado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        Receber:;

            GetButtonState();
            LockForm(false);
            MainListView.Enabled = false;
            EnvRecPanel.Visible = true;
            EnvRecPanel.BringToFront();
            EnvRecLabel.Text = "Recebimento de Processo";
            toolTip1.SetToolTip(OKEnvRecButton, "Receber processo");
            Despacho2List.SelectedIndex = -1;

            LocalLabel.Text = MainListView.SelectedItems[0].SubItems[3].Text;
            DataLabel.Text = GtiCore.Retorna_Data_Base_Sistema().ToString("dd/MM/yyyy");
            HoraLabel.Text = DateTime.Now.ToString("HH:mm");
            string sFuncionario = MainListView.SelectedItems[0].SubItems[6].Text;

            FuncionarioList.Items.Clear();
            string sNomeCompleto;
            if (MainListView.SelectedItems[0].SubItems[6].Text != "")
                sNomeCompleto = MainListView.SelectedItems[0].SubItems[6].Text;
            else
                sNomeCompleto = _sistemaRepository.Retorna_User_FullName(GtiCore.Retorna_Last_User());
            FuncionarioList.Items.Add(new CustomListBoxItem(sNomeCompleto, 999));
            List<UsuarioFuncStruct> ListaFunc = _protocoloRepository.Lista_Funcionario(nUserId);
            foreach (UsuarioFuncStruct item in ListaFunc) {
                FuncionarioList.Items.Add(new CustomListBoxItem(item.NomeCompleto, item.FuncLogin));
            }
            FuncionarioList.Text = sNomeCompleto;

        }

        private void CancelEnvRecButton_Click(object sender, EventArgs e) {
            LockForm(true);
            SetButtonState();
            EnvRecPanel.Hide();
            MainListView.Enabled = true;
        }

        private void OKEnvRecButton_Click(object sender, EventArgs e) {
            Exception ex = null;
            bool bReceber = EnvRecLabel.Text == "Recebimento de Processo" ? true : false;
            int Ano = _protocoloRepository.Extract_Ano_Processo(NumProcLabel.Text);
            int Numero = _protocoloRepository.Extract_Numero_ProcessoNoDV(NumProcLabel  .Text);
            int Seq = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[1].Text);
            short CCusto = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[2].Text);
            DateTime Data = Convert.ToDateTime(DataLabel.Text);
            DateTime Hora = Convert.ToDateTime(HoraLabel.Text);
            DateTime DataHora = new DateTime(Data.Year, Data.Month, Data.Day, Hora.Hour, Hora.Second, Hora.Second);
            short? CodDespacho = Despacho2List.SelectedIndex == -1 ? Convert.ToInt16(0) : Convert.ToInt16(Despacho2List.SelectedValue);

            Tramitacao reg = new Tramitacao {
                Ano = Convert.ToInt16(Ano),
                Numero = Numero,
                Seq = Convert.ToByte(Seq),
                Ccusto = CCusto,
                Datahora = DataHora,
                Despacho = CodDespacho == 0 ? null : CodDespacho
            };

            if (bReceber) {
                if (FuncionarioList.SelectedIndex == -1) {
                    MessageBox.Show("Selecione um funcionário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } else {
                    GtiTypes.CustomListBoxItem selectedItem = (GtiTypes.CustomListBoxItem)FuncionarioList.SelectedItem;
                    reg.Userid = selectedItem._value;
                    if (reg.Userid < 999)
                        reg.Userid = _sistemaRepository.Retorna_User_LoginId("F" + Convert.ToInt32(reg.Userid).ToString("000"));
                    else
                        reg.Userid = _sistemaRepository.Retorna_User_LoginId(GtiCore.Retorna_Last_User());

                    ex = _protocoloRepository.Excluir_Tramite(Ano, Numero, Seq);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Erro!", ex.Message, ex);
                        eBox.ShowDialog();
                    }
                    ex = _protocoloRepository.Incluir_Tramite(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Erro!", ex.Message, ex);
                        eBox.ShowDialog();
                    }
                }
            } else {
                if (CodDespacho == 0) {
                    MessageBox.Show("Selecione um despacho para o trâmite.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reg.Dataenvio = DataHora;
                GtiTypes.CustomListBoxItem selectedItem = (GtiTypes.CustomListBoxItem)FuncionarioList.SelectedItem;
                reg.Userid2 = selectedItem._value;

                if (reg.Userid2 < 999)
                    reg.Userid2 = _sistemaRepository.Retorna_User_LoginId("F" + Convert.ToInt32(reg.Userid2).ToString("000"));
                else
                    reg.Userid2 = _sistemaRepository.Retorna_User_LoginId(GtiCore.Retorna_Last_User());

                ex = _protocoloRepository.Alterar_Tramite(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Erro!", ex.Message, ex);
                    eBox.ShowDialog();
                }
            }

            CarregaTramite();
            LockForm(true);
            SetButtonState();
            EnvRecPanel.Hide();
            MainListView.Enabled = true;

        }

        private void EnviarButton_Click(object sender, EventArgs e) {
            if (MainListView.SelectedItems.Count == 0) {
                MessageBox.Show("Selecione o trâmite que deseja enviar.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bFechado) {
                MessageBox.Show("O processo não está aberto.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {
                bool bFind = false;
                int CodCC = Convert.ToInt16(MainListView.SelectedItems[0].SubItems[2].Text);
                int nUserId = _sistemaRepository.Retorna_User_LoginId(Properties.Settings.Default.LastUser);
                List<UsuariocentroCusto> Lista = _protocoloRepository.Lista_CentroCusto_Usuario(_sistemaRepository.Retorna_User_LoginId(GtiCore.Retorna_Last_User()));
                foreach (UsuariocentroCusto item in Lista) {
                    if (item.Codigo == CodCC) {
                        bFind = true;
                        break;
                    }
                }

                if (!bFind && nUserId != 433) {
                    MessageBox.Show("Você não pode trâmitar deste local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (String.IsNullOrEmpty(MainListView.SelectedItems[0].SubItems[4].Text)) {
                    MessageBox.Show("Ainda não houve recebimento deste local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MainListView.Items.Count == 1)
                    goto Enviar;

                if (MainListView.Items.Count - 1 == MainListView.SelectedItems[0].Index)
                    goto Enviar;

                if (MainListView.SelectedItems[0].Index > 0 && String.IsNullOrEmpty(MainListView.Items[MainListView.SelectedItems[0].Index - 1].SubItems[9].Text)) {
                    MessageBox.Show("O local anterior ainda não foi tramitado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        Enviar:;

            GetButtonState();
            LockForm(false);
            MainListView.Enabled = false;
            EnvRecPanel.Visible = true;
            EnvRecPanel.BringToFront();
            EnvRecLabel.Text = "Envio de Processo";
            if (MainListView.SelectedItems[0].SubItems[7].Text != "")
                Despacho2List.Text = MainListView.SelectedItems[0].SubItems[7].Text;
            else
                Despacho2List.SelectedIndex = -1;

            toolTip1.SetToolTip(OKEnvRecButton, "Enviar processo");
            LocalLabel.Text = MainListView.SelectedItems[0].SubItems[3].Text;
            DataLabel.Text = GtiCore.Retorna_Data_Base_Sistema().ToString("dd/MM/yyyy");
            HoraLabel.Text = DateTime.Now.ToString("HH:mm");
            String sFuncionario = MainListView.SelectedItems[0].SubItems[6].Text;

            FuncionarioList.Items.Clear();
            String sNomeCompleto;
            if (MainListView.SelectedItems[0].SubItems[6].Text != "")
                sNomeCompleto = MainListView.SelectedItems[0].SubItems[6].Text;
            else
                sNomeCompleto = _sistemaRepository.Retorna_User_FullName(GtiCore.Retorna_Last_User());
            FuncionarioList.Items.Add(new GtiTypes.CustomListBoxItem(sNomeCompleto, 999));
            List<UsuarioFuncStruct> ListaFunc = _protocoloRepository.Lista_Funcionario(_sistemaRepository.Retorna_User_LoginId(GtiCore.Retorna_Last_User()));
            foreach (UsuarioFuncStruct item in ListaFunc) {
                FuncionarioList.Items.Add(new GtiTypes.CustomListBoxItem(item.NomeCompleto, item.FuncLogin));
            }
            FuncionarioList.Text = sNomeCompleto;

        }
    }
}
