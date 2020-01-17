using GTI_v4.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GTI_v4.Forms {
    public partial class Config : Form {
        public Config() {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e) {
            Size = new Size(Properties.Settings.Default.Form_Config_Size.Width, Properties.Settings.Default.Form_Config_Size.Height);
            Location = new Point(Properties.Settings.Default.Form_Config_Location.X, Properties.Settings.Default.Form_Config_Location.Y);
            FillGrid();
        }

        private void FillGrid() {
            GtiConfig Pms = new GtiConfig {
                PathApp = AppDomain.CurrentDomain.BaseDirectory,
                DataBaseReal = Properties.Settings.Default.DataBaseReal,
                DataBaseTeste = Properties.Settings.Default.DataBaseTeste
            };
            Pms.PathReport = Pms.PathApp + "\\report";
            Pms.PathAnexo = Properties.Settings.Default.Path_Anexo;
            Pms.ServerName = Properties.Settings.Default.ServerName;
            Pms.ComputerName = Environment.MachineName;
            Pms.UserName = GtiCore.Retorna_Last_User();
            pGrid.SelectedObject = Pms;
        }

        private void pGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            if (e.OldValue != e.ChangedItem.Value) {
                switch (e.ChangedItem.Label) {
                    case "Servidor de Dados":
                        Properties.Settings.Default.ServerName = e.ChangedItem.Value.ToString();

                        Main f1 = (Main)Application.OpenForms["Main"];
                        f1.ServidorToolStripStatus.Text = e.ChangedItem.Value.ToString();
                        break;
                    default:
                        break;
                }
                Properties.Settings.Default.Save();
                FillGrid();
            }
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e) {
            Properties.Settings.Default.Form_Config_Size = Size;
            Properties.Settings.Default.Form_Config_Location = Location;
            Properties.Settings.Default.Save();
        }
    }
}
