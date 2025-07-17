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
/// Implement storage for list box specific values.
/// </summary>
public class PaletteListStateRedirect : PaletteDoubleRedirect
                                            
{
    #region Instance Fields
    private readonly PaletteRedirect? _redirect;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteListStateRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteListStateRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        NeedPaintHandler needPaint)
        : base(redirect, backStyle, borderStyle, needPaint)
    {
        Debug.Assert(redirect != null);

        // Remember the redirect reference
        _redirect = redirect;

        // Create the item redirector
        Item = new PaletteTripleRedirect(redirect!,
            PaletteBackStyle.ButtonListItem,
            PaletteBorderStyle.ButtonListItem,
            PaletteContentStyle.ButtonListItem,
            needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault && Item.IsDefault;

    #endregion

    #region Item
    /// <summary>
    /// Gets the item appearance overrides.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect Item { get; }

    private bool ShouldSerializeItem() => !Item.IsDefault;

    #endregion
}