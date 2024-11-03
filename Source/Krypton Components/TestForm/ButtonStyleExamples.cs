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
    public partial class ButtonStyleExamples : KryptonForm
    {
        public ButtonStyleExamples()
        {
            InitializeComponent();
        }

        private void ButtonStyleExamples_Load(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"Button styles for theme: {kryptonThemeComboBox1.Text}";

            // Button fixed states
            buttonDisabled.SetFixedState(PaletteState.Disabled);
            buttonDefaultFocus.SetFixedState(PaletteState.NormalDefaultOverride);
            buttonNormal.SetFixedState(PaletteState.Normal);
            buttonTracking.SetFixedState(PaletteState.Tracking);
            buttonPressed.SetFixedState(PaletteState.Pressed);
            buttonCheckedNormal.SetFixedState(PaletteState.CheckedNormal);
            buttonCheckedTracking.SetFixedState(PaletteState.CheckedTracking);
            buttonCheckedPressed.SetFixedState(PaletteState.CheckedPressed);
        }

        private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"Button styles for theme: {kryptonThemeComboBox1.Text}";
        }

        private void buttonLive_Click(object sender, EventArgs e)
        {

        }

        private void buttonDefaultFocus_Click(object sender, EventArgs e)
        {

        }

        private void buttonTracking_Click(object sender, EventArgs e)
        {

        }

        private void buttonNormal_Click(object sender, EventArgs e)
        {

        }

        private void buttonPressed_Click(object sender, EventArgs e)
        {

        }

        private void buttonDisabled_Click(object sender, EventArgs e)
        {

        }

        private void buttonCheckedNormal_Click(object sender, EventArgs e)
        {

        }

        private void buttonCheckedTracking_Click(object sender, EventArgs e)
        {

        }

        private void buttonCheckedPressed_Click(object sender, EventArgs e)
        {

        }
    }
}
