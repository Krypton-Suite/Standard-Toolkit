#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for issue #2475: <see cref="KryptonForm.HelpButton"/> renders a themed title-bar help button.
/// </summary>
public sealed class Bug2475KryptonFormHelpButtonDemo : KryptonForm
{
    private readonly KryptonWrapLabel _lblInfo;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonCheckBox _chkHelpButton;
    private readonly KryptonCheckBox _chkMinimizeBox;
    private readonly KryptonCheckBox _chkMaximizeBox;
    private readonly KryptonButton _btnOpenSample;

    public Bug2475KryptonFormHelpButtonDemo()
    {
        Text = @"Bug #2475 - KryptonForm Help Button";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(760, 420);
        MinimumSize = new Size(640, 360);
        HelpButton = true;
        MinimizeBox = false;
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedDialog;

        _lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 120,
            Text =
                @"How to test issue #2475:" + Environment.NewLine +
                @"1) A themed ? button should appear in the title bar when HelpButton is checked." + Environment.NewLine +
                @"2) Click the ? button — HelpRequested should fire and update the status line below." + Environment.NewLine +
                @"3) Unlike native WinForms, Krypton shows the help button whenever HelpButton is true (min/max may stay visible)." + Environment.NewLine +
                @"4) Use Open sample form to open a child with the same settings, or toggle the check boxes on this host."
        };

        _chkHelpButton = new KryptonCheckBox
        {
            Text = @"HelpButton",
            Checked = HelpButton,
            AutoSize = true
        };

        _chkMinimizeBox = new KryptonCheckBox
        {
            Text = @"MinimizeBox",
            Checked = MinimizeBox,
            AutoSize = true
        };

        _chkMaximizeBox = new KryptonCheckBox
        {
            Text = @"MaximizeBox",
            Checked = MaximizeBox,
            AutoSize = true
        };

        _btnOpenSample = new KryptonButton
        {
            Text = @"Open sample form",
            AutoSize = true
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            Height = 40,
            Text = @"Click the title-bar help button to verify HelpRequested."
        };

        var optionsPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 44,
            Padding = new Padding(12, 8, 12, 8)
        };

        var optionsFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            AutoSize = true
        };
        optionsFlow.Controls.Add(_chkHelpButton);
        optionsFlow.Controls.Add(_chkMinimizeBox);
        optionsFlow.Controls.Add(_chkMaximizeBox);
        optionsFlow.Controls.Add(_btnOpenSample);
        optionsPanel.Controls.Add(optionsFlow);

        var contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };
        contentPanel.Controls.Add(_lblStatus);
        contentPanel.Controls.Add(optionsPanel);
        contentPanel.Controls.Add(_lblInfo);

        Controls.Add(contentPanel);

        HelpRequested += (_, e) =>
        {
            _lblStatus.Text = $@"HelpRequested at {DateTime.Now:HH:mm:ss.fff} (Handled={e.Handled})";
            if (!e.Handled)
            {
                KryptonMessageBox.Show(this, @"Help button clicked — HelpRequested fired.", @"Help");
                e.Handled = true;
            }
        };

        _chkHelpButton.CheckedChanged += (_, _) =>
        {
            HelpButton = _chkHelpButton.Checked;
        };

        _chkMinimizeBox.CheckedChanged += (_, _) =>
        {
            MinimizeBox = _chkMinimizeBox.Checked;
        };

        _chkMaximizeBox.CheckedChanged += (_, _) =>
        {
            MaximizeBox = _chkMaximizeBox.Checked;
        };

        _btnOpenSample.Click += (_, _) =>
        {
            using var sample = new Bug2475HelpSampleForm(HelpButton, MinimizeBox, MaximizeBox);
            sample.ShowDialog(this);
        };
    }

    private sealed class Bug2475HelpSampleForm : KryptonForm
    {
        public Bug2475HelpSampleForm(bool helpButton, bool minimizeBox, bool maximizeBox)
        {
            Text = @"Bug #2475 sample";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(480, 260);
            HelpButton = helpButton;
            MinimizeBox = minimizeBox;
            MaximizeBox = maximizeBox;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            HelpRequested += (_, e) =>
            {
                KryptonMessageBox.Show(this, @"Sample form help requested.", @"Help");
                e.Handled = true;
            };

            var label = new KryptonWrapLabel
            {
                Dock = DockStyle.Fill,
                Text = @"Child sample form. Click the ? button in the title bar.",
                Padding = new Padding(12)
            };
            Controls.Add(label);
        }
    }
}
