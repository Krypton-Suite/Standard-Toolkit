#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Regression demo for Issue #3842: <see cref="KryptonMessageBoxExtended"/> RTL routing on the
/// optional-checkbox <c>bool</c> and <see cref="CheckState"/> return paths.
/// </summary>
public sealed class Bug3842MessageBoxExtendedRtlRoutingDemo : KryptonForm
{
    private readonly KryptonWrapLabel _lblInstructions;
    private readonly KryptonWrapLabel _lblResult;
    private readonly KryptonCheckBox _chkThreeState;

    public Bug3842MessageBoxExtendedRtlRoutingDemo()
    {
        Text = @"3842 — MessageBox Extended RTL routing";
        Size = new Size(720, 360);
        StartPosition = FormStartPosition.CenterScreen;

        _lblInstructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            Height = 96,
            Padding = new Padding(12),
            Text =
                "Issue #3842: the bool- and CheckState-returning ShowCore paths must display the RTL extended message box.\r\n" +
                "Each button below shows a message box with an optional checkbox. Toggle the checkbox, dismiss the dialog, and confirm the reported return value matches.\r\n" +
                "RTL scenarios use MessageBoxOptions.RtlReading; LTR scenarios use default options."
        };

        _chkThreeState = new KryptonCheckBox
        {
            Dock = DockStyle.Top,
            Padding = new Padding(12, 0, 12, 8),
            Text = @"Use three-state checkbox (CheckState path only)"
        };

        var buttonPanel = new KryptonPanel { Dock = DockStyle.Top, Height = 120, Padding = new Padding(12) };
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

        layout.Controls.Add(CreateActionButton("LTR — bool return", (_, _) => ShowBoolPath(useRtl: false)), 0, 0);
        layout.Controls.Add(CreateActionButton("RTL — bool return", (_, _) => ShowBoolPath(useRtl: true)), 1, 0);
        layout.Controls.Add(CreateActionButton("LTR — CheckState return", (_, _) => ShowCheckStatePath(useRtl: false)), 0, 1);
        layout.Controls.Add(CreateActionButton("RTL — CheckState return", (_, _) => ShowCheckStatePath(useRtl: true)), 1, 1);
        buttonPanel.Controls.Add(layout);

        _lblResult = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            Text = @"Last result: (none)"
        };

        Controls.Add(_lblResult);
        Controls.Add(buttonPanel);
        Controls.Add(_chkThreeState);
        Controls.Add(_lblInstructions);
    }

    private KryptonButton CreateActionButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(6),
            Values = { Text = text }
        };
        button.Click += onClick;
        return button;
    }

    private void ShowBoolPath(bool useRtl)
    {
        MessageBoxOptions options = useRtl ? MessageBoxOptions.RtlReading : 0;

        bool result = KryptonMessageBoxExtended.ShowCoreWithBoolResult(
            this,
            "Toggle the optional checkbox, then click OK.",
            useRtl ? @"RTL optional checkbox (bool)" : @"LTR optional checkbox (bool)",
            ExtendedMessageBoxButtons.OK,
            ExtendedKryptonMessageBoxIcon.Question,
            KryptonMessageBoxDefaultButton.Button1,
            options,
            null,
            null,
            null,
            null,
            null,
            null,
            [Color.Empty, Color.Empty, Color.Empty, Color.Empty],
            null,
            null,
            null,
            null,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            null,
            ExtendedKryptonMessageBoxMessageContainerType.Normal,
            null,
            null,
            null,
            null,
            ContentAlignment.MiddleLeft,
            null,
            null,
            true,
            false,
            null,
            "Do not show this again",
            false,
            null,
            null,
            null,
            null);

        _lblResult.Text = $@"Last result ({(useRtl ? "RTL" : "LTR")} bool): {result}";
    }

    private void ShowCheckStatePath(bool useRtl)
    {
        MessageBoxOptions options = useRtl ? MessageBoxOptions.RtlReading : 0;
        bool threeState = _chkThreeState.Checked;
        CheckState initialState = threeState ? CheckState.Indeterminate : CheckState.Unchecked;

        CheckState result = KryptonMessageBoxExtended.ShowCoreWithCheckStateResult(
            this,
            "Toggle the optional checkbox, then click OK.",
            useRtl ? @"RTL optional checkbox (CheckState)" : @"LTR optional checkbox (CheckState)",
            ExtendedMessageBoxButtons.OK,
            ExtendedKryptonMessageBoxIcon.Question,
            KryptonMessageBoxDefaultButton.Button1,
            options,
            null,
            null,
            null,
            null,
            null,
            null,
            [Color.Empty, Color.Empty, Color.Empty, Color.Empty],
            null,
            null,
            null,
            null,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            null,
            ExtendedKryptonMessageBoxMessageContainerType.Normal,
            null,
            null,
            null,
            null,
            ContentAlignment.MiddleLeft,
            null,
            null,
            true,
            false,
            initialState,
            "Do not show this again",
            threeState,
            null,
            null,
            null,
            null);

        _lblResult.Text = $@"Last result ({(useRtl ? "RTL" : "LTR")} CheckState): {result}";
    }
}
