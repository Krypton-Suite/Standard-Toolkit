using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class KryptonMenuAndToolStripExampleForm : KryptonForm
    {
        public KryptonMenuAndToolStripExampleForm()
        {
            InitializeComponent();

            var hint = new KryptonLabel
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 48,
                Padding = new Padding(8)
            };
            hint.Values.Text =
                "KryptonMenuStrip (MainMenuStrip) with a KryptonToolStrip hosted in KryptonToolStripContainer. " +
                "Use Insert Standard Items in the designer. Change the global theme from another demo or KryptonManager to confirm colours and fonts follow the palette.";
            kryptonToolStripContainer1.ContentPanel.Controls.Add(hint);
        }
    }
}
