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
/// Implement storage for storage for a tree node triple.
/// </summary>
public class PaletteTreeNodeTriple : Storage
{
    #region Instance Fields
    private readonly PaletteTriple _paletteNode;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteTreeNodeTriple class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteTreeNodeTriple([DisallowNull] PaletteTripleRedirect inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        _paletteNode = new PaletteTriple(inherit!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _paletteNode.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">The palette state to populate with.</param>
    public virtual void PopulateFromBase(PaletteState state) => _paletteNode.PopulateFromBase(state);

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public virtual void SetInherit(PaletteTripleRedirect inherit) => _paletteNode.SetInherit(inherit);

    #endregion

    #region Node
    /// <summary>
    /// Gets access to the node palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining node appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteTriple Node => _paletteNode;

    private bool ShouldSerializeNode() => !_paletteNode.IsDefault;

    #endregion
}