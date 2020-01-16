using GTI_v4.Classes;
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
            Refresh();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            ServidorToolStripStatus.Text = Properties.Settings.Default.ServerName;

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            VersaoToolStripStatus.Text = $"{version.Major}" + "." + $"{version.Minor}" + "." + $"{version.Build}";
            this.Text += VersaoToolStripStatus.Text;

            LockForm(true);
            //Forms.Login login = new Forms.Login();
            //login.ShowDialog();

        }

        public void LockForm(bool bLocked) {
            foreach (ToolStripItem ts in TopBarToolStrip.Items) {
                ts.Enabled = !bLocked;
            }

            List<ToolStripMenuItem> mItems = GtiCore.GetItems(this.MenuBarStrip);
            foreach (var item in mItems) {
                item.Enabled = !bLocked;
            }
            Dv1Option.Enabled = !bLocked;
            Dv2Option.Enabled = !bLocked;
            DVText.ReadOnly = bLocked;
            DVLabel.Enabled = !bLocked;
            lblDV2.Enabled = !bLocked;
            DV3label.Enabled = !bLocked;
        }



    }
}
