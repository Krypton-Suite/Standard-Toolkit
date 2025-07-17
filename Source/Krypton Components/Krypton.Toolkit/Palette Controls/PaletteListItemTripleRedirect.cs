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
/// Implement storage for a list item triple.
/// </summary>
public class PaletteListItemTripleRedirect : Storage                                            
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteListItemTripleRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="backStyle">Initial background style.</param>
    /// <param name="borderStyle">Initial border style.</param>
    /// <param name="contentStyle">Initial content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteListItemTripleRedirect([DisallowNull] PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);
        Item = new PaletteTripleRedirect(redirect!, backStyle, borderStyle, contentStyle, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Item.IsDefault;

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