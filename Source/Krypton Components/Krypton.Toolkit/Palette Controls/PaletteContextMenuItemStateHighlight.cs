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
/// Storage for KryptonContextMenuItem highlight state values.
/// </summary>
public class PaletteContextMenuItemStateHighlight : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContextMenuItemStateHighlight class.
    /// </summary>
    /// <param name="redirect">Redirector for inheriting values.</param>
    public PaletteContextMenuItemStateHighlight(PaletteContextMenuRedirect redirect)
        : this(redirect.ItemHighlight, redirect.ItemSplit)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteContextMenuItemStateHighlight class.
    /// </summary>
    /// <param name="redirect">Redirector for inheriting values.</param>
    public PaletteContextMenuItemStateHighlight(PaletteContextMenuItemStateRedirect redirect)
        : this(redirect.ItemHighlight, redirect.ItemSplit)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteContextMenuItemStateHighlight class.
    /// </summary>
    /// <param name="redirectItemHighlight">Redirector for the ItemHighlight.</param>
    /// <param name="redirectItemSplit">Redirector for the ItemSplit.</param>
    public PaletteContextMenuItemStateHighlight(PaletteDoubleMetricRedirect? redirectItemHighlight,
        PaletteDoubleRedirect redirectItemSplit)
    {
        if (redirectItemHighlight is null)
        {
            throw new ArgumentNullException(nameof(redirectItemHighlight));
        }

        ItemHighlight = new PaletteDoubleMetric(redirectItemHighlight);
        ItemSplit = new PaletteDouble(redirectItemSplit);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ItemHighlight.IsDefault &&
                                      ItemSplit.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    /// <param name="state">State to inherit.</param>
    public void PopulateFromBase(KryptonPaletteCommon common,
        PaletteState state)
    {
        common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuItemHighlight;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuItemHighlight;
        ItemHighlight.PopulateFromBase(state);
        common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuSeparator;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuSeparator;
        ItemSplit.PopulateFromBase(state);
    }
    #endregion

    #region ItemHighlight
    /// <summary>
    /// Gets access to the item highlight appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item highlight appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDoubleMetric ItemHighlight { get; }

    private bool ShouldSerializeItemHighlight() => !ItemHighlight.IsDefault;

    #endregion

    #region ItemSplit
    /// <summary>
    /// Gets access to the item split appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item split appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble ItemSplit { get; }

    private bool ShouldSerializeItemSplit() => !ItemSplit.IsDefault;

    #endregion
}