#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Implementation class used to provide application button context menu details to view elements.
/// </summary>
public class AppButtonMenuProvider : IContextMenuProvider
{
    #region Instance Fields

    private readonly IContextMenuProvider? _parent;
    private ToolStripDropDownCloseReason? _closeReason;
    private readonly KryptonContextMenuItemCollection _menuCollection;

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
    /// <param name="viewManager">View manager used to organize keyboard events.</param>
    /// <param name="menuCollection">Top level set of menu items.</param>
    /// <param name="viewColumns">Stack used for adding new columns.</param>
    /// <param name="palette">Local palette setting to use initially.</param>
    /// <param name="paletteMode">Palette mode setting to use initially.</param>
    /// <param name="redirector">Redirector used for obtaining palette values.</param>
    /// <param name="needPaintDelegate">Delegate used to when paint changes occur.</param>
    public AppButtonMenuProvider(ViewContextMenuManager viewManager,
        KryptonContextMenuItemCollection menuCollection,
        ViewLayoutStack viewColumns,
        PaletteBase? palette,
        PaletteMode paletteMode,
        PaletteRedirect redirector,
        NeedPaintHandler needPaintDelegate)
    {
        // Store incoming state
        ProviderViewManager = viewManager;
        _menuCollection = menuCollection;
        ProviderViewColumns = viewColumns;
        ProviderPalette = palette;
        ProviderPaletteMode = paletteMode;
        ProviderRedirector = redirector;
        ProviderNeedPaintDelegate = needPaintDelegate;

        // Create all other state
        _parent = null;
        ProviderEnabled = true;
        ProviderCanCloseMenu = true;
        ProviderShowHorz = KryptonContextMenuPositionH.After;
        ProviderShowVert = KryptonContextMenuPositionV.Top;
        ProviderStateCommon = new PaletteContextMenuRedirect(redirector, needPaintDelegate);
        ProviderStateNormal = new PaletteContextMenuItemState(ProviderStateCommon);
        ProviderStateDisabled = new PaletteContextMenuItemState(ProviderStateCommon);
        ProviderStateHighlight = new PaletteContextMenuItemStateHighlight(ProviderStateCommon);
        ProviderStateChecked = new PaletteContextMenuItemStateChecked(ProviderStateCommon);
        ProviderImages = new PaletteRedirectContextMenu(redirector, new ContextMenuImages(needPaintDelegate));
    }
    #endregion

    #region FixedViewBase
    /// <summary>
    /// Gets and sets the view to use as the fixed sub menu area.
    /// </summary>
    public ViewBase FixedViewBase { get; set; }

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
        else if (Close != null)
        {
            _closeReason = e.CloseReason;
            Close(this, e);
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
    public bool ProviderShowSubMenuFixed(KryptonContextMenuItem menuItem) => (FixedViewBase != null) && _menuCollection.Contains(menuItem);

    /// <summary>
    /// The rectangle used for showing a fixed location for the sub menu.
    /// </summary>
    /// <param name="menuItem">Menu item that needs to show sub menu.</param>
    /// <returns>Screen rectangle to use as display rectangle.</returns>
    public Rectangle ProviderShowSubMenuFixedRect(KryptonContextMenuItem menuItem)
    {
        if (ProviderShowSubMenuFixed(menuItem))
        {
            Rectangle screenRect = FixedViewBase.OwningControl!.RectangleToScreen(FixedViewBase.ClientRectangle);
            screenRect.Y++;
            screenRect.Width -= 3;
            screenRect.Height -= 4;
            return screenRect;
        }
        else
        {
            return Rectangle.Empty;
        }
    }

    /// <summary>
    /// Sets the reason for the context menu being closed.
    /// </summary>
    public ToolStripDropDownCloseReason? ProviderCloseReason
    {
        get => _parent?.ProviderCloseReason ?? _closeReason;

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