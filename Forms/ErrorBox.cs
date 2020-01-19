using GTI_v4.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class ErrorBox : Form {
        public ErrorBox(string Title, string Message, Exception exception) {
            InitializeComponent();
            Text = Title;
            txtMsg.Text = Message;
            if (exception.InnerException != null)
                txtDetail.Text = exception.InnerException.InnerException.ToString();
            else
                txtDetail.Text = "";
        }

        private void btOk_Click(object sender, EventArgs e) {
            Close();
        }

        private void btDetail_Click(object sender, EventArgs e) {
            if (Size.Height < 300) {
                Size = new Size(400, 330);
                btDetail.Image = Resources.Acima;
            } else {
                Size = new Size(400, 147);
                btDetail.Image = Resources.Abaixo;
            }
        }

        private void ErrorBox_Load(object sender, EventArgs e) {
            btDetail.Image = Resources.Abaixo;
        }
    }
}
