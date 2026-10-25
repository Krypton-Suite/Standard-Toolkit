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
/// Represents a ribbon group label.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupLabel), "ToolboxBitmaps.KryptonRibbonGroupLabel.bmp")]
[Designer(typeof(KryptonRibbonGroupLabelDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty("Text")]
public class KryptonRibbonGroupLabel : KryptonRibbonGroupItem
{
    #region Instance Fields
    private bool _visible;
    private bool _enabled;
    private Image? _imageSmall;
    private Image? _imageLarge;
    private string _textLine1;
    private string _textLine2;
    private GroupItemSize _itemSizeCurrent;
    private KryptonCommand? _command;
    private readonly NeedPaintHandler _needPaintDelegate;
    private readonly PaletteRibbonText _stateNormal;
    private readonly PaletteRibbonText _stateDisabled;

    #endregion

    #region Events
    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Ribbon")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Occurs when the design time context menu is requested.
    /// </summary>
    [Category(@"Design Time")]
    [Description(@"Occurs when the design time context menu is requested.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event MouseEventHandler? DesignTimeContextMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupLabel class.
    /// </summary>
    public KryptonRibbonGroupLabel()
    {
        // Default fields
        _visible = true;
        _enabled = true;
        _imageSmall = null;
        _imageLarge = null;
        _textLine1 = nameof(Label);
        _textLine2 = string.Empty;
        _itemSizeCurrent = GroupItemSize.Medium;

        // Create delegate fired by a change to one of the state palettes
        _needPaintDelegate = OnPaletteNeedPaint;

        // Create palette entries for customizing the label text color
        _stateNormal = new PaletteRibbonText(_needPaintDelegate);
        _stateDisabled = new PaletteRibbonText(_needPaintDelegate);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the small label image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Small label image.")]
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

    private bool ShouldSerializeImageSmall() => ImageSmall != null;

    /// <summary>
    /// Gets and sets the large label image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Large label image.")]
    [RefreshProperties(RefreshProperties.All)]
    public Image? ImageLarge
    {
        get => _imageLarge;

        set
        {
            if (_imageLarge != value)
            {
                _imageLarge = value;
                OnPropertyChanged(nameof(ImageLarge));
            }
        }
    }

    private bool ShouldSerializeImageLarge() => ImageLarge != null;

    /// <summary>
    /// Gets and sets the display text line 1 for the label.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Label display text line 1.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(nameof(Label))]
    public string TextLine1
    {
        get => _textLine1;

        set
        {
            // We never allow an empty text value
            if (string.IsNullOrEmpty(value))
            {
                value = nameof(Label);
            }

            if (value != _textLine1)
            {
                _textLine1 = value;
                OnPropertyChanged(nameof(TextLine1));
            }
        }
    }

    /// <summary>
    /// Gets and sets the display text line 2 for the label.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Label display text line 2.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
    public string TextLine2
    {
        get => _textLine2;

        set
        {
            if (value != _textLine2)
            {
                _textLine2 = value;
                OnPropertyChanged(nameof(TextLine2));
            }
        }
    }


    /// <summary>
    /// Gets and sets the visible state of the label.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the label is visible or hidden.")]
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
    /// Gets and sets the enabled state of the group label.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the group label is enabled.")]
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
    /// Gets access to the label text normal appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining label text normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !_stateNormal.IsDefault;

    /// <summary>
    /// Gets access to the label text disabled appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining label text disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the Wrapped Controls Tooltips.
    /// </summary>
    public override ToolTipValues ToolTipValues => _toolTipValues;


    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the group label.")]
    [DefaultValue(null)]
    public KryptonCommand? KryptonCommand
    {
        get => _command;

        set
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

    /// <summary>
    /// Gets and sets the maximum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMaximum
    {
        get => GroupItemSize.Large;
        set { }
    }

    /// <summary>
    /// Gets and sets the minimum allowed size of the item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GroupItemSize ItemSizeMinimum
    {
        get => GroupItemSize.Small;
        set { }
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
        return new ViewDrawRibbonGroupLabel(ribbon, this, needPaint);
    }

    /// <summary>
    /// Internal design time property.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ViewBase? LabelView { get; set; }

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
            case nameof(TextLine1):
                OnPropertyChanged(nameof(TextLine1));
                break;
            case "ExtraText":
                OnPropertyChanged(nameof(TextLine2));
                break;
            case nameof(ImageSmall):
                OnPropertyChanged(nameof(ImageSmall));
                break;
            case nameof(ImageLarge):
                OnPropertyChanged(nameof(ImageLarge));
                break;
            case nameof(Enabled):
                OnPropertyChanged(nameof(Enabled));
                break;
        }
    }

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region Internal
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    internal NeedPaintHandler? ViewPaintDelegate { get; set; }

    internal void OnDesignTimeContextMenu(MouseEventArgs e) => DesignTimeContextMenu?.Invoke(this, e);

    internal override bool ProcessCmdKey(ref Message msg, Keys keyData) =>
        // A label never has any command keys to process
        false;

    #endregion

    #region Implementation
    private void OnPaletteNeedPaint(object? sender, NeedLayoutEventArgs e) =>
        // Pass request onto the view provided paint delegate
        ViewPaintDelegate?.Invoke(this, e);
    #endregion
}