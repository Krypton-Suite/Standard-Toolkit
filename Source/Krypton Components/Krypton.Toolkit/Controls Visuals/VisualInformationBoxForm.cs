#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualInformationBoxForm : KryptonForm
{
    public VisualInformationBoxForm()
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();
    }

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Both message labels are docked, so the client size follows the text and the label fills it.
        Control label = kwlblMessageText.Visible ? kwlblMessageText : klwlblMessageText;
        Font font = label.Font ?? SystemFonts.MessageBoxFont ?? SystemFonts.DefaultFont;
        KryptonDialogLayout.FitClientToText(this, label.Text, font, new Size(80, 90), new Size(240, 140), null);
        KryptonDialogLayout.EnableResizable(this);
    }
}