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
/// Represents a single recent document entry in the ribbon application button menu.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonRecentDoc), "ToolboxBitmaps.KryptonRibbonRecentDoc.png")]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public class KryptonRibbonRecentDoc : Component
{
    #region Instance Fields
    private Image? _image;
    private Color _imageTransparentColor;
    private string _text;
    private string _extraText;
    private object? _tag;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the recent document item is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the recent document item is clicked.")]
    public event EventHandler? Click;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonRecentDoc class.
    /// </summary>
    public KryptonRibbonRecentDoc()
    {
        // Default fields
        _text = "Recent Document";
        _extraText = string.Empty;
        _imageTransparentColor = Color.Empty;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the main text for the recent document entry.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Main text for the recent document entry.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Recent Document")]
    public string Text
    {
        get => _text;

        set
        {
            // We never allow an empty text value
            if (string.IsNullOrEmpty(value))
            {
                value = @"Recent Document";
            }

            if (value != _text)
            {
                _text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the extra text for the recent document entry.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Extra text for the recent document entry.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
    public string ExtraText
    {
        get => _extraText;

        set
        {
            if (value != _extraText)
            {
                _extraText = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the recent document image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Image for the recent document entry.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;

        set => _image = value;
    }

    /// <summary>
    /// Gets and sets the image color to make transparent.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Image color to make transparent.")]
    [DefaultValue(typeof(Color), "")]
    [Localizable(true)]
    [Bindable(true)]
    public Color ImageTransparentColor
    {
        get => _imageTransparentColor;

        set
        {
            if (value != _imageTransparentColor)
            {
                _imageTransparentColor = value;
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
            }
        }
    }

    private bool ShouldSerializeTag() => Tag != null;

    private void ResetTag() => Tag = null;

    /// <summary>
    /// Generates a Click event for the component.
    /// </summary>
    public void PerformClick() => OnClick(EventArgs.Empty);
    #endregion

    #region Protected
    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);
    #endregion
}