using System;

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

        private void kbtnUserInputNotification_Click(object sender, EventArgs e)
        {
            UserInputToastNotificationTest inputToastNotification = new UserInputToastNotificationTest();

            inputToastNotification.Show();
        }

        private void kbtnQuickNotificationTest_Click(object sender, EventArgs e)
        {
            ToastNotificationQuickTestForm quickToastNotification = new ToastNotificationQuickTestForm();

            quickToastNotification.Show();
        }
    }
}
