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
    public partial class DateTimeExample : Form
    {
        public DateTimeExample()
        {
            InitializeComponent();
        }

        private void kryptonColorButton1_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            kryptonDateTimePicker1.StateCommon.Back.Color1 = e.Color;
        }
    }
}
