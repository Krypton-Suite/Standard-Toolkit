#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

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

        string? result = KryptonToastNotification.ShowNotification(comboBoxNotificationData) as string;

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
        KryptonUserInputToastNotificationData dateTimeNotificationData = new KryptonUserInputToastNotificationData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastNotificationIcon.Information,
            NotificationInputAreaType = KryptonToastNotificationInputAreaType.DateTime,
            CountDownSeconds = 60
        };

        DateTime result = (DateTime)KryptonToastNotification.ShowNotification(dateTimeNotificationData);

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnDomainUpDownNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastNotificationData domainUpDownNotificationData = new KryptonUserInputToastNotificationData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastNotificationIcon.Information,
            NotificationInputAreaType = KryptonToastNotificationInputAreaType.DomainUpDown,
            UserInputList = GetItems(),
            CountDownSeconds = 60
        };

        string? result = KryptonToastNotification.ShowNotification(domainUpDownNotificationData) as string;

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnMaskedTextBoxNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastNotificationData maskedTextBoxNotificationData =
            new KryptonUserInputToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                NotificationInputAreaType = KryptonToastNotificationInputAreaType.MaskedTextBox,
                CountDownSeconds = 60
            };

        string? result = KryptonToastNotification.ShowNotification(maskedTextBoxNotificationData) as string;

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnNumericUpDownNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastNotificationData numericUpDownNotificationData =
            new KryptonUserInputToastNotificationData()
            {
                NotificationTitle = @"Hello World!",
                NotificationContent = @"This is a simple test...",
                NotificationIcon = KryptonToastNotificationIcon.Information,
                NotificationInputAreaType = KryptonToastNotificationInputAreaType.NumericUpDown,
                CountDownSeconds = 60
            };

        int result = (int)KryptonToastNotification.ShowNotification(numericUpDownNotificationData);

        KryptonMessageBox.Show($"Result = {result}");
    }

    private void kbtnTextBoxNotificaton_Click(object sender, EventArgs e)
    {
        KryptonUserInputToastNotificationData textBoxNotificationData = new KryptonUserInputToastNotificationData()
        {
            NotificationTitle = @"Hello World!",
            NotificationContent = @"This is a simple test...",
            NotificationIcon = KryptonToastNotificationIcon.Information,
            NotificationInputAreaType = KryptonToastNotificationInputAreaType.TextBox,
            ToastNotificationCueText = @"Type your response here...",
            CountDownSeconds = 60
        };

        string? result = KryptonToastNotification.ShowNotification(textBoxNotificationData) as string;

        KryptonMessageBox.Show($"Result = {result}");
    }
}