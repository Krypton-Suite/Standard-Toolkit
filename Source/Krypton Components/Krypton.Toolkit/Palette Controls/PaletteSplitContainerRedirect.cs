#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Extend storage for the split container with background and border information combined with separator information.
/// </summary>
public class PaletteSplitContainerRedirect : PaletteDoubleRedirect
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteSplitContainerRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backContainerStyle">Initial split container background style.</param>
    /// <param name="borderContainerStyle">Initial split container border style.</param>
    /// <param name="backSeparatorStyle">Initial separator background style.</param>
    /// <param name="borderSeparatorStyle">Initial separator border style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteSplitContainerRedirect(PaletteRedirect redirect,
        PaletteBackStyle backContainerStyle,
        PaletteBorderStyle borderContainerStyle,
        PaletteBackStyle backSeparatorStyle,
        PaletteBorderStyle borderSeparatorStyle,
        NeedPaintHandler needPaint)
        : base(redirect, backContainerStyle, borderContainerStyle, needPaint) =>
        // Create the embedded separator palette information
        Separator = new PaletteSeparatorPaddingRedirect(redirect, backSeparatorStyle, borderSeparatorStyle, needPaint);

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault &&
                                      Separator.IsDefault;

    #endregion

    #region Border
    /// <summary>
    /// Gets access to the border palette details.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteBorder Border => base.Border;

    #endregion

    #region Separator
    /// <summary>
    /// Get access to the overrides for defining separator appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteSeparatorPaddingRedirect Separator { get; }

    private bool ShouldSerializeSeparator() => !Separator.IsDefault;

    #endregion
}