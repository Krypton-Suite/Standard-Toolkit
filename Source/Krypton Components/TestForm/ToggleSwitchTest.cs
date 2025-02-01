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
    public partial class ToggleSwitchTest : KryptonForm
    {
        public ToggleSwitchTest()
        {
            InitializeComponent();
        }

        private void ToggleSwitchTest_Load(object sender, EventArgs e)
        {
            kryptonWrapLabel1.Text = $@"Is toggle switch checked: {ktsTest.Checked}";
        }

        private void ktsTest_CheckedChanged(object sender, EventArgs e)
        {
            kryptonWrapLabel1.Text = $@"Is toggle switch checked: {ktsTest.Checked}";
        }
    }
}
