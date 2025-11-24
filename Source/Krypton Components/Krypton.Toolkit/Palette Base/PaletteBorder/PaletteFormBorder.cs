#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Implement storage for palette border details.
/// </summary>
public class PaletteFormBorder : PaletteBorder
{
    private readonly VisualForm _ownerForm;

    #region Identity

    /// <summary>
    /// Initialize a new instance of the PaletteBorder class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="ownerForm"></param>
    public PaletteFormBorder([DisallowNull] IPaletteBorder inherit,
        NeedPaintHandler? needPaint,
        VisualForm ownerForm)
        : base(inherit, needPaint)
    {
        _ownerForm = ownerForm;
    }
    #endregion

    #region Width
    internal bool UseThemeFormChromeBorderWidth { get; set; } = true;

    /// <summary>
    /// Gets and sets the border width.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Border width.")]
    [DefaultValue(-1)]
    [RefreshProperties(RefreshProperties.All)]
    public override int Width
    {
        get
        {
            if (Draw == InheritBool.False)
            {
                return -1;
            }

            if (!UseThemeFormChromeBorderWidth)
            {
                return _ownerForm.RealWindowBorders.Horizontal / 2;
            }
            else
            {
                return base.Width;
            }
        }

        set => base.Width = value;
    }

    /// <summary>
    /// Gets the border rounding.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border rounding.</returns>
    public override float GetBorderRounding(PaletteState state)
    {
        if (Draw == InheritBool.False || Rounding == -1F)
        {
            return 0;
        }

        return base.GetBorderRounding(state);
    }

    /// <summary>
    /// Gets the graphics hint for drawing the border.
    /// </summary>
    [KryptonPersist(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(PaletteGraphicsHint.None)]
    public override PaletteGraphicsHint GraphicsHint
    {
        // #1757: Make sure that the little transparency elements on the curves do not show up for Form Borders
        get => PaletteGraphicsHint.None;

        set
        {
            // Do nothing
        }
    }

    /// <inheritdoc />
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state)
    {
        // #1757: Make sure that the little transparency elements on the curves do not show up for Form Borders
        return PaletteGraphicsHint.None;
    }

    /// https://github.com/Krypton-Suite/Standard-Toolkit/issues/139
    internal (int xBorder, int yBorder) BorderWidths(FormBorderStyle formFormBorderStyle)
    {
        int xBorder;
        int yBorder;

        if (UseThemeFormChromeBorderWidth)
        {
            xBorder = 0;
            yBorder = 0;
        }
        else
        {
            switch (formFormBorderStyle)
            {
                case FormBorderStyle.None:
                    xBorder = 0;
                    yBorder = 0;
                    break;

                case FormBorderStyle.FixedSingle:
                case FormBorderStyle.FixedToolWindow:
                    xBorder = PI.GetSystemMetrics(PI.SM_.CXFIXEDFRAME);
                    yBorder = PI.GetSystemMetrics(PI.SM_.CYFIXEDFRAME);
                    break;

                case FormBorderStyle.Fixed3D:
                    xBorder = PI.GetSystemMetrics(PI.SM_.CXEDGE);
                    yBorder = PI.GetSystemMetrics(PI.SM_.CYEDGE);
                    break;

                case FormBorderStyle.FixedDialog:
                    xBorder = PI.GetSystemMetrics(PI.SM_.CXDLGFRAME);
                    yBorder = PI.GetSystemMetrics(PI.SM_.CYDLGFRAME);
                    break;

                case FormBorderStyle.Sizable:
                case FormBorderStyle.SizableToolWindow:
                    xBorder = PI.GetSystemMetrics(PI.SM_.CXSIZEFRAME);
                    yBorder = PI.GetSystemMetrics(PI.SM_.CYSIZEFRAME);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(formFormBorderStyle), formFormBorderStyle, null);
            }
        }

        return (xBorder, yBorder);
    }
    #endregion
}