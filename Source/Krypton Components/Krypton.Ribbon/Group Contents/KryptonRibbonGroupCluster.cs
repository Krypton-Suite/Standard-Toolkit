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
/// Represents a ribbon group container that displays a cluster of buttons.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupCluster), "ToolboxBitmaps.KryptonRibbonGroupCluster.bmp")]
[Designer(typeof(KryptonRibbonGroupClusterDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Visible))]
public class KryptonRibbonGroupCluster : KryptonRibbonGroupContainer
{
    #region Instance Fields

    private GroupItemSize _itemSizeMax;
    private GroupItemSize _itemSizeMin;
    private GroupItemSize _itemSizeCurrent;
    private bool _visible;
    #endregion

    #region Events
    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the design time wants to add a button.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddButton;

    /// <summary>
    /// Occurs when the design time wants to add a color button.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddColorButton;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupCluster class.
    /// </summary>
    public KryptonRibbonGroupCluster()
    {
        // Default fields
        _itemSizeMax = GroupItemSize.Medium;
        _itemSizeMin = GroupItemSize.Small;
        _itemSizeCurrent = GroupItemSize.Medium;
        _visible = true;

        // Create collection for holding triple items
        Items = [];
        Items.Clearing += OnRibbonGroupClusterClearing;
        Items.Cleared += OnRibbonGroupClusterCleared;
        Items.Inserted += OnRibbonGroupClusterInserted;
        Items.Removed += OnRibbonGroupClusterRemoved;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the owning ribbon control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override KryptonRibbon? Ribbon
    {
        get => base.Ribbon;

        set
        {
            base.Ribbon = value;

            // Forward the reference to all children (just in case the children
            // are added before the this object is added to the owner)
            foreach (KryptonRibbonGroupItem item in Items)
            {
                item.Ribbon = value;
            }
        }
    }

    /// <summary>
    /// Gets access to the owning ribbon tab.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override KryptonRibbonTab? RibbonTab
    {
        get => base.RibbonTab;

        set
        {
            base.RibbonTab = value;

            // Forward the reference to all children (just in case the children
            // are added before the this object is added to the owner)
            foreach (KryptonRibbonGroupItem item in Items)
            {
                item.RibbonTab = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the button cluster container.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the button cluster is visible or hidden.")]
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

                // Update all contained elements to reflect the same sizing
                foreach (IRibbonGroupItem item in Items)
                {
                    item.ItemSizeMaximum = _itemSizeMax;
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

                // Update all contained elements to reflect the same sizing
                foreach (IRibbonGroupItem item in Items)
                {
                    item.ItemSizeMinimum = _itemSizeMin;
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
    /// Return the spacing gap between the provided previous item and this item.
    /// </summary>
    /// <param name="previousItem">Previous item.</param>
    /// <returns>Pixel gap between previous item and this item.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ItemGap(IRibbonGroupItem previousItem) =>
        // We always want 3 pixels space between previous item and us
        3;

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => _toolTipValues;

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
        return new ViewLayoutRibbonGroupCluster(ribbon, this, needPaint);
    }

    /// <summary>
    /// Gets the collection of ribbon group button cluster items.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of ribbon group button cluster items.")]
    [MergableProperty(false)]
    [Editor(typeof(KryptonRibbonGroupClusterCollectionEditor), typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonRibbonGroupClusterCollection Items { get; }

    /// <summary>
    /// Gets an array of all the contained components.
    /// </summary>
    /// <returns>Array of child components.</returns>
    public override Component[] GetChildComponents()
    {
        var array = new Component[Items.Count];
        Items.CopyTo(array, 0);
        return array;
    }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? ClusterView { get; set; }

    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    internal void OnDesignTimeAddButton() => DesignTimeAddButton?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddColorButton() => DesignTimeAddColorButton?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Ask the containers to check for command key processing
        foreach (KryptonRibbonGroupItem item in Items)
        {
            if (item.Visible && item.ProcessCmdKey(ref msg, keyData))
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Private
    private void OnRibbonGroupClusterClearing(object? sender, EventArgs e)
    {
        // Remove the back references
        foreach (IRibbonGroupItem item in Items)
        {
            item.Ribbon = null;
            item.RibbonTab = null;
            item.RibbonContainer = null;
        }
    }

    private void OnRibbonGroupClusterCleared(object? sender, EventArgs e)
    {
        // Only need to update display if this tab is selected
        if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
        {
            Ribbon.PerformNeedPaint(true);
        }
    }

    private void OnRibbonGroupClusterInserted(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
    {
        // Setup the back references
        e.Item!.Ribbon = Ribbon;
        e.Item.RibbonTab = RibbonTab;
        e.Item.RibbonContainer = this;

        // Force the child item to the fixed lines sizing
        e.Item.ItemSizeMaximum = ItemSizeMaximum;
        e.Item.ItemSizeMinimum = ItemSizeMinimum;
        e.Item.ItemSizeCurrent = ItemSizeCurrent;

        // Only need to update display if this tab is selected and the group is visible
        if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
        {
            Ribbon.PerformNeedPaint(true);
        }
    }

    private void OnRibbonGroupClusterRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
    {
        // Remove the back references
        e.Item!.Ribbon = null;
        e.Item.RibbonTab = null;
        e.Item.RibbonContainer = null;

        // Only need to update display if this tab is selected and the group was visible
        if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
        {
            Ribbon.PerformNeedPaint(true);
        }
    }
    #endregion
}