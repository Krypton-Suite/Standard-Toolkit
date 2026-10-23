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
/// Represents a ribbon group cluster button.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupClusterButton), "ToolboxBitmaps.KryptonRibbonGroupClusterButton.bmp")]
[Designer(typeof(KryptonRibbonGroupClusterButtonDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(ButtonType))]
public class KryptonRibbonGroupClusterButton : KryptonRibbonGroupItem
{
    #region Static Fields
    private static readonly Image? _defaultButtonImageSmall = GenericImageResources.ButtonImageSmall;
    #endregion

    #region Instance Fields
    private bool _enabled;
    private bool _visible;
    private bool _checked;
    private string _textLine;
    private string _keyTip;
    private Image? _imageSmall;
    private GroupItemSize _itemSizeMax;
    private GroupItemSize _itemSizeMin;
    private GroupItemSize _itemSizeCurrent;
    private KryptonCommand? _command;
    private GroupButtonType _buttonType;
    private ContextMenuStrip? _contextMenuStrip;
    private KryptonContextMenu? _kryptonContextMenu;
    private EventHandler? _kcmFinishDelegate;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the button is clicked.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs when the button is clicked.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the drop-down button type is pressed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs when the drop-down button type is pressed.")]
    public event EventHandler<ContextMenuArgs>? DropDown;

    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupClusterButton class.
    /// </summary>
    public KryptonRibbonGroupClusterButton()
    {
        // Default fields
        _enabled = true;
        _visible = true;
        _checked = false;
        _textLine = string.Empty;
        _keyTip = "B";
        ShortcutKeys = Keys.None;
        _itemSizeMax = GroupItemSize.Medium;
        _itemSizeMin = GroupItemSize.Small;
        _itemSizeCurrent = GroupItemSize.Medium;
        _imageSmall = _defaultButtonImageSmall;
        _buttonType = GroupButtonType.Push;
        _contextMenuStrip = null;
        _kryptonContextMenu = null;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the display text line for the button.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Button display text line.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
    public string TextLine
    {
        get => _textLine;

        set
        {
            if (value != _textLine)
            {
                _textLine = value;
                OnPropertyChanged(nameof(TextLine));
            }
        }
    }

    /// <summary>
    /// Gets and sets the key tip for the ribbon group cluster button.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon group cluster button key tip.")]
    [DefaultValue("B")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"B";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the small button image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Small button image.")]
    [RefreshProperties(RefreshProperties.All)]
    public Image? ImageSmall
    {
        get => _imageSmall;

        set
        {
            if (_imageSmall != value)
            {
                _imageSmall = value;
                OnPropertyChanged(nameof(ImageSmall));
            }
        }
    }

    private bool ShouldSerializeImageSmall() => ImageSmall != _defaultButtonImageSmall;

    /// <summary>
    /// Gets and sets the visible state of the cluster button.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the cluster button is visible or hidden.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool Visible
    {
        get => _visible;

        set
        {
            if (value != _visible)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Make the ribbon group visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon group hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets and sets the enabled state of the group entry.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group button is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (value != _enabled)
            {
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }

    /// <summary>
    /// Gets and sets the checked state of the group entry.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group button is checked.")]
    [DefaultValue(false)]
    public bool Checked
    {
        get => _checked;

        set
        {
            if (value != _checked)
            {
                _checked = value;
                OnPropertyChanged(nameof(Checked));
            }
        }
    }

    /// <summary>
    /// Gets and sets the operation of the group button.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines how the group button operation.")]
    [DefaultValue(typeof(GroupButtonType), "Push")]
    public GroupButtonType ButtonType
    {
        get => _buttonType;

        set
        {
            if (value != _buttonType)
            {
                _buttonType = value;
                OnPropertyChanged(nameof(ButtonType));
            }
        }
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to fire click event of the cluster button.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;


    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => _toolTipValues;

    /// <summary>
    /// Gets and sets the context strip for showing when the button is pressed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Context menu strip to be shown when the button is pressed.")]
    [DefaultValue(null)]
    public ContextMenuStrip? ContextMenuStrip
    {
        get => _contextMenuStrip;

        set
        {
            if (value != _contextMenuStrip)
            {
                _contextMenuStrip = value;
                OnPropertyChanged(nameof(ContextMenuStrip));
            }
        }
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu for showing when the button is pressed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"KryptonContextMenu to be shown when the button is pressed.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _kryptonContextMenu;

        set
        {
            if (value != _kryptonContextMenu)
            {
                _kryptonContextMenu = value;
                OnPropertyChanged(nameof(KryptonContextMenu));
            }
        }
    }

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the cluster button.")]
    [DefaultValue(null)]
    public KryptonCommand? KryptonCommand
    {
        get => _command;

        set
        {
            if (_command != value)
            {
                if (_command != value)
                {
                    if (_command != null)
                    {
                        _command.PropertyChanged -= OnCommandPropertyChanged;
                    }

                    _command = value;
                    OnPropertyChanged(nameof(KryptonCommand));

                    if (_command != null)
                    {
                        _command.PropertyChanged += OnCommandPropertyChanged;
                    }
                }

            }
        }
    }

    /// <summary>
    /// Gets and sets the maximum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMaximum
    {
        get => _itemSizeMax;

        set
        {
            // We can never be bigger than medium
            if (value == GroupItemSize.Large)
            {
                value = GroupItemSize.Medium;
            }

            if (_itemSizeMax != value)
            {
                _itemSizeMax = value;

                if (_itemSizeMax == GroupItemSize.Small)
                {
                    _itemSizeMin = GroupItemSize.Small;
                }

                OnPropertyChanged(nameof(ItemSizeMaximum));
            }
        }
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMinimum
    {
        get => _itemSizeMin;

        set
        {
            // We can never be bigger than medium
            if (value == GroupItemSize.Large)
            {
                value = GroupItemSize.Medium;
            }

            if (_itemSizeMin != value)
            {
                _itemSizeMin = value;

                if (_itemSizeMin == GroupItemSize.Medium)
                {
                    _itemSizeMax = GroupItemSize.Medium;
                }

                OnPropertyChanged(nameof(ItemSizeMinimum));
            }
        }
    }

    /// <summary>
    /// Gets and sets the current item size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeCurrent
    {
        get => _itemSizeCurrent;

        set
        {
            if (_itemSizeCurrent != value)
            {
                _itemSizeCurrent = value;
                OnPropertyChanged(nameof(ItemSizeCurrent));
            }
        }
    }

    /// <summary>
    /// Creates an appropriate view element for this item.
    /// </summary>
    /// <param name="ribbon">Reference to the owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    /// <returns>ViewBase derived instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ViewBase CreateView(KryptonRibbon ribbon,
        NeedPaintHandler needPaint)
    {
        _toolTipValues.NeedPaint = needPaint;
        return new ViewDrawRibbonGroupClusterButton(ribbon, this, needPaint);
    }

    /// <summary>
    /// Generates a Click event for a button.
    /// </summary>
    public void PerformClick() => PerformClick(null);

    /// <summary>
    /// Generates a Click event for a button.
    /// </summary>
    /// <param name="finishDelegate">Delegate fired during event processing.</param>
    public void PerformClick(EventHandler? finishDelegate) => OnClick(finishDelegate);

    /// <summary>
    /// Generates a DropDown event for a button.
    /// </summary>
    public void PerformDropDown() => PerformDropDown(null);

    /// <summary>
    /// Generates a DropDown event for a button.
    /// </summary>
    /// <param name="finishDelegate">Delegate fired during event processing.</param>
    public void PerformDropDown(EventHandler? finishDelegate) => OnDropDown(finishDelegate);

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? ClusterButtonView { get; set; }

    #endregion

    #region Protected
    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected virtual void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "Text":
                OnPropertyChanged(nameof(TextLine));
                break;
            case nameof(ImageSmall):
                OnPropertyChanged(nameof(ImageSmall));
                break;
            case nameof(Enabled):
                OnPropertyChanged(nameof(Enabled));
                break;
            case nameof(Checked):
                OnPropertyChanged(nameof(Checked));
                break;
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="finishDelegate">Delegate fired during event processing.</param>
    protected virtual void OnClick(EventHandler? finishDelegate)
    {
        var fireDelegate = true;

        if (!Ribbon!.InDesignMode)
        {
            // Events only occur when enabled
            if (Enabled)
            {
                // A check button should always toggle state
                if (ButtonType == GroupButtonType.Check)
                {
                    // Push back the change to the attached command
                    if (KryptonCommand != null)
                    {
                        KryptonCommand.Checked = !KryptonCommand.Checked;
                    }
                    else
                    {
                        Checked = !Checked;
                    }
                }

                // In showing a popup we fire the delegate before the click so that the
                // minimized popup is removed out of the way before the event is handled
                // because if the event shows a dialog then it would appear behind the popup
                if (VisualPopupManager.Singleton.CurrentPopup != null)
                {
                    // Do we need to fire a delegate stating the click processing has finished?
                    if (fireDelegate)
                    {
                        finishDelegate?.Invoke(this, EventArgs.Empty);
                    }

                    fireDelegate = false;
                }

                // Generate actual click event
                Click?.Invoke(this, EventArgs.Empty);

                // Clicking the button should execute the associated command
                KryptonCommand?.PerformExecute();
            }
        }

        // Do we need to fire a delegate stating the click processing has finished?
        if (fireDelegate)
        {
            finishDelegate?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Raises the DropDown event.
    /// </summary>
    /// <param name="finishDelegate">Delegate fired during event processing.</param>
    protected virtual void OnDropDown(EventHandler? finishDelegate)
    {
        var fireDelegate = true;

        if (!Ribbon!.InDesignMode)
        {
            // Events only occur when enabled
            if (Enabled)
            {
                if (ButtonType is GroupButtonType.DropDown or GroupButtonType.Split)
                {
                    if (KryptonContextMenu != null)
                    {
                        var contextArgs = new ContextMenuArgs(KryptonContextMenu);

                        // Generate an event giving a chance for the krypton context menu strip to 
                        // be shown to be provided/modified or the action even to be cancelled
                        DropDown?.Invoke(this, contextArgs);

                        // If user did not cancel and there is still a krypton context menu strip to show
                        if (contextArgs is { Cancel: false, KryptonContextMenu: not null })
                        {
                            var screenRect = Rectangle.Empty;

                            // Convert the view for the button into screen coordinates
                            if ((Ribbon != null) && (ClusterButtonView != null))
                            {
                                screenRect = Ribbon.ViewRectangleToScreen(ClusterButtonView);
                            }

                            if (CommonHelper.ValidKryptonContextMenu(contextArgs.KryptonContextMenu))
                            {
                                // Cache the finish delegate to call when the menu is closed
                                _kcmFinishDelegate = finishDelegate;

                                // Show at location we were provided, but need to convert to screen coordinates
                                contextArgs.KryptonContextMenu.Closed += OnKryptonContextMenuClosed;
                                if (contextArgs.KryptonContextMenu.Show(this, new Point(screenRect.X, screenRect.Bottom + 1)))
                                {
                                    fireDelegate = false;
                                }
                            }
                        }
                    }
                    else if (ContextMenuStrip != null)
                    {
                        var contextArgs = new ContextMenuArgs(ContextMenuStrip);

                        // Generate an event giving a chance for the context menu strip to be
                        // shown to be provided/modified or the action even to be cancelled
                        DropDown?.Invoke(this, contextArgs);

                        // If user did not cancel and there is still a context menu strip to show
                        if (contextArgs is { Cancel: false, ContextMenuStrip: not null })
                        {
                            var screenRect = Rectangle.Empty;

                            // Convert the view for the button into screen coordinates
                            if ((Ribbon != null) && (ClusterButtonView != null))
                            {
                                screenRect = Ribbon.ViewRectangleToScreen(ClusterButtonView);
                            }

                            if (CommonHelper.ValidContextMenuStrip(contextArgs.ContextMenuStrip))
                            {
                                // Do not fire the delegate in this routine, wait for the popup manager to show it
                                fireDelegate = false;

                                //...show the context menu below and at th left of the button
                                VisualPopupManager.Singleton.ShowContextMenuStrip(contextArgs.ContextMenuStrip,
                                    new Point(screenRect.X, screenRect.Bottom + 1),
                                    finishDelegate);
                            }
                        }
                    }
                }
            }
        }

        // Do we need to fire a delegate stating the click processing has finished?
        if (fireDelegate)
        {
            finishDelegate?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Only interested in key processing if this button definition 
        // is enabled and itself and all parents are also visible
        if (Enabled && ChainVisible)
        {
            // Do we have a shortcut definition for ourself?
            if (ShortcutKeys != Keys.None)
            {
                // Does it match the incoming key combination?
                if (ShortcutKeys == keyData)
                {
                    // Button type determines what event to fire
                    switch (ButtonType)
                    {
                        case GroupButtonType.Push:
                        case GroupButtonType.Check:
                            PerformClick();
                            return true;

                        case GroupButtonType.DropDown:
                        case GroupButtonType.Split:
                            PerformDropDown();
                            return true;

                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            DebugTools.NotImplemented(ButtonType.ToString());
                            break;
                    }

                    return true;
                }
            }

            // Check the types that have a relevant context menu strip
            if (ButtonType is GroupButtonType.DropDown or GroupButtonType.Split)
            {
                if (KryptonContextMenu != null)
                {
                    if (KryptonContextMenu.ProcessShortcut(keyData))
                    {
                        return true;
                    }
                }

                if (ContextMenuStrip != null)
                {
                    if (CommonHelper.CheckContextMenuForShortcut(ContextMenuStrip, ref msg, keyData))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    #endregion

    #region Implementation
    private void OnKryptonContextMenuClosed(object? sender, EventArgs e)
    {
        if (sender is KryptonContextMenu kcm)
        {
            kcm.Closed -= OnKryptonContextMenuClosed;

            // Fire any associated finish delegate
            if (_kcmFinishDelegate != null)
            {
                _kcmFinishDelegate(this, e);
                _kcmFinishDelegate = null;
            }
        }
    }
    #endregion
}