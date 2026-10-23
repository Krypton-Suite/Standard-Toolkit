#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual demo for <see cref="Krypton.Toolkit.KryptonToolTip"/> — themed tooltips on standard WinForms and Krypton controls (Issues #3380 and #4192).
/// </summary>
public partial class KryptonToolTipTest : KryptonForm
{
    private int _interactiveClicks;

    public KryptonToolTipTest()
    {
        InitializeComponent();
        kryptonToolTip1.ContainerControl = this;
        kryptonToolTip1.ToolTipValues.ToolTipStyle = LabelStyle.SuperTip;
        kryptonToolTip1.SetToolTip(btnStandardWinFormsButton,
            "Standard WinForms button",
            "This is a themed Krypton tooltip on a plain System.Windows.Forms.Button via KryptonToolTip.");
        kryptonToolTip1.SetToolTip(kbtnSample,
            "Krypton theme",
            "KryptonButton can already show built-in tips; here the same wrapper shows a tooltip for comparison.");
        kryptonToolTip1.SetToolTip(pnlHoverRegion,
            "Panel region",
            "Hover anywhere on this panel surface to verify hit-testing hooks on composite controls.");
        kryptonToolTip1.SetLinkToolTip(kbtnLinkTip,
            "Issue #4192",
            "Open the GitHub feature request",
            "https://github.com/Krypton-Suite/Standard-Toolkit/issues/4192");

        var host = new TableLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 1,
            Padding = new Padding(2),
            BackColor = Color.Transparent
        };
        var caption = new KryptonLabel { AutoSize = true };
        caption.Values.Text = "Any Control: click to count.";
        var action = new KryptonButton
        {
            AutoSize = true,
            MinimumSize = new Size(120, 28)
        };
        action.Values.Text = "Increment";
        action.Click += (_, _) =>
        {
            _interactiveClicks++;
            klblInteractiveStatus.Values.Text = $"Interactive clicks: {_interactiveClicks}";
        };
        host.Controls.Add(caption);
        host.Controls.Add(action);
        kryptonToolTip1.SetToolTip(kbtnInteractiveTip, "Hosted controls", host, ownsContent: true);
        kryptonToolTip1.LinkClicked += (_, e) =>
        {
            klblInteractiveStatus.Values.Text = $"Link clicked: {e.Url}";
        };

        kryptonToolTip1.SetToolTip(kbtnHtmlTip, "HTML fragment",
            Krypton.Toolkit.Utilities.KryptonHtmlToolTipContent.Create("See <a href=\"https://github.com/Krypton-Suite/Standard-Toolkit/issues/4192\">issue 4192</a> for hosted HTML."),
            ownsContent: true);

        kbtnSample.ToolTipValues.EnableToolTips = false;

        klblInstructions.Values.Text =
            "Hover the controls below. SuperTip text uses show / auto-close delays. "
            + "Move onto Hyperlink / Hosted / HTML popups to click. Click away to close. "
            + "LinkClicked updates the status line.";
    }
}
