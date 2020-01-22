using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using GTI_v4.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static GTI_v4.Classes.GtiTypes;

namespace GTI_v4.Forms {
    public partial class Processo_Assunto : Form {
        IProtocoloRepository protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        bool bCheck, bSoAtivo, bSoNAtivo;
        int _oldIndex;
        int hoveredIndex = -1;

        public Processo_Assunto() {
            InitializeComponent();
            bCheck = false;
            bSoAtivo = false;
            bSoNAtivo = false;
            Carrega_Lista(bSoAtivo, bSoNAtivo);
            Carrega_Local();
            Carrega_Doc();
        }

        private void Carrega_Lista(bool Somente_Ativo, bool Somente_inativo) {
            MainCheckedListBox.Items.Clear();
            GtiCore.Ocupado(this);
            List<Assunto> lista = protocoloRepository.Lista_Assunto(Somente_Ativo, Somente_inativo, FilterText.Text);
            MainCheckedListBox.Sorted = false;
            foreach (Assunto item in lista) {
                MainCheckedListBox.Items.Add(new GtiTypes.CustomListBoxItem2(item.Nome.ToString(), item.Codigo, item.Ativo));
                MainCheckedListBox.SetItemChecked(MainCheckedListBox.Items.Count - 1, item.Ativo);
            }
            MainCheckedListBox.Sorted = true;
            if (MainCheckedListBox.Items.Count > 0) {
                MainCheckedListBox.SelectedIndex = 0;
                MainCheckBoxList_SelectedIndexChanged(null, null);
            } else {
                Doc2List.Items.Clear();
                CC2List.Items.Clear();
            }

            GtiCore.Liberado(this);
        }

        private void Carrega_Local() {
            CC1List.Items.Clear();
            GtiCore.Ocupado(this);
            List<Centrocusto> lista = protocoloRepository.Lista_Local(true, false);
            foreach (Centrocusto item in lista) {
                CC1List.Items.Add(new GtiTypes.CustomListBoxItem(item.Descricao.ToString(), item.Codigo));
            }
            GtiCore.Liberado(this);
        }

        private void Carrega_Doc() {
            Doc1List.Items.Clear();
            GtiCore.Ocupado(this);
            List<Documento> lista = protocoloRepository.Lista_Documento();
            foreach (Documento item in lista) {
                Doc1List.Items.Add(new GtiTypes.CustomListBoxItem(item.Nome.ToString(), item.Codigo));
            }
            GtiCore.Liberado(this);
        }

        private void Carrega_Assunto_Local(short Assunto) {
            CC2List.Items.Clear();
            GtiCore.Ocupado(this);
            List<AssuntoLocal> lista = protocoloRepository.Lista_Assunto_Local(Assunto);
            CC2List.Sorted = false;
            foreach (AssuntoLocal item in lista) {
                CC2List.Items.Add(new GtiTypes.CustomListBoxItem(item.Nome.ToString(), item.Codigo));
            }
            GtiCore.Liberado(this);
        }

        private void Carrega_Assunto_Documento(short Assunto) {
            Doc2List.Items.Clear();
            GtiCore.Ocupado(this);
            List<AssuntoDocStruct> lista = protocoloRepository.Lista_Assunto_Documento(Assunto);
            foreach (AssuntoDocStruct item in lista) {
                Doc2List.Items.Add(new GtiTypes.CustomListBoxItem(item.Nome.ToString(), item.Codigo));
            }
            GtiCore.Liberado(this);
        }

        private void AddButton_Click(object sender, EventArgs e) {
            InputBox iBox = new InputBox();
            String sCod = iBox.Show("", "Informação", "Digite o nome do assunto.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Assunto reg = new Assunto {
                    Ativo = true,
                    Nome = sCod.ToUpper()
                };
                Exception ex = protocoloRepository.Incluir_Assunto(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Assunto já cadastrado.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista(bSoAtivo, bSoNAtivo);
            }
        }

