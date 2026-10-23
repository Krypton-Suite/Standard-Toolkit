#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a single ribbon tab.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonTab), "ToolboxBitmaps.KryptonRibbonTab.bmp")]
[Designer(typeof(KryptonRibbonTabDesigner))]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public class KryptonRibbonTab : Component
{
    #region Instance Fields
    private object? _tag;
    private string _text;
    private string _keyTip;
    private string _contextName;
    private bool _visible;
    private KryptonRibbon? _ribbon;

    #endregion

    #region Events
    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the design time wants to add a group.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddGroup;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonTab class.
    /// </summary>
    public KryptonRibbonTab()
    {
        // Default fields
        _text = "Tab";
        _keyTip = "T";
        _contextName = string.Empty;
        _visible = true;

        // Create the collection for defining groups
        Groups = [];
        Groups.Clearing += OnRibbonGroupsClearing;
        Groups.Cleared += OnRibbonGroupsCleared;
        Groups.Inserted += OnRibbonGroupsInserted;
        Groups.Removed += OnRibbonGroupsRemoved;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose of per-group resources
            foreach (KryptonRibbonGroup group in Groups)
            {
                @group.Dispose();
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the owning ribbon control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRibbon? Ribbon
    {
        get => _ribbon;

        internal set
        {
            _ribbon = value;

            // Forward the reference to all children (just in case the children
            // are added before the group is added to the tab, in which case the
            // reference will not be set as yet)
            foreach (KryptonRibbonGroup group in Groups)
            {
                @group.Ribbon = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the display text for the ribbon tab.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon tab display text.")]
    [DefaultValue("Tab")]
    public string Text
    {
        get => _text;

        set
        {
            // We never allow an empty text value
            if (string.IsNullOrEmpty(value))
            {
                value = @"Tab";
            }

            if (value != _text)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));

                // Only need to update display if this tab is visible
                if ((_ribbon != null) && Visible)
                {
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the key tip for the ribbon tab.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Ribbon tab key tip.")]
    [DefaultValue("T")]
    public string KeyTip
    {
        get => _keyTip;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                value = @"T";
            }

            _keyTip = value.ToUpper();
        }
    }

    /// <summary>
    /// Gets and sets the name of the context this tab is associated with.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Only display tab when this context is active.")]
    [DefaultValue("")]
    [AllowNull]
    public string ContextName
    {
        get => _contextName;

        set
        {
            // Always maintain a value reference
            value ??= string.Empty;

            if (value != _contextName)
            {
                _contextName = value;
                OnPropertyChanged(nameof(ContextName));

                // Only need to update display if this tab is visible
                if ((_ribbon != null) && Visible)
                {
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
    }

    private bool ShouldSerializeContextName() => !string.IsNullOrEmpty(_contextName);

    /// <summary>
    /// Reset the ContextName to the default value.
    /// </summary>
    private void ResetContextName() => ContextName = string.Empty;

    /// <summary>
    /// Gets and sets the visible state of the ribbon tab.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the ribbon tab is visible or hidden.")]
    [DefaultValue(true)]
    public bool Visible
    {
        get => _visible;

        set
        {
            if (value != _visible)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));

                // Must try and layout to show change
                if (_ribbon != null)
                {
                    // If selected, find another tab to select
                    if (_ribbon.SelectedTab == this)
                    {
                        _ribbon.ResetSelectedTab();
                    }

                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Make the ribbon tab visible.
    /// </summary>
    public void Show() => Visible = true;

    /// <summary>
    /// Make the ribbon tab hidden.
    /// </summary>
    public void Hide() => Visible = false;

    /// <summary>
    /// Gets the collection of ribbon tab groups.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of ribbon tab groups.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonRibbonGroupCollection Groups { get; }

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [Bindable(true)]
    public object? Tag
    {
        get => _tag;

        set
        {
            if (value != _tag)
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }
    }

    private bool ShouldSerializeTag() => Tag != null;

    private void ResetTag() => Tag = null;

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? TabView { get; set; }

    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal void OnDesignTimeAddGroup() => DesignTimeAddGroup?.Invoke(this, EventArgs.Empty);

    internal bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Ask the groups to check for command key processing
        foreach (KryptonRibbonGroup group in Groups)
        {
            if (@group.Visible && @group.ProcessCmdKey(ref msg, keyData))
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Private
    private void OnRibbonGroupsClearing(object? sender, EventArgs e)
    {
        // Remove the back references
        foreach (KryptonRibbonGroup group in Groups)
        {
            group.Ribbon = null;
            group.RibbonTab = null;
        }
    }

    private void OnRibbonGroupsCleared(object? sender, EventArgs e)
    {
        // Only need to update display if this tab is selected
        if ((_ribbon != null) && (_ribbon.SelectedTab == this))
        {
            _ribbon.PerformNeedPaint(true);
        }
    }

    private void OnRibbonGroupsInserted(object sender, TypedCollectionEventArgs<KryptonRibbonGroup> e)
    {
        // Setup the back references
        e.Item!.Ribbon = _ribbon;
        e.Item.RibbonTab = this;

        // Only need to update display if this tab is selected and the group is visible
        if ((_ribbon != null) && (_ribbon.SelectedTab == this) && Visible)
        {
            _ribbon.PerformNeedPaint(true);
        }
    }

    private void OnRibbonGroupsRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonGroup> e)
    {
        // Remove the back references
        e.Item!.Ribbon = null;
        e.Item.RibbonTab = null;

        // Only need to update display if this tab is selected and the group was visible
        if ((_ribbon != null) && (_ribbon.SelectedTab == this) && Visible)
        {
            _ribbon.PerformNeedPaint(true);
        }
    }
    #endregion
}