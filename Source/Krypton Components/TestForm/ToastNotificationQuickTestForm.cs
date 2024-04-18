#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForm
{
    public partial class ToastNotificationQuickTestForm : KryptonForm
    {
        public ToastNotificationQuickTestForm()
        {
            InitializeComponent();
        }

        private void kbtnBasicNotification_Click(object sender, EventArgs e)
        {
            KryptonBasicToastNotificationData data = new KryptonBasicToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                CountDownSeconds = 60
            };

            KryptonToastNotification.ShowBasicNotification(data);
        }

        private void kbtnBasicNotificationChecked_Click(object sender, EventArgs e)
        {
            KryptonBasicToastNotificationData data = new KryptonBasicToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                ShowDoNotShowAgainOption = true,
                IsDoNotShowAgainOptionChecked = true,
                CountDownSeconds = 60
            };

            bool result = KryptonToastNotification.ShowBasicNotificationWithBooleanReturnValue(data);

            KryptonMessageBox.Show($"Result = {result}", string.Empty, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }

        private void kbtnBasicNotificationCheckState_Click(object sender, EventArgs e)
        {
            KryptonBasicToastNotificationData data = new KryptonBasicToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                ShowDoNotShowAgainOption = true,
                DoNotShowAgainOptionCheckState = CheckState.Checked,
                UseDoNotShowAgainOptionThreeState = true,
                CountDownSeconds = 60
            };

            CheckState result = KryptonToastNotification.ShowBasicNotificationWithCheckStateReturnValue(data);

            KryptonMessageBox.Show($"Result = {result}", string.Empty, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }

        private void kbtnBasicNotificationWithProgressBar_Click(object sender, EventArgs e)
        {
            KryptonBasicToastNotificationData data = new KryptonBasicToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                CountDownSeconds = 60
            };

            KryptonToastNotification.ShowBasicProgressBarNotification(data);
        }

        private void kbtnBasicNotificationWithProgressBarChecked_Click(object sender, EventArgs e)
        {
            KryptonBasicToastNotificationData data = new KryptonBasicToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                ShowDoNotShowAgainOption = true,
                IsDoNotShowAgainOptionChecked = true,
                CountDownSeconds = 60
            };

            bool result = KryptonToastNotification.ShowBasicProgressBarNotificationWithBooleanReturnValue(data);

            KryptonMessageBox.Show($"Result = {result}", string.Empty, KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }

        private void kbtnBasicNotificationWithProgressBarCheckState_Click(object sender, EventArgs e)
        {
            KryptonBasicToastNotificationData data = new KryptonBasicToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                ShowDoNotShowAgainOption = true,
                DoNotShowAgainOptionCheckState = CheckState.Checked,
                UseDoNotShowAgainOptionThreeState = true,
                CountDownSeconds = 60
            };

            CheckState result =
                KryptonToastNotification.ShowBasicProgressBarNotificationWithCheckStateReturnValue(data);

            KryptonMessageBox.Show($"Result = {result}");
        }

        private void kbtnComboBoxNotificaton_Click(object sender, EventArgs e)
        {
            KryptonUserInputToastNotificationData comboBoxNotificationData = new KryptonUserInputToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                UserInputList = GetItems(),
                NotificationInputAreaType = KryptonToastNotificationInputAreaType.ComboBox,
                CountDownSeconds = 60
            };

            string result = KryptonToastNotification.ShowNotificationWithComboBox(comboBoxNotificationData);

            //string result = KryptonToastNotification.ShowNotificationWithComboBox(null,
            //    @"This is a simple test...", @"Hello World!", KryptonToastNotificationIcon.Exclamation, GetItems(), 1,
            //    ComboBoxStyle.DropDown, null, null, 60);

            KryptonMessageBox.Show($"Result = {result}");
        }

        private ArrayList GetItems()
        {
            ArrayList items = new ArrayList();

            for (int i = 0; i < 10; i++)
            {
                items.Add(i);
            }

            return items;
        }
    }
}
