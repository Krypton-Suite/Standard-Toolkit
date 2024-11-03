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
    public partial class ControlStyles : KryptonForm
    {
        public ControlStyles()
        {
            InitializeComponent();
        }

        private void kbtnButtonStyles_Click(object sender, EventArgs e)
        {
            new ButtonStyleExamples().Show();
        }

        private void kbtnCheckBoxStyles_Click(object sender, EventArgs e)
        {
            new CheckBoxStyleExamples().Show();
        }

        private void kbtnRadioButtonStyles_Click(object sender, EventArgs e)
        {
            new RadioButtonStyleExamples().Show();
        }

        private void kbtnPanelStyles_Click(object sender, EventArgs e)
        {
            new PanelStyleExamples().Show();
        }

        private void kbtnLabelStyles_Click(object sender, EventArgs e)
        {
            new LabelStyleExamples().Show();
        }
    }
}
