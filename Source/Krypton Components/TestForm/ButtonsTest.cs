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
    public partial class ButtonsTest : KryptonForm
    {
        public ButtonsTest()
        {
            InitializeComponent();
        }

        private void kcbtnDropDown_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            kryptonButton3.Values.DropDownArrowColor = e.Color;

            kryptonButton4.Values.DropDownArrowColor = e.Color;

            kryptonButton7.Values.DropDownArrowColor = e.Color;

            kryptonButton8.Values.DropDownArrowColor = e.Color;
        }
    }
}
