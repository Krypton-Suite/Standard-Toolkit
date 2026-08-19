#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Office 2007+-style shortcut menu: a <see cref="KryptonMiniToolbar"/> in its own popup, paired with a <see cref="KryptonContextMenu"/>.
/// </summary>
[ToolboxItem(true)]
[DefaultEvent(nameof(Opening))]
[DefaultProperty(nameof(Menu))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonEnhancedContextMenuDesigner))]
[Description(@"Displays an Office-style shortcut menu with an optional Mini Toolbar.")]
public class KryptonEnhancedContextMenu : Component
{
    #region Instance Fields

    private bool _disposed;
    private bool _ownsMenu;
    private bool _ownsMiniToolbar;
    private KryptonContextMenu _menu;
    private KryptonMiniToolbar _miniToolbar;
    private VisualEnhancedContextMenu? _popup;
    private VisualMiniToolbarPopup? _toolbarPopup;
    private int _miniToolbarGap;
    private const int DefaultMiniToolbarGap = 2;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the enhanced context menu is opening.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the enhanced context menu is opening.")]
    public event CancelEventHandler? Opening;

    /// <summary>
    /// Occurs when the enhanced context menu is opened.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the enhanced context menu is opened.")]
    public event EventHandler? Opened;

    /// <summary>
    /// Occurs when the enhanced context menu is about to close.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the enhanced context menu is about to close.")]
    public event CancelEventHandler? Closing;

