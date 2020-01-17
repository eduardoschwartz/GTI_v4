using GTI_v4.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Main : Form {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams {
            get {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle |= CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public Main() {
            InitializeComponent();
            this.DoubleBuffered = true;
            DateTimePicker t = new DateTimePicker {
                AutoSize = false,
                Width = 100,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Name = "sbDataBase",
            };
            TopBarToolStrip.Renderer = new MySR();
            t.CloseUp += new System.EventHandler(SbDataBase_CloseUp);

            BarStatus.Items.Insert(17, new ToolStripControlHost(t));
            MaquinaToolStripStatus.Text = Environment.MachineName;
            DataBaseToolStripStatus.Text = Properties.Settings.Default.DataBaseReal;
            
        }

        private void SbDataBase_CloseUp(object sender, EventArgs e) {
            MessageBox.Show(ReturnDataBaseValue().ToString());
        }

        public DateTime ReturnDataBaseValue() {
            DateTime dValue = DateTime.Today;

            DateTimePicker dbox;
            foreach (Control c in BarStatus.Controls) {
                if (c is DateTimePicker) {
                    dbox = c as DateTimePicker;
                    dValue = dbox.Value.Date;
                }
            }
            return dValue;
        }

        private void Main_Load(object sender, EventArgs e) {
            IsMdiContainer = true;
            CorFundo();
            Refresh();
            
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            ServidorToolStripStatus.Text = Properties.Settings.Default.ServerName;
            
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            VersaoToolStripStatus.Text = $"{version.Major}" + "." + $"{version.Minor}" + "." + $"{version.Build}";
            Text += VersaoToolStripStatus.Text;

            LockForm(true);
            Forms.Login login = new Forms.Login();
            login.ShowDialog();

        }

        public void LockForm(bool bLocked) {
            //foreach (ToolStripItem ts in TopBarToolStrip.Items) {
            //    ts.Enabled = !bLocked;
            //}

            //List<ToolStripMenuItem> mItems = GtiCore.GetItems(this.MenuBarStrip);
            //foreach (var item in mItems) {
            //    item.Enabled = !bLocked;
            //}
            Dv1Option.Enabled = !bLocked;
            Dv2Option.Enabled = !bLocked;
            DVText.ReadOnly = bLocked;
            DVLabel.Enabled = !bLocked;
            lblDV2.Enabled = !bLocked;
            DV3label.Enabled = !bLocked;
        }

        private void CorFundo() {
            MdiClient ctlMDI;

            foreach (Control ctl in this.Controls) {
                try {
                    ctlMDI = (MdiClient)ctl;
                    ctlMDI.BackColor = Color.LightSteelBlue;

                } catch {
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            Application.Exit();
        }

        private void SairButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Deseja fechar todas as janelas e retornar a tela de login?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Form[] charr = this.MdiChildren;
                foreach (Form chform in charr) {
                    chform.Close();
                }
                LockForm(true);
                
                Forms.Login login = new Forms.Login();
                login.ShowDialog();
            }
        }

        private void ConfigMenu_Click(object sender, EventArgs e) {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Config);
            if (formToShow != null) {
                formToShow.Show();
            } else {
                Forms.Config f1 = new Forms.Config {
                    Tag = "Menu",
                    MdiParent = this
                };
                f1.Show();
                f1.BringToFront();
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e) {
            ConfigMenu_Click(sender, e);
        }
    }
}
