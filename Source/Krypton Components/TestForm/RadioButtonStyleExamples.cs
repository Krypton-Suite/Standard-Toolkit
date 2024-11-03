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
    public partial class RadioButtonStyleExamples : KryptonForm
    {
        public RadioButtonStyleExamples()
        {
            InitializeComponent();
        }

        private void RadioButtonStyleExamples_Load(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"RadioButton styles for theme: {kryptonThemeComboBox1.Text}";

            // RadioButton fixed states
            rbFocus.SetFixedState(true, true, false, false);
            rbCheckedDisabled.SetFixedState(false, false, false, false);
            rbCheckedNormal.SetFixedState(false, true, false, false);
            rbCheckedTracking.SetFixedState(false, true, true, false);
            rbCheckedPressed.SetFixedState(false, true, false, true);
            rbUncheckedDisabled.SetFixedState(false, false, false, false);
            rbUncheckedNormal.SetFixedState(false, true, false, false);
            rbUncheckedTracking.SetFixedState(false, true, true, false);
            rbUncheckedPressed.SetFixedState(false, true, false, true);
        }

        private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"RadioButton styles for theme: {kryptonThemeComboBox1.Text}";
        }
    }
}
