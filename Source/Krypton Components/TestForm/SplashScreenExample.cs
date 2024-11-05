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
            KryptonSplashScreenData splashScreenData = new KryptonSplashScreenData()
            {
                ApplicationLogo = new Bitmap(ktxtLogo.Text),
                Assembly = Assembly.GetExecutingAssembly(), //Assembly.LoadFile(ktxtAssembly.Text),
                NextWindow = this,
                ShowCopyright = kchkShowCopyright.Checked,
                ShowCloseButton = kcbShowCloseButton.Checked,
                ShowMinimizeButton = kcbShowMinimizeButton.Checked,
                ShowApplicationName = kchkShowApplicationName.Checked,
                ShowProgressBar = kchkShowProgressBar.Checked,
                ShowProgressBarPercentage = kchkShowProgressBarPercentage.Checked,
                ShowVersion = kchkShowVersion.Checked,
                Timeout = Convert.ToInt32(knudTimeout.Value)
            };

            KryptonSplashScreen.Show(splashScreenData);
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
