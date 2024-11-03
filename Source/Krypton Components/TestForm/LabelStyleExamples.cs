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
    public partial class LabelStyleExamples : KryptonForm
    {
        public LabelStyleExamples()
        {
            InitializeComponent();
        }

        private void LabelStyleExamples_Load(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"Label styles for theme: {kryptonThemeComboBox1.Text}";

            // Labels fixed states
            label1Disabled.SetFixedState(PaletteState.Disabled);
            label1Normal.SetFixedState(PaletteState.Normal);
            label1LinkDisabled.SetFixedState(PaletteState.Disabled);
            label1Visited.SetFixedState(PaletteState.Normal);
            label1NotVisited.SetFixedState(PaletteState.Normal);
            label1Pressed.SetFixedState(PaletteState.Pressed);
        }

        private void kryptonThemeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonLabel1.Text = $"Label styles for theme: {kryptonThemeComboBox1.Text}";
        }
    }
}
