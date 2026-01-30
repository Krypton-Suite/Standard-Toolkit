#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 */
#endregion

using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// MDI child form for Ribbon MDI demo (Issue #2921).
/// Can be created as resizable (Sizable) or no-resize (FixedSingle) to test both scenarios.
/// </summary>
public class RibbonMdiChildForm : KryptonForm
{
    private readonly bool _resizable;
    private KryptonLabel? _label;

    public RibbonMdiChildForm(bool resizable)
    {
        _resizable = resizable;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 280);
        FormBorderStyle = _resizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
        MaximizeBox = true;
        MinimizeBox = true;
        Name = "RibbonMdiChildForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = _resizable ? "Resize Test Window" : "No Resize";
        //
        // Label with instructions
        //
        _label = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Text = _resizable
                ? "Resizable MDI child.\r\n\r\n• Use title bar close/min/max — hit areas should match the buttons.\r\n• Open another child as Maximized to check for double ribbon tabs.\r\n• Close all children — double tabs should not remain."
                : "No-Resize MDI child (FixedSingle).\r\n\r\n• Use title bar close/min/max — hit areas should match the buttons.\r\n• QAT and caption drag should align with visuals (Issue #2921).",
            LabelStyle = LabelStyle.TitlePanel,
            StateCommon =
            {
                LongText =
                {
                    TextH = PaletteRelativeAlign.Center,
                    TextV = PaletteRelativeAlign.Center,
                    Font = new Font("Segoe UI", 10F)
                }
            }
        };
        Controls.Add(_label);
        ResumeLayout(false);
    }
}
