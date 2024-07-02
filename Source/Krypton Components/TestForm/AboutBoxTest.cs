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
    public partial class AboutBoxTest : KryptonForm
    {
        public AboutBoxTest()
        {
            InitializeComponent();
        }

        private void kbtnShow_Click(object sender, EventArgs e)
        {
            KryptonAboutBoxData aboutBoxData = new KryptonAboutBoxData()
            {
                ApplicationName = @"TestForm",
                CurrentAssembly = Assembly.GetExecutingAssembly(),
                HeaderImage = null,
                MainImage = null,
                ShowToolkitInformation = false,
                UseFullBuiltOnDate = false
            };

            KryptonAboutToolkitData aboutToolkitData = new KryptonAboutToolkitData();


        }
    }
}
