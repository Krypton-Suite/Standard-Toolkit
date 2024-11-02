using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            ISplashScreenData splashScreenData = null;

            splashScreenData.ApplicationLogo = Image.FromFile(ktxtLogo.Text);
                Assembly = Assembly.LoadFile(ktxtAssembly.Text),
                NextWindow = null,
                ShowCopyright = kchkShowCopyright.Checked,
                ShowProgressBar = kchkShowProgressBar.Checked,
                ShowProgressBarPercentage = kchkShowProgressBarPercentage.Checked,
                ShowVersion = kchkShowVersion.Checked,
                Timeout = (int)knudTimeout.Value
            };
        }
    }
}
