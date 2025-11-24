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
/// Redirection for KryptonContextMenuItem state values.
/// </summary>
public class PaletteContextMenuItemStateRedirect : Storage
{
    #region Instance Fields
    private readonly PaletteRedirectDouble _itemHighlight;
    private readonly PaletteRedirectTriple _itemImage;
    private readonly PaletteRedirectContent _itemShortcutText;
    private readonly PaletteRedirectDouble _itemSplit;
    private readonly PaletteRedirectContent _itemStandard;
    private readonly PaletteRedirectContent _itemAlternate;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContextMenuItemStateRedirect class.
    /// </summary>
    public PaletteContextMenuItemStateRedirect()
    {
        _itemHighlight = new PaletteRedirectDouble();
        _itemImage = new PaletteRedirectTriple();
        _itemShortcutText = new PaletteRedirectContent();
        _itemSplit = new PaletteRedirectDouble();
        _itemStandard = new PaletteRedirectContent();
        _itemAlternate = new PaletteRedirectContent();

        ItemHighlight = new PaletteDoubleMetricRedirect(_itemHighlight, PaletteBackStyle.ContextMenuItemHighlight, PaletteBorderStyle.ContextMenuItemHighlight);
        ItemImage = new PaletteTripleJustImageRedirect(_itemImage, PaletteBackStyle.ContextMenuItemImage, PaletteBorderStyle.ContextMenuItemImage, PaletteContentStyle.ContextMenuItemImage);
        ItemShortcutText = new PaletteContentInheritRedirect(_itemShortcutText, PaletteContentStyle.ContextMenuItemShortcutText);
        ItemSplit = new PaletteDoubleRedirect(_itemSplit, PaletteBackStyle.ContextMenuSeparator, PaletteBorderStyle.ContextMenuSeparator);
        ItemTextStandard = new PaletteContentInheritRedirect(_itemStandard, PaletteContentStyle.ContextMenuItemTextStandard);
        ItemTextAlternate = new PaletteContentInheritRedirect(_itemAlternate, PaletteContentStyle.ContextMenuItemTextAlternate);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => true;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="provider">Provider for acquiring context menu information.</param>
    public void SetRedirector(IContextMenuProvider provider)
    {
        _itemHighlight.Target = provider.ProviderStateCommon.ItemHighlight?.GetRedirector();
        _itemImage.Target = provider.ProviderStateCommon.ItemImage.GetRedirector();
        _itemShortcutText.Target = provider.ProviderStateCommon.ItemShortcutTextRedirect.GetRedirector();
        _itemSplit.Target = provider.ProviderStateCommon.ItemSplit.GetRedirector();
        _itemStandard.Target = provider.ProviderStateCommon.ItemTextStandardRedirect.GetRedirector();
        _itemAlternate.Target = provider.ProviderStateCommon.ItemTextAlternateRedirect.GetRedirector();

        _itemHighlight.SetRedirectStates(provider.ProviderStateDisabled.ItemHighlight, provider.ProviderStateNormal.ItemHighlight);
        _itemImage.SetRedirectStates(provider.ProviderStateDisabled.ItemImage, provider.ProviderStateNormal.ItemImage);
        _itemShortcutText.SetRedirectStates(provider.ProviderStateDisabled.ItemShortcutText, provider.ProviderStateNormal.ItemShortcutText);
        _itemSplit.SetRedirectStates(provider.ProviderStateDisabled.ItemSplit, provider.ProviderStateNormal.ItemSplit, provider.ProviderStateHighlight.ItemSplit, provider.ProviderStateHighlight.ItemSplit);
        _itemStandard.SetRedirectStates(provider.ProviderStateDisabled.ItemTextStandard, provider.ProviderStateNormal.ItemTextStandard);
        _itemAlternate.SetRedirectStates(provider.ProviderStateDisabled.ItemTextAlternate, provider.ProviderStateNormal.ItemTextAlternate);
    }
    #endregion

    #region ItemHighlight
    /// <summary>
    /// Gets access to the item image highlight entries.
    /// </summary>
    public PaletteDoubleMetricRedirect? ItemHighlight { get; }

    #endregion

    #region ItemImage
    /// <summary>
    /// Gets access to the item image appearance entries.
    /// </summary>
    public PaletteTripleJustImageRedirect ItemImage { get; }

    #endregion

    #region ItemShortcutText
    /// <summary>
    /// Gets access to the item shortcut text appearance entries.
    /// </summary>
    public PaletteContentInheritRedirect ItemShortcutText { get; }

    #endregion

    #region ItemSplit
    /// <summary>
    /// Gets access to the item split appearance entries.
    /// </summary>
    public PaletteDoubleRedirect ItemSplit { get; }

    #endregion

    #region ItemTextAlternate
    /// <summary>
    /// Gets access to the alternate item text appearance entries.
    /// </summary>
    public PaletteContentInheritRedirect ItemTextAlternate { get; }

    #endregion

    #region ItemTextStandard
    /// <summary>
    /// Gets access to the standard item text appearance entries.
    /// </summary>
    public PaletteContentInheritRedirect ItemTextStandard { get; }

    #endregion
}