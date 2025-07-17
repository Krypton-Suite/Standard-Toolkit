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
/// Represents a single ribbon quick access toolbar entry.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonQATButton), "ToolboxBitmaps.KryptonRibbonQATButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Image))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public class KryptonRibbonQATButton : Component,
    IQuickAccessToolbarButton
{
    #region Static Fields
    private static readonly Image? _defaultImage = GenericImageResources.QATButtonDefault;
    #endregion

    #region Instance Fields
    private object? _tag;
    private Image? _image;
    private bool _visible;
    private bool _enabled;
    private string _text;
    private KryptonCommand? _command;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the quick access toolbar button has been clicked.
    /// </summary>
    public event EventHandler? Click;

    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonQATButton class.
    /// </summary>
    public KryptonRibbonQATButton()
    {
        // Default fields
        _image = _defaultImage;
        _visible = true;
        _enabled = true;
        _text = "QAT Button";
        ShortcutKeys = Keys.None;
        ToolTipImageTransparentColor = Color.Empty;
        ToolTipTitle = string.Empty;
        ToolTipBody = string.Empty;
        ToolTipStyle = LabelStyle.ToolTip;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the owning ribbon control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRibbon? Ribbon { get; private set; }

    /// <summary>
    /// Gets and sets the application button image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"Application button image.")]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Image
    {
        get => _image;

        set
        {
            if (_image != value)
            {
                if (value != null)
                {
                    // The image must be 16x16 or less in order to be Displayed on the
                    // quick access toolbar. So we reject anything bigger than 16x16.
                    if ((value.Width > 16) || (value.Height > 16))
                    {
                        throw new ArgumentOutOfRangeException(nameof(Image), @"Image must be 16x16 or smaller.");
                    }
                }

                _image = value;
                OnPropertyChanged(nameof(Image));

                // Only need to update display if we are visible
                if (Visible)
                {
                    Ribbon?.PerformNeedPaint(false);
                }
            }
        }
    }

    private bool ShouldSerializeImage() => Image != _defaultImage;

    /// <summary>
    /// Gets and sets the visible state of the ribbon quick access toolbar entry.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the ribbon quick access toolbar entry is visible or hidden.")]
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
                if (Ribbon != null)
                {
                    Ribbon.PerformNeedPaint(true);
                    Ribbon.UpdateQAT();
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
    /// Gets and sets the enabled state of the ribbon quick access toolbar entry.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines whether the ribbon quick access toolbar entry is enabled.")]
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

                // Must try and paint to show change
                if (Visible)
                {
                    Ribbon?.PerformNeedPaint(false);
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the display text of the quick access toolbar button.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"QAT button text.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("QAT Button")]
    public string Text
    {
        get => _text;

        set
        {
            // We never allow an empty text value
            if (string.IsNullOrEmpty(value))
            {
                value = @"QAT Button";
            }

            if (value != _text)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets and sets the shortcut key combination.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Shortcut key combination to fire click event of the quick access toolbar button.")]
    public Keys ShortcutKeys { get; set; }

    private bool ShouldSerializeShortcutKeys() => ShortcutKeys != Keys.None;

    /// <summary>
    /// Resets the ShortcutKeys property to its default value.
    /// </summary>
    public void ResetShortcutKeys() => ShortcutKeys = Keys.None;

    /// <summary>
    /// Gets and sets the tooltip label style for the quick access button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Tooltip style for the quick access toolbar button.")]
    [DefaultValue(typeof(LabelStyle), nameof(ToolTip))]
    public LabelStyle ToolTipStyle { get; set; }

    /// <summary>
    /// Gets and sets the image for the item ToolTip.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Display image associated ToolTip.")]
    [DefaultValue(null)]
    [Localizable(true)]
    public Image? ToolTipImage { get; set; }

    /// <summary>
    /// Gets and sets the color to draw as transparent in the ToolTipImage.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Color to draw as transparent in the ToolTipImage.")]
    [KryptonDefaultColor]
    [Localizable(true)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public Color ToolTipImageTransparentColor { get; set; }

    /// <summary>
    /// Gets and sets the title text for the item ToolTip.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Title text for use in associated ToolTip.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue("")]
    [Localizable(true)]
    public string ToolTipTitle { get; set; }

    /// <summary>
    /// Gets and sets the body text for the item ToolTip.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Body text for use in associated ToolTip.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue("")]
    [Localizable(true)]
    public string ToolTipBody { get; set; }

    #region ToolTipShadow
    /// <summary>
    /// Gets and sets the tooltip label style.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Button tooltip Shadow.")]
    [DefaultValue(true)]
    public bool ToolTipShadow { get; set; } = true; // Backward compatible -> "Material Design" suggests this to be false

    private bool ShouldSerializeToolTipShadow() => !ToolTipShadow;

    private void ResetToolTipShadow() => ToolTipShadow = true;
    #endregion

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the quick access toolbar button.")]
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

                // Only need to update display if we are visible
                if (Visible)
                {
                    Ribbon?.PerformNeedPaint(false);
                }
            }
        }
    }

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
    #endregion

    #region IQuickAccessToolbarButton
    /// <summary>
    /// Provides a back reference to the owning ribbon control instance.
    /// </summary>
    /// <param name="ribbon">Reference to owning instance.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void SetRibbon(KryptonRibbon? ribbon) => Ribbon = ribbon;

    /// <summary>
    /// Gets the entry image.
    /// </summary>
    /// <returns>Image value.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Image? GetImage() => KryptonCommand?.ImageSmall ?? Image;

    /// <summary>
    /// Gets the entry text.
    /// </summary>
    /// <returns>Text value.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string GetText() => KryptonCommand?.TextLine1 ?? Text;

    /// <summary>
    /// Gets the entry enabled state.
    /// </summary>
    /// <returns>Enabled value.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool GetEnabled() => KryptonCommand?.Enabled ?? Enabled;

    /// <summary>
    /// Gets the entry shortcut keys state.
    /// </summary>
    /// <returns>ShortcutKeys value.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Keys GetShortcutKeys() => ShortcutKeys;

    /// <summary>
    /// Gets the entry visible state.
    /// </summary>
    /// <returns>Visible value.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool GetVisible() => Visible;

    /// <summary>
    /// Sets a new value for the visible state.
    /// </summary>
    /// <param name="visible"></param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void SetVisible(bool visible) => Visible = visible;

    /// <summary>
    /// Gets the tooltip label style.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public LabelStyle GetToolTipStyle() => ToolTipStyle;

    /// <summary>
    /// Gets and sets the image for the item ToolTip.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Image? GetToolTipImage() => ToolTipImage!;

    /// <summary>
    /// Gets and sets the color to draw as transparent in the ToolTipImage.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Color GetToolTipImageTransparentColor() => ToolTipImageTransparentColor;

    /// <summary>
    /// Gets and sets the title text for the item ToolTip.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string GetToolTipTitle() => ToolTipTitle;

    /// <summary>
    /// Gets and sets the body text for the item ToolTip.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string GetToolTipBody() => ToolTipBody;

    /// <summary>Gets the tool tip shadow value.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool GetToolTipShadow() => ToolTipShadow;

    /// <summary>
    /// Generates a Click event for a button.
    /// </summary>
    public void PerformClick() => OnClick(EventArgs.Empty);
    #endregion

    #region Protected
    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected virtual void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var refresh = false;

        switch (e.PropertyName)
        {
            case nameof(Text):
                refresh = true;
                OnPropertyChanged(nameof(Text));
                break;
            case "ImageSmall":
                refresh = true;
                OnPropertyChanged(nameof(Image));
                break;
            case nameof(Enabled):
                refresh = true;
                OnPropertyChanged(nameof(Enabled));
                break;
        }

        if (refresh)
        {
            // Only need to update display if we are visible
            if (Visible)
            {
                Ribbon?.PerformNeedPaint(false);
            }
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnClick(EventArgs e)
    {
        // Perform processing that is common to any action that would dismiss
        // any popup controls such as the showing minimized group popup
        Ribbon?.ActionOccurred();

        Click?.Invoke(this, e);

        // Clicking the button should execute the associated command
        KryptonCommand?.PerformExecute();
    }

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}