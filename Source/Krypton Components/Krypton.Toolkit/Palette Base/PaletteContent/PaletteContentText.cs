#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Implement storage for palette content text details.
/// </summary>
public class PaletteContentText : Storage
{
    #region Internal Classes
    private class InternalStorage
    {
        public Font? ContentTextFont;
        public PaletteTextHint ContentTextHint;
        public PaletteTextTrim ContentTextTrim;
        public PaletteTextHotkeyPrefix ContentTextPrefix;
        public PaletteRelativeAlign ContentTextH;
        public PaletteRelativeAlign ContentTextV;
        public PaletteRelativeAlign ContentTextMultiLineH;
        public InheritBool ContentTextMultiLine;
        public Color ContentTextColor1;
        public Color ContentTextColor2;
        public PaletteColorStyle ContentTextColorStyle;
        public PaletteRectangleAlign ContentTextColorAlign;
        public float ContentTextColorAngle;
        public Image? ContentTextImage;
        public PaletteImageStyle ContentTextImageStyle;
        public PaletteRectangleAlign ContentTextImageAlign;

        /// <summary>
        /// Initialize a new instance of the InternalStorage structure.
        /// </summary>
        public InternalStorage()
        {
            // Set to default values
            ContentTextHint = PaletteTextHint.Inherit;
            ContentTextTrim = PaletteTextTrim.Inherit;
            ContentTextPrefix = PaletteTextHotkeyPrefix.Inherit;
            ContentTextH = PaletteRelativeAlign.Inherit;
            ContentTextV = PaletteRelativeAlign.Inherit;
            ContentTextMultiLineH = PaletteRelativeAlign.Inherit;
            ContentTextMultiLine = InheritBool.Inherit;
            ContentTextColor1 = GlobalStaticValues.EMPTY_COLOR;
            ContentTextColor2 = GlobalStaticValues.EMPTY_COLOR;
            ContentTextColorStyle = PaletteColorStyle.Inherit;
            ContentTextColorAlign = PaletteRectangleAlign.Inherit;
            ContentTextColorAngle = -1;
            ContentTextImageStyle = PaletteImageStyle.Inherit;
            ContentTextImageAlign = PaletteRectangleAlign.Inherit;
        }

        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public bool IsDefault => (ContentTextFont == null) &&
                                 (ContentTextHint == PaletteTextHint.Inherit) &&
                                 (ContentTextTrim == PaletteTextTrim.Inherit) &&
                                 (ContentTextPrefix == PaletteTextHotkeyPrefix.Inherit) &&
                                 (ContentTextH == PaletteRelativeAlign.Inherit) &&
                                 (ContentTextV == PaletteRelativeAlign.Inherit) &&
                                 (ContentTextMultiLineH == PaletteRelativeAlign.Inherit) &&
                                 (ContentTextMultiLine == InheritBool.Inherit) &&
                                 (ContentTextColor1 == GlobalStaticValues.EMPTY_COLOR) &&
                                 (ContentTextColor2 == GlobalStaticValues.EMPTY_COLOR) &&
                                 (ContentTextColorStyle == PaletteColorStyle.Inherit) &&
                                 (ContentTextColorAlign == PaletteRectangleAlign.Inherit) &&
                                 (ContentTextColorAngle == -1) &&
                                 (ContentTextImage == null) &&
                                 (ContentTextImageStyle == PaletteImageStyle.Inherit) &&
                                 (ContentTextImageAlign == PaletteRectangleAlign.Inherit);
    }
    #endregion

