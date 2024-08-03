#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    public partial class CustomMessageBoxTest : KryptonForm
    {
        private const string SEED_TEXT =
            @"// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006-2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2024. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 4.7.0.0  www.ComponentFactory.com
// *****************************************************************************
";

        private MessageBoxIcon _mbIcon = MessageBoxIcon.Warning;
        private KryptonMessageBoxIcon _kmbIcon = KryptonMessageBoxIcon.Warning;
        private KryptonMessageBoxButtons _mbButtons = KryptonMessageBoxButtons.OKCancel;
        private MessageBoxOptions _options = 0;

        public CustomMessageBoxTest()
        {
            InitializeComponent();

            HelpRequested += CustomMessageBoxTest_HelpRequested;
        }

        private void icon_CheckedChanged(object sender, EventArgs e)
        {
            if (krbIconNone.Checked)
            {
                _mbIcon = MessageBoxIcon.None;
                _kmbIcon = KryptonMessageBoxIcon.None;
            }
            else if (krbIconError.Checked)
            {
                _mbIcon = MessageBoxIcon.Error;
                _kmbIcon = KryptonMessageBoxIcon.Error;
            }
            else if (krbIconQuestion.Checked)
            {
                _mbIcon = MessageBoxIcon.Question;
                _kmbIcon = KryptonMessageBoxIcon.Question;
            }
            else if (krbIconWarning.Checked)
            {
                _mbIcon = MessageBoxIcon.Warning;
                _kmbIcon = KryptonMessageBoxIcon.Warning;
            }
            else if (krbIconInformation.Checked)
            {
                _mbIcon = MessageBoxIcon.Information;
                _kmbIcon = KryptonMessageBoxIcon.Information;
            }
            else if (krbIconShield.Checked)
            {
                _mbIcon = MessageBoxIcon.None;
                _kmbIcon = KryptonMessageBoxIcon.Shield;
            }
            else if (!krbIconWinLogo.Checked)
            {
                _mbIcon = MessageBoxIcon.None;
                _kmbIcon = KryptonMessageBoxIcon.WindowsLogo;
            }
        }

        private void buttons_CheckedChanged(object sender, EventArgs e)
        {
            if (krbButtonsOk.Checked)
            {
                _mbButtons = KryptonMessageBoxButtons.OK;
            }
            else if (krbButtonsOkCancel.Checked)
            {
                _mbButtons = KryptonMessageBoxButtons.OKCancel;
            }
            else if (krbButtonsRetryCancel.Checked)
            {
                _mbButtons = KryptonMessageBoxButtons.RetryCancel;
            }
            else if (krbButtonsAbortRetryIgnore.Checked)
            {
                _mbButtons = KryptonMessageBoxButtons.AbortRetryIgnore;
            }
            else if (krbButtonsYesNo.Checked)
            {
                _mbButtons = KryptonMessageBoxButtons.YesNo;
            }
            else if (krbButtonsYesNoCancel.Checked)
            {
                _mbButtons = KryptonMessageBoxButtons.YesNoCancel;
            }
            else if (krbButtonsCancelTryContinue.Checked)
            {
                _mbButtons = KryptonMessageBoxButtons.CancelTryContinue;
            }
        }

        private void CustomMessageBoxTest_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            // Create a custom Help window in response to the HelpRequested event.
            using var helpForm = new Form
            {
                // Set up the form position, size, and title caption.
                StartPosition = FormStartPosition.Manual,
                Size = new Size(200, 400),
                DesktopLocation = new Point(DesktopBounds.X + Size.Width, DesktopBounds.Top),
                Text = @"Help Form"
            };

            // Create a label to contain the Help text.
            Label helpLabel = new Label();

            // Add the label to the form and set its text.
            helpForm.Controls.Add(helpLabel);
            helpLabel.Dock = DockStyle.Fill;

            // Use the sender parameter to identify the context of the Help request.
            // The parameter must be cast to the Control type to get the Tag property.
            Control? senderControl = sender as Control;

            helpLabel.Text = $@"Help information shown in response to user action on the '{(string?)senderControl!.Tag}' message.";

            // Set the Help form to be owned by the main form. This helps
            // to ensure that the Help form is disposed of.
            AddOwnedForm(helpForm);

            // Show the custom Help window.
            helpForm.Show();

            // Indicate that the HelpRequested event is handled.
            hlpevent.Handled = true;
        }

        private void kchkMessageBoxOptionsRightAlign_CheckedChanged(object sender, EventArgs e)
        {
            if (kchkMessageBoxOptionsRightAlign.Checked)
            {
                _options |= MessageBoxOptions.RightAlign;
            }
            else
            {
                _options &= ~MessageBoxOptions.RightAlign;
            }
        }

        private void kcbMessageBoxOptionsRtlReading_CheckedChanged(object sender, EventArgs e)
        {
            if (kcbMessageBoxOptionsRtlReading.Checked)
            {
                _options |= MessageBoxOptions.RtlReading;
            }
            else
            {
                _options &= ~MessageBoxOptions.RtlReading;
            }
        }

        private void kbtnShow_Click(object sender, EventArgs e)
        {
            string text =
                "// *****************************************************************************\r\n" +
                "// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)\n" +
                "//  © Component Factory Pty Ltd, 2006-2016, All rights reserved.\n" +
                "// The software and associated documentation supplied hereunder are the\n" +
                "//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\r" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  proprietary\tinformatio\tof\tComponent\tFactory\tPty\tLtd, 13\tSwallows\tClose,\n" +
                "//  Mornington,\r\n// Vic 3931,\r// Australia\n// and are supplied subject to licence terms.\n" +
                "//\n" +
                "//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2024. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)\n" +
                "//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2024. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)\n" +
                "//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2024. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)\n" +
                "//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2024. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)\n" +
                "//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2024. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)\n" +
                "//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2024. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)\n" +
                "//  Version 4.7.0.0  www.ComponentFactory.com\r\n" +
                "// *****************************************************************************\r\n";

            string s = CommonHelper.NormalizeLineBreaks(text);
            krtbMessageBody.WordWrap = false;
            krtbMessageBody.Text = s;
            this.MaximizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            krtbMessageBody.Dock = DockStyle.Fill;
            var res = KryptonMessageBox.Show(this, s, ktxtCaption.Text,
                _mbButtons,
                displayHelpButton: kcbShowHelp.Checked,
                _kmbIcon, KryptonMessageBoxDefaultButton.Button1,
                options: _options, kchkShowCtrlCopyText.Checked);

        }

        private void kbtnTestText_Click(object sender, EventArgs e)
        {
            krtbMessageBody.Text = SEED_TEXT;
        }
    }
}
