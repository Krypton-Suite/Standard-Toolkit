#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Issue #2103: KryptonForm RightToLeft / RightToLeftLayout caption chrome vs native Form.
/// </summary>
public partial class RTLFormBorderTest : KryptonForm
{
    private const int ModeLeftToRight = 0;
    private const int ModeRightToLeftOnly = 1;
    private const int ModeRightToLeftLayout = 2;

    public RTLFormBorderTest()
    {
        InitializeComponent();
        kcmbRtlMode.SelectedIndex = ModeRightToLeftLayout;
        ApplySelectedMode();
    }

    private void kchkbtnSwitchLayout_Click(object sender, EventArgs e)
    {
        kcmbRtlMode.SelectedIndex = kchkbtnSwitchLayout.Checked
            ? ModeRightToLeftLayout
            : ModeLeftToRight;
        ApplySelectedMode();
    }

    private void kcmbRtlMode_SelectedIndexChanged(object sender, EventArgs e) => ApplySelectedMode();

    private void kbtnOpenNativeForm_Click(object sender, EventArgs e)
    {
        var native = new Form
        {
            Text = Text,
            Size = Size,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.Sizable,
            RightToLeft = RightToLeft,
            RightToLeftLayout = RightToLeftLayout
        };
        native.Controls.Add(new Label
        {
            AutoSize = false,
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            Text = "Native Form with the same RightToLeft / RightToLeftLayout. Compare caption glyphs, min/max/close side, and left/right resize."
        });
        native.Show(this);
    }

    private void ApplySelectedMode()
    {
        int index = kcmbRtlMode.SelectedIndex;
        if (index < 0)
        {
            return;
        }

        switch (index)
        {
            case ModeRightToLeftOnly:
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = false;
                break;
            case ModeRightToLeftLayout:
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = true;
                break;
            default:
                RightToLeft = RightToLeft.No;
                RightToLeftLayout = false;
                break;
        }

        kchkbtnSwitchLayout.Checked = RightToLeftLayout;
    }

    private void knudCaptionIconPadding_ValueChanged(object sender, EventArgs e)
    {
        int extra = (int)knudCaptionIconPadding.Value;
        CaptionIconPadding = new Padding(extra);
    }
}
