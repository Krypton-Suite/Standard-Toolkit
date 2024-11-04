using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class SplashScreenExample : KryptonForm
    {
        public SplashScreenExample()
        {
            InitializeComponent();
        }

        private void kbtnShow_Click(object sender, EventArgs e)
        {
           KryptonSplashScreen.Show(Assembly.LoadFrom(ktxtAssembly.Text), kchkShowProgressBar.Checked, Convert.ToInt32(knudTimeout.Value), Image.FromFile(ktxtLogo.Text));
        }

        private void kcmdChosenAssembly_Execute(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ktxtAssembly.Text = Path.GetFullPath(ofd.FileName);
            }
        }

        private void kcmdChosenLogo_Execute(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ktxtLogo.Text = Path.GetFullPath(ofd.FileName);
            }
        }
    }
}
