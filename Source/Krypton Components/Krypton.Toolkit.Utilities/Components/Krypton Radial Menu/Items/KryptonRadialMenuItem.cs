#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Standard command sector in a <see cref="KryptonRadialMenu"/>, with optional nested children and <see cref="KryptonCommand"/> binding.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(Click))]
public class KryptonRadialMenuItem : KryptonRadialMenuItemBase
{
    #region Nested

    private sealed class KryptonCommandReferenceConverter : ReferenceConverter
    {
        public KryptonCommandReferenceConverter()
            : base(typeof(KryptonCommand))
        {
        }
    }

    #endregion

    #region Instance Fields

    private string _text;
    private bool _checked;
    private bool _checkOnClick;
    private bool _autoClose;
    private bool _largeKryptonCommandImage;
    private KryptonCommand? _command;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the item is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the item is clicked.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the <see cref="Checked"/> property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the Checked property changes.")]
    public event EventHandler? CheckedChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuItem"/> class.
    /// </summary>
    public KryptonRadialMenuItem()
        : this(@"MenuItem", null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuItem"/> class.
    /// </summary>
    /// <param name="text">Initial text.</param>
    public KryptonRadialMenuItem(string text)
        : this(text, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuItem"/> class.
    /// </summary>
    /// <param name="text">Initial text.</param>
    /// <param name="clickHandler">Optional click handler.</param>
    public KryptonRadialMenuItem(string? text, EventHandler? clickHandler)
    {
        _text = text ?? string.Empty;
        _autoClose = true;
        Items = [];
        if (clickHandler != null)
        {
            Click += clickHandler;
        }
    }

    /// <inheritdoc />
    public override string ToString() => (string.IsNullOrEmpty(Text) ? "(Radial Menu Item)" : Text)!;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the item text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text displayed on the item sector.")]
    [DefaultValue(@"MenuItem")]
    [Localizable(true)]
    public string? Text
    {
        get => _text;
        set
        {
            value ??= string.Empty;
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets or sets whether a bound <see cref="KryptonCommand"/> uses <see cref="KryptonCommand.ImageLarge"/>
    /// instead of <see cref="KryptonCommand.ImageSmall"/> when <see cref="Image"/> is not set.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, uses KryptonCommand.ImageLarge instead of ImageSmall for the sector.")]
    [DefaultValue(false)]
    public bool LargeKryptonCommandImage
    {
        get => _largeKryptonCommandImage;
        set
        {
            if (_largeKryptonCommandImage != value)
            {
                _largeKryptonCommandImage = value;
                OnPropertyChanged(nameof(LargeKryptonCommandImage));
            }
        }
    }

    /// <summary>
    /// Gets the image drawn on the sector: explicit <see cref="Image"/>, otherwise the bound command image.
    /// </summary>
    [Browsable(false)]
    public Image? ResolveImage
    {
        get
        {
            if (Image != null)
            {
                return Image;
            }

            if (_command == null)
            {
                return null;
            }

            return _largeKryptonCommandImage ? _command.ImageLarge : _command.ImageSmall;
        }
    }

    /// <summary>
    /// Gets the transparent colour used when drawing <see cref="ResolveImage"/>.
    /// </summary>
    [Browsable(false)]
    public Color ResolveImageTransparentColor =>
        Image != null || _command == null ? ImageTransparentColor : _command.ImageTransparentColor;

    /// <summary>
    /// Gets the text drawn on the sector: bound command text when present, otherwise <see cref="Text"/>.
    /// </summary>
    [Browsable(false)]
    public string ResolveText =>
        _command != null && !string.IsNullOrEmpty(_command.Text) ? _command.Text : _text;

    /// <summary>
    /// Gets or sets whether the item is checked.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the item is checked.")]
    [DefaultValue(false)]
    public bool Checked
    {
        get => _checked;
        set
        {
            if (_checked != value)
            {
                _checked = value;
                OnPropertyChanged(nameof(Checked));
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether clicking the item toggles <see cref="Checked"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether clicking toggles the Checked state.")]
    [DefaultValue(false)]
    public bool CheckOnClick
    {
        get => _checkOnClick;
        set
        {
            if (_checkOnClick != value)
            {
                _checkOnClick = value;
                OnPropertyChanged(nameof(CheckOnClick));
            }
        }
    }

    /// <summary>
    /// Gets or sets whether activating a leaf item closes the menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether activating a leaf item closes the menu.")]
    [DefaultValue(true)]
    public bool AutoClose
    {
        get => _autoClose;
        set
        {
            if (_autoClose != value)
            {
                _autoClose = value;
                OnPropertyChanged(nameof(AutoClose));
            }
        }
    }

    /// <summary>
    /// Gets or sets the associated <see cref="KryptonCommand"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the item.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(KryptonCommandReferenceConverter))]
    public KryptonCommand? KryptonCommand
    {
        get => _command;
        set
        {
            if (!ReferenceEquals(_command, value))
            {
                _command = value;
                OnPropertyChanged(nameof(KryptonCommand));
            }
        }
    }

    /// <summary>
    /// Gets the nested child items for a submenu ring.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Child items shown when this item is opened as a submenu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonRadialMenuItemCollection Items { get; }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool HasChildren => Items.GetVisibleItems().Any();

    /// <summary>
    /// Raises the click sequence (optional check toggle, command, Click event).
    /// </summary>
    public void PerformClick()
    {
        if (!Enabled)
        {
            return;
        }

        if (CheckOnClick)
        {
            Checked = !Checked;
        }

        if (_command != null)
        {
            _command.PerformExecute();
        }

        OnClick(EventArgs.Empty);
    }

    #endregion

    #region Protected

    /// <summary>
    /// Raises the <see cref="Click"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);

    #endregion
}
