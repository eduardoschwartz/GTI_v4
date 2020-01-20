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

    public partial class ZoomBox : Form {
        private Form FormCalled { get; set; }
        public string ReturnText;

        public ZoomBox(String Titulo, Form FormCalling, string Texto, bool ReadOnly, int MaxLength = 0) {
            InitializeComponent();
            Text = Titulo;
            Location = new Point(FormCalling.Location.X + (FormCalling.Width - this.Width) / 2, FormCalling.Location.Y + (FormCalling.Height - this.Height) + 55 / 2);
            FormCalled = FormCalling;
            if (MaxLength > 0)
                txtZoom.MaxLength = MaxLength;
            txtZoom.Text = Texto;
            txtZoom.ReadOnly = ReadOnly;
            if (ReadOnly)
                tBar.Focus();
        }

        private void ZoomBox_FormClosed(object sender, FormClosedEventArgs e) {
            ReturnText = txtZoom.Text;
        }

        private void btSair_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
