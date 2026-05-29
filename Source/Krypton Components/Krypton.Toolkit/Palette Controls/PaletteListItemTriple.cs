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
public class PaletteListItemTriple : Storage
{
    #region Instance Fields
    private readonly PaletteTriple _paletteItem;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteListItemTriple class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteListItemTriple([DisallowNull] PaletteTripleRedirect inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        _paletteItem = new PaletteTriple(inherit!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _paletteItem.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">The palette state to populate with.</param>
    public virtual void PopulateFromBase(PaletteState state) => _paletteItem.PopulateFromBase(state);

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public virtual void SetInherit(PaletteTripleRedirect inherit) => _paletteItem.SetInherit(inherit);

    #endregion

    #region Item
    /// <summary>
    /// Gets access to the item palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteTriple Item => _paletteItem;

    private bool ShouldSerializeItem() => !_paletteItem.IsDefault;

    #endregion
}