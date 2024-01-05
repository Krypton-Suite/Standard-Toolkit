using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class RibbonTest : KryptonForm
    {
        public RibbonTest()
        {
            InitializeComponent();
        }

        private void krcbAllowFormIntegrate_CheckedChanged(object sender, EventArgs e)
        {
            kryptonRibbon1.AllowFormIntegrate = krcbAllowFormIntegrate.Checked;
        }
    }
}
