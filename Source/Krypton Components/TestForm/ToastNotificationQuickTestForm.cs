#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion

using Krypton.Utilities;

namespace TestForm;

public partial class ToastNotificationQuickTestForm : KryptonForm
{
    public ToastNotificationQuickTestForm()
    {
        InitializeComponent();
    }

    private void kbtnBasicNotification_Click(object sender, EventArgs e)
    {
        KryptonBasicToastData data = new KryptonBasicToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            CountDownSeconds = 60
        };

        KryptonToast.ShowBasicNotification(data);
    }

    private void kbtnBasicNotificationChecked_Click(object sender, EventArgs e)
    {
        KryptonBasicToastData data = new KryptonBasicToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            ShowDoNotShowAgainOption = true,
            IsDoNotShowAgainOptionChecked = true,
            CountDownSeconds = 60
        };

        bool result = KryptonToast.ShowBasicNotificationWithBooleanReturnValue(data);

        KryptonMessageBox.Show($"Result = {result}", string.Empty, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
    }

    private void kbtnBasicNotificationCheckState_Click(object sender, EventArgs e)
    {
        KryptonBasicToastData data = new KryptonBasicToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            ShowDoNotShowAgainOption = true,
            DoNotShowAgainOptionCheckState = CheckState.Checked,
            UseDoNotShowAgainOptionThreeState = true,
            CountDownSeconds = 60
        };

        CheckState result = KryptonToast.ShowBasicNotificationWithCheckStateReturnValue(data);

        KryptonMessageBox.Show($"Result = {result}", string.Empty, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
    }

    private void kbtnBasicNotificationWithProgressBar_Click(object sender, EventArgs e)
    {
        KryptonBasicToastData data = new KryptonBasicToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            CountDownSeconds = 60
        };

        KryptonToast.ShowBasicProgressBarNotification(data);
    }

    private void kbtnBasicNotificationWithProgressBarChecked_Click(object sender, EventArgs e)
    {
        KryptonBasicToastData data = new KryptonBasicToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            ShowDoNotShowAgainOption = true,
            IsDoNotShowAgainOptionChecked = true,
            CountDownSeconds = 60
        };

        bool result = KryptonToast.ShowBasicProgressBarNotificationWithBooleanReturnValue(data);

        KryptonMessageBox.Show($"Result = {result}", string.Empty, KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Information);
    }

    private void kbtnBasicNotificationWithProgressBarCheckState_Click(object sender, EventArgs e)
    {
        KryptonBasicToastData data = new KryptonBasicToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            ShowDoNotShowAgainOption = true,
            DoNotShowAgainOptionCheckState = CheckState.Checked,
            UseDoNotShowAgainOptionThreeState = true,
            CountDownSeconds = 60
        };

        CheckState result =
            KryptonToast.ShowBasicProgressBarNotificationWithCheckStateReturnValue(data);

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnComboBoxNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastData comboBoxNotificationData = new KryptonUserInputToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            UserInputList = GetItems(),
            NotificationInputAreaType = KryptonToastInputAreaType.ComboBox,
            CountDownSeconds = 60
        };

        string? result = KryptonToast.ShowNotification(comboBoxNotificationData) as string;

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

        items.Add(@"Apples");
        items.Add(@"Bananas");
        items.Add(@"Pears");
        items.Add(@"Oranges");

        return items;
    }

    private void kbtnDateTimeNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastData dateTimeNotificationData = new KryptonUserInputToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            NotificationInputAreaType = KryptonToastInputAreaType.DateTime,
            CountDownSeconds = 60
        };

        DateTime result = (DateTime)KryptonToast.ShowNotification(dateTimeNotificationData);

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnDomainUpDownNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastData domainUpDownNotificationData = new KryptonUserInputToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            NotificationInputAreaType = KryptonToastInputAreaType.DomainUpDown,
            UserInputList = GetItems(),
            CountDownSeconds = 60
        };

        string? result = KryptonToast.ShowNotification(domainUpDownNotificationData) as string;

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnMaskedTextBoxNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastData maskedTextBoxNotificationData =
            new KryptonUserInputToastData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastIcon.Information,
                NotificationInputAreaType = KryptonToastInputAreaType.MaskedTextBox,
                CountDownSeconds = 60
            };

        string? result = KryptonToast.ShowNotification(maskedTextBoxNotificationData) as string;

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnNumericUpDownNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastData numericUpDownNotificationData =
            new KryptonUserInputToastData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastIcon.Information,
                NotificationInputAreaType = KryptonToastInputAreaType.NumericUpDown,
                CountDownSeconds = 60
            };

        int result = (int)KryptonToast.ShowNotification(numericUpDownNotificationData);

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnTextBoxNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastData textBoxNotificationData = new KryptonUserInputToastData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastIcon.Information,
            NotificationInputAreaType = KryptonToastInputAreaType.TextBox,
            ToastNotificationCueText = @"Type your response here...",
            CountDownSeconds = 60
        };

        string? result = KryptonToast.ShowNotification(textBoxNotificationData) as string;

        KryptonMessageBox.Show($"Result = {result}");
    }
}