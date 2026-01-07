#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Provides customizable data for the ribbon notification bar.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonRibbonNotificationBarData : INotifyPropertyChanged
{
    #region Instance Fields
    private bool _visible;
    private RibbonNotificationBarType _type;
    private string _text;
    private string _title;
    private Image? _icon;
    private bool _showIcon;
    private bool _showCloseButton;
    private bool _showActionButtons;
    private string[] _actionButtonTexts;
    private Image[]? _actionButtonImages;
    private Color _customBackColor;
    private Color _customForeColor;
    private Color _customBorderColor;
    private int _autoDismissSeconds;
    private Padding _padding;
    private int _height;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonNotificationBarData class.
    /// </summary>
    public KryptonRibbonNotificationBarData()
    {
        _visible = false;
        _type = RibbonNotificationBarType.Information;
        _text = string.Empty;
        _title = string.Empty;
        _icon = null;
        _showIcon = true;
        _showCloseButton = true;
        _showActionButtons = true;
        _actionButtonTexts = new[] { "Update now" };
        _actionButtonImages = null;
        _customBackColor = Color.FromArgb(255, 242, 204);
        _customForeColor = Color.Black;
        _customBorderColor = Color.FromArgb(255, 192, 0);
        _autoDismissSeconds = 0;
        _padding = new Padding(12, 8, 12, 8);
        _height = 0; // Auto-calculate
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => $"Visible: {_visible}, Type: {_type}, Text: {_text}";
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets whether the notification bar is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines whether the notification bar is visible.")]
    [DefaultValue(false)]
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the type of notification bar.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The type of notification bar which determines default colors.")]
    [DefaultValue(RibbonNotificationBarType.Information)]
    public RibbonNotificationBarType Type
    {
        get => _type;
        set
        {
            if (_type != value)
            {
                _type = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the main text message displayed in the notification bar.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The main text message displayed in the notification bar.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value ?? string.Empty;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the title text displayed before the main text (e.g., "UPDATES AVAILABLE").
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The title text displayed before the main text.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value ?? string.Empty;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the icon image displayed in the notification bar.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The icon image displayed in the notification bar.")]
    [DefaultValue(null)]
    public Image? Icon
    {
        get => _icon;
        set
        {
            if (_icon != value)
            {
                _icon = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether to show the icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines whether to show the icon.")]
    [DefaultValue(true)]
    public bool ShowIcon
    {
        get => _showIcon;
        set
        {
            if (_showIcon != value)
            {
                _showIcon = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether to show the close button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines whether to show the close button.")]
    [DefaultValue(true)]
    public bool ShowCloseButton
    {
        get => _showCloseButton;
        set
        {
            if (_showCloseButton != value)
            {
                _showCloseButton = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether to show action buttons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Determines whether to show action buttons.")]
    [DefaultValue(true)]
    public bool ShowActionButtons
    {
        get => _showActionButtons;
        set
        {
            if (_showActionButtons != value)
            {
                _showActionButtons = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the array of action button texts.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The array of action button texts.")]
    [DefaultValue(null)]
    [Localizable(true)]
    public string[] ActionButtonTexts
    {
        get => _actionButtonTexts;
        set
        {
            if (_actionButtonTexts != value)
            {
                _actionButtonTexts = value ?? Array.Empty<string>();
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the array of action button images (optional, can be null).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The array of action button images (optional).")]
    [DefaultValue(null)]
    public Image[]? ActionButtonImages
    {
        get => _actionButtonImages;
        set
        {
            if (_actionButtonImages != value)
            {
                _actionButtonImages = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the custom background color (used when Type is Custom).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The custom background color (used when Type is Custom).")]
    [DefaultValue(typeof(Color), "255, 242, 204")]
    public Color CustomBackColor
    {
        get => _customBackColor;
        set
        {
            if (_customBackColor != value)
            {
                _customBackColor = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the custom foreground color (used when Type is Custom).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The custom foreground color (used when Type is Custom).")]
    [DefaultValue(typeof(Color), "Black")]
    public Color CustomForeColor
    {
        get => _customForeColor;
        set
        {
            if (_customForeColor != value)
            {
                _customForeColor = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the custom border color (used when Type is Custom).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The custom border color (used when Type is Custom).")]
    [DefaultValue(typeof(Color), "255, 192, 0")]
    public Color CustomBorderColor
    {
        get => _customBorderColor;
        set
        {
            if (_customBorderColor != value)
            {
                _customBorderColor = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the number of seconds before the notification bar automatically dismisses (0 = never).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The number of seconds before the notification bar automatically dismisses (0 = never).")]
    [DefaultValue(0)]
    public int AutoDismissSeconds
    {
        get => _autoDismissSeconds;
        set
        {
            if (_autoDismissSeconds != value)
            {
                _autoDismissSeconds = Math.Max(0, value);
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the padding around the notification bar content.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The padding around the notification bar content.")]
    [DefaultValue(typeof(Padding), "12, 8, 12, 8")]
    public Padding Padding
    {
        get => _padding;
        set
        {
            if (_padding != value)
            {
                _padding = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the height of the notification bar (0 = auto-calculate based on content).
    /// </summary>
    [Category(@"Layout")]
    [Description(@"The height of the notification bar (0 = auto-calculate based on content).")]
    [DefaultValue(0)]
    public int Height
    {
        get => _height;
        set
        {
            if (_height != value)
            {
                _height = Math.Max(0, value);
                OnPropertyChanged();
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
