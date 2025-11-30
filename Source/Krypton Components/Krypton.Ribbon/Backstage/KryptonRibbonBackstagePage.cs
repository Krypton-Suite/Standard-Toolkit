#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a page in the ribbon backstage view.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonRibbonBackstagePageDesigner))]
[Description(@"Represents a page in the ribbon backstage view.")]
public class KryptonRibbonBackstagePage : Component
{
    #region Instance Fields
    private string _text;
    private string _textTitle;
    private string _textDescription;
    private Image? _image;
    private Color _backColor;
    private bool _visible;
    private bool _enabled;
    private object? _tag;
    private Control? _contentPanel;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the page is selected.
    /// </summary>
    public event EventHandler? Selected;

    /// <summary>
    /// Occurs when the page is clicked.
    /// </summary>
    public event EventHandler? Click;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonBackstagePage class.
    /// </summary>
    public KryptonRibbonBackstagePage()
    {
        _text = "Page";
        _textTitle = string.Empty;
        _textDescription = string.Empty;
        _image = null;
        _backColor = Color.Empty;
        _visible = true;
        _enabled = true;
        _tag = null;
        _contentPanel = null;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonRibbonBackstagePage class with specified text.
    /// </summary>
    /// <param name="text">The text to display on the navigation button.</param>
    public KryptonRibbonBackstagePage(string text) : this()
    {
        _text = text ?? throw new ArgumentNullException(nameof(text));
    }

    /// <summary>
    /// Initialize a new instance of the KryptonRibbonBackstagePage class with text and content control.
    /// </summary>
    /// <param name="text">The text to display on the navigation button.</param>
    /// <param name="contentControl">The control to display as page content.</param>
    public KryptonRibbonBackstagePage(string text, Control contentControl) : this(text)
    {
        _contentPanel = contentControl ?? throw new ArgumentNullException(nameof(contentControl));
    }

    /// <summary>
    /// Initialize a new instance of the KryptonRibbonBackstagePage class with text, title and description.
    /// </summary>
    /// <param name="text">The text to display on the navigation button.</param>
    /// <param name="title">The title to display in the content area.</param>
    /// <param name="description">The description to display in the content area.</param>
    public KryptonRibbonBackstagePage(string text, string title, string description) : this(text)
    {
        _textTitle = title ?? string.Empty;
        _textDescription = description ?? string.Empty;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the text associated with the page.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Text associated with the page.")]
    [DefaultValue(@"Page")]
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// Gets and sets the title text for the page content area.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Title text for the page content area.")]
    [DefaultValue(@"")]
    public string TextTitle
    {
        get => _textTitle;
        set
        {
            if (_textTitle != value)
            {
                _textTitle = value;
                OnPropertyChanged(nameof(TextTitle));
            }
        }
    }

    /// <summary>
    /// Gets and sets the description text for the page content area.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Description text for the page content area.")]
    [DefaultValue(@"")]
    public string TextDescription
    {
        get => _textDescription;
        set
        {
            if (_textDescription != value)
            {
                _textDescription = value;
                OnPropertyChanged(nameof(TextDescription));
            }
        }
    }

    /// <summary>
    /// Gets and sets the image associated with the page.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Image associated with the page.")]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;
        set
        {
            if (_image != value)
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
    }

    /// <summary>
    /// Gets and sets the background color for the page content area.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Background color for the page content area.")]
    [DefaultValue(typeof(Color), "Empty")]
    public Color BackColor
    {
        get => _backColor;
        set
        {
            if (_backColor != value)
            {
                _backColor = value;
                OnPropertyChanged(nameof(BackColor));
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if the page is visible.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines if the page is visible.")]
    [DefaultValue(true)]
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if the page is enabled.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Determines if the page is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Bindable(true)]
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(StringConverter))]
    public object? Tag
    {
        get => _tag;
        set => _tag = value;
    }

    /// <summary>
    /// Gets and sets the control that provides the page content.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Control that provides the page content.")]
    [DefaultValue(null)]
    public Control? ContentPanel
    {
        get => _contentPanel;
        set
        {
            if (_contentPanel != value)
            {
                _contentPanel = value;
                OnPropertyChanged(nameof(ContentPanel));
            }
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Generates a string that represents the current object.
    /// </summary>
    /// <returns>String that represents the current object.</returns>
    public override string ToString() => Text;
    #endregion

    #region Protected Methods
    /// <summary>
    /// Raises the Selected event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelected(EventArgs e) => Selected?.Invoke(this, e);

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);

    /// <summary>
    /// Raises property changed notifications.
    /// </summary>
    /// <param name="propertyName">Name of the changed property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        // Notify any listeners
        PropertyChanged?.Invoke(propertyName);
    }

    /// <summary>
    /// Internal event for property changes.
    /// </summary>
    internal event Action<string>? PropertyChanged;
    #endregion

    #region Internal Methods
    /// <summary>
    /// Internal method to raise the Selected event.
    /// </summary>
    internal void PerformSelected() => OnSelected(EventArgs.Empty);

    /// <summary>
    /// Internal method to raise the Click event.
    /// </summary>
    internal void PerformClick() => OnClick(EventArgs.Empty);
    #endregion
}
