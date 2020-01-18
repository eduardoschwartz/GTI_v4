using GTI_v4.Classes;
using System;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class InputBox : Form {
        private GtiCore.ETweakMode eMode;
        private int eDecimal = 2;

        public InputBox() {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e) {
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e) {
            TxtInput.Text = "";
            Close();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                cmdOK.PerformClick();
            else if (e.KeyCode == Keys.Escape)
                cmdCancel.PerformClick();
        }

        public string Show(string previewInput, string Title, string TheMessage) {
//            eMode = gtiCore.eTweakMode.Normal;
            this.Text = Title;
            lblTitulo.Text = TheMessage;
            TxtInput.Text = previewInput;
            base.ShowDialog();
            return TxtInput.Text;
        }

        public string Show(string previewInput, string Title, string TheMessage, int length) {
            //eMode = gtiCore.eTweakMode.Normal;
            if (length > 0) TxtInput.MaxLength = length;
            this.Text = Title;
            lblTitulo.Text = TheMessage;
            TxtInput.Text = previewInput;
            base.ShowDialog();
            return TxtInput.Text;
        }
        public string Show(string previewInput, string Title, string TheMessage, int length, GtiCore.ETweakMode Mode) {
            eMode = Mode;
            if (length > 0) TxtInput.MaxLength = length;
            this.Text = Title;
            lblTitulo.Text = TheMessage;
            TxtInput.Text = previewInput;
            base.ShowDialog();
            return TxtInput.Text;
        }
        public string Show(string previewInput, string Title, string TheMessage, int length, GtiCore.ETweakMode Mode, int decimalNumber) {
            eMode = Mode;
            eDecimal = decimalNumber;
            if (length > 0) TxtInput.MaxLength = length;
            this.Text = Title;
            lblTitulo.Text = TheMessage;
            TxtInput.Text = previewInput;
            base.ShowDialog();
            return TxtInput.Text;
        }

        public new string Show() {
            TxtInput.Text = "";
            base.ShowDialog();
            return TxtInput.Text;
        }
        public new string ShowDialog() {
            return this.Show();
        }
        public new string Show(IWin32Window owner) {
            TxtInput.Text = "";
            base.ShowDialog(owner);
            return TxtInput.Text;
        }
        public new string ShowDialog(IWin32Window owner) {
            return this.Show(owner);
        }

        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e) {
            e.KeyChar = GtiCore.Tweak(TxtInput, e.KeyChar, eMode, eDecimal);
            if (e.KeyChar == '\0')
                e.Handled = true;
        }
    }
}