        private void EditButton_Click(object sender, EventArgs e) {
            if (MainCheckedListBox.SelectedItem == null) return;
            InputBox iBox = new InputBox();
            String sCod = iBox.Show(MainCheckedListBox.Text, "Informação", "Digite o nome do assunto.", 40);
            if (!string.IsNullOrEmpty(sCod)) {
                Assunto reg = new Assunto();
                GtiTypes.CustomListBoxItem2 selectedData = (GtiTypes.CustomListBoxItem2)MainCheckedListBox.SelectedItem;
                reg.Codigo = Convert.ToInt16(selectedData._value);
                reg.Nome = sCod.ToUpper();
                reg.Ativo = Convert.ToBoolean(selectedData._ativo);
                Exception ex = protocoloRepository.Alterar_Assunto(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", "Assunto já cadastrado.", ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista(bSoAtivo, bSoNAtivo);
            }
        }

        private void DelButton_Click(object sender, EventArgs e) {
            if (MainCheckedListBox.SelectedItem == null) return;
            if (MessageBox.Show("Excluir este assunto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Assunto reg = new Assunto();
                CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
                reg.Codigo = Convert.ToInt16(selectedData._value);
                Exception ex = protocoloRepository.Excluir_Assunto(reg);
                if (ex != null) {
                    ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                    eBox.ShowDialog();
                } else
                    Carrega_Lista(bSoAtivo, bSoNAtivo);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void MainCheckBoxList_DoubleClick(object sender, EventArgs e) {
            if (MainCheckedListBox.SelectedItem == null) return;
            Editbutton.PerformClick();
        }

        private void AtivarButton_Click(object sender, EventArgs e) {
            if (MainCheckedListBox.SelectedItem == null) return;
            bCheck = true;
            MainCheckedListBox.SetItemChecked(MainCheckedListBox.SelectedIndex, !MainCheckedListBox.GetItemChecked(MainCheckedListBox.SelectedIndex));

            Assunto reg = new Assunto();
            GtiTypes.CustomListBoxItem2 selectedData = (GtiTypes.CustomListBoxItem2)MainCheckedListBox.SelectedItem;
            reg.Codigo = Convert.ToInt16(selectedData._value);
            reg.Nome = selectedData._name;
            reg.Ativo = selectedData._ativo;
            Exception ex = protocoloRepository.Alterar_Assunto(reg);
            if (ex != null) {
                ErrorBox eBox = new ErrorBox("Atenção", "Erro desconhecido.", ex);
                eBox.ShowDialog();

            }
        }

        private void MainCheckBoxList_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (bCheck) {
                bCheck = false;
                return;
            }
            if (MainCheckedListBox.SelectedItem == null) return;
            e.NewValue = e.CurrentValue;
        }

        private void SomenteOsInativosToolStripMenuItem_Click(object sender, EventArgs e) {
            bSoAtivo = false; bSoNAtivo = true;
            Carrega_Lista(bSoAtivo, bSoNAtivo);
        }

        private void FilterButton_Click(object sender, EventArgs e) {
            Carrega_Lista(bSoAtivo, bSoNAtivo);
        }

        private void FilterText_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter)
                FilterButton_Click(null, null);
        }

        private void CC2List_MouseDown(object sender, MouseEventArgs e) {
            if (CC2List.SelectedItem == null) return;
            _oldIndex = CC2List.SelectedIndex;
            CC2List.DoDragDrop(CC2List.SelectedItem, DragDropEffects.Move);
        }

        private void CC2List_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        private void CC1Button_Click(object sender, EventArgs e) {
            if (MainCheckedListBox.SelectedItem == null)
                MessageBox.Show("Selecione um assunto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
                bool bAtivo = Convert.ToBoolean(selectedData._ativo);
                if (bAtivo) {
                    if (CC1List.SelectedItem == null)
                        MessageBox.Show("Selecione o local que deseja incluir na tramitação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        CustomListBoxItem selectedItem = (CustomListBoxItem)CC1List.SelectedItem;
                        CC2List.Items.Add(new CustomListBoxItem(selectedItem._name, selectedItem._value));
                        Atualizar_Local();
                    }
                } else
                    MessageBox.Show("Apenas assuntos ativos podem ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CC2Button_Click(object sender, EventArgs e) {
            GtiTypes.CustomListBoxItem2 selectedData = (GtiTypes.CustomListBoxItem2)MainCheckedListBox.SelectedItem;
            bool bAtivo = Convert.ToBoolean(selectedData._ativo);
            if (bAtivo) {
                if (CC2List.SelectedItem == null)
                    MessageBox.Show("Selecione o local que deseja excluir da tramitação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    _oldIndex = CC2List.SelectedIndex;
                    CC2List.Items.RemoveAt(_oldIndex);
                    Atualizar_Local();
                }
            } else
                MessageBox.Show("Apenas assuntos ativos podem ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CC2List_DragDrop(object sender, DragEventArgs e) {
            CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
            bool bAtivo = Convert.ToBoolean(selectedData._ativo);
            if (bAtivo) {
                Point point = CC2List.PointToClient(new Point(e.X, e.Y));
                int index = CC2List.IndexFromPoint(point);
                if (index < 0) index = CC2List.Items.Count - 1;
                object data = e.Data.GetData(typeof(CustomListBoxItem));
                CC2List.Items.RemoveAt(_oldIndex);
                CC2List.Items.Insert(index, data);
                Atualizar_Local();
            } else
                MessageBox.Show("Apenas assuntos ativos podem ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Doc1Button_Click(object sender, EventArgs e) {
            if (MainCheckedListBox.SelectedItem == null)
                MessageBox.Show("Selecione um assunto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
                bool bAtivo = Convert.ToBoolean(selectedData._ativo);
                if (bAtivo) {
                    if (Doc1List.SelectedItem == null)
                        MessageBox.Show("Selecione o documento que deseja incluir na tramitação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else {
                        bool bFind = false;
                        CustomListBoxItem selectedItem = (CustomListBoxItem)Doc1List.SelectedItem;
                        foreach (CustomListBoxItem item in Doc2List.Items) {
                            if (item._value == selectedItem._value) {
                                bFind = true;
                                break;
                            }
                        }
                        if (bFind)
                            MessageBox.Show("Documento já incluso no assunto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else {
                            Doc2List.Items.Add(new CustomListBoxItem(selectedItem._name, selectedItem._value));
                            Atualizar_Documento();
                        }
                    }
                } else
                    MessageBox.Show("Apenas assuntos ativos podem ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Doc2Button_Click(object sender, EventArgs e) {
            CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
            bool bAtivo = Convert.ToBoolean(selectedData._ativo);
            if (bAtivo) {
                if (Doc2List.SelectedItem == null)
                    MessageBox.Show("Selecione o documento que deseja excluir da tramitação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    _oldIndex = Doc2List.SelectedIndex;
                    Doc2List.Items.RemoveAt(_oldIndex);
                    Atualizar_Documento();
                }
            } else
                MessageBox.Show("Apenas assuntos ativos podem ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainCheckBoxList_SelectedIndexChanged(object sender, EventArgs e) {
            if (MainCheckedListBox.SelectedItems.Count > 0) {
                CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
                Carrega_Assunto_Local(Convert.ToInt16(selectedData._value));
                Carrega_Assunto_Documento(Convert.ToInt16(selectedData._value));
            }
        }

        private void Doc2List_MouseMove(object sender, MouseEventArgs e) {
            int newHoveredIndex = Doc2List.IndexFromPoint(e.Location);
            if (hoveredIndex != newHoveredIndex) {
                hoveredIndex = newHoveredIndex;
                if (hoveredIndex > -1) {
                    tTp.Active = false;
                    tTp.SetToolTip(Doc2List, ((CustomListBoxItem)Doc2List.Items[hoveredIndex])._name);
                    tTp.Active = true;
                }
            }
        }

        private void CC2List_MouseMove(object sender, MouseEventArgs e) {
            int newHoveredIndex = CC2List.IndexFromPoint(e.Location);
            if (hoveredIndex != newHoveredIndex) {
                hoveredIndex = newHoveredIndex;
                if (hoveredIndex > -1) {
                    tTp.Active = false;
                    tTp.SetToolTip(CC2List, ((CustomListBoxItem)CC2List.Items[hoveredIndex])._name);
                    tTp.Active = true;
                }
            }
        }

        private void MainCheckBoxList_MouseMove(object sender, MouseEventArgs e) {
            int newHoveredIndex = MainCheckedListBox.IndexFromPoint(e.Location);
            if (hoveredIndex != newHoveredIndex) {
                hoveredIndex = newHoveredIndex;
                if (hoveredIndex > -1) {
                    tTp.Active = false;
                    tTp.SetToolTip(MainCheckedListBox, ((GtiTypes.CustomListBoxItem2)MainCheckedListBox.Items[hoveredIndex])._name);
                    tTp.Active = true;
                }
            }
        }

        private void ExibirTodosOsAssuntosToolStripMenuItem_Click(object sender, EventArgs e) {
            bSoAtivo = false; bSoNAtivo = false;
            Carrega_Lista(bSoAtivo, bSoNAtivo);
        }

        private void SomenteOsAtivosToolStripMenuItem_Click(object sender, EventArgs e) {
            bSoAtivo = true; bSoNAtivo = false;
            Carrega_Lista(bSoAtivo, bSoNAtivo);
        }

        private void Atualizar_Local() {
            CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
            int CodAssunto = Convert.ToInt16(selectedData._value);

            List<Assuntocc> Lista = new List<Assuntocc>();
            int x = 1;
            foreach (CustomListBoxItem item in CC2List.Items) {
                Assuntocc reg = new Assuntocc {
                    Codassunto = Convert.ToInt16(CodAssunto),
                    Codcc = Convert.ToInt16(item._value),
                    Seq = Convert.ToInt16(x)
                };
                Lista.Add(reg);
                x++;
            }
            Exception ex = protocoloRepository.Incluir_Assunto_Local(Lista);
        }

        private void Atualizar_Documento() {
            CustomListBoxItem2 selectedData = (CustomListBoxItem2)MainCheckedListBox.SelectedItem;
            short CodAssunto = Convert.ToInt16(selectedData._value);

            List<Assuntodoc> Lista = new List<Assuntodoc>();
            foreach (CustomListBoxItem item in Doc2List.Items) {
                Assuntodoc reg = new Assuntodoc {
                    Codassunto = CodAssunto,
                    Coddoc = Convert.ToInt16(item._value)
                };
                Lista.Add(reg);
            }
            Exception ex = protocoloRepository.Incluir_Assunto_Documento(Lista);
        }
                                            
    }
}
