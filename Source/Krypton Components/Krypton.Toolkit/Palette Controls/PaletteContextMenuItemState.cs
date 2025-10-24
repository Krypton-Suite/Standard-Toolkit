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
/// Storage for KryptonContextMenuItem state values.
/// </summary>
public class PaletteContextMenuItemState : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContextMenuItemState class.
    /// </summary>
    /// <param name="redirect">Redirector for inheriting values.</param>
    public PaletteContextMenuItemState(PaletteContextMenuRedirect redirect)
        : this(redirect.ItemHighlight, redirect.ItemImage,
            redirect.ItemShortcutTextRedirect, redirect.ItemSplit,
            redirect.ItemTextStandardRedirect, redirect.ItemTextAlternateRedirect)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteContextMenuItemState class.
    /// </summary>
    /// <param name="redirect">Redirector for inheriting values.</param>
    public PaletteContextMenuItemState(PaletteContextMenuItemStateRedirect redirect)
        : this(redirect.ItemHighlight, redirect.ItemImage,
            redirect.ItemShortcutText, redirect.ItemSplit,
            redirect.ItemTextStandard, redirect.ItemTextAlternate)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteContextMenuItemState class.
    /// </summary>
    /// <param name="redirectItemHighlight">Redirector for ItemHighlight.</param>
    /// <param name="redirectItemImage">Redirector for ItemImage.</param>
    /// <param name="redirectItemShortcutText">Redirector for ItemShortcutText.</param>
    /// <param name="redirectItemSplit">Redirector for ItemSplit.</param>
    /// <param name="redirectItemTextAlternate">Redirector for ItemTextStandard.</param>
    /// <param name="redirectItemTextStandard">Redirector for ItemTextAlternate.</param>
    public PaletteContextMenuItemState(PaletteDoubleMetricRedirect? redirectItemHighlight,
        PaletteTripleJustImageRedirect redirectItemImage,
        PaletteContentInheritRedirect redirectItemShortcutText,
        PaletteDoubleRedirect redirectItemSplit,
        PaletteContentInheritRedirect redirectItemTextStandard,
        PaletteContentInheritRedirect redirectItemTextAlternate)
    {
        if (redirectItemHighlight is null)
        {
            throw new ArgumentNullException(nameof(redirectItemHighlight));
        }

        ItemHighlight = new PaletteDoubleMetric(redirectItemHighlight);
        ItemImage = new PaletteTripleJustImage(redirectItemImage);
        ItemShortcutText = new PaletteContentJustShortText(redirectItemShortcutText);
        ItemSplit = new PaletteDouble(redirectItemSplit);
        ItemTextStandard = new PaletteContentJustText(redirectItemTextStandard);
        ItemTextAlternate = new PaletteContentJustText(redirectItemTextAlternate);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ItemHighlight.IsDefault &&
                                      ItemImage.IsDefault &&
                                      ItemShortcutText.IsDefault &&
                                      ItemSplit.IsDefault &&
                                      ItemTextStandard.IsDefault &&
                                      ItemTextAlternate.IsDefault;

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
        common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuItemImage;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuItemImage;
        common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemImage;
        ItemImage.PopulateFromBase(state);
        common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemShortcutText;
        ItemShortcutText.PopulateFromBase(state);
        common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuSeparator;
        common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuSeparator;
        ItemSplit.PopulateFromBase(state);
        common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemTextStandard;
        ItemTextStandard.PopulateFromBase(state);
        common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemTextAlternate;
        ItemTextAlternate.PopulateFromBase(state);
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

    #region ItemImage
    /// <summary>
    /// Gets access to the item image appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item image appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleJustImage ItemImage { get; }

    private bool ShouldSerializeItemImage() => !ItemImage.IsDefault;

    #endregion

    #region ItemShortcutText
    /// <summary>
    /// Gets access to the item shortcut text appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item shortcut text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContentJustShortText ItemShortcutText { get; }

    private bool ShouldSerializeItemShortcutText() => !ItemShortcutText.IsDefault;

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

    #region ItemTextAlternate
    /// <summary>
    /// Gets access to the alternate item text appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining alternate item text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContentJustText ItemTextAlternate { get; }

    private bool ShouldSerializeItemTextAlternate() => !ItemTextAlternate.IsDefault;

    #endregion

    #region ItemTextStandard
    /// <summary>
    /// Gets access to the standard item text appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining standard item text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContentJustText ItemTextStandard { get; }

    private bool ShouldSerializeItemTextStandard() => !ItemTextStandard.IsDefault;

    #endregion
}