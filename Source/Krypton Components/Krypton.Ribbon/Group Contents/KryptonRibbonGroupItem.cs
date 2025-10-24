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
/// Represents the base class for all ribbon group items.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public abstract class KryptonRibbonGroupItem : Component,
    IRibbonGroupItem,
    IBindableComponent
{
    #region Instance Fields
    private object? _tag;

#pragma warning disable CS1591
#pragma warning disable CS3008 // Identifier is not CLS-compliant
    protected ToolTipValues _toolTipValues;
#pragma warning restore CS3008 // Identifier is not CLS-compliant
#pragma warning restore CS1591

    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupItem class.
    /// </summary>
    protected KryptonRibbonGroupItem() =>
        // Do the Tooltip Magic
        _toolTipValues = new ToolTipValues(null/*NeedPaintDelegate*/, GetDpiFactor); // Must be replaced by appropriate call

    private float GetDpiFactor() =>
        (Ribbon != null)
            ? Ribbon.DeviceDpi / 96F
            : 1.0f;

    #endregion Identity

    #region Public
    /// <summary>
    /// Gets access to the owning ribbon control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual KryptonRibbon? Ribbon { get; set; }

    /// <summary>
    /// Gets access to the owning ribbon tab.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual KryptonRibbonTab? RibbonTab { get; set; }

    /// <summary>
    /// Gets and sets the owning ribbon container instance.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual KryptonRibbonGroupContainer? RibbonContainer { get; set; }

    /// <summary>
    /// Gets the visible state of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract bool Visible { get; set; }

    /// <summary>
    /// Gets and sets the maximum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract GroupItemSize ItemSizeMaximum { get; set; }

    /// <summary>
    /// Gets and sets the minimum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract GroupItemSize ItemSizeMinimum { get; set; }

    /// <summary>
    /// Gets and sets the current item size.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract GroupItemSize ItemSizeCurrent { get; set; }

    /// <summary>
    /// Return the spacing gap between the provided previous item and this item.
    /// </summary>
    /// <param name="previousItem">Previous item.</param>
    /// <returns>Pixel gap between previous item and this item.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual int ItemGap(IRibbonGroupItem previousItem) =>
        // If the previous item is a group button cluster then we want 3 pixels
        previousItem is KryptonRibbonGroupCluster ? 3 : 1;

    /// <summary>
    /// Creates an appropriate view element for this item.
    /// </summary>
    /// <param name="ribbon">Reference to the owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying changes in display.</param>
    /// <returns>ViewBase derived instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract ViewBase CreateView(KryptonRibbon ribbon, NeedPaintHandler needPaint);

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [Bindable(true)]
    [DefaultValue(null)]
    public object? Tag
    {
        get => _tag;

        set
        {
            if (value != null && value != _tag)
            {
                _tag = value;
            }
        }
    }

    private bool ShouldSerializeTag() => Tag != null;

    private void ResetTag() => Tag = null;

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Control ToolTip Text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public abstract ToolTipValues ToolTipValues
    {
        // Return base objects tooltip
        get;
    }

    private bool ShouldSerializeToolTipValues() => !ToolTipValues.IsDefault;

    /// <summary>
    /// Resets the ToolTipValues property to its default value.
    /// </summary>
    public void ResetToolTipValues() => ToolTipValues.Reset();

    #endregion#endregion

    #region Protected
    /// <summary>
    /// Get a value indicating if all parent containers are visible.
    /// </summary>
    protected bool ChainVisible
    {
        get
        {
            KryptonRibbonGroupContainer? parent = RibbonContainer;

            // Search up chain until we find the top
            while (parent != null)
            {
                // If any parent is not visible, then abort
                if (!parent.Visible)
                {
                    return false;
                }

                // Move up a level
                parent = parent.RibbonContainer;
            }

            return true;
        }
    }
    #endregion

    #region Internal
    internal abstract bool ProcessCmdKey(ref Message msg, Keys keyData);

    #endregion

    #region IBindableComponent Members
    private BindingContext? _bindingContext;
    private ControlBindingsCollection? _dataBindings;

    /// <summary>
    /// 
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public BindingContext BindingContext
    {
        get => _bindingContext ??= [];
        set => _bindingContext = value;
    }

    /// <summary>
    ///     Retrieves the bindings for this control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category(@"Data")]
    [Description(@"ControlBindings")]
    [RefreshProperties(RefreshProperties.All)]
    [ParenthesizePropertyName(true)]
    public ControlBindingsCollection DataBindings => _dataBindings ??= new ControlBindingsCollection(this);

    #endregion
}