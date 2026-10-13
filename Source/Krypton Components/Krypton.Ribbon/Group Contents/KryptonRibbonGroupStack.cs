#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Ribbon group container that holds any number of items inside the three-line group height.
/// </summary>
/// <remarks>
/// <see cref="RibbonGroupStackLayout.Columns"/> fills down columns of three.
/// <see cref="RibbonGroupStackLayout.Adaptive"/> uses a single row of large items and wraps medium and small items into columns of three.
/// </remarks>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupStack), "ToolboxBitmaps.KryptonRibbonGroupStack.bmp")]
[Designer("Krypton.Ribbon.KryptonRibbonGroupStackDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Visible))]
public class KryptonRibbonGroupStack : KryptonRibbonGroupContainer
{
    #region Instance Fields

    private GroupItemSize _itemSizeMax;
    private GroupItemSize _itemSizeMin;
    private GroupItemSize _itemSizeCurrent;
    private RibbonItemAlignment _itemAlignment;
    private RibbonGroupStackLayout _stackLayout;
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
    /// Occurs when the design time wants to add a check box.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddCheckBox;

    /// <summary>
    /// Occurs when the design time wants to add a radio button.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddRadioButton;

    /// <summary>
    /// Occurs when the design time wants to add a label.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddLabel;

    /// <summary>
    /// Occurs when the design time wants to add a custom control.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddCustomControl;

    /// <summary>
    /// Occurs when the design time wants to add a cluster.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddCluster;

    /// <summary>
    /// Occurs when the design time wants to add a text box.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddTextBox;

    /// <summary>
    /// Occurs when the design time wants to add a masked text box.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddMaskedTextBox;

    /// <summary>
    /// Occurs when the design time wants to add a rich text box.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddRichTextBox;

    /// <summary>
    /// Occurs when the design time wants to add a combobox.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddComboBox;

    /// <summary>
    /// Occurs when the design time wants to add a numeric up down.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddNumericUpDown;

    /// <summary>
    /// Occurs when the design time wants to add a domain up down.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddDomainUpDown;

    /// <summary>
    /// Occurs when the design time wants to add a date time picker.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddDateTimePicker;

    /// <summary>
    /// Occurs when the design time wants to add a track bar.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddTrackBar;

    /// <summary>
    /// Occurs when the design time wants to add a theme combobox.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event EventHandler? DesignTimeAddThemeComboBox;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;

    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the <see cref="KryptonRibbonGroupStack"/> class.
    /// </summary>
    public KryptonRibbonGroupStack()
    {
        _visible = true;
        _itemAlignment = RibbonItemAlignment.Near;
        _stackLayout = RibbonGroupStackLayout.Columns;
        _itemSizeMax = GroupItemSize.Large;
        _itemSizeMin = GroupItemSize.Small;
        _itemSizeCurrent = GroupItemSize.Large;

        Items = new KryptonRibbonGroupStackCollection();
        Items.Clearing += OnRibbonGroupStackClearing;
        Items.Cleared += OnRibbonGroupStackCleared;
        Items.Inserted += OnRibbonGroupStackInserted;
        Items.Removed += OnRibbonGroupStackRemoved;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (KryptonRibbonGroupItem item in Items!)
            {
                item.Dispose();
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
    public override KryptonRibbon? Ribbon
    {
        get => base.Ribbon;

        set
        {
            base.Ribbon = value;

            foreach (KryptonRibbonGroupItem item in Items!)
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

            foreach (KryptonRibbonGroupItem item in Items!)
            {
                item.RibbonTab = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets how to align a child that is narrower than its column, and how a short column sits in the row.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How to align items that are narrower than their column.")]
    [DefaultValue(typeof(RibbonItemAlignment), "Near")]
    public RibbonItemAlignment ItemAlignment
    {
        get => _itemAlignment;

        set
        {
            if (_itemAlignment != value)
            {
                _itemAlignment = value;
                OnPropertyChanged(nameof(ItemAlignment));
            }
        }
    }

    /// <summary>
    /// Gets and sets whether items always fill columns of three, or use a large horizontal row that wraps when smaller.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Columns fills columns of three at every size. Adaptive uses one row of large items and wraps smaller items into columns of three.")]
    [DefaultValue(typeof(RibbonGroupStackLayout), "Columns")]
    public RibbonGroupStackLayout StackLayout
    {
        get => _stackLayout;

        set
        {
            if (_stackLayout != value)
            {
                _stackLayout = value;
                ApplyChildSizeLimits();
                OnPropertyChanged(nameof(StackLayout));
            }
        }
    }

    /// <summary>
    /// Gets and sets the visible state of the stack group container.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the stack group container is visible or hidden.")]
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
    /// Gets and sets the maximum allowed size of the container.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Maximum size of the stack container.")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(GroupItemSize), "Large")]
    [RefreshProperties(RefreshProperties.All)]
    public GroupItemSize MaximumSize
    {
        get => ItemSizeMaximum;
        set => ItemSizeMaximum = value;
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the container.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Minimum size of the stack container.")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof(GroupItemSize), "Small")]
    [RefreshProperties(RefreshProperties.All)]
    public GroupItemSize MinimumSize
    {
        get => ItemSizeMinimum;
        set => ItemSizeMinimum = value;
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
            if (_itemSizeMax != value)
            {
                _itemSizeMax = value;

                switch (_itemSizeMax)
                {
                    case GroupItemSize.Medium:
                        if (_itemSizeMin == GroupItemSize.Large)
                        {
                            _itemSizeMin = GroupItemSize.Medium;
                        }
                        break;
                    case GroupItemSize.Small:
                        _itemSizeMin = GroupItemSize.Small;
                        break;
                }

                ApplyChildSizeLimits();
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
            if (_itemSizeMin != value)
            {
                _itemSizeMin = value;

                switch (_itemSizeMin)
                {
                    case GroupItemSize.Large:
                        _itemSizeMax = GroupItemSize.Large;
                        break;
                    case GroupItemSize.Medium:
                        if (_itemSizeMax == GroupItemSize.Small)
                        {
                            _itemSizeMax = GroupItemSize.Medium;
                        }
                        break;
                }

                ApplyChildSizeLimits();
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

                foreach (IRibbonGroupItem item in Items!)
                {
                    item.ItemSizeCurrent = ToChildSize(value);
                }

                OnPropertyChanged(nameof(ItemSizeCurrent));
            }
        }
    }

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
        return new ViewLayoutRibbonGroupStack(ribbon, this, needPaint);
    }

    /// <summary>
    /// Gets the collection of ribbon group stack items.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of ribbon group stack items.")]
    [MergableProperty(false)]
    [Editor(typeof(KryptonRibbonGroupStackCollectionEditor), typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonRibbonGroupStackCollection? Items { get; }

    /// <summary>
    /// Gets an array of all the contained components.
    /// </summary>
    /// <returns>Array of child components.</returns>
    public override Component[] GetChildComponents()
    {
        var array = new Component[Items!.Count];
        Items.CopyTo(array, 0);
        return array;
    }

    /// <summary>
    /// Internal design time properties.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? StackView { get; set; }

    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    /// <summary>
    /// Maps a container size to the size applied to each child.
    /// </summary>
    /// <param name="containerSize">Size currently requested for the stack.</param>
    /// <returns>Size the children should use.</returns>
    internal GroupItemSize ToChildSize(GroupItemSize containerSize)
    {
        if (_stackLayout == RibbonGroupStackLayout.Adaptive)
        {
            return containerSize;
        }

        switch (containerSize)
        {
            case GroupItemSize.Large:
                return GroupItemSize.Medium;
            case GroupItemSize.Medium:
            case GroupItemSize.Small:
                return GroupItemSize.Small;
            default:
                Debug.Assert(false);
                return GroupItemSize.Medium;
        }
    }

    internal void OnDesignTimeAddButton() => DesignTimeAddButton?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddColorButton() => DesignTimeAddColorButton?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddCheckBox() => DesignTimeAddCheckBox?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddRadioButton() => DesignTimeAddRadioButton?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddLabel() => DesignTimeAddLabel?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddCustomControl() => DesignTimeAddCustomControl?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddCluster() => DesignTimeAddCluster?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddTextBox() => DesignTimeAddTextBox?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddMaskedTextBox() => DesignTimeAddMaskedTextBox?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddRichTextBox() => DesignTimeAddRichTextBox?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddComboBox() => DesignTimeAddComboBox?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddNumericUpDown() => DesignTimeAddNumericUpDown?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddDomainUpDown() => DesignTimeAddDomainUpDown?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddDateTimePicker() => DesignTimeAddDateTimePicker?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddTrackBar() => DesignTimeAddTrackBar?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeAddThemeComboBox() => DesignTimeAddThemeComboBox?.Invoke(this, EventArgs.Empty);

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        foreach (KryptonRibbonGroupItem item in Items!)
        {
            if (item.ProcessCmdKey(ref msg, keyData))
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Private
    private void ApplyChildSizeLimits()
    {
        foreach (IRibbonGroupItem item in Items!)
        {
            item.ItemSizeMaximum = ToChildSize(ItemSizeMaximum);
            item.ItemSizeMinimum = ToChildSize(ItemSizeMinimum);
            item.ItemSizeCurrent = ToChildSize(ItemSizeCurrent);
        }
    }

    private void OnRibbonGroupStackClearing(object? sender, EventArgs e)
    {
        foreach (KryptonRibbonGroupItem item in Items!)
        {
            item.Ribbon = null;
            item.RibbonTab = null;
            item.RibbonContainer = null;
        }
    }

    private void OnRibbonGroupStackCleared(object? sender, EventArgs e)
    {
        if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
        {
            Ribbon.PerformNeedPaint(true);
        }
    }

    private void OnRibbonGroupStackInserted(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
    {
        e.Item!.Ribbon = Ribbon;
        e.Item.RibbonTab = RibbonTab;
        e.Item.RibbonContainer = this;
        e.Item.ItemSizeMaximum = ToChildSize(ItemSizeMaximum);
        e.Item.ItemSizeMinimum = ToChildSize(ItemSizeMinimum);
        e.Item.ItemSizeCurrent = ToChildSize(ItemSizeCurrent);

        if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
        {
            Ribbon.PerformNeedPaint(true);
        }
    }

    private void OnRibbonGroupStackRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
    {
        e.Item!.Ribbon = null;
        e.Item.RibbonTab = null;
        e.Item.RibbonContainer = null;

        if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
        {
            Ribbon.PerformNeedPaint(true);
        }
    }
    #endregion
}
