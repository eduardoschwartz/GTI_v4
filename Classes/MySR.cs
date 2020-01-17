using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTI_v4.Classes {
    public class MySR : ToolStripSystemRenderer {
        public MySR() { }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
            if (e.ToolStrip.GetType() == typeof(ToolStrip)) {
                // skip render border
            } else {
                // do render border
                base.OnRenderToolStripBorder(e);
            }
        }
    }
}
