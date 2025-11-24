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
/// Base class that all menu types must derive from and implement.
/// </summary>
[DesignerCategory(@"code")]
public abstract class KryptonContextMenuItemBase : Component, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _visible;
    private ToolTipValues _toolTipValues;
    private VisualPopupToolTip? _visualPopupToolTip;
    private IContextMenuProvider? _provider;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the <see cref="KryptonContextMenuItem"/> wants to display a tooltip.
    /// </summary>
    [Description(@"Occurs when the KryptonContextMenuItem wants to display a tooltip.")]
    [Category(@"Behavior")]
    public event EventHandler<ToolTipNeededEventArgs>? ToolTipNeeded;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItem class.
    /// </summary>
    protected KryptonContextMenuItemBase()
    {
        _visible = true;
        _toolTipValues = new ToolTipValues(null, GetDpiFactor);
        ToolTipManager = new ToolTipManager(_toolTipValues);
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;
    }

    private float GetDpiFactor() =>
        (_visualPopupToolTip != null)
            ? _visualPopupToolTip.DeviceDpi / 96F
            : 1F;

    #endregion

    #region Public
    /// <summary>
    /// Returns the number of child menu items.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract int ItemChildCount { get; }

    /// <summary>
    /// Returns the indexed child menu item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract KryptonContextMenuItemBase? this[int index] { get; }

    /// <summary>
    /// Test for the provided shortcut and perform relevant action if a match is found.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if shortcut was handled, otherwise false.</returns>
    public abstract bool ProcessShortcut(Keys keyData);

    /// <summary>
    /// Returns a view appropriate for this item based on the object it is inside.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="parent">Owning object reference.</param>
    /// <param name="columns">Containing columns.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    /// <returns>ViewBase that is the root of the view hierarchy being added.</returns>
    /// <remarks>Make sure to call `SetProvider(provider);`
    /// </remarks>
    public abstract ViewBase GenerateView(IContextMenuProvider provider,
        object parent,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn);

    internal void SetProvider([DisallowNull] IContextMenuProvider provider)
    {
        Debug.Assert(provider.ProviderRedirector != null);
        _provider = provider;
        for (var idx = 0; idx < ItemChildCount; ++idx)
        {
            this[idx]?.SetProvider(provider);
        }
    }

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [KryptonPersist]
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [DefaultValue(null)]
    [Bindable(true)]
    public object? Tag { get; set; }

    /// <summary>
    /// Gets and sets if the item is visible in the context menu.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Determines if the item is visible in the context menu.")]
    [DefaultValue(true)]
    [Bindable(true)]
    public bool Visible
    {
        get => _visible;

        set
        {
            if (_visible != value)
            {
                _visible = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Visible)));
            }
        }
    }

    /// <summary>
    /// Gets access to the ToolTipValues content.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"ToolTip")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public ToolTipValues ToolTipValues
    {
        get => _toolTipValues;
        set => _toolTipValues = value;
    }

    private bool ShouldSerializeToolTipValues() => !ToolTipValues.IsDefault;

    /// <summary>
    /// Resets the ToolTipValues property to its default value.
    /// </summary>
    public void ResetToolTipValues() => ToolTipValues.Reset();

    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ToolTipNeeded event.
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnToolTipNeeded(ToolTipNeededEventArgs e) => ToolTipNeeded?.Invoke(this, e);
    #endregion

    #region Internal
    /// <summary>
    /// Gets access to the ToolTipManager used for displaying tool tips.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal ToolTipManager? ToolTipManager { get; }

    internal void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        //if (!IsDisposed)
        {
            // Do not show tooltips when the form we are in does not have focus
            //Form? topForm = FindForm();
            //if (topForm is { ContainsFocus: false })
            //{
            //    return;
            //}

            // Never show tooltips are design time
            //if (!DesignMode)
            if (_toolTipValues.EnableToolTips)
            {
                // Remove any currently showing tooltip
                _visualPopupToolTip?.Dispose();

                // See if there is a tooltip to display for the new selection.
                var args = new ToolTipNeededEventArgs(0, this)
                {
                    Heading = _toolTipValues.Heading,
                    Description = _toolTipValues.Description,
                    Icon = _toolTipValues.Image
                };
                OnToolTipNeeded(args);
                if (args.IsEmpty)
                {
                    return;
                }

                _toolTipValues.Heading = args.Heading;
                _toolTipValues.Description = args.Description;
                _toolTipValues.Image = args.Icon;

                // Create the actual tooltip popup object
                if (_provider != null)
                {
                    var renderer = _provider.ProviderRedirector.Target!.GetRenderer();
                    _visualPopupToolTip = new VisualPopupToolTip(_provider.ProviderRedirector,
                        _toolTipValues,
                        renderer,
                        PaletteBackStyle.ControlToolTip,
                        PaletteBorderStyle.ControlToolTip,
                        CommonHelper.ContentStyleFromLabelStyle(_toolTipValues.ToolTipStyle),
                        _toolTipValues.ToolTipShadow);
                    _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;
                    _visualPopupToolTip.ShowRelativeTo(e.Target, e.ControlMousePosition);
                }
            }
        }
    }

    internal void OnCancelToolTip(object? _, EventArgs e) =>
        // Remove any currently showing tooltip
        _visualPopupToolTip?.Dispose();

    internal void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page any more
        _visualPopupToolTip = null;
    }
    #endregion
}