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
/// Represents a shortcut menu with Krypton palette styling.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonContextMenu), "ToolboxBitmaps.KryptonContextMenu.bmp")]
[DefaultEvent(nameof(Opening))]
[DefaultProperty(nameof(PaletteMode))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonContextMenuDesigner))]
[Description(@"Displays a shortcut menu in popup window.")]
public class KryptonContextMenu : Component
{
    #region Instance Fields

    private readonly PaletteRedirectContextMenu _redirectorImages;
    private readonly PaletteRedirect _redirector;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the context menu is opening.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when context menu is opening but not Displayed as yet.")]
    public event CancelEventHandler? Opening;

    /// <summary>
    /// Occurs when the context menu is opened.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the context menu is fully opened for display.")]
    public event EventHandler? Opened;

    /// <summary>
    /// Occurs when the context menu is about to close.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the context menu is about to close.")]
    public event CancelEventHandler? Closing;

    /// <summary>
    /// Occurs when the context menu has been closed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the context menu has been closed.")]
    public event ToolStripDropDownClosedEventHandler? Closed;
    #endregion

    #region Identity
    /// <summary>
    ///  Initialize a new instance of the KryptonContextMenu class.
    /// </summary>
    public KryptonContextMenu()
    {
        // Setup the need paint delegate
        NeedPaintHandler needPaintDelegate = OnNeedPaint;

        // Set default settings
        LocalCustomPalette = null;
        PaletteMode = PaletteMode.Global;
        Images = new ContextMenuImages(needPaintDelegate);
        _redirector = new PaletteRedirect(null);
        _redirectorImages = new PaletteRedirectContextMenu(_redirector, Images);
        Enabled = true;

        // Create the palette storage
        StateCommon = new PaletteContextMenuRedirect(_redirector, needPaintDelegate);
        StateNormal = new PaletteContextMenuItemState(StateCommon);
        StateDisabled = new PaletteContextMenuItemState(StateCommon);
        StateHighlight = new PaletteContextMenuItemStateHighlight(StateCommon);
        StateChecked = new PaletteContextMenuItemStateChecked(StateCommon);

        // Create the top level collection for menu items
        Items = [];
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Close();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the image value overrides.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image value overrides.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ContextMenuImages Images { get; }

    private bool ShouldSerializeImages() => !Images.IsDefault;

    /// <summary>
    /// Gets access to the common context menu appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common context menu appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the context menu disabled appearance values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context menu disabled appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemState StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the context menu normal appearance values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for context menu item normal appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemState StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the context menu normal appearance values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context menu checked appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemStateChecked StateChecked { get; }

    private bool ShouldSerializeStateChecked() => !StateChecked.IsDefault;

    /// <summary>
    /// Gets access to the context menu highlight appearance values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context menu highlight appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemStateHighlight StateHighlight { get; }

    private bool ShouldSerializeStateHighlight() => !StateHighlight.IsDefault;

    /// <summary>
    /// Collection of menu items for display.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Collection of menu items.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonContextMenuCollection Items { get; /*set;*/ }

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [Bindable(true)]
    [DefaultValue(null)]
    public object? Tag { get; set; }

    private bool ShouldSerializeTag() => Tag != null;

    /// <summary>
    /// </summary>
    public void ResetTag() => Tag = null;

    /// <summary>
    /// Gets and sets if the context menu is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the context menu is enabled.")]
    [DefaultValue(true)]
    [Bindable(true)]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Palette applied to drawing.")]
    public PaletteMode PaletteMode
    {
        [DebuggerStepThrough] get;
        set;
    }
    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;
    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Custom palette applied to drawing.")]
    [DefaultValue(null)]
    public KryptonCustomPaletteBase? LocalCustomPalette
    {
        [DebuggerStepThrough]
        get;
        set;
    }
    /// <summary>
    /// Resets the Palette property to its default value.
    /// </summary>
    private void ResetLocalCustomPalette() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets a reference to the caller that caused the context menu to be shown.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object Caller { get; private set; }

    /// <summary>
    /// Show the context menu at the current mouse location.
    /// </summary>
    /// <returns>Has the context menu become Displayed.</returns>
    /// <param name="caller">Reference to object causing the context menu to be shown.</param>
    public bool Show(object caller) =>
        // Without a screen location we just place it at the same location as the mouse
        Show(caller, Control.MousePosition);

    /// <summary>
    /// Show the context menu relative to the current mouse location.
    /// </summary>
    /// <param name="caller">Reference to object causing the context menu to be shown.</param>
    /// <param name="horz">Horizontal location relative to screen rectangle.</param>
    /// <param name="vert">Vertical location relative to screen rectangle.</param>
    /// <returns>Has the context menu become Displayed.</returns>
    public bool Show(object caller,
        KryptonContextMenuPositionH horz,
        KryptonContextMenuPositionV vert) =>
        // Without a screen location we just place it at the same location as the mouse
        Show(caller, Control.MousePosition, horz, vert);


    /// <summary>
    /// Show the context menu relative to the provided screen point.
    /// </summary>
    /// <param name="caller">Reference to object causing the context menu to be shown.</param>
    /// <param name="screenPt">Screen location.</param>
    /// <returns>Has the context menu become Displayed.</returns>
    public bool Show(object caller,
        Point screenPt) =>
        // Convert to a zero sized rectangle
        Show(caller, new Rectangle(screenPt, Size.Empty));

    /// <summary>
    /// Show the context menu relative to the provided screen rectangle.
    /// </summary>
    /// <param name="caller">Reference to object causing the context menu to be shown.</param>
    /// <param name="screenRect">Screen rectangle.</param>
    /// <returns>Has the context menu become Displayed.</returns>
    public bool Show(object caller,
        Rectangle screenRect) =>
        // When the relative position is not provided we assume a default 
        // of below and aligned to the left edge of the screen rectangle.
        Show(caller, screenRect, KryptonContextMenuPositionH.Left, KryptonContextMenuPositionV.Below);

    /// <summary>
    /// Show the context menu relative to the provided screen point.
    /// </summary>
    /// <param name="caller">Reference to object causing the context menu to be shown.</param>
    /// <param name="screenPt">Screen location.</param>
    /// <param name="horz">Horizontal location relative to screen rectangle.</param>
    /// <param name="vert">Vertical location relative to screen rectangle.</param>
    /// <returns>Has the context menu become Displayed.</returns>
    public bool Show(object caller,
        Point screenPt,
        KryptonContextMenuPositionH horz,
        KryptonContextMenuPositionV vert) =>
        // When providing just a point we turn this into a rectangle that happens to
        // have a zero size. We always position relative to a screen rectangle.
        Show(caller, new Rectangle(screenPt, Size.Empty), horz, vert);

    /// <summary>
    /// Show the context menu relative to the provided screen rectangle.
    /// </summary>
    /// <param name="caller">Reference to object causing the context menu to be shown.</param>
    /// <param name="screenRect">Screen rectangle.</param>
    /// <param name="horz">Horizontal location relative to screen rectangle.</param>
    /// <param name="vert">Vertical location relative to screen rectangle.</param>
    /// <returns>Has the context menu become Displayed.</returns>
    public bool Show(object caller,
        Rectangle screenRect,
        KryptonContextMenuPositionH horz,
        KryptonContextMenuPositionV vert) =>
        // By default we assume the context menu was not activated using the keyboard.
        Show(caller, screenRect, horz, vert, false, true);

    /// <summary>
    /// Show the context menu relative to the provided screen rectangle.
    /// </summary>
    /// <param name="caller">Reference to object causing the context menu to be shown.</param>
    /// <param name="screenRect">Screen rectangle.</param>
    /// <param name="horz">Horizontal location relative to screen rectangle.</param>
    /// <param name="vert">Vertical location relative to screen rectangle.</param>
    /// <param name="keyboardActivated">Was context menu initiated via a keyboard action.</param>
    /// <param name="constrain">Should size and position of menu be constrained by display size.</param>
    /// <returns>Has the context menu become Displayed.</returns>
    public bool Show(object caller,
        Rectangle screenRect,
        KryptonContextMenuPositionH horz,
        KryptonContextMenuPositionV vert,
        bool keyboardActivated,
        bool constrain)
    {
        var displayed = false;

        // Only need to show if not already displaying it
        if (VisualContextMenu == null)
        {
            // Remember the caller for us in events
            Caller = caller;

            // Give event handler a change to cancel the open request
            var cea = new CancelEventArgs();
            OnOpening(cea);

            if (!cea.Cancel)
            {
                // Set a default reason for the menu being dismissed
                CloseReason = ToolStripDropDownCloseReason.AppFocusChange;

                // Create the actual control used to show the context menu
                VisualContextMenu = CreateContextMenu(this, LocalCustomPalette, PaletteMode,
                    _redirector, _redirectorImages,
                    Items, Enabled, keyboardActivated);

                // Need to know when the visual control is removed
                VisualContextMenu.Disposed += OnContextMenuDisposed;

                // Request the menu be shown immediately
                VisualContextMenu.Show(screenRect, horz, vert, false, constrain);

                // Override the horz, vert setting so that sub menus appear right and below
                VisualContextMenu.ShowHorz = KryptonContextMenuPositionH.After;
                VisualContextMenu.ShowVert = KryptonContextMenuPositionV.Top;

                // Indicate the context menu is fully constructed and Displayed
                OnOpened(EventArgs.Empty);

                // The menu has actually become Displayed
                displayed = true;
            }
        }

        return displayed;
    }

    /// <summary>
    /// Close any showing context menu.
    /// </summary>
    public void Close() => Close(ToolStripDropDownCloseReason.CloseCalled);

    /// <summary>
    /// Close any showing context menu.
    /// </summary>
    /// <param name="reason">Reason why the context menu is being closed.</param>
    public void Close(ToolStripDropDownCloseReason reason)
    {
        // Remove any showing context menu
        if (VisualContextMenu != null)
        {
            CloseReason = reason;
            VisualPopupManager.Singleton.EndPopupTracking(VisualContextMenu);
        }
    }

    /// <summary>
    /// Test for the provided shortcut and perform relevant action if a match is found.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if shortcut was handled, otherwise false.</returns>
    public bool ProcessShortcut(Keys keyData) => Items.ProcessShortcut(keyData);

    #endregion

    #region Protected Virtual
    // ReSharper disable VirtualMemberNeverOverridden.Global
    /// <summary>
    /// Create a new visual context menu for showing the defined items.
    /// </summary>
    /// <param name="kcm">Owning KryptonContextMenu instance.</param>
    /// <param name="palette">Drawing palette.</param>
    /// <param name="paletteMode">Drawing palette mode.</param>
    /// <param name="redirector">Redirector for sourcing base values.</param>
    /// <param name="redirectorImages">Redirector for sourcing base images.</param>
    /// <param name="items">Collection of menu items.</param>
    /// <param name="enabled">Enabled state of the menu.</param>
    /// <param name="keyboardActivated">True is menu was keyboard initiated.</param>
    /// <returns>VisualContextMenu reference.</returns>
    protected virtual VisualContextMenu CreateContextMenu(KryptonContextMenu kcm,
        PaletteBase? palette,
        PaletteMode paletteMode,
        PaletteRedirect redirector,
        PaletteRedirectContextMenu redirectorImages,
        KryptonContextMenuCollection items,
        bool enabled,
        bool keyboardActivated) =>
        new VisualContextMenu(kcm, palette, paletteMode, redirector, redirectorImages, items, enabled,
            keyboardActivated);

    /// <summary>
    /// Raises the Opening event.
    /// </summary>
    /// <param name="e">A CancelEventArgs containing the event data.</param>
    protected virtual void OnOpening(CancelEventArgs e) => Opening?.Invoke(this, e);

    /// <summary>
    /// Raises the Opened event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnOpened(EventArgs e) => Opened?.Invoke(this, e);

    /// <summary>
    /// Raises the Closing event.
    /// </summary>
    /// <param name="e">A CancelEventArgs containing the event data.</param>
    protected internal virtual void OnClosing(CancelEventArgs e) => Closing?.Invoke(this, e);

    /// <summary>
    /// Raises the Closed event.
    /// </summary>
    /// <param name="e">An ToolStripDropDownClosedEventArgs containing the event data.</param>
    protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e) => Closed?.Invoke(this, e);
    // ReSharper restore VirtualMemberNeverOverridden.Global
    #endregion

    #region Internal
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal ToolStripDropDownCloseReason CloseReason { get; set; }

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    internal VisualContextMenu? VisualContextMenu { get; private set; }

    #endregion

    #region Implementation
    private void PerformNeedPaint(bool needLayout) => OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));

    private void OnNeedPaint(object? sender, [DisallowNull] NeedLayoutEventArgs e)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Pass request onto the displaying control if we have one
        VisualContextMenu?.PerformNeedPaint(e.NeedLayout);
    }

    private void OnContextMenuDisposed(object? sender, EventArgs e)
    {
        // Should still be caching a reference to actual display control
        if (VisualContextMenu != null)
        {
            // Unhook from control, so it can be garbage collected
            VisualContextMenu.Disposed -= OnContextMenuDisposed;

            // Copy to self the close reason
            if (VisualContextMenu.CloseReason.HasValue)
            {
                CloseReason = VisualContextMenu.CloseReason.Value;
            }

            // No longer need to cache reference
            VisualContextMenu = null;

            // Notify event handlers the context menu has been closed and why it closed
            OnClosed(new ToolStripDropDownClosedEventArgs(CloseReason));
        }
    }
    #endregion
}