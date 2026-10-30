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
    public partial class ProgressBarTriStateTest : KryptonForm
    {
        public ProgressBarTriStateTest()
        {
            InitializeComponent();
        }

        private void kryptonTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            kryptonProgressBar1.Value = kryptonTrackBar1.Value;
        }
    }
}
