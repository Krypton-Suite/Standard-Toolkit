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
/// Implementation class used to provide context menu details to view elements.
/// </summary>
public class ContextMenuProvider : IContextMenuProvider
{
    #region Instance Fields

    private readonly IContextMenuProvider? _parent;
    private ToolStripDropDownCloseReason? _closeReason;

    #endregion

    #region Events
    /// <summary>
    /// Raises the Dispose event.
    /// </summary>
    public event EventHandler? Dispose;

    /// <summary>
    /// Raises the Closing event.
    /// </summary>
    public event CancelEventHandler? Closing;

    /// <summary>
    /// Raises the Close event.
    /// </summary>
    public event EventHandler<CloseReasonEventArgs>? Close;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ContextMenuProvider class.
    /// </summary>
    /// <param name="provider">Original provider.</param>
    /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
    /// <param name="viewManager">Reference to view manager.</param>
    /// <param name="viewColumns">Columns view element.</param>
    public ContextMenuProvider(IContextMenuProvider provider,
        ViewContextMenuManager viewManager,
        ViewLayoutStack viewColumns,
        NeedPaintHandler needPaintDelegate)
    {
        _parent = provider;
        ProviderEnabled = provider.ProviderEnabled;
        ProviderCanCloseMenu = provider.ProviderCanCloseMenu;
        ProviderViewManager = viewManager;
        ProviderViewColumns = viewColumns;
        ProviderStateCommon = provider.ProviderStateCommon;
        ProviderStateDisabled = provider.ProviderStateDisabled;
        ProviderStateNormal = provider.ProviderStateNormal;
        ProviderStateHighlight = provider.ProviderStateHighlight;
        ProviderStateChecked = provider.ProviderStateChecked;
        ProviderImages = provider.ProviderImages;
        ProviderPalette = provider.ProviderPalette;
        ProviderPaletteMode = provider.ProviderPaletteMode;
        ProviderRedirector = provider.ProviderRedirector;
        ProviderNeedPaintDelegate = needPaintDelegate;
        ProviderShowHorz = provider.ProviderShowHorz;
        ProviderShowVert = provider.ProviderShowVert;
    }

    /// <summary>
    /// Initialize a new instance of the ContextMenuProvider class.
    /// </summary>
    /// <param name="contextMenu">Originating context menu instance.</param>
    /// <param name="viewManager">Reference to view manager.</param>
    /// <param name="viewColumns">Columns view element.</param>
    /// <param name="palette">Local palette setting to use initially.</param>
    /// <param name="paletteMode">Palette mode setting to use initially.</param>
    /// <param name="redirector">Redirector used for obtaining palette values.</param>
    /// <param name="redirectorImages">Redirector used for obtaining images.</param>
    /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
    /// <param name="enabled">Enabled state of the context menu.</param>
    public ContextMenuProvider(KryptonContextMenu contextMenu,
        ViewContextMenuManager viewManager,
        ViewLayoutStack viewColumns,
        PaletteBase? palette,
        PaletteMode paletteMode,
        PaletteRedirect redirector,
        PaletteRedirectContextMenu redirectorImages,
        NeedPaintHandler needPaintDelegate,
        bool enabled)
    {
        ProviderShowHorz = KryptonContextMenuPositionH.Left;
        ProviderShowVert = KryptonContextMenuPositionV.Below;

        ProviderEnabled = enabled;
        ProviderViewManager = viewManager;
        ProviderViewColumns = viewColumns;
        ProviderStateCommon = contextMenu.StateCommon;
        ProviderStateDisabled = contextMenu.StateDisabled;
        ProviderStateNormal = contextMenu.StateNormal;
        ProviderStateHighlight = contextMenu.StateHighlight;
        ProviderStateChecked = contextMenu.StateChecked;
        ProviderImages = redirectorImages;
        ProviderPalette = palette;
        ProviderPaletteMode = paletteMode;
        ProviderRedirector = redirector;
        ProviderNeedPaintDelegate = needPaintDelegate;
        ProviderCanCloseMenu = true;
    }

    /// <summary>
    /// Initialize a new instance of the ContextMenuProvider class.
    /// </summary>
    /// <param name="viewManager">Reference to view manager.</param>
    /// <param name="viewColumns">Columns view element.</param>
    /// <param name="palette">Local palette setting to use initially.</param>
    /// <param name="paletteMode">Palette mode setting to use initially.</param>
    /// <param name="stateCommon">State used to provide common palette values.</param>
    /// <param name="stateNormal">State used to provide normal palette values.</param>
    /// <param name="stateDisabled">State used to provide disabled palette values.</param>
    /// <param name="stateHighlight">State used to provide highlight palette values.</param>
    /// <param name="stateChecked">State used to provide checked palette values.</param>
    /// <param name="redirector">Redirector used for obtaining palette values.</param>
    /// <param name="redirectorImages">Redirector used for obtaining images.</param>
    /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
    /// <param name="enabled">Enabled state of the context menu.</param>
    public ContextMenuProvider(ViewContextMenuManager viewManager,
        ViewLayoutStack viewColumns,
        PaletteBase? palette,
        PaletteMode paletteMode,
        PaletteContextMenuRedirect stateCommon,
        PaletteContextMenuItemState stateDisabled,
        PaletteContextMenuItemState stateNormal,
        PaletteContextMenuItemStateHighlight stateHighlight,
        PaletteContextMenuItemStateChecked stateChecked,
        PaletteRedirect redirector,
        PaletteRedirectContextMenu redirectorImages,
        NeedPaintHandler needPaintDelegate,
        bool enabled)
    {
        ProviderShowHorz = KryptonContextMenuPositionH.Left;
        ProviderShowVert = KryptonContextMenuPositionV.Below;

        ProviderEnabled = enabled;
        ProviderViewManager = viewManager;
        ProviderViewColumns = viewColumns;
        ProviderStateCommon = stateCommon;
        ProviderStateDisabled = stateDisabled;
        ProviderStateNormal = stateNormal;
        ProviderStateHighlight = stateHighlight;
        ProviderStateChecked = stateChecked;
        ProviderImages = redirectorImages;
        ProviderPalette = palette;
        ProviderPaletteMode = paletteMode;
        ProviderRedirector = redirector;
        ProviderNeedPaintDelegate = needPaintDelegate;
    }
    #endregion

