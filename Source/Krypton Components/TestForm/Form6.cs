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
    public partial class Form6 : KryptonForm
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void kcbThemeOptions_CheckedChanged(object sender, EventArgs e)
        {
            kryptonToolkitPoweredByControl1.ShowThemeOption = kcbThemeOptions.Checked;
        }

        private void kcmbToolkitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // kryptonToolkitPoweredByControl1.ToolkitType = (ToolkitType)Enum.Parse(typeof(ToolkitType), kcmbToolkitType.Text);
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            //foreach (string value in Enum.GetNames(typeof(ToolkitType)))
            //{
            //    kcmbToolkitType.Items.Add(value);
            //}
        }
    }
}
