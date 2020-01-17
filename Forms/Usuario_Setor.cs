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
    public partial class Usuario_Setor : Form {
        private readonly ISistemaRepository _sistemaRepository = new SistemaRepository(GtiCore.Connection_Name());
        private readonly IProtocoloRepository _protocoloRepository = new ProtocoloRepository();
        readonly string _connection = GtiCore.Connection_Name();
        public  int Id { get; set; }
        public Usuario_Setor() {
            InitializeComponent();
            SetorComboBox.DataSource = _protocoloRepository.Lista_Local(true, true);
            SetorComboBox.DisplayMember = "Descricao";
            SetorComboBox.ValueMember = "Codigo";
        }

        private void GravarButton_Click(object sender, EventArgs e) {
            UsuarioStruct cUser = _sistemaRepository.Retorna_Usuario(Id);
            Usuario reg = new Usuario {
                Id = cUser.Id,
                Nomecompleto = cUser.Nome_completo,
                Nomelogin = cUser.Nome_login,
                Ativo = cUser.Ativo,
                Setor_atual = Convert.ToInt32(SetorComboBox.SelectedValue)
            };
            Exception ex = _sistemaRepository.Alterar_Usuario(reg);
            if (ex == null) {
                DialogResult = DialogResult.OK;
            } else {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }

        private void CancelarButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