    #region IContextMenuProvider
    /// <summary>
    /// Fires the Dispose event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    public void OnDispose(EventArgs e) => Dispose?.Invoke(this, e);

    /// <summary>
    /// Fires the Closing event.
    /// </summary>
    /// <param name="cea">An CancelEventArgs containing the event data.</param>
    public void OnClosing(CancelEventArgs cea)
    {
        if (_parent != null)
        {
            _parent.OnClosing(cea);
        }
        else
        {
            Closing?.Invoke(this, cea);
        }
    }

    /// <summary>
    /// Fires the Close event.
    /// </summary>
    /// <param name="e">A CloseReasonEventArgs containing the event data.</param>
    public void OnClose(CloseReasonEventArgs e)
    {
        if (_parent != null)
        {
            _parent.OnClose(e);
        }
        else
        {
            Close?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Does this provider have a parent provider.
    /// </summary>
    public bool HasParentProvider => _parent != null;

    /// <summary>
    /// Is the entire context menu enabled.
    /// </summary>
    public bool ProviderEnabled { get; }

    /// <summary>
    /// Is context menu capable of being closed.
    /// </summary>
    public bool ProviderCanCloseMenu { get; }

    /// <summary>
    /// Should the sub menu be shown at fixed screen location for this menu item.
    /// </summary>
    /// <param name="menuItem">Menu item that needs to show sub menu.</param>
    /// <returns>True if the sub menu should be a fixed size.</returns>
    public bool ProviderShowSubMenuFixed(KryptonContextMenuItem menuItem) => HasParentProvider && _parent!.ProviderShowSubMenuFixed(menuItem);

    /// <summary>
    /// Should the sub menu be shown at fixed screen location for this menu item.
    /// </summary>
    /// <param name="menuItem">Menu item that needs to show sub menu.</param>
    /// <returns>Screen rectangle to use as display rectangle.</returns>
    public Rectangle ProviderShowSubMenuFixedRect(KryptonContextMenuItem menuItem) =>
        HasParentProvider ? _parent!.ProviderShowSubMenuFixedRect(menuItem) : Rectangle.Empty;

    /// <summary>
    /// Sets the reason for the context menu being closed.
    /// </summary>
    public ToolStripDropDownCloseReason? ProviderCloseReason 
    { 
        get => _parent != null ? _parent.ProviderCloseReason : _closeReason;

        set
        {
            if (_parent != null)
            {
                _parent.ProviderCloseReason = value;
            }
            else
            {
                _closeReason = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the horizontal setting used to position the menu.
    /// </summary>
    public KryptonContextMenuPositionH ProviderShowHorz { get; set; }

    /// <summary>
    /// Gets and sets the vertical setting used to position the menu.
    /// </summary>
    public KryptonContextMenuPositionV ProviderShowVert { get; set; }

    /// <summary>
    /// Gets access to the layout for context menu columns.
    /// </summary>
    public ViewLayoutStack ProviderViewColumns { get; }

    /// <summary>
    /// Gets access to the context menu specific view manager.
    /// </summary>
    public ViewContextMenuManager ProviderViewManager { get; }

    /// <summary>
    /// Gets access to the context menu common state.
    /// </summary>
    public PaletteContextMenuRedirect ProviderStateCommon { get; }

    /// <summary>
    /// Gets access to the context menu disabled state.
    /// </summary>
    public PaletteContextMenuItemState ProviderStateDisabled { get; }

    /// <summary>
    /// Gets access to the context menu normal state.
    /// </summary>
    public PaletteContextMenuItemState ProviderStateNormal { get; }

    /// <summary>
    /// Gets access to the context menu highlight state.
    /// </summary>
    public PaletteContextMenuItemStateHighlight ProviderStateHighlight { get; }

    /// <summary>
    /// Gets access to the context menu checked state.
    /// </summary>
    public PaletteContextMenuItemStateChecked ProviderStateChecked { get; }

    /// <summary>
    /// Gets access to the context menu images.
    /// </summary>
    public PaletteRedirectContextMenu ProviderImages { get; }

    /// <summary>
    /// Gets access to the custom palette.
    /// </summary>
    public PaletteBase? ProviderPalette { get; }

    /// <summary>
    /// Gets access to the palette mode.
    /// </summary>
    public PaletteMode ProviderPaletteMode { get; }

    /// <summary>
    /// Gets access to the context menu redirector.
    /// </summary>
    public PaletteRedirect ProviderRedirector { get; }

    /// <summary>
    /// Gets a delegate used to indicate a repaint is required.
    /// </summary>
    public NeedPaintHandler ProviderNeedPaintDelegate { get; }

    #endregion
}