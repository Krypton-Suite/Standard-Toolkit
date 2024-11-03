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
    public partial class CheckBoxStyleExamples : KryptonForm
    {
        public CheckBoxStyleExamples()
        {
            InitializeComponent();
        }

        private void CheckBoxStyleExamples_Load(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"CheckBox styles for theme: {kryptonThemeComboBox1.Text}";

            // CheckBox fixed states
            cbFocus.SetFixedState(true, true, false, false);
            cbUncheckedDisabled.SetFixedState(false, false, false, false);
            cbUncheckedNormal.SetFixedState(false, true, false, false);
            cbUncheckedTracking.SetFixedState(false, true, true, false);
            cbUncheckedPressed.SetFixedState(false, true, false, true);
            cbCheckedDisabled.SetFixedState(false, false, false, false);
            cbCheckedNormal.SetFixedState(false, true, false, false);
            cbCheckedTracking.SetFixedState(false, true, true, false);
            cbCheckedPressed.SetFixedState(false, true, false, true);
            cbIndeterminateDisabled.SetFixedState(false, false, false, false);
            cbIndeterminateNormal.SetFixedState(false, true, false, false);
            cbIndeterminateTracking.SetFixedState(false, true, true, false);
            cbIndeterminatePressed.SetFixedState(false, true, false, true);
        }

        private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"CheckBox styles for theme: {kryptonThemeComboBox1.Text}";
        }
    }
}
