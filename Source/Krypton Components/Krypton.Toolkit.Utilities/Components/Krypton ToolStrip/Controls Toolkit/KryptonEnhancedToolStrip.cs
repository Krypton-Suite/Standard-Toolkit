#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>A standard tool strip, with a few enhancements.</summary>
[Description("A standard tool strip, with a few enhancements.")]
public class KryptonEnhancedToolStrip : ToolStrip
{
    #region Variables

    private readonly EnhancedToolStripValues _values;
    private readonly bool _useKryptonRender;
    #endregion

    #region Properties

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Tool strip behaviour settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public EnhancedToolStripValues Values => _values;

    private bool ShouldSerializeValues() => !_values.IsDefault;

    private void ResetValues() => _values.Reset();

    /// <summary>Gets or sets whether the ToolStripEx honors item clicks when its containing form does not have input focus.</summary>
    /// <remarks>Default value is false, which is the same behavior provided by the base ToolStrip class.</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ClickThrough { get => _values.ClickThrough; set => _values.ClickThrough = value; }
    #endregion

    #region Constructor

    public KryptonEnhancedToolStrip()
    {
        _values = new EnhancedToolStripValues(this);
        _useKryptonRender = true;

        RenderMode = ToolStripRenderMode.ManagerRenderMode;
    }
    #endregion

    #region Overrides
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (_values.ClickThrough && m.Msg == NativeConstants.WM_MOUSEACTIVATE && m.Result == (IntPtr)NativeConstants.MA_ACTIVATEANDEAT)
        {
            m.Result = (IntPtr)NativeConstants.MA_ACTIVATE;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (_useKryptonRender)
        {
            if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
            {
                BackColor = kpr.KCT.StatusStripGradientEnd;
            }
        }

        base.OnPaint(e);
    }
    #endregion
}