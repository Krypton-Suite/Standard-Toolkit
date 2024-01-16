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
    public partial class ToastNotificationTestChoice : KryptonForm
    {
        public ToastNotificationTestChoice()
        {
            InitializeComponent();
        }

        private void kbtnBasicNotification_Click(object sender, EventArgs e)
        {
            BasicToastNotificationTest basicToastNotification = new BasicToastNotificationTest();

            basicToastNotification.Show();
        }
    }
}