    #region Instance Fields
    private InternalStorage? _storage;
    private Font? _factoryFont;
    private PaletteTextHint _factoryHint = PaletteTextHint.Inherit;
    private PaletteTextTrim _factoryTrim = PaletteTextTrim.Inherit;
    private PaletteTextHotkeyPrefix _factoryPrefix = PaletteTextHotkeyPrefix.Inherit;
    private PaletteRelativeAlign _factoryTextH = PaletteRelativeAlign.Inherit;
    private PaletteRelativeAlign _factoryTextV = PaletteRelativeAlign.Inherit;
    private PaletteRelativeAlign _factoryMultiLineH = PaletteRelativeAlign.Inherit;
    private InheritBool _factoryMultiLine = InheritBool.Inherit;
    private Color _factoryColor1 = GlobalStaticValues.EMPTY_COLOR;
    private Color _factoryColor2 = GlobalStaticValues.EMPTY_COLOR;
    private PaletteColorStyle _factoryColorStyle = PaletteColorStyle.Inherit;
    private PaletteRectangleAlign _factoryColorAlign = PaletteRectangleAlign.Inherit;
    private float _factoryColorAngle = -1;
    private Image? _factoryImage;
    private PaletteImageStyle _factoryImageStyle = PaletteImageStyle.Inherit;
    private PaletteRectangleAlign _factoryImageAlign = PaletteRectangleAlign.Inherit;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Browsable(false)]  // SKC: Probably a special case for not exposing this event in the designer....
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContentText class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteContentText(NeedPaintHandler? needPaint) =>
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault =>
        Font == _factoryFont &&
        Hint == _factoryHint &&
        Trim == _factoryTrim &&
        Prefix == _factoryPrefix &&
        TextH == _factoryTextH &&
        TextV == _factoryTextV &&
        MultiLineH == _factoryMultiLineH &&
        MultiLine == _factoryMultiLine &&
        Color1.Equals(_factoryColor1) &&
        Color2.Equals(_factoryColor2) &&
        ColorStyle == _factoryColorStyle &&
        ColorAlign == _factoryColorAlign &&
        ColorAngle.Equals(_factoryColorAngle) &&
        Image == _factoryImage &&
        ImageStyle == _factoryImageStyle &&
        ImageAlign == _factoryImageAlign;

    /// <summary>
    /// Treats the current values as the unset designer default.
    /// </summary>
    public void CaptureFactoryDefaults()
    {
        _factoryFont = Font;
        _factoryHint = Hint;
        _factoryTrim = Trim;
        _factoryPrefix = Prefix;
        _factoryTextH = TextH;
        _factoryTextV = TextV;
        _factoryMultiLineH = MultiLineH;
        _factoryMultiLine = MultiLine;
        _factoryColor1 = Color1;
        _factoryColor2 = Color2;
        _factoryColorStyle = ColorStyle;
        _factoryColorAlign = ColorAlign;
        _factoryColorAngle = ColorAngle;
        _factoryImage = Image;
        _factoryImageStyle = ImageStyle;
        _factoryImageAlign = ImageAlign;
    }

    #endregion

    #region Font
    /// <summary>
    /// Gets the font for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Font for drawing the content text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Font? Font
    {
        get => _storage?.ContentTextFont;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextFont != value)
                {
                    _storage.ContentTextFont = value;
                    OnPropertyChanged(nameof(Font));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != null)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextFont = value
                    };
                    OnPropertyChanged(nameof(Font));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region Hint
    /// <summary>
    /// Gets the text rendering hint for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Text rendering hint for the content text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteTextHint Hint
    {
        get => _storage?.ContentTextHint ?? PaletteTextHint.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextHint != value)
                {
                    _storage.ContentTextHint = value;
                    OnPropertyChanged(nameof(Hint));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteTextHint.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextHint = value
                    };
                    OnPropertyChanged(nameof(Hint));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region Trim
    /// <summary>
    /// Gets the text trimming for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Text trimming style for the content text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteTextTrim Trim
    {
        get => _storage?.ContentTextTrim ?? PaletteTextTrim.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextTrim != value)
                {
                    _storage.ContentTextTrim = value;
                    OnPropertyChanged(nameof(Trim));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteTextTrim.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextTrim = value
                    };
                    OnPropertyChanged(nameof(Trim));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region Prefix
    /// <summary>
    /// Gets the drawing used for prefix characters.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"How to draw prefix characters for the content text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteTextHotkeyPrefix Prefix
    {
        get => _storage?.ContentTextPrefix ?? PaletteTextHotkeyPrefix.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextPrefix != value)
                {
                    _storage.ContentTextPrefix = value;
                    OnPropertyChanged(nameof(Prefix));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteTextHotkeyPrefix.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextPrefix = value
                    };
                    OnPropertyChanged(nameof(Prefix));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region TextH
    /// <summary>
    /// Gets the horizontal relative alignment of the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative horizontal alignment of content text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRelativeAlign TextH
    {
        get => _storage?.ContentTextH ?? PaletteRelativeAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextH != value)
                {
                    _storage.ContentTextH = value;
                    OnPropertyChanged(nameof(TextH));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteRelativeAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextH = value
                    };
                    OnPropertyChanged(nameof(TextH));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region TextV
    /// <summary>
    /// Gets the vertical relative alignment of the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative vertical alignment of content text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRelativeAlign TextV
    {
        get => _storage?.ContentTextV ?? PaletteRelativeAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextV != value)
                {
                    _storage.ContentTextV = value;
                    OnPropertyChanged(nameof(TextV));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteRelativeAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextV = value
                    };
                    OnPropertyChanged(nameof(TextV));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region MultiLineH
    /// <summary>
    /// Gets the relative horizontal alignment of multiline content text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative horizontal alignment of multiline content text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRelativeAlign MultiLineH
    {
        get => _storage?.ContentTextMultiLineH ?? PaletteRelativeAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextMultiLineH != value)
                {
                    _storage.ContentTextMultiLineH = value;
                    OnPropertyChanged(nameof(MultiLineH));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteRelativeAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextMultiLineH = value
                    };
                    OnPropertyChanged(nameof(MultiLineH));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region MultiLine
    /// <summary>
    /// Gets the flag indicating if multiline text is allowed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Flag indicating if multiline text is allowed..")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual InheritBool MultiLine
    {
        get => _storage?.ContentTextMultiLine ?? InheritBool.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextMultiLine != value)
                {
                    _storage.ContentTextMultiLine = value;
                    OnPropertyChanged(nameof(MultiLine));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != InheritBool.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextMultiLine = value
                    };
                    OnPropertyChanged(nameof(MultiLine));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region Color1
    /// <summary>
    /// Gets and sets the first color for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Main color for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color Color1
    {
        get => _storage?.ContentTextColor1 ?? GlobalStaticValues.EMPTY_COLOR;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColor1 != value)
                {
                    _storage.ContentTextColor1 = value;
                    OnPropertyChanged(nameof(Color1));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != GlobalStaticValues.EMPTY_COLOR)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColor1 = value
                    };
                    OnPropertyChanged(nameof(Color1));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region Color2
    /// <summary>
    /// Gets and sets the second color for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Secondary color for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color Color2
    {
        get => _storage?.ContentTextColor2 ?? GlobalStaticValues.EMPTY_COLOR;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColor2 != value)
                {
                    _storage.ContentTextColor2 = value;
                    OnPropertyChanged(nameof(Color2));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != GlobalStaticValues.EMPTY_COLOR)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColor2 = value
                    };
                    OnPropertyChanged(nameof(Color2));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ColorStyle
    /// <summary>
    /// Gets and sets the color drawing style for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color drawing style for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteColorStyle ColorStyle
    {
        get => _storage?.ContentTextColorStyle ?? PaletteColorStyle.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColorStyle != value)
                {
                    _storage.ContentTextColorStyle = value;
                    OnPropertyChanged(nameof(ColorStyle));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteColorStyle.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColorStyle = value
                    };
                    OnPropertyChanged(nameof(ColorStyle));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ColorAlign
    /// <summary>
    /// Gets and set the color alignment for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color alignment style for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRectangleAlign ColorAlign
    {
        get => _storage?.ContentTextColorAlign ?? PaletteRectangleAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColorAlign != value)
                {
                    _storage.ContentTextColorAlign = value;
                    OnPropertyChanged(nameof(ColorAlign));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteRectangleAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColorAlign = value
                    };
                    OnPropertyChanged(nameof(ColorAlign));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ColorAngle
    /// <summary>
    /// Gets and sets the color angle for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color angle for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual float ColorAngle
    {
        get => _storage?.ContentTextColorAngle ?? -1f;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColorAngle != value)
                {
                    _storage.ContentTextColorAngle = value;
                    OnPropertyChanged(nameof(ColorAngle));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != -1f)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColorAngle = value
                    };
                    OnPropertyChanged(nameof(ColorAngle));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the image for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Image? Image
    {
        get => _storage?.ContentTextImage;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextImage != value)
                {
                    _storage.ContentTextImage = value;
                    OnPropertyChanged(nameof(Image));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != null)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextImage = value
                    };
                    OnPropertyChanged(nameof(Image));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ImageStyle
    /// <summary>
    /// Gets and sets the image style for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image style for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteImageStyle ImageStyle
    {
        get => _storage?.ContentTextImageStyle ?? PaletteImageStyle.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextImageStyle != value)
                {
                    _storage.ContentTextImageStyle = value;
                    OnPropertyChanged(nameof(ImageStyle));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteImageStyle.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextImageStyle = value
                    };
                    OnPropertyChanged(nameof(ImageStyle));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ImageAlign
    /// <summary>
    /// Gets and set the image alignment for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image alignment style for the text.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRectangleAlign ImageAlign
    {
        get => _storage?.ContentTextImageAlign ?? PaletteRectangleAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextImageAlign != value)
                {
                    _storage.ContentTextImageAlign = value;
                    OnPropertyChanged(nameof(ImageAlign));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteRectangleAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextImageAlign = value
                    };
                    OnPropertyChanged(nameof(ImageAlign));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ShouldSerialize
    private bool ShouldSerializeFont() => Font != _factoryFont;
    private void ResetFont() => Font = _factoryFont;
    private bool ShouldSerializeHint() => Hint != _factoryHint;
    private void ResetHint() => Hint = _factoryHint;
    private bool ShouldSerializeTrim() => Trim != _factoryTrim;
    private void ResetTrim() => Trim = _factoryTrim;
    private bool ShouldSerializePrefix() => Prefix != _factoryPrefix;
    private void ResetPrefix() => Prefix = _factoryPrefix;
    private bool ShouldSerializeTextH() => TextH != _factoryTextH;
    private void ResetTextH() => TextH = _factoryTextH;
    private bool ShouldSerializeTextV() => TextV != _factoryTextV;
    private void ResetTextV() => TextV = _factoryTextV;
    private bool ShouldSerializeMultiLineH() => MultiLineH != _factoryMultiLineH;
    private void ResetMultiLineH() => MultiLineH = _factoryMultiLineH;
    private bool ShouldSerializeMultiLine() => MultiLine != _factoryMultiLine;
    private void ResetMultiLine() => MultiLine = _factoryMultiLine;
    private bool ShouldSerializeColor1() => !Color1.Equals(_factoryColor1);
    private void ResetColor1() => Color1 = _factoryColor1;
    private bool ShouldSerializeColor2() => !Color2.Equals(_factoryColor2);
    private void ResetColor2() => Color2 = _factoryColor2;
    private bool ShouldSerializeColorStyle() => ColorStyle != _factoryColorStyle;
    private void ResetColorStyle() => ColorStyle = _factoryColorStyle;
    private bool ShouldSerializeColorAlign() => ColorAlign != _factoryColorAlign;
    private void ResetColorAlign() => ColorAlign = _factoryColorAlign;
    private bool ShouldSerializeColorAngle() => !ColorAngle.Equals(_factoryColorAngle);
    private void ResetColorAngle() => ColorAngle = _factoryColorAngle;
    private bool ShouldSerializeImage() => Image != _factoryImage;
    private void ResetImage() => Image = _factoryImage;
    private bool ShouldSerializeImageStyle() => ImageStyle != _factoryImageStyle;
    private void ResetImageStyle() => ImageStyle = _factoryImageStyle;
    private bool ShouldSerializeImageAlign() => ImageAlign != _factoryImageAlign;
    private void ResetImageAlign() => ImageAlign = _factoryImageAlign;
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="property">Name of the property changed.</param>
    protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    #endregion
}