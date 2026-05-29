#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

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