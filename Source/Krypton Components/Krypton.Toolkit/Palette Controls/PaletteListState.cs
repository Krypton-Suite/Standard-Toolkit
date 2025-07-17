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
/// Implement storage for border,background and contained triple.
/// </summary>
public class PaletteListState : PaletteDouble
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteListState class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteListState(PaletteListStateRedirect inherit,
        NeedPaintHandler needPaint)
        : base(inherit, needPaint) =>
        Item = new PaletteTriple(inherit.Item, needPaint);

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault && Item.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Which state to populate from.</param>
    public override void PopulateFromBase(PaletteState state)
    {
        base.PopulateFromBase(state);
        Item.PopulateFromBase(state);
    }
    #endregion

    #region Item
    /// <summary>
    /// Gets the item appearance overrides.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple Item { get; }

    private bool ShouldSerializeItem() => !Item.IsDefault;

    #endregion
}