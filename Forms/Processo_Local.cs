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

namespace GTI_v4.Forms {
    public partial class Processo_Local : Form {
        IProtocoloRepository protocoloRepository = new ProtocoloRepository(GtiCore.Connection_Name());
        List<Centrocusto> Lista;
        bool bAddNew;
        
        public Processo_Local() {
            InitializeComponent();
            ControlBehaviour(true);
            Lista = protocoloRepository.Lista_Local(false, false);
            TreeNode root = null;
            var departments = Lista;
            PopulateTree(ref root, departments);
            tvMain.Nodes.Add(root);
            tvMain.ExpandAll();
            tvMain.Nodes[0].EnsureVisible();
        }

        public void PopulateTree(ref TreeNode root, List<Centrocusto> departments) {
            if (root == null) {
                root = new TreeNode {
                    Text = "LOCAIS DE TRAMITAÇÃO",
                    ForeColor = System.Drawing.Color.Red,
                    Tag = null
                };
                var details = departments.Where(t => t.Vinculo == 0);
                foreach (var detail in details) {
                    var child = new TreeNode() {
                        Text = detail.Descricao.ToUpper(),
                        Tag = detail.Codigo,
                    };
                    child.ForeColor = System.Drawing.Color.Blue;
                    PopulateTree(ref child, departments);
                    root.Nodes.Add(child);
                }
            } else {
                var id = (short)root.Tag;
                var details = departments.Where(t => t.Vinculo == id);
                foreach (var detail in details) {
                    var child = new TreeNode() {
                        Text = detail.Descricao.ToUpper(),
                        Tag = detail.Codigo,
                    };
                    PopulateTree(ref child, departments);
                    root.Nodes.Add(child);
                }
            }
        }

        private void tvMain_AfterSelect(object sender, TreeViewEventArgs e) {
            Clear_Reg();
            if (tvMain.Nodes[0].IsSelected) {
                btEdit.Enabled = false;
                btDel.Enabled = false;
            } else {
                btEdit.Enabled = true;
                btDel.Enabled = true;
            }
            TreeNode tParent = e.Node.Parent;
            txtVinculo.Tag = tParent == null || tParent.Tag == null ? "-1" : tParent.Tag.ToString();
            txtVinculo.Text = tParent == null ? "LOCAIS DE TRAMITAÇÃO" : tParent.Text;
            txtDescricao.Text = tvMain.SelectedNode.Text;
            txtDescricao.Tag = tvMain.SelectedNode.Tag == null ? "0" : tvMain.SelectedNode.Tag.ToString();

            for (int i = 0; i < Lista.Count; i++) {
                if (Lista[i].Codigo == Convert.ToInt16(txtDescricao.Tag.ToString())) {
                    txtFone.Text = Lista[i].Telefone;
                    chkAtivo.Checked = Lista[i].Ativo;
                    break;
                }
            }
        }

        private void ControlBehaviour(bool bStart) {
            btAdd.Enabled = bStart;
            btEdit.Enabled = bStart;
            btDel.Enabled = bStart;
            btExit.Enabled = bStart;
            btGravar.Enabled = !bStart;
            btCancelar.Enabled = !bStart;
            txtVinculo.ReadOnly = true;
            txtFone.ReadOnly = bStart;
            txtDescricao.ReadOnly = bStart;
            chkAtivo.Enabled = !bStart;
            tvMain.Enabled = bStart;
        }

        private void btAdd_Click(object sender, EventArgs e) {
            bAddNew = true;
            Clear_Reg();
            ControlBehaviour(false);
            txtDescricao.Focus();
        }

        private void btEdit_Click(object sender, EventArgs e) {
            bAddNew = false;
            ControlBehaviour(false);
            txtDescricao.Focus();
        }

        private void btDel_Click(object sender, EventArgs e) {
            int nCodigo = Convert.ToInt32(txtDescricao.Tag);
            if (nCodigo == 0)
                MessageBox.Show("Selecione um local.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                if (MessageBox.Show("Excluir este registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    protocoloRepository.Excluir_Local(nCodigo);
                    tvMain.SelectedNode.Remove();
                    tvMain.Nodes[0].EnsureVisible();
                    Clear_Reg();
                }
            }
        }

        private void btGravar_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Gravar os dados?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Centrocusto reg = new Centrocusto {
                    Vinculo = Convert.ToInt16(txtVinculo.Tag),
                    Codigo = Convert.ToInt16(txtDescricao.Tag),
                    Descricao = txtDescricao.Text,
                    Telefone = txtFone.Text,
                    Ativo = chkAtivo.Checked
                };

                Exception ex;

                if (bAddNew) {
                    short nLastCod = protocoloRepository.Retorna_Ultimo_Codigo_Local();
                    reg.Vinculo = Convert.ToInt16(txtDescricao.Tag);
                    reg.Codigo = Convert.ToInt16(nLastCod + 1);
                    ex = protocoloRepository.Incluir_Local(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                        eBox.ShowDialog();
                    } else {
                        var child = new TreeNode() {
                            Text = txtDescricao.Text.ToUpper(),
                            Tag = (nLastCod + 1).ToString(),
                        };
                        tvMain.SelectedNode.Nodes.Add(child);
                        Lista.Add(reg);
                        ControlBehaviour(true);
                    }
                } else {
                    ex = protocoloRepository.Alterar_Local(reg);
                    if (ex != null) {
                        ErrorBox eBox = new ErrorBox("Atenção", ex.Message, ex);
                        eBox.ShowDialog();
                    } else {
                        tvMain.SelectedNode.Text = txtDescricao.Text.ToUpper();
                        for (int i = 0; i < Lista.Count; i++) {
                            if (Lista[i].Codigo == Convert.ToInt32(txtDescricao.Tag.ToString())) {
                                Lista[i].Telefone = txtFone.Text;
                                Lista[i].Ativo = chkAtivo.Checked;
                                break;
                            }
                        }
                        ControlBehaviour(true);
                    }
                }
            }
        }

        private void btExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void btCancelar_Click(object sender, EventArgs e) {
            ControlBehaviour(true);
        }

        private void Clear_Reg() {
            txtDescricao.Text = "";
            txtFone.Text = "";
            chkAtivo.Checked = false;
        }


    }
}
