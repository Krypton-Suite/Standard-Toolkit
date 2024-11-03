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
    public partial class PanelStyleExamples : KryptonForm
    {
        public PanelStyleExamples()
        {
            InitializeComponent();
        }

        private void PanelStyleExamples_Load(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"Panel styles for theme: {kryptonThemeComboBox1.Text}";

            // Panel fixed states
            panel1Disabled.SetFixedState(PaletteState.Disabled);
            kryptonPanel3.SetFixedState(PaletteState.Disabled);
            kryptonLabel8.SetFixedState(PaletteState.Disabled);
            panel1Normal.SetFixedState(PaletteState.Normal);
        }

        private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"Panel styles for theme: {kryptonThemeComboBox1.Text}";
        }
    }
}
