#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a custom menu item for the themed system menu that can be configured in the designer.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ThemedSystemMenuItemValues : IComponent
{
    #region Instance Fields
    private string _text = string.Empty;
    private string _shortcut = string.Empty;
    private bool _enabled = true;
    private bool _visible = true;
    private bool _insertBeforeClose = true;
    private Image? _image;
    private KryptonCommand? _command;
    private ISite? _site;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the component is disposed.
    /// </summary>
    public event EventHandler? Disposed;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ThemedSystemMenuItem class.
    /// </summary>
    public ThemedSystemMenuItemValues()
    {
    }

    /// <summary>
    /// Initialize a new instance of the ThemedSystemMenuItem class.
    /// </summary>
    /// <param name="text">The text to display for the menu item.</param>
    public ThemedSystemMenuItemValues(string text)
    {
        _text = text ?? string.Empty;
    }

    /// <summary>
    /// Initialize a new instance of the ThemedSystemMenuItem class.
    /// </summary>
    /// <param name="text">The text to display for the menu item.</param>
    /// <param name="shortcut">The keyboard shortcut text.</param>
    public ThemedSystemMenuItemValues(string text, string shortcut)
    {
        _text = text ?? string.Empty;
        _shortcut = shortcut ?? string.Empty;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the text to display for the menu item.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The text to display for the menu item.")]
    [DefaultValue("")]
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
    /// Gets or sets the keyboard shortcut text (e.g., "Ctrl+S").
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The keyboard shortcut text to display (e.g., 'Ctrl+S').")]
    [DefaultValue("")]
    public string Shortcut
    {
        get => _shortcut;
        set
        {
            if (_shortcut != value)
            {
                _shortcut = value ?? string.Empty;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the menu item is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines whether the menu item is enabled and can be clicked.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the menu item is visible.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines whether the menu item is visible in the menu.")]
    [DefaultValue(true)]
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
    /// Gets or sets whether to insert this item before the Close item.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"If true, inserts this item before the Close item; otherwise adds it at the end.")]
    [DefaultValue(true)]
    public bool InsertBeforeClose
    {
        get => _insertBeforeClose;
        set
        {
            if (_insertBeforeClose != value)
            {
                _insertBeforeClose = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the image to display for the menu item.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The image to display next to the menu item text.")]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;
        set
        {
            if (_image != value)
            {
                _image = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the KryptonCommand associated with this menu item.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The KryptonCommand that will be executed when this menu item is clicked.")]
    [DefaultValue(null)]
    public KryptonCommand? Command
    {
        get => _command;
        set
        {
            if (_command != value)
            {
                _command = value;
                OnPropertyChanged();
            }
        }
    }
    #endregion

    #region IComponent
    /// <summary>
    /// Gets or sets the ISite associated with the IComponent.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ISite? Site
    {
        get => _site;
        set => _site = value;
    }

    /// <summary>
    /// Gets a value indicating whether the component can raise an event.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DesignMode => _site?.DesignMode ?? false;
    #endregion

    #region Public Methods
    /// <summary>
    /// Creates a KryptonContextMenuItem from this ThemedSystemMenuItem.
    /// </summary>
    /// <returns>A new KryptonContextMenuItem instance.</returns>
    public KryptonContextMenuItem CreateContextMenuItem()
    {
        var item = new KryptonContextMenuItem();

        // Set the text with shortcut if provided
        if (!string.IsNullOrEmpty(_shortcut))
        {
            item.Text = $"{_text}\t{_shortcut}";
        }
        else
        {
            item.Text = _text;
        }

        item.Enabled = _enabled;
        item.Image = _image;

        // Set the command if provided
        if (_command != null)
        {
            item.KryptonCommand = _command;
        }

        return item;
    }

    /// <summary>
    /// Returns a string representation of the menu item.
    /// </summary>
    /// <returns>A string containing the text and shortcut.</returns>
    public override string ToString()
    {
        if (!string.IsNullOrEmpty(_shortcut))
        {
            return $"{_text} ({_shortcut})";
        }
        return _text;
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    protected virtual void OnPropertyChanged()
    {
        // This can be extended to raise PropertyChanged events if needed
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _image?.Dispose();
            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion
}