    /// <summary>
    /// Occurs when the enhanced context menu has closed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the enhanced context menu has closed.")]
    public event EventHandler? Closed;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEnhancedContextMenu"/> class.
    /// </summary>
    public KryptonEnhancedContextMenu()
    {
        _miniToolbar = new KryptonMiniToolbar();
        _miniToolbar.ItemClick += OnMiniToolbarItemClick;
        _ownsMiniToolbar = true;
        _menu = new KryptonContextMenu();
        _ownsMenu = true;
        ShowMiniToolbar = true;
        MiniToolbarPosition = KryptonMiniToolbarPosition.Auto;
        KeepMiniToolbarAfterCommand = true;
        _miniToolbarGap = DefaultMiniToolbarGap;
        Enabled = true;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            Close();
            _miniToolbar.ItemClick -= OnMiniToolbarItemClick;
            if (_ownsMiniToolbar)
            {
                _miniToolbar.Dispose();
            }

            if (_ownsMenu)
            {
                _menu.Dispose();
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the Mini Toolbar shown above or below the menu.
    /// Assign a form-level <see cref="KryptonMiniToolbar"/> to share items with selection fade, or configure the owned instance in the designer.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Mini Toolbar shown with the context menu. Configure Items here, or assign a standalone KryptonMiniToolbar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonMiniToolbar MiniToolbar
    {
        get => _miniToolbar;
        set
        {
            ThrowHelper.ThrowIfNull(value);
            if (!ReferenceEquals(_miniToolbar, value))
            {
                _miniToolbar.ItemClick -= OnMiniToolbarItemClick;
                if (_ownsMiniToolbar)
                {
                    _miniToolbar.Dispose();
                }

                _miniToolbar = value!;
                _ownsMiniToolbar = false;
                _miniToolbar.ItemClick += OnMiniToolbarItemClick;
            }
        }
    }

    /// <summary>
    /// Gets or sets the context menu displayed under the Mini Toolbar.
    /// Configure <see cref="KryptonContextMenu.Items"/> on this instance, or assign an existing menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Context menu displayed with the Mini Toolbar. Configure Items in the designer, or assign an existing KryptonContextMenu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonContextMenu Menu
    {
        get => _menu;
        set
        {
            ThrowHelper.ThrowIfNull(value);
            if (!ReferenceEquals(_menu, value))
            {
                if (_ownsMenu)
                {
                    _menu.Dispose();
                }

                _menu = value!;
                _ownsMenu = false;
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the Mini Toolbar is shown with the menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the Mini Toolbar is shown with the menu.")]
    [DefaultValue(true)]
    public bool ShowMiniToolbar { get; set; }

    /// <summary>
    /// Gets or sets the Mini Toolbar position relative to the menu.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Position of the Mini Toolbar relative to the menu.")]
    [DefaultValue(KryptonMiniToolbarPosition.Auto)]
    public KryptonMiniToolbarPosition MiniToolbarPosition { get; set; }

    /// <summary>
    /// Gets or sets the pixel gap between the Mini Toolbar and the paired context menu.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Pixel gap between the Mini Toolbar and the paired context menu.")]
    [DefaultValue(DefaultMiniToolbarGap)]
    public int MiniToolbarGap
    {
        get => _miniToolbarGap;
        set => _miniToolbarGap = Math.Max(0, value);
    }

    /// <summary>
    /// Gets or sets whether clicking a Mini Toolbar command dismisses the menu list but keeps the Mini Toolbar.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Clicking a Mini Toolbar command dismisses the menu list but keeps the Mini Toolbar.")]
    [DefaultValue(true)]
    public bool KeepMiniToolbarAfterCommand { get; set; }

    /// <summary>
    /// Gets or sets whether the enhanced menu is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the enhanced context menu is enabled.")]
    [DefaultValue(true)]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets whether the enhanced menu or its paired Mini Toolbar is currently displayed.
    /// </summary>
    [Browsable(false)]
    public bool IsShowing => IsMenuShowing || IsToolbarShowing;

    /// <summary>
    /// Show the enhanced context menu at the current mouse location.
    /// </summary>
    /// <param name="caller">Reference to the object causing the menu to be shown.</param>
    /// <returns>True if displayed.</returns>
    public bool Show(object caller) => Show(caller, Control.MousePosition);

    /// <summary>
    /// Show the enhanced context menu at the provided screen point.
    /// </summary>
    /// <param name="caller">Reference to the object causing the menu to be shown.</param>
    /// <param name="screenPt">Screen location.</param>
    /// <returns>True if displayed.</returns>
    public bool Show(object caller, Point screenPt) =>
        Show(caller, new Rectangle(screenPt, Size.Empty),
            KryptonContextMenuPositionH.Left, KryptonContextMenuPositionV.Below, false, true);

    /// <summary>
    /// Show the enhanced context menu relative to the provided screen rectangle.
    /// </summary>
    /// <param name="caller">Reference to the object causing the menu to be shown.</param>
    /// <param name="screenRect">Screen rectangle.</param>
    /// <param name="horz">Horizontal alignment.</param>
    /// <param name="vert">Vertical alignment.</param>
    /// <param name="keyboardActivated">True if opened from the keyboard.</param>
    /// <param name="constrain">True to constrain to the working area.</param>
    /// <returns>True if displayed.</returns>
    public bool Show(object caller,
        Rectangle screenRect,
        KryptonContextMenuPositionH horz,
        KryptonContextMenuPositionV vert,
        bool keyboardActivated,
        bool constrain)
    {
        if (!Enabled)
        {
            return false;
        }

        Close();

        var cea = new CancelEventArgs();
        Opening?.Invoke(this, cea);
        if (cea.Cancel)
        {
            return false;
        }

        _popup = new VisualEnhancedContextMenu(this, _menu, keyboardActivated);
        _popup.Disposed += OnPopupDisposed;
        Size menuSize = _popup.GetPreferredSize();
        Rectangle workingArea = Screen.GetWorkingArea(screenRect);
        if (constrain)
        {
            menuSize.Width = Math.Min(workingArea.Width, menuSize.Width);
            menuSize.Height = Math.Min(workingArea.Height, menuSize.Height);
        }

        Rectangle menuBounds = new Rectangle(PositionMenu(screenRect, menuSize, horz, vert, constrain, workingArea), menuSize);

        var position = MiniToolbarPosition;
        if (position == KryptonMiniToolbarPosition.Auto)
        {
            position = screenRect.Top < 80 ? KryptonMiniToolbarPosition.Below : KryptonMiniToolbarPosition.Above;
        }

        if (ShowMiniToolbar && MiniToolbar.Items.Count > 0)
        {
            PaletteBase palette = MiniToolbar.ResolvePalette();
            _toolbarPopup = new VisualMiniToolbarPopup(MiniToolbar, palette.GetRenderer(), false);
            _toolbarPopup.Disposed += OnToolbarPopupDisposed;
            _toolbarPopup.LayoutStrip();
            Size toolbarSize = _toolbarPopup.CalculatePreferredSize();
            if (constrain)
            {
                toolbarSize.Width = Math.Min(workingArea.Width, toolbarSize.Width);
                toolbarSize.Height = Math.Min(workingArea.Height, toolbarSize.Height);
            }

            Rectangle toolbarBounds = PositionToolbar(menuBounds, toolbarSize, position, workingArea, MiniToolbarGap);
            // Track the Mini Toolbar first so it is stacked under the menu: a toolbar click
            // dismisses the menu popup but can leave the bar (KeepMiniToolbarAfterCommand).
            _toolbarPopup.Show(toolbarBounds);
        }

        _popup.ShowAt(menuBounds);
        Opened?.Invoke(this, EventArgs.Empty);
        return true;
    }

    /// <summary>
    /// Close any showing enhanced context menu.
    /// </summary>
    public void Close() => Close(ToolStripDropDownCloseReason.CloseCalled);

    /// <summary>
    /// Close any showing enhanced context menu.
    /// </summary>
    /// <param name="reason">Reason the menu is closing.</param>
    public void Close(ToolStripDropDownCloseReason reason)
    {
        if (_popup != null && !_popup.IsDisposed)
        {
            VisualPopupManager.Singleton.EndPopupTracking(_popup);
        }

        if (_toolbarPopup != null && !_toolbarPopup.IsDisposed)
        {
            VisualPopupManager.Singleton.EndPopupTracking(_toolbarPopup);
        }
    }

    #endregion

    #region Internal

    /// <summary>
    /// Raises <see cref="Closing"/> for the visual popup provider.
    /// </summary>
    /// <param name="e">Cancel arguments.</param>
    internal void RaiseClosing(CancelEventArgs e) => Closing?.Invoke(this, e);

    #endregion

    #region Implementation

    private bool IsMenuShowing => _popup != null && !_popup.IsDisposed && _popup.Visible;

    private bool IsToolbarShowing => _toolbarPopup != null && !_toolbarPopup.IsDisposed && _toolbarPopup.Visible;

    private void OnMiniToolbarItemClick(object? sender, KryptonMiniToolbarItemClickEventArgs e)
    {
        if (KeepMiniToolbarAfterCommand)
        {
            if (_popup != null && !_popup.IsDisposed)
            {
                VisualPopupManager.Singleton.EndPopupTracking(_popup);
            }
        }
        else
        {
            Close(ToolStripDropDownCloseReason.ItemClicked);
        }
    }

    private void OnPopupDisposed(object? sender, EventArgs e)
    {
        if (_popup != null)
        {
            _popup.Disposed -= OnPopupDisposed;
            _popup = null;
        }

        TryRaiseClosed();
    }

    private void OnToolbarPopupDisposed(object? sender, EventArgs e)
    {
        if (_toolbarPopup != null)
        {
            _toolbarPopup.Disposed -= OnToolbarPopupDisposed;
            _toolbarPopup = null;
        }

        TryRaiseClosed();
    }

    private void TryRaiseClosed()
    {
        if (_popup == null && _toolbarPopup == null)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }
    }

    private static Point PositionMenu(Rectangle screenRect,
        Size menuSize,
        KryptonContextMenuPositionH horz,
        KryptonContextMenuPositionV vert,
        bool constrain,
        Rectangle workingArea)
    {
        var screenPt = Point.Empty;
        screenPt.X = horz switch
        {
            KryptonContextMenuPositionH.After => screenRect.Right,
            KryptonContextMenuPositionH.Before => screenRect.Left - menuSize.Width,
            KryptonContextMenuPositionH.Left => screenRect.Left,
            KryptonContextMenuPositionH.Right => screenRect.Right - menuSize.Width,
            _ => screenPt.X
        };
        screenPt.Y = vert switch
        {
            KryptonContextMenuPositionV.Above => screenRect.Top - menuSize.Height,
            KryptonContextMenuPositionV.Below => screenRect.Bottom,
            KryptonContextMenuPositionV.Top => screenRect.Top,
            KryptonContextMenuPositionV.Bottom => screenRect.Bottom - menuSize.Height,
            _ => screenPt.Y
        };

        if (constrain)
        {
            screenPt.X = Math.Max(screenPt.X, workingArea.X);
            screenPt.Y = Math.Max(screenPt.Y, workingArea.Y);
            if ((screenPt.X + menuSize.Width) > workingArea.Right)
            {
                screenPt.X = workingArea.Right - menuSize.Width;
            }

            if ((screenPt.Y + menuSize.Height) > workingArea.Bottom)
            {
                screenPt.Y = workingArea.Bottom - menuSize.Height;
            }
        }

        return screenPt;
    }

    private static Rectangle PositionToolbar(Rectangle menuBounds,
        Size toolbarSize,
        KryptonMiniToolbarPosition position,
        Rectangle workingArea,
        int gap)
    {
        var above = position != KryptonMiniToolbarPosition.Below;
        var x = menuBounds.X;
        var y = above
            ? menuBounds.Y - gap - toolbarSize.Height
            : menuBounds.Bottom + gap;

        if (above && y < workingArea.Top)
        {
            y = menuBounds.Bottom + gap;
        }
        else if (!above && (y + toolbarSize.Height) > workingArea.Bottom)
        {
            y = menuBounds.Y - gap - toolbarSize.Height;
        }

        if (x + toolbarSize.Width > workingArea.Right)
        {
            x = Math.Max(workingArea.Left, workingArea.Right - toolbarSize.Width);
        }

        if (x < workingArea.Left)
        {
            x = workingArea.Left;
        }

        if (y < workingArea.Top)
        {
            y = workingArea.Top;
        }

        return new Rectangle(new Point(x, y), toolbarSize);
    }

    #endregion
}
