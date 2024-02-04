using System;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class FormBorderTest : KryptonForm
    {
        public FormBorderTest()
        {
            InitializeComponent();
        }

        private void kbtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ChangeBorderStyle(FormBorderStyle borderStyle) => FormBorderStyle = borderStyle;

        private void FormBorderTest_Load(object sender, EventArgs e)
        {
            foreach (var value in Enum.GetValues(typeof(FormBorderStyle)))
            {
                kcmbBorderStyle.Items.Add(value);
            }

            kcmbBorderStyle.SelectedIndex = 4;
        }

        private void kcmbBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeBorderStyle((FormBorderStyle)Enum.Parse(typeof(FormBorderStyle), kcmbBorderStyle.Text));
        }
    }
}
