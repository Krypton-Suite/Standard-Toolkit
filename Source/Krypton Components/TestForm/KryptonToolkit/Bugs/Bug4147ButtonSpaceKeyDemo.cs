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
/// Demo for issue #4147: Space must activate a focused KryptonButton when the mouse is not hovering it.
/// </summary>
public sealed class Bug4147ButtonSpaceKeyDemo : KryptonForm
{
    private const string DemoTitle = @"Bug #4147 - Button Space Key";

    private readonly KryptonWrapLabel _lblStatus;
    private int _kryptonClicks;
    private int _nativeClicks;

    public Bug4147ButtonSpaceKeyDemo()
    {
        Text = DemoTitle;
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(720, 380);
        MinimumSize = new Size(640, 320);

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 110,
            Text =
                @"How to test issue #4147:" + Environment.NewLine +
                @"1) Keep the mouse pointer away from both buttons." + Environment.NewLine +
                @"2) Press Tab until focus is on ""KryptonButton"" (or ""Native Button"")." + Environment.NewLine +
                @"3) Press Space — the click count for that button must increase." + Environment.NewLine +
                @"4) Move the mouse onto the focused button and press Space again — it must still click."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 48,
            Text = @"Clicks — Krypton: 0 | Native: 0"
        };

        var comparison = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2,
            Padding = new Padding(16)
        };
        comparison.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        comparison.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        comparison.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        comparison.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        var lblKrypton = new KryptonLabel
        {
            Text = @"KryptonButton",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.BoldControl
        };
        var lblNative = new KryptonLabel
        {
            Text = @"Native Button",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.BoldControl
        };

        var kryptonButton = new KryptonButton
        {
            Text = @"KryptonButton",
            Dock = DockStyle.Top,
            Height = 40,
            TabIndex = 0
        };
        kryptonButton.Click += (_, _) =>
        {
            _kryptonClicks++;
            UpdateStatus();
        };

        var nativeButton = new Button
        {
            Text = @"Native Button",
            Dock = DockStyle.Top,
            Height = 40,
            TabIndex = 1,
            UseVisualStyleBackColor = true
        };
        nativeButton.Click += (_, _) =>
        {
            _nativeClicks++;
            UpdateStatus();
        };

        comparison.Controls.Add(lblKrypton, 0, 0);
        comparison.Controls.Add(lblNative, 1, 0);
        comparison.Controls.Add(kryptonButton, 0, 1);
        comparison.Controls.Add(nativeButton, 1, 1);

        var content = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8)
        };
        content.Controls.Add(comparison);

        Controls.Add(content);
        Controls.Add(_lblStatus);
        Controls.Add(lblInfo);
    }

    private void UpdateStatus() =>
        _lblStatus.Text = $@"Clicks — Krypton: {_kryptonClicks} | Native: {_nativeClicks}";
}
