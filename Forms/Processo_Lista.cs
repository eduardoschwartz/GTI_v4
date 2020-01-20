using GTI_v4.Models;
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
    public partial class Processo_Lista : Form {
        public ProcessoNumero ReturnValue { get; set; } = new ProcessoNumero();

        public Processo_Lista() {
            InitializeComponent();
        }
    }
}
